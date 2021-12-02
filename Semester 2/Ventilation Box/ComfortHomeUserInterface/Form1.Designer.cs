namespace ComfortHomeUserInterface
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnChart = new System.Windows.Forms.Button();
            this.tbxTemp = new System.Windows.Forms.TextBox();
            this.tbxVOC = new System.Windows.Forms.TextBox();
            this.tbxCO2 = new System.Windows.Forms.TextBox();
            this.tbxHum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.btnFan = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnThresholdSettings = new System.Windows.Forms.Button();
            this.btnZigbee = new System.Windows.Forms.Button();
            this.cbxPorts = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxRooms = new System.Windows.Forms.ComboBox();
            this.Timeout_timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.PortName = "COM3";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnChart
            // 
            this.btnChart.Location = new System.Drawing.Point(11, 8);
            this.btnChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(82, 22);
            this.btnChart.TabIndex = 1;
            this.btnChart.Text = "Open Charts";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbxTemp
            // 
            this.tbxTemp.BackColor = System.Drawing.Color.White;
            this.tbxTemp.Enabled = false;
            this.tbxTemp.Location = new System.Drawing.Point(87, 37);
            this.tbxTemp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxTemp.Name = "tbxTemp";
            this.tbxTemp.Size = new System.Drawing.Size(32, 20);
            this.tbxTemp.TabIndex = 3;
            // 
            // tbxVOC
            // 
            this.tbxVOC.BackColor = System.Drawing.Color.White;
            this.tbxVOC.Enabled = false;
            this.tbxVOC.Location = new System.Drawing.Point(87, 128);
            this.tbxVOC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxVOC.Name = "tbxVOC";
            this.tbxVOC.Size = new System.Drawing.Size(32, 20);
            this.tbxVOC.TabIndex = 4;
            // 
            // tbxCO2
            // 
            this.tbxCO2.BackColor = System.Drawing.Color.White;
            this.tbxCO2.Enabled = false;
            this.tbxCO2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbxCO2.Location = new System.Drawing.Point(87, 97);
            this.tbxCO2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxCO2.Name = "tbxCO2";
            this.tbxCO2.Size = new System.Drawing.Size(32, 20);
            this.tbxCO2.TabIndex = 5;
            // 
            // tbxHum
            // 
            this.tbxHum.BackColor = System.Drawing.Color.White;
            this.tbxHum.Enabled = false;
            this.tbxHum.Location = new System.Drawing.Point(87, 67);
            this.tbxHum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxHum.Name = "tbxHum";
            this.tbxHum.Size = new System.Drawing.Size(32, 20);
            this.tbxHum.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Humidity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Temperature:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "CO2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "VOC:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.lblRoomName);
            this.groupBox1.Controls.Add(this.btnFan);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbxTemp);
            this.groupBox1.Controls.Add(this.tbxVOC);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxCO2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbxHum);
            this.groupBox1.Location = new System.Drawing.Point(10, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 284);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control panel";
            // 
            // lblRoomName
            // 
            this.lblRoomName.AutoSize = true;
            this.lblRoomName.Location = new System.Drawing.Point(149, 15);
            this.lblRoomName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(38, 13);
            this.lblRoomName.TabIndex = 21;
            this.lblRoomName.Text = "Room:";
            // 
            // btnFan
            // 
            this.btnFan.Location = new System.Drawing.Point(87, 180);
            this.btnFan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFan.Name = "btnFan";
            this.btnFan.Size = new System.Drawing.Size(73, 34);
            this.btnFan.TabIndex = 20;
            this.btnFan.Text = "Fan On/Off";
            this.btnFan.UseVisualStyleBackColor = true;
            this.btnFan.Click += new System.EventHandler(this.btnFan_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(122, 131);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "ppb";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 99);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "ppm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(122, 71);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "%RH";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(122, 41);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "°C";
            // 
            // btnThresholdSettings
            // 
            this.btnThresholdSettings.Location = new System.Drawing.Point(196, 8);
            this.btnThresholdSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThresholdSettings.Name = "btnThresholdSettings";
            this.btnThresholdSettings.Size = new System.Drawing.Size(108, 21);
            this.btnThresholdSettings.TabIndex = 19;
            this.btnThresholdSettings.Text = "Threshold Settings";
            this.btnThresholdSettings.UseVisualStyleBackColor = true;
            this.btnThresholdSettings.Click += new System.EventHandler(this.btnThresholdSettings_Click);
            // 
            // btnZigbee
            // 
            this.btnZigbee.Location = new System.Drawing.Point(98, 8);
            this.btnZigbee.Name = "btnZigbee";
            this.btnZigbee.Size = new System.Drawing.Size(93, 21);
            this.btnZigbee.TabIndex = 18;
            this.btnZigbee.Text = "Zigbee debug";
            this.btnZigbee.UseVisualStyleBackColor = true;
            this.btnZigbee.Click += new System.EventHandler(this.btnZigbee_Click);
            // 
            // cbxPorts
            // 
            this.cbxPorts.FormattingEnabled = true;
            this.cbxPorts.Location = new System.Drawing.Point(531, 10);
            this.cbxPorts.Name = "cbxPorts";
            this.cbxPorts.Size = new System.Drawing.Size(121, 21);
            this.cbxPorts.TabIndex = 20;
            this.cbxPorts.SelectedIndexChanged += new System.EventHandler(this.cbxPorts_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(446, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Available Ports:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fan Speed:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ComfortHomeUserInterface.Properties.Resources.off;
            this.pictureBox1.Location = new System.Drawing.Point(56, 71);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(383, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 284);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fan State";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(308, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Rooms:";
            // 
            // cbxRooms
            // 
            this.cbxRooms.FormattingEnabled = true;
            this.cbxRooms.Location = new System.Drawing.Point(350, 10);
            this.cbxRooms.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxRooms.Name = "cbxRooms";
            this.cbxRooms.Size = new System.Drawing.Size(92, 21);
            this.cbxRooms.TabIndex = 23;
            this.cbxRooms.SelectedIndexChanged += new System.EventHandler(this.cbxRooms_SelectedIndexChanged);
            // 
            // Timeout_timer
            // 
            this.Timeout_timer.Enabled = true;
            this.Timeout_timer.Interval = 1000;
            this.Timeout_timer.Tick += new System.EventHandler(this.Timeout_timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(661, 322);
            this.Controls.Add(this.cbxRooms);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxPorts);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnThresholdSettings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnZigbee);
            this.Controls.Add(this.btnChart);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Ventilation Box";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.TextBox tbxTemp;
        private System.Windows.Forms.TextBox tbxVOC;
        private System.Windows.Forms.TextBox tbxCO2;
        private System.Windows.Forms.TextBox tbxHum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnZigbee;
        private System.Windows.Forms.Button btnThresholdSettings;
        private System.Windows.Forms.Button btnFan;
        private System.Windows.Forms.ComboBox cbxPorts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxRooms;
        private System.Windows.Forms.Timer Timeout_timer;
        private System.Windows.Forms.Label lblRoomName;
    }
}

