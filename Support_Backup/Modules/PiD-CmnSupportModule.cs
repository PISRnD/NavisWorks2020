using Autodesk.Navisworks.Api;
using Autodesk.Windows;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using PivdcNavisworksSupportModel;

namespace PivdcNavisworksSupportModule
{
    public static class ToolSupport_Bkp
    {
        public static string DateTimeNameInterNationFormat()
        {
            string yr = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hr = DateTime.Now.Hour.ToString();
            string mn = DateTime.Now.Minute.ToString();
            string sec = DateTime.Now.Second.ToString();
            string fullName = string.Format("{0}{1}{2}{3}{4}{5}", yr, month, day, hr, mn, sec);
            return fullName;
        }

        public static string GetToolWorkAreaText()
        {
            string PinnacleWorkAreaFile = string.Format("{0}\\AccessType.ptlt", SupportDatas.AssemblyDirectory);
            if (!File.Exists(PinnacleWorkAreaFile))
            {
                return string.Empty;
            }
            string[] pinWorkArea = File.ReadAllLines(PinnacleWorkAreaFile);
            string pinVender = pinWorkArea[0];
            string pinLicType = pinWorkArea[1];
            pinVender = pinVender.Trim();
            pinVender = pinVender.ToUpper();
            pinLicType = pinLicType.Trim();
            if (pinVender is "PISD")
            {
                return pinLicType;
            }
            return string.Empty;
        }

        public static bool IsValidLicense(out int toolLicType)
        {
            string workAreaData = GetToolWorkAreaText();
            if ((workAreaData is "111ext") || (workAreaData is "111extOffline"))
            {

                toolLicType = 1;

            }
            else
            {
                toolLicType = 0;

            }
            if (((workAreaData is "111ext") || (workAreaData is "111extOffline"))
               && !InternetLicenseCheck.IsValidLicense(string.Format("{0}\\PinLic\\lic.txt", Directory.GetParent(SupportDatas.AssemblyDirectory.TrimEnd(Path.DirectorySeparatorChar)).FullName)))
            {
                return false;
            }
            return true;
        }

        //public static bool IsValidLicense(this Document document, string toolNameInDatabase)
        //{
        //    string workAreaData = GetToolWorkAreaText();

        //    if (workAreaData is "000int")
        //    {
        //        string fileSavePath = document.GetSavedPath();
        //        if (string.IsNullOrEmpty(fileSavePath) || string.IsNullOrWhiteSpace(fileSavePath))
        //        {
        //            ShowErrorMessage("Warning", "Save the file and try again.");
        //            return false;
        //        }
        //        SupportDatas.ToolNameInDatabase = toolNameInDatabase;
        //        if (!document.OpenLoginProjectInfoWindow(SupportDatas.ToolNameInDatabase))
        //        {
        //            ShowErrorMessage("Error message", "Unable to login to the database.\nContact rnd@pinnaclecad.com");
        //            return false;
        //        }
        //        SupportDatas.ToolIdPerDatabase = DatabaseInformation.IsValidRevitTool(SupportDatas.ToolNameInDatabase, SupportDatas.RevitToolConnectionString);
        //        return true;
        //    }
        //    else if ((workAreaData is "111ext") || (workAreaData is "111extOffline"))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public static void ShowErrorMessage(string windowTitle, string errorMessage)
        {
            TaskDialog taskDialog = new TaskDialog
            {
                MainInstruction = errorMessage,
                WindowTitle = windowTitle
            };
            taskDialog.Show();
        }

        /// <summary>
        /// Inserts the usage details to the SQl server.
        /// </summary>
        /// <param name="numberOfProcessedElement">Number of elements processed.</param>
        /// <param name="timeTaken">Time taken to execute the elements in Milliseconds.</param>
        //public static void InsertUsage(this Document document, long numberOfProcessedElement, [Optional] long timeTaken)
        //{
        //    string workAreaData = GetToolWorkAreaText();
        //    if (workAreaData == "000int")
        //    {
        //        if (timeTaken == 0)
        //        {
        //            timeTaken = Convert.ToInt64(DatabaseManager.GetDuration());
        //        }
        //        else
        //        {
        //            DatabaseManager.timein_milisec = timeTaken;
        //            timeTaken /= 1000;
        //        }
        //        DatabaseManager.InsertUsages(SupportDatas.ToolNameInDatabase, SupportDatas.ToolIdPerDatabase, Environment.MachineName,
        //            System.Security.Principal.WindowsIdentity.GetCurrent().Name, numberOfProcessedElement, timeTaken,
        //            SupportDatas.RevitVersion, document.GetSavedPath(), false);
        //    }
        //    else if ((workAreaData == "111ext") || (workAreaData == "111extOffline"))
        //    {
        //        //Show nothing
        //    }
        //    else
        //    {
        //        ShowErrorMessage("PiVDC tool says : Error message", "The plugin origin is from Pinnacle Infotech");
        //    }
        //}

