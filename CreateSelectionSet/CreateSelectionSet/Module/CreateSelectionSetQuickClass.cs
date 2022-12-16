using Autodesk.Navisworks.Api.Plugins;
using Utility.Model;
using Application = Autodesk.Navisworks.Api.Application;
using PivdcNavisworksSupportModule;

namespace CreateSelectionSet
{
    [PluginAttribute("CreateSelectionSet", "PIS_CreateSelectionSet_3", DisplayName = "Create Selection Set", ToolTip = "Automatically creates Selection sets according to the distinct values of a Revit Parameter")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class CreateSelectionSet:AddInPlugin
    {       
        /// <summary>
        /// This is the entry point of the Create Selection Set add-ins
        /// </summary>
        /// <param name="parameters">This is the argument</param>
        /// <returns>returns 0 or 1</returns>
        public override int Execute(params string[] parameters)
        {
            if (ElementDetails.Document.IsValidTool("CreateSelectionSet",true))
            {
                ElementDetails.Document = Application.ActiveDocument;
                ElementDetails.ModelItemCollection = ElementDetails.Document.CurrentSelection.SelectedItems;
                UICreateSelectionSet form = new UICreateSelectionSet();
                form.ShowDialog();
            }
                return 0;           
        }
               
    }
}
