using Autodesk.Navisworks.Api;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Utility.Model;
using Utility.Module;

namespace ServicesBelowCeiling
{
    public partial class UIServicesBelowCeiling : Form
    {
        public static DateTime dtS = new DateTime();
        public static DateTime dtE = new DateTime();
        public static int sec = 0; 
        Document doc;
        Autodesk.Navisworks.Api.Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        Units projectunits { get; set; }
        public UIOperationServicesBelowCeiling UIOperationServicesBelowCeilingObj { get; set; }

        /// <summary>
        /// This is the constructor class of UI
        /// </summary>
        public UIServicesBelowCeiling()
        {
            InitializeComponent();
            UIOperationServicesBelowCeilingObj = new UIOperationServicesBelowCeiling(this);
            this.doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
             projectunits = doc.Units;                
            cmbUnit.Items.AddRange(Enum.GetNames(typeof(Units)));
            cmbUnit.SelectedIndex = cmbUnit.FindString(projectunits.ToString());
        }

        /// <summary>
        /// This method exceutes while loading the UI
        /// </summary>
        /// <param name="sender">Not implemented</param>
        /// <param name="e">Not implemented</param>
        private void UI_ServicesBelowCeiling_Load(object sender, EventArgs e)
        {
            rdBtnBelowCeiling.Checked = true;
            if (rdBtnBelowCeiling.Checked == true)
            {
                lblUnit.Enabled = false;
                lblFloorHeight.Enabled = false;
                cmbLevel.Enabled = false;
                cmbUnit.Enabled = false;
                lblLevel.Enabled = false;
                numFloorHeight.Enabled = false;
                lblCategoryName.Enabled = false;
                lblPropertyName.Enabled = false;
                lblPropertyValue.Enabled = false;
                txtCategoryDisplayName.Enabled = false;
                cmbPropertyName.Enabled = false;
                cmbPropertyValue.Enabled = false;
                txtBoxPropertyName.Enabled = false;
                txtPropertyValue.Enabled = false;
                rdBtnRevit.Enabled = true;
                rdBtnOther.Enabled = true;
                PopulateListBox();
            }
        }

        /// <summary>
        /// This method toggles 
        /// </summary>
        /// <param name="sender">Not implemented</param>
        /// <param name="e">Not implemented</param>
        private void button_Find_Click(object sender, EventArgs e)
        {
            if (rdBtnFloorHeight.Checked==true)
            {
                FindFromFloor();
            }
            else if(rdBtnAlignedCeiling.Checked==true)
            {
                CeilingAlignedElements(doc);                
            }
            else if(rdBtnBelowCeiling.Checked==true)
            {              
                Find();
            }
        }


        private void PopulateListBox()
        {
            Search search = new Search();
            SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category");
            search.SearchConditions.Add(sc1);
            search.Selection.SelectAll();
            search.Locations = SearchLocations.DescendantsAndSelf;
            List<string> categories = new List<string>();
            categories = search.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()).ToList();
            categories = categories.Distinct().OrderBy(x => x).ToList();
            this.Invoke(new System.Action(() => { listBox_categories.DataSource = categories; }));
            if (listBox_categories.Items.Count == 0)
            {
                MessageBox.Show("There is no Revit elements");

            }
        }

