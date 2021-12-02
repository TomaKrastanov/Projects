struct growBoxWaterPrio
{
    int priority;
    int waterLevel;
    int id;
    growBoxWaterPrio *next = 0;
};

#define MAX_NUMBER_OF_GROWBOXES 100

class WaterPriority()
{
private:
    int numberOfBoxes = 0;
    int requiredWaterLevel = 100;
    growBoxWaterPrio *growBoxList;
    growBoxWaterPrio *thisGrowBox;
public:
    WaterPriority();
    int removeGrowBox(int id);
    int checkIfHighestPriority();
    void updateThisWaterPriority(int priority);
    int updateWaterLevel(int id, int waterLevel, int priority);
    void updateThisWaterLevel(int moistureMeasurement);
};