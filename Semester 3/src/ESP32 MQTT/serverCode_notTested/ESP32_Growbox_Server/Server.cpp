#include "Server.h"

GrowboxServer::GrowboxServer(IPAddress &broadcastAddr, WiFiUDP &UDP) : broadcastAddr(broadcastAddr), UDP(UDP)
{
  boxListNew = new growBox;
  numberOfBoxes = 0;
}

void GrowboxServer::Create()
{
  Serial.begin(115200);
  WiFi.begin(WIFI_SSID, WIFI_PASS);
  Serial.println("Connecting to wifi");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  setServerIP(WiFi.localIP());
  UDP.begin(UDP_PORT);
  Serial.println("Creation of Server completed!");
}
void GrowboxServer::setServerIP(IPAddress ip)
{
  SERVERIP = ip;
}
void GrowboxServer::setWatertankIP(IPAddress ip)
{
  waterTankIP = ip;
}
void GrowboxServer::waterLeak()
{
  Serial.println("in Water leak function");

  //need to be implemented. maybe send leak warning to dashboard
}
void GrowboxServer::setWaterTankData(String Data)
{
  waterTankData = Data;
}
void GrowboxServer::addBox(IPAddress IP)
{
  growBox *incomingBox = new growBox;
  if ((boxListNew != NULL) && (incomingBox != NULL))
  {
    incomingBox->next = NULL;
    growBox *temp = boxListNew;
    while (temp->next != NULL)
    {
      if (temp->ip == IP)
      {
        Serial.println("IP already exists");
        broadcastMessage(IP, "$Announce:01#");
        delete incomingBox;
        return;
      }
      else
      {
        temp = temp->next;
      }
    }
    temp->next = incomingBox;
    broadcastMessage(IP, "$Announce:00#");
    Serial.print("registered box IP: ");
    Serial.println(IP);
    while (boxListNew != NULL)
    {
      String existingIP = (String)boxListNew->ip;
      broadcastMessage(IP, "$AddNode:" + existingIP + ENDCHAR);
      unsigned long preMillisAck = millis();
      int ack = UDP.parsePacket();
      char receiveack[255];
      while (!ack)
      {
        if (millis() - preMillisAck > 5000)
        {
          Serial.println("timeout waiting for growbox");
          delete incomingBox;
          return;
        }
      }
      int len = UDP.read(receiveack, 255);
      if (len > 0)
      {
        receiveack[len] = '\0';
      }
      String incomingMsg = String(receiveack);
      if (incomingMsg.indexOf(ADDNODE) == 0)
      {
        incomingMsg.remove(incomingMsg.indexOf(ENDCHAR), 1);
        incomingMsg.remove(incomingMsg.indexOf(ADDNODE), 8);
        if (incomingMsg.indexOf(OK) == -1)
        {
          Serial.println("no respond from growbox");
          delete incomingBox;
          return;
        }
      }
    }
  }
  delete incomingBox;
}

void GrowboxServer::removeNode()
{
  // Store head node
  growBox *temp = boxListNew;
  growBox *prev = NULL;
  IPAddress thisIp = UDP.remoteIP();
  // If head node itself holds
  // the key to be deleted
  if (temp != NULL && temp->ip == thisIp)
  {
    boxListNew = temp->next; // Changed head
    delete temp;            // free old head
    Serial.println("IP removed");
    broadcastMessage(thisIp, "$Remove:00#");
    return;
  }

  // Else Search for the key to be deleted,
  // keep track of the previous node as we
  // need to change 'prev->next' */
  else
  {
    while (temp != NULL && temp->ip != thisIp)
    {
      prev = temp;
      temp = temp->next;
    }

    // If key was not present in linked list
    if (temp == NULL)
    {
      Serial.println("IP not registered");
      broadcastMessage(thisIp, "$Remove:01#");
      return;
    }
    // Unlink the node from linked list
    prev->next = temp->next;
    Serial.println("IP removed");
    broadcastMessage(thisIp, "$Remove:00#");
    // Free memory
    delete temp;
  }
}
void GrowboxServer::cycleCompleted(String endtime)
{
  growBox *temp = boxListNew;
  IPAddress thisIP = UDP.remoteIP();
  while (temp->ip != thisIP)
  {
    temp = temp->next;
    if (temp == NULL)
    {
      broadcastMessage(thisIP, "$CycleCompleted:01#");
      Serial.println("IP not registered");
      delete temp;
      return;
    }
  }
  delete temp;
  broadcastMessage(thisIP, "$CycleCompleted:00#");
}



void GrowboxServer::broadcastMessage(IPAddress ip, String reply)
// The server will send the address of other nodes to whoever it connects to.
{
  char sendMsg[255];
  reply.toCharArray(sendMsg, reply.length() + 1);
  UDP.beginPacket(ip, 4210);
  int i = 0;
  while (sendMsg[i] != '\0')
  {
    UDP.write((uint8_t)sendMsg[i++]);
  }
  i = 0;
  UDP.endPacket();
}

String GrowboxServer::SplitData(String data, char separator, int index)
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