        public void CeilingAlignedElements(Document document)
        {
            DatabaseManager.SetTime();
            ElementDetails.CategoryName = txtCategoryDisplayName.Text;
            if (txtBoxPropertyName.Text.Length == 0)
            { 
            ElementDetails.PropertyName = cmbPropertyName.SelectedItem.ToString();
            }
            else
            {
                ElementDetails.PropertyName = txtBoxPropertyName.Text;
            }
            if (txtPropertyValue.Text.Length == 0)
            {
                ElementDetails.PropertyValue = cmbPropertyValue.SelectedItem.ToString();
            }
            else
            {
                ElementDetails.PropertyValue = txtPropertyValue.Text;
            }
            Search searchAirTerminals = new Search();
            
            SearchCondition scAirTeminals = SearchCondition.HasPropertyByDisplayName(ElementDetails.CategoryName, ElementDetails.PropertyName).EqualValue(VariantData.FromDisplayString(ElementDetails.PropertyValue));
            searchAirTerminals.SearchConditions.Add(scAirTeminals);
            searchAirTerminals.Selection.SelectAll();
            searchAirTerminals.Locations = SearchLocations.DescendantsAndSelf;
            IEnumerable<ModelItem> intersectAirTerminals = searchAirTerminals.FindAll(document, false).ToList();
            int count = 0;
            intersectAirTerminals.ToList().ForEach(airTerminals =>
            {
                BoundingBox3D airTerminalsBounding = airTerminals.BoundingBox();
                Search searchCeilings = new Search();
                SearchCondition scCeilings = SearchCondition.HasPropertyByDisplayName(ElementDetails.CategoryName, ElementDetails.PropertyName).EqualValue(VariantData.FromDisplayString("Ceilings"));
                searchCeilings.SearchConditions.Add(scCeilings);
                searchCeilings.Selection.SelectAll();
                searchCeilings.Locations = SearchLocations.DescendantsAndSelf;
                IEnumerable<ModelItem> intersectCeilings = searchCeilings.FindAll(document, false)
                .Where(x => airTerminalsBounding.Intersects(x.BoundingBox())).ToList();

                foreach (ModelItem intersectCeiling in intersectCeilings.ToList())
                {

                    BoundingBox3D intersectCeilingBB = intersectCeiling.BoundingBox();
                    double value1 = airTerminalsBounding.Min.Z;
                    double value2 = intersectCeilingBB.Min.Z;
                    double result = Math.Abs(value1 - value2);
                    if (result < 0.02)
                        continue;

                    if ((result > 0.02) || (value1 > value2))
                    {
                        ModelItemCollection modelItems = new ModelItemCollection();
                        modelItems.Add(airTerminals);
                        SavedItem si = new SelectionSet(modelItems);
                        si.DisplayName = string.Format("One:{0}", count.ToString());
                        document.SelectionSets.AddCopy(si);
                        count++;
                    }
                    else
                    {
                        Search searchRooms = new Search();
                        SearchCondition scRooms = SearchCondition.HasPropertyByDisplayName(ElementDetails.CategoryName, ElementDetails.PropertyName).EqualValue(VariantData.FromDisplayString("Rooms"));
                        searchRooms.SearchConditions.Add(scRooms);
                        searchRooms.Selection.SelectAll();
                        searchRooms.Locations = SearchLocations.DescendantsAndSelf;
                        IEnumerable<ModelItem> intersectRooms = searchRooms.FindAll(document, false)
                        .Where(x => airTerminalsBounding.Intersects(x.BoundingBox())).ToList();
                        if (intersectRooms.Any())
                        {
                            ModelItemCollection modelItems = new ModelItemCollection();
                            modelItems.Add(airTerminals);
                            SavedItem si = new SelectionSet(modelItems);
                            si.DisplayName = string.Format("One:{0}", count.ToString());
                            document.SelectionSets.AddCopy(si);
                            count++;
                        }
                    }
                }               
            });
            MessageBox.Show("Process completed successfully.");
            ToolSupport.InsertUsage(oDoc, count);         
        }

        private void FetchAllLevels()
        {            
            Search search1 = new Search();
            SearchCondition sc2 = SearchCondition.HasPropertyByDisplayName("Level", "Name");
            search1.SearchConditions.Add(sc2);
            search1.Selection.SelectAll();
            search1.Locations = SearchLocations.DescendantsAndSelf;
            
            List<string> layerNameList = new List<string>();
           layerNameList = search1.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Level").FirstOrDefault().Properties.Where(y => y.DisplayName == "Name").FirstOrDefault().Value.ToDisplayString()).ToList().Distinct().OrderBy(x => x).ToList();
            layerNameList.ToList().ForEach((layer) =>
            {
                cmbLevel.Items.Add(layer);
            });
        }

