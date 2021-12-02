#ifndef WATERSYSTEMGROWBOX_H
#define WATERSYSTEMGROWBOX_H
#include <WiFi.h>
#include "GrowBox.h"

#define MAX_NUMBER_OF_GROWBOXES 100

class WaterSystemGrowBox
{
private:
    int numberOfBoxes = 0;
    int requiredWaterLevel = -1;
    int valveState = 0;
    gbAddress *thisGrowBox;
    std::shared_ptr<Growbox> growBox;
public:
    WaterSystemGrowBox(std::shared_ptr<Growbox> growBoxPtr);
    int removeGrowBox(IPAddress IP);
    int checkIfHighestPriority();
    void updateThisWaterPriority(int priority);
    int updateWaterLevel(IPAddress IP, int waterLevel, int priority);
    void updateThisWaterLevel(float moistureMeasurement);
    void communicateWaterPriority(Growbox *growBox);
    void saveWaterPrio(int newWaterLevel, int newPrio, IPAddress IP);
    void updateValveState(int isHighestPrio);
    void sendValveState(int state);
    void sendWaterLevel();
    void setRequiredWaterLevel(int reqWaterLevel);
};

#endif
