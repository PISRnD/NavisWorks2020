using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WIN = System.Windows;
using System.Windows.Controls;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Api;
using System.Data.SqlClient;
using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModule;

namespace ClashClusters
{
    /// <summary>
    /// Interaction logic for GroupClashesInterface.xaml
    /// </summary>
    public partial class GroupClashesInterface : UserControl
    {
        public ObservableCollection<CustomClashTest> ClashTests { get; set; }
        public ObservableCollection<GroupingMode> GroupByList { get; set; }
        public ObservableCollection<GroupingMode> GroupThenList { get; set; }
        public ClashTest SelectedClashTest { get; set; }
        public static int sec = 0, count;
        public static DateTime dtS;
        public static DateTime dtE;
        public static string _FileNm = string.Empty;
        public static bool isOfflineTool = false;
        public static string constr = "Data Source=10.1.2.47;Initial Catalog=AppDatabase;User ID=PIHD;pwd=p!$$@cle2017"; //for durgapur
        Autodesk.Navisworks.Api.Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        public GroupClashesInterface()
        {
            InitializeComponent();

            ClashTests = new ObservableCollection<CustomClashTest>();
            GroupByList = new ObservableCollection<GroupingMode>();
            GroupThenList = new ObservableCollection<GroupingMode>();
            RegisterChanges();

            this.DataContext = this;
        }

        private void Group_Button_Click(object sender, WIN.RoutedEventArgs e)
        {
            _FileNm = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentFileName;
            DatabaseManager.SetTime();
            if (ClashTestListBox.SelectedItems.Count != 0)
            {
                //Unsubscribe temporarly
                UnRegisterChanges();

                foreach (object selectedItem in ClashTestListBox.SelectedItems)
                {
                    CustomClashTest selectedClashTest = (CustomClashTest)selectedItem;
                    ClashTest clashTest = selectedClashTest.ClashTest;
                    if (count==0)
                    {
                        count = clashTest.Children.Count; 
                    }
                    if (clashTest.Children.Count != 0)
                    {
                        //Some selection check
                        if (comboBoxGroupBy.SelectedItem == null) comboBoxGroupBy.SelectedItem = GroupingMode.None;
                        if (comboBoxThenBy.SelectedItem == null) comboBoxThenBy.SelectedItem = GroupingMode.None;

                            if ((GroupingMode)comboBoxThenBy.SelectedItem != GroupingMode.None
                        || (GroupingMode)comboBoxGroupBy.SelectedItem != GroupingMode.None)
                            {

                                if ((GroupingMode)comboBoxThenBy.SelectedItem == GroupingMode.None
                                    && (GroupingMode)comboBoxGroupBy.SelectedItem != GroupingMode.None)
                                {
                                    GroupingMode mode = (GroupingMode)comboBoxGroupBy.SelectedItem;
                                    ClusteringFunctions.GroupClashes(clashTest, mode, GroupingMode.None, (bool)keepExistingGroupsCheckBox.IsChecked, false);
                                }
                                else if ((GroupingMode)comboBoxGroupBy.SelectedItem == GroupingMode.None
                                    && (GroupingMode)comboBoxThenBy.SelectedItem != GroupingMode.None)
                                {
                                    GroupingMode mode = (GroupingMode)comboBoxThenBy.SelectedItem;
                                    ClusteringFunctions.GroupClashes(clashTest, mode, GroupingMode.None, (bool)keepExistingGroupsCheckBox.IsChecked, false);
                                }
                                else
                                {
                                    GroupingMode byMode = (GroupingMode)comboBoxGroupBy.SelectedItem;
                                    GroupingMode thenByMode = (GroupingMode)comboBoxThenBy.SelectedItem;
                                    ClusteringFunctions.GroupClashes(clashTest, byMode, thenByMode, (bool)keepExistingGroupsCheckBox.IsChecked, false);
                                }
                            }
                        #region
                        //Modification for count wise - Not finalize
                        //if (Chk_GrpByClash.IsChecked==false)
                        //{
                        //    if ((GroupingMode)comboBoxThenBy.SelectedItem != GroupingMode.None
                        //|| (GroupingMode)comboBoxGroupBy.SelectedItem != GroupingMode.None)
                        //    {

                        //        if ((GroupingMode)comboBoxThenBy.SelectedItem == GroupingMode.None
                        //            && (GroupingMode)comboBoxGroupBy.SelectedItem != GroupingMode.None)
                        //        {
                        //            GroupingMode mode = (GroupingMode)comboBoxGroupBy.SelectedItem;
                        //            ClusteringFunctions.GroupClashes(clashTest, mode, GroupingMode.None, (bool)keepExistingGroupsCheckBox.IsChecked, (bool)Chk_GrpByClash.IsChecked);
                        //        }
                        //        else if ((GroupingMode)comboBoxGroupBy.SelectedItem == GroupingMode.None
                        //            && (GroupingMode)comboBoxThenBy.SelectedItem != GroupingMode.None)
                        //        {
                        //            GroupingMode mode = (GroupingMode)comboBoxThenBy.SelectedItem;
                        //            ClusteringFunctions.GroupClashes(clashTest, mode, GroupingMode.None, (bool)keepExistingGroupsCheckBox.IsChecked, (bool)Chk_GrpByClash.IsChecked);
                        //        }
                        //        else
                        //        {
                        //            GroupingMode byMode = (GroupingMode)comboBoxGroupBy.SelectedItem;
                        //            GroupingMode thenByMode = (GroupingMode)comboBoxThenBy.SelectedItem;
                        //            ClusteringFunctions.GroupClashes(clashTest, byMode, thenByMode, (bool)keepExistingGroupsCheckBox.IsChecked, (bool)Chk_GrpByClash.IsChecked);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    ClusteringFunctions.GroupClashes(clashTest, GroupingMode.None, GroupingMode.None, false, (bool)Chk_GrpByClash.IsChecked);
                        //}
                        #endregion

                    }
                }
                RegisterChanges();
                if (count>0)
                {
                    ToolSupport.InsertUsage(oDoc, count); 
                }               
                count = 0;
            }
        }

