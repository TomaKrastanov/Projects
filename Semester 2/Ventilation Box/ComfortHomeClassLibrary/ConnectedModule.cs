using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
   public class ConnectedModule
    {
        public string id;                                                               // The PAN id of the module
        public bool timedout = false;                                                   // Stores the timeout status.
        public DateTime lastcontact;                                                    // Stores the time of last contact.
        public List<attachedSensor> attachedSensors = new List<attachedSensor>();       // Stores the list of sensors attached to this module.
        Fan attachedfan = new Fan();

        public ConnectedModule(string ID)
        {
            id = ID;
            timedout = false;
            lastcontact = DateTime.Now;
        }

        // The attachedSensor struct contains all the relevant data of the sensor attached to the module.
        public struct attachedSensor
        {
            public Sensor sensor;
            public string sensortype;
            public string id;
        }

        // Timeout() sets the timeout status.
        public void Timeout(bool state)
        {
            timedout = state;
            if (state)
            {
                foreach(attachedSensor attachedsensor in attachedSensors)
                {
                    attachedsensor.sensor.SetCurrent(0);
                }
            }
        }
        // assignFan() attaches the fan that the module controls.
        public void assignFan(Fan fan)
        {
            attachedfan = fan;
        }

        // AddSensor() handles adding sensors to the module.
        public void AddSensor(string TYPE, string ID)
        {
            attachedSensor addSensor;
            addSensor.sensor = new CO2Sensor(string.Empty); // empty sensor instance which will be replaced.
            switch (TYPE)
            {
                case "CO2":
                    addSensor.sensor = new CO2Sensor(ID); //Initialized as Class to append CO2 to the sensor's name.r
                    addSensor.sensor.SetFan(attachedfan); //Attach the fan to the sensor
                    break;
                case "TEMPERATURE":
                    addSensor.sensor = new TemperatureSensor(ID); //Initialized as Class to append CO2 to the sensor's name.r
                    addSensor.sensor.SetFan(attachedfan); //Attach the fan to the sensor
                    break;
                case "HUMIDITY":
                    addSensor.sensor = new HumiditySensor(ID); //Initialized as Class to append CO2 to the sensor's name.r
                    addSensor.sensor.SetFan(attachedfan); //Attach the fan to the sensor
                    break;
                case "VOC":
                    addSensor.sensor = new VocSensor(ID); //Initialized as Class to append CO2 to the sensor's name.r
                    addSensor.sensor.SetFan(attachedfan); //Attach the fan to the sensor
                    break;

            }
            addSensor.sensortype = TYPE;
            addSensor.id = ID;
            attachedSensors.Add(addSensor);
        }

    }
}
