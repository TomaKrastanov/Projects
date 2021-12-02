#include <Wire.h> // Include wire library for I2C communication
#include <HardwareSerial.h> // Include hardware serial for UART communication

#define GetTempAndHumidityiditySensorAddress 0x44 // The I2C address of the temperature and humididty sensor
#define vocsensorAddress 0x5A // The I2C address of the voc sensor
#define greenLED 9 // The connected pin for the green LED
#define redLED 10 // The connected pin for the red LED

HardwareSerial zigBee (A1, A0); // Initialize the zigbee using UART
HardwareSerial co2 (A5, A4); // Initialize the co2 sensor using UART
unsigned char cmd[4] = {0x11, 0x01, 0x01, 0xED}; // Array of addressess needed for the command to requast of data from the co2 sensor
int zigbeeMode = 0; // Integer used to keep track if the sensor module is registered or not
uint16_t tvoc, lastTvoc; // Variables for the voc
int CO2level, lastCO2level; // Variables for the co2
float temperature, lastTemp; // Variables for the temperature
float humidity, lastHum; // Variables for the humididty
unsigned long previousMillis = 0; // Using millis instead of delay
String message, messageSplit[5]; // Variables used to read the messages that come from the ventilation box
String moduleID = "000D6F000E2AB8B7"; // The ID of the zigbee in the ventilation box
String sensorID[4] = {"&00&", "&01&", "&02&", "&03&"}; // Array of the sensor IDs
String sensorType[4] = {"TEMPERATURE", "HUMIDITY", "CO2", "VOC"};
String sensorValue[4]; // Array for the 4 different sensor values

void RegisterModule() // Function for the registartion command for a new module to the ventilation box
{
  zigBee.print("AT+UCAST:");
  zigBee.print("000D6F00108DCE37");
  zigBee.print(",#REG&");
  zigBee.print(moduleID);
  zigBee.print(sensorID[0]);
  zigBee.print(sensorType[0]);
  zigBee.print(sensorID[1]);
  zigBee.print(sensorType[1]);
  zigBee.print(sensorID[2]);
  zigBee.print(sensorType[2]);
  zigBee.print(sensorID[3]);
  zigBee.print(sensorType[3]);
  zigBee.print("$\r");
}

void SendData() // Function for the command for sending data to the ventilation box from all the sensors
{
  zigBee.print("AT+UCAST:");
  zigBee.print("000D6F00108DCE37");
  zigBee.print(",#SENDDATA&");
  zigBee.print(moduleID);
  zigBee.print(sensorID[0]);
  zigBee.print(sensorValue[0]);
  zigBee.print(sensorID[1]);
  zigBee.print(sensorValue[1]);
  zigBee.print(sensorID[2]);
  zigBee.print(sensorValue[2]);
  zigBee.print(sensorID[3]);
  zigBee.print(sensorValue[3]);
  zigBee.println("$\r");
}

String SplitString(String data, char separator, int index) // Function to split a string on a certain seperator character
{
  int found = 0;
  int strIndex[] = { 0, -1 };
  int maxIndex = data.length() - 1;

  for (int i = 0; i <= maxIndex && found <= index; i++) {
    if (data.charAt(i) == separator || i == maxIndex) {
      found++;
      strIndex[0] = strIndex[1] + 1;
      strIndex[1] = (i == maxIndex) ? i + 1 : i;
    }
  }
  return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}

void setup() {
  Wire.begin();
  Serial.begin(9600);
  co2.begin(9600);
  zigBee.begin(19200);
  zigBee.flush();
  // Set LEDs as outputs
  pinMode(greenLED, OUTPUT);
  pinMode(redLED, OUTPUT);
  digitalWrite(redLED, LOW); // Set the red LED on by default
  digitalWrite(greenLED, HIGH); // Set the green LED off by default

}



void GetTempAndHumidity() // Function for reading the incoming data from the sensor and turning it into actual values that can be sent to the ventilation box
{
  unsigned int data[6]; // Array for the incoming data

  // Start I2C Transmission
  Wire.beginTransmission(GetTempAndHumidityiditySensorAddress);
  // Send measurement command
  Wire.write(0x2C);
  Wire.write(0x06);
  // Stop I2C transmission
  Wire.endTransmission();
  //delay(500);

  // Request 6 bytes of data
  Wire.requestFrom(GetTempAndHumidityiditySensorAddress, 6);

  if (Wire.available() == 6) // If we recieve 6 bytes of data
  {
    for (int i = 0; i < 6; i++) // Loop through the bytes and add the to the array of data
      data[i] = Wire.read();
    temperature = ((((data[0] * 256.0) + data[1]) * 175) / 65535.0) - 45; // Formula to convert the data into degres celcius
    humidity = ((((data[3] * 256.0) + data[4]) * 100) / 65535.0); // Formula to convert the data into humidity levels

  }
  if (temperature <= 0 || humidity <= 0) // If the values we recieve are zero or lower set the value in the sensorValue array to 0 which would be read in the ventilation box as the module being desconnected
  {
    sensorValue[0] = "0";
    sensorValue[1] = "0";
  }
  else // Else set the regular values to the array
  {
    sensorValue[0] = temperature;
    sensorValue[1] = humidity;
  }

}

