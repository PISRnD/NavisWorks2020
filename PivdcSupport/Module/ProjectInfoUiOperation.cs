using PivdcSupportModel;
using PivdcSupportUi;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PivdcSupportModule
{
    public class ProjectInfoUiOperation
    {
        private DataTable AllDatabaseProjectData { get; set; }
        private string ProjectInfoConnection { get; set; }
        private DataView AllDatabaseProjectDataView { get; set; }
        private PISProjectInformationUI ProjectInfoUiObj { get; set; }

        public ProjectInfoUiOperation(PISProjectInformationUI pISProjectInformationUI, string projectInfoConnection)
        {
            ProjectInfoConnection = projectInfoConnection;
            ProjectInfoUiObj = pISProjectInformationUI;
        }

        public void LoadProjectInformationWindow()
        {
            try
            {
                ProjectInfoUiObj.Icon = Icon.ExtractAssociatedIcon(SupportDatas.WindowIconPath);
                ProjectInfoUiObj.Text = string.Format("PIS Project Information | {0}", SupportDatas.UserInterfaeTitle);
                if (ProjectInfoUiObj.cbAllProject.Checked)
                {
                    AllDatabaseProjectData = DatabaseInformation.GetAllProjectDetails(ProjectInfoConnection);
                }
                else
                {
                    AllDatabaseProjectData = DatabaseInformation.GetAllRunningProjectDetails(ProjectInfoConnection);
                }
                if (AllDatabaseProjectData != null)
                {
                    AllDatabaseProjectDataView = AllDatabaseProjectData.AsDataView();
                    ProjectInfoUiObj.dgvPisProjectDisplay.DataSource = AllDatabaseProjectDataView;
                    ProjectInfoUiObj.dgvPisProjectDisplay.Columns["ProjectID"].Visible = false;
                    ProjectInfoUiObj.dgvPisProjectDisplay.Columns["Username"].Visible = false;
                    ProjectInfoUiObj.dgvPisProjectDisplay.Columns["ProjectCode"].Width = 200;
                    ProjectInfoUiObj.dgvPisProjectDisplay.Columns["ProjectName"].Width = 200;
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        public void ProjectInfoSearch()
        {
            try
            {
                string searchText = string.Format("FullName like '%{0}%' or  ProjectCode like '%{1}%' or  ProjectName like '%{2}%' or Status like '%{3}%'",
                        ProjectInfoUiObj.tbSearch.Text, ProjectInfoUiObj.tbSearch.Text, ProjectInfoUiObj.tbSearch.Text, ProjectInfoUiObj.tbSearch.Text);
                AllDatabaseProjectDataView.RowFilter = searchText;
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        public void Btn_SaveClick()
        {
            try
            {
                string ProjectIdData = ProjectInfoUiObj.dgvPisProjectDisplay.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault()
                    .Cells["ProjectID"].Value.ToString();
                string ProjectCodeData = ProjectInfoUiObj.dgvPisProjectDisplay.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault()
                    .Cells["ProjectCode"].Value.ToString();
                SupportDatas.ProjectId = Convert.ToInt32(ProjectIdData);
                SupportDatas.ProjectCode = ProjectCodeData;
                ProjectInfoUiObj.Close();
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        public void ProjectInfoKeyPressEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ProjectInfoUiObj.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                ProjectInfoSearch();
            }
        }
    }
}