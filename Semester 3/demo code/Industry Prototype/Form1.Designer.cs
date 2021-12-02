namespace Industry_Prototype
{
    partial class Display
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
            this.dataTimer = new System.Windows.Forms.Timer(this.components);
            this.dataPort = new System.IO.Ports.SerialPort(this.components);
            this.lbxGrowBox = new System.Windows.Forms.ListBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.registerPage = new System.Windows.Forms.TabPage();
            this.btnShowData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxLight = new System.Windows.Forms.CheckBox();
            this.cbxSoilHum = new System.Windows.Forms.CheckBox();
            this.cbxHum = new System.Windows.Forms.CheckBox();
            this.cbxTemp = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.addBox = new System.Windows.Forms.Button();
            this.dataPage = new System.Windows.Forms.TabPage();
            this.tbxLight = new System.Windows.Forms.TextBox();
            this.tbxSoilHum = new System.Windows.Forms.TextBox();
            this.tbxHum = new System.Windows.Forms.TextBox();
            this.tbxTemp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fanPage = new System.Windows.Forms.TabPage();
            this.pbxFan = new System.Windows.Forms.PictureBox();
            this.fanTimer = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tab.SuspendLayout();
            this.registerPage.SuspendLayout();
            this.dataPage.SuspendLayout();
            this.fanPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFan)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTimer
            // 
            this.dataTimer.Enabled = true;
            this.dataTimer.Interval = 200;
            this.dataTimer.Tick += new System.EventHandler(this.dataTimer_Tick);
            // 
            // dataPort
            // 
            this.dataPort.PortName = "COM12";
            // 
            // lbxGrowBox
            // 
            this.lbxGrowBox.FormattingEnabled = true;
            this.lbxGrowBox.ItemHeight = 16;
            this.lbxGrowBox.Location = new System.Drawing.Point(416, 30);
            this.lbxGrowBox.Margin = new System.Windows.Forms.Padding(4);
            this.lbxGrowBox.Name = "lbxGrowBox";
            this.lbxGrowBox.Size = new System.Drawing.Size(664, 340);
            this.lbxGrowBox.TabIndex = 8;
            // 
            // tab
            // 
            this.tab.Controls.Add(this.registerPage);
            this.tab.Controls.Add(this.dataPage);
            this.tab.Controls.Add(this.fanPage);
            this.tab.Location = new System.Drawing.Point(16, 15);
            this.tab.Margin = new System.Windows.Forms.Padding(4);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1137, 489);
            this.tab.TabIndex = 9;
            // 
            // registerPage
            // 
            this.registerPage.Controls.Add(this.btnShowData);
            this.registerPage.Controls.Add(this.label2);
            this.registerPage.Controls.Add(this.lbxGrowBox);
            this.registerPage.Controls.Add(this.cbxLight);
            this.registerPage.Controls.Add(this.cbxSoilHum);
            this.registerPage.Controls.Add(this.cbxHum);
            this.registerPage.Controls.Add(this.cbxTemp);
            this.registerPage.Controls.Add(this.label1);
            this.registerPage.Controls.Add(this.tbxName);
            this.registerPage.Controls.Add(this.addBox);
            this.registerPage.Location = new System.Drawing.Point(4, 25);
            this.registerPage.Margin = new System.Windows.Forms.Padding(4);
            this.registerPage.Name = "registerPage";
            this.registerPage.Padding = new System.Windows.Forms.Padding(4);
            this.registerPage.Size = new System.Drawing.Size(1129, 460);
            this.registerPage.TabIndex = 0;
            this.registerPage.Text = "Register";
            this.registerPage.UseVisualStyleBackColor = true;
            // 
            // btnShowData
            // 
            this.btnShowData.Location = new System.Drawing.Point(960, 378);
            this.btnShowData.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(121, 49);
            this.btnShowData.TabIndex = 16;
            this.btnShowData.Text = "Show Data";
            this.btnShowData.UseVisualStyleBackColor = true;
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Choose Sensors:";
            // 
            // cbxLight
            // 
            this.cbxLight.AutoSize = true;
            this.cbxLight.Location = new System.Drawing.Point(61, 241);
            this.cbxLight.Margin = new System.Windows.Forms.Padding(4);
            this.cbxLight.Name = "cbxLight";
            this.cbxLight.Size = new System.Drawing.Size(110, 21);
            this.cbxLight.TabIndex = 14;
            this.cbxLight.Text = "Light Sensor";
            this.cbxLight.UseVisualStyleBackColor = true;
            // 
            // cbxSoilHum
            // 
            this.cbxSoilHum.AutoSize = true;
            this.cbxSoilHum.Location = new System.Drawing.Point(61, 210);
            this.cbxSoilHum.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSoilHum.Name = "cbxSoilHum";
            this.cbxSoilHum.Size = new System.Drawing.Size(160, 21);
            this.cbxSoilHum.TabIndex = 13;
            this.cbxSoilHum.Text = "Soil Humidity Sensor";
            this.cbxSoilHum.UseVisualStyleBackColor = true;
            // 
            // cbxHum
            // 
            this.cbxHum.AutoSize = true;
            this.cbxHum.Location = new System.Drawing.Point(61, 180);
            this.cbxHum.Margin = new System.Windows.Forms.Padding(4);
            this.cbxHum.Name = "cbxHum";
            this.cbxHum.Size = new System.Drawing.Size(133, 21);
            this.cbxHum.TabIndex = 12;
            this.cbxHum.Text = "Humidity Sensor";
            this.cbxHum.UseVisualStyleBackColor = true;
            // 
            // cbxTemp
            // 
            this.cbxTemp.AutoSize = true;
            this.cbxTemp.Location = new System.Drawing.Point(61, 149);
            this.cbxTemp.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTemp.Name = "cbxTemp";
            this.cbxTemp.Size = new System.Drawing.Size(161, 21);
            this.cbxTemp.TabIndex = 11;
            this.cbxTemp.Text = "Temperature Sensor";
            this.cbxTemp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "GrowBox Name:";
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(195, 62);
            this.tbxName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(132, 22);
            this.tbxName.TabIndex = 9;
            // 
            // addBox
            // 
            this.addBox.Location = new System.Drawing.Point(61, 290);
            this.addBox.Margin = new System.Windows.Forms.Padding(4);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(121, 49);
            this.addBox.TabIndex = 8;
            this.addBox.Text = "Add GrowBox";
            this.addBox.UseVisualStyleBackColor = true;
            this.addBox.Click += new System.EventHandler(this.addBox_Click);
            // 
            // dataPage
            // 
            this.dataPage.Controls.Add(this.label10);
            this.dataPage.Controls.Add(this.label8);
            this.dataPage.Controls.Add(this.label7);
            this.dataPage.Controls.Add(this.tbxLight);
            this.dataPage.Controls.Add(this.tbxSoilHum);
            this.dataPage.Controls.Add(this.tbxHum);
            this.dataPage.Controls.Add(this.tbxTemp);
            this.dataPage.Controls.Add(this.label6);
            this.dataPage.Controls.Add(this.label5);
            this.dataPage.Controls.Add(this.label4);
            this.dataPage.Controls.Add(this.label3);
            this.dataPage.Location = new System.Drawing.Point(4, 25);
            this.dataPage.Margin = new System.Windows.Forms.Padding(4);
            this.dataPage.Name = "dataPage";
            this.dataPage.Padding = new System.Windows.Forms.Padding(4);
            this.dataPage.Size = new System.Drawing.Size(1129, 460);
            this.dataPage.TabIndex = 1;
            this.dataPage.Text = "Data";
            this.dataPage.UseVisualStyleBackColor = true;
            // 
            // tbxLight
            // 
            this.tbxLight.Location = new System.Drawing.Point(153, 192);
            this.tbxLight.Margin = new System.Windows.Forms.Padding(4);
            this.tbxLight.Name = "tbxLight";
            this.tbxLight.ReadOnly = true;
            this.tbxLight.Size = new System.Drawing.Size(42, 22);
            this.tbxLight.TabIndex = 7;
            // 
            // tbxSoilHum
            // 
            this.tbxSoilHum.Location = new System.Drawing.Point(153, 150);
            this.tbxSoilHum.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSoilHum.Name = "tbxSoilHum";
            this.tbxSoilHum.ReadOnly = true;
            this.tbxSoilHum.Size = new System.Drawing.Size(42, 22);
            this.tbxSoilHum.TabIndex = 6;
            // 
            // tbxHum
            // 
            this.tbxHum.Location = new System.Drawing.Point(153, 106);
            this.tbxHum.Margin = new System.Windows.Forms.Padding(4);
            this.tbxHum.Name = "tbxHum";
            this.tbxHum.ReadOnly = true;
            this.tbxHum.Size = new System.Drawing.Size(42, 22);
            this.tbxHum.TabIndex = 5;
            // 
            // tbxTemp
            // 
            this.tbxTemp.Location = new System.Drawing.Point(153, 63);
            this.tbxTemp.Margin = new System.Windows.Forms.Padding(4);
            this.tbxTemp.Name = "tbxTemp";
            this.tbxTemp.ReadOnly = true;
            this.tbxTemp.Size = new System.Drawing.Size(42, 22);
            this.tbxTemp.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Light:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 154);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Soil Humidity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Humidity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Temperature:";
            // 
            // fanPage
            // 
            this.fanPage.Controls.Add(this.pbxFan);
            this.fanPage.Location = new System.Drawing.Point(4, 25);
            this.fanPage.Name = "fanPage";
            this.fanPage.Size = new System.Drawing.Size(1129, 460);
            this.fanPage.TabIndex = 2;
            this.fanPage.Text = "Fan";
            this.fanPage.UseVisualStyleBackColor = true;
            // 
            // pbxFan
            // 
            this.pbxFan.Location = new System.Drawing.Point(19, 17);
            this.pbxFan.Name = "pbxFan";
            this.pbxFan.Size = new System.Drawing.Size(365, 339);
            this.pbxFan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxFan.TabIndex = 10;
            this.pbxFan.TabStop = false;
            // 
            // fanTimer
            // 
            this.fanTimer.Interval = 1000;
            this.fanTimer.Tick += new System.EventHandler(this.fanTimer_Tick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(202, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "°C";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(202, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "LUX";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(202, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "%RH";
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 514);
            this.Controls.Add(this.tab);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Display";
            this.Text = "Display";
            this.Load += new System.EventHandler(this.Display_Load);
            this.tab.ResumeLayout(false);
            this.registerPage.ResumeLayout(false);
            this.registerPage.PerformLayout();
            this.dataPage.ResumeLayout(false);
            this.dataPage.PerformLayout();
            this.fanPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxFan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer dataTimer;
        private System.IO.Ports.SerialPort dataPort;
        private System.Windows.Forms.ListBox lbxGrowBox;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage registerPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxLight;
        private System.Windows.Forms.CheckBox cbxSoilHum;
        private System.Windows.Forms.CheckBox cbxHum;
        private System.Windows.Forms.CheckBox cbxTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Button addBox;
        private System.Windows.Forms.TabPage dataPage;
        private System.Windows.Forms.TextBox tbxLight;
        private System.Windows.Forms.TextBox tbxSoilHum;
        private System.Windows.Forms.TextBox tbxHum;
        private System.Windows.Forms.TextBox tbxTemp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShowData;
        private System.Windows.Forms.TabPage fanPage;
        private System.Windows.Forms.PictureBox pbxFan;
        private System.Windows.Forms.Timer fanTimer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}

