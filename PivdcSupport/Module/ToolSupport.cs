using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PivdcSupportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PivdcSupportModule
{
    public static class ToolSupport
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

        public static string ConvertToString(this object objTemp)
        {
            if (objTemp is null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(objTemp);
            }
        }

        public static int ConvertToInteger(this string strngTemp)
        {
            if (string.IsNullOrEmpty(strngTemp) || string.IsNullOrWhiteSpace(strngTemp))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(strngTemp);
            }
        }

        /// <summary>
        /// check if the tool is valid according to the external and internal used
        /// </summary>
        /// <param name="document">The current Revit document</param>
        /// <param name="toolDatabaseName">The tool name to be matched database name</param>
        /// <returns>True will return while online licensing is checked for external used, and login, project and tool ID info validated for internal</returns>
        public static bool IsValidTool(this Document document, string toolDatabaseName, bool callingFromRibbon)
        {
            //check online licensing with the shared license key
            SupportDatas.PinnacleLicenseFile = string.Format("{0}\\PinLic\\lic.txt",
                Directory.GetParent(SupportDatas.AssemblyDirectory.TrimEnd(Path.DirectorySeparatorChar)).FullName);
            SupportDatas.ToolNameInDatabase = toolDatabaseName;
            bool validTool = false;
            try
            {
                if ((SupportDatas.ToolLicenseAccessType is "111ext") && !callingFromRibbon)
                {
                    validTool = true;
                }
                else if ((SupportDatas.ToolLicenseAccessType is "000int") && !callingFromRibbon)
                {
                    //check the tool name with database tool names, logged in and project ID enter or not
                    if (document.OpenLoginProjectInfoWindow(toolDatabaseName))
                    {
                        validTool = true;
                    }
                    else
                    {
                        new Exception("Unable to validate the tool or the login information from the databse.").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                }
                else if ((SupportDatas.ToolLicenseAccessType is "000int") && callingFromRibbon)
                {
                    validTool = true;
                }
                //check if its valid of online licensing or internal use
                else if ((SupportDatas.ToolLicenseAccessType is "111ext") && callingFromRibbon)
                {
                    //check online licensing with the shared license key
                    SupportDatas.PinnacleLicenseFile = string.Format("{0}\\PinLic\\lic.txt",
                        Directory.GetParent(SupportDatas.AssemblyDirectory.TrimEnd(Path.DirectorySeparatorChar)).FullName);
                    if (OnlineLicenseCheck.IsOlineValidLicense(SupportDatas.PinnacleLicenseFile))
                    {
                        validTool = true;
                    }
                    else
                    {
                        new Exception("License is not verified externally.").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                }
                else
                {
                    new Exception("Tool license access type data is not correct.").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            if (!validTool)
            {
                TaskDialog.Show(SupportDatas.TaskDialogErrorTitle, "You are not in connection, contact your provider for assistance");
            }
            return validTool;
        }

        /// <summary>
        /// The User login and project code verification for internal use
        /// </summary>
        /// <param name="document">The current Revit document required for project id schema write</param>
        /// <param name="toolNameInDatabase">The tool name to be matched database name</param>
        /// <returns>return true only while user login and project code as well as tool database name is verified</returns>
        public static bool OpenLoginProjectInfoWindow(this Document document, string toolNameInDatabase)
        {
            SupportDatas.RvtDocument = document;
            SupportDatas.ToolIdPerDatabase = DatabaseInformation.IsValidRevitTool(toolNameInDatabase, SupportDatas.RevitToolConnectionString);
            bool isProjctIdValidate = false;
            DatabaseInformation.UpdateEmployeeIdInObj(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnection, SupportDatas.CentralLoginConnectionString);
            SupportDatas.ToolUATStatus = DatabaseInformation.UATToolStatus(SupportDatas.RevitToolConnectionString, toolNameInDatabase, true);
            if (SupportDatas.ToolUATStatus > 1)
            {
                isProjctIdValidate = ProjectIdValidation.IsProjectInformationValidate(document, SupportDatas.RevitToolConnectionString);
                if (SupportDatas.ProjectCode == "Unknown")
                {
                    int projectId = DatabaseInformation.GetProjectIdNCode(SupportDatas.RevitToolConnectionString, out string projCode);
                    if (projectId > 0)
                    {
                        projCode = DatabaseInformation.GetProjectCodeFromId(projectId, SupportDatas.RevitToolConnectionString);
                        using (Transaction transaction = new Transaction(document, "WriteProjectInformation"))
                        {
                            try
                            {
                                transaction.Start();
                                ProjectInformationReadWrite.WriteProjectInformation(document, projectId);
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                if (transaction.GetStatus() == TransactionStatus.Started) transaction.RollBack();
                                SupportDatas.ToolIdPerDatabase = Convert.ToInt32(SupportDatas.ToolUsageValidationToolId);
                                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                                isProjctIdValidate = false;
                            }
                        }
                        SupportDatas.ProjectId = ProjectInformationReadWrite.ReadProjectInformation(document);
                    }
                    SupportDatas.ProjectCode = projCode;
                }
            }
            else if (SupportDatas.ToolUATStatus == 1)
            {
                isProjctIdValidate = true; //tool is under UAT.
            }
            return isProjctIdValidate;
        }

        /// <summary>
        /// Inserts the usage details to the SQl server.
        /// </summary>
        /// <param name="document">Autodesk Revit project file opened and currently active.</param>
        /// <param name="numberOfProcessedElement">Number of element instances processed.</param>
        /// <param name="timeTaken">Time taken to execute the elements in Milliseconds.</param>
        public static void InsertUsage(this Document document, long numberOfProcessedElement, [Optional] long timeTaken)
        {
            if (SupportDatas.ToolLicenseAccessType == "111ext")
            {
                UsageDetail usageDetail = new UsageDetail();
                usageDetail.addin_id = Convert.ToString(SupportDatas.ToolIdPerDatabase);
                usageDetail.addin_name = SupportDatas.ToolNameInDatabase;
                usageDetail.addin_ver = SupportDatas.RevitVersion;
                usageDetail.dtp_name = Environment.MachineName;
                usageDetail.project = Convert.ToString(SupportDatas.ProjectId);
                usageDetail.time_taken_ms = Convert.ToString(timeTaken);
                usageDetail.time_taken = Convert.ToString((timeTaken / 1000));
                usageDetail.domain_name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                usageDetail.emp_id = SupportDatas.CurrentLoginInformation.EmployeeId;
                usageDetail.filename = document.GetSavedPath();
                usageDetail.ip = OnlineLicenseCheck.GetLocalIPAddress();
                usageDetail.mac = OnlineLicenseCheck.GetMacAddress();
                usageDetail.no_element_proccess = Convert.ToString(numberOfProcessedElement);
                string readFromConfig = File.ReadAllText(SupportDatas.PinnacleLicenseFile);
                string[] readFromFileArray = readFromConfig.Split('\n');
                usageDetail.subscription_key = readFromFileArray[0].Replace("\n", "");
                usageDetail.UsageDate = System.DateTime.Now.ToString();
                OnlineUsage usage = new OnlineUsage();
                if (!usage.InsertUsage(usageDetail, "https://usage.pivdc.com/log"))
                {
                    new Exception("Unable to Insert the tool usage detail").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            else if (SupportDatas.ToolLicenseAccessType == "000int")
            {
                UsageDetail usageDetail = new UsageDetail();
                usageDetail.addin_id = Convert.ToString(SupportDatas.ToolIdPerDatabase);
                usageDetail.addin_name = SupportDatas.ToolNameInDatabase;
                usageDetail.addin_ver = SupportDatas.RevitVersion;
                usageDetail.dtp_name = Environment.MachineName;
                usageDetail.project = Convert.ToString(SupportDatas.ProjectId);
                usageDetail.time_taken_ms = Convert.ToString(timeTaken);
                usageDetail.time_taken = Convert.ToString((timeTaken / 1000));
                usageDetail.domain_name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                usageDetail.emp_id = SupportDatas.CurrentLoginInformation.EmployeeId;
                usageDetail.filename = document.GetSavedPath();
                usageDetail.ip = OnlineLicenseCheck.GetLocalIPAddress();
                usageDetail.mac = OnlineLicenseCheck.GetMacAddress();
                usageDetail.no_element_proccess = Convert.ToString(numberOfProcessedElement);
                string readFromConfig = File.ReadAllText(SupportDatas.PinnacleLicenseFile);
                string[] readFromFileArray = readFromConfig.Split('\n');
                usageDetail.subscription_key = readFromFileArray[0].Replace("\n", "");
                usageDetail.UsageDate = System.DateTime.Now.ToString();
                OnlineUsage usage = new OnlineUsage();
                //if (!usage.InsertUsage(usageDetail, "https://usage.preprod.pivdc.com/log"))
                if (!usage.InsertUsage(usageDetail, "https://usage.pivdc.com/log"))
                {
                    new Exception("Unable to Insert the tool usage detail").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            else if ((SupportDatas.ToolLicenseAccessType == "111ext") || (SupportDatas.ToolLicenseAccessType == "111extOffline"))
            {
                //Future code
            }
            else
            {
                TaskDialog.Show("PiVDC tool says : Error message", "The plugin origin is from Pinnacle Infotech");
            }

        }


        public static void InsertUsage1(this Document document, long numberOfProcessedElement, [Optional] long timeTaken)
        {
            if (SupportDatas.InsertUsageVaiAmazon)
            {
                UsageDetail usageDetail = new UsageDetail();
                usageDetail.addin_id = Convert.ToString(SupportDatas.ToolIdPerDatabase);
                usageDetail.addin_name = SupportDatas.ToolNameInDatabase;
                usageDetail.addin_ver = SupportDatas.RevitVersion;
                usageDetail.dtp_name = Environment.MachineName;
                usageDetail.project = Convert.ToString(SupportDatas.ProjectId);
                usageDetail.time_taken_ms = Convert.ToString(timeTaken);
                usageDetail.time_taken = Convert.ToString((timeTaken / 1000));
                usageDetail.domain_name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                usageDetail.emp_id = SupportDatas.CurrentLoginInformation.EmployeeId;
                usageDetail.filename = document.GetSavedPath();
                usageDetail.ip = OnlineLicenseCheck.GetLocalIPAddress();
                usageDetail.mac = OnlineLicenseCheck.GetMacAddress();
                usageDetail.no_element_proccess = Convert.ToString(numberOfProcessedElement);
                string readFromConfig = File.ReadAllText(SupportDatas.PinnacleLicenseFile);
                string[] readFromFileArray = readFromConfig.Split('\n');
                usageDetail.subscription_key = readFromFileArray[0].Replace("\n", "");
                usageDetail.UsageDate = System.DateTime.Now.ToString();
                OnlineUsage usage = new OnlineUsage();
                if (!usage.InsertUsage1(usageDetail))
                {
                    new Exception("Unable to Insert the tool usage detail").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            else
            {
                if (SupportDatas.ToolLicenseAccessType == "000int")
                {
                    if (timeTaken == 0)
                    {
                        timeTaken = Convert.ToInt64(DatabaseManager.GetDuration());
                    }
                    else
                    {
                        DatabaseManager.timein_milisec = timeTaken;
                        timeTaken /= 1000;
                    }
                    if (!DatabaseManager.InsertUsages(SupportDatas.ToolNameInDatabase, SupportDatas.ToolIdPerDatabase, Environment.MachineName,
                         System.Security.Principal.WindowsIdentity.GetCurrent().Name, numberOfProcessedElement, timeTaken,
                         SupportDatas.RevitVersion, document.GetSavedPath(), false))
                    {
                        new Exception("Unable to Insert the tool usage detail").ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                }
                else if ((SupportDatas.ToolLicenseAccessType == "111ext") || (SupportDatas.ToolLicenseAccessType == "111extOffline"))
                {
                    //Future code
                }
                else
                {
                    TaskDialog.Show("PiVDC tool says : Error message", "The plugin origin is from Pinnacle Infotech");
                }
            }
        }

        /// <summary>
        /// Getting the current Revit file name alongwith it's path. //Implemented by Mr. Umakanta
        /// </summary>
        /// <param name="document">The Current Revit file</param>
        /// <returns>The path of the current Revit file</returns>
        public static string GetSavedPath(this Document document)
        {
            if (document.PathName.StartsWith("BIM 360://"))
            {
                string fileName = document.WorksharingCentralGUID + ".rvt";
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string collaborationDir = Path.Combine(localAppData, "Autodesk\\Revit\\Autodesk Revit " + SupportDatas.RevitVersion, "CollaborationCache");
                string file = Directory.GetFiles(collaborationDir, fileName, SearchOption.AllDirectories)
                    .FirstOrDefault(x => new FileInfo(x).Directory?.Name != "CentralCache");
                return file;
            }
            else
            {
                return document.PathName;
            }
        }

        public static List<ToolDescriptionClass> GetToolDescription(string fileName)
        {
            fileName = string.Format("{0}\\UIConfiguration\\{1}", Path.GetDirectoryName(SupportDatas.RefFilesLocation), fileName);
            object toolDescription = typeof(List<ToolDescriptionClass>).ReadXmlFile(fileName);
            List<ToolDescriptionClass> toolDescriptionList = new List<ToolDescriptionClass>();
            if (toolDescription is null)
            {
                return toolDescriptionList;
            }
            string addinVerify = string.Format("{0}\\VerifiedAddins.ptv", Path.GetDirectoryName(SupportDatas.RefFilesLocation));
            if (File.Exists(addinVerify))
            {
                string[] targetAddins = File.ReadAllLines(addinVerify);
                toolDescriptionList = toolDescription as List<ToolDescriptionClass>;
                toolDescriptionList = toolDescriptionList.Where(toolDesc => targetAddins
                .Contains(string.Format("{0}:{1}:{2}:{3}", toolDesc.Package, toolDesc.PanelName, toolDesc.AddinName, toolDesc.ButtonText))).ToList();
            }
            return toolDescriptionList.OrderBy(ltd => ltd.PluginOrderNumber).ToList();
        }

        public static BitmapImage UserInterfaceIcon(this object objTemp)
        {
            if (objTemp is null)
            {
                //Future code
            }
            if (File.Exists(SupportDatas.WindowIconPath))
            {
                return new BitmapImage(new Uri(SupportDatas.WindowIconPath));
            }
            return null;
        }

        public static System.Windows.Controls.Image UserInterfaceIcon(this object objTemp, string fullyQualifiedImagePath)
        {
            if (objTemp is null)
            {
                //Future code
            }
            if (File.Exists(fullyQualifiedImagePath))
            {
                return new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(fullyQualifiedImagePath)),
                    Stretch = Stretch.Fill
                };
            }
            return null;
        }

        public static void ShowErrorMessage(this Exception exception)
        {
            TaskDialog.Show(SupportDatas.TaskDialogErrorTitle, exception.Message);
        }

        public static void ShowErrorMessage(this string errorMessage, string errorMessageFileName)
        {
            TaskDialog taskDialog = new TaskDialog(SupportDatas.TaskDialogErrorTitle)
            {
                MainContent = "Processing error",
                MainInstruction = errorMessage
            };
            taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Export error message to a text file");
            if (taskDialog.Show() == TaskDialogResult.CommandLink1)
            {
                string errorMsgDir = string.Format("{0}\\{1}", SupportDatas.ToolWorkReportPath, SupportDatas.ToolNameInDatabase);
                if (!Directory.Exists(errorMsgDir))
                {
                    errorMsgDir = Directory.CreateDirectory(errorMsgDir).FullName;
                }
                string rptFile = string.Format("{0}\\{1}_{2}.txt", errorMsgDir, errorMessageFileName, DateTimeNameInterNationFormat());
                if (File.Exists(rptFile))
                {
                    File.Delete(rptFile);
                }
                File.WriteAllText(rptFile, errorMessage);
                Process.Start("notepad.exe", rptFile);
            }
        }

        public static ImageSource CreateImageSource(string fullyQualifiedImagePath)
        {
            return BitmapDecoder.Create(new Uri(fullyQualifiedImagePath), BitmapCreateOptions.None, BitmapCacheOption.None).Frames[0];
        }

        public static void ExportData(string sourcePath, string initialDirectory)
        {
            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory,
                Title = "Exporting..."
            };
            string destinationPath = string.Format("{0}\\{1}", initialDirectory, Path.GetFileNameWithoutExtension(sourcePath));
            saveFileDialog.FileName = destinationPath;
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                initialDirectory = saveFileDialog.FileName;
                destinationPath = string.Format("{0}{1}", initialDirectory, Path.GetExtension(sourcePath));
                File.Copy(sourcePath, destinationPath, true);
                System.Windows.MessageBox.Show(string.Format("File exported successfully. At the location,\n{0}", destinationPath));
            }
            else
            {
                System.Windows.MessageBox.Show("User interrupted the operation.");
            }
        }

        public static void ImportData(string destinationPath, string initialDirectory)
        {
            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = openFileDialog.FileName;
                string message = "You will loose the existing data.\nDo you wish to continue?";
                string title = "Confirmation window";
                if (MessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string destinationFilePath = destinationPath;
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(sourcePath, destinationFilePath, true);
                }
                else
                {
                    System.Windows.MessageBox.Show("User interrupted the operation.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("User interrupted the operation.");
            }
        }

        public static string BrowseFolder(string initialDirectory)
        {
            string selectedFolder = string.Empty;
            if (string.IsNullOrEmpty(initialDirectory) || string.IsNullOrWhiteSpace(initialDirectory))
            {
                initialDirectory = SupportDatas.ToolWorkReportPath;
            }
            if (!Directory.Exists(initialDirectory))
            {
                Directory.CreateDirectory(initialDirectory);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a folter...",
                InitialDirectory = initialDirectory,
                CheckFileExists = false,
                Multiselect = false,
                ValidateNames = false,
                CheckPathExists = true,
                ReadOnlyChecked = true,
                FileName = "x"
            };
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult is DialogResult.OK)
            {
                selectedFolder = Path.GetDirectoryName(openFileDialog.FileName);
            }
            if (!string.IsNullOrEmpty(selectedFolder) && !string.IsNullOrWhiteSpace(selectedFolder))
            {
                try
                {
                    using (FileStream fs = File.Create(string.Format("{0}\\t.txt", selectedFolder)))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                        fs.Write(info, 0, info.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    File.Delete(string.Format("{0}\\t.txt", selectedFolder));
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
                    MessageBox.Show("You do not have permission to read or write files to this location please use another location");
                    selectedFolder = string.Empty;
                }
            }
            return selectedFolder;
        }
    }
}