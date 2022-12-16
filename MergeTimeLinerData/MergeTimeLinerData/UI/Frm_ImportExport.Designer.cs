namespace TimelineMerger
{
    partial class Frm_ImportExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ImportExport));
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(6, 19);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(98, 33);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Start Export File";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OpenFile);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 67);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose Simulation Appearence File";
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(6, 19);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(98, 33);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "Open File";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(500, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 67);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose Task File";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Location = new System.Drawing.Point(256, 95);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(216, 67);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose Configuration File";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 33);
            this.button2.TabIndex = 0;
            this.button2.Text = "Open File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            this.openFileDialog3.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog3_FileOk);
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(256, 193);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(216, 16);
            this.Progress.TabIndex = 5;
            // 
            // Frm_ImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 215);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_ImportExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportExport";
            this.Load += new System.EventHandler(this.Frm_ImportExport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.ProgressBar Progress;
    }
}