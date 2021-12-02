using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
    public class VocSensor: Sensor
    {
        public VocSensor(string name) : base(name + " VOC") { }
        public int VocTHold = 600;  // Default VOC threshold

        public override void CompareValue()
        {
            if (SensorValue > 400 && SensorValue < VocTHold) //Function tath determines the fan speed based on the sensor values and the set threshold
                fan.vocReading = "MEDIUM";
            if (SensorValue > VocTHold)
                fan.vocReading = "HIGH";
            if (SensorValue > 200 && SensorValue < 400)
                fan.vocReading = "LOW";
            if (SensorValue < 200)
                fan.vocReading = "OFF";
        }

        public override void SetThreshold(int vocTh) // Function to sety the user desire threshold
        {
            VocTHold = vocTh;
        }
    }
}
