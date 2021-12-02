using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace SensorDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] ports;
        private void Form1_Load(object sender, EventArgs e)
        {
            ports = SerialPort.GetPortNames();
            foreach (string s in ports)
            {
                cbComport.Items.Add(s);
            }
        }

        private void cbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen != true)
            {
                int selectedIndex = cbComport.SelectedIndex;
                serialPort1.PortName = ports[selectedIndex];
                serialPort1.Open();
                timer1.Start();
            }
            else
            {
                serialPort1.Close();
                int selectedIndex = cbComport.SelectedIndex;
                serialPort1.PortName = ports[selectedIndex];
                serialPort1.Open();
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.BytesToRead > 0)
            {
                string incoming = serialPort1.ReadLine().Trim();
                if (incoming.Contains("#T"))
                {
                    string[] temp = incoming.Split('T');
                    lbMessage.Items.Add("Current temperature is: " + temp[1] + "℃");
                }
                else if (incoming.Contains("#H"))
                {
                    string[] temp = incoming.Split('H');
                    lbMessage.Items.Add("Current humidity is: " + temp[1] + "%");
                }
            }

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            serialPort1.Write("readT&");
            serialPort1.Write("readH&");
        }
    }
}
