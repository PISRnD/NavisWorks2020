using Autodesk.Navisworks.Api.Plugins;
using PivdcNavisworksSupportModule;
//using PiNavisworks.PiNavisworksSupport;
using Utility.Model;

namespace ClashOptimiser
{
    [PluginAttribute("ClashOptimizer", "PIS_ClashOptimizer_1", DisplayName = "Clash Optimizer", ToolTip = "Clash Optimiser")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class ClashOptimiserQuickClass : AddInPlugin
    {     
 
        public override int  Execute(params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("Clash Optimiser",true))
            {
                ClashOptimiserUI clashOptimiserUI = new ClashOptimiserUI();
                clashOptimiserUI.Show();
            }
            return 0;
        }       
    }    
}
