using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using MoreLinq;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.Model;
using Utility.Module;

namespace ClashOptimiser
{
    public class UIOperationClassOptimiser
    {
        #region Variable Declaration
        public DocumentClashTests DocumentCLashTest { get; set; }
        public DocumentClash DocumentClash { get; set; }
        public double TolerenceValue { get; set; }
        public int TotalElementProcessed { get; set; }
        public int GroupCount { get; set; }
        public string CategoryName { get; set; }
        public List<ClashItemInformation> ClashItemInformationList { get; set; }
        public List<List<SavedItem>> GroupSavedItemList {get; set;}
        public ClashOptimiserUI ClashOptimiserUIObj { get; set; }
        public List<SavedItem> SavedItemLst { get; set; }
        public List<SavedItem> SavedItemLst1 { get; set; }
        #endregion

        /// <summary>
        /// This is the constructor class of UIOperationClassOptimiser
        /// </summary>
        /// <param name="clashOptimiserUI">This is the argument of ClashOptimiserUI</param>
        public UIOperationClassOptimiser(ClashOptimiserUI clashOptimiserUI)
        {
            ClashOptimiserUIObj = clashOptimiserUI;
            TolerenceValue = 0;
            TotalElementProcessed = 0;
            GroupCount = 0;
            ClashItemInformationList = new List<ClashItemInformation>();
            GroupSavedItemList = new List<List<SavedItem>>();
            SavedItemLst = new List<SavedItem>();
            SavedItemLst1 = new List<SavedItem>();
        }

         /// <summary>
     /// This method is to get the count of the groupItem as per the ClashResultStatus such
     /// as Approved, Reviewed
     /// </summary>
     /// <param name="group">This is the GroupItem</param>
     /// <param name="clashResultStatus"></param>
     /// <returns>This returns the number of count</returns>
        public int GetCount(GroupItem group, ClashResultStatus clashResultStatus)
        {
            int count = 0;
            foreach (SavedItem childSavedItem in group.Children)
            {
                /* If we only wanted to access first-level children
                 * without reference to whether they were groups or results
                 * then we could:
                 *
                 * // access groups and results via shared interface
                 * IClashResult result = child as IClashResult;
                 * 
                 * operate on that and not recurse further. */

                // GroupItem is the base-class of ClashResultGroup which defines
                // group-like behaviour, if we needed to access ay ClashResultGroup properties
                // we could cast to that equivalently... 
                GroupItem childGroupItem = childSavedItem as GroupItem;
                // is this a group?
                if (childGroupItem != null)
                {
                    // operate on the group's children
                    //GetCount(child_group, rs);
                    ClashResultGroup clashResultGroup = childGroupItem as ClashResultGroup;
                    ////Added by Preeti
                    if (clashResultGroup.Status == clashResultStatus)
                        count++;
                }
                else
                {
                    // Not a group, so must be a result.
                    ClashResult clashResult = childSavedItem as ClashResult;
                    if (clashResult.Status == clashResultStatus)
                        count++;
                    // act on result...
                }
            }
            return count;
        }

        /// <summary>
        /// This method is to update the clash test in the dataClashGrid
        /// </summary>
        /// <param name="clashTest">ClashTest</param>
        /// <param name="dataGridViewRow">DataGridViewRow</param>
        public void UpdateTest(ClashTest clashTest, DataGridViewRow dataGridViewRow)
        {
            int activeNew = GetCount(clashTest, ClashResultStatus.Active) + GetCount(clashTest, ClashResultStatus.New);
            int reviewed = GetCount(clashTest, ClashResultStatus.Reviewed);
            int approved = GetCount(clashTest, ClashResultStatus.Approved);
            int index = ClashOptimiserUIObj.dataClashGrid.Rows.IndexOf(dataGridViewRow);
            ClashOptimiserUIObj.dataClashGrid.Rows[index].Cells["colNwAc"].Value = activeNew.ToString();
            ClashOptimiserUIObj.dataClashGrid.Rows[index].Cells["colReviewed"].Value = reviewed.ToString();
            ClashOptimiserUIObj.dataClashGrid.Rows[index].Cells["colApproved"].Value = approved.ToString();
            ClashOptimiserUIObj.dataClashGrid.Rows[index].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.right-sm.png");
            ClashOptimiserUIObj.dataClashGrid.Rows[index].Cells["colSort"].Value = (activeNew + reviewed + approved);
            ClashOptimiserUIObj.dataClashGrid.Sort(ClashOptimiserUIObj.dataClashGrid.Columns["colSort"], ListSortDirection.Descending);
            ClashOptimiserUIObj.dataClashGrid.Refresh();
            ClashOptimiserUIObj.dataClashGrid.AllowUserToResizeColumns = true;
        }