        /// <summary>
        /// Getting the current Revit file name alongwith it's path. //Implemented by Mr. Umakanta
        /// </summary>
        /// <param name="document">The Current Revit file</param>
        /// <returns>The path of the current Revit file</returns>
        //public static string GetSavedPath(this Document document)
        //{
        //    //if (document.FileName.StartsWith("BIM 360://"))
        //    //{
        //    //    string fileName = document.WorksharingCentralGUID + ".rvt";
        //    //    string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //    //    string collaborationDir = Path.Combine(localAppData, "Autodesk\\Revit\\Autodesk Revit " + SupportDatas.RevitVersion, "CollaborationCache");
        //    //    string file = Directory.GetFiles(collaborationDir, fileName, SearchOption.AllDirectories)
        //    //        .FirstOrDefault(x => new FileInfo(x).Directory?.Name != "CentralCache");
        //    //    return file;
        //    //}
        //    //else
        //    //{
        //    //    return document.PathName;
        //    //}
        //    return document.FileName;
        //}

        //public static List<ToolDescriptionClass> GetToolDescription(string fileName)
        //{
        //    fileName = string.Format("{0}\\UIConfiguration\\{1}", Path.GetDirectoryName(SupportDatas.RefFilesLocation), fileName);
        //    object toolDescription = typeof(List<ToolDescriptionClass>).ReadXmlFile(fileName);
        //    List<ToolDescriptionClass> toolDescriptionList = new List<ToolDescriptionClass>();
        //    if (toolDescription is null)
        //    {
        //        return toolDescriptionList;
        //    }
        //    string addinVerify = string.Format("{0}\\VerifiedAddins.ptv", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
        //    if (File.Exists(addinVerify))
        //    {
        //        string[] targetAddins = File.ReadAllLines(addinVerify);
        //        toolDescriptionList = toolDescription as List<ToolDescriptionClass>;
        //        toolDescriptionList = toolDescriptionList.Where(toolDesc => targetAddins
        //        .Contains(string.Format("{0}:{1}:{2}:{3}", toolDesc.Package, toolDesc.PanelName, toolDesc.AddinName, toolDesc.ButtonText))).ToList();
        //    }
        //    return toolDescriptionList.OrderBy(ltd => ltd.PluginOrderNumber).ToList();
        //}

        //public static BitmapImage UserInterfaceIcon(this object objTemp)
        //{
        //    if (objTemp is null)
        //    {
        //        //Future code
        //    }
        //    if (File.Exists(SupportDatas.WindowIconPath))
        //    {
        //        return new BitmapImage(new Uri(SupportDatas.WindowIconPath));
        //    }
        //    return null;
        //}

        //public static void ShowErrorMessage(this Exception exception)
        //{
        //    TaskDialog.Show(SupportDatas.ErrorMessageTitle, exception.ToString());
        //}

        //public static void ShowErrorMessage(this string errorMessage, string errorMessageFileName)
        //{
        //    TaskDialog taskDialog = new TaskDialog(SupportDatas.ErrorMessageTitle)
        //    {
        //        MainContent = "Processing error",
        //        MainInstruction = errorMessage
        //    };
        //    taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Export error message to a text file");
        //    if (taskDialog.Show() == TaskDialogResult.CommandLink1)
        //    {
        //        string errorMsgDir = string.Format("{0}\\{1}", SupportDatas.ToolWorkReportPath, SupportDatas.ToolNameInDatabase);
        //        if (!Directory.Exists(errorMsgDir))
        //        {
        //            errorMsgDir = Directory.CreateDirectory(errorMsgDir).FullName;
        //        }
        //        string rptFile = string.Format("{0}\\{1}_{2}.txt", errorMsgDir, errorMessageFileName, DateTimeNameInterNationFormat());
        //        if (File.Exists(rptFile))
        //        {
        //            File.Delete(rptFile);
        //        }
        //        File.WriteAllText(rptFile, errorMessage);
        //        Process.Start("notepad.exe", rptFile);
        //    }
        //}

