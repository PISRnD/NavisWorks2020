using Autodesk.Navisworks.Api;
using Microsoft.WindowsAPICodePack.Dialogs;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility.Module;
using Application = Autodesk.Navisworks.Api.Application;
//using PiNavisworks.PiNavisworksSupport;

namespace CreateRevitSchedule
{
    /// <summary>
    /// Interaction logic for CreateRevitScheduleUI.xaml
    /// </summary>
    public partial class CreateRevitScheduleUI : Window
    {
        List<string> properties = new List<string>();
        Document doc;
        DataTable dt = new DataTable();
        long timeTaken;
        public static DateTime dtS = new DateTime();
        public static DateTime dtE = new DateTime();
        public static int sec = 0;
        Autodesk.Navisworks.Api.Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        public string CategoryName { get; set; }
        public string PropertyName { get; set; }

        public CreateRevitScheduleUI()
        {
            InitializeComponent();
            this.doc = Application.ActiveDocument;
        }

        private  void CreateRevitScheduleUI_Loaded(object sender, RoutedEventArgs e)
        {
         
            //if (File.Exists(string.Format("{0}{1}", LoginWindow.AssmLoc, @"\Images\Pinnacle_icon.ico")))
            //{
            //    imageIcon.Source = new BitmapImage(new Uri(LoginWindow.AssmLoc + @"\Images\Pinnacle_icon.ico"));
            //}
            //if (rdBtnRevit.IsChecked == true)
            //{
                CategoryName = "Element";
                PropertyName = "Category";
                txtBlockCategoryName.IsEnabled = false;
                txtcategoryName.IsEnabled = false;
                Progress progress = Application.BeginProgress("ListCategories");
                progress.Update(0);
                List<string> categories = new List<string>();
                categories = UtilityClass.ParameterValue(CategoryName, PropertyName);
                progress.Update(0.5);
                this.Dispatcher.Invoke(new System.Action(() => { listBox_categories.ItemsSource = categories; }));
                progress.Update(1);
                Application.EndProgress();
         //   }
          
        }

        private List<string> FetchCategories(string CategoryName,string PropertyName)
        {
            Search search = new Search();
           
            SearchCondition sc1 = SearchCondition.HasPropertyByDisplayName(CategoryName, PropertyName);
            search.SearchConditions.Add(sc1);
            search.Selection.SelectAll();
            search.Locations = SearchLocations.DescendantsAndSelf;
            List<string> categories = new List<string>();
            ModelItemCollection modelItems = new ModelItemCollection();
            try
            {
                modelItems = search.FindAll(Autodesk.Navisworks.Api.Application.ActiveDocument, false);
                categories = modelItems.Select(x => x.PropertyCategories.Where(y => y.DisplayName == CategoryName).FirstOrDefault().Properties.Where(y => y.DisplayName == PropertyName).FirstOrDefault().Value.ToDisplayString()).ToList();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
          //  progress.Update(0.5);
            categories = categories.Distinct().OrderBy(x => x).ToList();
            return categories;
        }


        public ModelItemCollection getSCModelItemCollection(string CategoryName,string PropertyName)
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
                    SearchCondition sc = SearchCondition.HasPropertyByDisplayName(CategoryName, PropertyName).EqualValue(VariantData.FromDisplayString(category));
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
                MessageBox.Show("unable to get the elements. Running low on memory or the file is corrupted. Please try later..");
            }
            progress.Update(1);

            Application.EndProgress();
            return coll;
        }

