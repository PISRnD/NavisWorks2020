namespace PiNavisworks.PiNavisworksSupport
{
    partial class PISProjectInformationUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PISProjectInformationUI));
            this.lblsearch = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbAllProject = new System.Windows.Forms.CheckBox();
            this.dgvPisProjectDisplay = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisProjectDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblsearch
            // 
            this.lblsearch.AutoSize = true;
            this.lblsearch.Location = new System.Drawing.Point(3, 6);
            this.lblsearch.Name = "lblsearch";
            this.lblsearch.Size = new System.Drawing.Size(53, 16);
            this.lblsearch.TabIndex = 16;
            this.lblsearch.Text = "Search:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbAllProject);
            this.panel1.Controls.Add(this.lblsearch);
            this.panel1.Controls.Add(this.dgvPisProjectDisplay);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.tbSearch);
            this.panel1.Location = new System.Drawing.Point(3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 360);
            this.panel1.TabIndex = 15;
            // 
            // cbAllProject
            // 
            this.cbAllProject.AutoSize = true;
            this.cbAllProject.Location = new System.Drawing.Point(3, 327);
            this.cbAllProject.Name = "cbAllProject";
            this.cbAllProject.Size = new System.Drawing.Size(94, 20);
            this.cbAllProject.TabIndex = 21;
            this.cbAllProject.Text = "All Projects";
            this.cbAllProject.UseVisualStyleBackColor = true;
            this.cbAllProject.CheckedChanged += new System.EventHandler(this.cbAllProjectCheckedChanged);
            // 
            // dgvPisProjectDisplay
            // 
            this.dgvPisProjectDisplay.AllowUserToAddRows = false;
            this.dgvPisProjectDisplay.AllowUserToResizeRows = false;
            this.dgvPisProjectDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPisProjectDisplay.Location = new System.Drawing.Point(3, 70);
            this.dgvPisProjectDisplay.MultiSelect = false;
            this.dgvPisProjectDisplay.Name = "dgvPisProjectDisplay";
            this.dgvPisProjectDisplay.ReadOnly = true;
            this.dgvPisProjectDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPisProjectDisplay.Size = new System.Drawing.Size(606, 240);
            this.dgvPisProjectDisplay.TabIndex = 20;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(534, 316);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 40);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveClick);
            // 
            // tbSearch
            // 
            this.tbSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(3, 37);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(606, 27);
            this.tbSearch.TabIndex = 17;
            this.tbSearch.TextChanged += new System.EventHandler(this.txtsearchTextChanged);
            // 
            // PISProjectInformationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 371);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PISProjectInformationUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIS Project Information | Pinnacle Dock v1.0.0";
            this.Load += new System.EventHandler(this.UIPISProjectInformaitonLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PISProjectInformationUIKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisProjectDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblsearch;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox tbSearch;
        public System.Windows.Forms.DataGridView dgvPisProjectDisplay;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.CheckBox cbAllProject;
    }
}