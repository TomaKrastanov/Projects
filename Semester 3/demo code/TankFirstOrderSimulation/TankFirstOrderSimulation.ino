
#include <Display.h>
#include <Wire.h>
// Fontys ICT-Tech
// O. Figaroa
// DT-IRT1  04-2018.
// RichShield included for testing 12-2020
const int ledRed     =   4;
const int ledYellow  =   7;
// Global variables of Process
double fBOutput = 0;
double fBInput = 0;
double sy; // 1st derivative
double Tconstant = 15; // Initial
//
//Global variables of PID Controller
//Global variables of PID Controller
double Perr = 0;   // e[k]  current error (SetPoint - ProcessOut]
double Perr1 = 0;  // e[k-1]
double Perr2 = 0;  // e[k-2]
double SetPoint = 0;
bool toggle_setp = true;
double MV_out, MV_out1; //  u[k], u[k-1]
double MV_raw;
bool firstScan = true;

String Comm;
int Res = 10, preRes = 10;
int button1State       = 1;
int prevbutton1State   = 1;
long lastDebounceTime1 = 0;
unsigned long DEBOUNCE_DELAY = 15;
//
const int Potmeter  = 14; // Res
const int KEY1      = 8;
const int KEY2      = 9;
const int MINRES = 1;
const int MAXRES = 5;  //Higher value gives larger spikes in water level when turning the potential meter

#define I2C_ADDRESS 42

//   ====== Function Prototypes    ===========
void DiscretePID(double SetPoint, double ProcessOut);
void FirstOrderProcess(int r);
void _InitInterrupt(void);
void requestHandler();
void receiveHandler();

void setup() {
  pinMode(ledRed, OUTPUT);
  pinMode(ledYellow, OUTPUT);
  pinMode(Potmeter, INPUT);
  pinMode(KEY1, INPUT_PULLUP);   // Right Button richshield
  pinMode(KEY2, INPUT_PULLUP);   // Left Button richshield
  _InitInterrupt();
  Serial.begin(9600);
  Wire.begin(I2C_ADDRESS);
  Wire.onRequest(requestHandler);
  Wire.onReceive(receiveHandler);
}

void loop() {
  /*
     Potentiometer Input low - high 10 ..1010 instead of 0..1023
  */
  Res = map(analogRead(Potmeter), 10, 1010, MINRES, MAXRES); // ValveQo control
  /*
     (10 =< Res <=50)
     R = 10/Res    R << on opening valves to sub-stations
     Res changes on Commands
  */
  bool comm_er = false;
  if (Serial.available()) {
    Comm = Serial.readStringUntil('\n');
    if (Comm == "SetPoint+") {
      SetPoint++;
    } else if (Comm == "SetPoint-") {
      SetPoint--;
    } else if (Comm == "NewSubst+") {
      Res++;
    } else if (Comm == "NewSubst-") {
      Res--;
    } else {
      comm_er = true;
    }
    if (comm_er) {
      Serial.println("WrongCommand " + String(Comm) + " received.");
    } else {
      Serial.println("Command " + String(Comm) + " received.");
    }
    if (Res > MAXRES) {
      Res = MAXRES;
    }
    if (Res < MINRES) {
      Res = MINRES;
    }
  }
  int readKEY1 = digitalRead(KEY1);
  if (readKEY1 != prevbutton1State) {
    // reset the debouncing timer
    lastDebounceTime1 = millis();
  }
  if ((millis() - lastDebounceTime1) > DEBOUNCE_DELAY) { // non blocking delay
    if (readKEY1 != button1State) {
      button1State = readKEY1;
      if (button1State == 0) {
        //   One Shot SetPoint   5 or 0
        if (toggle_setp) {
          SetPoint = 5;   //
        } else {
          SetPoint = 0;   //
        }
        toggle_setp = !toggle_setp;
        //
      }
    }
  }
  prevbutton1State = readKEY1;
  /*
    // For Serial Monitor or Serial plot
     Remove the comments sign from Serial.prints
  */
  //
  Serial.print(SetPoint); // Setpoint
  Serial.print(" ");
  Serial.print(fBOutput); // Process Output
  Serial.print(" ");
  Serial.println(MV_out); // Manipulated Value (Output PID) Input Flow Qi =cubic meters per second
  //  Serial.print("    ");
  //  Serial.print(Res);
  //  Serial.print(" ");
  //  Serial.println(Tconstant);
  // Display Output on 7 segment display of RichShield
  Display.show((float)fBOutput);

  // Control Saturation
  if (MV_raw >= 1.5) {
    digitalWrite(ledYellow, HIGH);
  } else {
    digitalWrite(ledYellow, LOW);
  }
} // END OF LOOP

