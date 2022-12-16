using PivdcNavisworksSupportModule;
using System;
using System.Windows.Forms;

namespace PiNavisworks.PiNavisworksSupport
{
    public partial class PISProjectInformationUI : Form
    {
        //The application ID for this function is 142 for ProjectInformation
        //declared for ExceptionLogAPI log system
        private ProjectInfoUiOperation ProjectInfoUiOperationObj { get; set; }

        /// <summary>
        /// initialization the main window
        /// </summary>
        public PISProjectInformationUI(string projectInfoConnection)
        {
            ProjectInfoUiOperationObj = new ProjectInfoUiOperation(this, projectInfoConnection);
            InitializeComponent();
        }

        /// <summary>
        /// Display the initial information with the window
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void UIPISProjectInformaitonLoad(object sender, EventArgs e)
        {
            ProjectInfoUiOperationObj.LoadProjectInformationWindow();
        }

        /// <summary>
        /// Save the chosen data from the opened wizard to the current document
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void btnSaveClick(object sender, EventArgs e)
        {
            ProjectInfoUiOperationObj.Btn_SaveClick();
        }

        /// <summary>
        /// The searching will be done as key down at the search field
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void PISProjectInformationUIKeyDown(object sender, KeyEventArgs e)
        {
            ProjectInfoUiOperationObj.ProjectInfoKeyPressEvent(sender, e);
        }

        /// <summary>
        /// If all project check box is activated then all project information will be displayed
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void cbAllProjectCheckedChanged(object sender, EventArgs e)
        {
            ProjectInfoUiOperationObj.LoadProjectInformationWindow();
        }

        /// <summary>
        /// The text change will be applicable to search for display the all project to wizard
        /// </summary>
        /// <param name="sender">The action with object information</param>
        /// <param name="e">The key event arguments</param>
        private void txtsearchTextChanged(object sender, EventArgs e)
        {
            ProjectInfoUiOperationObj.ProjectInfoSearch();
        }
    }
}