void GrowboxServer::DistributeData(String data)
{
  boxListNew->humidity = SplitData(data, ':', 1);
  boxListNew->ldr = SplitData(data, ':', 2);
  boxListNew->temperature = SplitData(data, ':', 3);
  boxListNew->soilHumidity = SplitData(data, ':', 4);
  Serial.println(boxListNew->humidity);
  Serial.println(boxListNew->ldr);
  Serial.println(boxListNew->temperature);
  Serial.println(boxListNew->soilHumidity);
}
void GrowboxServer::sendClimateData(IPAddress ip, String Temp, String soilMoisture, String ldr)
{
  growBox *temp = boxListNew;
  while (temp->ip != ip)
  {
    temp = temp->next;
    if (temp == NULL)
    {
      broadcastMessage(ip, "Data:01#");
      Serial.println("IP not registered");
      delete temp;
      return;
    }
  }
  temp->temperature = Temp;
  temp->soilHumidity = soilMoisture;
  temp->ldr = ldr;
  delete temp;                         //need to be checked
  broadcastMessage(ip, "Data:00#");
}
void GrowboxServer::pauseProgram(IPAddress ip)
{
  String pauseProgram = PAUSE + ENDCHAR;
  broadcastMessage(ip, pauseProgram);
  unsigned long preMillisAck = millis();
  int ack = UDP.parsePacket();
  char receiveack[255];
  while (!ack)
  {
    if (millis() - preMillisAck > 5000)
    {
      Serial.println("timeout waiting for growbox");
      return;
    }
  }
  int len = UDP.read(receiveack, 255);
  if (len > 0)
  {
    receiveack[len] = '\0';
  }
  String incomingMsg = String(receiveack);
  if (incomingMsg.indexOf(PAUSE) == 0)
  {
    incomingMsg.remove(incomingMsg.indexOf(ENDCHAR), 1);
    incomingMsg.remove(incomingMsg.indexOf(PAUSE), 7);
    if (incomingMsg.indexOf("01") == 0)
    {
      Serial.println("Program paused, no error");
      //send to mqtt need to be implemented
      return;
    }
    else if (incomingMsg.indexOf("02") == 0)
    {
      Serial.println("No program assigned");
      //send to mqtt need to be implemented
      return;
    }
    else if (incomingMsg.indexOf("03") == 0)
    {
      Serial.println("Program already paused");
      //send to mqtt need to be implemented
      return;
    }
  }
}
void GrowboxServer::resumeProgram(IPAddress ip)
{
  String resumeProgram = RESUME + ENDCHAR;
  broadcastMessage(ip, resumeProgram);
  unsigned long preMillisAck = millis();
  int ack = UDP.parsePacket();
  char receiveack[255];
  while (!ack)
  {
    if (millis() - preMillisAck > 5000)
    {
      Serial.println("timeout waiting for growbox");
      return;
    }
  }
  int len = UDP.read(receiveack, 255);
  if (len > 0)
  {
    receiveack[len] = '\0';
  }
  String incomingMsg = String(receiveack);
  if (incomingMsg.indexOf(RESUME) == 0)
  {
    incomingMsg.remove(incomingMsg.indexOf(ENDCHAR), 1);
    incomingMsg.remove(incomingMsg.indexOf(RESUME), 7);
    if (incomingMsg.indexOf("01") == 0)
    {
      Serial.println("Program resumed, no error");
      //send to mqtt need to be implemented
      return;
    }
    else if (incomingMsg.indexOf("02") == 0)
    {
      Serial.println("No program assigned");
      //send to mqtt need to be implemented
      return;
    }
    else if (incomingMsg.indexOf("03") == 0)
    {
      Serial.println("Program already running");
      //send to mqtt need to be implemented
      return;
    }
  }
}
void GrowboxServer::sensorError(String sensorType, String errorType)
{
  IPAddress thisIP = UDP.remoteIP();
  growBox *temp = boxListNew;
  while (temp->ip != thisIP)
  {
    temp = temp->next;
    if (temp == NULL)
    {
      broadcastMessage(thisIP, "SensorError:01#");
      Serial.println("IP not registered");
      delete temp;
      return;
    }
  }
  Serial.println("Sensor error recorded");
  Serial.print("SensorType: ");
  Serial.println(sensorType);
  Serial.print("ErrorType: ");
  Serial.println(errorType);
  //need to be stored somewhere
  delete temp;
  return;
}
void GrowboxServer::GetJsonBuffer(String humidity, String ldr, String temperature, String soilHumidity)
{
  //  char buffer[BUF_LEN];
  StaticJsonDocument<BUF_LEN> doc;
  //  client.loop();
  // delay(1000);
  doc["humidity"] = humidity;
  doc["ldr"] = ldr;
  doc["temperature"] = temperature;
  doc["soilHumidity"] = soilHumidity;

  serializeJson(doc, msgBuffer);
}

void GrowboxServer::replyRequestProgramData()
{
  growBox *temp = boxListNew;
  IPAddress thisIP = UDP.remoteIP();
  while (temp->ip != thisIP)
  {
    temp = temp->next;
    if (temp == NULL)
    {
      broadcastMessage(thisIP, "$ReqProgSpec:00#");
      Serial.println("IP not registered");
      delete temp;
      return;
    }
  }
  String endtime, Temp, soilmoisture, lightintensity, lightcycle;
  endtime = temp->endtime;
  Temp = temp->temperature;
  soilmoisture = temp->soilHumidity;
  lightintensity = temp->ldr;
  lightcycle = temp->lightcycle;
  String reply = REQPROGSPEC + endtime + ":" + Temp + ":" + soilmoisture + ":" + lightintensity + ":" + lightcycle + ENDCHAR;
  broadcastMessage(thisIP, reply);
  return;
}


String GrowboxServer::getHumid()
{
  return boxListNew->humidity;
}
String GrowboxServer::getldr()
{
  return boxListNew->ldr;
}
String GrowboxServer::getTemp()
{
  return boxListNew->temperature;
}
String GrowboxServer::getsoilHumidity()
{
  return boxListNew->soilHumidity;
}
