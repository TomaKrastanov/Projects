namespace ComfortHomeUserInterface
{
    partial class Form4
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
            this.btn_dassl = new System.Windows.Forms.Button();
            this.btn_en = new System.Windows.Forms.Button();
            this.btn_bcast = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_data = new System.Windows.Forms.Label();
            this.lb_message = new System.Windows.Forms.ListBox();
            this.lb_data = new System.Windows.Forms.ListBox();
            this.tb_bcast = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_dassl
            // 
            this.btn_dassl.Location = new System.Drawing.Point(69, 44);
            this.btn_dassl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_dassl.Name = "btn_dassl";
            this.btn_dassl.Size = new System.Drawing.Size(56, 19);
            this.btn_dassl.TabIndex = 0;
            this.btn_dassl.Text = "dassl";
            this.btn_dassl.UseVisualStyleBackColor = true;
            this.btn_dassl.Click += new System.EventHandler(this.btn_dassl_Click);
            // 
            // btn_en
            // 
            this.btn_en.Location = new System.Drawing.Point(69, 67);
            this.btn_en.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_en.Name = "btn_en";
            this.btn_en.Size = new System.Drawing.Size(56, 19);
            this.btn_en.TabIndex = 1;
            this.btn_en.Text = "en";
            this.btn_en.UseVisualStyleBackColor = true;
            this.btn_en.Click += new System.EventHandler(this.btn_en_Click);
            // 
            // btn_bcast
            // 
            this.btn_bcast.Location = new System.Drawing.Point(69, 161);
            this.btn_bcast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_bcast.Name = "btn_bcast";
            this.btn_bcast.Size = new System.Drawing.Size(56, 19);
            this.btn_bcast.TabIndex = 4;
            this.btn_bcast.Text = "bcast";
            this.btn_bcast.UseVisualStyleBackColor = true;
            this.btn_bcast.Click += new System.EventHandler(this.btn_bcast_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(1099, 11);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(56, 19);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Location = new System.Drawing.Point(217, 23);
            this.lbl_message.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(68, 13);
            this.lbl_message.TabIndex = 6;
            this.lbl_message.Text = "MessageBox";
            // 
            // lbl_data
            // 
            this.lbl_data.AutoSize = true;
            this.lbl_data.Location = new System.Drawing.Point(525, 22);
            this.lbl_data.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.Size = new System.Drawing.Size(48, 13);
            this.lbl_data.TabIndex = 7;
            this.lbl_data.Text = "DataBox";
            // 
            // lb_message
            // 
            this.lb_message.FormattingEnabled = true;
            this.lb_message.Location = new System.Drawing.Point(219, 39);
            this.lb_message.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lb_message.Name = "lb_message";
            this.lb_message.Size = new System.Drawing.Size(264, 355);
            this.lb_message.TabIndex = 8;
            // 
            // lb_data
            // 
            this.lb_data.FormattingEnabled = true;
            this.lb_data.Location = new System.Drawing.Point(527, 38);
            this.lb_data.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lb_data.Name = "lb_data";
            this.lb_data.Size = new System.Drawing.Size(628, 355);
            this.lb_data.TabIndex = 9;
            // 
            // tb_bcast
            // 
            this.tb_bcast.Location = new System.Drawing.Point(69, 185);
            this.tb_bcast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_bcast.Name = "tb_bcast";
            this.tb_bcast.Size = new System.Drawing.Size(98, 20);
            this.tb_bcast.TabIndex = 10;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1169, 428);
            this.Controls.Add(this.tb_bcast);
            this.Controls.Add(this.lb_data);
            this.Controls.Add(this.lb_message);
            this.Controls.Add(this.lbl_data);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_bcast);
            this.Controls.Add(this.btn_en);
            this.Controls.Add(this.btn_dassl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form4";
            this.Text = "Zigbee Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dassl;
        private System.Windows.Forms.Button btn_en;
        private System.Windows.Forms.Button btn_bcast;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.ListBox lb_message;
        private System.Windows.Forms.ListBox lb_data;
        private System.Windows.Forms.TextBox tb_bcast;
    }
}