using Autodesk.Navisworks.Api.Data;
using Autodesk.Navisworks.Api.DocumentParts;
using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModel;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PivdcNavisworksSupportModule
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
                if (System.IO.File.Exists(SupportDatas.WindowIconPath))
                {
                    ProjectInfoUiObj.Icon = Icon.ExtractAssociatedIcon(SupportDatas.WindowIconPath);
                }
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
                foreach (var item in ReleaseWindow.OthersInformation)
                {
                    try
                    {
                        if (item.Key == "ProjectCode")
                        {
                            ProjectInfoUiObj.tbSearch.Text = item.Value;
                        }

                    }
                    catch (Exception ex)
                    {
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
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
                SupportDatas.ProjectCode = ProjectInfoUiObj.dgvPisProjectDisplay.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault()
                    .Cells["ProjectCode"].Value.ToString();
                SupportDatas.ProjectId = Convert.ToInt32(ProjectIdData);
                MyProjectSettings MyProjectSettingsObj = new MyProjectSettings() { PISEmpName = System.Security.Principal.WindowsIdentity.GetCurrent().Name, PISProjectID = Convert.ToInt64(ProjectIdData) };
                CreateAndWriteDBTable(MyProjectSettingsObj);
                ProjectInfoUiObj.Close();
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ProjectInformationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void CreateAndWriteDBTable(MyProjectSettings MyProjectSettings)
        {
            //get document database 
            DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;

            // use transaction. The type for creation is Reset 
            using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
            {
                //setup SQL command  
                NavisworksCommand cmd = trans.Connection.CreateCommand();
                //creation of SQL syntax
                string sql = "CREATE TABLE IF NOT EXISTS pinnacleRndNavisDatabase( PISEmpName TEXT, PISProjectID INTEGER)";

                cmd.CommandText = sql;
                // do the job
                cmd.ExecuteNonQuery();
                //submit the transaction
                trans.Commit();
            }

            using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Edited))
            {
                NavisworksCommand cmd = trans.Connection.CreateCommand();

                //create adaptor to retrieve data from the data source.
                NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT PISEmpName, PISProjectID FROM pinnacleRndNavisDatabase", database.Value);

                //An empty DataTable instance is passed as an argument to the Fill method. When the method returns,
                //the DataTable instance will be populated with the queried data
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    var result = table.AsEnumerable().Where(x => x.Field<int>("PISProjectID") == GetProjectInfo());
                    if (result != null)
                    {
                        try
                        {
                            string update_sql = String.Format("UPDATE pinnacleRndNavisDatabase SET PISEmpName = '{0}', PISProjectID='{1}'", MyProjectSettings.PISEmpName, MyProjectSettings.PISProjectID);
                            //string update_sql = "UPDATE pinnacleRndNavisDatabase(PISEmpName, PISProjectID)" + " VALUES(@MyPstg.PISEmpName, @MyPstg.PISProjectID);";
                            cmd.CommandText = update_sql;

                            //execute SQL
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            string str = ex.ToString();
                        }
                    }
                    else
                    {
                        NavisworksParameter paramPISID = cmd.CreateParameter();
                        paramPISID.ParameterName = "@PISEmpName";
                        paramPISID.Value = MyProjectSettings.PISEmpName;
                        cmd.Parameters.Add(paramPISID);

                        NavisworksParameter paramProjectID = cmd.CreateParameter();
                        paramProjectID.ParameterName = "@PISProjectID";
                        paramProjectID.Value = MyProjectSettings.PISProjectID;
                        cmd.Parameters.Add(paramProjectID);


                        //build the SQL text
                        string insert_sql = String.Format("INSERT INTO pinnacleRndNavisDatabase(PISEmpName, PISProjectID) VALUES(@PISEmpName,@PISProjectID)");
                        cmd.CommandText = insert_sql;

                        //execute SQL
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }

                }
                else
                {

                    NavisworksParameter paramPISID = cmd.CreateParameter();
                    paramPISID.ParameterName = "@PISEmpName";
                    paramPISID.Value = MyProjectSettings.PISEmpName;
                    cmd.Parameters.Add(paramPISID);

                    NavisworksParameter paramProjectID = cmd.CreateParameter();
                    paramProjectID.ParameterName = "@PISProjectID";
                    paramProjectID.Value = MyProjectSettings.PISProjectID;
                    cmd.Parameters.Add(paramProjectID);


                    //build the SQL text
                    string insert_sql = "INSERT INTO pinnacleRndNavisDatabase(PISEmpName, PISProjectID)" + " VALUES(@PISEmpName, @PISProjectID);";
                    cmd.CommandText = insert_sql;

                    //execute SQL
                    cmd.ExecuteNonQuery();
                    trans.Commit();

                }


            }

        }

        public static int GetProjectInfo()
        {
            int ProjctID = 0;

            if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId) || SupportDatas.CurrentLoginInformation.LoginStatus
                   || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId))
            {
                try
                {
                    //get document database
                    DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;

                    //create adaptor to retrieve data from the data source.
                    NavisworksDataAdapter dataAdapter = new NavisworksDataAdapter("SELECT PISEmpName, PISProjectID FROM pinnacleRndNavisDatabase", database.Value);

                    //An empty DataTable instance is passed as an argument to the Fill method. When the method returns,
                    //the DataTable instance will be populated with the queried data
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        //EmployeeID = LoginWindow.global_loginfo.EMP_ID;
                        foreach (DataRow row in table.Rows)
                        {
                            if (Convert.ToInt32(row["PISProjectID"]) > 0)
                            {
                                ProjctID = Convert.ToInt32(row["PISProjectID"]);
                                break;
                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }

            }

            return ProjctID;
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