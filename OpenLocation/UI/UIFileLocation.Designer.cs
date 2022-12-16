namespace NwcOpenLocation
{
    partial class UIFileLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIFileLocation));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewFileInfo = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(650, 5);
            this.panel3.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewFileInfo);
            this.panel1.Location = new System.Drawing.Point(2, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 175);
            this.panel1.TabIndex = 13;
            // 
            // dataGridViewFileInfo
            // 
            this.dataGridViewFileInfo.AllowUserToAddRows = false;
            this.dataGridViewFileInfo.AllowUserToDeleteRows = false;
            this.dataGridViewFileInfo.AllowUserToOrderColumns = true;
            this.dataGridViewFileInfo.AllowUserToResizeRows = false;
            this.dataGridViewFileInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFileInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.FileLocation});
            this.dataGridViewFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFileInfo.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFileInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewFileInfo.Name = "dataGridViewFileInfo";
            this.dataGridViewFileInfo.ReadOnly = true;
            this.dataGridViewFileInfo.RowHeadersVisible = false;
            this.dataGridViewFileInfo.RowTemplate.Height = 35;
            this.dataGridViewFileInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFileInfo.Size = new System.Drawing.Size(650, 175);
            this.dataGridViewFileInfo.TabIndex = 55;
            this.dataGridViewFileInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFileInfoCellContentClick);
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FileName.DefaultCellStyle = dataGridViewCellStyle1;
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 87;
            // 
            // FileLocation
            // 
            this.FileLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
            this.FileLocation.DefaultCellStyle = dataGridViewCellStyle2;
            this.FileLocation.HeaderText = "FileLocation";
            this.FileLocation.Name = "FileLocation";
            this.FileLocation.ReadOnly = true;
            this.FileLocation.Width = 102;
            // 
            // UIFileLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 185);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UIFileLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Location Extractor | PiVDCNavisworks";
            this.Load += new System.EventHandler(this.MainPageLoad);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileLocation;
        public System.Windows.Forms.DataGridView dataGridViewFileInfo;
    }
}