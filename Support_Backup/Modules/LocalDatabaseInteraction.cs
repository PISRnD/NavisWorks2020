using PivdcNavisworksSupportModel;
using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace PivdcNavisworksSupportModule
{
    /// <summary>
    /// Interaction with local database
    /// </summary>
    public static class LocalDatabaseInteraction
    {
        /// <summary>
        /// The information will be deleted from the local database against the domain namennnnnnnnnnnnnnn
        /// </summary>
        /// <param name="domainName">The domain name</param>
        /// <returns>True will return if the information is deleted successfully</returns>
        public static bool DeleteLocalDatabaseInformation(string domainName, string LocalDBConnectionString)
        {
            SQLiteConnection sQLiteConnection = null;
            SQLiteCommand sQLiteCommand;
            sQLiteConnection = new SQLiteConnection(LocalDBConnectionString);
            try
            {
                sQLiteConnection.Open();
                sQLiteCommand = sQLiteConnection.CreateCommand();
                string sQLiteCommandString = string.Format("DELETE FROM Local_Login where DomainName = '{0}'", domainName);
                sQLiteCommand.CommandText = sQLiteCommandString;
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
            return false;
        }

        /// <summary>
        /// Insert the information into the local database as supplied information
        /// </summary>
        /// <param name="loginInformation">The login information to insert into local database</param>
        /// <returns>True will return if the information is deleted successfully</returns>
        public static bool InsertInformationToLocalDatabase(LoginInformation loginInformation, string localDBLocation, string localDBConnectionString)
        {
            SQLiteConnection sQLiteConnection = null;
            if (File.Exists(localDBLocation))
            {
                try
                {
                    SQLiteCommand sQLiteCommand;
                    sQLiteConnection = new SQLiteConnection(localDBConnectionString);
                    try
                    {
                        sQLiteConnection.Open();
                        sQLiteCommand = sQLiteConnection.CreateCommand();
                        string sQLiteCommandString = string.Format("INSERT INTO Local_Login(DomainName, EMP_ID, Password, IsActive, IsLoggedIn, LoggedinDate, EMP_NAME) VALUES" +
                            " ('{0}','{1}','{2}','1','1','{3}','{4}')",
                            loginInformation.DomainName, loginInformation.EmployeeId, loginInformation.Password, DateTime.Now.ToString(), loginInformation.EmployeeName);
                        sQLiteCommand.CommandText = sQLiteCommandString;
                        sQLiteCommand.ExecuteNonQuery();
                        sQLiteConnection.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        sQLiteConnection.Close();
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        return false;
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
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checking if the logged information is already exist into the local database
        /// </summary>
        /// <param name="localDBLocation">The local Database location</param>
        /// <param name="localDBConnectionString">The connection string of local Database</param>
        /// <returns>The login information will return to display for the user</returns>
        public static LoginInformation HasLoggedInformation(string localDBLocation, string localDBConnectionString)
        {
            SupportDatas.CurrentLoginInformation.DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (File.Exists(localDBLocation))
            {
                try
                {
                    SQLiteConnection sQLiteConnection = new SQLiteConnection(localDBConnectionString);
                    try
                    {
                        sQLiteConnection.Open();
                        SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
                        string sqlQuery = string.Format("SELECT EMP_ID,EMP_NAME FROM Local_Login where  DomainName ='{0}'", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                        sQLiteCommand.CommandText = sqlQuery;
                        SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
                        if (sQLiteDataReader.HasRows)
                        {
                            while (sQLiteDataReader.Read()) // Read() returns true if there is still a result line to read
                            {
                                SupportDatas.CurrentLoginInformation.EmployeeId = sQLiteDataReader.GetString(0);
                                SupportDatas.CurrentLoginInformation.EmployeeName = sQLiteDataReader.GetString(1);
                            }
                        }
                        else
                        {
                            SupportDatas.CurrentLoginInformation.EmployeeId = string.Empty;
                            SupportDatas.CurrentLoginInformation.EmployeeName = string.Empty;
                            SupportDatas.CurrentLoginInformation.LoginStatus = false;
                        }
                        sQLiteConnection.Close();
                        string imageLocation = string.Format("{0}\\Images", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
                        if (!string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId)
                            && !string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId)
                            && !string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName)
                            && !string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeName))
                        {
                            SupportDatas.CurrentLoginInformation.LoginStatus = true;
                            SupportDatas.CurrentLoginInformation.LoginLogoutIcon = string.Format("{0}\\LoggedIn.png", imageLocation);
                            SupportDatas.CurrentLoginInformation.StatusMessage = "You are logged in...";
                        }
                        else
                        {
                            SupportDatas.CurrentLoginInformation.LoginLogoutIcon = string.Format("{0}\\LoggedOut.png", imageLocation);
                            SupportDatas.CurrentLoginInformation.StatusMessage = "Click here to login...";
                        }
                        return SupportDatas.CurrentLoginInformation;
                    }
                    catch (Exception ex)
                    {
                        sQLiteConnection.Close();
                        SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        return SupportDatas.CurrentLoginInformation;
                    }
                }
                catch (Exception ex)
                {
                    SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.LoginToolId);
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    return SupportDatas.CurrentLoginInformation;
                }
            }
            else
            {
                return SupportDatas.CurrentLoginInformation;
            }
        }
    }
}