        public static ImageSource CreateImageSource(string fullyQualifiedImagePath)
        {
            return BitmapDecoder.Create(new Uri(fullyQualifiedImagePath), BitmapCreateOptions.None, BitmapCacheOption.None).Frames[0];
        }
    }

    public static class InternetLicenseCheck
    {
        public static bool IsValidLicense(string path)
        {
            WebClient webclient = new WebClient();
            string macId = getMacId();
            macId = Regex.Replace(macId, "[^A-Za-z0-9]+", "");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string[] readLicFile = File.ReadAllLines(path);
            string licCode = readLicFile[0];
            string licRaft = readLicFile[1];
            licCode = licCode.Trim();
            licRaft = licRaft.Trim();
            string endPoint = string.Format("https://license.pivdc.com/verifyLicense?ProductKey={0}&MacID={1}&PluginName={2}", licCode, macId, licRaft);
            string result = webclient.DownloadString(endPoint);
            if (result.Contains("true"))
            {
                webclient.Dispose();
                return true;
            }
            webclient.Dispose();
            return false;
        }

        private static string getMacId()
        {
            string macAddresses = "";
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += RemoveSpecialCharacters(RemoveSpace(nic.GetPhysicalAddress().ToString()));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return macAddresses;
        }

        public static string RemoveSpecialCharacters(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string RemoveSpace(string text)
        {
            return Regex.Replace(text, @"\s+", ""); //Replaces all(+) space characters (\s) with empty("")
        }
    }

    /// <summary>
    /// Offline/ Online Error logging 
    /// </summary>
    //public static class ErrorLog
    //{
    //public static void ErrorLogEntry(this Exception exception, string errorPage)
    //{
    //    string workAreaData = ToolSupport.GetToolWorkAreaText();
    //    if (workAreaData == "000int")
    //    {
    //        if (string.IsNullOrEmpty(SupportDatas.EmployeeId) || string.IsNullOrWhiteSpace(SupportDatas.EmployeeId)
    //            || string.IsNullOrEmpty(SupportDatas.ToolNameInDatabase) || string.IsNullOrWhiteSpace(SupportDatas.ToolNameInDatabase))
    //        {
    //            TextFileLog(exception.ToString());
    //        }
    //        else if (!LogError(SupportDatas.EmployeeId, SupportDatas.ToolNameInDatabase, exception.ToString(), exception.GetType().ToString(),
    //            SupportDatas.SystemHostName, errorPage))
    //        {
    //            TextFileLog(exception.ToString());
    //        }
    //    }
    //    else if ((workAreaData == "111ext") || workAreaData == "111extOffline")
    //    {
    //        TextFileLog(exception.ToString());
    //    }
    //    else
    //    {
    //        //Future code
    //    }
    //}

    //    /// <summary>
    //    /// Log to local system
    //    /// </summary>
    //    /// <param name="errorMessage"> error message.</param>
    //    private static void TextFileLog(string errorMessage)
    //    {
    //        // This should give you something like C:\Users\Public\Documents
    //        string errorLogDirectory = string.Format("{0}\\PinnacleErrorLog", Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
    //        if (!Directory.Exists(errorLogDirectory))
    //        {
    //            Directory.CreateDirectory(errorLogDirectory);
    //        }
    //        string errorLogFileFullPath = string.Format("{0}\\ErrorLog.txt", errorLogDirectory);
    //        File.AppendAllText(errorLogFileFullPath, string.Format("error={0} at {1}\n", errorMessage, DateTime.Now.ToLongDateString()));
    //    }

    //    /// <summary>
    //    /// Log error in web with userid, appliction ID & error details
    //    /// </summary>
    //    /// <param name="uid">User Id from Login details</param>
    //    /// <param name="appID">Application ID from config file</param>
    //    /// <param name="error">Exception from Catch block or manual entry</param>
    //    /// <param name="errorType">Type of error</param>
    //    /// <param name="errorLocation">For Windows/APP/Plugin: System/IP | For Web: Server/Project- Optional </param>
    //    /// <param name="errorPage">App Class/Controller- method.. </param>
    //    /// <returns>return true if the error was enter into the error log file</returns>
    //    private static bool LogError(string uid, string appID, string error, string errorType, string errorLocation, string errorPage)
    //    {
    //        string urlString = string.Format("http://10.1.2.47:2029/api/error?uid={0}&appid={1}&errmsg={2}&errtype={3}&errloc={4}&errpage={5}",
    //            uid, appID, error, errorType, errorLocation, errorPage);
    //        var client = new RestClient(urlString);
    //        client.Timeout = -1;
    //        var request = new RestRequest(Method.GET);
    //        IRestResponse response = client.Execute(request);
    //        Console.WriteLine(response.Content);
    //        if (response.Content.Contains("true"))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            TextFileLog(error);
    //            return false;
    //        }
    //    }
    //}

