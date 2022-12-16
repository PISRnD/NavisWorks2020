using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Levelwise_Viewpoint_Creater.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levelwise_Viewpoint_Creater.Module
{

    [PluginAttribute("LevelwiseViewpointExporter", "ID_LevelwiseViewpointExporter",
        DisplayName = "Viewpoint Exporter", ToolTip = "Level wise Viewpoint Exporter")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class ExportViewPointExporter : AddInPlugin
    {
        public Document document { get; set; }
        public override int Execute(params string[] parameters)
        {
            document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            FrmViewPointExporter frmViewPointExporter = new FrmViewPointExporter(document);
            frmViewPointExporter.ShowDialog();

            return 0;
        }
    }
}
