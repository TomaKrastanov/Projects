#include "Server.h"

IPAddress broadcastAddress(192, 168, 31, 255);
WiFiUDP UDP;
GrowboxServer server(broadcastAddress, UDP);

EspMQTTClient client
(
  "test.mosquitto.org", // MQTT Broker
  1883,              // The MQTT port, default to 1883. this line can be omitted
  "Server"     // Client name that uniquely identify your device
);

void setup()
{
  server.Create();
}

void onConnectionEstablished() {
  client.subscribe("GrowboxServer/commands", [] (const String & payload)  {
    Serial.println(payload);
  });
}

void loop() {
  client.loop();
  char packet[255];
  int packetSize = UDP.parsePacket();
  if (packetSize)
  {
    Serial.println("UDP message received!");
    int len = UDP.read(packet, 255);
    if (len > 0)
    {
      packet[len] = '\0';
    }
    String incomingMsg = String(packet);
    Serial.print("Incoming UDP message is: ");
    Serial.println(incomingMsg);
    incomingMsg.remove(incomingMsg.indexOf(ENDCHAR), 1);
    if (incomingMsg.indexOf(ANNOUNCE) == 0)
    { // After growbox announces it self, send add node msg
      Serial.println("ANNOUNCE received");
      server.addBox(UDP.remoteIP());
    }
    else if (incomingMsg.indexOf(SENDDATA) == 0)
    {
      Serial.println("SENDDATA received");
      server.DistributeData(incomingMsg);
      server.GetJsonBuffer(server.getHumid(), server.getldr(), server.getTemp(), server.getsoilHumidity());
      client.publish("growbox/data", server.msgBuffer);
    }
    else if (incomingMsg.indexOf(REQPROGSPEC) == 0)
    {
      Serial.println("REQPROGSPEC received");
      incomingMsg.remove(incomingMsg.indexOf(REQPROGSPEC), 13);
      server.replyRequestProgramData();
    }
    else if (incomingMsg.indexOf(CYCLECOMPLETED) == 0)
    {
      Serial.println("CYCLECOMPLETED received");
      incomingMsg.remove(incomingMsg.indexOf(CYCLECOMPLETED), 16);
      server.cycleCompleted(incomingMsg);
      String reply = CYCLECOMPLETED + UDP.remoteIP() + incomingMsg + ENDCHAR;
      client.publish("GrowboxServer/completed", reply);
    }
    else if (incomingMsg.indexOf(SENDCLIMATEDATA) == 0)
    {
      Serial.println("SENDCLIMATEDATA received");
      incomingMsg.remove(incomingMsg.indexOf(SENDCLIMATEDATA), 6);
      String Temperature = server.SplitData(incomingMsg, ':', 1);
      String SoilMoisture = server.SplitData(incomingMsg, ':', 2);
      String ldr = server.SplitData(incomingMsg, ':', 3);
      server.sendClimateData(UDP.remoteIP(), Temperature, SoilMoisture, ldr);
    }
    else if (incomingMsg.indexOf(SENSORERROR) == 0)
    {
      Serial.println("SENSORERROR received");
      incomingMsg.remove(incomingMsg.indexOf(SENSORERROR), 13);
      String sensorType = server.SplitData(incomingMsg, ':', 1);
      String errorType = server.SplitData(incomingMsg, ':', 2);
      server.sensorError(sensorType, errorType);
    }
    else if (incomingMsg.indexOf(WATERLEAK) == 0)
    {
      Serial.println("WATERLEAK received");
      incomingMsg.remove(incomingMsg.indexOf(WATERLEAK), 11);
      server.waterLeak();
    }
    else if (incomingMsg.indexOf(WATERTANKANNOUNCE) == 0)
    {
      Serial.println("WATERTANKANNOUNCE received");
      incomingMsg.remove(incomingMsg.indexOf(WATERTANKANNOUNCE), 19);
      server.setWatertankIP(UDP.remoteIP());
    }
    else if (incomingMsg.indexOf(WATERTANKDATA) == 0)
    {
      Serial.println("WATERTANKDATA received");
      incomingMsg.remove(incomingMsg.indexOf(WATERTANKDATA), 19);
      String waterLevel = server.SplitData(incomingMsg, ':', 1);
      server.setWaterTankData(waterLevel);
    }
  }
}
