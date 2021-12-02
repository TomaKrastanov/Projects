using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
   public class TemperatureSensor: Sensor
    {

        public TemperatureSensor(string name) : base(name + " Temperature") { }
        public int tempTHold = 25; // Default temperature threshold
        public override void CompareValue() //Function tath determines the fan speed based on the sensor values and the set threshold
        {
            if (SensorValue >= 21 && SensorValue < tempTHold)
                fan.tempReading = "MEDIUM";
            if (SensorValue >= tempTHold)
                fan.tempReading = "HIGH";
            if (SensorValue >= 17 && SensorValue < 21)
                fan.tempReading = "LOW";
            if (SensorValue < 17)
                fan.tempReading = "OFF";
        }

        public override void SetThreshold(int tempTH) // Function to sety the user desire threshold
        {
            tempTHold = tempTH;
        }
    }
}
