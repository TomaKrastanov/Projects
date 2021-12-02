#include "DHT.h"
#define DHTPIN 2     // Digital pin connected to the DHT sensor
#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321
DHT dht(DHTPIN, DHTTYPE);
float h = 0.0;
float t = 0.0;
unsigned long Pmillis = 0;
#define TFT_DC    7
#define TFT_RST   8
#define SCR_WD   240
#define SCR_HT   240   // 320 - to allow access to full 240x320 frame buffer
#include <SPI.h>
#include <Adafruit_GFX.h>
#include <Arduino_ST7789_Fast.h>
Arduino_ST7789 lcd = Arduino_ST7789(TFT_DC, TFT_RST);
void setup() {
  Serial.begin(9600);
  dht.begin();
  lcd.init(SCR_WD, SCR_HT);
  lcd.fillScreen(BLACK);
  lcd.setCursor(0, 0);
  lcd.setTextColor(WHITE, BLUE);
  lcd.setTextSize(3);
  lcd.println("DHT22 Test");
}

void loop() {
  if (Serial.available() > 0)
  {
    String incoming = Serial.readStringUntil('&');
    if (incoming == "readT")
    {
      Serial.print("#T");
      Serial.println(t);
    }
    else if (incoming == "readH")
    {
      Serial.print("#H");
      Serial.println(h);
    }
  }
  if (millis() - Pmillis > 500)
  {
    h = dht.readHumidity();
    t = dht.readTemperature();
    Pmillis = millis();
    //lcd.fillScreen(BLACK);
    lcd.setCursor(0, 0);
    lcd.setTextColor(WHITE, BLUE);
    lcd.setTextSize(3);
    lcd.println("DHT22 Data");
    lcd.println();
    lcd.println("Temperature: ");
    lcd.print(t);
    lcd.println(" Celsius");
    lcd.println();
    lcd.println("Humidity: ");
    lcd.print(h);
    lcd.println("%");
  }

}
