using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComfortHomeUserInterface
{
    public partial class Form2 : Form
    {

        Form1 mainForm;

        string time; // String for following the time at which the graphs are updated
        // Sensor values that inherit from form1
        public int co2 = 0;
        public float temp = 0;
        public float hum = 0;
        public int voc = 0;
        public Form2(Form1 mainForm)
        {
            InitializeComponent();
        }

        public void Chart(Chart chart) // Function for greating the graphs and visualizing them
        {
            time = DateTime.Now.ToString("HH:mm:ss"); // Get the exact time when the value is charted
            // Depending on which graph is slected for the use of this function, graph the value over time 
            if (chart == chart1)
                chart1.Series["Temp"].Points.AddXY(time, temp);
            else if (chart == chart4)
                chart4.Series["Hum"].Points.AddXY(time, hum);
            else if (chart == chart3)
                chart3.Series["CO2"].Points.AddXY(time, co2);
            else if (chart == chart2)
                chart2.Series["VOC"].Points.AddXY(time, voc);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // On each timer tick call the chart function for each chart
            Chart(chart1);
            Chart(chart2);
            Chart(chart3);
            Chart(chart4);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start(); // Start the timer
        }

        private void cmbChartSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Depending on which graph is selected form the graph combobox we display that one and hide the others
            if (cmbChartSelector.SelectedIndex == 0)
            {
                chart1.Visible = true;
                chart2.Visible = false;
                chart3.Visible = false;
                chart4.Visible = false;
            }
            else if (cmbChartSelector.SelectedIndex == 1)
            {
                chart1.Visible = false;
                chart2.Visible = false;
                chart3.Visible = false;
                chart4.Visible = true;
            }
            else if (cmbChartSelector.SelectedIndex == 2)
            {
                chart1.Visible = false;
                chart2.Visible = false;
                chart3.Visible = true;
                chart4.Visible = false;
            }
            else if (cmbChartSelector.SelectedIndex == 3)
            {
                chart1.Visible = false;
                chart2.Visible = true;
                chart3.Visible = false;
                chart4.Visible = false;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }
    }
}
