using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry_Prototype
{

    public class GrowBox
    {
        public string Name { get; set; }
        public Sensors temperature, humidity, light, soilHumidity;
        public List<Sensors> sensorList = new List<Sensors>();

        public GrowBox(string name)
        {

            this.Name = name;
        }

        string CheckSelection()
        {
            string selection = "";
            foreach (Sensors sensor in sensorList)
                selection += $" {sensor.Name} ";
            return selection;
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Sensors: {CheckSelection()}";
        }
    }
}
