namespace ServicesBelowCeiling
{
    partial class UIServicesBelowCeiling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIServicesBelowCeiling));
            this.listBox_categories = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Find = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblFloorHeight = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.numFloorHeight = new System.Windows.Forms.TextBox();
            this.rdBtnAlignedCeiling = new System.Windows.Forms.RadioButton();
            this.txtCategoryDisplayName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.lblPropertyName = new System.Windows.Forms.Label();
            this.rdBtnBelowCeiling = new System.Windows.Forms.RadioButton();
            this.rdBtnFloorHeight = new System.Windows.Forms.RadioButton();
            this.lblPropertyValue = new System.Windows.Forms.Label();
            this.lblPropertyNameOR = new System.Windows.Forms.Label();
            this.lblPropertyValueOR = new System.Windows.Forms.Label();
            this.txtBoxPropertyName = new System.Windows.Forms.TextBox();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.cmbPropertyValue = new System.Windows.Forms.ComboBox();
            this.cmbPropertyName = new System.Windows.Forms.ComboBox();
            this.rdBtnRevit = new System.Windows.Forms.RadioButton();
            this.rdBtnOther = new System.Windows.Forms.RadioButton();
            this.grpFileOption = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.grpFileOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_categories
            // 
            this.listBox_categories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_categories.Font = new System.Drawing.Font("Candara", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_categories.FormattingEnabled = true;
            this.listBox_categories.Location = new System.Drawing.Point(3, 19);
            this.listBox_categories.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox_categories.Name = "listBox_categories";
            this.listBox_categories.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_categories.Size = new System.Drawing.Size(540, 366);
            this.listBox_categories.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_categories);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(546, 389);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Revit categories to find";
            // 
            // button_Find
            // 
            this.button_Find.Location = new System.Drawing.Point(481, 643);
            this.button_Find.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Find.Name = "button_Find";
            this.button_Find.Size = new System.Drawing.Size(68, 28);
            this.button_Find.TabIndex = 3;
            this.button_Find.Text = "Find";
            this.button_Find.UseVisualStyleBackColor = true;
            this.button_Find.Click += new System.EventHandler(this.button_Find_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(3, -4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(559, 10);
            this.panel6.TabIndex = 15;
            // 
            // lblFloorHeight
            // 
            this.lblFloorHeight.AutoSize = true;
            this.lblFloorHeight.Location = new System.Drawing.Point(12, 492);
            this.lblFloorHeight.Name = "lblFloorHeight";
            this.lblFloorHeight.Size = new System.Drawing.Size(111, 16);
            this.lblFloorHeight.TabIndex = 17;
            this.lblFloorHeight.Text = "From Floor Height";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(366, 492);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(30, 16);
            this.lblUnit.TabIndex = 18;
            this.lblUnit.Text = "Unit";
            // 
            // cmbUnit
            // 
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(402, 485);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(147, 24);
            this.cmbUnit.TabIndex = 19;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(212, 492);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(36, 16);
            this.lblLevel.TabIndex = 22;
            this.lblLevel.Text = "Level";
            // 
            // cmbLevel
            // 
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(254, 485);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(106, 24);
            this.cmbLevel.TabIndex = 23;
            // 
            // numFloorHeight
            // 
            this.numFloorHeight.Location = new System.Drawing.Point(131, 489);
            this.numFloorHeight.Name = "numFloorHeight";
            this.numFloorHeight.Size = new System.Drawing.Size(75, 22);
            this.numFloorHeight.TabIndex = 24;
            // 
            // rdBtnAlignedCeiling
            // 
            this.rdBtnAlignedCeiling.AutoSize = true;
            this.rdBtnAlignedCeiling.Location = new System.Drawing.Point(6, 527);
            this.rdBtnAlignedCeiling.Name = "rdBtnAlignedCeiling";
            this.rdBtnAlignedCeiling.Size = new System.Drawing.Size(144, 20);
            this.rdBtnAlignedCeiling.TabIndex = 26;
            this.rdBtnAlignedCeiling.Text = "AlignedCeilingCheck";
            this.rdBtnAlignedCeiling.UseVisualStyleBackColor = true;
            this.rdBtnAlignedCeiling.CheckedChanged += new System.EventHandler(this.rdBtnAlignedCeiling_CheckedChanged);
            // 
            // txtCategoryDisplayName
            // 
            this.txtCategoryDisplayName.Location = new System.Drawing.Point(131, 550);
            this.txtCategoryDisplayName.Name = "txtCategoryDisplayName";
            this.txtCategoryDisplayName.Size = new System.Drawing.Size(164, 22);
            this.txtCategoryDisplayName.TabIndex = 27;
            this.txtCategoryDisplayName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCategoryDisplayName_KeyPress);
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(18, 550);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(61, 16);
            this.lblCategoryName.TabIndex = 29;
            this.lblCategoryName.Text = "TabName";
            // 
            // lblPropertyName
            // 
            this.lblPropertyName.AutoSize = true;
            this.lblPropertyName.Location = new System.Drawing.Point(21, 579);
            this.lblPropertyName.Name = "lblPropertyName";
            this.lblPropertyName.Size = new System.Drawing.Size(90, 16);
            this.lblPropertyName.TabIndex = 30;
            this.lblPropertyName.Text = "PropertyName";
            // 
            // rdBtnBelowCeiling
            // 
            this.rdBtnBelowCeiling.AutoSize = true;
            this.rdBtnBelowCeiling.Checked = true;
            this.rdBtnBelowCeiling.Location = new System.Drawing.Point(154, 459);
            this.rdBtnBelowCeiling.Name = "rdBtnBelowCeiling";
            this.rdBtnBelowCeiling.Size = new System.Drawing.Size(99, 20);
            this.rdBtnBelowCeiling.TabIndex = 31;
            this.rdBtnBelowCeiling.TabStop = true;
            this.rdBtnBelowCeiling.Text = "BelowCeiling";
            this.rdBtnBelowCeiling.UseVisualStyleBackColor = true;
            this.rdBtnBelowCeiling.CheckedChanged += new System.EventHandler(this.rdBtnBelowCeiling_CheckedChanged);
            // 
            // rdBtnFloorHeight
            // 
            this.rdBtnFloorHeight.AutoSize = true;
            this.rdBtnFloorHeight.Location = new System.Drawing.Point(15, 459);
            this.rdBtnFloorHeight.Name = "rdBtnFloorHeight";
            this.rdBtnFloorHeight.Size = new System.Drawing.Size(91, 20);
            this.rdBtnFloorHeight.TabIndex = 32;
            this.rdBtnFloorHeight.Text = "FloorHeight";
            this.rdBtnFloorHeight.UseVisualStyleBackColor = true;
            this.rdBtnFloorHeight.CheckedChanged += new System.EventHandler(this.rdBtnFloorHeight_CheckedChanged);
            // 
            // lblPropertyValue
            // 
            this.lblPropertyValue.AutoSize = true;
            this.lblPropertyValue.Location = new System.Drawing.Point(24, 614);
            this.lblPropertyValue.Name = "lblPropertyValue";
            this.lblPropertyValue.Size = new System.Drawing.Size(88, 16);
            this.lblPropertyValue.TabIndex = 33;
            this.lblPropertyValue.Text = "PropertyValue";
            // 
            // lblPropertyNameOR
            // 
            this.lblPropertyNameOR.AutoSize = true;
            this.lblPropertyNameOR.Location = new System.Drawing.Point(312, 584);
            this.lblPropertyNameOR.Name = "lblPropertyNameOR";
            this.lblPropertyNameOR.Size = new System.Drawing.Size(26, 16);
            this.lblPropertyNameOR.TabIndex = 37;
            this.lblPropertyNameOR.Text = "OR";
            // 
            // lblPropertyValueOR
            // 
            this.lblPropertyValueOR.AutoSize = true;
            this.lblPropertyValueOR.Location = new System.Drawing.Point(312, 617);
            this.lblPropertyValueOR.Name = "lblPropertyValueOR";
            this.lblPropertyValueOR.Size = new System.Drawing.Size(26, 16);
            this.lblPropertyValueOR.TabIndex = 38;
            this.lblPropertyValueOR.Text = "OR";
            // 
            // txtBoxPropertyName
            // 
            this.txtBoxPropertyName.Location = new System.Drawing.Point(366, 579);
            this.txtBoxPropertyName.Name = "txtBoxPropertyName";
            this.txtBoxPropertyName.Size = new System.Drawing.Size(183, 22);
            this.txtBoxPropertyName.TabIndex = 39;
            this.txtBoxPropertyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxPropertyNameKeyPress);
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.Location = new System.Drawing.Point(366, 614);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(183, 22);
            this.txtPropertyValue.TabIndex = 40;
            this.txtPropertyValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPropertyValueKeyPress);
            // 
            // cmbPropertyValue
            // 
            this.cmbPropertyValue.FormattingEnabled = true;
            this.cmbPropertyValue.Location = new System.Drawing.Point(131, 617);
            this.cmbPropertyValue.Name = "cmbPropertyValue";
            this.cmbPropertyValue.Size = new System.Drawing.Size(175, 24);
            this.cmbPropertyValue.TabIndex = 42;
            // 
            // cmbPropertyName
            // 
            this.cmbPropertyName.FormattingEnabled = true;
            this.cmbPropertyName.Location = new System.Drawing.Point(131, 584);
            this.cmbPropertyName.Name = "cmbPropertyName";
            this.cmbPropertyName.Size = new System.Drawing.Size(175, 24);
            this.cmbPropertyName.TabIndex = 43;
            this.cmbPropertyName.SelectedIndexChanged += new System.EventHandler(this.cmbPropertyNameSelectedIndexChanged);
            // 
            // rdBtnRevit
            // 
            this.rdBtnRevit.AutoSize = true;
            this.rdBtnRevit.Checked = true;
            this.rdBtnRevit.Location = new System.Drawing.Point(6, 21);
            this.rdBtnRevit.Name = "rdBtnRevit";
            this.rdBtnRevit.Size = new System.Drawing.Size(53, 20);
            this.rdBtnRevit.TabIndex = 44;
            this.rdBtnRevit.TabStop = true;
            this.rdBtnRevit.Text = "Revit";
            this.rdBtnRevit.UseVisualStyleBackColor = true;
            this.rdBtnRevit.CheckedChanged += new System.EventHandler(this.rdBtnRevitCheckedChanged);
            // 
            // rdBtnOther
            // 
            this.rdBtnOther.AutoSize = true;
            this.rdBtnOther.Location = new System.Drawing.Point(125, 21);
            this.rdBtnOther.Name = "rdBtnOther";
            this.rdBtnOther.Size = new System.Drawing.Size(57, 20);
            this.rdBtnOther.TabIndex = 45;
            this.rdBtnOther.Text = "Other";
            this.rdBtnOther.UseVisualStyleBackColor = true;
            this.rdBtnOther.CheckedChanged += new System.EventHandler(this.rdBtnOtherCheckedChanged);
            // 
            // grpFileOption
            // 
            this.grpFileOption.Controls.Add(this.rdBtnRevit);
            this.grpFileOption.Controls.Add(this.rdBtnOther);
            this.grpFileOption.Location = new System.Drawing.Point(6, 12);
            this.grpFileOption.Name = "grpFileOption";
            this.grpFileOption.Size = new System.Drawing.Size(200, 51);
            this.grpFileOption.TabIndex = 46;
            this.grpFileOption.TabStop = false;
            this.grpFileOption.Text = "File Option";
            // 
            // UIServicesBelowCeiling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(561, 677);
            this.Controls.Add(this.grpFileOption);
            this.Controls.Add(this.cmbPropertyName);
            this.Controls.Add(this.cmbPropertyValue);
            this.Controls.Add(this.txtPropertyValue);
            this.Controls.Add(this.txtBoxPropertyName);
            this.Controls.Add(this.lblPropertyValueOR);
            this.Controls.Add(this.lblPropertyNameOR);
            this.Controls.Add(this.lblPropertyValue);
            this.Controls.Add(this.rdBtnFloorHeight);
            this.Controls.Add(this.rdBtnBelowCeiling);
            this.Controls.Add(this.lblPropertyName);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.txtCategoryDisplayName);
            this.Controls.Add(this.rdBtnAlignedCeiling);
            this.Controls.Add(this.numFloorHeight);
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblFloorHeight);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.button_Find);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UIServicesBelowCeiling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services Below Ceiling | Navisworks Package-v3.0.0";
            this.Load += new System.EventHandler(this.UI_ServicesBelowCeiling_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpFileOption.ResumeLayout(false);
            this.grpFileOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.ListBox listBox_categories;
        public System.Windows.Forms.RadioButton rdBtnFloorHeight;
        public System.Windows.Forms.RadioButton rdBtnRevit;
        public System.Windows.Forms.RadioButton rdBtnOther;
        public System.Windows.Forms.GroupBox grpFileOption;
        public System.Windows.Forms.Label lblFloorHeight;
        public System.Windows.Forms.Label lblUnit;
        public System.Windows.Forms.ComboBox cmbUnit;
        public System.Windows.Forms.Label lblLevel;
        public System.Windows.Forms.ComboBox cmbLevel;
        public System.Windows.Forms.TextBox numFloorHeight;
        public System.Windows.Forms.RadioButton rdBtnAlignedCeiling;
        public System.Windows.Forms.TextBox txtCategoryDisplayName;
        public System.Windows.Forms.RadioButton rdBtnBelowCeiling;
        public System.Windows.Forms.TextBox txtBoxPropertyName;
        public System.Windows.Forms.TextBox txtPropertyValue;
        public System.Windows.Forms.ComboBox cmbPropertyValue;
        public System.Windows.Forms.ComboBox cmbPropertyName;
        public System.Windows.Forms.Button button_Find;
        public System.Windows.Forms.Label lblCategoryName;
        public System.Windows.Forms.Label lblPropertyName;
        public System.Windows.Forms.Label lblPropertyValue;
        public System.Windows.Forms.Label lblPropertyNameOR;
        public System.Windows.Forms.Label lblPropertyValueOR;
    }
}