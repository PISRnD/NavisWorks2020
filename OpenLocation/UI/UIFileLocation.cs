using System;
using System.Windows.Forms;
using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModule;

namespace NwcOpenLocation
{
    public partial class UIFileLocation : Form
    {
        UIOperationFileLocation UIOperationFileLocationObj { get; set; }

        /// <summary>
        /// This is the constructor class of UIFileLocation
        /// </summary>
        public UIFileLocation()
        {
            InitializeComponent();
            UIOperationFileLocationObj= new UIOperationFileLocation(this);
        }

        /// <summary>
        /// This is the load method and it initializes all the variables and data
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void MainPageLoad(object sender, EventArgs e)
        {
            UIOperationFileLocationObj.Load();
        }

        /// <summary>
        /// This is the file information of the cell content click operation
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param
        private void dataGridViewFileInfoCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UIOperationFileLocationObj.DatagridCellContentClickOperation(e);
        }       
     
    }
}
