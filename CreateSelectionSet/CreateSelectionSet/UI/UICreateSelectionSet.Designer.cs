namespace CreateSelectionSet
{
    partial class UICreateSelectionSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UICreateSelectionSet));
            this.CreateSelectionSetButton = new System.Windows.Forms.Button();
            this.lblPropertyName = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTabName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblOR = new System.Windows.Forms.Label();
            this.cmbTabName = new System.Windows.Forms.ComboBox();
            this.cmbPropertyName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CreateSelectionSetButton
            // 
            this.CreateSelectionSetButton.Location = new System.Drawing.Point(180, 208);
            this.CreateSelectionSetButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateSelectionSetButton.Name = "CreateSelectionSetButton";
            this.CreateSelectionSetButton.Size = new System.Drawing.Size(146, 34);
            this.CreateSelectionSetButton.TabIndex = 1;
            this.CreateSelectionSetButton.Text = "Create Selection Sets";
            this.CreateSelectionSetButton.UseVisualStyleBackColor = true;
            this.CreateSelectionSetButton.Click += new System.EventHandler(this.CreateSelectionSetButtonClick);
            // 
            // lblPropertyName
            // 
            this.lblPropertyName.AutoSize = true;
            this.lblPropertyName.Location = new System.Drawing.Point(5, 111);
            this.lblPropertyName.Name = "lblPropertyName";
            this.lblPropertyName.Size = new System.Drawing.Size(102, 16);
            this.lblPropertyName.TabIndex = 2;
            this.lblPropertyName.Text = "Property Name :";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(326, 5);
            this.panel6.TabIndex = 15;
            // 
            // lblTabName
            // 
            this.lblTabName.AutoSize = true;
            this.lblTabName.Location = new System.Drawing.Point(5, 13);
            this.lblTabName.Name = "lblTabName";
            this.lblTabName.Size = new System.Drawing.Size(73, 16);
            this.lblTabName.TabIndex = 17;
            this.lblTabName.Text = "Tab Name :";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(1, 36);
            this.txtCategoryName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(325, 22);
            this.txtCategoryName.TabIndex = 0;
            this.txtCategoryName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCategoryNameKeyPress);
            // 
            // lblOR
            // 
            this.lblOR.AutoSize = true;
            this.lblOR.Location = new System.Drawing.Point(134, 64);
            this.lblOR.Name = "lblOR";
            this.lblOR.Size = new System.Drawing.Size(26, 16);
            this.lblOR.TabIndex = 18;
            this.lblOR.Text = "OR";
            // 
            // cmbTabName
            // 
            this.cmbTabName.FormattingEnabled = true;
            this.cmbTabName.Location = new System.Drawing.Point(3, 82);
            this.cmbTabName.Name = "cmbTabName";
            this.cmbTabName.Size = new System.Drawing.Size(323, 24);
            this.cmbTabName.TabIndex = 19;
            this.cmbTabName.SelectedIndexChanged += new System.EventHandler(this.cmbTabNameSelectedIndexChanged);
            // 
            // cmbPropertyName
            // 
            this.cmbPropertyName.FormattingEnabled = true;
            this.cmbPropertyName.Location = new System.Drawing.Point(3, 133);
            this.cmbPropertyName.Name = "cmbPropertyName";
            this.cmbPropertyName.Size = new System.Drawing.Size(323, 24);
            this.cmbPropertyName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "OR";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(2, 180);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(324, 22);
            this.txtPropertyName.TabIndex = 22;
            this.txtPropertyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPropertyNameKeyPress);
            // 
            // UICreateSelectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 247);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPropertyName);
            this.Controls.Add(this.cmbTabName);
            this.Controls.Add(this.lblOR);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.lblTabName);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.CreateSelectionSetButton);
            this.Controls.Add(this.lblPropertyName);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UICreateSelectionSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateSelectionSet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.Button CreateSelectionSetButton;
        public System.Windows.Forms.Label lblPropertyName;
        public System.Windows.Forms.Label lblTabName;
        public System.Windows.Forms.TextBox txtCategoryName;
        public System.Windows.Forms.Label lblOR;
        public System.Windows.Forms.ComboBox cmbTabName;
        public System.Windows.Forms.ComboBox cmbPropertyName;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPropertyName;
    }
}