using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using java.lang;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using ComfortHomeClassLibrary;

namespace ComfortHomeUserInterface
{
    public partial class Form1 : Form
    {
        int roomCounter = 1; // Counter for the registered devices
        int index = 0; // Index of the selected room
        string[] ports = SerialPort.GetPortNames(); // Array of all the available serial ports
        // Initializations of the other forms
        Form2 form2; 
        Form3 form3;
        Form4 form4;
        List<ConnectedModule> connections = new List<ConnectedModule>(); // List of connected modules to the system
        Fan fan = new Fan(); // Initializing a fan
        private bool fanState = true; // Boolean to keep track of the fan state
        bool connectedToPort = false; // Boolean to check if a connection has been established
        ZigbeeDongle dongle = new ZigbeeDongle(); // Initalizing a dongle for the communication
        public Fan.States previousStatusFan = Fan.States.OFF; // Variable to keep track of the previous state of tha fan

        void TranslateMsg(string inputstring)
        {
            // First off the part of the string that the dongle adds needs to be removed.
            if (inputstring.Contains("="))
            {
                inputstring = inputstring.Substring(inputstring.LastIndexOf('=') + 1);
            }
            // Then the message gets divided into its major components.
            string[] Message_Components;
            Message_Components = inputstring.Split('&');
            // Then the program goes through every component based on the first component.
            int i = 0;

            foreach (string message in Message_Components)
            {
                // Then the message gets split into the separate messages by it's end and start character.
                if (message.StartsWith("#"))
                {
                    List<string> Single_Message_Components = new List<string>();
                    for (int j = i; j < Message_Components.Length; j++)
                    {
                        if (Message_Components[j].EndsWith("$")) // If we are at the final message component we remove the ending character and add it to the list of message components
                        {
                            Message_Components[j] = Message_Components[j].Remove(Message_Components[j].LastIndexOf('$'), 1);
                            Single_Message_Components.Add(Message_Components[j]);
                            j = Message_Components.Length;
                        }
                        else // Else we just add it to the list of message componenets
                        {
                            Single_Message_Components.Add(Message_Components[j]);
                        }
                    }
                    // The message is handled differently for different message types.
                    switch (Message_Components[i])
                    {
                        // #REG indicates a module registration.
                        case "#REG":
                            // If the id already exists but is timed out, that means a previously lost module is back online.
                            bool taken = false;
                            foreach(ConnectedModule module in connections)
                            {
                                if (module.id == Single_Message_Components[i + 1])
                                {
                                    if (module.timedout)
                                    {
                                        taken = false;
                                        module.Timeout(false);
                                    }
                                    else
                                    {
                                        taken = true;
                                        dongle.Unicast("#REGACK$", module);
                                        MessageBox.Show($"Module {module.id} connected");
                                    }
                                }
                            }
                            // If the id is new the module is registered as a new connected module.
                            if (!taken)
                            {
                                bool Correct = true;            // records the validity of the message.
                                ConnectedModule module = new ConnectedModule(Single_Message_Components[i + 1]);
                                cbxRooms.Items.Add($"Room {roomCounter}");
                                roomCounter++;
                                module.assignFan(fan);
                                string type = string.Empty;     // stores sensor type.
                                string id = string.Empty;       // stores sensor id.
                                // Then the program goes through every subsequent combination of 2 message components to register the sensors.
                                for (int k = i + 2; k < Single_Message_Components.Count; k += 2)
                                {
                                        if (Single_Message_Components[k+1] != "CO2" &&
                                            Single_Message_Components[k+1] != "VOC" &&
                                            Single_Message_Components[k+1] != "TEMPERATURE" &&
                                            Single_Message_Components[k+1] != "HUMIDITY")
                                        {
                                        // The type doesn't exist, an error has occured.
                                            dongle.Unicast("#REGACK&01$", module);
                                            Correct = false;
                                        }
                                        type = Single_Message_Components[k+1];
                                        id = Single_Message_Components[k];
                                        module.AddSensor(type, id);
                                }
                                if (Correct)
                                {
                                    // no mistakes were found, module added.
                                    connections.Add(module);
                                    dongle.Unicast("#REGACK$", module);
                                }
                            }
                            break;
                            // #SENDDATA indicates incoming sensor data.
                        case "#SENDDATA":

                            bool module_found = false;          // Stores the module id validity for error handling.
                            // First we need the module with the right id.
                            ConnectedModule sensormodule = new ConnectedModule(string.Empty);
                            foreach (ConnectedModule connectedmodule in connections)
                            {
                                if (connectedmodule.id == Single_Message_Components[i + 1]) // if the module is registered we accept the message and save the last contact we had with the module
                                                                                            // we also match the index of the module so later we can match it with the combobox for selecting rooms
                                {
                                    sensormodule = connectedmodule;
                                    sensormodule.lastcontact = DateTime.Now;
                                    index = connections.IndexOf(sensormodule);
                                    module_found = true;
                                }
                            }
                            if (module_found)
                            {
                                // Then the program goes through every subsequent combination of 2 message components to read incoming sensor data.
                                for (int k = i + 2; k < Single_Message_Components.Count; k += 2)
                                {
                                    bool sensorfound = false;       // Stores the sensor id validity.
                                    foreach (ConnectedModule.attachedSensor attachedsensor in sensormodule.attachedSensors)
                                    {
                                        if (Single_Message_Components[k] == attachedsensor.id)
                                        {
                                            sensorfound = true;
                                            // For every data received that comes in succesfully the program sends an acknowledgement.
                                            dongle.Unicast(("#SENDDATAACK&" + (sensormodule.id) + "&01$"), sensormodule);
                                            // Write the data to the sensor class.
                                            attachedsensor.sensor.SetCurrent(float.Parse(Single_Message_Components[k + 1]));
                                            if (index == cbxRooms.SelectedIndex) // If the index of the connected module matches the selected room we store the values we recieve into the textboxes
                                            {
                                                switch (attachedsensor.sensortype)
                                                {
                                                    // We check what the sensortype is and we enter the data readings in the correct textboxes, if we recieve a 0 from a module we acknowledge the sensor has been disconnected
                                                    case "CO2":
                                                        if (attachedsensor.sensor.SensorValue == 0)
                                                            tbxCO2.Text = "Disc";
                                                        else
                                                            tbxCO2.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                                        break;
                                                    case "TEMPERATURE":
                                                        if (attachedsensor.sensor.SensorValue == 0)
                                                            tbxTemp.Text = "Disc";
                                                        else
                                                            tbxTemp.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                                        break;
                                                    case "HUMIDITY":
                                                        if (attachedsensor.sensor.SensorValue == 0)
                                                            tbxHum.Text = "Disc";
                                                        else
                                                            tbxHum.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                                        break;
                                                    case "VOC":
                                                        if (attachedsensor.sensor.SensorValue == 0)
                                                            tbxVOC.Text = "Disc";
                                                        else
                                                            tbxVOC.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    if (!sensorfound)
                                    {
                                        // If the sensor id is unknown, an error has occured.
                                        dongle.Unicast(("#SENDDATAACK&" + (sensormodule.id) + "&04$"), sensormodule);
                                    }
                                }
                            }
                            else
                            {
                                // If the module id is unknown, an error has occured.
                                sensormodule.id = Single_Message_Components[i + 1];
                                dongle.Unicast(("#SENDDATAACK&" + (Single_Message_Components[i + 1]) + "&03$"), sensormodule);
                            }
                            break;
                        // #SPIKE indicates incoming elevated sensor data, this broadly functions the exact same as #SENDDATA, only for a single measurement.
                        case "#SPIKE":

                            ConnectedModule spikemodule = new ConnectedModule(string.Empty);
                            bool found = false;
                            foreach (ConnectedModule connectedmodule in connections)
                            {
                                if (connectedmodule.id == Single_Message_Components[i + 1])
                                {
                                    spikemodule = connectedmodule;
                                    found = true;
                                }
                            }
                            if (found)
                            {
                                bool sensor_found = false;
                                foreach (ConnectedModule.attachedSensor spikesensor in spikemodule.attachedSensors)
                                {
                                    if (Single_Message_Components[i + 2] == spikesensor.id)
                                    {
                                        sensor_found = true;
                                        spikesensor.sensor.SetCurrent(float.Parse(Single_Message_Components[i + 3]));
                                        dongle.Unicast("#SPIKEACK$", spikemodule);
                                        switch (spikesensor.sensortype)
                                        {
                                            case ("VOC"):
                                                tbxVOC.Text = Convert.ToString(spikesensor.sensor.SensorValue);
                                                break;
                                            case ("CO2"):
                                                tbxCO2.Text = Convert.ToString(spikesensor.sensor.SensorValue);
                                                break;
                                            case ("TEMPERATURE"):
                                                tbxTemp.Text = Convert.ToString(spikesensor.sensor.SensorValue);
                                                break;
                                            case ("HUMIDITY"):
                                                tbxHum.Text = Convert.ToString(spikesensor.sensor.SensorValue);
                                                break;
                                        }
                                    }
                                }
                                if (!sensor_found)  // If the sensor id is unknown, an error has occured.
                                {
                                    dongle.Unicast("#SPIKEACK&02$", spikemodule);
                                }
                            }
                            else
                            {
                                dongle.Unicast("#SPIKEACK&01$", spikemodule); // If the module id is unknown, an error has occured.
                            }
                            break;
                    }
                }
                i++;
            }


            foreach (ConnectedModule Sensormodule in connections)
            {
                index = connections.IndexOf(Sensormodule); // Set the index of the module in the list
                foreach (ConnectedModule.attachedSensor sensor in Sensormodule.attachedSensors)
                {
                    if (index == cbxRooms.SelectedIndex) // If the index mathces the selected index of the room combo box
                    {
                        // We send those readings to the form that graphs the values
                        switch (sensor.sensortype)
                        {
                            case "CO2":
                                form2.co2 = (int)sensor.sensor.SensorValue;
                                break;
                            case "TEMPERATURE":
                                form2.temp = (int)sensor.sensor.SensorValue;
                                break;
                            case "HUMIDITY":
                                form2.hum = (int)sensor.sensor.SensorValue;
                                break;
                            case "VOC":
                                form2.voc = (int)sensor.sensor.SensorValue;
                                break;

                        }
                        // We send the threshold if it was set by the user
                        switch (sensor.sensortype)
                        {
                            case "CO2":
                                if (form3.co2TH != 0)
                                {
                                    sensor.sensor.SetThreshold(form3.co2TH);
                                }
                                break;
                            case "TEMPERATURE":
                                if (form3.tempTH != 0)
                                {
                                    sensor.sensor.SetThreshold(form3.tempTH);
                                }
                                break;
                            case "HUMIDITY":
                                if (form3.humTH != 0)
                                {
                                    sensor.sensor.SetThreshold(form3.humTH);
                                }
                                break;
                            case "VOC":
                                if (form3.vocTH != 0)
                                {
                                    sensor.sensor.SetThreshold(form3.vocTH);
                                }
                                break;

                        }
                    }

                }
            }





        }
        public Form1()
        {
            InitializeComponent();
            form2 = new Form2(this);
            form3 = new Form3(this);
            form4 = new Form4(this, dongle);
            fan.SetState("OFF"); //Fan is currently set to off.

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connectedToPort == true) // If a connection has been established start reading from the serial port
            {
                if (dongle.port.BytesToRead > 0)
                    TranslateMsg(dongle.readData()); 
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            form2.Show();
            form3.Hide();
            form4.Hide();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.BaudRate = 19200; // Set the baud rate to 19200 so it is compatible with the zigbee

            foreach(string port in ports) // Check all of the available ports and add them in a combobox
            {
                cbxPorts.Items.Add(port);
            }
            dongle.port = serialPort1; // set the dongle port to be the same as the serial port
            //Start both timers
            timer1.Start();
            Timeout_timer.Start();
        }


        private void btnThresholdSettings_Click(object sender, EventArgs e)
        {
            form3.Show();
            form2.Hide();
            form4.Hide();
        }

        private Image getImage(Fan.States status) // Function that checks the fan state and uses the appropriate photo or gif to visualize the fan speed
        {
            var fileName = status.ToString().ToLower();
            if (status == Fan.States.OFF)
                return Image.FromFile(fileName + ".png");

            return Image.FromFile(fileName + ".gif");
        }

        private void btnFan_Click(object sender, EventArgs e)
        {
            fanState = !fanState;
        }

        private void btnZigbee_Click(object sender, EventArgs e)
        {
            form4.Show();
            form2.Hide();
            form3.Hide();
        }


        private void cbxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = cbxPorts.SelectedItem.ToString(); //Whenever a port is selected from the combobox of available ports set the name of the serial port 
            serialPort1.Open();
            connectedToPort = true; // Established connection
        }

        private void Timeout_timer_Tick(object sender, EventArgs e)
        {
            foreach(ConnectedModule module in connections)
            {
                int index = connections.IndexOf(module);
                if (index == cbxRooms.SelectedIndex)
                {
                    // If the time of last contact was over 15 seconds ago, the program sets it's timeout status to true.
                    if (DateTime.Compare(DateTime.Now, (module.lastcontact.AddSeconds(15))) > 0)
                    {
                        fanState = false;
                        if (!module.timedout)
                        {
                            module.Timeout(true);
                            module.Timeout(false);
                        }
                    }
                    else
                    {
                        fanState = true;
                        if (module.timedout)
                        {
                            module.Timeout(false);
                        }
                    }
                    // If the value coming from a sensor is 0, that means it is broken.
                    foreach (ConnectedModule.attachedSensor attachedsensor in module.attachedSensors)
                    {
                        switch (attachedsensor.sensortype)
                        {
                            case "CO2":
                                if (attachedsensor.sensor.SensorValue == 0)
                                    tbxCO2.Text = "Disc";
                                else
                                    tbxCO2.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                break;
                            case "TEMPERATURE":
                                if (attachedsensor.sensor.SensorValue == 0)
                                    tbxTemp.Text = "Disc";
                                else
                                    tbxTemp.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                break;
                            case "HUMIDITY":
                                if (attachedsensor.sensor.SensorValue == 0)
                                    tbxHum.Text = "Disc";
                                else
                                    tbxHum.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                break;
                            case "VOC":
                                if (attachedsensor.sensor.SensorValue == 0)
                                    tbxVOC.Text = "Disc";
                                else
                                    tbxVOC.Text = Convert.ToString(attachedsensor.sensor.SensorValue);
                                break;
                        }

                    }
                }
            }
            if (fanState == true) // If the fan is on check at what state it is
            {

                //
                //In order not to read the image every time the port is read and the previous status does not drain the fan
                //
                if (fan.state != previousStatusFan)
                {
                    pictureBox1.Image = getImage(fan.state);
                    previousStatusFan = fan.state;
                }
            }
            else // If the fan is off set that state to the fan
            {
                fan.SetState("OFF");
                pictureBox1.Image = getImage(fan.state);
            }
            label1.Text = $"Fan Speed: {fan.state}"; // Change the label to diplsay the proper fan state
        }

        private void cbxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRoomName.Text = $"Room: {cbxRooms.SelectedIndex + 1}";
        }

    }
}
