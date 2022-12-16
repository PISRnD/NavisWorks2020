using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using PivdcNavisworksSupportModule;
//using PiNavisworks.PiNavisworksSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;

namespace ServicesBelowCeiling
{
    [PluginAttribute("ServicesBelowCeiling", "PIS_ServicesBelowCeiling_4", DisplayName = "Services Below Ceiling", ToolTip = "Identify services below ceilings")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class ServicesBelowCeilingQuickClass : AddInPlugin
    {
        Document doc;
        // public static int ToolId = ToolValidation.IsValidNavisTool(ToolValidation.ToolConnectionString, "ServicesBelowCeiling");
        public override int Execute(params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("ServicesBelowCeiling",true))
            { 
                this.doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            UIServicesBelowCeiling obj = new UIServicesBelowCeiling();
            obj.Show();
        }
            return 0;
        }
    }
    
}
