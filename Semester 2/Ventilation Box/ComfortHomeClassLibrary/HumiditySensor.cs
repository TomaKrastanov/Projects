using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
   public class HumiditySensor: Sensor
    {
        public HumiditySensor(string name) : base(name + " Humidity") { }
        private int HumTHold = 90; // Default humidity threshold

        public override void CompareValue() //Function tath determines the fan speed based on the sensor values and the set threshold
        {
            if (SensorValue > 60 && SensorValue < HumTHold)
                fan.humidReading = "MEDIUM";
            if (SensorValue > HumTHold)
                fan.humidReading = "HIGH";
            if (SensorValue > 50 && SensorValue < 60)
                fan.humidReading = "LOW";
            if (SensorValue < 50)
                fan.humidReading = "OFF";
        }

        public override void SetThreshold(int humTH) // Function to sety the user desire threshold
        {
            HumTHold = humTH;
        }
    }
}
