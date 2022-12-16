using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utility.Model;

namespace NwcOpenLocation
{
    [PluginAttribute("NwcOpenLocation", "PIS_NwcOpenLocation_6", DisplayName = "LocationExtractor", ToolTip = "Locate the append file in the current Navisworks file")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]

    ///<summary>
    ///This is the entry class of the Location Extractor add-ins
    ///</summary>
    public class NwcLocationExtractorQuickClass : AddInPlugin
    {
        #region VariableDeclaration
        public Document Document { get; set; }
        public static List<string> AppendFiles { get; set; }
        public static int count { get; set; }
        public string FileName { get; set; } 
        #endregion

        /// <summary>
        /// This is the entry point of the NwcLocation Extractor add-ins
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int Execute(params string[] parameters)
        {
            AppendFiles = null;
            this.Document = Application.ActiveDocument;         
            FileName = Document.CurrentFileName;
            if (ElementDetails.Document.IsValidTool("nwcLocationExtractor",true))
            {
                try
                {
                    DatabaseManager.SetTime();
                    AppendFiles = Document.Models.Select(x => x.FileName).ToList();
                    if (AppendFiles.Count > 0)
                    {
                        start();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Append file information not found !");
                    }
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }            
            return 0;
        }

        /// <summary>
        /// This is the start method which calls the UIFileLocation form
        /// </summary>
        public void start()
        {
            UIFileLocation uIFileLocation = new UIFileLocation();
            uIFileLocation.Show();
        }
      
    }
}
