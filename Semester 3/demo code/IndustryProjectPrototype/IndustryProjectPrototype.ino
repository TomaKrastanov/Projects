/*****ShowData******/
int dhtData = 0;
float ldrData = 0, tempData = 0;

/********Delay*********/
unsigned long previousMillis;
const long interval = 1000;
unsigned long previousMillis1 = 0;
int interval1 = 2000;

/*******DHT22********/
#include <dht.h>
#define dht22_pin 12
dht DHT;

/********LDR********/
#define ldr_pin A2
int ldrRawValue;
float resistance_sensor, lux, luxFinalValue;

/***********SoilMoisture***********/
# define soil_pin A9
int trigger = 300;
float soilVal;

/******NTC*******/
#define ntc_pin A1
int ntcRawValue;
const int ntc_r25 = 10000, ntc_constant = 3950;
float resistance, temperature;

/*****Display*****/
#include <LiquidCrystal.h>
const int rs = 49, en = 45, d4 = 46, d5 = 33, d6 = 44, d7 = 22;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

/*
  #include <Wire.h>
  #include <SFE_MicroOLED.h>
  #define display_pin 1
  #define reset_pin 9
  MicroOLED Oled(reset_pin, display_pin);
*/

/*********LED*******/
#define ledY 7
#define ledB 6
#define ledG 5
#define ledR 4

/****Button*****/
#define btn1 8
int btn1State = LOW, lastBtn1State = LOW, reading;

/****Debounce*****/
unsigned long lastDebounceTime = 0, debounceDelay = 50;

/********ModeSelect*******/
int mode = 0;

void setup()
{
  // Wire.begin();
  lcd.begin(16, 2);
  lcd.clear();
  Serial.begin(9600);
  Serial2.begin(115200);
  pinMode(dht22_pin, INPUT);
  pinMode(ldr_pin, INPUT);
  pinMode(ntc_pin, INPUT);
  pinMode(soil_pin, INPUT);
  pinMode(ledY, OUTPUT);
  pinMode(ledB, OUTPUT);
  pinMode(ledG, OUTPUT);
  pinMode(ledR, OUTPUT);
  pinMode(btn1, INPUT_PULLUP);
  digitalWrite(ledY, LOW);
  digitalWrite(ledB, LOW);
  digitalWrite(ledG, LOW);
  digitalWrite(ledR, LOW);
}

bool flag = false;

void loop()
{
  internalSetup();
  unsigned long currentMillis = millis();
  ShowData(Mode());
  if (currentMillis - previousMillis >= interval)
  {
    Dht22();
    Ldr();
    Temp();
    SendData();
    previousMillis = currentMillis;
  }
}

void internalSetup()
{
  reading = digitalRead(btn1);
  DHT.read11(dht22_pin);
  ldrRawValue = analogRead(ldr_pin);
  ntcRawValue = analogRead(ntc_pin);
}

int Dht22()
{
  int dhtFinalValue = DHT.humidity;
  return dhtFinalValue;
}


float Ldr()
{
  resistance_sensor = (float)(1023 - ldrRawValue) * 10 / ldrRawValue;
  lux = 325 * pow(resistance_sensor, -1.4);
  luxFinalValue = lux / 100;
  return luxFinalValue;
}

float Temp()
{
  resistance   = (float)ntcRawValue * ntc_r25 / (1023 - ntcRawValue);
  temperature  = 1 / (log(resistance / ntc_r25) / ntc_constant + 1 / 298.15) - 273.15;
  return temperature;
}

float Soil()
{
  if (analogRead(soil_pin) <= 0)
  {
    Serial.println("Disc");
    return -1;
  }
  soilVal =  analogRead(soil_pin);
  return soilVal;
}

int Mode()
{
  if (reading != lastBtn1State)
    lastDebounceTime = millis();

  if ((millis() - lastDebounceTime) > debounceDelay)
  {
    if (reading != btn1State)
    {
      btn1State = reading;

      if (btn1State == LOW)
      {
        mode++;
        if (mode == 5)
          mode = 0;
      }
    }
  }
  lastBtn1State = reading;
  return mode;
}

void ShowData(int Mode)
{
  switch (Mode)
  {
    case 1:
      {
        digitalWrite(ledR, LOW);
        digitalWrite(ledB, HIGH);
        String humMsg = "HUM " + String(Dht22()) + "%";
        lcd.clear();
        lcd.print(humMsg);
        /*
          Oled.clear(PAGE);
          String humMsg = "HUM " + String(Dht11()) + "%";
          Oled.setCursor(0, 0);
          Oled.print(humMsg);
          Oled.display();
        */
      }
      break;
    case 2:
      {
        digitalWrite(ledB, LOW);
        digitalWrite(ledY, HIGH);
        String ldrMsg = "LUX " + String(Ldr());
        lcd.clear();
        lcd.print(ldrMsg);
        /*
          Oled.clear(PAGE);
          String ldrMsg = "LUX " + String(Ldr());
          Oled.setCursor(0, 20);
          Oled.print(ldrMsg);
          Oled.display();
        */
      }
      break;
    case 3:
      {
        digitalWrite(ledY, LOW);
        digitalWrite(ledR, HIGH);
        String tempMsg = "TEMP " + String(Temp());
        lcd.clear();
        lcd.print(tempMsg);
        /*
          Oled.clear(PAGE);
          String tempMsg = "TEMP " + String(Temp());
          Oled.setCursor(0, 40);
          Oled.print(tempMsg);
          Oled.display();
        */
      }
      break;
    case 4:
      lcd.clear();
      lcd.print("Moisture Sensor Value:");
      lcd.setCursor(0, 1);
      lcd.println(Soil());
      break;
    default:
      digitalWrite(ledY, LOW);
      digitalWrite(ledB, LOW);
      digitalWrite(ledG, LOW);
      digitalWrite(ledR, LOW);
      lcd.clear();
      lcd.print("Press button to");
      lcd.setCursor(0, 1);
      lcd.print("change mode");
      break;
  }
}

void SendData()
{
  dhtData = Dht22();
  ldrData = Ldr();
  tempData = Temp();
  soilVal = Soil();

  
  String message = "$SendData:" + String(dhtData) + ':' + String(ldrData) + ':' + String(tempData) + ':' + String(soilVal) + '#';
  Serial.println(message);
  Serial2.println(message);
}
