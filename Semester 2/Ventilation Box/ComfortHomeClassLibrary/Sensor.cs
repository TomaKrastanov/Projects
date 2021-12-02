using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
   public abstract class Sensor
    {
        public String Name { get; set; } //Get/set name
        public Fan fan;                  //Fan to reference
        public Sensor(string name)
        {
            this.Name = name;
        }
        public void SetFan(Fan _fan)
        {
            this.fan = _fan;
        }

        public float SensorValue = 0;      //Current value reading
        public float MostRecentValue = 0;  //Most recent value before current (part of algorithm)

        public void SetCurrent(float value) //Sets current value and initializes comparison
        {
            SensorValue = value;
            CompareValue();
            fan.Prioritize();
        }

        public abstract void CompareValue();
        public abstract void SetThreshold(int humTH);
    }
}
