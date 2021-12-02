#include "Growbox.h"
#include "WaterSystemGrowBox.h"

IPAddress ServerIp(192, 168, 137, 5);
WiFiUDP UDP;

std::shared_ptr<Growbox> box;
std::shared_ptr<WaterSystemGrowBox> wateringSystem;

String backUpUDPMessage;
String receiveUDP();
String receiveSerial();
void removeGrowBoxReceive(String message);
void addGrowBoxReceive(String message);
void sendDataReceive(String message);
void announceGrowBoxReceiveReply(String message);
void cycleCompleteCommand(String message);
void saveWaterPrioOtherGrowBox(String message);
void waterPriorityReceive(String message);
void waterLevelReceive(String message);

//UDP
int packetSize;

void setup() {
  box = std::make_shared<Growbox>(ServerIp, UDP);
  wateringSystem = std::make_shared<WaterSystemGrowBox>(box);
  box->ConnectToServer();
}

void loop() {
  String msgUDP = receiveUDP();
  if(msgUDP != "")
  {
    backUpUDPMessage = msgUDP;
    Serial.println(msgUDP);
    String identifierUDP = box->SplitData(msgUDP, ':', 0) + SEPARATORCHAR;
    if(identifierUDP == REMOVE){
      removeGrowBoxReceive(msgUDP);
    } else if(identifierUDP == ADDNODE){
      //Serial.println("Trying to add node");
      addGrowBoxReceive(msgUDP);
    } else if(identifierUDP == ASSIGNPROGRAM){
      assignProgramReceive(msgUDP);
      waterPriorityReceive(msgUDP);
      setSoilMoisture(msgUDP);
    } else if(identifierUDP == ANNOUNCE){
      //announceGrowBoxReceiveReply(msgUDP);
    } else if(identifierUDP == WATERPRIORITY)
    {
      saveWaterPrioOtherGrowBox(msgUDP);
    }
  }
  String msgSerial = receiveSerial();
  if(msgSerial != "")
  {
    String identifierSerial = box->SplitData(msgSerial, ':', 0) + SEPARATORCHAR;
    if(identifierSerial == SENDDATA)
    {
      msgSerial += ENDCHAR;
      sendDataReceive(msgSerial);
      waterLevelReceive(msgSerial);
    } else if(identifierSerial == CYCLECOMPLETED){
      cycleCompleteCommand(msgSerial);
    }
  }
}

void setSoilMoisture(String message)
{
  wateringSystem->setRequiredWaterLevel((box->SplitData(message, ':', 4)).toInt());
}

void waterPriorityReceive(String message) {
  //int tempWaterLevel = (box->SplitData(incomingMsg, ':', 1)).toInt();
  int tempPrio = (box->SplitData(message, ':', 8)).toInt();
  wateringSystem->updateThisWaterPriority(tempPrio);
}

void waterLevelReceive(String message) {
  float waterLevel = (box->SplitData(message, ':', 4)).toFloat();
  wateringSystem->updateThisWaterLevel(waterLevel);
}

void saveWaterPrioOtherGrowBox(String message)
{
  Serial.println("water prio of other box received");
  int tempWaterLevel = (box->SplitData(message, ':', 1)).toInt();
  int tempPrio = (box->SplitData(message, ':', 2)).toInt();
  wateringSystem->saveWaterPrio(tempWaterLevel, tempPrio, UDP.remoteIP());
}

void announceGrowBoxReceiveReply(String message)
{
  
}

void cycleCompleteCommand(String message)
{
  box->programActive = false;
  box->sendMessageServer(message);
  Serial.println("Cycle complete command send");
}

void removeGrowBoxReceive(String message){
  if(box->Disconnect(box->SplitData(message, ':', 1)))
  {
    box->sendMessageServer("Remove:00#");
  } else {
    box->sendMessageServer("Remove:01#");
  }
}

void addGrowBoxReceive(String message) {
  if(box->addBox(box->SplitData(message, ':', 1)) != -1)
  {
    box->sendMessageServer("$AddNode:00#");
    Serial.println("Box added");
  } else {
    //box->sendMessageServer("$AddNode:01#");
    Serial.println("Failed to add box");
  }
  wateringSystem->sendWaterLevel();
}

void sendDataReceive(String message) {
  box->sendMessageServer(message);
  Serial.println("Send Send data");
}

void assignProgramReceive(String message)
{
  box->programActive = true;
  Serial2.println(message);
  Serial2.flush();
  Serial.println("Send program assignment");
}


String receiveUDP()
{
  char packet[255];
  String incomingMsgLocal = "";
  int packetSize = UDP.parsePacket();
  if (packetSize)
  { // Communication through wifi
    int len = UDP.read(packet, 255);

    if (len > 0)
      packet[len] = '\0';

    incomingMsgLocal = String(packet);          // Receive msg through UDP and turn to a string
    incomingMsgLocal.remove(incomingMsgLocal.indexOf(ENDCHAR), 1);  // Remove ENDCHAR '#' for com protocol
  }
  return incomingMsgLocal;
}

String receiveSerial()
{
  String incomingMsgLocal = "";
  if (Serial2.available() > 0)
  { 
    incomingMsgLocal = Serial2.readStringUntil(ENDCHAR);
  }
  return incomingMsgLocal;
}
