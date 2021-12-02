#include "WaterSystemGrowBox.h"

#define REQ_WATER_LEVEL(p) (float)requiredWaterLevel / 100 * p

//Define how to get the soilMoisture from the arduino
//Define how to get the priority from the arduino

WaterSystemGrowBox::WaterSystemGrowBox(std::shared_ptr<Growbox> growBoxPtr)
{
  growBox = growBoxPtr;
  thisGrowBox = new gbAddress;
  thisGrowBox->waterLevel = -1;
}

///Return value 1: this box has priority and can open water valve, 0: this box does not have priority and should keep water valve closed or shut it, -1: invalid waterLevel
int WaterSystemGrowBox::checkIfHighestPriority()
{
  if (thisGrowBox->waterLevel == -1)
  {
    Serial.println("testing0");
    return 0;
  }
  if (growBox->boxList == NULL)
  {
    Serial.println("testing1");
    return 1;
  }
  gbAddress *tempGrowBox = growBox->boxList;
  do 
  {
    if (tempGrowBox->waterLevel != -1)
    {
      if (tempGrowBox->waterLevel < thisGrowBox->waterLevel)
      {
        Serial.println("testing2");
        Serial.println(tempGrowBox->waterLevel);
        return 0;
      }
      if (tempGrowBox->waterLevel == thisGrowBox->waterLevel && tempGrowBox->priority < thisGrowBox->priority)
      {
        Serial.println("testing3");
        return 0;
      }
      if (tempGrowBox->waterLevel == thisGrowBox->waterLevel && tempGrowBox->priority == thisGrowBox->priority && tempGrowBox->ip[3] < growBox->localIPAddress[3])
      {
        Serial.println("testing4");
        return 0;
      }
    }
    if(tempGrowBox->next != NULL) {
      tempGrowBox = tempGrowBox->next;
    }
  } while (tempGrowBox->next != NULL);
  Serial.println("testing5");
  return 1;
}

void WaterSystemGrowBox::updateThisWaterPriority(int priority)
{
  thisGrowBox->priority = priority;
  Serial.println(thisGrowBox->priority);
}

///Return value 1: value of a growbox is updated, 2: new growbox is created
int WaterSystemGrowBox::updateWaterLevel(IPAddress IP, int waterLevel, int priority)
{
  gbAddress *tempGrowBox = growBox->boxList;
  if (tempGrowBox != NULL)
  {
    if (IP == tempGrowBox->ip)
    {
      tempGrowBox->priority = priority;
      tempGrowBox->waterLevel = waterLevel;
      return 1;
    }
    while (tempGrowBox->next != NULL)
    {
      if (IP == tempGrowBox->next->ip)
      {
        tempGrowBox->next->priority = priority;
        tempGrowBox->next->waterLevel = waterLevel;
        return 1;
      }
      tempGrowBox = tempGrowBox->next;
    }
  } else {
    growBox->boxList = new gbAddress;
    growBox->boxList->ip = IP;
    growBox->boxList->priority = priority;
    growBox->boxList->waterLevel = waterLevel;
    return 2;
  }
  tempGrowBox->next = new gbAddress;
  tempGrowBox->next->ip = IP;
  tempGrowBox->next->priority = priority;
  tempGrowBox->next->waterLevel = waterLevel;
  return 2;
}

///Return value 1: grow box is successfulky removed -1: id does not match any grow boxes registered
int WaterSystemGrowBox::removeGrowBox(IPAddress IP)
{
  if (growBox->boxList == NULL)
  {
    return -1;
  }
  gbAddress *tempGrowBox = growBox->boxList;
  if (tempGrowBox->next != NULL)
  {
    if (tempGrowBox->next->ip == IP)
    {
      gbAddress *toBeDeletedGrowbox = tempGrowBox->next;
      tempGrowBox->next = tempGrowBox->next->next;
      delete toBeDeletedGrowbox;
      updateValveState(checkIfHighestPriority());
      return 1;
    }
    tempGrowBox = tempGrowBox->next;
  }
  return -1;
}

