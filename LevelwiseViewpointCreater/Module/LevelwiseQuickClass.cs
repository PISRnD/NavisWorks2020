using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Levelwise_Viewpoint_Creater.Model;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Levelwise_Viewpoint_Creater
{
    [PluginAttribute("LevelwiseViewpointCreator", "PIS_LevelwiseViewpointCreator_11",
        DisplayName = "Levelwise Viewpoint Creator", ToolTip = "Levelwise Viewpoint Creator")]
    [AddInPluginAttribute(AddInLocation.AddIn, CallCanExecute = CallCanExecute.Always)]
    public class LevelwiseQuickClass : AddInPlugin
    {
        public static string AssemblyLocation { get; set; }
        public static int ToolId { get; set; }
        public static bool IsLicenseApplied { get; set; }
        public static string EmployeeID { get; set; }
        public static string ToolConnectionString { get; set; }
        public static Configuration confuguration { get; set; }
        public static string SystemHostName { get; set; }
        public Document document { get; set; }
        public override int Execute(params string[] parameters)
        {
            document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            FetchAllLevels();
            return 0;
        }

        private void FetchAllLevels()
        {
            Search search1 = new Search();
            SearchCondition sc2 = SearchCondition.HasPropertyByDisplayName("Level", "Name");
            search1.SearchConditions.Add(sc2);
            search1.Selection.SelectAll();
            search1.Locations = SearchLocations.DescendantsAndSelf;

            List<string> layerNameList = new List<string>();
            layerNameList = search1.FindIncremental(Application.ActiveDocument, false)
            .Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Level")
            .FirstOrDefault().Properties.Where(y => y.DisplayName == "Name")
            .FirstOrDefault().Value.ToDisplayString()).ToList().Distinct().OrderBy(x => x).ToList();
            List<MyItem> levelwiseElementList = new List<MyItem>();
            layerNameList.ToList().ForEach((levelItem) =>
            {
                Search search = new Search();
                SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Level", "Name")
                .EqualValue(VariantData.FromDisplayString(levelItem));
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                levelwiseElementList = search.FindAll(document, false).Cast<ModelItem>()
                .Select(x => new MyItem() { mi = x, level = levelItem, bbx = x.BoundingBox(true) }).ToList();

              
                BoundingBox3D boundingBox3D = new BoundingBox3D();

                int index = 0;

                MyItem myItem = levelwiseElementList.Where(x => x.bbx.HasVolume)
                .OrderByDescending(x => x.bbx.Volume).FirstOrDefault();

                levelwiseElementList.ToList().OrderBy(x => x.level).ToList().ForEach(item =>
                {
                    if (index == 0)
                    {
                        boundingBox3D = item.bbx;
                    }
                    else
                    {
                        boundingBox3D.Extend(item.bbx.Min);
                        boundingBox3D.Extend(item.bbx.Center);
                        boundingBox3D.Extend(item.bbx.Max);
                    }
                    index++;

                });

                Viewpoint vPoint = document.CurrentViewpoint.CreateCopy();
                vPoint.Position = boundingBox3D.Center;
                SavedViewpoint savedViewpoint = new SavedViewpoint(vPoint);
                savedViewpoint.DisplayName = string.Format("{0}", levelItem);
                document.SavedViewpoints.AddCopy(savedViewpoint);

                


            });
        }

        public List<MyItem> GetAllELements()
        {
            List<MyItem> mi_floors = new List<MyItem>();
            try
            {
                Search search = new Search();
                SearchCondition sc1 = SearchCondition
                    .HasPropertyByDisplayName("Element", "Category")
                    .EqualValue(VariantData.FromDisplayString("Floors"));
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                mi_floors = search.FindAll(document, false)
                    .Cast<ModelItem>().Select(x => new MyItem()
                    { mi = x, level = x.PropertyCategories
                    .FindPropertyByDisplayName("Level", "Name").Value.ToDisplayString() })
                    .ToList();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            return mi_floors;
        }
    }
}
