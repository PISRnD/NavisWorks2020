namespace Test
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
            this.label_HeaderTVersion = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_FooterTVersion2 = new System.Windows.Forms.Label();
            this.label_FooterTVersion1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_HeaderTVersion
            // 
            this.label_HeaderTVersion.AutoSize = true;
            this.label_HeaderTVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_HeaderTVersion.ForeColor = System.Drawing.Color.Black;
            this.label_HeaderTVersion.Location = new System.Drawing.Point(156, 16);
            this.label_HeaderTVersion.Name = "label_HeaderTVersion";
            this.label_HeaderTVersion.Size = new System.Drawing.Size(206, 16);
            this.label_HeaderTVersion.TabIndex = 0;
            this.label_HeaderTVersion.Text = "True Height of Column : MainPage";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(12, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(628, 5);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.label_FooterTVersion2);
            this.panel3.Controls.Add(this.label_FooterTVersion1);
            this.panel3.Location = new System.Drawing.Point(154, 224);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 50);
            this.panel3.TabIndex = 5;
            // 
            // label_FooterTVersion2
            // 
            this.label_FooterTVersion2.AutoSize = true;
            this.label_FooterTVersion2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FooterTVersion2.ForeColor = System.Drawing.Color.White;
            this.label_FooterTVersion2.Location = new System.Drawing.Point(88, 26);
            this.label_FooterTVersion2.Name = "label_FooterTVersion2";
            this.label_FooterTVersion2.Size = new System.Drawing.Size(182, 16);
            this.label_FooterTVersion2.TabIndex = 1;
            this.label_FooterTVersion2.Text = "True Height of Column - v1.0.0";
            // 
            // label_FooterTVersion1
            // 
            this.label_FooterTVersion1.AutoSize = true;
            this.label_FooterTVersion1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FooterTVersion1.ForeColor = System.Drawing.Color.White;
            this.label_FooterTVersion1.Location = new System.Drawing.Point(69, 10);
            this.label_FooterTVersion1.Name = "label_FooterTVersion1";
            this.label_FooterTVersion1.Size = new System.Drawing.Size(212, 16);
            this.label_FooterTVersion1.TabIndex = 0;
            this.label_FooterTVersion1.Text = "Pinnacle Infotech Solutions - R && D";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_HeaderTVersion);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(126, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 50);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
           // this.pictureBox1.Image = global::Test.Properties.Resources.PinnacleWPFLogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 534);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_HeaderTVersion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_FooterTVersion2;
        private System.Windows.Forms.Label label_FooterTVersion1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}