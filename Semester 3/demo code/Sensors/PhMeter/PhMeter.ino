#define SensorPin A0            //pH meter Analog output to Arduino Analog Input 0
#define Offset 41.02740741      //deviation compensate

#define samplingInterval 20
#define printInterval 500
#define ArrayLenth  40    //times of collection
#define uart  Serial

int pHArray[ArrayLenth];   //Store the average value of the sensor feedback
int pHArrayIndex = 0;

void setup(void)
{
  uart.begin(9600);
  uart.println("pH meter experiment!");    //Test the uart monitor
}

void loop(void)
{
  static unsigned long samplingTime = millis();
  static unsigned long printTime = millis();
  static float pHValue, voltage;

  if (millis() - samplingTime > samplingInterval)
  {
    pHArray[pHArrayIndex++] = analogRead(SensorPin);
    if (pHArrayIndex == ArrayLenth){pHArrayIndex = 0;}
    voltage = avergearray(pHArray, ArrayLenth) * 5.0 / 1024;
    pHValue = -19.18518519 * voltage + Offset;
    samplingTime = millis();
  }

  if (millis() - printTime > printInterval)  //Every 800 milliseconds, print a numerical, convert the state of the LED indicator
  {
    uart.print("    pH value: ");
    uart.println(pHValue, 2);
    printTime = millis();
  }
}

double avergearray(int* arr, int number) {
  int i;
  int max, min;
  double avg;
  long amount = 0;
  if (number <= 0) 
  {
    uart.println("Error number for the array to avraging!/n");
    return 0;
  }
  if (number < 5)
  { //less than 5, calculated directly statistics
    for (i = 0; i < number; i++)
    {
      amount += arr[i];
    }
    avg = amount / number;
    return avg;
  }
  else
  {
    if (arr[0] < arr[1])
    {
      min = arr[0]; max = arr[1];
    }
    else
    {
      min = arr[1]; max = arr[0];
    }
    for (i = 2; i < number; i++)
    {
      if (arr[i] < min)
      {
        amount += min;      //arr<min
        min = arr[i];
      }
      else
      {
        if (arr[i] > max)
        {
          amount += max;  //arr>max
          max = arr[i];
        }
        else
        {
          amount += arr[i]; //min<=arr<=max
        }
      }
    }
    avg = (double)amount / (number - 2);
  }
  return avg;
}
