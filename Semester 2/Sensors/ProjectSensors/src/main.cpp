#include <Arduino.h>
#include "Adafruit_CCS811.h"
#include <SoftwareSerial.h>
#include <WEMOS_SHT3X.h>

Adafruit_CCS811 ccs;
SHT3X sht30(0x45);
#define pwmPin 10
SoftwareSerial mySerial(7, 6); // RX, TX
unsigned INTERVAL = 5000;
unsigned startTime = 0;
const unsigned long CONN_TIMEOUT = 6000;
const int TIMEOUT = 6000;
//const unsigned long INTERVALD = 100;

float temperature = 0, humidity = 0;

String msg;

//enum Acknowledgement ReadAcknowledge();
enum Acknowledgement
{
  UNKNOWN,
  ACK,
  NACK
  // TIMEDOUT
};
enum State
{
  BOOTUP,
  READCO2UART,
  READCO2PWM,
  PRINTCO2MHS,
  READVOC,
  PRINTVOC,
  READTEMP
};
State state = BOOTUP;

// gas concentration reading
byte cmd[9] = {0xFF, 0x01, 0x86, 0x00, 0x00, 0x00, 0x00, 0x00, 0x79};

const uint8_t CMD_MR1000[] = {0xff, 0x01, 0x99, 0x00, 0x00, 0x00, 0x03, 0xE8, 0x7B};
const uint8_t CMD_MR2000[] = {0xff, 0x01, 0x99, 0x00, 0x00, 0x00, 0x07, 0xD0, 0x8B};
const uint8_t CMD_MR3000[] = {0xff, 0x01, 0x99, 0x00, 0x00, 0x00, 0x0B, 0xB8, 0xA3};
const uint8_t CMD_MR5000[] = {0xff, 0x01, 0x99, 0x00, 0x00, 0x00, 0x13, 0x88, 0xCB};

unsigned char response[9];
unsigned long th, tl, ppm, ppm2, ppm3 = 0;
unsigned long voc;
unsigned long pwmPeriod = 0;
unsigned int measurementRange = 0;
bool connected = false;
unsigned long tlTime = 0;
unsigned long msgReceivedTime = 0;

enum MeasuremenModes
{
  MR1000,
  MR2000,
  MR3000,
  MR5000
};

int ReadCO2UART();
//int ReadCO2PWM();
void printCO2();
bool CalibrateVOCSensor();
void printReadings();
unsigned long GetPeriodPWM();
void SetMeasurementRange(enum MeasuremenModes range);
void ReadTempSensor();
bool ReadTemperature(float *temperature, float *humidity);
bool IsDisconnected();
bool CheckConnection();
void myDelay(int del);
void printTempHumi();

void setup()
{
  Serial.begin(9600);
  mySerial.begin(9600);
  pinMode(pwmPin, INPUT);
  startTime = millis();
  Serial.setTimeout(500); // Set the timeout for Serial port.

  if (!ccs.begin())
  {
    Serial.println("Failed to start sensor! Please check your wiring.");
    while (1)
      ;
  }

  SetMeasurementRange(MR1000); // MR1000, MR2000, MR3000, MR5000

  //while(!ccs.available()); // Wait until css is ready!
  CalibrateVOCSensor();

  //pwmPeriod = GetPeriodPWM();
}

String ReadCommand()
{
  String msg = mySerial.readStringUntil('\n');
  return msg;
}

void SendCO2Measurements(int ppm)
{
  mySerial.println(ppm);
}
void SendVOCMeasurements(int voc){
  mySerial.println(ccs.getTVOC());
  //Serial.print(ccs.getTVOC());
}
void SendTempMeasurements(float temperature, float humidity){
  mySerial.println(temperature);
  mySerial.println(humidity);
}
void SendHumidityMeasurement(float humidity){
  mySerial.println(humidity);
}

void SendNotAcknowledge()
{
  mySerial.println("NACK");
}

enum Acknowledgement ReadAcknowledge()
{
  msgReceivedTime = millis();
  //if (millis() - msgReceivedTime < CONN_TIMEOUT)
  // {
  if (mySerial.available())
  {
    char c = mySerial.read();
    msgReceivedTime = millis();
    connected = true;
    if (c == '$')
    { // begin token.
      msg = "";
    }
    else
    {
      if (c == '#')
      {
        if (msg == "ACK")
        {
          return ACK;
        }
        else
        {
          if (msg == "NACK")
          {
            return NACK;
          }
          else
          {
            return UNKNOWN;
          }
        }
      }
      else
      {
        msg = msg + c; // creating message.
      }
    }
  }
  //}
  // At this point there is a timeout.
  connected = false;
  return UNKNOWN;
}

