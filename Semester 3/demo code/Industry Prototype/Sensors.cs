using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry_Prototype
{
    public abstract class Sensors
    {
        public String Name { get; set; }
        public float sensValue;
        public Sensors(string name)
        {
            this.Name = name;
        }

        public void SetValue(float value)
        {
            sensValue = value;
        }



    }
}
