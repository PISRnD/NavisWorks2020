using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Navisworks.Api.Plugins;

namespace ClashClusters
{
    [Plugin("ClashClusters.ClashClustersPane", "PIS_ClashClusters_7", DisplayName = "Clash Clusters", ToolTip = "Clash Clusters")]
    [DockPanePlugin(300, 380)]
    class ClashClustersPane : DockPanePlugin
    {
        public override Control CreateControlPane()
        {
            //create the control that will be used to display in the pane
            ClashClustersHostingControl control = new ClashClustersHostingControl();

            control.Dock = DockStyle.Fill;

            //create the control
            control.CreateControl();

            return control;
        }

        public override void DestroyControlPane(Control pane)
        {
            pane.Dispose();
        }
    }
}
