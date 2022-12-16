using PivdcNavisworksSupportModel;
using PivdcSupportUi;
using System;
using System.Configuration;
using System.IO;

namespace PivdcNavisworksSupportModule
{
    public static class InitiateProcess
    {
        public static bool Initialize(string assmeblyFullName, string softwareVersion, bool isCommand, out int licType)
        {
            /*Old*
            SupportDatas.CurrentLoginInformation = new LoginInformation();
            SupportDatas.EmployeeId = configuration.AppSettings.Settings["EmployeeId"].Value;
            SupportDatas.RevitVersion = softwareVersion;
            SupportDatas.RevitToolConnectionString = configuration.AppSettings.Settings["DBConnection"].Value;
            SupportDatas.AssemblyLocation = Path.GetDirectoryName(configuration.FilePath);
            SupportDatas.RefFilesLocation = string.Format(@"{0}\Resources\ReferenceFiles", SupportDatas.AssemblyLocation);
            SupportDatas.LocalDBLocation = configuration.AppSettings.Settings["LocalDBLocation"].Value
                .Replace("#AssemblyLocation#", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
            SupportDatas.CentralLoginConnectionString = configuration.AppSettings.Settings["CentralLoginConnection"].Value;
            SupportDatas.ProjectInfoConnection = configuration.AppSettings.Settings["ProjectInfoConnection"].Value;
            SupportDatas.LocalDBConnectionString = configuration.AppSettings.Settings["LocalDBConnection"].Value;
            SupportDatas.LocalDBConnectionString = SupportDatas.LocalDBConnectionString.Replace("#LocalDBLocation#", SupportDatas.LocalDBLocation);
            SupportDatas.ToolID = Convert.ToInt32(configuration.AppSettings.Settings["ToolId"].Value);
            SupportDatas.SystemHostName = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
            SupportDatas.WindowIconPath = string.Format("{0}\\Images\\Pinnacle_icon.ico", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
            SupportDatas.LoginToolId = configuration.AppSettings.Settings["Login"].Value;
            SupportDatas.ProjectInformationToolId = configuration.AppSettings.Settings["ProjectInformation"].Value;
            SupportDatas.ToolUsageValidationToolId = configuration.AppSettings.Settings["ToolUsageValidation"].Value;
            SupportDatas.UserInterfaeTitle = configuration.AppSettings.Settings["UserInterfaeTitle"].Value;
            SupportDatas.ToolWorkReportPath = configuration.AppSettings.Settings["ToolWorkReportPath"].Value;
            SupportDatas.ToolWorkReportPath = string.Format("{0}{1}", Environment.GetEnvironmentVariable("USERPROFILE"), SupportDatas.ToolWorkReportPath);

            if (!Directory.Exists(SupportDatas.ToolWorkReportPath))
            {
                SupportDatas.ToolWorkReportPath = Directory.CreateDirectory(SupportDatas.ToolWorkReportPath).FullName;
            }
            WorkstnSettings workstnSettings = new WorkstnSettings();
            SupportDatas.WorkstationSettingsDatas = workstnSettings.GetWorkstationSettings();
            if (ToolSupport.IsValidLicense(out licType) && (licType is 0))
            {
                LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnectionString);
                return true;
            }
            else if (licType is 1)
            {
                return true;
            }
            else
            {
                ToolSupport.ShowErrorMessage("PiVDC tool Says : ", "License verification error.\nContact your provider [rnd@pinnaclecad.com] for assistance.");
                return false;
            }
            *Old*/
            licType = 5;
            SupportDatas.CurrentLoginInformation = new LoginInformation
            {
                DomainName = string.Empty,
                EmployeeId = string.Empty,
                EmployeeName = string.Empty,
                LoginLogoutIcon = string.Empty,
                LoginStatus = false,
                Password = string.Empty,
                StatusMessage = "Initiated"
            };
            string tempData = assmeblyFullName;
            SupportDatas.AssemblyDirectory = Path.GetDirectoryName(tempData);
            SupportDatas.LocalDBLocation = string.Format("{0}\\Pinnacle.db", SupportDatas.AssemblyDirectory);
            //SupportDatas.CentralConnection = "Data Source=10.1.2.47;Initial Catalog=CentralLogin;User ID=pihd;pwd=p!$$@cle2017";
            //SupportDatas.ProjectInfoConnection = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=pihd;pwd=p!$$@cle2017";
            SupportDatas.LocalDBConnection = string.Format("Data Source={0};Version=3;FailIfMissing=True;Compress=True;", SupportDatas.LocalDBLocation);

            if (!isCommand)
            {
                tempData = string.Format("{0}\\PiNavisworks.dll", SupportDatas.AssemblyDirectory);
                if (!File.Exists(tempData))
                {
                    ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The Ribbon assembly is not found.\nContact your provider for assistance.");
                    return false;
                }
            }
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(tempData);
            if (configuration is null)
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The configuration file is missing.\nContact your provider for assistance.");
                return false;
            }
            tempData = string.Format("{0}\\AccessType.ptlt", SupportDatas.AssemblyDirectory);
            if (!File.Exists(tempData))
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The License type verification file does not exists.\nContact your provider for assistance.");
                return false;
            }
            tempData = File.ReadAllText(tempData);
            if (string.IsNullOrEmpty(tempData) || string.IsNullOrWhiteSpace(tempData) || !tempData.Contains("PISD"))
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The License type verification file is corrupted.\nContact your provider for assistance.");
                return false;
            }
            if (!tempData.Contains("000int") && !tempData.Contains("111ext"))
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The License type verification file is corrupted.\nContact your provider for assistance.");
                return false;
            }
            if (tempData.Contains("000int"))
            {
                SupportDatas.ToolLicenseAccessType = "000int";
            }
            else if (tempData.Contains("111ext"))
            {
                SupportDatas.ToolLicenseAccessType = "111ext";
            }
            else
            {
                SupportDatas.ToolLicenseAccessType = "Unknown";
            }
            tempData = string.Format("{0}\\RelevanceMechanism.tsvr", SupportDatas.AssemblyDirectory);
            if (!File.Exists(tempData))
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The connection option file does not exists.\nContact your provider for assistance.");
                return false;
            }
            tempData = File.ReadAllText(tempData);
            if (string.IsNullOrEmpty(tempData) || string.IsNullOrWhiteSpace(tempData) || !tempData.Contains("PISD") || !tempData.Contains("local-road"))
            {
                ToolSupport.ShowErrorMessage(SupportDatas.TaskDialogErrorTitle, "The connection option file is corrupted.\nContact your provider for assistance.");
                return false;
            }
            tempData = tempData.ToLower();
            if (tempData.Contains("confined") && tempData.Contains("false"))
            {
                SupportDatas.CanLoginThroughAmazon = false;
            }
            else
            {
                SupportDatas.CanLoginThroughAmazon = true;
            }
            tempData = string.Format("{0}\\InsertMechanism.ptuc", SupportDatas.AssemblyDirectory);
            if (!File.Exists(tempData))
            {
                ToolSupport.ShowErrorMessageNavis(SupportDatas.TaskDialogErrorTitle, "The connection option file does not exists.\nContact your provider for assistance.");
                return false;
            }
            tempData = File.ReadAllText(tempData);
            if (string.IsNullOrEmpty(tempData) || string.IsNullOrWhiteSpace(tempData) || !tempData.Contains("PISD") || !tempData.Contains("local-road"))
            {
                ToolSupport.ShowErrorMessageNavis(SupportDatas.TaskDialogErrorTitle, "The connection option file is corrupted.\nContact your provider for assistance.");
                return false;
            }
            tempData = tempData.ToLower();
            if (tempData.Contains("confined") && tempData.Contains("false"))
            {
                SupportDatas.InsertUsageVaiAmazon = false;
                SupportDatas.InsertUsageAmazonLink = string.Empty;
            }
            else
            {
                tempData = string.Format("{0}\\InsertMechanism.ptuc", SupportDatas.AssemblyDirectory);
                SupportDatas.InsertUsageVaiAmazon = true;
                SupportDatas.InsertUsageAmazonLink = File.ReadAllLines(tempData)[1].Replace("Insert-Use-Detail", "");
            }
            SupportDatas.ToolIdPerDatabase = Convert.ToInt32(configuration.AppSettings.Settings["RibbonToolId"].Value);
            SupportDatas.RevitVersion = softwareVersion;
            //SupportDatas.AppDatabaseConnection = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=PIHD;pwd=p!$$@cle2017";
            SupportDatas.RefFilesLocation = string.Format(@"{0}\Resource\ReferenceFiles", SupportDatas.AssemblyDirectory);
            SupportDatas.ToolIdPerDatabase = Convert.ToInt32(configuration.AppSettings.Settings["ToolId"].Value);
            SupportDatas.SystemHostName = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
            SupportDatas.WindowIconPath = string.Format("{0}\\Images\\Pinnacle_icon.ico", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
            SupportDatas.LoginToolId = configuration.AppSettings.Settings["Login"].Value;
            SupportDatas.ProjectInformationToolId = configuration.AppSettings.Settings["ProjectInformation"].Value;
            SupportDatas.ToolUsageValidationToolId = configuration.AppSettings.Settings["ToolUsageValidation"].Value;
            SupportDatas.UserInterfaeTitle = configuration.AppSettings.Settings["UserInterfaeTitle"].Value;
            SupportDatas.ToolWorkReportPath = configuration.AppSettings.Settings["ToolWorkReportPath"].Value;
            SupportDatas.ToolReleaseDate = configuration.AppSettings.Settings["ReleaseDate"].Value;
            SupportDatas.ToolReleaseVersion = configuration.AppSettings.Settings["ReleaseVersion"].Value;
            SupportDatas.ToolWorkReportPath = string.Format("{0}{1}", Environment.GetEnvironmentVariable("USERPROFILE"), SupportDatas.ToolWorkReportPath);
            if (!Directory.Exists(SupportDatas.ToolWorkReportPath))
            {
                SupportDatas.ToolWorkReportPath = Directory.CreateDirectory(SupportDatas.ToolWorkReportPath).FullName;
            }
            WorkstnSettings workstnSettings = new WorkstnSettings();
            SupportDatas.WorkstationSettingsDatas = workstnSettings.GetWorkstationSettings();
            if (SupportDatas.ToolLicenseAccessType is "000int")
            {
                SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction
                    .HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                if (!SupportDatas.CurrentLoginInformation.LoginStatus || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId)
                    || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId)
                    || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName)
                    || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeName))
                {
                    LoginWindow loginWindow = new LoginWindow(SupportDatas.CentralLoginConnectionString,
                        SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                    loginWindow.ShowDialog();
                }
                SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction
                    .HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection);
                if (!SupportDatas.CurrentLoginInformation.LoginStatus || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId)
                    || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId)
                    || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName)
                    || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeName))
                {
                    ToolSupport.ShowErrorMessage("PiVDC tool Says : ", "Encountered login error.\nContact your provider [rnd@pinnaclecad.com] for assistance.");
                    return false;
                }
                return true;
            }
            else if (SupportDatas.ToolLicenseAccessType is "111ext")
            {
                return true;
            }
            else
            {
                ToolSupport.ShowErrorMessage("PiVDC tool Says : ", "License verification error.\nContact your provider [rnd@pinnaclecad.com] for assistance.");
                return false;
            }
            return true;
        }

        /* Older public static bool OpenLoginProjectInfoWindow(this Document document, string ToolDatabaseName)
        {
            SupportDatas.ToolID = DatabaseInformation.IsValidRevitTool(ToolDatabaseName, SupportDatas.RevitToolConnectionString);
            bool isProjctIdValidate = false;
            SupportDatas.EmployeeId = DatabaseInformation
                .EmployeeIdByLoggedIn(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnectionString, SupportDatas.CentralLoginConnectionString);
            SupportDatas.ToolUATStatus = DatabaseInformation
                .UATToolStatus(SupportDatas.RevitToolConnectionString, SupportDatas.ProjectInfoConnection, ToolDatabaseName, true);
            if (SupportDatas.ToolUATStatus > 1)
            {
                isProjctIdValidate = ProjectInfoValidation.IsHasProjectInfo(out _);
                SupportDatas.ProjectCode = DatabaseInformation.GetProjectCodeFromId(ProjectInfoUiOperation.GetProjectInfo(), SupportDatas.ProjectInfoConnection);

                if (SupportDatas.ProjectCode == "Unknown")
                {
                    string projCode = string.Empty;
                    int projectId = DatabaseInformation.GetProjectIdNCode(SupportDatas.ProjectInfoConnection, out projCode);
                    if (projectId > 0)
                    {
                        projCode = DatabaseInformation.GetProjectCodeFromId(projectId, SupportDatas.ProjectInfoConnection);
                        using (Transaction transaction = new Transaction(document, "WriteProjectInformation"))
                        {
                            try
                            {
                                //transaction.Start();
                                //  ProjectInfoUiOperation.
                                MyProjectSettings MyProjectSettingsObj = new MyProjectSettings() { PISEmpName = System.Security.Principal.WindowsIdentity.GetCurrent().Name, PISProjectID = projectId };
                                ProjectInfoUiOperation.CreateAndWriteDBTable(MyProjectSettingsObj);
                                // ProjectInformationReadWrite.WriteProjectInformation(document, projectId);
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                if (!transaction.IsCommitted) transaction.Dispose();
                                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                            }
                        }
                        SupportDatas.ProjectId = projectId;
                        SupportDatas.ProjectCode = projCode;
                    }
                }
            }
            else if (SupportDatas.ToolUATStatus == 1)
            {
                isProjctIdValidate = true; //tool is under UAT.
            }
            return isProjctIdValidate;
        } Older*/
    }
}