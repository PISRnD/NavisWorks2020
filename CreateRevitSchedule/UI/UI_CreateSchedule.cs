using Autodesk.Navisworks.Api;
using PivdcNavisworksSupportModule;
//using PiNavisworks.PiNavisworksSupport;//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//using PiNavisworks.PiNavisworksSupport;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Autodesk.Navisworks.Api.Application;

namespace CreateRevitSchedule
{
    public partial class UI_CreateSchedule : Form
    {

        List<string> properties = new List<string>();
        Document doc;
        DataTable dt = new DataTable();
     //   long timeTaken;
        public static DateTime dtS = new DateTime();
        public static DateTime dtE = new DateTime();
        public static int sec = 0;
        Autodesk.Navisworks.Api.Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;

        public UI_CreateSchedule()
        {
            InitializeComponent();
            this.doc = Application.ActiveDocument;

        }

        private  void UI_CreateSchedule_Load(object sender, EventArgs e)
        {

            //await Task.Run(new Action(() =>
            //{

            Progress progress = Application.BeginProgress("ListCategories");
            progress.Update(0);
            //Search search = new Search();
            //SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category");
            //search.SearchConditions.Add(sc1);
            //search.Selection.SelectAll();
            //search.Locations = SearchLocations.DescendantsAndSelf;
            ////List<ModelItem> modelItems = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false).ToList();
            //Thread.Sleep(5000);
            List<string> categories = new List<string>();
            //categories = search.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()).ToList();

            categories = FetchCategories();
            progress.Update(0.5);
            this.Invoke(new System.Action(() => { listBox_categories.DataSource = categories; }));
            progress.Update(1);
            Application.EndProgress();

            //}));

        }
        private  List<string> FetchCategories()
        {
            Search search = new Search();
            SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category");
            search.SearchConditions.Add(sc1);
            search.Selection.SelectAll();
            search.Locations = SearchLocations.DescendantsAndSelf;
            //List<ModelItem> modelItems = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false).ToList();
          //  Thread.Sleep(5000);
            List<string> categories = new List<string>();
            ModelItemCollection modelItems = new ModelItemCollection();
            try
            {
                modelItems = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false);
                 categories = modelItems.Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()).ToList();
             //   categories = modelItems.Select(x => x.PropertyCategories.FindPropertyByDisplayName("Element", "Category").Value?.ToDisplayString()).ToList();
            }
            catch (Exception ex)
            {

                string str = ex.ToString();
            }
            //progress.Update(0.5);
            categories = categories.Distinct().OrderBy(x => x).ToList();
            return categories;
        }

        public ModelItemCollection getSCModelItemCollection()
        {
            ModelItemCollection coll = new ModelItemCollection();
            Progress progress = Application.BeginProgress();
            Thread.Sleep(2000);
            try
            {
                List<string> selectedCategories = listBox_categories.SelectedItems.Cast<string>().ToList();
                Search search = new Search();
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                double i = 0;
                foreach (string category in selectedCategories)
                {
                    SearchCondition sc = SearchCondition.HasPropertyByDisplayName("Element", "Category").EqualValue(VariantData.FromDisplayString(category));
                    search.SearchConditions.AddGroup(new List<SearchCondition>() { sc });
                    i = i + (0.5 / selectedCategories.Count);
                    progress.Update(i);
                }
                Thread.Sleep(2);
                progress.Update(0.5);
                coll = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                MessageBox.Show("unable to get the elements. Running low on memory or the file is corrupted. Please try later..");
            }
            progress.Update(1);
            
            Application.EndProgress();
            return coll;
        }
        private void listBox_categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invoke(new System.Action(() => { listBox_Properties.DataSource = null; }));

            //await Task.Run(new Action(() => 
            //{


            //List<ModelItem> SCModelItems = FillSCModelItemsList();
            //List<string> selectedCategories = listBox_categories.SelectedItems.Cast<string>().ToList();

            //Thread.Sleep(100);
            //if (SCModelItems.Count > 0)
            //{
            //    properties = SCModelItems.Where(x => selectedCategories.Contains(x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()))
            //                 .SelectMany(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Select(y => y.DisplayName).ToList()).ToList();
            //    properties = properties.Distinct().OrderBy(x => x).ToList();
            //    this.Invoke(new System.Action(() => { listBox_Properties.DataSource = properties; }));
            //}



            //SCModelItems = null;

            //}));
            /*-------------------------------------------------------------------------------------*/
            try
            {
                Progress progress = Application.BeginProgress("Listproperties");
                progress.Update(0);
                List<string> selectedCategories = listBox_categories.SelectedItems.Cast<string>().ToList();
                Search search = new Search();
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                progress.Update(0.1);
                double i = 0.1;
                foreach (string category in selectedCategories)
                {
                    SearchCondition sc = SearchCondition.HasPropertyByDisplayName("Element", "Category").EqualValue(VariantData.FromDisplayString(category));
                    search.SearchConditions.AddGroup(new List<SearchCondition>() { sc });
                    i = i + (0.7 / selectedCategories.Count);
                    progress.Update(i);
                }
                progress.Update(0.8);
                List<string> properties = new List<string>();
                properties = search.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Where(x => selectedCategories.Contains(x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()))
                             .SelectMany(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Select(y => y.DisplayName).ToList()).ToList();
                progress.Update(0.9);
                properties = properties.Distinct().OrderBy(x => x).ToList();
                this.Invoke(new System.Action(() => { listBox_Properties.DataSource = properties; }));
                progress.Update(1);
                Application.EndProgress();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                MessageBox.Show("unable to get the elements. Running low on memory or the file is corrupted. Please try later..");
            }

        }

        private  void button_show_Click(object sender, EventArgs e)
        {          
            ShowSelectedPropertiesData();              
        }

        private void ShowSelectedPropertiesData()
        {
            // dtS = DateTime.Now;
            DatabaseManager.SetTime();
            ModelItemCollection SCModelItemsCollections = getSCModelItemCollection();
            Progress progress = Application.BeginProgress();

            List<string> selectedProperties = listBox_Properties.SelectedItems.Cast<string>().ToList();
            if (selectedProperties.Count > 0 && SCModelItemsCollections.Count > 0)
            {
                dt = new DataTable();
                foreach (string selectedProperty in selectedProperties)
                {
                    dt.Columns.Add(selectedProperty);
                }
                dt.Columns.Add("Attachment");
               progress.Update(0.2);
                this.Invoke(new System.Action(() => { progressBar1.Maximum = SCModelItemsCollections.Count; progressBar1.Step = 1; progressBar1.Value = 0; }));
                double i = 0.2;
                foreach (ModelItem SCmodelItem in SCModelItemsCollections)
                {
                   
                        try
                        {
                            List<string> row = new List<string>();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                try
                                {
                                    if (dc.ColumnName == "Attachment")
                                    {
                                        row.Add(getSuperParentName(SCmodelItem));
                                    }
                                    else
                                    {
                                        DataProperty dp = SCmodelItem.PropertyCategories.Where(x => x.DisplayName == "Element").FirstOrDefault().Properties.Where(x => x.DisplayName == dc.ColumnName).FirstOrDefault();
                                        row.Add(getValue(dp));
                                    }
                                }
                                catch (Exception)
                                {

                                }

                                Thread.Sleep(5);
                            }
                            dt.Rows.Add(row.ToArray());
                            Thread.Sleep(5);
                            this.Invoke(new System.Action(() => { progressBar1.PerformStep(); }));
                        }
                        catch (Exception ex)
                        {
                            string str = ex.ToString();
                        }
                        i = i + (0.8 / SCModelItemsCollections.Count);
                                       
                }
                this.Invoke(new System.Action(() => { dataGridView_Schedule.DataSource = null; dataGridView_Schedule.DataSource = dt; dataGridView_Schedule.Refresh(); }));
                progress.Update(1);
                Application.EndProgress();
            }
            if (dt.Rows.Count>0)
            {
                ToolSupport.InsertUsage(oDoc, dt.Rows.Count); 
            }         
        }
        private string getValue(DataProperty dp)
        {
            if (dp != null && !dp.Value.IsNone)
            {
                if (dp.Value.IsDouble) return dp.Value.ToDouble() + "";
                else if (dp.Value.IsBoolean) return dp.Value.ToBoolean() + "";
                else if (dp.Value.IsDateTime) return dp.Value.ToDateTime() + "";
                else if (dp.Value.IsDisplayString) return dp.Value.ToDisplayString();
                else if (dp.Value.IsDoubleAngle) return dp.Value.ToDoubleAngle() + "";
                else if (dp.Value.IsDoubleArea) return dp.Value.ToDoubleArea() + "";
                else if (dp.Value.IsDoubleLength) return dp.Value.ToDoubleLength() + "";
                else if (dp.Value.IsDoubleVolume) return dp.Value.ToDoubleVolume() + "";
                else if (dp.Value.IsInt32) return dp.Value.ToInt32() + "";
                else if (dp.Value.IsIdentifierString) return dp.Value.ToIdentifierString();
                else if (dp.Value.IsNamedConstant) return dp.Value.ToNamedConstant().DisplayName;
                else if (dp.Value.IsPoint2D) return dp.Value.ToPoint2D().X + ", " + dp.Value.ToPoint2D().Y;
                else if (dp.Value.IsPoint3D) return dp.Value.ToPoint3D().X + ", " + dp.Value.ToPoint3D().Y + "," + dp.Value.ToPoint3D().Z;
                else return "unknown";
            }
            else
            {
                return "";
            }
        }
        public string getSuperParentName(ModelItem mi)
        {
            string name = "";
            try
            {
                bool cont = true;
                ModelItem parent;
                do
                {
                    parent = mi.Parent;
                    if (parent != null)
                    {
                        PropertyCategory pc = parent.PropertyCategories.Where(x => x.DisplayName == "XRef").FirstOrDefault();
                        if (pc != null)
                        {
                            pc = parent.PropertyCategories.Where(x => x.DisplayName == "Item").FirstOrDefault();
                            name = pc.Properties.Where(x => x.DisplayName == "Name").FirstOrDefault().Value.ToDisplayString();
                            cont = false;
                        }
                    }
                    else
                    {
                        cont = false;
                    }
                    mi = parent;
                }
                while (cont);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            return name;
        }

        private void button_export_Click(object sender, EventArgs e)
        {         
            string savePath = "";
            folderBrowserDialog1.Description = "Select a folder to export the reports";
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savePath = folderBrowserDialog1.SelectedPath;
                if (Directory.Exists(savePath))
                {
                    try
                    {

                        if (dt != null && !dt.HasErrors)
                        {
                            using (FileStream stream = new FileStream(savePath+"\\"+Application.ActiveDocument.Title+".xls", FileMode.Create, FileAccess.Write))
                            {
                                ExcelWriter writer = new ExcelWriter(stream);
                                writer.BeginWrite();
                                int row = 1, col = 0;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    writer.WriteCell(0, col, dc.ColumnName);
                                    col++;
                                }

                                foreach (DataRow dr in dt.Rows)
                                {
                                    for (int i = 0; i < col; i++)
                                    {
                                        writer.WriteCell(row, i, dr[i] + "");
                                    }
                                    row++;
                                }
                                writer.EndWrite();
                                stream.Close();
                            }

                            Process.Start(savePath + "\\" + Application.ActiveDocument.Title + ".xls");
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                        MessageBox.Show("unable to create report. Check whether the file is open or not");
                    }  
                }                            
            }
        }

        private void button_ExportMultiple_Click(object sender, EventArgs e)
        {
            string savePath = "";
            folderBrowserDialog1.Description = "Select a folder to export the reports";
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savePath = folderBrowserDialog1.SelectedPath;
                if (Directory.Exists(savePath))
                {
                    List<string> fileNames;
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            fileNames = openFileDialog1.FileNames.ToList();
                            this.Invoke(new System.Action(() => { progressBar1.Maximum = fileNames.Count; progressBar1.Step = 1; progressBar1.Value = 0; }));
                            foreach (string fileName in fileNames)
                            {
                                try
                                {
                                    Application.ActiveDocument.OpenFile(fileName);
                                    ModelItemCollection SCModelItemsCollections = getSCModelItemCollection();
                                    Progress progress = Application.BeginProgress();

                                    List<string> selectedProperties = listBox_Properties.SelectedItems.Cast<string>().ToList();
                                    if (selectedProperties.Count > 0 && SCModelItemsCollections.Count > 0)
                                    {
                                        dt = new DataTable();
                                        foreach (string selectedProperty in selectedProperties)
                                        {
                                            dt.Columns.Add(selectedProperty);
                                        }
                                        dt.Columns.Add("Attachment");
                                        progress.Update(0.2);
                                        //this.Invoke(new System.Action(() => { progressBar1.Maximum = SCModelItemsCollections.Count; progressBar1.Step = 1; progressBar1.Value = 0; }));
                                        double i = 0.2;
                                        foreach (ModelItem SCmodelItem in SCModelItemsCollections)
                                        {
                                            try
                                            {
                                                try
                                                {
                                                    List<string> row = new List<string>();
                                                    foreach (DataColumn dc in dt.Columns)
                                                    {
                                                        try
                                                        {
                                                            if (dc.ColumnName == "Attachment")
                                                            {
                                                                row.Add(getSuperParentName(SCmodelItem));
                                                            }
                                                            else
                                                            {
                                                                DataProperty dp = SCmodelItem.PropertyCategories.Where(x => x.DisplayName == "Element").FirstOrDefault().Properties.Where(x => x.DisplayName == dc.ColumnName).FirstOrDefault();
                                                                row.Add(getValue(dp));
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {

                                                        }

                                                        Thread.Sleep(5);
                                                    }
                                                    dt.Rows.Add(row.ToArray());
                                                    Thread.Sleep(5);
                                                    //this.Invoke(new System.Action(() => { progressBar1.PerformStep(); }));
                                                }
                                                catch (Exception ex)
                                                {
                                                    string str = ex.ToString();
                                                }
                                                i = i + (0.8 / SCModelItemsCollections.Count);
                                                progress.Update(i);
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                        //this.Invoke(new System.Action(() => { dataGridView_Schedule.DataSource = null; dataGridView_Schedule.DataSource = dt; dataGridView_Schedule.Refresh(); }));
                                        progress.Update(1);
                                        Application.EndProgress();
                                    }
                                    else
                                    {
                                        MessageBox.Show("please select the properties to be quantified");
                                    }


                                    try
                                    {

                                        if (dt != null && !dt.HasErrors)
                                        {

                                            using (FileStream stream = new FileStream(savePath+"\\" + Path.GetFileNameWithoutExtension(fileName) + ".xls", FileMode.Create, FileAccess.Write))
                                            {
                                                ExcelWriter writer = new ExcelWriter(stream);
                                                writer.BeginWrite();
                                                int row = 1, col = 0;
                                                foreach (DataColumn dc in dt.Columns)
                                                {
                                                    writer.WriteCell(0, col, dc.ColumnName);
                                                    col++;
                                                }

                                                foreach (DataRow dr in dt.Rows)
                                                {
                                                    for (int i = 0; i < col; i++)
                                                    {
                                                        writer.WriteCell(row, i, dr[i] + "");
                                                    }
                                                    row++;
                                                }
                                                writer.EndWrite();
                                                stream.Close();
                                            }

                                            //Process.Start(@"c:\report.xls");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string str = ex.ToString();
                                        MessageBox.Show("unable to create report. Check whether the file is open or not/Or you might not have enough permissions");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    string str = ex.ToString();
                                    MessageBox.Show("unable to load the file : " + fileName);
                                }
                                this.Invoke(new System.Action(() => { progressBar1.PerformStep(); }));
                            }
                            MessageBox.Show("Export finished");
                        }
                        catch (Exception ex)
                        {
                            string str = ex.ToString();
                            MessageBox.Show("OOps.. Multiple file operation could not be executed");
                        }

                    }  
                }
                else
                {
                    MessageBox.Show("Invalid directory");
                }
            }
        }

        private  void Test_Click(object sender, EventArgs e)
        {
            //await Task.Run(new Action(() =>
            //{

                Progress progress = Application.BeginProgress("ListCategories");
                progress.Update(0);
                //Search search = new Search();
                //SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName("Element", "Category");
                //search.SearchConditions.Add(sc1);
                //search.Selection.SelectAll();
                //search.Locations = SearchLocations.DescendantsAndSelf;
                ////List<ModelItem> modelItems = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false).ToList();
                //Thread.Sleep(5000);
                List<string> categories = new List<string>();
                //categories = search.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Select(x => x.PropertyCategories.Where(y => y.DisplayName == "Element").FirstOrDefault().Properties.Where(y => y.DisplayName == "Category").FirstOrDefault().Value.ToDisplayString()).ToList();

                categories = FetchCategories();
                progress.Update(0.5);
                this.Invoke(new System.Action(() => { listBox_categories.DataSource = categories; }));
                progress.Update(1);
                Application.EndProgress();

           // }));
        }
    }
}
