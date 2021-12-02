using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
    public class CO2Sensor: Sensor
    {
        public CO2Sensor(string name) : base(name + " CO2") { } //appends CO2 to the end of the sensor name for identification.
        private int Co2THold = 1500; // Default CO2 threshold

        public override void CompareValue() //Function tath determines the fan speed based on the sensor values and the set threshold
        {
            if (SensorValue > 1000 && SensorValue < Co2THold)
                fan.co2Reading = "MEDIUM";
            if (SensorValue > Co2THold)
                fan.co2Reading = "HIGH";
            if (SensorValue > 800 && SensorValue < 1000)
                fan.co2Reading = "LOW";
            if (SensorValue < 800)
                fan.co2Reading = "OFF";
        }

        public override void SetThreshold(int co2TH) // Function to sety the user desire threshold
        {
            Co2THold = co2TH;
        }
    }
}
