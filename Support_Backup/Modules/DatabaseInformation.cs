using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModel;
using PivdcSupportUi;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace PivdcNavisworksSupportModule
{
    /// <summary>
    /// Database interactions
    /// </summary>
    public static class DatabaseInformation
    {
        public delegate void UpdateLoginInterface();
        public static event UpdateLoginInterface UpdateLoginInterfaceObject;
        /// <summary>
        /// Collecting all running project details which are available at pinnacle declared project information of BIMPMS
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>A datatable will return containing all valid running project information from BIMPMS</returns>
        public static DataTable GetAllRunningProjectDetailsCADPMS(string connectionString)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("select * from [dbo].[ProjectInfo] where Status like 'Running'", sqlConnection);
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return dataTable;
        }
        /// <summary>
        /// Getting the Revit tool usage report and insert to the related database
        /// </summary>
        /// <param name="connectionString">The data base iteration connection string</param>
        /// <param name="toolName">The Revit plugin name</param>
        /// <param name="toolId">The Revit plugin id</param>
        /// <param name="emplyeeId">The employee loggedin id</param>
        /// <param name="systemName">The system name</param>
        /// <param name="domainName">The domain name</param>
        /// <param name="elementProcessCount">Number of element process count by the plugin</param>
        /// <param name="timeTakenMillisecond">The time in millisecond taken by the current Revit tool related action</param>
        /// <param name="revitVersion">The Revit version</param>
        /// <param name="revitFilename">The Revit file full path</param>
        /// <param name="projectId">The Project ID</param>
        /// <param name="isUAT">True, while the Revit tool is under UAT</param>
        /// <returns>True, while the used data successfully inserted to the related Revit tool usage database</returns>
        public static bool RevitToolUsageRecordCollection(string connectionString, string toolName, int toolId, string emplyeeId, string systemName, string domainName, long elementProcessCount,
            long timeTakenMillisecond, string revitVersion, string revitFilename, string projectId, bool isUAT)
        {
            long empid = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(emplyeeId, @"[^\d]", ""));
            long pluginId = Convert.ToInt32(toolId);
            bool usageInserted = true;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    string sqlStoreProcedure = "Sp_InsertUsagesTracking_New";
                    SqlCommand command = new SqlCommand(sqlStoreProcedure, sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AddIn_Name", toolName));
                    command.Parameters.Add(new SqlParameter("@AddInsId", pluginId));
                    command.Parameters.Add(new SqlParameter("@DTPName", systemName));
                    command.Parameters.Add(new SqlParameter("@DomainName", domainName));
                    command.Parameters.Add(new SqlParameter("@NoOfElementProcess", elementProcessCount));
                    command.Parameters.Add(new SqlParameter("@TimeTaken", timeTakenMillisecond));
                    command.Parameters.Add(new SqlParameter("@TimeTaken_Milliseconds", timeTakenMillisecond));
                    command.Parameters.Add(new SqlParameter("@version", revitVersion));
                    command.Parameters.Add(new SqlParameter("@Filename", revitFilename));
                    command.Parameters.Add(new SqlParameter("@Project", projectId));
                    command.Parameters.Add(new SqlParameter("@EmpID", empid));
                    command.Parameters.Add(new SqlParameter("@isUATRecord", isUAT));
                    command.ExecuteNonQuery();
                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                usageInserted = false;

            }
            return usageInserted;
        }
        /// <summary>
        /// The tool UAT status
        /// </summary>
        /// <param name="connectionString">The database connection string is use to connect with Pinnacle database server</param>
        /// <param name="toolName">The tool name</param>
        /// <returns>The status of tool UAT span validation</returns>
        public static int UATToolStatus(string connectionString, string projectInfromationConnectionString, string toolName, bool isRevitTool)
        {
            //0=Offline tool
            //1=Under UAT
            //2=Over UAT

            int toolSatus = 0;
            try
            {
                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(toolName))
                {
                    return toolSatus;
                }
                int isValidToolId = 0;
                if (isRevitTool)
                {
                    isValidToolId = IsValidRevitTool(toolName, connectionString);

                }
                else if (!isRevitTool)
                {
                    isValidToolId = IsValidAutoCADTool(toolName, connectionString);
                }
                if (isValidToolId > 0)
                {
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation,
                        SupportDatas.LocalDBConnection);
                    if (!string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId))
                    {
                        string toolEntryDate = string.Empty;
                        string validDay = string.Empty;
                        if (!isRevitTool)
                        {
                            validDay = UATSpanInformationAutoCAD(connectionString, toolName, out toolEntryDate);
                        }
                        else if (isRevitTool)
                        {
                            validDay = UATSpanInformation(connectionString, toolName, out toolEntryDate);
                        }
                        if (!string.IsNullOrEmpty(toolEntryDate))
                        {
                            int UATDay = Convert.ToInt32(validDay);
                            DateTime dateTime = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
                            TimeSpan timeSpan = dateTime.Subtract(Convert.ToDateTime(toolEntryDate));
                            if (UATDay >= timeSpan.Days)
                            {
                                toolSatus = 1;
                                return toolSatus;
                            }
                            else
                            {
                                toolSatus = 2;
                                return toolSatus;
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Unable to read related information! Contact to rnd@pinnaclecad.com");
                            return toolSatus;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to loggedin! Contact to rnd@pinnaclecad.com");
                        return toolSatus;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Unable to connect with pinnacle domin! Contact to rnd@pinnaclecad.com");
                    return toolSatus;
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                return toolSatus;
            }

        }
        /// <summary>
        /// getting the employee id if this is offline tool
        /// </summary>
        /// <param name="localDBLocation">The local database location</param>
        /// <param name="localDBConnectionString">The local Database connection string</param>
        /// <returns>return employee id if the login has been done or return zero</returns>
        public static string EmployeeIdByLoggedIn(string localDBLocation, string localDBConnectionString, string centralLoginConnectionString)
        {
            if (System.IO.File.Exists(localDBLocation))
            {
                try
                {
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                    if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId))
                    {
                        LoginWindow loginWindow = new LoginWindow(centralLoginConnectionString, localDBLocation, localDBConnectionString);
                        loginWindow.ShowDialog();
                        SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                        if (!(UpdateLoginInterfaceObject is null))
                        {
                            UpdateLoginInterfaceObject();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            return SupportDatas.CurrentLoginInformation.EmployeeId;
        }
        /// <summary>
        /// Check the database if the tool name is exist and get the AutoCAD tool Id.
        /// </summary>
        /// <param name="toolName">The tool name to check with database entry</param>
        /// <param name="connectionString">The AutoCAD database connection string is use to connect with Pinnacle database server</param>
        /// <param name="offLine">Is the tool is assigned for offline use</param>
        /// <returns>Will return the id of the AutoCAD tool if not exist return zero</returns>
        public static int IsValidAutoCADTool(string toolName, string connectionString, bool offLine = false)
        {
            int count = 0;
            if (!offLine)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                try
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    SqlCommand sqlCommand = new SqlCommand("select id from _toolNames where _toolName = @toolName", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("toolName", toolName));
                    SqlDataReader sqlDataReader;
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        count = Convert.ToInt32(sqlDataReader["id"]);
                    }
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            return count;
        }
        /// <summary>
        /// Check the database if the tool name is exist and get the Revit tool Id.
        /// </summary>
        /// <param name="addinName">The tool name to check with database entry</param>
        /// <param name="connectionString">The Revit database connection string is use to connect with Pinnacle database server</param>
        /// <param name="offLine">Is the tool is assigned for offline use</param>
        /// <returns>Will return the id of the Revit tool if not exist return zero</returns>
        public static int IsValidRevitTool(string addinName, string connectionString)
        {
            int count = 0;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand("select * from [AppDatabase].[dbo].TBL_ToolsManualTimeChart where AddinsName='" + addinName + "'", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Parameters.Add(new SqlParameter("AddinsName", addinName));
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    count = Convert.ToInt32(sqlDataReader["Srno"]);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return count;
        }

        /// <summary>
        /// check if the logged in information exist in the current file
        /// </summary>
        /// <param name="localDBLocation">The local database location</param>
        /// <param name="localDBConnectionString">The local Database connection string</param>
        /// <param name="isOffline">Is the tool is offline used</param>
        /// <returns>return true if the login has been done</returns>
        public static bool IsLoginGet(string localDBLocation, string localDBConnectionString, string centralLoginConnectionString, bool isOffline = false)
        {
            try
            {
                SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId))
                {
                    LoginWindow loginWindow = new LoginWindow(centralLoginConnectionString, localDBLocation, localDBConnectionString);
                    loginWindow.ShowDialog();
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                    if (SupportDatas.CurrentLoginInformation.LoginStatus)
                    {
                        if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static bool RemoveLogginInformationFromLocalDB(string localDBLocation, string localDBConnectionString, bool isOffline = false)
        {
            if (isOffline)
            {
                return true;
            }
            else
            {
                System.Data.SQLite.SQLiteConnection sQLiteConnection = new System.Data.SQLite.SQLiteConnection(localDBConnectionString);
                if (System.IO.File.Exists(localDBLocation))
                {
                    try
                    {
                        string domainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                        sQLiteConnection.Open();
                        System.Data.SQLite.SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
                        sQLiteCommand.CommandText = "DELETE FROM Local_Login where DomainName = '" + domainName + "'";
                        int result = sQLiteCommand.ExecuteNonQuery();
                        sQLiteConnection.Close();
                        if (result > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        sQLiteConnection.Close();
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        ///Getting the logged information is already exist into the local database
        /// </summary>
        /// <param name="localDBLocation">The local Database location</param>
        /// <param name="localDBConnectionString">The connection string of local Database</param>
        /// <returns>The employeeId will return to display for the user</returns>
        public static void LogginInformationFromLocalDB(string localDBLocation, string localDBConnectionString)
        {
            if (System.IO.File.Exists(localDBLocation))
            {
                try
                {
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                }
                catch (Exception ex)
                {
                    SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// getting the employee id if this is offline tool
        /// </summary>
        /// <param name="localDBLocation">The local database location</param>
        /// <param name="localDBConnectionString">The local Database connection string</param>
        /// <param name="isOffline">Is the tool is offline used</param>
        /// <returns>return employee id if the login has been done or return zero</returns>
        public static void UpdateEmployeeIdInObj(string localDBLocation, string localDBConnectionString, string centralLoginConnectionString)
        {
            if (System.IO.File.Exists(localDBLocation))
            {
                try
                {
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                    if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId) || !SupportDatas.CurrentLoginInformation.LoginStatus)
                    {
                        LoginWindow loginWindow = new LoginWindow(centralLoginConnectionString, localDBLocation, localDBConnectionString);
                        loginWindow.ShowDialog();
                        SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(localDBLocation, localDBConnectionString);
                        if (!(UpdateLoginInterfaceObject is null))
                        {
                            UpdateLoginInterfaceObject();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// The tool UAT status
        /// </summary>
        /// <param name="connectionString">The database connection string is use to connect with Pinnacle database server</param>
        /// <param name="toolName">The tool name</param>
        /// <param name="isOffline">Is the tool is assigned for offline use</param>
        /// <returns>The status of tool UAT span validation</returns>
        public static int UATToolStatus(string connectionString, string toolName, bool isRevitTool)
        {
            //0=Offline tool
            //1=Under UAT
            //2=Over UAT

            int toolSatus = 0;
            try
            {
                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(toolName))
                {
                    return toolSatus;
                }
                int isValidToolId = 0;
                if (isRevitTool)
                {
                    isValidToolId = IsValidRevitTool(toolName, connectionString);
                }
                else
                {
                    isValidToolId = IsValidAutoCADTool(toolName, connectionString);
                }
                if (isValidToolId > 0)
                {
                    SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                    if (string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId) || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId)
                        || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName) || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeName)
                        || !SupportDatas.CurrentLoginInformation.LoginStatus)
                    {
                        new Exception("Local database does not  have the login information.").ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
                    }
                    else
                    {
                        string toolEntryDate = string.Empty;
                        string validDay = string.Empty;
                        if (!isRevitTool)
                        {
                            validDay = UATSpanInformationAutoCAD(connectionString, toolName, out toolEntryDate);
                        }
                        else
                        {
                            validDay = UATSpanInformation(connectionString, toolName, out toolEntryDate);
                        }
                        if (string.IsNullOrEmpty(toolEntryDate) || string.IsNullOrWhiteSpace(toolEntryDate))
                        {
                            new Exception("Tool entry date related issue in the databse.").ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                        else
                        {
                            int UATDay = Convert.ToInt32(validDay);
                            DateTime dateTime = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);
                            TimeSpan timeSpan = dateTime.Subtract(Convert.ToDateTime(toolEntryDate));
                            if (UATDay >= timeSpan.Days)
                            {
                                toolSatus = 1;
                            }
                            else
                            {
                                toolSatus = 2;
                            }
                        }
                    }
                }
                else
                {
                    new Exception("The tool is coming as invalid tool as toolId is 0.").ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return toolSatus;
        }

        /// <summary>
        /// Getting UAT information for the tool by name
        /// </summary>
        /// <param name="connectionString">The AutoCAD database connection string is use to connect with Pinnacle database server</param>
        /// <param name="toolName">The tool name</param>
        /// <param name="toolEntryDate">The tool entry date will be out</param>
        /// <returns>The tool UAT day in count will be return</returns>
        private static string UATSpanInformation(string connectionString, string toolName, out string toolEntryDate)
        {
            string toolEntryDay = toolName;
            string toolUATValidDay = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select AddinsName,edate,UATSpan  from [AppDatabase].[dbo].TBL_ToolsManualTimeChart where AddinsName='" + toolName + "'", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                System.Data.DataSet dataSet = new System.Data.DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    toolEntryDay = Convert.ToString(dataSet.Tables[0].Rows[0]["edate"]);
                    toolUATValidDay = Convert.ToString(dataSet.Tables[0].Rows[0]["UATSpan"]);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            sqlConnection.Close();
            toolEntryDate = toolEntryDay;
            return toolUATValidDay;
        }

        /// <summary>
        /// Getting UAT information for the tool by name
        /// </summary>
        /// <param name="connectionString">The AutoCAD database connection string is use to connect with Pinnacle database server</param>
        /// <param name="toolName">The tool name</param>
        /// <param name="toolEntryDate">The tool entry date will be out</param>
        /// <returns>The tool UAT day in count will be return</returns>
        private static string UATSpanInformationAutoCAD(string autoCADConnectionString, string toolName, out string toolEntryDate)
        {
            string toolEntryDay = toolName;
            string toolUATValidDay = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(autoCADConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select _toolName,edate,UATSpan  from [AutoCAD_Tools].[dbo].[_toolNames] where _toolName='" + toolName + "'", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                System.Data.DataSet dataSet = new System.Data.DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    toolEntryDay = Convert.ToString(dataSet.Tables[0].Rows[0]["edate"]);
                    toolUATValidDay = Convert.ToString(dataSet.Tables[0].Rows[0]["UATSpan"]);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            sqlConnection.Close();
            toolEntryDate = toolEntryDay;
            return toolUATValidDay;
        }

        /// <summary>
        /// Collecting all project details which are available at pinnacle declared project information
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>A datatable will return containing all valid project information</returns>
        public static DataTable GetAllProjectDetails(string connectionString)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("select * from [dbo].[ProjectInfo]", sqlConnection);
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return dataTable;
        }

        /// <summary>
        /// Getting project code against the project ID which
        /// </summary>
        /// <param name="ProjectId">The project ID which may getting from saved value from related documents</param>
        /// <param name="projectInfoConnectionString">The connection string by which interact with database to get project related information</param>
        /// <returns>The project code value as recorded against the project id in the database</returns>
        public static string GetProjectCodeFromId(int ProjectId, string projectInfoConnectionString)
        {
            string projectCode = "Unknown";
            DataTable dataTable = GetAllProjectDetails(projectInfoConnectionString);
            if (dataTable.Rows.Count > 0)
            {
                string searchString = string.Format("{0}={1}", "ProjectID", ProjectId);
                DataRow[] foundRow = dataTable.Select(searchString);

                // Display column 1 of the found row.
                if (foundRow != null && foundRow.Length > 0 && foundRow[0] != null)
                {
                    projectCode = foundRow[0]["ProjectCode"].ToString();
                }
            }
            return projectCode;
        }

        /// <summary>
        /// Collecting all running project details which are available at pinnacle declared project information
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>A datatable will return containing all valid running project information</returns>
        public static DataTable GetAllRunningProjectDetails(string connectionString)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand("select * from [dbo].[ProjectInfo] where Status like 'Running'", sqlConnection);
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return dataTable;
        }

        /// <summary>
        /// The AutoCAD tool usage record will be collected to the server at usage table
        /// </summary>
        /// <param name="connectionString">Connection string for AutoCAD database to build the database connection</param>
        /// <param name="toolID">The AutoCAD tool id as per database tool name entry</param>
        /// <param name="emplyeeId">The employee Id who is using this tool</param>
        /// <param name="filePath">The drawing file path</param>
        /// <param name="count">The count of element process based on hit policy in the database</param>
        /// <param name="millisecond">The tool processing time in millisecond</param>
        /// <param name="projectId">The project Id which is assigned by the user as per shown database project id values</param>
        /// <param name="isOfflineTool">Is the tool is applicable to the user who are not in the pinnacle domain</param>
        /// <returns>return true if the usage report inserted successfully into the AutoCAD database</returns>
        public static bool AutoCADToolUsageRecordCollection(string connectionString, int toolID, string emplyeeId, string filePath, int count, int millisecond, string projectId, bool isOfflineTool = false)
        {
            bool usageInserted = true;
            try
            {
                string emp = System.Text.RegularExpressions.Regex.Match(emplyeeId, @"[0-9\.]+").Value;
                string prjID = projectId.ToString();
                if (string.IsNullOrEmpty(projectId))
                {
                    prjID = "UAT_" + toolID;
                }
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                long employeeNumericId = Convert.ToInt64(emplyeeId.Replace("PIS", ""));
                //if the element process count less than one then its update the count as one
                count = count < 1 ? 1 : count;
                if (!isOfflineTool)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand("insert into _Usage_tbl(_toolId, _EmpID,_UserName, _date, _Path, _TimeTaken_milisec, _ObjectsPrepared,_Project) values(@toolId, @_EmpID, @UserName, @date, @Path,@TimeTaken,@ObjectsPrepared,@Project)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("toolId", toolID));
                    sqlCommand.Parameters.Add(new SqlParameter("_EmpID", employeeNumericId));
                    sqlCommand.Parameters.Add(new SqlParameter("UserName", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
                    sqlCommand.Parameters.Add(new SqlParameter("date", DateTime.Now.ToString("yyyy-MM-dd")));
                    sqlCommand.Parameters.Add(new SqlParameter("Path", filePath + ""));
                    sqlCommand.Parameters.Add(new SqlParameter("TimeTaken", millisecond));
                    sqlCommand.Parameters.Add(new SqlParameter("ObjectsPrepared", count));
                    sqlCommand.Parameters.Add(new SqlParameter("Project", projectId + ""));
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                usageInserted = false;
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return usageInserted;
        }

        /// <summary>
        /// The Revit tool usage record will be collected to the server at usage table
        /// </summary>
        /// <param name="connectionString">Connection string for database to build the database connection</param>
        /// <param name="toolName">The tool id as per database tool name entry</param>
        /// <param name="toolID">The Revit tool id as per database tool name entry</param>
        /// <param name="emplyeeId">The employee Id who is using this tool</param>
        /// <param name="filePath">The Revit file path</param>
        /// <param name="count">The count of element process based on hit policy in the database</param>
        /// <param name="millisecond">The tool processing time in millisecond</param>
        /// <param name="projectId">The project Id which is assigned by the user as per shown database project id values</param>
        /// <param name="isOfflineTool">Is the tool is applicable to the user who are not in the pinnacle domain</param>
        /// <returns>return true if the usage report inserted successfully into the Revit database</returns>
        public static bool RevitToolUsageRecordCollection(string connectionString, string toolName, int toolID, string emplyeeId, string filePath, int count, int millisecond, string projectId, string version, bool isOfflineTool = false)
        {
            bool usageInserted = true;
            try
            {
                string domainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                long employeeNumericId = Convert.ToInt64(emplyeeId.Replace("PIS", ""));
                //if the element process count less than one then its update the count as one
                count = count < 1 ? 1 : count;
                if (!isOfflineTool)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand("insert into UsagesTracking(AddIn_Name, AddInsID, DTPName, DomainName, NoOfElementProcess,TimeTaken,  UsaseDate, Version, FileName, Project, EmpID, TimeTaken_Milliseconds, PDHUsername) " +
                        "values(@AddIn_Name, @AddInsID, @DTPName,@DomainName,@NoOfElementProcess,@TimeTaken,@UsaseDate,@Version,@FileName,@Project,@EmpID,@TimeTaken_Milliseconds,@PDHUsername)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@AddIn_Name", toolName));
                    sqlCommand.Parameters.Add(new SqlParameter("@AddInsID", toolID));
                    sqlCommand.Parameters.Add(new SqlParameter("@DTPName", Environment.MachineName.ToString()));
                    sqlCommand.Parameters.Add(new SqlParameter("@DomainName", domainName));
                    sqlCommand.Parameters.Add(new SqlParameter("@NoOfElementProcess", count));
                    sqlCommand.Parameters.Add(new SqlParameter("@TimeTaken", (millisecond / 1000)));
                    sqlCommand.Parameters.Add(new SqlParameter("@UsaseDate", DateTime.Now.ToString("yyyy-MM-dd")));
                    sqlCommand.Parameters.Add(new SqlParameter("@Version", version));
                    sqlCommand.Parameters.Add(new SqlParameter("@FileName", filePath + ""));
                    sqlCommand.Parameters.Add(new SqlParameter("@Project", projectId + ""));
                    sqlCommand.Parameters.Add(new SqlParameter("@EmpID", employeeNumericId));
                    sqlCommand.Parameters.Add(new SqlParameter("@TimeTaken_Milliseconds", millisecond));
                    sqlCommand.Parameters.Add(new SqlParameter("@PDHUsername", employeeNumericId));

                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                usageInserted = false;
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return usageInserted;
        }

        /// <summary>
        /// Collect project information id and Code
        /// </summary>
        /// <param name="projectInfromationConnectionString">Project information connection as string</param>
        /// <param name="isOffline">is the tool for offline use</param>
        /// <param name="projectCode">Project Code</param>
        /// <returns>return project id and out the project code</returns>
        public static int GetProjectIdNCode(string projectInfromationConnectionString, out string projectCode)
        {
            int projectId;
            PISProjectInformationUI pisProjectInformationUI = new PISProjectInformationUI(projectInfromationConnectionString);
            if (pisProjectInformationUI.ShowDialog() == DialogResult.OK)
            {
                projectId = SupportDatas.ProjectId;
                projectCode = SupportDatas.ProjectCode;
            }
            else
            {
                projectId = 0;
                projectCode = "Unknown";
                SupportDatas.ProjectId = 0;
                SupportDatas.ProjectCode = "Unknown";
            }
            return projectId;
        }

        public static LoginInformation LoginInformationMatchedWithCentralDatabase(string email, LoginInformation loginInformation, string centralLoginConnectionString)
        {
            try
            {
                string sqlQuery = string.Format("SELECT EMP_CODE,EMP_NAME FROM TBL_HROne_EmployeeDetails where  email = '{0}' ", email);
                SqlConnection sqlConnection = new SqlConnection(centralLoginConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    loginInformation.EmployeeId = dataSet.Tables[0].Rows[0]["EMP_CODE"].ToString();
                    loginInformation.EmployeeName = dataSet.Tables[0].Rows[0]["EMP_NAME"].ToString();
                }
                sqlConnection.Close();
                if ((!string.IsNullOrEmpty(loginInformation.EmployeeId)) && (!string.IsNullOrEmpty(loginInformation.EmployeeName)))
                {
                    loginInformation.LoginStatus = true;
                    return loginInformation;
                }
            }
            catch (Exception ex)
            {
                loginInformation.LoginStatus = false;
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                return loginInformation;
            }
            return loginInformation;
        }

        /// <summary>
        /// getting login information by matched with central database information
        /// </summary>
        /// <param name="loginInformation"> The logging information which needs to check with central database</param>
        /// <returns>Return null if the checking is fail or its returns user login information</returns>
        public static LoginInformation LoginInformationMatchedWithCentralDatabase(LoginInformation loginInformation, string centralLoginConnectionString)
        {
            try
            {
                string encriptedPassword = Transformers.Encrypt(loginInformation.Password);
                loginInformation.Password = encriptedPassword;
                string employeeCode = string.Format("{0:00000}", Convert.ToInt32(loginInformation.EmployeeId));
                employeeCode = string.Format("PIS{0}", employeeCode);
                string sqlQuery = string.Format("SELECT EMP_CODE,EMP_NAME FROM TBL_HROne_EmployeeDetails where  EMP_CODE = '{0}' and ENCPASSWORD = '{1}'",
                    employeeCode, loginInformation.Password);
                SqlConnection sqlConnection = new SqlConnection(centralLoginConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    loginInformation.EmployeeId = dataSet.Tables[0].Rows[0]["EMP_CODE"].ToString();
                    loginInformation.EmployeeName = dataSet.Tables[0].Rows[0]["EMP_NAME"].ToString();
                }
                sqlConnection.Close();
                if ((!string.IsNullOrEmpty(loginInformation.EmployeeId)) && (!string.IsNullOrEmpty(loginInformation.EmployeeName)))
                {
                    loginInformation.LoginStatus = true;
                    return loginInformation;
                }
            }
            catch (Exception ex)
            {
                loginInformation.LoginStatus = false;
                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                return loginInformation;
            }
            return loginInformation;
        }
    }
}