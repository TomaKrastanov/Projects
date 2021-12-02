#include <WiFiUdp.h>
#include <EspMQTTClient.h>
#include "ArduinoJson.h"

#define MAXGROWBOXES 10
#define UDP_PORT 4210
#define BUF_LEN 256

#define WIFI_SSID "Xiaomi_A08C"
#define WIFI_PASS "MUHWNFBA"

// COM PROTOCOL MACROS
#define ENDCHAR '#'
#define ANNOUNCE "$Announce:"
#define ADDNODE "$AddNode:"
#define REMOVENODE "$Remove:"
#define ASSIGNPROGRAM "$Program:"
#define REQPROGSPEC "$ReqProgSpec:"
#define CYCLECOMPLETED "$CycleCompleted:"
#define SENDDATA "$SendData:"
#define ASSIGNPROGRAM "$Program:"
#define SENDCLIMATEDATA "$Data:"
#define PAUSE "$Pause:"
#define RESUME "$Resume:"
#define SENSORERROR "$SensorError:"
#define WATERLEAK "$WaterLeak:"
#define WATERTANKANNOUNCE "$WaterTankAnnounce:"
#define WATERTANKDATA "$WaterTankData:"
#define WATERLEAK "$WaterLeak:"
#define WATERTANKANNOUNCE "$WaterTankAnnounce:"
#define WATERTANKDATA "$WaterTankData:"
#define OK "00"


/////////////////////////////////////////////
struct growBox
{
  IPAddress ip;
  String humidity;
  String ldr;
  String soilHumidity;
  String temperature;

  String endtime;
  String lightcycle;
  growBox *next;
};
/////////////////////////////////////////////


class GrowboxServer {
  private:
    int numberOfBoxes;
    String waterTankData;
    WiFiUDP &UDP;
    IPAddress &broadcastAddr;
    growBox *boxListNew;
    IPAddress SERVERIP;
    IPAddress waterTankIP;
  public:
    GrowboxServer(IPAddress &broadcastAddr, WiFiUDP &UDP);
    char msgBuffer[BUF_LEN];
    void Create();
    void addBox(IPAddress newIP);
    void removeNode();
    void cycleCompleted(String endtime);
    void sendClimateData(IPAddress ip, String Temp, String soilMoisture, String ldr);//Stroes the incoming climate data from growbox
    void broadcastMessage(IPAddress ip, String reply);
    void pauseProgram(IPAddress ip);
    void resumeProgram(IPAddress ip);
    void replyRequestProgramData();
    String SplitData(String data, char seperator, int index);
    void DistributeData(String data);
    void GetJsonBuffer(String humidity, String ldr, String temperature, String soilHumidity);
    void sensorError(String sensorType, String errorType);
    String getHumid();
    String getldr();
    String getTemp();
    String getsoilHumidity();
    void setWaterTankData(String Data);
    void setServerIP(IPAddress ip);
    void setWatertankIP(IPAddress ip);
    void waterLeak();
};