        /// <summary>
        /// This method is to load the test in the datagrid from the naviswors file clash tests
        /// </summary>
        /// <param name="filename"></param>
        public void LoadTest(string filename)
        {
           DocumentClash = Autodesk.Navisworks.Api.Application.Documents.Where(x => x.Title == filename).FirstOrDefault().GetClash();//Autodesk.Navisworks.Api.Application.ActiveDocument.GetClash();
            DocumentCLashTest = DocumentClash.TestsData;
            ClashOptimiserUIObj.dataClashGrid.Rows.Clear();
            foreach (ClashTest clashTest in DocumentCLashTest.Tests)
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();                
                int activeNewClashTest = GetCount(clashTest, ClashResultStatus.Active) + GetCount(clashTest, ClashResultStatus.New);
                int reviewedClashTest = GetCount(clashTest, ClashResultStatus.Reviewed);
                int approvedClashTest = GetCount(clashTest, ClashResultStatus.Approved);
                ClashOptimiserUIObj.dataClashGrid.Rows.Add(GetImage("ClashOptimiser.Images.close-sm.png"), clashTest.DisplayName, activeNewClashTest.ToString(), reviewedClashTest.ToString(), approvedClashTest.ToString(), (activeNewClashTest + reviewedClashTest + approvedClashTest));
            }
            (ClashOptimiserUIObj.dataClashGrid.Columns["colImg"] as DataGridViewImageColumn).Image = GetImage("ClashOptimiser.Images.close-sm.png");
            ClashOptimiserUIObj.dataClashGrid.Sort(ClashOptimiserUIObj.dataClashGrid.Columns["colSort"], ListSortDirection.Descending);
            ClashOptimiserUIObj.dataClashGrid.Refresh();
            ClashOptimiserUIObj.dataClashGrid.AllowUserToResizeColumns = true;
        }
      