        /// <summary>
        /// This method is to find the services which are under the given height
        /// </summary>
        public void FindFromFloor()
        {
            List<MyItem> mi_floors = getFloors();
            List<MyItem> mi_services = getServices();
            List<MyItem> mi_Issue_Services = new List<MyItem>();
            int i = 1;
            //dtS = DateTime.Now;
            DatabaseManager.SetTime();
            MyItem myItem = null;
            FetchAllLevels();
           myItem = mi_floors.Where(x => x.level == cmbLevel.SelectedItem.ToString()).Select(x => x).FirstOrDefault();
            double floorElevation = 0;
            if (myItem != null)
            {
                 floorElevation = myItem.bbx.Max.Z;
            }
            double userinput = Convert.ToDouble(numFloorHeight.Text);
            double floorElevationUpto = 0.0;
            if(cmbUnit.SelectedItem.ToString()!= doc.Units.ToString())
            {               
              double changeValueFactor= UnitConversion.ScaleFactor((Units)Enum.Parse(typeof(Units),cmbUnit.SelectedItem.ToString()), doc.Units);
              userinput = userinput * changeValueFactor;
              floorElevationUpto = floorElevation + userinput;
            }
            else
            {
                floorElevationUpto = floorElevation + userinput;
            }
            mi_services.ToList().ForEach((service) =>
            {
                if (service.bbx.Min.Z >= floorElevation && service.bbx.Max.Z <= floorElevationUpto)
                {
                    mi_Issue_Services.Add(service);
                    ModelItemCollection modelItems = new ModelItemCollection();
                    modelItems.Add(service.mi);
                    String value1 = service.mi.PropertyCategories.FindPropertyByDisplayName("Element ID", "Value").Value.ToString();
                    SavedItem si = new SelectionSet(modelItems);
                    si.DisplayName = string.Format("{0}:{1}", value1, i.ToString());
                    doc.SelectionSets.AddCopy(si);
                    i++;
                }
            });
            MessageBox.Show("Process completed successfully.");
            ToolSupport.InsertUsage(oDoc, i);
            //  DatabaseManager.InsertUsages("ServicesBelowCeiling", Engine.ToolId, System.Environment.MachineName, System.Security.Principal.WindowsIdentity.GetCurrent().Name, i, Convert.ToInt64(sec),"2020", oDoc.FileName);           
        }

