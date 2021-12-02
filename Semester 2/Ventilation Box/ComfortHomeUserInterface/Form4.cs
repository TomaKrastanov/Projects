using ComfortHomeClassLibrary;
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
    public partial class Form4 : Form
    {
        ZigbeeDongle zigbee; // Create an instance of the ZigBee dongle class
        public Form4(Form1 mainForm, ZigbeeDongle zgb) // Inherit the zigbee dongle from form1
        {
            InitializeComponent();
            zigbee = zgb; // Set our nwe instance of the zigbee class inherit from the one in form1
            zigbee.messageBox = new ListBox();
            zigbee.dataBox = new ListBox();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            zigbee.messageBox = lb_message; // Add the messages we send to one of the listboxes
            zigbee.dataBox = lb_data; // Add the messages we recieve to the other listbox
        }

        private void btn_dassl_Click(object sender, EventArgs e)
        {
            zigbee.Disassociate(); // When this button is clicked make the zigbee leave whatever network it is currently in
        }

        private void btn_en_Click(object sender, EventArgs e)
        {
            zigbee.Setup_PAN(); // When this button is clicked establish a new network for the zigbee
        }

        private void btn_bcast_Click(object sender, EventArgs e)
        {
            zigbee.Broadcast(tb_bcast.Text); // When this button is clicked send a bradcast message to all of the modules connected to the same network
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            lb_data.Items.Clear(); // Cleare the listbox in which we display the messages we recieve from connected modules
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }
    }
}
