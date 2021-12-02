#ifndef GROWBOX_H
#define GROWBOX_H

#include <WiFi.h>
#include <WiFiUdp.h>
#include <time.h>
#include <stdio.h>
#include <String.h>

//UDP Connection
#define WIFI_SSID "TomaPC"
#define WIFI_PASS "12345678"
#define UDP_PORT 4210

//Growbox limit
#define MAXGROWBOXES 10

// COM PROTOCOL MACROS
#define ENDCHAR '#'
#define SEPARATORCHAR ':'
#define ANNOUNCE "$Announce:"
#define ADDNODE "$AddNode:"
#define SENDDATA "$SendData:"
#define ASSIGNPROGRAM "$Program:"
#define PAUSE "$Pause"
#define REMOVE "$Remove:"
#define RESUME "$Resume"
#define WATERPRIORITY "$WaterPrio:"
#define ISWATERING "$IsWatering:"
#define ERRORCODE "$Error:#"
#define CYCLECOMPLETED "$CycleCompleted:"

struct gbAddress {
  IPAddress ip;
  int waterLevel = -1;
  int priority = -1; 
  gbAddress *next = NULL;
};

class Growbox {
  private:
    // Growbox variables
    int numberOfBoxes = 0;
    // Communication variables

    IPAddress &ServerIp;
    WiFiUDP &UDP;
  public:
    bool programActive = false;
    gbAddress *boxList;
    IPAddress localIPAddress;
    Growbox(IPAddress &ServerIp, WiFiUDP &UDP);
    int Disconnect(String IP);
    void ConnectToServer();
    int addBox(String ip);
    void sendMessageServer(String reply);
    String SplitData(String data, char separator, int index);
    void DistributeWaterLevel(gbAddress *thisGrowBox);
    void SendMessage(String message, IPAddress ip, int port);
    IPAddress str2IP(String str);
    int getIpBlock(int index, String str); 
};

#endif