        /// <summary>
        /// This method is to find the elements which follows the condition
        /// </summary>
        public void Find()
        {
            List<MyItem> mi_floors = getFloors();
            List<MyItem> mi_ceilings = getCeilings();
            List<MyItem> mi_services = getServices();           
            int i = 1;
            DatabaseManager.SetTime();
           // dtS = DateTime.Now;
            foreach (MyItem mi_ceiling in mi_ceilings)
            {
                try
                {
                    List<MyItem> mi_Issue_Services = new List<MyItem>();                   
                    BoundingBox3D bbx_ceiling = mi_ceiling.bbx;
                    double minZ_ceiling = mi_ceiling.bbx.Min.Z;
                    MyItem mi_floorUnder = mi_floors.Where(x => (x.bbx.Max.Z < minZ_ceiling) && (new BoundingBox3D(new Point3D(bbx_ceiling.Min.X, bbx_ceiling.Min.Y, x.bbx.Max.Z), new Point3D(bbx_ceiling.Max.X, bbx_ceiling.Max.Y, bbx_ceiling.Min.Z)).Intersects(x.bbx))).OrderByDescending(x => x.bbx.Max.Z).FirstOrDefault();//Commented By preeti
                    if (mi_floorUnder != null)
                    {                        
                        BoundingBox3D bbx = new BoundingBox3D(new Point3D(bbx_ceiling.Min.X, bbx_ceiling.Min.Y, mi_floorUnder.bbx.Max.Z), new Point3D(bbx_ceiling.Max.X, bbx_ceiling.Max.Y, bbx_ceiling.Min.Z));
                        mi_Issue_Services.AddRange(mi_services.Where(x => x.bbx.Intersects(bbx)));
                        if (mi_Issue_Services.Count > 0)
                        {
                            mi_Issue_Services = mi_Issue_Services.Distinct().ToList();
                            ModelItemCollection micl_issueServices = new ModelItemCollection();
                            mi_Issue_Services.ForEach((x) =>
                            {                              
                                micl_issueServices.Add(x.mi);
                            });
                            SavedItem si = new SelectionSet(micl_issueServices);
                            String value1 = mi_ceiling.mi.PropertyCategories.FindPropertyByDisplayName("Element ID", "Value").Value.ToString();
                            si.DisplayName = string.Format("{0}:{1}", value1, i.ToString());
                            //  si.DisplayName = i + " -> " + mi_ceiling.mi.PropertyCategories.FindPropertyByDisplayName("Element", "Name").Value.ToDisplayString() + " C id : " + mi_ceiling.mi.PropertyCategories.FindPropertyByDisplayName("Element", "Id").Value.ToInt32();
                            this.doc.SelectionSets.AddCopy(si);
                            i++;
                        }
                    }               
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message);
                }
                //dtE = DateTime.Now;
                //sec += (dtE - dtS).Seconds;
            }
            MessageBox.Show("Process completed successfully.");
            ToolSupport.InsertUsage(oDoc, i);
            //   DatabaseManager.InsertUsages("ServicesBelowCeiling", Engine.ToolId, System.Environment.MachineName, System.Security.Principal.WindowsIdentity.GetCurrent().Name, i, Convert.ToInt64(sec),  "2020", oDoc.FileName);           
        }

        /// <summary>
        /// This method is to get all the floors document
        /// </summary>
        /// <returns>returns the list of floor item</returns>
        public List<MyItem> getFloors()
        {
            List<MyItem> mi_floors = new List<MyItem>();
            try
            {
                Search search = new Search();
                SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category").EqualValue(VariantData.FromDisplayString("Floors"));
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;              
                mi_floors = search.FindAll(this.doc, false).Cast<ModelItem>().Select(x => new MyItem() { mi = x, myCat = MyCategory.floors, level= x.PropertyCategories.FindPropertyByDisplayName("Level", "Name").Value.ToDisplayString()}).ToList();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            return mi_floors;
        }
          
        /// <summary>
        /// This method is to get the ceiling of the navisworks document
        /// </summary>
        /// <returns>returns the list of ceiling item</returns>
        public List<MyItem> getCeilings()
        {
            List<MyItem> mi_ceilings = new List<MyItem>();
            try
            {
                Search search = new Search();
                SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category").EqualValue(VariantData.FromDisplayString("Ceilings"));
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                mi_ceilings = search.FindAll(this.doc, false).Cast<ModelItem>().Select(x => new MyItem() { mi = x, myCat = MyCategory.ceiling }).ToList();
            }
            catch (Exception ex)
            {

            }
            return mi_ceilings;
        }

        /// <summary>
        /// This method is to get the services of the navisworks document
        /// </summary>
        /// <returns>returns the list of services item</returns>
        public List<MyItem> getServices()
        {
            List<MyItem> mi_Pipes = new List<MyItem>();
            if (rdBtnRevit.Checked == true)
            {
                try
                {
                    List<string> selectedCategories = listBox_categories.SelectedItems.Cast<string>().ToList();
                    Search search = new Search();
                    foreach (string category in selectedCategories)
                    {
                        SearchCondition sc = SearchCondition.HasPropertyByDisplayName("Element", "Category").EqualValue(VariantData.FromDisplayString(category));
                        search.SearchConditions.AddGroup(new List<SearchCondition>() { sc });
                    }
                    search.Selection.SelectAll();
                    search.Locations = SearchLocations.DescendantsAndSelf;
                    mi_Pipes = search.FindAll(this.doc, false).Cast<ModelItem>().Select(x => new MyItem() { mi = x, myCat = MyCategory.services }).ToList();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                ElementDetails.CategoryName = txtCategoryDisplayName.Text;
                if (txtBoxPropertyName.Text.Length == 0)
                {
                    ElementDetails.PropertyName = cmbPropertyName.SelectedItem.ToString();
                }
                else
                {
                    ElementDetails.PropertyName = txtBoxPropertyName.Text;
                }
                if (txtPropertyValue.Text.Length == 0)
                {
                    ElementDetails.PropertyValue = cmbPropertyValue.SelectedItem.ToString();
                }
                else
                {
                    ElementDetails.PropertyValue = txtPropertyValue.Text;
                }
                try
                {
                    List<string> selectedCategories = listBox_categories.SelectedItems.Cast<string>().ToList();
                    Search search = new Search();
                    foreach (string category in selectedCategories)
                    {
                        SearchCondition sc = SearchCondition.HasPropertyByDisplayName(ElementDetails.CategoryName, ElementDetails.PropertyName).EqualValue(VariantData.FromDisplayString(ElementDetails.PropertyValue));
                        search.SearchConditions.AddGroup(new List<SearchCondition>() { sc });
                    }
                    search.Selection.SelectAll();
                    search.Locations = SearchLocations.DescendantsAndSelf;
                    mi_Pipes = search.FindAll(this.doc, false).Cast<ModelItem>().Select(x => new MyItem() { mi = x, myCat = MyCategory.services }).ToList();
                }
                catch (Exception ex)
                {

                }
            }
            return mi_Pipes;
        }

    

        private void rdBtnBelowCeiling_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnBelowCeiling.Checked)
            {
                if (rdBtnRevit.Checked == true)
                {
                    listBox_categories.Enabled = true;
                    lblUnit.Enabled = false;
                    lblFloorHeight.Enabled = false;
                    cmbLevel.Enabled = false;
                    lblLevel.Enabled = false;
                    numFloorHeight.Enabled = false;
                    lblCategoryName.Enabled = false;
                    lblPropertyName.Enabled = false;
                    lblPropertyValue.Enabled = false;
                    txtCategoryDisplayName.Enabled = false;
                    txtBoxPropertyName.Enabled = false;
                    txtPropertyValue.Enabled = false;
                    cmbPropertyName.Enabled = false;
                    cmbPropertyValue.Enabled = false;
                }
                else
                {
                    lblUnit.Enabled = false;
                    lblFloorHeight.Enabled = false;
                    cmbLevel.Enabled = false;
                    lblLevel.Enabled = false;
                    numFloorHeight.Enabled = false;
                    lblCategoryName.Enabled = true;
                    lblPropertyName.Enabled = true;
                    lblPropertyValue.Enabled = true;
                    txtCategoryDisplayName.Enabled = true;
                    txtBoxPropertyName.Enabled = true;
                    txtPropertyValue.Enabled = true;
                    cmbPropertyName.Enabled = true;
                    cmbPropertyValue.Enabled = true;
                }
            }
            else
            {
                lblUnit.Enabled = true;
                lblFloorHeight.Enabled = true;
                cmbLevel.Enabled = true;
                lblLevel.Enabled = true;
                numFloorHeight.Enabled = true;
                lblCategoryName.Enabled = true;
                lblPropertyName.Enabled = true;
                lblPropertyValue.Enabled = true;
                txtCategoryDisplayName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
            }
        }

        private void rdBtnFloorHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnFloorHeight.Checked)
            {
                lblUnit.Enabled = true;
                lblFloorHeight.Enabled = true;
                cmbLevel.Enabled = true;
                cmbUnit.Enabled = true;
                lblLevel.Enabled = true;
                numFloorHeight.Enabled = true;
                lblCategoryName.Enabled = false;
                lblPropertyName.Enabled = false;
                lblPropertyValue.Enabled = false;
                txtCategoryDisplayName.Enabled = false;
                cmbPropertyName.Enabled = false;
                cmbPropertyValue.Enabled = false;
                grpFileOption.Enabled = false;
                listBox_categories.Enabled = false;
                FetchAllLevels();
            }
            else
            {
                lblUnit.Enabled = false;
                lblFloorHeight.Enabled = false;
                cmbLevel.Enabled = false;
                cmbUnit.Enabled = false;
                lblLevel.Enabled = false;
                numFloorHeight.Enabled = false;
                lblCategoryName.Enabled = true;
                lblPropertyName.Enabled = true;
                lblPropertyValue.Enabled = true;
                txtCategoryDisplayName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
            }
        }

        private void rdBtnAlignedCeiling_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnAlignedCeiling.Checked)
            {
                lblUnit.Enabled = false;
                lblFloorHeight.Enabled = false;
                cmbLevel.Enabled = false;
                lblLevel.Enabled = false;
                numFloorHeight.Enabled = false;
                lblCategoryName.Enabled = true;
                lblPropertyName.Enabled = true;
                lblPropertyValue.Enabled = true;
                txtCategoryDisplayName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
                txtBoxPropertyName.Enabled = true;
                txtPropertyValue.Enabled = true;
                grpFileOption.Enabled = false;
                listBox_categories.Enabled = false;
            }
            else
            {
                lblUnit.Enabled = true;
                lblFloorHeight.Enabled = true;
                cmbLevel.Enabled = true;
                lblLevel.Enabled = true;
                numFloorHeight.Enabled = true;
                lblCategoryName.Enabled = false;
                lblPropertyName.Enabled = false;
                lblPropertyValue.Enabled = false;
                txtCategoryDisplayName.Enabled = false;
                cmbPropertyName.Enabled = false;
                cmbPropertyValue.Enabled = false;
                txtBoxPropertyName.Enabled = false;
                txtPropertyValue.Enabled = false;
                grpFileOption.Enabled = true;
                listBox_categories.Enabled = true;
            }
         
        }

        private void txtCategoryDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ElementDetails.CategoryName = txtCategoryDisplayName.Text;
            if(txtCategoryDisplayName.TextLength < 1)
            {
                try
                {
                    cmbPropertyName.Items.Clear();
                    cmbPropertyValue.Items.Clear();
                    txtBoxPropertyName.Clear();
                    txtPropertyValue.Clear();
                }
                catch (Exception)
                {

                  //  throw;
                }
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
             
                UtilityClass.ParameterNamesWindows(ElementDetails.CategoryName, doc, cmbPropertyName);
            }

        }

        private void cmbPropertyNameSelectedIndexChanged(object sender, EventArgs e)
        {
            UIOperationServicesBelowCeilingObj.PropertyNameSelectedIndexChanged();
        }

        

        private void txtBoxPropertyNameKeyPress(object sender, KeyPressEventArgs e)
        {
            UIOperationServicesBelowCeilingObj.TextBoxPropertyNameTextOrKeyPress();
            
        }

        private void txtPropertyValueKeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPropertyValue.TextLength > 0 && cmbPropertyValue.Enabled == true)
            {
                cmbPropertyValue.Enabled = false;                
            }
            else
            {
                cmbPropertyValue.Enabled = true;
            }
        }

    

     

        private void rdBtnOtherCheckedChanged(object sender, EventArgs e)
        {
            UIOperationServicesBelowCeilingObj.OtherButtonCheckedChanged();
        }

        private void rdBtnRevitCheckedChanged(object sender, EventArgs e)
        {
            UIOperationServicesBelowCeilingObj.RevitButtonCheckChanged();

        }
    }

    /// <summary>
    /// This is the MyItem class
    /// </summary>
    public class MyItem
    {
        private ModelItem _mi;
        public ModelItem mi
        {
            get
            {
                return _mi;
            }
            set
            {
                _mi = value;
                bbx = value.BoundingBox();               
            }
        }
        public MyCategory myCat { get; set; }
        public BoundingBox3D bbx { get; set; }
        public string level { get; set; }
    }

    /// <summary>
    /// This is the enum definition of MyCategory
    /// </summary>
    public enum MyCategory
    {
        floors = 0,
        ceiling = 1,
        services=2
    }
}