void GetCO2()
{
  unsigned int data[7]; // Array for ther incoming data
  co2.write(cmd, 4); // Send the cmd array to the sensor
  delay(500);
  if (co2.available() > 0) // If we have incoming data from the sensor
  {
    for (int i = 0; i < 7; i++) // Loop through it all and add the incoming data to the data array
      data[i] = co2.read();
    CO2level = data[3] * 256 + data[4]; // Formula for converting the incoming data into co2 ppm
    sensorValue[2] = CO2level;

  }
  else // If no data is incomming set the sensor value to 0 which will be treated as the sensor being disconnected
    sensorValue[2] = "0";
}

void GetVOC()
{
  unsigned int data[9]; // Array for the incoming data
  Wire.requestFrom(vocsensorAddress, 9); // Request 9 bytes from the VOC sensor
  if (Wire.available() == 9) // If 9 bytes are recieved
  {
    for (int i = 0; i < 9; i++) // Save those bytes in the data array
      data[i] = Wire.read();
  }
  tvoc = data[7] * 256 + data[8]; // Formula for the VOC levels
  if (tvoc <= 0) // If the voc is 0 or less set the sensor value as 0 which will be treated as the sensor being disconnected
    sensorValue[3] = "0";
  else // Else set the voc level in the sensor value array
    sensorValue[3] = tvoc;
}

void CheckSpike() // Function taht checks for spikes in the data readings
{
  if (temperature > (lastTemp + 5)) // Check if there is a spike in the temperature, if there is send the spike data
  {
    zigBee.print("AT+UCAST:");
    zigBee.print("000D6F00108DCE37");
    zigBee.print(",#SPIKE&");
    zigBee.print(moduleID);
    zigBee.print(sensorID[0]);
    zigBee.print(lastTemp);
    zigBee.println("$\r");
  }
  if (humidity > (lastHum + 10)) // Check if there is a spike in the humidity, if there is send the spike data
  {
    zigBee.print("AT+UCAST:");
    zigBee.print("000D6F00108DCE37");
    zigBee.print(",#SPIKE&");
    zigBee.print(moduleID);
    zigBee.print(sensorID[1]);
    zigBee.print(lastHum);
    zigBee.println("$\r");
  }
  if (CO2level > (lastCO2level + 500)) // Check if there is a spike in the co2 levels, if there is send the spike data
  {
    zigBee.print("AT+UCAST:");
    zigBee.print("000D6F00108DCE37");
    zigBee.print(",#SPIKE&");
    zigBee.print(moduleID);
    zigBee.print(sensorID[2]);
    zigBee.print(lastCO2level);
    zigBee.println("$\r");
  }
  if (tvoc > (lastTvoc + 200)) // Check if there is a spike in the voc levels, if there is send the spike data
  {
    zigBee.print("AT+UCAST:");
    zigBee.print("000D6F00108DCE37");
    zigBee.print(",#SPIKE&");
    zigBee.print(moduleID);
    zigBee.print(sensorID[3]);
    zigBee.print(lastCO2level);
    zigBee.println("$\r");
  }
}

void loop() {
  if (zigbeeMode == 0) // If the module is not registered
  {
    String response;
    RegisterModule(); // Call the registe module method

    if (zigBee.available() > 0); // If there is a response from the ventilation box
    {
      response = zigBee.readStringUntil('$'); // Read the response until the end character
      Serial.println(response);
    }
    response = SplitString(response, '=', 1); // Split the incoming message on the '=' character
    if (response == "#REGACK") // If we recieve an acknowledgement: turn off the red LED, turn on the green LED, and register the device
    {
      digitalWrite(redLED, HIGH);
      digitalWrite(greenLED, LOW);
      zigbeeMode = 1;
    }
  }

  unsigned long currentMillis = millis(); // Using millis instead of delays

  if (zigbeeMode == 1) // If module registered
  {
    // Call the methods to read form the sensors
    GetTempAndHumidity();
    GetCO2();
    GetVOC();
    CheckSpike(); // Check for spikes
    lastTemp = temperature;
    lastCO2level = CO2level;
    lastTvoc = tvoc;
    lastHum = humidity;
    if (currentMillis - previousMillis >= 10000) { // Send the aquired data from the sensors once every 10 seconds

      previousMillis = currentMillis;
      SendData();
    }
  }
}