    public static class ListToDataTableConverterUtility
    {
        public static DataTable ConvertToDataTable(List<object> dataList, string tableName, Type type)
        {
            DataTable dataTable = new DataTable(tableName);
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name);
            }
            foreach (var dataItem in dataList)
            {
                var values = new object[propertyInfos.Length];
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    values[i] = propertyInfos[i].GetValue(dataItem, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }

    public class JtWindowHandle : System.Windows.Forms.IWin32Window
    {
        IntPtr _hwnd { get; set; }

        public JtWindowHandle(IntPtr h)
        {
            _hwnd = h;
        }

        public IntPtr Handle
        {
            get
            {
                return _hwnd;
            }
        }
    }

    //public static class GeneralExcelOperation
    //{
    //    public static string WriteGenralExcel(string fullFilePath, DataTable dataTable)
    //    {
    //        try
    //        {
    //            if (File.Exists(fullFilePath))
    //            {
    //                File.Delete(fullFilePath);
    //            }

    //            using (ExcelPackage objExcelPackage = new ExcelPackage())
    //            {
    //                ExcelWorksheet objWorkSheet = objExcelPackage.Workbook.Worksheets.Add(dataTable.TableName);
    //                objWorkSheet.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.Custom);
    //                objWorkSheet.Cells.AutoFitColumns();

    //                FileStream objFileStream = File.Create(fullFilePath);
    //                objFileStream.Close();
    //                File.WriteAllBytes(fullFilePath, objExcelPackage.GetAsByteArray());
    //            }

    //            return fullFilePath;
    //        }
    //        catch (Exception ex)
    //        {
    //            ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
    //            return null;
    //        }
    //    }
    //}

    //public static class XmlOperation
    //{
    //    public static object ReadXmlFile(this Type dataType, string xmlFullFilePath)
    //    {
    //        if (File.Exists(xmlFullFilePath))
    //        {
    //            FileStream fileStreamRead = new FileStream(xmlFullFilePath, FileMode.Open, FileAccess.Read);
    //            XmlSerializer xmlReadObject = new XmlSerializer(dataType);
    //            if (fileStreamRead.Length > 0)
    //            {
    //                object availableData = xmlReadObject.Deserialize(fileStreamRead);
    //                fileStreamRead.Close();
    //                return availableData;
    //            }
    //            fileStreamRead.Close();
    //        }
    //        return null;
    //    }

    //    public static void CreateOrReplaceXml(this object dataToCreateXML, string xmlFullFilePath)
    //    {
    //        if (File.Exists(xmlFullFilePath))
    //        {
    //            File.Delete(xmlFullFilePath);
    //        }
    //        if (!Directory.Exists(Path.GetDirectoryName(xmlFullFilePath)))
    //        {
    //            Directory.CreateDirectory(Path.GetDirectoryName(xmlFullFilePath));
    //        }
    //        FileStream fileStream = new FileStream(xmlFullFilePath, FileMode.OpenOrCreate, FileAccess.Write);
    //        XmlSerializer xmlSaveObject = new XmlSerializer(dataToCreateXML.GetType());
    //        xmlSaveObject.Serialize(fileStream, dataToCreateXML);
    //        fileStream.Close();
    //    }
    //}

    //public class WorkstnSettings
    //{
    //    private string workstationSettingFileName { get; set; }

    //    public WorkstnSettings()
    //    {
    //        workstationSettingFileName = string.Format("{0}\\WorkstationSeetings.xml", SupportDatas.RefFilesLocation);
    //    }

    //    public WorkstationSettingsData GetWorkstationSettings()
    //    {
    //        return typeof(WorkstationSettingsData).ReadXmlFile(workstationSettingFileName) as WorkstationSettingsData;
    //    }
    //}
}