void loop()
{
  bool answer;
 unsigned long current_time = millis();

  //Measure CO2 at time intervals.
  if (current_time - tlTime > 100)
  {
    ppm = ReadCO2UART();
    printCO2();
    voc = CalibrateVOCSensor();
    printReadings();
    printTempHumi();
    tlTime = current_time;
  }


  // Service the commands.
  if (mySerial.available())
  {
    String command = ReadCommand();
    if (command == "GetCO2Measurements")
    {
      SendCO2Measurements(ppm); // = ackowledge.
    }
      if (command == "GetVOCMeasurements")
    {
      SendVOCMeasurements(ccs.getTVOC()); // = ackowledge.
    }
      if (command == "GetTempMeasurements")
    {
      SendTempMeasurements(temperature, humidity); // = ackowledge.
    }
    else if (command == "")
    {
      // There is a timeout whilew receiving the command.
      SendNotAcknowledge(); // = not ackowledge.
    }
  }

 
}
int ReadCO2UART()
{
  // USING MH-Z19B SENSOR;
  mySerial.write(cmd, 9);
  mySerial.readBytes(response, 9);
  unsigned int responseHigh = (unsigned int)response[2];
  unsigned int responseLow = (unsigned int)response[3];
  return (256 * responseHigh) + responseLow;
}

void SetMeasurementRange(enum MeasuremenModes range)
{
  switch (range)
  {
  case MR1000:
    mySerial.write(CMD_MR1000, 9);
    measurementRange = 1000;
    break;
  case MR2000:
    mySerial.write(CMD_MR2000, 9);
    measurementRange = 2000;
    break;
  case MR3000:
    mySerial.write(CMD_MR3000, 9);
    measurementRange = 3000;
    break;
  case MR5000:
    mySerial.write(CMD_MR5000, 9);
    measurementRange = 5000;
    break;
  }
}

void printCO2()
{
  // USING MH-Z19B SENSOR;

  Serial.println("USING MH-Z19B SENSOR");
  Serial.print("CO2 via UART is: "); // ppm(UART)
  Serial.println(ppm);               // ppm(UART)

  //Serial.print("CO2 via PWM with ");
  //Serial.print(measurementRange);
 // Serial.print("ppm as limit is: "); // ppm2(PWM) ith 2000ppm as limit
 // Serial.println(ppm2);              // ppm2(PWM) ith 2000ppm as limit
                                     // ppm3(PWN) with 5000ppm as limit
  Serial.println("-----------");
}

bool CalibrateVOCSensor()
{
  // USING CCS811 SENSOR
  //calibrate temperature sensor
  if (ccs.available()){
    float temp = ccs.calculateTemperature();
   
    ccs.setTempOffset(temp - 25.0);
  }
 return true; 
}

void printReadings()
{
  // USING CCS811 SENSOR

  if (ccs.available())
  {
    //float temp = ccs.calculateTemperature();
    if (!ccs.readData())
    {
      Serial.println("USING CCS811 SENSOR");
     // Serial.print("CO2: ");
     // Serial.print(ccs.geteCO2());
      Serial.print("ppm, TVOC: ");
      Serial.print(ccs.getTVOC());
      Serial.print("ppb");
      Serial.println();
    }
    else
    {
      Serial.println("ERROR!");
      while (1)
        ;
    }
  }
}

bool ReadTemperature(float *temperature, float *humidity)
{

  if (sht30.get() == 0)
  {
    
    *temperature = sht30.cTemp;
    *humidity = sht30.humidity;
   
    return true;
  }
  else
  {
    //Serial.println("Error!");
    return false;
  }
}

// checking if system is disconnedted
bool IsDisconnected()
{
  return !connected;
}
// checking if connected or not
bool CheckConnection()
{
  if (millis() - msgReceivedTime > TIMEOUT)
  {
    return false;
  }
  else
  {
    return true;
  }
}

void printTempHumi(){

  
  bool result = ReadTemperature(&temperature, &humidity);
  if (result)
  {
    Serial.print("Temp = ");
    Serial.print(temperature);
    Serial.print(" Humidity = ");
    Serial.println(humidity);
  }

}

