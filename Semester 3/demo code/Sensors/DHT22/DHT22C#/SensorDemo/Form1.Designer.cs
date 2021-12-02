
namespace SensorDemo
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
            this.cbComport = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lbMessage = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbComport
            // 
            this.cbComport.FormattingEnabled = true;
            this.cbComport.Location = new System.Drawing.Point(88, 126);
            this.cbComport.Name = "cbComport";
            this.cbComport.Size = new System.Drawing.Size(238, 29);
            this.cbComport.TabIndex = 0;
            this.cbComport.SelectedIndexChanged += new System.EventHandler(this.cbComport_SelectedIndexChanged);
            // 
            // lbMessage
            // 
            this.lbMessage.FormattingEnabled = true;
            this.lbMessage.ItemHeight = 21;
            this.lbMessage.Location = new System.Drawing.Point(500, 126);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(661, 571);
            this.lbMessage.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(88, 434);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(234, 112);
            this.btnRequest.TabIndex = 2;
            this.btnRequest.Text = "Request Data";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 788);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.cbComport);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComport;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ListBox lbMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRequest;
    }
}

