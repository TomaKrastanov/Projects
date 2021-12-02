#include "GrowBox.h"

Growbox::Growbox(IPAddress &ServerIp, WiFiUDP &UDP) : ServerIp(ServerIp), UDP(UDP)
{
  boxList = NULL;
  numberOfBoxes = 0;
}

void Growbox::ConnectToServer()
{
  Serial.begin(115200);
  Serial2.begin(115200);
  // Begin WiFi
  WiFi.begin(WIFI_SSID, WIFI_PASS);

  // Connecting to WiFi...
  Serial.print("Connecting to ");
  Serial.print(WIFI_SSID);

  // Loop continuously while WiFi is not connected
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }

  // Connected to WiFi
  Serial.println();
  Serial.print("Connected! IP address: ");
  localIPAddress = WiFi.localIP();
  Serial.println(localIPAddress);

  // Announce itself on connection to server
  String anounceMsg = ANNOUNCE + WiFi.localIP().toString() + "#";
  sendMessageServer(anounceMsg);

  // Begin listening to UDP port
  UDP.begin(UDP_PORT);
  Serial.print("Listening on UDP port ");
  Serial.println(UDP_PORT);
}

void Growbox::sendMessageServer(String reply)
// The server will send the address of other nodes to whoever it connects to.
{
  Serial.print("IM SENDING A MESSAGE NOW: ");
  Serial.print(reply);
  char sendMsg[255];
  reply.toCharArray(sendMsg, reply.length() + 1);
  UDP.beginPacket(ServerIp, 4210);
  int i = 0;
  while (sendMsg[i] != '\0')
  {
    UDP.write((uint8_t)sendMsg[i++]);
  }
  i = 0;
  UDP.endPacket();
}

int Growbox::addBox(String ip)
{
  Serial.print("ip address:  ");
  Serial.println(ip);
  IPAddress tempIP = str2IP(ip);
  gbAddress *temp = boxList;
  if(tempIP == localIPAddress)
  {
    return 0;
  }
  if(temp == NULL)
  {
    boxList = new gbAddress;
    boxList->ip = tempIP;
    Serial.println("NEW GROWBOX IS ADDED");
    return 1;
  }
  if(temp->ip == tempIP)
  {
    return -1;
  }
  if(temp->next != NULL) {
    while(temp->next != NULL)
    {
      if(temp->ip == tempIP)
      {
        return -1;
      }
      temp = temp->next;
    }
  }
  temp->next = new gbAddress;
  temp->next->ip = tempIP;
  Serial.println("NEW GROWBOX IS ADDED");
  return 1;
}

int Growbox::Disconnect(String IP)
{
  gbAddress *temp = boxList;
  IPAddress compareVal;
  if (compareVal.fromString(IP) & boxList != NULL)
  {
    while (temp->next != NULL)
    {
      if (temp->next->ip == compareVal)
      {
        gbAddress *del = temp->next;
        temp->next = del->next;
        Serial.print("Removing box with IP: "); Serial.println(del->ip);
        delete del;
        return 1;
      }
      else
        temp = temp->next;
    }
  }
  return -1;
}

void Growbox::SendMessage(String message, IPAddress ip, int port)
{
  char sendMsg[255];
  message.toCharArray(sendMsg, message.length() + 1);
  UDP.beginPacket(ip, port);
  int i = 0;
  while (sendMsg[i] != '\0')
  {
    UDP.write((uint8_t)sendMsg[i++]);
  }
  UDP.endPacket();
}

void Growbox::DistributeWaterLevel(gbAddress *thisGrowBox)
{
  if(!programActive) {
    return;
  }
  gbAddress *temp = boxList;
  Serial.println("Sending watering level");
  while(temp != NULL)
  {
    String message = WATERPRIORITY;
    message += thisGrowBox->waterLevel;
    message += SEPARATORCHAR;
    message += thisGrowBox->priority;
    message += ENDCHAR;
    Serial.println(message);
    Serial.println("instance");
    SendMessage(message, temp->ip, 4210); 
    temp = temp->next;
    Serial.println("instance2");
  }
}

String Growbox::SplitData(String data, char separator, int index)
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

IPAddress Growbox::str2IP(String str) {

    IPAddress ret( getIpBlock(0,str),getIpBlock(1,str),getIpBlock(2,str),getIpBlock(3,str) );
    return ret;

}

int Growbox::getIpBlock(int index, String str) {
    char separator = '.';
    int found = 0;
    int strIndex[] = {0, -1};
    int maxIndex = str.length()-1;
  
    for(int i=0; i<=maxIndex && found<=index; i++){
      if(str.charAt(i)==separator || i==maxIndex){
          found++;
          strIndex[0] = strIndex[1]+1;
          strIndex[1] = (i == maxIndex) ? i+1 : i;
      }
    }
    
    return found>index ? str.substring(strIndex[0], strIndex[1]).toInt() : 0;
}
