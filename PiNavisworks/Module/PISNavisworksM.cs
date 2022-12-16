using System;
using System.Linq;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Windows;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using Autodesk.Navisworks.Api.DocumentParts;
using Autodesk.Navisworks.Api.Data;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
//using PiNavisworks.PiNavisworksSupport;
using System.IO;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Gui.Roamer.AIRLook;
using Utility.Model;
using PivdcNavisworksSupportModule;
using PivdcNavisworksSupportModel;
using PiNavisworks.PiNavisworksSupport;

namespace PiNavisworks
{
    [Plugin("PISNavisworksM", "PinnacleTab", DisplayName = "PISNavisworksM", Options = PluginOptions.SupportsControls, SupportsIsSelfEnabled = true)]
    [Strings("PISNavisworksM.name")]
    [RibbonLayout("PISNavisworksM.xaml")]

    [RibbonTab("ID_PISTab_1", DisplayName = "PinnacleTab")]
    [Command("ID_ClashOptimizer_1", Icon = "ClashOptimizerSmall.ico", LargeIcon = "ClashOptimizer.ico", ToolTip = "Used to reduce number of clash against a Clash Test by grouping similar clash")]
    [Command("ID_CreateRevitSchedule_2", Icon = "CreateRevitScheduleSmall.ico", LargeIcon = "CreateRevitSchedule.ico", ToolTip = "This tool is used to create schedules of revit elements in Navisworks")]
    [Command("ID_CreateSelectionSet_3", Icon = "CreateSelectionSetSmall.ico", LargeIcon = "CreateSelectionSet.ico", ToolTip = "Automatically creates Selection Sets according to the distinct values of a Revit Parameter")]
    [Command("ID_ServicesBelowCeiling_4", Icon = "ServiceBelowCeilingSmall.ico", LargeIcon = "ServiceBelowCeiling.ico", ToolTip = "This tool is used to identify the MEP services that are running below ceiling")]
    [Command("ID_TimelineMerger_5", Icon = "TimeLinerSmall.ico", LargeIcon = "TimeLiner.ico", ToolTip = "The purpose of this add-ins is to append Timeliner Data in a merged Navisworks files")]
    [Command("ID_NwcOpenLocation_6", Icon = "NwcOpenLocationSmall.ico", LargeIcon = "NwcOpenLocation.ico", ToolTip = "The purpose of this add-ins is to list all the file including the current Naviswork file name and location")]
    [Command("ID_GroupClashes_7", Icon = "ClashClusterSmall.ico", LargeIcon = "ClashCluster.ico", ToolTip = "The purpose of this add-ins is easily group the clashes based on selected service name")]
    [Command("ID_LevelwiseViewpointCreator_11", Icon = "ClashClusterSmall.ico", LargeIcon = "ClashCluster.ico", ToolTip = "The purpose of this add-ins is easily group the clashes based on selected service name")]
    [Command("ID_InformationWindow_8", Icon = "NavisPackageInformation_Small.png", LargeIcon = "NavisPackageInformation.png", ToolTip = "Provides Information about Latest Release")]
    [Command("ID_Usage_9", Icon = "NwcUsage_Small.png", LargeIcon = "NwcUsage.png", ToolTip = "Provides tool usage information")]
    [Command("ID_Help_10", Icon = "NwcHelp_Small.png", LargeIcon = "NwcHelp.png", ToolTip = "Provides the Help manual for the tools")]

