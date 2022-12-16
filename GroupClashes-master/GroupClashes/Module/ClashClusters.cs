using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Navisworks.Api.Plugins;
using OfflineInstaller;
using System.Data.SqlClient;
using PiNavisworks.PiNavisworksSupport;
using Utility.Model;
using PivdcNavisworksSupportModule;

namespace ClashClusters
{
    [Plugin("ClashClusters", "PIS_ClashClusters_7", DisplayName = "Clash Clusters")]
    [Strings("ClashClusters.name")]
    [RibbonLayout("ClashClusters.xaml")]
    [RibbonTab("ID_GroupClashesTab",
        DisplayName = "Clash Clusters")]


    public class RibbonHandler : CommandHandlerPlugin
    {
        private bool m_toShowTab;
        private bool m_toEnableButton;
        
        
        public RibbonHandler()
        {
            m_toShowTab = false; // to show tab or not
            m_toEnableButton = false; // to enable button or not
        }
    //    public static int ToolId = ToolValidation.IsValidNavisTool(ToolValidation.ToolConnectionString, "nwcClashCluster");
        public override int ExecuteCommand(string commandId, params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("nwcClashCluster",true))
            {
                if (Autodesk.Navisworks.Api.Application.IsAutomated)
                {
                    throw new InvalidOperationException("Invalid when running using Automation");
                }

                //Find the plugin
                PluginRecord pr = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("GroupClashes.GroupClashesPane.PIS_ClashCluster_7");

                if (pr != null && pr is DockPanePluginRecord && pr.IsEnabled)
                {
                    //check if it needs loading
                    if (pr.LoadedPlugin == null)
                    {
                        pr.LoadPlugin();
                    }

                    DockPanePlugin dpp = pr.LoadedPlugin as DockPanePlugin;
                    if (dpp != null)
                    {
                        dpp.Visible = !dpp.Visible;
                    }
                }
            }
            return 0;
        }

        public override CommandState CanExecuteCommand(String commandId)
        {
            CommandState state = new CommandState();
            state.IsVisible = true;
            state.IsEnabled = true;
            state.IsChecked = true;

            return state;
        }

        public override bool CanExecuteRibbonTab(string name)
        {
            return true;
        }

        public override bool TryShowCommandHelp(string name)
        {
            FileInfo dllFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string pathToHtmlFile = Path.Combine(dllFileInfo.Directory.FullName, @"Help\Help.html");
            System.Diagnostics.Process.Start(pathToHtmlFile);
            return true;
        }


    }
}


