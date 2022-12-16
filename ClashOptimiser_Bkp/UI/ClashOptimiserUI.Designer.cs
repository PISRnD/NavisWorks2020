namespace ClashOptimiser
{
    partial class ClashOptimiserUI
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
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClashOptimiserUI));
            this.lstAvailFiles = new System.Windows.Forms.ListBox();
            this.dataClashGrid = new System.Windows.Forms.DataGridView();
            this.colImg = new System.Windows.Forms.DataGridViewImageColumn();
            this.colClash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNwAc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReviewed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApproved = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOptimise = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.numTolerence = new System.Windows.Forms.NumericUpDown();
            this.lblTolerence = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkIgnoreApproved = new System.Windows.Forms.CheckBox();
            this.chkIgnoreReviewed = new System.Windows.Forms.CheckBox();
            this.trackTolerence = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdBtnBoundingBox = new System.Windows.Forms.RadioButton();
            this.rdBtnCategory = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkArrange = new System.Windows.Forms.CheckBox();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.cmbPropertyName = new System.Windows.Forms.ComboBox();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.lblParameterValue = new System.Windows.Forms.Label();
            this.cmbPropertyValue = new System.Windows.Forms.ComboBox();
            this.lblPropertyNameOR = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.lblPropertyValue = new System.Windows.Forms.Label();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.cmbTabName = new System.Windows.Forms.ComboBox();
            this.lblTabNameOR = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataClashGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerence)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTolerence)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.lstAvailFiles);
            groupBox1.Location = new System.Drawing.Point(1, 10);
            groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBox1.Size = new System.Drawing.Size(628, 97);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Available Files";
            // 
            // lstAvailFiles
            // 
            this.lstAvailFiles.FormattingEnabled = true;
            this.lstAvailFiles.ItemHeight = 16;
            this.lstAvailFiles.Location = new System.Drawing.Point(6, 23);
            this.lstAvailFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstAvailFiles.Name = "lstAvailFiles";
            this.lstAvailFiles.Size = new System.Drawing.Size(616, 68);
            this.lstAvailFiles.TabIndex = 0;
            this.lstAvailFiles.SelectedIndexChanged += new System.EventHandler(this.lstAvailFiles_SelectedIndexChanged);
            // 
            // dataClashGrid
            // 
            this.dataClashGrid.AllowUserToAddRows = false;
            this.dataClashGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataClashGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImg,
            this.colClash,
            this.colNwAc,
            this.colReviewed,
            this.colApproved,
            this.colSort});
            this.dataClashGrid.Location = new System.Drawing.Point(6, 109);
            this.dataClashGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataClashGrid.Name = "dataClashGrid";
            this.dataClashGrid.ReadOnly = true;
            this.dataClashGrid.RowHeadersVisible = false;
            this.dataClashGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataClashGrid.Size = new System.Drawing.Size(623, 117);
            this.dataClashGrid.TabIndex = 0;
            this.dataClashGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataClashGrid_CellContentClick);
            this.dataClashGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataClashGrid_CellPainting);
            this.dataClashGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.dataClashGrid_Paint);
            // 
            // colImg
            // 
            this.colImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colImg.HeaderText = "";
            this.colImg.MinimumWidth = 20;
            this.colImg.Name = "colImg";
            this.colImg.ReadOnly = true;
            this.colImg.Width = 20;
            // 
            // colClash
            // 
            this.colClash.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colClash.HeaderText = "Clash Test";
            this.colClash.Name = "colClash";
            this.colClash.ReadOnly = true;
            this.colClash.Width = 93;
            // 
            // colNwAc
            // 
            this.colNwAc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colNwAc.HeaderText = "New/Active";
            this.colNwAc.Name = "colNwAc";
            this.colNwAc.ReadOnly = true;
            this.colNwAc.Width = 96;
            // 
            // colReviewed
            // 
            this.colReviewed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colReviewed.HeaderText = "Reviewed";
            this.colReviewed.Name = "colReviewed";
            this.colReviewed.ReadOnly = true;
            this.colReviewed.Width = 86;
            // 
            // colApproved
            // 
            this.colApproved.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colApproved.HeaderText = "Approved";
            this.colApproved.Name = "colApproved";
            this.colApproved.ReadOnly = true;
            this.colApproved.Width = 85;
            // 
            // colSort
            // 
            this.colSort.HeaderText = "Sort";
            this.colSort.Name = "colSort";
            this.colSort.ReadOnly = true;
            this.colSort.Visible = false;
            // 
            // btnOptimise
            // 
            this.btnOptimise.Location = new System.Drawing.Point(532, 370);
            this.btnOptimise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOptimise.Name = "btnOptimise";
            this.btnOptimise.Size = new System.Drawing.Size(81, 31);
            this.btnOptimise.TabIndex = 1;
            this.btnOptimise.Text = "OPTIMIZE";
            this.btnOptimise.UseVisualStyleBackColor = true;
            this.btnOptimise.Click += new System.EventHandler(this.btnOptimise_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 331);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(626, 28);
            this.progressBar1.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(397, 367);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 31);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "UNGROUP";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name :";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(54, 233);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(106, 16);
            this.lblTest.TabIndex = 4;
            this.lblTest.Text = "Clash Test Name";
            this.lblTest.Click += new System.EventHandler(this.lblTest_Click);
            // 
            // numTolerence
            // 
            this.numTolerence.DecimalPlaces = 1;
            this.numTolerence.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numTolerence.Location = new System.Drawing.Point(331, 231);
            this.numTolerence.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numTolerence.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numTolerence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTolerence.Name = "numTolerence";
            this.numTolerence.Size = new System.Drawing.Size(79, 22);
            this.numTolerence.TabIndex = 5;
            this.numTolerence.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTolerence.Visible = false;
            // 
            // lblTolerence
            // 
            this.lblTolerence.AutoSize = true;
            this.lblTolerence.Location = new System.Drawing.Point(254, 233);
            this.lblTolerence.Name = "lblTolerence";
            this.lblTolerence.Size = new System.Drawing.Size(70, 16);
            this.lblTolerence.TabIndex = 6;
            this.lblTolerence.Text = "Tolerence :";
            this.lblTolerence.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkIgnoreApproved);
            this.groupBox2.Controls.Add(this.chkIgnoreReviewed);
            this.groupBox2.Location = new System.Drawing.Point(2, 261);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(302, 39);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ignore";
            // 
            // chkIgnoreApproved
            // 
            this.chkIgnoreApproved.AutoSize = true;
            this.chkIgnoreApproved.Location = new System.Drawing.Point(199, 16);
            this.chkIgnoreApproved.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkIgnoreApproved.Name = "chkIgnoreApproved";
            this.chkIgnoreApproved.Size = new System.Drawing.Size(79, 20);
            this.chkIgnoreApproved.TabIndex = 0;
            this.chkIgnoreApproved.Text = "Approved";
            this.chkIgnoreApproved.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreReviewed
            // 
            this.chkIgnoreReviewed.AutoSize = true;
            this.chkIgnoreReviewed.Location = new System.Drawing.Point(111, 16);
            this.chkIgnoreReviewed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkIgnoreReviewed.Name = "chkIgnoreReviewed";
            this.chkIgnoreReviewed.Size = new System.Drawing.Size(80, 20);
            this.chkIgnoreReviewed.TabIndex = 0;
            this.chkIgnoreReviewed.Text = "Reviewed";
            this.chkIgnoreReviewed.UseVisualStyleBackColor = true;
            // 
            // trackTolerence
            // 
            this.trackTolerence.Location = new System.Drawing.Point(79, 12);
            this.trackTolerence.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackTolerence.Maximum = 200;
            this.trackTolerence.Minimum = 1;
            this.trackTolerence.Name = "trackTolerence";
            this.trackTolerence.Size = new System.Drawing.Size(180, 45);
            this.trackTolerence.TabIndex = 9;
            this.trackTolerence.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackTolerence.Value = 35;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.trackTolerence);
            this.groupBox3.Location = new System.Drawing.Point(310, 259);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(319, 64);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tolerence";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Global";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Local";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(1, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(630, 5);
            this.panel6.TabIndex = 14;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // rdBtnBoundingBox
            // 
            this.rdBtnBoundingBox.AutoSize = true;
            this.rdBtnBoundingBox.Checked = true;
            this.rdBtnBoundingBox.Location = new System.Drawing.Point(2, 372);
            this.rdBtnBoundingBox.Name = "rdBtnBoundingBox";
            this.rdBtnBoundingBox.Size = new System.Drawing.Size(106, 20);
            this.rdBtnBoundingBox.TabIndex = 16;
            this.rdBtnBoundingBox.TabStop = true;
            this.rdBtnBoundingBox.Text = "Bounding Box";
            this.rdBtnBoundingBox.UseVisualStyleBackColor = true;
            this.rdBtnBoundingBox.CheckedChanged += new System.EventHandler(this.rdBtnBoundingBox_CheckedChanged);
            // 
            // rdBtnCategory
            // 
            this.rdBtnCategory.AutoSize = true;
            this.rdBtnCategory.Location = new System.Drawing.Point(161, 372);
            this.rdBtnCategory.Name = "rdBtnCategory";
            this.rdBtnCategory.Size = new System.Drawing.Size(59, 20);
            this.rdBtnCategory.TabIndex = 17;
            this.rdBtnCategory.Text = "Name";
            this.rdBtnCategory.UseVisualStyleBackColor = true;
            this.rdBtnCategory.CheckedChanged += new System.EventHandler(this.rdBtnCategory_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // chkArrange
            // 
            this.chkArrange.AutoSize = true;
            this.chkArrange.Location = new System.Drawing.Point(273, 373);
            this.chkArrange.Name = "chkArrange";
            this.chkArrange.Size = new System.Drawing.Size(71, 20);
            this.chkArrange.TabIndex = 18;
            this.chkArrange.Text = "Arrange";
            this.chkArrange.UseVisualStyleBackColor = true;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(412, 409);
            this.txtCategoryName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(203, 22);
            this.txtCategoryName.TabIndex = 19;
            this.txtCategoryName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCategoryName_KeyPress);
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(5, 407);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(73, 16);
            this.lblCategoryName.TabIndex = 20;
            this.lblCategoryName.Text = "Tab Name :";
            // 
            // cmbPropertyName
            // 
            this.cmbPropertyName.FormattingEnabled = true;
            this.cmbPropertyName.Location = new System.Drawing.Point(122, 435);
            this.cmbPropertyName.Name = "cmbPropertyName";
            this.cmbPropertyName.Size = new System.Drawing.Size(234, 24);
            this.cmbPropertyName.TabIndex = 22;
            this.cmbPropertyName.SelectedIndexChanged += new System.EventHandler(this.cmbPropertyName_SelectedIndexChanged);
            // 
            // lblParameterName
            // 
            this.lblParameterName.AutoSize = true;
            this.lblParameterName.Location = new System.Drawing.Point(5, 435);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(102, 16);
            this.lblParameterName.TabIndex = 21;
            this.lblParameterName.Text = "Property Name :";
            // 
            // lblParameterValue
            // 
            this.lblParameterValue.AutoSize = true;
            this.lblParameterValue.Location = new System.Drawing.Point(7, 470);
            this.lblParameterValue.Name = "lblParameterValue";
            this.lblParameterValue.Size = new System.Drawing.Size(96, 16);
            this.lblParameterValue.TabIndex = 23;
            this.lblParameterValue.Text = "Property Value:";
            // 
            // cmbPropertyValue
            // 
            this.cmbPropertyValue.FormattingEnabled = true;
            this.cmbPropertyValue.Location = new System.Drawing.Point(122, 470);
            this.cmbPropertyValue.Name = "cmbPropertyValue";
            this.cmbPropertyValue.Size = new System.Drawing.Size(234, 24);
            this.cmbPropertyValue.TabIndex = 24;
            // 
            // lblPropertyNameOR
            // 
            this.lblPropertyNameOR.AutoSize = true;
            this.lblPropertyNameOR.Location = new System.Drawing.Point(373, 444);
            this.lblPropertyNameOR.Name = "lblPropertyNameOR";
            this.lblPropertyNameOR.Size = new System.Drawing.Size(26, 16);
            this.lblPropertyNameOR.TabIndex = 25;
            this.lblPropertyNameOR.Text = "OR";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(414, 437);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(201, 22);
            this.txtPropertyName.TabIndex = 26;
            this.txtPropertyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPropertyName_KeyPress);
            // 
            // lblPropertyValue
            // 
            this.lblPropertyValue.AutoSize = true;
            this.lblPropertyValue.Location = new System.Drawing.Point(373, 476);
            this.lblPropertyValue.Name = "lblPropertyValue";
            this.lblPropertyValue.Size = new System.Drawing.Size(26, 16);
            this.lblPropertyValue.TabIndex = 27;
            this.lblPropertyValue.Text = "OR";
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.Location = new System.Drawing.Point(412, 472);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(203, 22);
            this.txtPropertyValue.TabIndex = 28;
            // 
            // cmbTabName
            // 
            this.cmbTabName.FormattingEnabled = true;
            this.cmbTabName.Location = new System.Drawing.Point(122, 406);
            this.cmbTabName.Name = "cmbTabName";
            this.cmbTabName.Size = new System.Drawing.Size(234, 24);
            this.cmbTabName.TabIndex = 29;
            this.cmbTabName.SelectedIndexChanged += new System.EventHandler(this.cmbTabName_SelectedIndexChanged);
            // 
            // lblTabNameOR
            // 
            this.lblTabNameOR.AutoSize = true;
            this.lblTabNameOR.Location = new System.Drawing.Point(373, 414);
            this.lblTabNameOR.Name = "lblTabNameOR";
            this.lblTabNameOR.Size = new System.Drawing.Size(26, 16);
            this.lblTabNameOR.TabIndex = 30;
            this.lblTabNameOR.Text = "OR";
            // 
            // UI_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 498);
            this.Controls.Add(this.lblTabNameOR);
            this.Controls.Add(this.cmbTabName);
            this.Controls.Add(this.txtPropertyValue);
            this.Controls.Add(this.lblPropertyValue);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.lblPropertyNameOR);
            this.Controls.Add(this.cmbPropertyValue);
            this.Controls.Add(this.lblParameterValue);
            this.Controls.Add(this.cmbPropertyName);
            this.Controls.Add(this.lblParameterName);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.chkArrange);
            this.Controls.Add(this.rdBtnCategory);
            this.Controls.Add(this.rdBtnBoundingBox);
            this.Controls.Add(this.btnOptimise);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblTolerence);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.numTolerence);
            this.Controls.Add(this.dataClashGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTest);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UI_Display";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clash Optimiser | NavisworksPackage - v3.0.0";
            this.Load += new System.EventHandler(this.UI_Display_Load);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataClashGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerence)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTolerence)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataClashGrid;
        private System.Windows.Forms.Button btnOptimise;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.NumericUpDown numTolerence;
        private System.Windows.Forms.Label lblTolerence;
        private System.Windows.Forms.ListBox lstAvailFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkIgnoreApproved;
        private System.Windows.Forms.CheckBox chkIgnoreReviewed;
        private System.Windows.Forms.TrackBar trackTolerence;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdBtnBoundingBox;
        private System.Windows.Forms.RadioButton rdBtnCategory;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chkArrange;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.ComboBox cmbPropertyName;
        private System.Windows.Forms.Label lblParameterName;
        private System.Windows.Forms.Label lblParameterValue;
        private System.Windows.Forms.ComboBox cmbPropertyValue;
        private System.Windows.Forms.DataGridViewImageColumn colImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNwAc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReviewed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApproved;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSort;
        private System.Windows.Forms.Label lblPropertyNameOR;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.Label lblPropertyValue;
        private System.Windows.Forms.TextBox txtPropertyValue;
        private System.Windows.Forms.ComboBox cmbTabName;
        private System.Windows.Forms.Label lblTabNameOR;
    }
}