using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NwcOpenLocation
{
    public class UIOperationFileLocation
    {
        UIFileLocation UIFileLocation { get; set; }

        /// <summary>
        /// This is the constructor class of UIOperationFileLocation
        /// </summary>
        /// <param name="uIFileLocation">This is the object of UIFileLocation class</param>
        public UIOperationFileLocation(UIFileLocation uIFileLocation)
        {
            UIFileLocation=uIFileLocation;
        }

        /// <summary>
        /// This method initializes all the data variables associated with it
        /// </summary>
        public void Load()
        {
            DatabaseManager.SetTime();
            Autodesk.Navisworks.Api.Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            if (NwcLocationExtractorQuickClass.AppendFiles.Count > 0)
            {
                UIFileLocation.dataGridViewFileInfo.Refresh();
                UIFileLocation.dataGridViewFileInfo.Rows.Clear();
                for (int i = 0; i < NwcLocationExtractorQuickClass.AppendFiles.Count; i++)
                {
                    UIFileLocation.dataGridViewFileInfo.Rows.Add();
                    UIFileLocation.dataGridViewFileInfo.Rows[i].Cells["FileName"].Value = System.IO.Path.GetFileName(NwcLocationExtractorQuickClass.AppendFiles[i]);
                    UIFileLocation.dataGridViewFileInfo.Rows[i].Cells["FileLocation"].Value = System.IO.Path.GetDirectoryName(NwcLocationExtractorQuickClass.AppendFiles[i]);
                    NwcLocationExtractorQuickClass.count++;
                }
                ToolSupport.InsertUsage(document, NwcLocationExtractorQuickClass.count);
            }
        }

        /// <summary>
        /// This method opens the location of the file whose cell is clicked
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        public void DatagridCellContentClickOperation(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string value = UIFileLocation.dataGridViewFileInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (value is DBNull) 
                    { 
                        return; 
                    }
                    if (value != null && e.ColumnIndex == 1)
                    {
                        System.Diagnostics.Process.Start(value);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
