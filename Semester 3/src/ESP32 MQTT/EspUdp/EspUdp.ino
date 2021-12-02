#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

// Set WiFi credentials
#define WIFI_SSID "Xiaomi_A08C"
#define WIFI_PASS "MUHWNFBA"
#define UDP_PORT 4210

IPAddress broadcastIp(192, 168, 137, 255);
// UDP
WiFiUDP UDP;
char packet[255];
char reply[] = "Hello From Tianyi!";
String command = "test";

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

  // If packet received...
  int packetSize = UDP.parsePacket();
  if (packetSize) {
    Serial.print("Received packet! Size: ");
    Serial.println(packetSize);
    int len = UDP.read(packet, 255);
    if (len > 0)
    {
      packet[len] = '\0';
    }
    Serial.print("Packet received: ");
    Serial.println(packet);

    // Send return packet
    if ((String)packet == command)
    {
      Serial.println("Right command");
      //UDP.beginPacket(UDP.remoteIP(), UDP.remotePort());
      UDP.beginPacket(UDP.remoteIP(), UDP.remotePort());
      int i = 0;
      while (reply[i] != 0)
      {
        UDP.write((uint8_t)reply[i++]);
      }
      Serial.println(UDP.remoteIP());
      Serial.println(UDP.remotePort());
      UDP.endPacket();
    }
  }
}
