namespace CreateRevitSchedule
{
    partial class UI_CreateSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_CreateSchedule));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox_Properties = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_categories = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Schedule = new System.Windows.Forms.DataGridView();
            this.button_show = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_ExportMultiple = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label_HeaderTVersion = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Schedule)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_Properties);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBox_categories);
            this.groupBox1.Location = new System.Drawing.Point(3, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(293, 769);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Fields";
            // 
            // listBox_Properties
            // 
            this.listBox_Properties.FormattingEnabled = true;
            this.listBox_Properties.ItemHeight = 16;
            this.listBox_Properties.Location = new System.Drawing.Point(6, 391);
            this.listBox_Properties.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox_Properties.Name = "listBox_Properties";
            this.listBox_Properties.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Properties.Size = new System.Drawing.Size(281, 372);
            this.listBox_Properties.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fields";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categories";
            // 
            // listBox_categories
            // 
            this.listBox_categories.FormattingEnabled = true;
            this.listBox_categories.ItemHeight = 16;
            this.listBox_categories.Location = new System.Drawing.Point(6, 52);
            this.listBox_categories.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox_categories.Name = "listBox_categories";
            this.listBox_categories.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_categories.Size = new System.Drawing.Size(281, 308);
            this.listBox_categories.TabIndex = 0;
            this.listBox_categories.SelectedIndexChanged += new System.EventHandler(this.listBox_categories_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_Schedule);
            this.groupBox2.Location = new System.Drawing.Point(302, 60);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(859, 770);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Schedule";
            // 
            // dataGridView_Schedule
            // 
            this.dataGridView_Schedule.AllowUserToOrderColumns = true;
            this.dataGridView_Schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Schedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Schedule.Location = new System.Drawing.Point(3, 19);
            this.dataGridView_Schedule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView_Schedule.Name = "dataGridView_Schedule";
            this.dataGridView_Schedule.Size = new System.Drawing.Size(853, 747);
            this.dataGridView_Schedule.TabIndex = 0;
            // 
            // button_show
            // 
            this.button_show.Location = new System.Drawing.Point(914, 867);
            this.button_show.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_show.Name = "button_show";
            this.button_show.Size = new System.Drawing.Size(83, 28);
            this.button_show.TabIndex = 2;
            this.button_show.Text = "Show";
            this.button_show.UseVisualStyleBackColor = true;
            this.button_show.Click += new System.EventHandler(this.button_show_Click);
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(1003, 867);
            this.button_export.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(154, 28);
            this.button_export.TabIndex = 3;
            this.button_export.Text = "Export to spreadsheet";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 833);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1160, 28);
            this.progressBar1.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // button_ExportMultiple
            // 
            this.button_ExportMultiple.Location = new System.Drawing.Point(3, 867);
            this.button_ExportMultiple.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_ExportMultiple.Name = "button_ExportMultiple";
            this.button_ExportMultiple.Size = new System.Drawing.Size(136, 28);
            this.button_ExportMultiple.TabIndex = 5;
            this.button_ExportMultiple.Text = "Export Multiple";
            this.button_ExportMultiple.UseVisualStyleBackColor = true;
            this.button_ExportMultiple.Visible = false;
            this.button_ExportMultiple.Click += new System.EventHandler(this.button_ExportMultiple_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label_HeaderTVersion);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1160, 50);
            this.panel5.TabIndex = 14;
            // 
            // label_HeaderTVersion
            // 
            this.label_HeaderTVersion.AutoSize = true;
            this.label_HeaderTVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_HeaderTVersion.ForeColor = System.Drawing.Color.Black;
            this.label_HeaderTVersion.Location = new System.Drawing.Point(477, 16);
            this.label_HeaderTVersion.Name = "label_HeaderTVersion";
            this.label_HeaderTVersion.Size = new System.Drawing.Size(186, 16);
            this.label_HeaderTVersion.TabIndex = 0;
            this.label_HeaderTVersion.Text = "Create Revit Schedule : Option";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CreateRevitSchedule.Properties.Resources.PinnacleWPFLogo;
            this.pictureBox2.Location = new System.Drawing.Point(0, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 47);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(0, 52);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1160, 5);
            this.panel6.TabIndex = 15;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Location = new System.Drawing.Point(0, 903);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1160, 50);
            this.panel7.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(490, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Create Revit Schedule - v1.0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(474, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pinnacle Infotech Solutions - R && D";
            // 
            // UI_CreateSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 954);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.button_ExportMultiple);
            this.Controls.Add(this.button_show);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UI_CreateSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Option";
            this.Load += new System.EventHandler(this.UI_CreateSchedule_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Schedule)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox_Properties;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_categories;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_show;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dataGridView_Schedule;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_ExportMultiple;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label_HeaderTVersion;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}