        private void listBox_categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new System.Action(() => { listBox_Properties.ItemsSource = null; }));
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
                    SearchCondition sc = SearchCondition.HasPropertyByDisplayName(CategoryName, PropertyName).EqualValue(VariantData.FromDisplayString(category));
                    search.SearchConditions.AddGroup(new List<SearchCondition>() { sc });
                    i = i + (0.7 / selectedCategories.Count);
                    progress.Update(i);
                }
                progress.Update(0.8);
                List<string> properties = new List<string>();
                properties = search.FindIncremental(Autodesk.Navisworks.Api.Application.ActiveDocument, false).Where(x => selectedCategories.Contains(x.PropertyCategories.Where(y => y.DisplayName == CategoryName).FirstOrDefault().Properties.Where(y => y.DisplayName == PropertyName).FirstOrDefault().Value.ToDisplayString()))
                             .SelectMany(x => x.PropertyCategories.Where(y => y.DisplayName ==CategoryName).FirstOrDefault().Properties.Select(y => y.DisplayName).ToList()).ToList();
                progress.Update(0.9);
                properties = properties.Distinct().OrderBy(x => x).ToList();
                this.Dispatcher.Invoke(new System.Action(() => { listBox_Properties.ItemsSource = properties; }));
                progress.Update(1);
                Application.EndProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to get the elements. Running low on memory or the file is corrupted. Please try later..");
            }

        }

        private  void button_show_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectedPropertiesData();         
        }
        private void ShowSelectedPropertiesData()
        {
            dtS = DateTime.Now;
            ModelItemCollection SCModelItemsCollections = getSCModelItemCollection(CategoryName,PropertyName);
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
                this.Dispatcher.Invoke(new System.Action(() => { progressBar1.Maximum = SCModelItemsCollections.Count; progressBar1.Value = 0; }));
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
                                    DataProperty dp = SCmodelItem.PropertyCategories.Where(x => x.DisplayName == CategoryName).FirstOrDefault().Properties.Where(x => x.DisplayName == dc.ColumnName).FirstOrDefault();
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
                        this.Dispatcher.Invoke(new System.Action(() => { progressBar1.Value = i; }));
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                    i = i + (0.8 / SCModelItemsCollections.Count);

                }
                this.Dispatcher.Invoke(new System.Action(() => { dataGridView_Schedule.ItemsSource = null; dataGridView_Schedule.ItemsSource = dt.DefaultView; dataGridView_Schedule.Items.Refresh(); }));
                progress.Update(1);
                Application.EndProgress();
            }
            //dtE = DateTime.Now;
            //sec += (dtE - dtS).Seconds;
            if (dt.Rows.Count>0)
            {
                ToolSupport.InsertUsage(oDoc, dt.Rows.Count); 
            }
           // DatabaseManager.InsertUsages("CreateRevitSchedule", Engine.ToolId, System.Environment.MachineName, System.Security.Principal.WindowsIdentity.GetCurrent().Name, dt.Rows.Count, Convert.ToInt64(sec), "2020", oDoc.FileName);
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

            }
            return name;
        }

        private void button_export_Click(object sender, RoutedEventArgs e)
        {
            string savePath = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                savePath = dialog.FileName;
                if (Directory.Exists(savePath))
                {
                    try
                    {

                        if (dt != null && !dt.HasErrors)
                        {
                            using (FileStream stream = new FileStream(savePath + "\\" + Application.ActiveDocument.Title + ".xls", FileMode.Create, FileAccess.Write))
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
                        MessageBox.Show("unable to create report. Check whether the file is open or not");
                    }
                }
            }
        }

  

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);

        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
            btnMax.Visibility = Visibility.Hidden;
            btnRestore.Visibility = Visibility.Visible;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Hidden;
        }

        private void rdBtnOther_Checked(object sender, RoutedEventArgs e)
        {
            listBox_categories.ItemsSource = null;
            txtcategoryName.IsEnabled = true;
            txtBlockCategoryName.IsEnabled = true;
        }

        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.Key == Key.Enter)
            {
               
                CategoryName = txtcategoryName.Text;
                try
                {
                    UtilityClass.ParameterNames(CategoryName, doc, cmbPropertyName);
                }
                catch (Exception ex)
                {

                  
                }               
            }
        }
     
        private void FillPropertyName(string CategoryName)
        {
            cmbPropertyName.Items.Clear();
            Search search = new Search();
            SearchCondition sc1 = SearchCondition.HasCategoryByDisplayName(CategoryName);
            search.SearchConditions.Add(sc1);
            search.Selection.SelectAll();
            search.Locations = SearchLocations.DescendantsAndSelf;
            ModelItem modelItems = search.FindFirst(this.doc, true);
            var category = modelItems.PropertyCategories.Where(x => x.DisplayName == CategoryName).FirstOrDefault();
            cmbPropertyName.ItemsSource = (category.Properties.Cast<Object>().ToArray());
            cmbPropertyName.DisplayMemberPath = "DisplayName";
        }

    
        private void rdBtnOther_Unchecked(object sender, RoutedEventArgs e)
        {
            txtcategoryName.IsEnabled = false;
            txtBlockCategoryName.IsEnabled = false;
        }

        private void cmbPropertyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryName = txtcategoryName.Text;
            PropertyName= ((DataProperty)(cmbPropertyName.SelectedItem)).DisplayName;
            List<string> propertyvalues = UtilityClass.ParameterValue(CategoryName, PropertyName);
            listBox_categories.ItemsSource = null;
            listBox_categories.ItemsSource = propertyvalues;
        }

        private void txtPropertyName_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