        private void Ungroup_Button_Click(object sender, WIN.RoutedEventArgs e)
        {
            _FileNm = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentFileName;
            DatabaseManager.SetTime();
            if (ClashTestListBox.SelectedItems.Count != 0)
            {
                //Unsubscribe temporarly
                UnRegisterChanges();
                foreach (object selectedItem in ClashTestListBox.SelectedItems)
                {
                    CustomClashTest selectedClashTest = (CustomClashTest)selectedItem;
                    ClashTest clashTest = selectedClashTest.ClashTest;
                    if (count == 0)
                    {
                        count = clashTest.Children.Count;
                    }
                    if (clashTest.Children.Count != 0)
                    {
                        ClusteringFunctions.UnGroupClashes(clashTest);
                    }
                }               
                RegisterChanges();
                ToolSupport.InsertUsage(oDoc, count);                
                count = 0;
            }
        }

        private void RegisterChanges()
        {
            //When the document change
            Application.MainDocument.Database.Changed += DocumentClashTests_Changed;

            //When a clash test change
            DocumentClashTests dct = Application.MainDocument.GetClash().TestsData;
            //Register
            dct.Changed += DocumentClashTests_Changed;

            //Get all clash tests and check up to date
            GetClashTests();
                CheckPlugin();
                LoadComboBox(); 
        }

        private void UnRegisterChanges()
        {
            //When the document change
            Application.MainDocument.Database.Changed -= DocumentClashTests_Changed;

            //When a clash test change
            DocumentClashTests dct = Application.MainDocument.GetClash().TestsData;
            //Register
            dct.Changed -= DocumentClashTests_Changed;
        }

        void DocumentClashTests_Changed(object sender, EventArgs e)
        {
            GetClashTests();
            CheckPlugin();
            LoadComboBox();

        }

        private void GetClashTests()
        {
            DocumentClashTests dct = Application.MainDocument.GetClash().TestsData;
            ClashTests.Clear();

            foreach (SavedItem savedItem in dct.Tests)
            {
                if (savedItem.GetType() == typeof(ClashTest))
                {
                    ClashTests.Add(new CustomClashTest(savedItem as ClashTest));
                }
            }
        }

        private void CheckPlugin()
        {
            //Inactive if there is no document open or there are no clash tests
            if (Application.MainDocument == null
                || Application.MainDocument.IsClear
                || Application.MainDocument.GetClash() == null
                || Application.MainDocument.GetClash().TestsData.Tests.Count == 0)
            {
                Group_Button.IsEnabled = false;
                comboBoxGroupBy.IsEnabled = false;
                comboBoxThenBy.IsEnabled = false;
                Ungroup_Button.IsEnabled = false;
            }
            else
            {
                Group_Button.IsEnabled = true;
                comboBoxGroupBy.IsEnabled = true;
                comboBoxThenBy.IsEnabled = true;
                Ungroup_Button.IsEnabled = true;
            }
        }

