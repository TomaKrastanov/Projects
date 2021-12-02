using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortHomeUserInterface
{
    public partial class Form3 : Form
    {
        Form1 mainForm;
        public Form3(Form1 mainForm)
        {
            InitializeComponent();
        }
        public int tempTH, humTH, co2TH, vocTH; // Threshold values taht inherit from form1 whenever a threshold is set or changed

        private void btnTempTH_Click(object sender, EventArgs e) // Whenever the button to set a threshold is clicked we check what is in the text box and only accept numbers otherwise we display a message box with a proper message
                                                                 // If the value is a number we set it as the proper threshold
        {
            string tempThold = tbxTempTH.Text;
            if (tempThold == "")
                MessageBox.Show("Please set a threshold");
            else if (!int.TryParse(tempThold, out tempTH))
                MessageBox.Show("Please enter a valid value");
            else
                tempTH = Convert.ToInt32(tempThold);
        }

        private void btnHumTH_Click(object sender, EventArgs e) // Whenever the button to set a threshold is clicked we check what is in the text box and only accept numbers otherwise we display a message box with a proper message
                                                                // If the value is a number we set it as the proper threshold
        {
            string humThold = tbxHumTH.Text;
            if (humThold == "")
                MessageBox.Show("Please set a threshold");
            else if (!int.TryParse(humThold, out humTH))
                MessageBox.Show("Please enter a valid value");
            else
                humTH = Convert.ToInt32(humThold);
        }

        private void btnCo2TH_Click(object sender, EventArgs e) // Whenever the button to set a threshold is clicked we check what is in the text box and only accept numbers otherwise we display a message box with a proper message
                                                                // If the value is a number we set it as the proper threshold
        {
            string co2Thold = tbxCO2TH.Text;
            if (co2Thold == "")
                MessageBox.Show("Please set a threshold");
            else if (!int.TryParse(co2Thold, out co2TH))
                MessageBox.Show("Please enter a valid value");
            else
                co2TH = Convert.ToInt32(co2Thold);
        }

        private void btnVocTH_Click(object sender, EventArgs e) // Whenever the button to set a threshold is clicked we check what is in the text box and only accept numbers otherwise we display a message box with a proper message
                                                                // If the value is a number we set it as the proper threshold
        {
            string vocThold = tbxVOCTH.Text;
            if (vocThold == "")
                MessageBox.Show("Please set a threshold");
            else if (!int.TryParse(vocThold, out vocTH))
                MessageBox.Show("Please enter a valid value");
            else
                vocTH = Convert.ToInt32(vocThold);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }
    }
}
