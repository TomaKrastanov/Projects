#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
  
// Set WiFi credentials
#define WIFI_SSID "yourAP"
#define WIFI_PASS "yourPassword"
#define UDP_PORT 4210

IPAddress broadcastIp(192, 168, 4, 255);
// UDP
WiFiUDP UDP;
char packet[255];
char reply[] = "Packet received!";
  
void setup() {
  // Setup serial port
  Serial.begin(115200);
  Serial.println();
  
  // Begin WiFi
  WiFi.begin(WIFI_SSID, WIFI_PASS);
  
  // Connecting to WiFi...
  Serial.print("Connecting to ");
  Serial.print(WIFI_SSID);
  // Loop continuously while WiFi is not connected
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
  
  // Connected to WiFi
  Serial.println();
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
 
  // Begin listening to UDP port
  UDP.begin(UDP_PORT);
  Serial.print("Listening on UDP port ");
  Serial.println(UDP_PORT);
  
}
 
void loop() {
    UDP.beginPacket(broadcastIp, 4210);
    UDP.write(reply);
    UDP.endPacket();
    delay(1000);
}
