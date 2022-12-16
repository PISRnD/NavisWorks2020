using Autodesk.Navisworks.Api.Data;
using Autodesk.Navisworks.Api.DocumentParts;
using PivdcNavisworksSupportModel;
using PivdcNavisworksSupportModule;
using PivdcSupportUi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PiNavisworks.PiNavisworksSupport
{
    public partial class ReleaseWindow : System.Windows.Forms.Form
    {
        string con;
        public static string EmpID = string.Empty;
        public static bool offlineTool = false;
        public static int projID = 0;
        public static Dictionary<string, string> OthersInformation = new Dictionary<string, string>();

        public ReleaseWindow(string PisID, int projectId, string conn, bool isOffline)
        {
            con = conn;
            EmpID = PisID;
            projID = projectId;
            offlineTool = isOffline;
            InitializeComponent();
        }

        private void Rel_Window_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (!OthersInformation.ContainsKey("Release Date"))
            {
                OthersInformation.Add("EmployeeID", SupportDatas.CurrentLoginInformation.EmployeeId);
                OthersInformation.Add("EmployeeName", SupportDatas.CurrentLoginInformation.EmployeeName);
                OthersInformation.Add("Release Date", SupportDatas.ToolReleaseDate);
                OthersInformation.Add("Release Version", SupportDatas.ToolReleaseVersion);
                OthersInformation.Add("Software Version", SupportDatas.RevitVersion);
            }
            if (IsLoggedIn())
            {
                button_LogIn.Text = "LoggedOut";
                button_PrjInfo.Enabled = true;
            }
            else if (!IsLoggedIn())
            {
                button_PrjInfo.Enabled = false;
                button_LogIn.Text = "LogIN";
            }
            SupportDatas.ProjectCode = DatabaseInformation.GetProjectCodeFromId(ProjectInfoUiOperation.GetProjectInfo(), SupportDatas.RevitToolConnectionString);
            if (OthersInformation.ContainsKey("ProjectCode"))
            {
                OthersInformation.Remove("ProjectCode");
            }
            if (!string.IsNullOrEmpty(SupportDatas.ProjectCode))
            {
                button_PrjInfo.Text = "Override ProjectInfo";
                OthersInformation.Add("ProjectCode", SupportDatas.ProjectCode);
            }
            else if (string.IsNullOrEmpty(SupportDatas.ProjectCode))
            {
                button_PrjInfo.Text = "Provide ProjectInfo";
                OthersInformation.Add("ProjectCode", SupportDatas.ProjectCode);
            }
            foreach (var item in OthersInformation)
            {
                try
                {
                    dataGridView1.Rows.Add(new Object[] { item.Key, item.Value });
                    this.dataGridView1.Sort(this.dataGridView1.Columns["Criteria"], ListSortDirection.Ascending);
                }
                catch (Exception ex)
                {

                }
            }
            this.Refresh();
        }

        private void button_LogIn_Click(object sender, EventArgs e)
        {
            LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
            if (!SupportDatas.CurrentLoginInformation.LoginStatus)
            {
                LoginWindow loginWindow = new LoginWindow(SupportDatas.CentralLoginConnectionString, SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                loginWindow.ShowDialog();
            }
            else
            {
                if (System.Windows.MessageBox.Show("Do you want to Logout", "Question", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
                {
                    if (SupportDatas.CurrentLoginInformation.LoginStatus)
                    {
                        if (OthersInformation.ContainsKey("EmployeeID"))
                        {
                            OthersInformation.Remove("EmployeeID");
                            OthersInformation.Add("EmployeeID", "");
                        }
                        if (OthersInformation.ContainsKey("EmployeeName"))
                        {
                            OthersInformation.Remove("EmployeeName");
                            OthersInformation.Add("EmployeeName", "");
                        }
                        if (OthersInformation.ContainsKey("Status"))
                        {
                            OthersInformation.Remove("Status");
                            OthersInformation.Add("Status", "Please click on Login Button to login");
                        }
                    }
                    LocalDatabaseInteraction.DeleteLocalDatabaseInformation(System.Security.Principal.WindowsIdentity.GetCurrent().Name, SupportDatas.LocalDBConnection);
                }
                else
                {
                    if (!OthersInformation.ContainsKey("Status"))
                    {
                        OthersInformation.Add("Status", "You are logged-in");
                    }
                    else if (OthersInformation.ContainsKey("Status"))
                    {
                        OthersInformation.Remove("Status");
                        OthersInformation.Add("Status", "You are logged-in");
                    }
                }
            }
            Rel_Window_Load(null, null);
        }

        private void button_PrjInfo_Click(object sender, EventArgs e)
        {
            try
            {
                PISProjectInformationUI frm = new PISProjectInformationUI(SupportDatas.RevitToolConnectionString);
                frm.ShowDialog();

                frm.Close();
            }
            catch (Exception ex)
            {

                string str = ex.ToString();
            }
            dataGridView1.Rows.Clear();
            OthersInformation.Remove("ProjectCode");
            if (!string.IsNullOrEmpty(SupportDatas.ProjectCode))
            {
                button_PrjInfo.Text = "Override ProjectInfo";
                OthersInformation.Add("ProjectCode", SupportDatas.ProjectCode);
            }
            else if (string.IsNullOrEmpty(SupportDatas.ProjectCode))
            {
                button_PrjInfo.Text = "Provide ProjectInfo";
                OthersInformation.Add("ProjectCode", SupportDatas.ProjectCode);
            }

            foreach (var item in OthersInformation)
            {
                try
                {
                    dataGridView1.Rows.Add(new Object[] { item.Key, item.Value });
                    this.dataGridView1.Sort(this.dataGridView1.Columns["Criteria"], ListSortDirection.Ascending);
                }
                catch (Exception ex)
                {

                }
            }

        }

        public bool IsLoggedIn()
        {
            try
            {
                LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                if (!SupportDatas.CurrentLoginInformation.LoginStatus)
                {
                    if (!OthersInformation.ContainsKey("Status"))
                    {
                        OthersInformation.Add("Status", "Please click on Login Button to login");
                    }
                    return false;
                }
                else
                {
                    EmpID = SupportDatas.CurrentLoginInformation.EmployeeId;
                    OthersInformation.Remove("EmployeeID");
                    OthersInformation.Remove("EmployeeName");
                    OthersInformation.Remove("Status");
                    if (!OthersInformation.ContainsKey("EmployeeID") && !OthersInformation.ContainsKey("EmployeeName") && !OthersInformation.ContainsKey("Status"))
                    {
                        OthersInformation.Add("EmployeeID", EmpID);
                        OthersInformation.Add("EmployeeName", SupportDatas.CurrentLoginInformation.EmployeeName);
                        OthersInformation.Add("Status", "You are logged-in");
                    }
                    return true;
                }
            }
            catch (Autodesk.Navisworks.Api.RuntimeLoaderException ex)
            {
                string str = ex.ToString();
                return false;
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            DeletedRecordsDB();
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
                    foreach (DataRow row in table.Rows)
                    {
                        MessageBox.Show("Values are there");
                    }
                }
                else
                {
                    MessageBox.Show("Values not there");
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        public void DeletedRecordsDB()
        {
            //get document database 
            DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
            // use transaction. The type for creation is Reset 
            using (NavisworksTransaction trans = database.BeginTransaction(DatabaseChangedAction.Reset))
            {
                try
                {
                    //setup SQL command  
                    NavisworksCommand cmd = trans.Connection.CreateCommand();
                    //creation of SQL syntax
                    string sql = "DELETE from pinnacleRndNavisDatabase";

                    cmd.CommandText = sql;
                    // do the job
                    cmd.ExecuteNonQuery();
                    //submit the transaction
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }

        }
    }
}
