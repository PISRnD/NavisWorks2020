using Autodesk.Navisworks.Api;
using System;
using System.Windows.Forms;
using Application = Autodesk.Navisworks.Api.Application;
using Utility.Module;
using Utility.Model;

namespace CreateSelectionSet
{

    public partial class UICreateSelectionSet : Form
    {
        #region Declaration
        public UIOperationCreateSelectionSet UIOperationCreateSelectionSetObj { get; set; } 
        #endregion

        /// <summary>
        /// This is the constructor class of UICreateSelectionSet
        /// </summary>
        public UICreateSelectionSet()
        {
            InitializeComponent();
            ElementDetails.Document = Application.ActiveDocument;
            ElementDetails.DateTimeStart = new DateTime();
            ElementDetails. DateTimeEnd = new DateTime();
            ElementDetails.TimeInSeconds = 0;
            cmbPropertyName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTabName.DropDownStyle = ComboBoxStyle.DropDownList;
            UIOperationCreateSelectionSetObj = new UIOperationCreateSelectionSet(this);
            UIOperationCreateSelectionSetObj.ExtractionOfTabName(cmbTabName, txtCategoryName);
        }

        /// <summary>
        /// This event triggers on the button click of CreateSelectionSet and creates the selection set
        /// as per the Property Name and TabName
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void CreateSelectionSetButtonClick(object sender, EventArgs e)
        {
            UIOperationCreateSelectionSetObj.CreationOfSelectionSet(txtPropertyName, cmbPropertyName);
        }

        /// <summary>
        /// This method is to populate the Tab or Category Name with the input Category Name selection change
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void cmbTabNameSelectedIndexChanged(object sender, EventArgs e)
        {
            UIOperationCreateSelectionSetObj.TabNameSelectedIndexChangedButton();
        }
    
        /// <summary>
        /// This method is to populate the Category or Tab Name with the input Category Name with "Enter" key press
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e"> Enter key Press</param>
        private void txtCategoryNameKeyPress(object sender, KeyPressEventArgs e)
        {
            UIOperationCreateSelectionSetObj.CategoryNameTextOrKeyPress(e);
        }

        /// <summary>
        /// This event is to populate the Property Name with the input Category Name with "Enter" key press
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e"> Enter key Press</param>
        private void txtPropertyNameKeyPress(object sender, KeyPressEventArgs e)
        {
            UIOperationCreateSelectionSetObj.PropertyNameTextOrKeyPress();
        }
    }
}