        private void LoadComboBox()
        {
            GroupByList.Clear();
            GroupThenList.Clear();

            foreach (GroupingMode mode in Enum.GetValues(typeof(GroupingMode)).Cast<GroupingMode>())
            {
                GroupThenList.Add(mode);
                GroupByList.Add(mode);
            }

            if (Application.MainDocument.Grids.ActiveSystem == null)
            {
                GroupByList.Remove(GroupingMode.GridIntersection);
                GroupByList.Remove(GroupingMode.Level);
                GroupThenList.Remove(GroupingMode.GridIntersection);
                GroupThenList.Remove(GroupingMode.Level);
            }

            comboBoxGroupBy.SelectedIndex = 0;
            comboBoxThenBy.SelectedIndex = 0;
        }

        public bool InsertUsages(string AddIn_Name, string DTPName, string DomainName, long NoOfElementProcess, long TimeTaken, string version, string filename)
        {
            if (isOfflineTool)
            {
                //System.IO.File.WriteAllText("data.txt", AddIn_Name + "," + DTPName + "," + DomainName + "," + NoOfElementProcess + "," + TimeTaken + "," + version + "," + filename + "," + DateTime.Now);
                return true;
            }
            else
            {
                bool b = true;
                try
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand command = new SqlCommand("Sp_InsertUsagesTracking", con);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@AddIn_Name", AddIn_Name));
                        command.Parameters.Add(new SqlParameter("@DTPName", DTPName));
                        command.Parameters.Add(new SqlParameter("@DomainName", DomainName));
                        command.Parameters.Add(new SqlParameter("@NoOfElementProcess", NoOfElementProcess));
                        command.Parameters.Add(new SqlParameter("@TimeTaken", TimeTaken));
                        command.Parameters.Add(new SqlParameter("@version", version));
                        command.Parameters.Add(new SqlParameter("@Filename", filename));
                        command.Parameters.Add(new SqlParameter("@Project", "29072019"));
                        // command.Parameters.Add(new SqlParameter("@EmpID", AppVariables.EmpID));
                        command.ExecuteNonQuery();

                    }
                }
                catch
                {
                    b = false;
                }
                //System.IO.File.WriteAllText(AppVariables.DPath + @"\HidePanel.txt", "Show");
                return b;
            }
        }

        private void Chk_GrpByClash_Checked(object sender, WIN.RoutedEventArgs e)
        {
            comboBoxGroupBy.IsEnabled = false;
            comboBoxThenBy.IsEnabled = false;
            keepExistingGroupsCheckBox.IsChecked = false;
            keepExistingGroupsCheckBox.IsEnabled = false;
        }

        private void Chk_GrpByClash_Unchecked(object sender, WIN.RoutedEventArgs e)
        {
            comboBoxGroupBy.IsEnabled = true;
            comboBoxThenBy.IsEnabled = true;
            keepExistingGroupsCheckBox.IsChecked = false;
            keepExistingGroupsCheckBox.IsEnabled = true;
        }
    }

    public class CustomClashTest
    {
        public CustomClashTest(ClashTest test)
        {
            _clashTest = test;
        }

        public string DisplayName { get { return _clashTest.DisplayName; } }

        private ClashTest _clashTest;
        public ClashTest ClashTest { get { return _clashTest; } }

        public string SelectionAName
        {
            get { return GetSelectedItem(_clashTest.SelectionA); }
        }

        public string SelectionBName
        {
            get { return GetSelectedItem(_clashTest.SelectionB); }
        }

        private string GetSelectedItem(ClashSelection selection)
        {
            string result = "";
            if (selection.Selection.HasSelectionSources)
            {
                result = selection.Selection.SelectionSources.FirstOrDefault().ToString();
                if (result.Contains("lcop_selection_set_tree\\"))
                {
                    result = result.Replace("lcop_selection_set_tree\\", "");
                }

                if (selection.Selection.SelectionSources.Count > 1)
                {
                    result = result + " (and other selection sets)";
                }

            }
            else if (selection.Selection.GetSelectedItems().Count == 0)
            {
                result = "No item have been selected.";
            }
            else if (selection.Selection.GetSelectedItems().Count == 1)
            {
                result = selection.Selection.GetSelectedItems().FirstOrDefault().DisplayName;
            }
            else
            {
                result = selection.Selection.GetSelectedItems().FirstOrDefault().DisplayName;
                foreach (ModelItem item in selection.Selection.GetSelectedItems().Skip(1))
                {
                    result = result + "; " + item.DisplayName;
                }
            }

            return result;
        }

    }
}
