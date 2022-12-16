using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.DocumentParts;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api.Timeliner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;
using System.IO;
using System.Data;
using PiNavisworks.PiNavisworksSupport;
using Utility.Model;
using PivdcNavisworksSupportModule;

namespace TimelineMerger
{
   // [PluginAttribute("TimelineMerger", "Pinnacle", DisplayName = "Timeline Merger", ToolTip = "Timeline Merger")]
    [PluginAttribute("TimelineMerger", "TimelineMerger_5", DisplayName = "Timeline Merger", ToolTip = "Timeline Merger")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always,
        CanToggle = true,
        Icon = @"Images\createSelectionSet16X16.ico",
        LargeIcon = @"Images\clashoptimiser32X32.png",
       LoadForCanExecute = true,
        Shortcut = "Ctrl+t",
        ShortcutWindowTypes = "")]
    public class Engine : AddInPlugin
    {

       // public static int ToolId = ToolValidation.IsValidNavisTool(ToolValidation.ToolConnectionString, "TimelineMerger_Configure");
        public override int Execute(params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("TimelineMerger_Configure",true))
            {
                try
                {
                    Nw.Document doc = Nw.Application.ActiveDocument;
                    Nw.DocumentParts.IDocumentTimeliner tl = doc.Timeliner;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Navisworks_Timeliner f = new Navisworks_Timeliner();
                f.ShowDialog();
            }
            return 0;
        }  
    }
}
