namespace Levelwise_Viewpoint_Creater.UI
{
    partial class FrmViewPointExporter
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
            this.ViewpointList = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtExportLoc = new System.Windows.Forms.TextBox();
            this.lblExport = new System.Windows.Forms.Label();
            this.treeViewPointList = new System.Windows.Forms.TreeView();
            this.btnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ViewpointList
            // 
            this.ViewpointList.AutoSize = true;
            this.ViewpointList.Location = new System.Drawing.Point(12, 9);
            this.ViewpointList.Name = "ViewpointList";
            this.ViewpointList.Size = new System.Drawing.Size(77, 13);
            this.ViewpointList.TabIndex = 1;
            this.ViewpointList.Text = "View point list :";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(557, 283);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 31);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtExportLoc
            // 
            this.txtExportLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExportLoc.Location = new System.Drawing.Point(12, 299);
            this.txtExportLoc.Name = "txtExportLoc";
            this.txtExportLoc.Size = new System.Drawing.Size(535, 31);
            this.txtExportLoc.TabIndex = 3;
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Location = new System.Drawing.Point(12, 283);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(87, 13);
            this.lblExport.TabIndex = 4;
            this.lblExport.Text = "Export Location :";
            // 
            // treeViewPointList
            // 
            this.treeViewPointList.CheckBoxes = true;
            this.treeViewPointList.Location = new System.Drawing.Point(15, 9);
            this.treeViewPointList.Name = "treeViewPointList";
            this.treeViewPointList.Size = new System.Drawing.Size(641, 252);
            this.treeViewPointList.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(557, 320);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(88, 31);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // FrmViewPointExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 361);
            this.Controls.Add(this.treeViewPointList);
            this.Controls.Add(this.lblExport);
            this.Controls.Add(this.txtExportLoc);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.ViewpointList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmViewPointExporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Point Exporter";
            this.Load += new System.EventHandler(this.FrmViewPointExporter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ViewpointList;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtExportLoc;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.TreeView treeViewPointList;
        private System.Windows.Forms.Button btnImport;
    }
}