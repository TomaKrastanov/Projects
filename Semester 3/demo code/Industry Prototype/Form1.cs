using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Industry_Prototype
{
    public partial class Display : Form
    {
        GrowBox gb, gbData;
        List<GrowBox> gbList = new List<GrowBox>();
        readonly Bitmap[] movingFan = new Bitmap[25];
        int index, count, i = 0;
        public static int timerFanSpeed = 40;
        int temperatureTH = 28;

        public Display()
        {
            InitializeComponent();
        }

        void UpdateListBox()
        {
            lbxGrowBox.Items.Clear();
            foreach(GrowBox gb in gbList)
            {
                lbxGrowBox.Items.Add(gb.ToString());
            }
        }

        void CheckThresholds()
        {
            if (gb.temperature.sensValue > temperatureTH)
            {
                timerFanSpeed -= 10;
                if (timerFanSpeed <= 1)
                    timerFanSpeed = 1;

            }
            else if (gb.temperature.sensValue < temperatureTH)
            {
                timerFanSpeed += 10;
                if (timerFanSpeed > 100)
                    timerFanSpeed = 100;
            }
        }

        void ShowData()
        {
            gbData = gbList.ElementAt(index);
            tbxTemp.Text = gbData.temperature.sensValue.ToString();
            tbxHum.Text = gbData.humidity.sensValue.ToString();
            tbxSoilHum.Text = gbData.soilHumidity.sensValue.ToString();
            tbxLight.Text = gbData.light.sensValue.ToString();
        }


        private void Display_Load(object sender, EventArgs e)
        {
            dataPort.Open();
            dataTimer.Start();
            fanTimer.Start();
            for (int i = 1; i < 25; i++)
            {
                movingFan[i] = new Bitmap(@"C:\Users\Admin\git\semester-2-industry-project-gardening-box\Industry Prototype\bin\Debug\fan" + i.ToString() + ".gif", true);
            }
        }

        private void addBox_Click(object sender, EventArgs e)
        {
            gb = new GrowBox(tbxName.Text);
            if (cbxTemp.Checked)
            {
                gb.temperature = new TempSensor("Temperature");
                gb.sensorList.Add(gb.temperature);
            }
            if (cbxHum.Checked)
            {
                gb.humidity = new HumiditySensor("Humidity");
                gb.sensorList.Add(gb.humidity);
            }
            if (cbxSoilHum.Checked)
            {
                gb.soilHumidity = new SoilHumiditySensor("Soil Humidity");
                gb.sensorList.Add(gb.soilHumidity);
            }
            if (cbxLight.Checked)
            {
                gb.light = new LightSensor("Light");
                gb.sensorList.Add(gb.light);
            }

            gbList.Add(gb);
            UpdateListBox();
        }

        private void fanTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                fanTimer.Interval = timerFanSpeed;
            }
            catch (ArgumentOutOfRangeException)
            {
                fanTimer.Interval = 1;
            }

            pbxFan.Image = movingFan[i];
            i++;
            if (i == 24)
            {
                i = 1;
            }
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            index = lbxGrowBox.SelectedIndex;
        }


        private void dataTimer_Tick(object sender, EventArgs e)
        {
            if (dataPort.BytesToRead > 0)
            {
                if (gb != null)
                {
                    count = 0;
                    string message = dataPort.ReadLine().Trim();
                    foreach (char c in message)
                        if (c == ';')
                            count++;

                    if (message.StartsWith("Data") && message.EndsWith("End") && count == 5)
                    {
                        string[] data;
                        data = message.Split(';');
                        gb.humidity.sensValue = Convert.ToInt32(data[1]);
                        gb.light.sensValue = float.Parse(data[2]);
                        gb.temperature.sensValue = float.Parse(data[3]);
                        gb.soilHumidity.sensValue = float.Parse(data[4]);
                        ShowData();
                        CheckThresholds();
                    }
                }
            }
        }
    }
}
