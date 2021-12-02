namespace ComfortHomeUserInterface
{
    partial class Form3
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
            this.tbxTempTH = new System.Windows.Forms.TextBox();
            this.tbxVOCTH = new System.Windows.Forms.TextBox();
            this.tbxCO2TH = new System.Windows.Forms.TextBox();
            this.tbxHumTH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTempTH = new System.Windows.Forms.Button();
            this.btnHumTH = new System.Windows.Forms.Button();
            this.btnCo2TH = new System.Windows.Forms.Button();
            this.btnVocTH = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxTempTH
            // 
            this.tbxTempTH.Location = new System.Drawing.Point(22, 51);
            this.tbxTempTH.Margin = new System.Windows.Forms.Padding(2);
            this.tbxTempTH.Name = "tbxTempTH";
            this.tbxTempTH.Size = new System.Drawing.Size(76, 20);
            this.tbxTempTH.TabIndex = 0;
            // 
            // tbxVOCTH
            // 
            this.tbxVOCTH.Location = new System.Drawing.Point(163, 134);
            this.tbxVOCTH.Margin = new System.Windows.Forms.Padding(2);
            this.tbxVOCTH.Name = "tbxVOCTH";
            this.tbxVOCTH.Size = new System.Drawing.Size(76, 20);
            this.tbxVOCTH.TabIndex = 1;
            // 
            // tbxCO2TH
            // 
            this.tbxCO2TH.Location = new System.Drawing.Point(22, 134);
            this.tbxCO2TH.Margin = new System.Windows.Forms.Padding(2);
            this.tbxCO2TH.Name = "tbxCO2TH";
            this.tbxCO2TH.Size = new System.Drawing.Size(76, 20);
            this.tbxCO2TH.TabIndex = 2;
            // 
            // tbxHumTH
            // 
            this.tbxHumTH.Location = new System.Drawing.Point(163, 51);
            this.tbxHumTH.Margin = new System.Windows.Forms.Padding(2);
            this.tbxHumTH.Name = "tbxHumTH";
            this.tbxHumTH.Size = new System.Drawing.Size(76, 20);
            this.tbxHumTH.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Temperature Threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Humidity Threshold";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "CO2 Threshold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "VOC Threshold";
            // 
            // btnTempTH
            // 
            this.btnTempTH.Location = new System.Drawing.Point(22, 75);
            this.btnTempTH.Margin = new System.Windows.Forms.Padding(2);
            this.btnTempTH.Name = "btnTempTH";
            this.btnTempTH.Size = new System.Drawing.Size(56, 19);
            this.btnTempTH.TabIndex = 8;
            this.btnTempTH.Text = "Set";
            this.btnTempTH.UseVisualStyleBackColor = true;
            this.btnTempTH.Click += new System.EventHandler(this.btnTempTH_Click);
            // 
            // btnHumTH
            // 
            this.btnHumTH.Location = new System.Drawing.Point(163, 74);
            this.btnHumTH.Margin = new System.Windows.Forms.Padding(2);
            this.btnHumTH.Name = "btnHumTH";
            this.btnHumTH.Size = new System.Drawing.Size(56, 19);
            this.btnHumTH.TabIndex = 9;
            this.btnHumTH.Text = "Set";
            this.btnHumTH.UseVisualStyleBackColor = true;
            this.btnHumTH.Click += new System.EventHandler(this.btnHumTH_Click);
            // 
            // btnCo2TH
            // 
            this.btnCo2TH.Location = new System.Drawing.Point(22, 157);
            this.btnCo2TH.Margin = new System.Windows.Forms.Padding(2);
            this.btnCo2TH.Name = "btnCo2TH";
            this.btnCo2TH.Size = new System.Drawing.Size(56, 19);
            this.btnCo2TH.TabIndex = 10;
            this.btnCo2TH.Text = "Set";
            this.btnCo2TH.UseVisualStyleBackColor = true;
            this.btnCo2TH.Click += new System.EventHandler(this.btnCo2TH_Click);
            // 
            // btnVocTH
            // 
            this.btnVocTH.Location = new System.Drawing.Point(163, 157);
            this.btnVocTH.Margin = new System.Windows.Forms.Padding(2);
            this.btnVocTH.Name = "btnVocTH";
            this.btnVocTH.Size = new System.Drawing.Size(56, 19);
            this.btnVocTH.TabIndex = 11;
            this.btnVocTH.Text = "Set";
            this.btnVocTH.UseVisualStyleBackColor = true;
            this.btnVocTH.Click += new System.EventHandler(this.btnVocTH_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(292, 236);
            this.Controls.Add(this.btnVocTH);
            this.Controls.Add(this.btnCo2TH);
            this.Controls.Add(this.btnHumTH);
            this.Controls.Add(this.btnTempTH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxHumTH);
            this.Controls.Add(this.tbxCO2TH);
            this.Controls.Add(this.tbxVOCTH);
            this.Controls.Add(this.tbxTempTH);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form3";
            this.Text = "Threshold Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxTempTH;
        private System.Windows.Forms.TextBox tbxVOCTH;
        private System.Windows.Forms.TextBox tbxCO2TH;
        private System.Windows.Forms.TextBox tbxHumTH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTempTH;
        private System.Windows.Forms.Button btnHumTH;
        private System.Windows.Forms.Button btnCo2TH;
        private System.Windows.Forms.Button btnVocTH;
    }
}