    public class PISNavisworksM : CommandHandlerPlugin
    {
        public static Autodesk.Navisworks.Api.Document document { get; set; }
        private string ButtonIconPath { get; set; }
        private string ImageGeneralLocation { get; set; }
        public bool IsLicenseApplied { get; set; }
        bool m_toEnableButton { get; set; }
        public override int ExecuteCommand(string commandId, params string[] parameters)
        {
            
            document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            ElementDetails.Document = document;
            PluginRecord Plgrecord = null;
            IsLicenseApplied = false;

            string assmeblyFullName = Assembly.GetExecutingAssembly().Location;
            string tabName = "PiNavisworks";
       //     SupportDatas.AppConfiguration = ConfigurationManager.OpenExeConfiguration(assmeblyFullName);
            SupportDatas.ToolIdPerDatabase = 144;
            SupportDatas.AssemblyDirectory = Path.GetDirectoryName(assmeblyFullName);
            SupportDatas.ErrorMessageTitle = "PiVDC tool error message";
            //NWRibbonButton
            RibbonTab ribbontab = ComponentManager.Ribbon.Tabs.Cast<RibbonTab>().FirstOrDefault(rbntb => rbntb.Title == tabName);
            SupportDatas.RevitVersion = "2020";
            //if (ribbontab is null)
            //{
            //    ribbontab.Title = "PiNavisworks";
            //}
            if (!InitiateProcess.Initialize(assmeblyFullName, SupportDatas.RevitVersion,false, out int toolLicType))
            {
                return 0;
            }
            //if (!(ribbontab is null) && !(toolLicType is 0))
            //{

            //    RibbonItem ribbonItem = ribbontab.Panels.Cast<RibbonPanel>().Select(pnl => pnl.FindItem("ID_InformationWindow_8")).FirstOrDefault();
            //    if (!(ribbonItem is null))
            //    {
            //        ribbonItem.IsVisible = false;
            //    }
            //    ribbonItem = ribbontab.Panels.Cast<RibbonPanel>().Select(pnl => pnl.FindItem("ID_Usage_9")).FirstOrDefault();
            //    if (!(ribbonItem is null))
            //    {
            //        ribbonItem.IsVisible = false;
            //    }
            //    ribbonItem = ribbontab.Panels.Cast<RibbonPanel>().Select(pnl => pnl.FindItem("ID_Help_10")).FirstOrDefault();
            //    if (!(ribbonItem is null))
            //    {
            //        ribbonItem.IsVisible = false;
            //    }
            //}
            if (document.Models.Count() == 0)
            {
                MessageBox.Show("Please load any relevant file before proceed. ");
            }
            else
            {
                if (commandId != "ID_InformationWindow_8" && commandId != "ID_Usage_9" && commandId != "ID_Help_10")
                {
                    switch (commandId)
                    {
                        case "ID_ClashOptimizer_1":
                            {

                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {
                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ClashOptimizer.PIS_ClashOptimizer_1");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\ClashOptimiser.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ClashOptimizer.PIS_ClashOptimizer_1");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ClashOptimizer.PIS_ClashOptimizer_1");
                                    }
                                    Plugin plgn = Plgrecord.LoadPlugin();
                                    if (plgn != null)
                                    {
                                        string[] pa = { "ClashOptimizer_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin ClashOptimizer but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }

                                break;
                            }
                        case "ID_CreateRevitSchedule_2":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {
                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateRevitSchedule.PIS_CreateRevitSchedule_2");

                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\CreateRevitSchedule.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateRevitSchedule.PIS_CreateRevitSchedule_2");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateRevitSchedule.PIS_CreateRevitSchedule_2");
                                    }
                                    Plugin plgn = Plgrecord.LoadPlugin();
                                    if (plgn != null)
                                    {
                                        string[] pa = { "CreateRevitSchedule_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin CreateSelectionSet but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_CreateSelectionSet_3":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {

                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateSelectionSet.PIS_CreateSelectionSet_3");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\CreateSelectionSet.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateSelectionSet.PIS_CreateSelectionSet_3");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("CreateSelectionSet.PIS_CreateSelectionSet_3");
                                    }
                                    Plugin plgn = Plgrecord.LoadPlugin();
                                    if (plgn != null)
                                    {
                                        string[] pa = { "CreateSelectionSet_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin CreateSelectionSet but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_ServicesBelowCeiling_4":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {

                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ServicesBelowCeiling.PIS_ServicesBelowCeiling_4");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\ServicesBelowCeiling.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ServicesBelowCeiling.PIS_ServicesBelowCeiling_4");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ServicesBelowCeiling.PIS_ServicesBelowCeiling_4");
                                    }
                                    Plugin plgn = null;
                                    try
                                    {
                                        plgn = Plgrecord.LoadPlugin();
                                    }
                                    catch (Exception ex)
                                    {

                                        string str = ex.ToString();
                                    }
                                    if (plgn != null)
                                    {
                                        string[] pa = { "ServicesBelowCeiling_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin ServicesBelowCeiling but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_TimelineMerger_5":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {

                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("TimelineMerger.TimelineMerger_5");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\TimelineMerger.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("TimelineMerger.TimelineMerger_5");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("TimelineMerger.TimelineMerger_5");
                                    }
                                    Plugin plgn = Plgrecord.LoadPlugin();
                                    if (plgn != null)
                                    {
                                        string[] pa = { "TimelineMerger" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin ServicesBelowCeiling but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_NwcOpenLocation_6":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {
                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("NwcOpenLocation.PIS_NwcOpenLocation_6");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\NwcLocationExtractor.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("NwcOpenLocation.PIS_NwcOpenLocation_6");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("NwcOpenLocation.PIS_NwcOpenLocation_6");
                                    }
                                    Plugin plgn = null;
                                    try
                                    {
                                        plgn = Plgrecord.LoadPlugin();
                                    }
                                    catch (Exception ex)
                                    {

                                        string str = ex.ToString();
                                    }

                                    if (plgn != null)
                                    {
                                        string[] pa = { "OpenLocation_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems

                                        System.Windows.MessageBox.Show("The app has tried to load the plugin NwcOpenLocation but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_GroupClashes_7":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {
                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ClashClusters.ClashClustersPane.PIS_ClashClusters_7");
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name will not work while the ribbon action needs to be perform
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\ClashClusters.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("ClashClusters.ClashClustersPane.PIS_ClashClusters_7");
                                    }
                                    if (Plgrecord != null && Plgrecord is DockPanePluginRecord && Plgrecord.IsEnabled)
                                    {
                                        //check if it needs loading
                                        if (Plgrecord.LoadedPlugin == null)
                                        {
                                            Plgrecord.LoadPlugin();
                                        }

                                        DockPanePlugin dpp = Plgrecord.LoadedPlugin as DockPanePlugin;
                                        if (dpp != null)
                                        {
                                            //switch the Visible flag
                                            dpp.Visible = !dpp.Visible;
                                        }
                                    }
                                    else
                                    {
                                        // any other problems
                                        System.Windows.MessageBox.Show("The app has tried to load the plugin ClashCluster but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                        case "ID_LevelwiseViewpointCreator_11":
                            {
                                bool projectInfoAvailable = CheckProjectInfo();
                                if (projectInfoAvailable)
                                {
                                    Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("LevelwiseViewpointCreator.PIS_LevelwiseViewpointCreator_11"); ;
                                    if (Plgrecord == null)
                                    {
                                        // load the plugin binary from full file path name
                                        Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(SupportDatas.AssemblyDirectory + "\\LevelwiseViewpointCreater.dll");
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("LevelwiseViewpointCreator.PIS_LevelwiseViewpointCreator_11");
                                    }
                                    else
                                    {
                                        Plgrecord = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("LevelwiseViewpointCreator.PIS_LevelwiseViewpointCreator_11");
                                    }
                                    Plugin plgn = null;
                                    try
                                    {
                                        plgn = Plgrecord.LoadPlugin();
                                    }
                                    catch (Exception ex)
                                    {

                                        string str = ex.ToString();
                                    }

                                    if (plgn != null)
                                    {
                                        string[] pa = { "LevelwiseViewpointCreator_PIS" }; plgn.GetType().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, null, plgn, pa);
                                    }
                                    else
                                    {
                                        // any other problems

                                        System.Windows.MessageBox.Show("The app has tried to load the plugin NwcOpenLocation but it is still null. Please place call RnD Pinnacle !");
                                    }
                                }
                                break;
                            }
                    }
                }


                else if ((commandId == "ID_InformationWindow_8") && (SupportDatas.ToolLicenseAccessType is "000int"))
                {
                    try
                    {
                        ReleaseWindow obj = new ReleaseWindow(SupportDatas.CurrentLoginInformation.EmployeeId,
                            GetProjectInfo(), SupportDatas.RevitToolConnectionString, IsLicenseApplied);
                        obj.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                }
                else if ((commandId == "ID_Usage_9") && (SupportDatas.ToolLicenseAccessType is "000int"))
                {
                    PinnacleToolUsageVisualizer_Navis();
                }
                else if ((commandId == "ID_Help_10") && (SupportDatas.ToolLicenseAccessType is "000int"))
                {
                    PinnacleToolsHelpSystemShow();
                }
                else
                {
                    System.Windows.MessageBox.Show("This is not a valid command.");
                }
            }
            return 0;
        }



        //public override CommandState CanExecuteCommand(String commandId)

        //{

        //    CommandState state = new CommandState();

        //    switch (commandId)

        //    {

        //        case "ID_PISTab_1":

        //            {

        //                // button3 is disabled/enabled by a flag

        //                state.IsEnabled = m_toEnableButton;

        //                break;

        //            }

        //        default:

        //            {

        //                // other commands are all visible and enabled

        //                state.IsVisible = true;

        //                state.IsEnabled = true;

        //                state.IsChecked = false;

        //                break;

        //            }

        //    }

        //    return state;

        //}

        //public override bool CanExecuteRibbonTab(String ribbonTabId)

        //{

        //    // The second ribbon tab is visible or not

        //    // by Button 2 toggles on/off

        //    if (ribbonTabId.Equals("ID_CustomTab_2"))

        //    {

        //      //  return m_toShowTab;

        //    }

        //    return true;

        //}

        //public static void UpdateLoginInformation()
        //{
        //    PiNavisworks.PiNavisworksSupport.SupportDatas.CurrentLoginInformation = LocalDatabaseInteraction.HasLoggedInformation(SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnectionString);
        //    PushButton LoginLogoutButton = SupportDatas.RvtPushButtons.FirstOrDefault(rpb => rpb.Name is "LoginStatus");
        //    if (!(LoginLogoutButton is null))
        //    {
        //        if (!string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.LoginLogoutIcon)
        //                && !string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.LoginLogoutIcon)
        //                && File.Exists(SupportDatas.CurrentLoginInformation.LoginLogoutIcon))
        //        {
        //            LoginLogoutButton.LargeImage = PiNavisworks.PiNavisworksSupport.ToolSupport.CreateImageSource(SupportDatas.CurrentLoginInformation.LoginLogoutIcon);
        //        }
        //        LoginLogoutButton.ToolTip = string.Format("Hi {0}!\n{1}", PiNavisworks.PiNavisworksSupport.SupportDatas.CurrentLoginInformation.EmployeeName,
        //            PiNavisworks.PiNavisworksSupport.SupportDatas.CurrentLoginInformation.StatusMessage);
        //        if (SupportDatas.CurrentLoginInformation.LoginStatus)
        //        {
        //            LoginLogoutButton.ItemText = "Logged In";
        //        }
        //        else
        //        {
        //            LoginLogoutButton.ItemText = "Logged Out";
        //        }
        //    }
        //    DatabaseInformation.UpdateLoginInterfaceObject += DatabaseInformation_UpdateLoginInterfaceObject;
        //}
        public void PinnacleToolsHelpSystemShow()
        {

            SupportDatas.ToolIdPerDatabase = DatabaseInformation.IsValidRevitTool("PIS_NavisWorkHelp", SupportDatas.RevitToolConnectionString);
            //if (isValidTool("PIS_NavisWorkHelp", true))
            //{
            SupportDatas.EmployeeId = SupportDatas.CurrentLoginInformation.EmployeeId;


            if (!string.IsNullOrEmpty(SupportDatas.EmployeeId) && !string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName))
            {
                try
                {
                    int sec = 0;
                    DatabaseManager.SetTime();
                    Autodesk.Navisworks.Api.Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
                    Process.Start(@"C:\PinnacleToolsHelpSystem\PinnacleToolsHelp.exe", "NavisWorks " + "clash");
                    ToolSupport.InsertUsage(oDoc, 1);
                }
                catch (System.Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show("Offline Help Application is missing. Please contact RND");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please Login.");
            }
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Make a call for R&D Section");

            //}
        }

        public void PinnacleToolUsageVisualizer_Navis()
        {
            SupportDatas.ToolIdPerDatabase = DatabaseInformation.IsValidRevitTool("PIS_TOOLUSAGENAVIS", SupportDatas.RevitToolConnectionString);
            // SupportDatas.ToolID = ToolValidation.IsValidNavisTool(ToolValidation.ToolConnectionString, "PIS_TOOLUSAGENAVIS");
            //if (isValidTool("PIS_TOOLUSAGENAVIS", false))
            //if (isValidTool("PIS_TOOLUSAGENAVIS", true))
            //{
            Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            DocumentDatabase database = Autodesk.Navisworks.Api.Application.ActiveDocument.Database;
            SupportDatas.EmployeeId = SupportDatas.CurrentLoginInformation.EmployeeId;


            if (!string.IsNullOrEmpty(SupportDatas.EmployeeId) && !string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeName))
            {
                try
                {
                    int sec = 0;
                    DateTime dtS = DateTime.Now;
                    string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                    string emp_id = SupportDatas.EmployeeId;
                    string emp_Nm = SupportDatas.CurrentLoginInformation.EmployeeName;
                    if (!string.IsNullOrEmpty(SupportDatas.EmployeeId))
                    {
                        emp_id = Convert.ToInt64(emp_id.Replace("PIS", "")).ToString();
                        //DateTime dtE = DateTime.Now;
                        //sec += (dtE - dtS).Milliseconds;
                        ToolSupport.InsertUsage(document, 1);
                        Process.Start(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Resources\Usage\ToolUsageVisualizer.exe", emp_id + " " + emp_Nm.Replace(" ", "#") + " " + date);

                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("error:" + ex.ToString());
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please Login.");
            }

        }


        public int NavisworksToolID(string connectionStringRevit, string toolDatabaseName)
        {

            return PISNavisworksM.IsValidNavisTool(toolDatabaseName, SupportDatas.RevitToolConnectionString);
        }

        /// <summary>
        /// Check the database if the tool name is exist and get the Revit tool Id.
        /// </summary>
        /// <param name="addinName">The tool name to check with database entry</param>
        /// <param name="connectionString">The Revit database connection string is use to connect with Pinnacle database server</param>
        /// <param name="offLine">Is the tool is assigned for offline use</param>
        /// <returns>Will return the id of the Revit tool if not exist return zero</returns>
        public static int IsValidNavisTool(string connectionString, string addinName, bool offLine = false)
        {
            int count = 2020;
            if (!offLine)
            {
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
                    if (count > 0)
                        return count;
                    else
                        return count;
                }
                catch (Exception ex)
                {

                    return count;
                }
            }
            else
            {
                return count;
            }
        }
        public static bool isValidTool(string toolName, bool offline = false)
        {
            if (!offline)
            {
                return false;
            }
            else
            {

                //  SqlConnection conn = new SqlConnection(@"Data Source=10.1.2.47;Initial Catalog=AutoCAD_Tools;Persist Security Info=True;User ID=umakanta;Password=uma@0364");
                SqlConnection conn = new SqlConnection(@"Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=PIHD;pwd=p!$$@cle2017");
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    //SqlCommand cmd = new SqlCommand("select count(id) as _count from _toolNames where _toolName = @toolName", conn);
                    SqlCommand cmd = new SqlCommand("select count(Srno) as _count from [dbo].[TBL_ToolsManualTimeChart] where AddinsName='" + toolName + "'", conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("toolName", toolName));
                    SqlDataReader sr;
                    sr = cmd.ExecuteReader();
                    int count = 0;
                    while (sr.Read())
                    {
                        count += Convert.ToInt32(sr["_count"]);
                    }
                    conn.Close();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                    return false;
                }
            }

        }

        public static int GetProjectInfo()
        {
            int ProjctID = 0;

            //   if ((RibbonLoginCheck() == true))
            //    {
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
                    // SupportDatas. EmployeeID = LoginWindow.global_loginfo.EMP_ID;
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

            // }

            return ProjctID;
        }

     

        //public static bool RibbonLoginCheck()
        //{
        //    try
        //    {
        //        if (!SupportDatas.CurrentLoginInformation.LoginStatus || string.IsNullOrEmpty(SupportDatas.CurrentLoginInformation.EmployeeId)
        //       || string.IsNullOrWhiteSpace(SupportDatas.CurrentLoginInformation.EmployeeId))
        //        {
        //            LoginWindow loginWindow = new LoginWindow(SupportDatas.CentralLoginConnectionString, SupportDatas.LocalDBLocation, SupportDatas.LocalDBConnectionString);
        //            loginWindow.ShowDialog();
        //          //    UpdateLoginInformation();
        //        }
        //        LoginWindow local = new LoginWindow();
        //        local. = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //        EmployeeID = LoginWindow.global_loginfo.EMP_ID;
        //        EmployeeName = LoginWindow.global_loginfo.Emp_Name;
        //        if (local.LocalDatabase_chk(local).login_status == false)
        //        {
        //            if (string.IsNullOrEmpty(local.status_message))
        //            {
        //                Window window = new Window
        //                {
        //                    Title = "MainPage",
        //                    Content = new LoginWindow(),
        //                    MaxHeight = 325,
        //                    MaxWidth = 310,
        //                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
        //                    Icon = new BitmapImage(new Uri(AssmLoc + @"\Images\rnd.ico"))


        //                };

        //                window.ShowDialog();
        //                if (LoginWindow.global_loginfo.login_status == true)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    System.Windows.MessageBox.Show("Please provide proper User Name and Password");
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        }
        //        else
        //        {
        //            LoginWindow.global_loginfo = local;
        //            return true;

        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        string str = ex.ToString();
        //        return false;
        //    }

        //}

        public bool CheckLicense()
        {
            if (System.IO.File.Exists(OfflineInstaller.OfflineInst.FileBasePath + "License.lic"))
            {
                OfflineInstaller.OfflineInst.CreateINTFile();
                if (OfflineInstaller.OfflineInst.Compare())
                {
                    return true;
                }

            }
            else
            {
                OfflineInstaller.OfflineInst.CreateINTFile();
                System.Windows.MessageBox.Show("License file missing.Please copy  License.int file from " + OfflineInstaller.OfflineInst.FileBasePath + " and send to R&D dept for further process");
                System.Diagnostics.Process.Start(OfflineInstaller.OfflineInst.FileBasePath);
                return false;
            }
            return false;

        }

        public bool CheckProjectInfo()
        {
            if (SupportDatas.ToolLicenseAccessType == "111ext")
            {
                return true;
            }
            if (ProjectInfoValidation.IsHasProjectInfo(out _))
            {
                return true;
            }
            else
            {
                if (System.Windows.MessageBox.Show("Please provide Project Information", "ProjectInfo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information) == System.Windows.MessageBoxResult.OK)
                {
                    try
                    {
                        PISProjectInformationUI obj = new PISProjectInformationUI(SupportDatas.RevitToolConnectionString);
                        obj.ShowDialog();
                        string projCode = string.Empty;
                        SupportDatas.ProjectId = DatabaseInformation.GetProjectIdNCode(SupportDatas.RevitToolConnectionString, out projCode);
                        if (ProjectInfoValidation.IsHasProjectInfo(out _))
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                }

            }
            return false;
        }

        public virtual bool TryShowCommandHelp(string name)
        {
            System.Windows.MessageBox.Show("Got little help");
            return true;
        }
    }
}
