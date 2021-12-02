using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortHomeClassLibrary
{
    public class ZigbeeDongle
    {
        public SerialPort port = new SerialPort("COM3");                    // COM port of the dongle.
        public ListBox messageBox = new ListBox();                          // Listbox for outgoing messages.
        public ListBox dataBox = new ListBox();                             // Listbox for incoming messages.
        
        // Open() opens the port.
        public void Open()
        {
            port.Open();
        }

        // SendATCommand sends messages to the provided address and writes the relevant data to the listboxes.
        private void SendATCommand(string command)
        {
            messageBox.Items.Clear();
            string answer;
            command += "\r";
            Regex regex = new Regex(@"(\w+)");

            port.Write(command);

            while (port.BytesToRead == 0) ;
            while (port.BytesToRead > 0)
            {
                answer = port.ReadLine();
                MatchCollection matches = regex.Matches(answer);

                if (matches.Count > 0)
                {
                    Console.WriteLine("{0} matches:", matches.Count);
                    foreach (Match match in matches)
                    {
                        messageBox.Items.Add(" " + match.Value);
                    }
                }
            }
        }

        // Disassociate() closes local PAN's and closes connections to other PAN.
        public void Disassociate()
        {
            SendATCommand("AT+DASSL");
        }

        // Setup_PAN() starts a new PAN that other Zigbee devices can connect to.
        public void Setup_PAN()
        {
            SendATCommand("AT+EN");
        }

        // Join_PAN joins the PAN with the specified id.
        public void Join_PAN(string PAN_ID)
        {
            SendATCommand(("AT+JPAN:" + PAN_ID));
        }
            
        // Broadcast() sends a message to all members of its PAN.
        public void Broadcast(string message)
        {
            SendATCommand(("AT+BCAST:00=" + message));
        }

        // Unicast() sends a message to a specific member of its PAN.
        public void Unicast(string message, ConnectedModule module)
        {
            SendATCommand(("AT+UCAST:" + module.id + ',' + message));
            // port.Write("AT+UCAST:" + module.id + "," + message + "\r");
        }

        // ReadData reads incoming data from the PAN and converts it to a string.
        public string readData()
        {
            string incoming_byte = port.ReadLine().Trim();
            dataBox.Items.Add(incoming_byte);
            return incoming_byte;
        }
    }
}