// ================= Below: user Functions ==================================//
/*
   10 milliseconds timer Interrupt
*/
void _InitInterrupt()
{
  cli();//stop interrupts
  TCCR2A = 0;// set entire TCCR1A register to 0
  TCCR2B = 0;// same for TCCR1B
  TCNT2  = 0;//initialize counter value to 0
  // set compare match register for f = 100Hz increments
  // equivalent to sampletime = 10 ms
  OCR2A = 155;// = (16*10^6) / (1*64 ) - 1 (must be <65536)
  // turn on CTC mode

  TCCR2A |= COM2A1 | COM2A0;
  
  TCCR2B |= (1 << WGM12);
  // Set CS12, CS11 and CS10 bits for 64 prescaler
  TCCR2B |= (1 << CS12) | (1 << CS10);   // /64 From prescaler
  // enable timer compare interrupt
  TIMSK2 |= (1 << OCIE2A);
  sei();//allow interrupts

} //end _init Interrupt

/*
   Interrupt vector of Timer 1
   these functions must be executed with a sample time of 10 milliseconds
   PID Controller
   FirstOrderProcess
*/

ISR(TIMER2_COMPA_vect) {
  //timer1 interrupt 100Hz
  // Excuting FirstOrder Simulation
  DiscretePID(SetPoint, fBOutput);
  FirstOrderProcess(Res);
}
/*
   Euler Emulation of 1st Order with 0.01 sec sampletime.
      R=10/r
                Gain*R
    Process = ------------
                RC.s +1
*/
void FirstOrderProcess(int r)
// First Order Simulation
{
  double Gain = 15 * (10 / (double)r);
  Tconstant = 15 * (10 / (double)r);
  double Tsample = 0.01;
  // Euler Emulation 1st Order with 0.01 sec sampletime.
  sy  = ( fBInput * Gain / Tconstant ) - ( fBOutput / Tconstant ) ;
  fBOutput  = fBOutput + ( sy * Tsample ) ;
}

void DiscretePID(double SetPoint, double ProcessOut)
/*
   Discrete (Advanced-) PID Controller Algorithm.(velocity PID )
   Trapezodial on Integration and Backward Euler on Derivative.
   Parameters (will be) obtained by Matlab/Simulink  by PID Tuning
   Check PowerPoint presentation: IA intro module 5 extra.
   Manual Mode ( Open loop ) not implemented yet
   YOU MIGHT CONSIDER TO PROGRAM YOUR OWN PID ALGORITHM IN HERE....
*/
{
  double Kp = 0.1165 ;
  double Ki = 0.009948 ;
  double Kd = 0.00 ;
  double Tsample = 0.01;
  if (firstScan == true ) {
    MV_out = 0;   // offset = 0
  }
  firstScan = false;
  Perr =  SetPoint - ProcessOut ;                        // Error e[k]
  // Difference equation of the PID Controller
  double A = ( Kp + ( Ki * ( Tsample / 2 ) ) + ( Kd / Tsample ) ) ;
  double B = ( - Kp + ( Ki * ( Tsample / 2 ) ) - ( 2 * Kd / Tsample ) ) ;
  double C = Kd / Tsample ;

  // (* Calculates Mv_out *)
  // MV_out =  Kp * Perr; // P-Control only!     u[k]
  MV_out = MV_out1 + A * Perr + B * Perr1 + C * Perr2 ; // PI, PD or PID-Control  u[k]
  MV_raw = MV_out;// unsaturated Control output
  // (* Saturation : Limit the Control Signal +/- MAX_SATURATION  *)
  //
  if (MV_out >= 1.5)
  {
    MV_out = 1.5;
  }
  if (MV_out <= 0)
  {
    MV_out = 0;
  }
  fBInput = MV_out;    // To Process input u[k]
  //* Update variables: *//
  MV_out1 = MV_out;      // u[k-1]
  Perr2 = Perr1;         // e[k-2]
  Perr1 = Perr;          // e[k-1]
}

void requestHandler()
{
  char tempBuffer[5];
  Wire.write(dtostrf(fBOutput,0,3, tempBuffer));
  Wire.write((uint8_t)SetPoint);
}

void receiveHandler()
{
  while(Wire.available())
  { 
    SetPoint = (uint8_t)Wire.read();
  }
}