        /// <summary>
        /// This method is to compare the bounding box
        /// </summary>
        /// <param name="boundingBox3DFirst">First Boundingbox</param>
        /// <param name="boundingBox3DSecond">Second Boundingbox</param>
        /// <returns>returns true, if both the bounding boxes are same, else return false</returns>
        public bool CompareBoundingBox(BoundingBox3D boundingBox3DFirst, BoundingBox3D boundingBox3DSecond)
        {
            double bbMinXres = Math.Abs(boundingBox3DFirst.Min.X - boundingBox3DSecond.Min.X);
            double bbMinYres = Math.Abs(boundingBox3DFirst.Min.Y - boundingBox3DSecond.Min.Y);
            double bbMinZres = Math.Abs(boundingBox3DFirst.Min.Z - boundingBox3DSecond.Min.Z);
            double bbMaxXres = Math.Abs(boundingBox3DFirst.Max.X - boundingBox3DSecond.Max.X);
            double bbMaxYres = Math.Abs(boundingBox3DFirst.Max.Y - boundingBox3DSecond.Max.Y);
            double bbMaxZres = Math.Abs(boundingBox3DFirst.Max.Z - boundingBox3DSecond.Max.Z);
            if (bbMinXres <= TolerenceValue && bbMinYres <= TolerenceValue && bbMinZres <= TolerenceValue && bbMaxXres <= TolerenceValue && bbMaxYres <= TolerenceValue && bbMaxZres <= TolerenceValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method is to compare the bounding boxes and returns the the list of saved items 
        /// which are having same bounding box
        /// </summary>
        /// <param name="boundingBox3D"></param>
        /// <param name="savedItems"></param>
        /// <returns>returns the List of Saved Item whose bounding box are same</returns>
        public List<SavedItem> CompareBB(BoundingBox3D boundingBox3D, List<SavedItem> savedItems)
        {
            return savedItems.Where(x => CompareBoundingBox(boundingBox3D, (x as ClashResult).BoundingBox)).ToList();
        }

        /// <summary>
        /// This method is to create the group as per the Name radiobutton checked nad user defined
        /// condition for Tab Name and property Name
        /// </summary>
        /// <param name="clashTest">ClashTest</param>
        /// <param name="savedItemList">List of the Saved Item</param>
        /// <param name="count">Count</param>
        /// <param name="GroupName">GroupName</param>
        public void CreateNewGroup(ClashTest clashTest, List<SavedItem> savedItemList, int count, string GroupName)
        {
            try
            {
                int countProcessedItems = 0;
                ClashResultGroup clashResultGroup = new ClashResultGroup();
                int groupIndex = clashTest.Children.IndexOfDisplayName(GroupName);
                if (-1 == groupIndex)
                {
                    ClashResultGroup newClashResultGroup = new ClashResultGroup();
                    newClashResultGroup.DisplayName = GroupName;
                    DocumentCLashTest.TestsInsertCopy(clashTest, 0, newClashResultGroup);
                    newClashResultGroup = (ClashResultGroup)clashTest.Children[0];
                    clashResultGroup = newClashResultGroup;
                }
                else
                {
                    clashResultGroup = (ClashResultGroup)clashTest.Children[groupIndex];
                }           
                savedItemList.ForEach(savedItemListFirst =>
                {
                    try
                    {
                        ClashResult clashResult = savedItemListFirst as ClashResult;
                        DocumentCLashTest.TestsMove(clashTest, clashTest.Children.IndexOf(clashResult), clashResultGroup, 0);
                        countProcessedItems++;
                    }
                    catch (Exception ex)
                    {
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                });
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This method is to create group by bounding box concept
        /// </summary>
        /// <param name="clashTest">ClashTest</param>
        /// <param name="savedItemList">List of Saved Item</param>
        /// <param name="count">count of the items processed</param>
        public void CreateGroup(ClashTest clashTest, List<SavedItem> savedItemList, int count)
        {
            try
            {
                ClashResultGroup clashResultGroup = new ClashResultGroup();
                int groupIndex = clashTest.Children.IndexOfDisplayName(string.Format("{0}({1} : {2})", "Group" + count.ToString(), "Number of Clashes", savedItemList.Count()));
                if (-1 == groupIndex)
                {
                    ClashResultGroup newClashResultGroup = new ClashResultGroup();
                    newClashResultGroup.DisplayName = string.Format("{0}({1} = {2})", "Group" + count.ToString(), "Number of Clashes", savedItemList.Count());
                    DocumentCLashTest.TestsInsertCopy(clashTest, 0, newClashResultGroup);
                    clashResultGroup = newClashResultGroup;
                    clashResultGroup = (ClashResultGroup)clashTest.Children[0];
                }
                else
                {
                    clashResultGroup = (ClashResultGroup)clashTest.Children[groupIndex];
                }
                savedItemList.ForEach(savedItem =>
                {
                    try
                    {
                        ClashResult clashResult = savedItem as ClashResult;
                        DocumentCLashTest.TestsMove(clashTest, clashTest.Children.IndexOf(clashResult), clashResultGroup, 0);
                    }
                    catch (Exception ex)
                    {
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                });
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This method is to create the group by category as per the user input for the defined
        /// TabName, PropertyName
        /// </summary>
        /// <param name="clashTest">ClashTest</param>
        /// <param name="savedItemList">List of the Saved Item</param>
        /// <param name="count">Count of the pocessed saved item</param>
        /// <param name="groupName">Group Name</param>
        public void CreateGroupByCategory(ClashTest clashTest, List<SavedItem> savedItemList, int count, string groupName)
        {
            try
            {
                ClashResultGroup clashResultGroup = new ClashResultGroup();
                int groupIndex = clashTest.Children.IndexOfDisplayName(string.Format("{0}({1} : {2})", groupName, "No. of clashes", savedItemList.Count()));
                if (-1 == groupIndex)
                {
                    ClashResultGroup newClashResultGroup = new ClashResultGroup();
                    newClashResultGroup.DisplayName = string.Format("{0}({1} : {2})", groupName, "No. of clashes", savedItemList.Count());
                    DocumentCLashTest.TestsInsertCopy(clashTest, 0, newClashResultGroup);
                    clashResultGroup = (ClashResultGroup)clashTest.Children[0];
                }
                else
                {
                    clashResultGroup = (ClashResultGroup)clashTest.Children[groupIndex];
                }
                //Commented by Preeti on 01-04-22
                savedItemList.ForEach(savedItem =>
                {
                    try
                    {
                        ClashResult clashResult = savedItem as ClashResult;
                        DocumentCLashTest.TestsMove(clashTest, clashTest.Children.IndexOf(clashResult), clashResultGroup, 0);
                        // Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                });
                //Commented by Preeti on 01-04-22
                savedItemList.ForEach(savedItem =>
                {
                    List<SavedItem> childSavedItem = new List<SavedItem>();
                    if (ClashOptimiserUIObj.chkIgnoreApproved.Checked == true && ClashOptimiserUIObj.chkIgnoreReviewed.Checked == true)
                    {
                        childSavedItem = clashTest.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(savedItemList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (savedItem as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (savedItem as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (savedItem as ClashResult).ViewBounds.Min.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (savedItem as ClashResult).ViewBounds.Min.Y) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (savedItem as ClashResult).ViewBounds.Max.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (savedItem as ClashResult).ViewBounds.Max.Y) <= TolerenceValue)).ToList();
                    }
                    else if (ClashOptimiserUIObj.chkIgnoreApproved.Checked == true)
                    {
                        childSavedItem = clashTest.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(savedItemList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (savedItem as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (savedItem as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (savedItem as ClashResult).ViewBounds.Min.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (savedItem as ClashResult).ViewBounds.Min.Y) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (savedItem as ClashResult).ViewBounds.Max.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (savedItem as ClashResult).ViewBounds.Max.Y) <= TolerenceValue)).ToList();
                    }
                    else if (ClashOptimiserUIObj.chkIgnoreReviewed.Checked == true)
                    {
                        childSavedItem = clashTest.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(savedItemList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (savedItem as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (savedItem as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (savedItem as ClashResult).ViewBounds.Min.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (savedItem as ClashResult).ViewBounds.Min.Y) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (savedItem as ClashResult).ViewBounds.Max.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (savedItem as ClashResult).ViewBounds.Max.Y) <= TolerenceValue)).ToList();
                    }
                    else
                    {
                        childSavedItem = clashTest.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(savedItemList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (savedItem as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (savedItem as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (savedItem as ClashResult).ViewBounds.Min.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (savedItem as ClashResult).ViewBounds.Min.Y) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (savedItem as ClashResult).ViewBounds.Max.X) <= TolerenceValue) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (savedItem as ClashResult).ViewBounds.Max.Y) <= TolerenceValue)).ToList();
                    }
                    //Commented by Preeti on 01-04-22
                    childSavedItem.ForEach((childItem) =>
                    {
                        DocumentCLashTest.TestsMove(clashTest, clashTest.Children.IndexOf(childItem), clashResultGroup, 0);
                        SavedItemLst.Remove(childItem);
                        SavedItemLst1.Remove(childItem);
                    });
                    //Commented by Preeti on 01-04-22
                });              
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This method is to filter the group
        /// </summary>
        /// <param name="groupRefined">Refined Group</param>
        /// <param name="ListFromFilter">List from filter</param>
        /// <returns>returns the list of the Saved Item</returns>
        public List<SavedItem> RefinedGroup(List<SavedItem> groupRefined, List<SavedItem> ListFromFilter)
        {
            List<SavedItem> tempSavedItemList = new List<SavedItem>();
            groupRefined.ToList().ForEach((groupRefinedItem) =>
            {
                try
                {
                    ClashResult clashResult = (groupRefinedItem as ClashResult);
                    ModelItem frItm1 = clashResult.Item1;
                    ModelItem frItm2 = clashResult.Item2;
                    ListFromFilter.ToList().ForEach((filterItem) =>
                    {
                        ClashResult to = (filterItem as ClashResult);
                        ModelItem toItm1 = to.Item1;
                        ModelItem toItm2 = to.Item2;
                    });
                    tempSavedItemList.AddRange(ListFromFilter.Where(y => ((y as ClashResult).Item1.IsSameInstance((groupRefinedItem as ClashResult).Item1)) == true || ((y as ClashResult).Item1.IsSameInstance((groupRefinedItem as ClashResult).Item2) == true) || ((y as ClashResult).Item2.IsSameInstance((groupRefinedItem as ClashResult).Item1) == true) || ((y as ClashResult).Item2.IsSameInstance((groupRefinedItem as ClashResult).Item2) == true)).ToList());
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            });
            return tempSavedItemList;
        }
      
        /// <summary>
        /// This method is to optimise the clash by adding the duplicate in the same group
        /// </summary>
        /// <param name="clashTest">Clash Test</param>
        /// <param name="documentClashTest">DocumentClashTest</param>
        /// <param name="dataGridViewRow">DataGridViewRow</param>
        public void OptimiseClash(ClashTest clashTest, DocumentClashTests documentClashTest, DataGridViewRow dataGridViewRow)
        {
            ClashTest clashTestFirst = clashTest;// oDCT.Tests[1] as ClashTest;
            SavedItemLst.Clear();
            SavedItemLst1.Clear();
            List<SavedItem> childItem = new List<SavedItem>();
            if (ClashOptimiserUIObj.chkIgnoreApproved.Checked == true && ClashOptimiserUIObj.chkIgnoreReviewed.Checked == true)
            {
                childItem = clashTestFirst.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }
            else if (ClashOptimiserUIObj.chkIgnoreApproved.Checked == true)
            {
                childItem = clashTestFirst.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }
            else if (ClashOptimiserUIObj.chkIgnoreReviewed.Checked == true)
            {
                childItem = clashTestFirst.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }
            else
            {
                childItem = clashTestFirst.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }          
            SavedItemLst.AddRange(childItem);
            SavedItemLst1.AddRange(childItem);
            int total = SavedItemLst1.Count;
            TotalElementProcessed += total;
            GroupCount = 0;
            if (ClashOptimiserUIObj.rdBtnBoundingBox.Checked == true)
            {
                SavedItemLst.ToList().ForEach((savedItemOne) =>
                {
                  List<SavedItem> savedItemList = SavedItemLst1.Where(y => (Math.Abs((savedItemOne as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((savedItemOne as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((savedItemOne as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= TolerenceValue) && (Math.Abs((savedItemOne as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= TolerenceValue) && (Math.Abs((savedItemOne as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= TolerenceValue) && (Math.Abs((savedItemOne as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= TolerenceValue)).ToList();                    
                    if (savedItemList.Count > 1)
                    {
                        GroupCount += 1;
                        CreateGroup(clashTestFirst, savedItemList, GroupCount);
                        SavedItemLst1 = SavedItemLst1.Except(savedItemList).ToList();
                        SavedItemLst = SavedItemLst.Except(savedItemList).ToList();
                    }
                    int step = Convert.ToInt32(((SavedItemLst1.Count * 100) / total));
                    ClashOptimiserUIObj.progressBar1.Value = 100 - step;
                });
            }
            else if (ClashOptimiserUIObj.rdBtnCategory.Checked == true)
            {
                if (ClashOptimiserUIObj.txtPropertyValue.Text.Length > 0)
                {
                    ElementDetails.PropertyValue = ClashOptimiserUIObj.txtPropertyValue.Text;
                }
                else
                {
                    ElementDetails.PropertyValue = ClashOptimiserUIObj.cmbPropertyValue.SelectedItem.ToString();
                }
                List<ModelItem> itemListOne = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item1).ToList();
                List<ModelItem> itemListTwo = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item2).ToList();
                for (int i = 0; i < itemListOne.Count(); i++)
                {
                    try
                    {
                        string propertyValue1 = itemListOne[i].PropertyCategories.Cast<PropertyCategory>().Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
                                       Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault()?.Value.ToDisplayString();
                        string propertyValue2 = itemListTwo[i].PropertyCategories.Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
                        Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault()?.Value.ToDisplayString();
                        string TempGroup = string.Empty;
                        string TempReverseGroup = string.Empty;
                        string ExistingGroup = string.Empty;
                        if (propertyValue1 == ElementDetails.PropertyValue || propertyValue2 == ElementDetails.PropertyValue)
                        {
                            TempGroup = string.Format("{0} vs {1}", propertyValue1, propertyValue2);
                            TempReverseGroup = string.Format("{0} vs {1}", propertyValue2, propertyValue1);
                            ExistingGroup = ClashItemInformationList.Where(x1 => x1.GroupName == TempGroup || x1.GroupName == TempReverseGroup).Select(x1 => x1.GroupName).FirstOrDefault();
                            if (ExistingGroup == null)
                            {
                                ClashItemInformationList.Add(new ClashItemInformation
                                {
                                    ModelItemOne = itemListOne[i],
                                    ModelItemTwo = itemListTwo[i],
                                    SavedModelItem = SavedItemLst1[i],
                                    PropertyValueOne = propertyValue1,
                                    PropertyValueTwo = propertyValue2,
                                    PropertyName = ElementDetails.PropertyName,
                                    CategoryName = CategoryName,
                                    GroupName = string.Format("{0} vs {1}", propertyValue1, propertyValue2),
                                    ReverseGroupName = string.Format("{0} vs {1}", propertyValue2, propertyValue1)
                                });
                            }
                            else
                            {
                                ClashItemInformationList.Add(new ClashItemInformation
                                {
                                    ModelItemOne = itemListOne[i],
                                    ModelItemTwo = itemListTwo[i],
                                    SavedModelItem = SavedItemLst1[i],
                                    PropertyValueOne = propertyValue1,
                                    PropertyValueTwo = propertyValue2,
                                    PropertyName = ElementDetails.PropertyName,
                                    CategoryName = CategoryName,
                                    GroupName = ExistingGroup,
                                    ReverseGroupName = (ExistingGroup == TempGroup) ? TempReverseGroup : TempGroup
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }

                }
                List<string> groupNameList = ClashItemInformationList.DistinctBy(x1 => x1.GroupName).Select(x1 => x1.GroupName).ToList();
                List<SavedItem> savedItems;
                groupNameList.ToList().ForEach((grpName) =>
                {
                    GroupCount += 1;
                    savedItems = new List<SavedItem>();
                    savedItems = ClashItemInformationList.Where(y => y.GroupName == grpName).Select(x => x.SavedModelItem).ToList();
                    grpName = String.Format("{0}:{1} = {2}", grpName, "Number of Clashes", savedItems.Count);
                    CreateNewGroup(clashTestFirst, savedItems, GroupCount, grpName);
                    SavedItemLst1 = SavedItemLst1.Except(savedItems).ToList();
                    SavedItemLst = SavedItemLst.Except(savedItems).ToList();
                });
            }
            ClashOptimiserUIObj.progressBar1.Value = 100;
            UpdateTest(clashTestFirst, dataGridViewRow);
        }

        /// <summary>
        /// This method is to change the data as per the TabName Selection Changed
        /// </summary>
        public void TabNameSelectedIndexChanged()
        {
            Utility.Model.ElementDetails.CategoryName = ClashOptimiserUIObj.cmbTabName.SelectedItem.ToString();
            UtilityClass.ParameterNamesWindowsForSelected(Utility.Model.ElementDetails.CategoryName, ClashOptimiserUIObj.cmbPropertyName, Utility.Model.ElementDetails.ModelItemCollection);

        }

        /// <summary>
        /// This method is to change the property name on the selcted index changed
        /// </summary>
        public void PropertyNameSelectedIndexChanged()
        {
            Utility.Model.ElementDetails.PropertyName = ClashOptimiserUIObj.cmbPropertyName.SelectedItem.ToString();
            PopulatePropertyValue();
        }

        /// <summary>
        /// This method is to populate the property value
        /// </summary>
        private void PopulatePropertyValue()
        {
            List<string> parameterValueList = UtilityClass.ParameterValue(Utility.Model.ElementDetails.CategoryName, Utility.Model.ElementDetails.PropertyName);
            ClashOptimiserUIObj.cmbPropertyValue.DataSource = null;
            ClashOptimiserUIObj.cmbPropertyValue.DataSource = parameterValueList;
        }

        /// <summary>
        /// This method do all the process as per the bounding box checked selection changed
        /// </summary>
        public void BoundingBoxCheckChanged()
        {
            if (ClashOptimiserUIObj.rdBtnBoundingBox.Checked == true)
            {
                ClashOptimiserUIObj.txtCategoryName.Enabled = false;
                ClashOptimiserUIObj.cmbPropertyName.Enabled = false;
                ClashOptimiserUIObj.cmbPropertyValue.Enabled = false;
                ClashOptimiserUIObj.lblParameterName.Enabled = false;
                ClashOptimiserUIObj.lblCategoryName.Enabled = false;
                ClashOptimiserUIObj.lblParameterValue.Enabled = false;
                ClashOptimiserUIObj.cmbTabName.Enabled = false;
                ClashOptimiserUIObj.txtPropertyName.Enabled = false;
                ClashOptimiserUIObj.txtPropertyValue.Enabled = false;

            }
            else
            {
                ClashOptimiserUIObj.txtCategoryName.Enabled = true;
                ClashOptimiserUIObj.cmbPropertyName.Enabled = true;
                ClashOptimiserUIObj.cmbPropertyValue.Enabled = true;
                ClashOptimiserUIObj.lblParameterName.Enabled = true;
                ClashOptimiserUIObj.lblCategoryName.Enabled = true;
                ClashOptimiserUIObj.lblParameterValue.Enabled = true;
                ClashOptimiserUIObj.cmbTabName.Enabled = true;
                ClashOptimiserUIObj.txtPropertyName.Enabled = true;
                ClashOptimiserUIObj. txtPropertyValue.Enabled = true;
            }
        }

        /// <summary>
        /// This method changes the CategoryName related controls update i.e. Property Name 
        /// in the key press
        /// </summary>
        /// <param name="e">KeyPressEventArgs</param>
        public void TextCategoryNameKeyPress(KeyPressEventArgs e)
        {
            Utility.Model.ElementDetails.CategoryName = ClashOptimiserUIObj.txtCategoryName.Text.Trim();
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                UtilityClass.ParameterNamesWindows(Utility.Model.ElementDetails.CategoryName, Utility.Model.ElementDetails.Document, ClashOptimiserUIObj.cmbPropertyName);
            }
        }

        /// <summary>
        /// This method populates the data on the Property Name on the key press
        /// </summary>
        public void TextPropertyNameKeyPress()
        {
            if (ClashOptimiserUIObj.txtPropertyName.TextLength > 0 && ClashOptimiserUIObj.cmbPropertyName.Enabled == true)
            {
                ClashOptimiserUIObj.cmbPropertyName.Enabled = false;
                Utility.Model.ElementDetails.PropertyName = ClashOptimiserUIObj.txtPropertyName.Text;
                PopulatePropertyValue();
            }
            else
            {
                ClashOptimiserUIObj.cmbPropertyName.Enabled = true;
            }
        }

        /// <summary>
        /// This method is to initialize all the variables on the form load
        /// </summary>
        public void Load()
        {
            ClashOptimiserUIObj.dataClashGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ClashOptimiserUIObj.dataClashGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ClashOptimiserUIObj.dataClashGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ClashOptimiserUIObj.dataClashGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ClashOptimiserUIObj.dataClashGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ClashOptimiserUIObj.lblCategoryName.Enabled = false;
            ClashOptimiserUIObj.lblParameterName.Enabled = false;
            ClashOptimiserUIObj.lblParameterValue.Enabled = false;
            ClashOptimiserUIObj.txtCategoryName.Enabled = false;
            ClashOptimiserUIObj.cmbPropertyName.Enabled = false;
            ClashOptimiserUIObj.cmbPropertyValue.Enabled = false;
            ClashOptimiserUIObj.cmbTabName.Enabled = false;
            ClashOptimiserUIObj.txtPropertyName.Enabled = false;
            ClashOptimiserUIObj.txtPropertyValue.Enabled = false;
            foreach (Document doc in Autodesk.Navisworks.Api.Application.Documents)
            {
                ClashOptimiserUIObj.lstAvailFiles.Items.Add(doc.Title);
            }
            if (ClashOptimiserUIObj.lstAvailFiles.Items.Count > 0)
                LoadTest(ClashOptimiserUIObj.lstAvailFiles.Items[0].ToString());
        }

        /// <summary>
        /// This method is to get the bitmap image
        /// </summary>
        /// <param name="imageinfo">Image Information</param>
        /// <returns>retrns the Bitmap</returns>
        public Bitmap GetImage(string imageinfo)
        {
            Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream(imageinfo);
            Bitmap image = new Bitmap(myStream);
            return image;
        }

        /// <summary>
        /// This method is to call the function to do the inital setting at the datgrid selection
        /// </summary>
        public void FuncCall()
        {
            DatabaseManager.SetTime();
            ClashOptimiserUIObj.progressBar1.Minimum = 0;
            ClashOptimiserUIObj.progressBar1.Maximum = 100;
            TolerenceValue = Convert.ToDouble(ClashOptimiserUIObj.trackTolerence.Value);
            ClashOptimiserUIObj.dataClashGrid.Enabled = false;
            ClashOptimiserUIObj.numTolerence.Enabled = false;
            ClashOptimiserUIObj.btnOptimise.Enabled = false;
            ClashOptimiserUIObj.btnReset.Enabled = false;
            ClashOptimiserUIObj.chkIgnoreApproved.Enabled = false;
            ClashOptimiserUIObj.chkIgnoreReviewed.Enabled = false;
            TotalElementProcessed = 0;
            foreach (DataGridViewRow row in ClashOptimiserUIObj.dataClashGrid.SelectedRows)
            {
                try
                {
                    ClashOptimiserUIObj.progressBar1.Value = 0;
                    string str = row.Cells["colClash"].Value.ToString();
                    ClashOptimiserUIObj.Invoke(new Action(() => ClashOptimiserUIObj.lblTest.Text = str));
                    SavedItem savedItem = DocumentCLashTest.Tests.Where(x => x.DisplayName == str).FirstOrDefault();
                    ClashOptimiserUIObj.lblTest.Text = str;
                    ClashOptimiserUIObj.dataClashGrid.Rows[ClashOptimiserUIObj.dataClashGrid.Rows.IndexOf(row)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    ClashOptimiserUIObj.dataClashGrid.Refresh();
                    ClashOptimiserUIObj.Refresh();
                    OptimiseClash(savedItem as ClashTest, DocumentCLashTest, row);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
                Thread.Sleep(1);
            }
            if (ClashOptimiserUIObj.chkArrange.Checked)
            {
                DocumentCLashTest.TestsSortTests(ClashTestSortMode.ResultsNumberSort, ClashSortDirection.SortDescending);
            }
            ClashOptimiserUIObj.dataClashGrid.Enabled = true;
            ClashOptimiserUIObj.numTolerence.Enabled = true;
            ClashOptimiserUIObj.btnOptimise.Enabled = true;
            ClashOptimiserUIObj.btnReset.Enabled = true;
            ClashOptimiserUIObj.chkIgnoreApproved.Enabled = true;
            ClashOptimiserUIObj.chkIgnoreReviewed.Enabled = true;
            string filname;

            if (TotalElementProcessed > 0)
            {
                if (ClashOptimiserUIObj.lstAvailFiles.SelectedIndex == -1)
                {
                    filname = ClashOptimiserUIObj.lstAvailFiles.Items[0].ToString();
                }
                else
                {
                    filname = ClashOptimiserUIObj.lstAvailFiles.Items[ClashOptimiserUIObj.lstAvailFiles.SelectedIndex].ToString();
                }
                ToolSupport.InsertUsage(Utility.Model.ElementDetails.Document, TotalElementProcessed);
            }
        }

        /// <summary>
        /// This method is to do the process by the Reset Button Click and it will ungroup all 
        /// the group clash test
        /// </summary>
        public void ResetButtonClick()
        {
            if (ClashOptimiserUIObj.rdBtnBoundingBox.Checked == true)
            {
                foreach (DataGridViewRow r in ClashOptimiserUIObj.dataClashGrid.SelectedRows)
                {
                    ClashTest clash = DocumentCLashTest.Tests.Where(x => x.DisplayName == r.Cells["colClash"].Value.ToString()).Cast<ClashTest>().FirstOrDefault();
                    int totalGroup = clash.Children.Where(x => x.DisplayName.Contains("Group") == true).Count();
                    List<SavedItem> saveitemLst = clash.Children.Where(x => x.DisplayName.Contains("Group") == true).Cast<SavedItem>().ToList();
                    ClashOptimiserUIObj.dataClashGrid.Rows[ClashOptimiserUIObj.dataClashGrid.Rows.IndexOf(r)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    ClashOptimiserUIObj.dataClashGrid.Refresh();
                    ClashOptimiserUIObj.Refresh();
                    int grpcnt = 0;
                    saveitemLst.ToList().ForEach((x) =>
                    {
                        string display = x.DisplayName;
                        if (x.DisplayName.Contains("Group") == true)
                        {
                            ClashResultGroup gr = x as ClashResultGroup;
                            gr.Children.ToList().ForEach((y) =>
                            {
                                DocumentCLashTest.TestsMove(gr, 0, clash, 0);
                            });

                            grpcnt++;
                            int step = Convert.ToInt32(((grpcnt * 100) / totalGroup));
                            ClashOptimiserUIObj.progressBar1.Value = step;
                        }
                        Thread.Sleep(1);
                    });
                    clash.Children.Where(x => x.DisplayName.Contains("Group") == true).ToList().ForEach((x) => DocumentCLashTest.TestsRemove(clash, x));
                    UpdateTest(clash, r);
                }
            }
            else if (ClashOptimiserUIObj.rdBtnCategory.Checked == true)
            {
                foreach (DataGridViewRow r in ClashOptimiserUIObj.dataClashGrid.SelectedRows)
                {
                    ClashTest clash = DocumentCLashTest.Tests.Where(x => x.DisplayName == r.Cells["colClash"].Value.ToString()).Cast<ClashTest>().FirstOrDefault();
                    int totalGroup = clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).Count();
                    List<SavedItem> saveitemLst = clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).Cast<SavedItem>().ToList();
                    ClashOptimiserUIObj.dataClashGrid.Rows[ClashOptimiserUIObj.dataClashGrid.Rows.IndexOf(r)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    ClashOptimiserUIObj.dataClashGrid.Refresh();
                    ClashOptimiserUIObj.Refresh();
                    int grpcnt = 0;
                    saveitemLst.ToList().ForEach((x) =>
                    {
                        string display = x.DisplayName;
                        if (x.DisplayName.Contains("vs") == true)
                        {
                            ClashResultGroup gr = x as ClashResultGroup;
                            gr.Children.ToList().ForEach((y) =>
                            {
                                DocumentCLashTest.TestsMove(gr, 0, clash, 0);
                            });
                            grpcnt++;
                            int step = Convert.ToInt32(((grpcnt * 100) / totalGroup));
                            ClashOptimiserUIObj.progressBar1.Value = step;
                        }
                        Thread.Sleep(1);
                    });
                    clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).ToList().ForEach((x) => DocumentCLashTest.TestsRemove(clash, x));
                    UpdateTest(clash, r);
                }
            }
        }

        /// <summary>
        /// This method is to avail the list by changing the selected index
        /// </summary>
        public void ListAvailSelectedIndexChanged()
        {
            LoadTest(ClashOptimiserUIObj.lstAvailFiles.Items[ClashOptimiserUIObj.lstAvailFiles.SelectedIndex].ToString());
        }

        /// <summary>
        /// This method is for the Category Check changed means when the category will be changed
        ///its corresponding data will be changes accordingly
        /// </summary>
        public void CategoryCheckChanged()
        {
            if (ClashOptimiserUIObj.rdBtnCategory.Checked == true)
            {
                ClashOptimiserUIObj. txtCategoryName.Enabled = true;
                ClashOptimiserUIObj.cmbPropertyName.Enabled = true;
                ClashOptimiserUIObj.cmbPropertyValue.Enabled = true;
                ClashOptimiserUIObj.lblParameterName.Enabled = true;
                ClashOptimiserUIObj.lblCategoryName.Enabled = true;
                ClashOptimiserUIObj.lblParameterValue.Enabled = true;
                ClashOptimiserUIObj.cmbTabName.Enabled = true;
                ClashOptimiserUIObj.txtPropertyName.Enabled = true;
                ClashOptimiserUIObj.txtPropertyValue.Enabled = true;
                ClashOptimiserUIObj.cmbTabName.DropDownStyle = ComboBoxStyle.DropDownList;
                ClashOptimiserUIObj.cmbPropertyName.DropDownStyle = ComboBoxStyle.DropDownList;
                ClashOptimiserUIObj.cmbPropertyValue.DropDownStyle = ComboBoxStyle.DropDownList;
                CategoryName = ClashOptimiserUIObj.txtCategoryName.Text;

                UtilityClass.ExtractionOfTabNameForWindows(ClashOptimiserUIObj.cmbTabName, Utility.Model.ElementDetails.ModelItemCollection);
            }
            else
            {
                ClashOptimiserUIObj.txtCategoryName.Enabled = false;
                ClashOptimiserUIObj.cmbPropertyName.Enabled = false;
                ClashOptimiserUIObj.cmbPropertyValue.Enabled = false;
                ClashOptimiserUIObj.lblParameterName.Enabled = false;
                ClashOptimiserUIObj.lblCategoryName.Enabled = false;
                ClashOptimiserUIObj.lblParameterValue.Enabled = false;
                ClashOptimiserUIObj.cmbTabName.Enabled = false;
                ClashOptimiserUIObj.txtPropertyName.Enabled = false;
                ClashOptimiserUIObj.txtPropertyValue.Enabled = false;
            }
        }
    }
}