/*There are a total of 6 water levels
  (For values inbetween mentioned ranges the water level will stay at current level, as to not quickly change when obtaining water)
  0   0-5%
  1   10-20%
  2   25-40%
  3   45-60%
  4   65-80%
  5   85-95%
  6   100%+
*/
void WaterSystemGrowBox::updateThisWaterLevel(float moistureMeasurement)
{
  Serial.println("updateThisWaterLevel");
  Serial.println(moistureMeasurement);
  if(!growBox->programActive) {
    Serial.println("No program assigned updateThisWaterLevel");
    return;
  }
  if(moistureMeasurement < thisGrowBox->waterLevel * 18 *.80|| moistureMeasurement > thisGrowBox->waterLevel * 18 *1.2) {
    thisGrowBox->waterLevel = moistureMeasurement / requiredWaterLevel * 5;
  }
  if(thisGrowBox->waterLevel == -1) {
    thisGrowBox->waterLevel = moistureMeasurement / requiredWaterLevel * 5;
  }
  int prevWaterLevel = thisGrowBox->waterLevel;
  if (moistureMeasurement <= REQ_WATER_LEVEL(5))
  {
    thisGrowBox->waterLevel = 0;
  } else if (moistureMeasurement > REQ_WATER_LEVEL(10) && moistureMeasurement <= REQ_WATER_LEVEL(20))
  {
    thisGrowBox->waterLevel = 1;
  } else if (moistureMeasurement > REQ_WATER_LEVEL(25) && moistureMeasurement <= REQ_WATER_LEVEL(40))
  {
    thisGrowBox->waterLevel = 2;
  } else if (moistureMeasurement > REQ_WATER_LEVEL(45) && moistureMeasurement <= REQ_WATER_LEVEL(60))
  {
    thisGrowBox->waterLevel = 3;
  } else if (moistureMeasurement > REQ_WATER_LEVEL(65) && moistureMeasurement <= REQ_WATER_LEVEL(80))
  {
    thisGrowBox->waterLevel = 4;
  } else if (moistureMeasurement > REQ_WATER_LEVEL(85) && moistureMeasurement < REQ_WATER_LEVEL(95))
  {
    thisGrowBox->waterLevel = 5;
  } else if (moistureMeasurement >= REQ_WATER_LEVEL(100))
  {
    thisGrowBox->waterLevel = 6;
  }
  Serial.println(thisGrowBox->waterLevel);
  sendWaterLevel();
  updateValveState(checkIfHighestPriority() && thisGrowBox->waterLevel < 6);
}

void WaterSystemGrowBox::saveWaterPrio(int newWaterLevel, int newPrio, IPAddress IP)
{
  gbAddress *prevTemp = growBox->boxList;
  gbAddress *temp = growBox->boxList;
  if(temp == NULL) {
    temp = new gbAddress;
    temp->ip = IP;
    temp->waterLevel = newWaterLevel;
    temp->priority = newPrio;
  } else {
    while (temp != NULL)
    {
      if (temp->ip == IP)
      {
        temp->waterLevel = newWaterLevel;
        temp->priority = newPrio;
        updateValveState(checkIfHighestPriority());
        return;
      }
      prevTemp = temp;
      temp = temp->next;
    }
    prevTemp->next = new gbAddress;
    prevTemp->next->ip = IP;
    prevTemp->next->waterLevel = newWaterLevel;
    prevTemp->next->priority = newPrio;
  }
  updateValveState(checkIfHighestPriority());
}

void WaterSystemGrowBox::sendValveState(int state)
{
  if(!growBox->programActive) {
    Serial.println("No program assigned");
    return;
  }
  String sendValveString = "$IsWatering:";
  sendValveString += state;
  sendValveString += ENDCHAR;
  Serial.print("valve message: ");
  Serial.println(sendValveString);
  Serial2.println(sendValveString);
}

void WaterSystemGrowBox::updateValveState(int isHighestPrio)
{
  //if (isHighestPrio != valveState) {
    valveState = isHighestPrio;
    sendValveState(isHighestPrio);
  //}
}

void WaterSystemGrowBox::sendWaterLevel()
{
  if(thisGrowBox->waterLevel != -1 && growBox->programActive) {
    growBox->DistributeWaterLevel(thisGrowBox);
  }
}

void WaterSystemGrowBox::setRequiredWaterLevel(int reqWaterLevel)
{
  requiredWaterLevel = reqWaterLevel;
}
