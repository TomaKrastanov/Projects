/* Code for esp32 being the intermediary of the Server and the Mega with sensors gathering data.
   Finish until now: Basic structure of communication protocol
*/

#include "Growbox.h"
#include "WaterSystemGrowBox.h"

IPAddress ServerIp(192, 168, 137, 157);
WiFiUDP UDP;
//Growbox box(ServerIp, UDP);
std::shared_ptr<Growbox> box;
std::shared_ptr<WaterSystemGrowBox> wateringSystem;

String receiveUDPmsg();

//UDP
char packet[255];
String incomingMsg;
int packetSize;

state_gbox currentState = ANNOUNCEPRESENCE;
state_gbox prevState = IDLE;

void setup()
{
  box = std::make_shared<Growbox>(ServerIp, UDP);
  wateringSystem = std::make_shared<WaterSystemGrowBox>(box);
}

void loop()
{

  switch (currentState)
  {
    case ANNOUNCEPRESENCE:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println("ANNOUNCE PRESENCE");
        box->ConnectToServer();
        prevState = currentState;
      }
      incomingMsg = receiveUDPmsg();

      if (incomingMsg == "$Announce:00")
        currentState = IDLE;
      else if (incomingMsg == "$Announce:01")
        currentState = IDLE;
      else if (incomingMsg == "$Announce:02")
        prevState = IDLE; // Try again
      break;
    case IDLE:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println("IDLE");
        prevState = currentState;
      }
      //Serial.println("im here");
      incomingMsg = receiveUDPmsg();
      //Serial.println(incomingMsg);
      if (incomingMsg.indexOf(REMOVE) == 0)
      { // TODO
        currentState = DISCONNECT;
      }
      else if (incomingMsg == PAUSE)
      { // TODO
        currentState = PAUSED;
      }
      else if (incomingMsg.indexOf(ADDNODE) == 0)
      { // Registering other growboxes
        currentState = ADDBOX;
      }
      else if (incomingMsg.indexOf(ASSIGNPROGRAM) == 0)
      {
        Serial.println("check");
        currentState = ASSIGNP;
      }
      else if (incomingMsg.indexOf(SENDDATA) == 0)
      { // UART - sends data to server from simulation
        currentState = SENDATA;
      }
      else if (incomingMsg.indexOf(WATERPRIORITY) == 0)
      {
        int tempWaterLevel = (box->SplitData(incomingMsg, ':', 1)).toInt();
        int tempPrio = (box->SplitData(incomingMsg, ':', 2)).toInt();
        wateringSystem->saveWaterPrio(tempWaterLevel, tempPrio, UDP.remoteIP());
      }
      break;
    case SENDATA:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println("SENDDATA");
        // wateringSystem->updateThisWaterLevel((box->SplitData(incomingMsg, ':', 3)).toInt());
        box->sendMessageServer(incomingMsg);
        prevState = currentState;
      }

      incomingMsg = receiveUDPmsg();

      if (incomingMsg == "$SendData:00")
      {
        Serial2.println(incomingMsg + ENDCHAR);
        currentState = IDLE; // previousState
      }
      else if (incomingMsg == "$SendData:01")
        Serial2.println(incomingMsg + ENDCHAR);

      break;
    case ADDBOX:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println("ADDBOX");

        if (box->addBox(incomingMsg.substring(strlen(ADDNODE))))
        {
          Serial.println(incomingMsg);
          incomingMsg.remove(incomingMsg.indexOf(ADDNODE), 9);
          if (incomingMsg != WiFi.localIP().toString())
            box->boxList->ip = box->str2IP(incomingMsg);
          box->sendMessageServer("$AddNode:00#");
        }
        else
          box->sendMessageServer("$AddNode:01#");

        prevState = currentState;
      }
      currentState = IDLE;
      break;
    case ASSIGNP:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println(incomingMsg);
        //wateringSystem->updateThisWaterPriority((box->SplitData(incomingMsg, ':', 6)).toInt());
        Serial2.println(incomingMsg);
        prevState = currentState;
      }
      incomingMsg = receiveUDPmsg();

      if (incomingMsg == "$Program:00")
      { // Send command to MEGA and continue receiving messages
        Serial.println("check2");
        box->sendMessageServer("Program:00" + ENDCHAR);
        currentState = IDLE; // previousState
      }
      else if (incomingMsg == "$Program:01")
      {
        box->sendMessageServer("Program:01" + ENDCHAR);
        currentState = IDLE; // previousState
      }
      else if (incomingMsg == "$Program:02")
      {
        box->sendMessageServer("Program:02" + ENDCHAR);
        currentState = IDLE; // previousState
      }
      break;
    case PAUSED:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial2.println(incomingMsg + ENDCHAR);
        prevState = currentState;
      }
      incomingMsg = receiveUDPmsg();

      if (incomingMsg == RESUME)
      { // Send command to MEGA and continue receiving messages
        Serial2.println(incomingMsg + ENDCHAR);
        currentState = IDLE; // previousState
      }
      break;
    case CYCLECOMPLETED:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        String completeMsg = "$CycleCompleted:" + box->localIP.toString() + ENDCHAR;
        box->sendMessageServer(completeMsg);
        prevState = currentState;
      }
      incomingMsg = receiveUDPmsg();

      if (incomingMsg == "$CycleCompleted:00")
      { // Send command to MEGA and continue receiving messages
        Serial2.println("$CycleCompleted:00" + ENDCHAR);
        currentState = IDLE; // previousState
      }
      else if (incomingMsg == "$CycleCompleted:01")
        Serial2.println("$CycleCompleted:01" + ENDCHAR);

      break;
    case WATERVAL:
      // ----------------------- TODO -------------------------------
      break;
    case WATERPRIO:
      // ----------------------- TODO -------------------------------
      break;
    case DISCONNECT:
      // ----------------------- DONE -------------------------------
      if (prevState != currentState)
      {
        Serial.println("REMOVE");

        if (box->Disconnect(incomingMsg.substring(strlen(REMOVE))))
          box->sendMessageServer("Remove:00#");
        else
          box->sendMessageServer("Remove:01#");

        prevState = currentState;
      }
      currentState = IDLE;
      break;
    default:
      break;
  }
}

String receiveUDPmsg()
{
  String incomingMsgLocal = "";
  if (Serial2.available() > 0)
  { // UART COM WITH SIMULATION
    //Serial.println('S');
    incomingMsgLocal = Serial2.readStringUntil(ENDCHAR);
    return incomingMsgLocal;
  }
  else
  { // UDP COM WITH SERVER
    // Serial.println('U');
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
}
