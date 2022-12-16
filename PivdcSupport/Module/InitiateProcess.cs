using PivdcSupportModel;
using System;
using System.Configuration;
using System.IO;

namespace PivdcSupportModule
{
    public static class InitiateProcess
    {
        public static bool Initialize(string assmeblyFullName, string softwareVersion, bool isCommand)
        {
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
            if (!isCommand)
            {
                tempData = string.Format("{0}\\PiDocRibbon.dll", SupportDatas.AssemblyDirectory);
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
            SupportDatas.ToolIdPerDatabase = Convert.ToInt32(configuration.AppSettings.Settings["RibbonToolId"].Value);
            SupportDatas.RevitVersion = softwareVersion;
            //SupportDatas.AppDatabaseConnection = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=PIHD;pwd=p!$$@cle2017";
            SupportDatas.RefFilesLocation = string.Format(@"{0}\Resource\ReferenceFiles", SupportDatas.AssemblyDirectory);
            SupportDatas.LocalDBLocation = string.Format("{0}\\Pinnacle.db", SupportDatas.AssemblyDirectory);
            //SupportDatas.CentralConnection = "Data Source=10.1.2.47;Initial Catalog=CentralLogin;User ID=pihd;pwd=p!$$@cle2017";
            //SupportDatas.ProjectInfoConnection = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=pihd;pwd=p!$$@cle2017";
            SupportDatas.LocalDBConnection = string.Format("Data Source={0};Version=3;FailIfMissing=True;Compress=True;", SupportDatas.LocalDBLocation);
            SupportDatas.ToolIdPerDatabase = Convert.ToInt32(configuration.AppSettings.Settings["ToolId"].Value);
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
            return true;
        }
    }
}