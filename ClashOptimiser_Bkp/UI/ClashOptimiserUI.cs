using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Utility.Model;
using Utility.Module;
using PiNavisworks.PiNavisworksSupport;
using PivdcNavisworksSupportModule;

namespace ClashOptimiser
{
    public partial class ClashOptimiserUI : Form
    {
        private delegate void EventHandle();
        public int progressCnt = 0;
        DocumentClash documentClash;
        int grpcnt = 0;
        DocumentClashTests oDCT;
        public double _tolerence = 0;
        int totalElementProcessed = 0;
        string displayname;
        public static DateTime dtS = new DateTime();
        public static DateTime dtE = new DateTime();
        public static int sec = 0;
        public string CategoryName { get; set; }

        public string ParameterValue { get; set; }
        public List<ClashItemInformation> ClashItemInformationList { get; set; }
       
       
        public ClashOptimiserUI()
        {
            InitializeComponent();
            ClashItemInformationList = new List<ClashItemInformation>();
            ElementDetails.Document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            ElementDetails.ModelItemCollection = ElementDetails.Document.CurrentSelection.SelectedItems;
        }
        public int GetCount(GroupItem group, ClashResultStatus rs)
        {
            int cnt = 0;
            foreach (SavedItem child in group.Children)
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
                GroupItem child_group = child as GroupItem;

                // is this a group?
                if (child_group != null)
                {
                    // operate on the group's children
                    //GetCount(child_group, rs);
                    ClashResultGroup result = child_group as ClashResultGroup;

                    ////Added by Preeti
                    //if(result.Selection1.Cast<ModelItem>().FirstOrDefault()!= null)
                    //{ 
                    //string firstChildName = result.Selection1.Cast<ModelItem>().FirstOrDefault().Parent.DisplayName;
                    //string secondChildName = result.Selection2.Cast<ModelItem>().FirstOrDefault().Parent.DisplayName;
                  
                    ////Added by Preeti
                    if (result.Status == rs)
                        cnt++;
                }
                else
                {
                    // Not a group, so must be a result.
                    ClashResult result = child as ClashResult;
                    if (result.Status == rs)
                        cnt++;
                    // act on result...
                }
            }
            return cnt;
        }

        public void UpdateTest(ClashTest t, DataGridViewRow r)
        {
            int AcNw = GetCount(t, ClashResultStatus.Active) + GetCount(t, ClashResultStatus.New);
            int Reviewed = GetCount(t, ClashResultStatus.Reviewed);
            int approved = GetCount(t, ClashResultStatus.Approved);
            int Index = dataClashGrid.Rows.IndexOf(r);
            dataClashGrid.Rows[Index].Cells["colNwAc"].Value = AcNw.ToString();
            dataClashGrid.Rows[Index].Cells["colReviewed"].Value = Reviewed.ToString();
            dataClashGrid.Rows[Index].Cells["colApproved"].Value = approved.ToString();
            dataClashGrid.Rows[Index].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.right-sm.png");
            dataClashGrid.Rows[Index].Cells["colSort"].Value = (AcNw + Reviewed + approved);
            dataClashGrid.Sort(this.dataClashGrid.Columns["colSort"], ListSortDirection.Descending);
            dataClashGrid.Refresh();
            dataClashGrid.AllowUserToResizeColumns = true;
            // dataClashGrid.Rows[Index].Selected = true
        }

        public void LoadTest(string filename)
        {
            documentClash = Autodesk.Navisworks.Api.Application.Documents.Where(x => x.Title == filename).FirstOrDefault().GetClash();//Autodesk.Navisworks.Api.Application.ActiveDocument.GetClash();
            oDCT = documentClash.TestsData;
            dataClashGrid.Rows.Clear();
            foreach (ClashTest t in oDCT.Tests)
            {
                DataGridViewRow r = new DataGridViewRow();
                //System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                // Stream myStream = myAssembly.GetManifestResourceStream("ClashOptimiser.Image.close-sm.png");
                //Bitmap image = new Bitmap(myStream);
                int AcNw = GetCount(t, ClashResultStatus.Active) + GetCount(t, ClashResultStatus.New);
                int Reviewed = GetCount(t, ClashResultStatus.Reviewed);
                int approved = GetCount(t, ClashResultStatus.Approved);
                dataClashGrid.Rows.Add(GetImage("ClashOptimiser.Images.close-sm.png"), t.DisplayName, AcNw.ToString(), Reviewed.ToString(), approved.ToString(), (AcNw + Reviewed + approved));
            }
            (dataClashGrid.Columns["colImg"] as DataGridViewImageColumn).Image = GetImage("ClashOptimiser.Images.close-sm.png");
            dataClashGrid.Sort(this.dataClashGrid.Columns["colSort"], ListSortDirection.Descending);
            dataClashGrid.Refresh();
            dataClashGrid.AllowUserToResizeColumns = true;
        }

        List<List<SavedItem>> grouplist = new List<List<SavedItem>>();
        public bool CompareBoundingBox(BoundingBox3D bb1, BoundingBox3D bb2)
        {
            double bbMinXres = Math.Abs(bb1.Min.X - bb2.Min.X);
            double bbMinYres = Math.Abs(bb1.Min.Y - bb2.Min.Y);
            double bbMinZres = Math.Abs(bb1.Min.Z - bb2.Min.Z);
            double bbMaxXres = Math.Abs(bb1.Max.X - bb2.Max.X);
            double bbMaxYres = Math.Abs(bb1.Max.Y - bb2.Max.Y);
            double bbMaxZres = Math.Abs(bb1.Max.Z - bb2.Max.Z);
            if (bbMinXres <= _tolerence && bbMinYres <= _tolerence && bbMinZres <= _tolerence && bbMaxXres <= _tolerence && bbMaxYres <= _tolerence && bbMaxZres <= _tolerence)
            {
                return true;
            }
            return false;
        }

        public List<SavedItem> CompareBB(BoundingBox3D bbx, List<SavedItem> savedItems)
        {
            return savedItems.Where(x => CompareBoundingBox(bbx, (x as ClashResult).BoundingBox)).ToList();
        }

        public void CreateNewGroup(ClashTest t, List<SavedItem> itmList, int cnt, string GroupName)
        {
            try
            {

                ClashResultGroup group = new ClashResultGroup();
                int groupNdx = t.Children.IndexOfDisplayName(GroupName);
                if (-1 == groupNdx)
                {
                    ClashResultGroup newGroup = new ClashResultGroup();
                  //  group.DisplayName = GroupName;
                   newGroup.DisplayName = GroupName;
                    oDCT.TestsInsertCopy(t, 0, newGroup);
                    newGroup = (ClashResultGroup)t.Children[0];
                    group = newGroup;
                }
                else
                {
                    group = (ClashResultGroup)t.Children[groupNdx];
                }              
                int count = 0;            
                    itmList.ForEach(y =>
                    {
                        try
                        {
                            ClashResult c = y as ClashResult;

                            oDCT.TestsMove(t, t.Children.IndexOf(c), group, 0);
                            count++;                      
                    }
                        catch (Exception ex)
                        {


                        }

                    });              
            }
            catch (Exception ex)
            {


            }

        }
        public void CreateGroup(ClashTest t, List<SavedItem> itmList, int cnt)
        {
            try
            {
                ClashResultGroup group = new ClashResultGroup();
                int groupNdx =  t.Children.IndexOfDisplayName(string.Format("{0}({1} : {2})", "Group" + cnt.ToString(), "Number of Clashes", itmList.Count()));
                if (-1 == groupNdx)
                {
                    ClashResultGroup newGroup = new ClashResultGroup();
                   
                    newGroup.DisplayName = string.Format("{0}({1} = {2})", "Group" + cnt.ToString(), "Number of Clashes", itmList.Count()); 
                    oDCT.TestsInsertCopy(t, 0, newGroup);
                    group = newGroup;
                    group = (ClashResultGroup)t.Children[0];
                }
                else
                {
                    group = (ClashResultGroup)t.Children[groupNdx];
                }               
                itmList.ForEach(y =>
                {
                    try
                    {
                        ClashResult c = y as ClashResult;
                        oDCT.TestsMove(t, t.Children.IndexOf(c), group, 0);

                        // Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {


                    }

                });                
            }
            catch (Exception ex)
            {


            }

        }
        public void CreateGroupByCategory(ClashTest t, List<SavedItem> itmList, int cnt, string groupName)
        {
            try
            {
                ClashResultGroup group = new ClashResultGroup();


              
                int groupNdx = t.Children.IndexOfDisplayName(string.Format("{0}({1} : {2})",groupName,"No. of clashes",itmList.Count()));
                if (-1 == groupNdx)
                {
                    ClashResultGroup newGroup = new ClashResultGroup();
                  //  newGroup.DisplayName = "Group" + cnt.ToString();
                    ////Added by Preeti
                    //string firstChildName = t.Children.FirstOrDefault().Parent.Chil.Selection1.Cast<ModelItem>().FirstOrDefault().Parent.DisplayName;
                    //string secondChildName = result.Selection2.Cast<ModelItem>().FirstOrDefault().Parent.DisplayName;

                    ////Added by Preeti
                   // string name = itmList.Select(x=>x.Parent.Children.)
                    newGroup.DisplayName = string.Format("{0}({1} : {2})",groupName,"No. of clashes",itmList.Count());
                    oDCT.TestsInsertCopy(t, 0, newGroup);
                    group = (ClashResultGroup)t.Children[0];
                }
                else
                {
                    group = (ClashResultGroup)t.Children[groupNdx];
                }
                // foreach (SavedItem i in x)
                //Commented by Preeti on 01-04-22
                itmList.ForEach(y =>
                {
                    try
                    {
                        ClashResult c = y as ClashResult;
                        oDCT.TestsMove(t, t.Children.IndexOf(c), group, 0);
                        // Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                    }

                });
                //Commented by Preeti on 01-04-22
                itmList.ForEach(y =>
                {
                    List<SavedItem> childItem = new List<SavedItem>();
                    if (chkIgnoreApproved.Checked == true && chkIgnoreReviewed.Checked == true)
                    {

                        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();

                    }
                    else if (chkIgnoreApproved.Checked == true)
                    {

                        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();

                    }
                    else if (chkIgnoreReviewed.Checked == true)
                    {
                        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();

                    }
                    else
                    {
                        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((b as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();

                    }
                    //Commented by Preeti on 01-04-22
                    childItem.ForEach((a) =>
                    {

                        oDCT.TestsMove(t, t.Children.IndexOf(a), group, 0);
                        SavedItemLst.Remove(a);
                        SavedItemLst1.Remove(a);
                    });
                    //Commented by Preeti on 01-04-22

                });
                //itmList.ForEach(y =>
                //{
                //    List<SavedItem> childItem = new List<SavedItem>();
                //    if (chkIgnoreApproved.Checked == true && chkIgnoreReviewed.Checked == true)
                //    {
                //        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status == ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).BoundingBox.Min.Z - (y as ClashResult).BoundingBox.Min.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Max.Z - (y as ClashResult).BoundingBox.Max.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Min.X - (y as ClashResult).BoundingBox.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Min.Y - (y as ClashResult).BoundingBox.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.X - (y as ClashResult).BoundingBox.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.Y - (y as ClashResult).BoundingBox.Max.Y) <= _tolerence)).ToList();

                //    }
                //    else if (chkIgnoreApproved.Checked == true)
                //    {

                //        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).BoundingBox.Min.Z - (y as ClashResult).BoundingBox.Min.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Max.Z - (y as ClashResult).BoundingBox.Max.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Min.X - (y as ClashResult).BoundingBox.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Min.Y - (y as ClashResult).BoundingBox.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.X - (y as ClashResult).BoundingBox.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.Y - (y as ClashResult).BoundingBox.Max.Y) <= _tolerence)).ToList();
                //    }
                //    else if (chkIgnoreReviewed.Checked == true)
                //    {

                //        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).BoundingBox.Min.Z - (y as ClashResult).BoundingBox.Min.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Max.Z - (y as ClashResult).BoundingBox.Max.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Min.X - (y as ClashResult).BoundingBox.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Min.Y - (y as ClashResult).BoundingBox.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.X - (y as ClashResult).BoundingBox.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.Y - (y as ClashResult).BoundingBox.Max.Y) <= _tolerence)).ToList();
                //    }
                //    else
                //    {
                //        childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).Except(itmList).Where(b => (Math.Abs((b as ClashResult).BoundingBox.Min.Z - (y as ClashResult).BoundingBox.Min.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Max.Z - (y as ClashResult).BoundingBox.Max.Z) <= 2) && (Math.Abs((b as ClashResult).BoundingBox.Min.X - (y as ClashResult).BoundingBox.Min.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Min.Y - (y as ClashResult).BoundingBox.Min.Y) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.X - (y as ClashResult).BoundingBox.Max.X) <= _tolerence) && (Math.Abs((b as ClashResult).BoundingBox.Max.Y - (y as ClashResult).BoundingBox.Max.Y) <= _tolerence)).ToList();
                //    }

                //    childItem.ForEach((a) =>
                //    {
                //        oDCT.TestsMove(t, t.Children.IndexOf(a), group, 0);
                //        SavedItemLst.Remove(a);
                //        SavedItemLst1.Remove(a);
                //    });

                //});
            }
            catch (Exception ex)
            {


            }

        }
        public List<SavedItem> RefinedGroup(List<SavedItem> grpRefined, List<SavedItem> ListFromFilter)
        {
            List<SavedItem> tempLst = new List<SavedItem>();
            grpRefined.ToList().ForEach((x) =>
            {
                try
                {
                    ClashResult frm = (x as ClashResult);
                    ModelItem frItm1 = frm.Item1;
                    ModelItem frItm2 = frm.Item2;
                    ListFromFilter.ToList().ForEach((y) =>
                        {
                            ClashResult to = (y as ClashResult);
                            ModelItem toItm1 = to.Item1;
                            ModelItem toItm2 = to.Item2;
                        });
                    tempLst.AddRange(ListFromFilter.Where(y => ((y as ClashResult).Item1.IsSameInstance((x as ClashResult).Item1)) == true || ((y as ClashResult).Item1.IsSameInstance((x as ClashResult).Item2) == true) || ((y as ClashResult).Item2.IsSameInstance((x as ClashResult).Item1) == true) || ((y as ClashResult).Item2.IsSameInstance((x as ClashResult).Item2) == true)).ToList());
                }
                catch (Exception)
                {


                }



            });
            return tempLst;
        }
        List<SavedItem> SavedItemLst = new List<SavedItem>();
        List<SavedItem> SavedItemLst1 = new List<SavedItem>();
        public void OptimiseClash(ClashTest tparam, DocumentClashTests oDCT, DataGridViewRow row)
        {
            //  DocumentClash documentClash =Autodesk.Navisworks.Api.Application.ActiveDocument.GetClash();
            //  DocumentClashTests oDCT = documentClash.TestsData;
            ClashTest t = tparam;// oDCT.Tests[1] as ClashTest;
            SavedItemLst.Clear();
            SavedItemLst1.Clear();
            List<SavedItem> childItem = new List<SavedItem>();

            if (chkIgnoreApproved.Checked == true && chkIgnoreReviewed.Checked == true)
            {

                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }
            else if (chkIgnoreApproved.Checked == true)
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            else if (chkIgnoreReviewed.Checked == true)
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            else
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            //-----------------------------------LOG DATA---------------------------------------------
            //File.Delete("d:\\clash_data.csv");
            //File.AppendAllText("d:\\clash_data.csv", "Clash,Distance,Centre-X,Center-Y,Center-Z,Item1-HashCode,Item2-HashCode,BBMin-X,BBMin-Y,BBMin-Z,BBMax-X,BBMax-Y,BBMax-Z,VBMin-X,VBMin-Y,VBMin-Z,VBMax-X,VBMax-Y,VBMax-Z\r\n");
            //foreach (SavedItem si in childItem.OrderByDescending(x => x.DisplayName).ToList())
            //{
            //    try
            //    {
            //        ClashResult cr = (si as ClashResult);
            //        // File.AppendAllText("d:\\clash_data.txt", "Clash:" + cr.DisplayName + "\r\nDistance:" + cr.Center.Distance.ToString() + "\r\nX=" + cr.Center.X.ToString() + "\r\nY=" + cr.Center.Y.ToString() + "\r\nZ=" + cr.Center.Z.ToString()  +"\r\n-------------------------------------------------------------------------------------\r\n");
            //        // File.AppendAllLines("d:\\clash_data.csv",Clash);
            //        File.AppendAllText("d:\\clash_data.csv", cr.DisplayName + "," + cr.Center.Distance.ToString() + "," + cr.Center.X.ToString() + "," + cr.Center.Y.ToString() + "," + cr.Center.Z.ToString() + "," + cr.Item1.InstanceHashCode + "," + cr.Item2.InstanceHashCode + "," + cr.BoundingBox.Min.X.ToString() + "," + cr.BoundingBox.Min.Y.ToString() + "," + cr.BoundingBox.Min.Z.ToString() + "," + cr.BoundingBox.Max.X.ToString() + "," + cr.BoundingBox.Max.Y.ToString() + "," + cr.BoundingBox.Max.Z.ToString() + "," + cr.ViewBounds.Min.X.ToString() + "," + cr.ViewBounds.Min.Y.ToString() + "," + cr.ViewBounds.Min.Z.ToString() + "," + cr.ViewBounds.Max.X.ToString() + "," + cr.ViewBounds.Max.Y.ToString() + "," + cr.ViewBounds.Max.Z.ToString() + "\r\n");
            //    }
            //    catch (Exception ex)
            //    {


            //    }

            //}
            //-----------------------------------END OF LOG DATA---------------------------------------------

            SavedItemLst.AddRange(childItem);
            SavedItemLst1.AddRange(childItem);
            int progresscnt = 0;


            int total = SavedItemLst1.Count;
            totalElementProcessed += total;
            // List<List<SavedItem>> savedItemGroups = new List<List<SavedItem>>();
            grpcnt = 0;
            string groupName = null;
            if (rdBtnBoundingBox.Checked == true)
            {
                SavedItemLst.ToList().ForEach((x) =>
                {


                    //   List<SavedItem> gadha = SavedItemLst1.Where(y => (Math.Abs((x as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= _tolerence)).ToList();
                    List<SavedItem> gadha = SavedItemLst1.Where(y => (Math.Abs((x as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();
                    //  List<SavedItem> gadha = SavedItemLst1.Where(y => (((x as ClashResult).BoundingBox.Min.X == (y as ClashResult).BoundingBox.Min.X) && ((x as ClashResult).BoundingBox.Min.Y == (y as ClashResult).BoundingBox.Min.Y) && ((x as ClashResult).BoundingBox.Min.Z == (y as ClashResult).BoundingBox.Min.Z) && ((x as ClashResult).BoundingBox.Max.X == (y as ClashResult).BoundingBox.Max.X) && ((x as ClashResult).BoundingBox.Max.Y == (y as ClashResult).BoundingBox.Max.Y) && ((x as ClashResult).BoundingBox.Max.Z == (y as ClashResult).BoundingBox.Max.Z))).ToList();

                    if (gadha.Count > 1)
                    {
                        grpcnt += 1;
                    //    groupName = "Group" + grpcnt.ToString();
                        CreateGroup(t, gadha, grpcnt);

                        SavedItemLst1 = SavedItemLst1.Except(gadha).ToList();
                        SavedItemLst = SavedItemLst.Except(gadha).ToList();

                    }
                  //  backgroundWorker1.RunWorkerAsync();
                    int step = Convert.ToInt32(((SavedItemLst1.Count * 100) / total));
                    progressBar1.Value = 100 - step;

                    // Thread.Sleep(1);

                });

            }
            else if (rdBtnCategory.Checked == true)
            {
                if (txtPropertyValue.Text.Length > 0)
                {
                    ElementDetails.PropertyValue = txtPropertyValue.Text;
                }
                else
                {
                    ElementDetails.PropertyValue = cmbPropertyValue.SelectedItem.ToString();
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
                        string str = ex.ToString();
                    }
                    
                    }
                List<string> groupNameList=  ClashItemInformationList.DistinctBy(x1 => x1.GroupName).Select(x1 => x1.GroupName).ToList();
                List<SavedItem> savedItems;
                groupNameList.ToList().ForEach((grpName) =>
                {
                    grpcnt += 1;
                    savedItems = new List<SavedItem>();
                    savedItems = ClashItemInformationList.Where(y => y.GroupName == grpName).Select(x => x.SavedModelItem).ToList();
                    grpName = String.Format("{0}:{1} = {2}", grpName,"Number of Clashes", savedItems.Count);
                    CreateNewGroup(t, savedItems, grpcnt, grpName);
                    SavedItemLst1 = SavedItemLst1.Except(savedItems).ToList();
                    SavedItemLst = SavedItemLst.Except(savedItems).ToList();
                });                      
            }          
            progressBar1.Value = 100;
            UpdateTest(t, row);
        }




        public void OptimiseClashBackup(ClashTest tparam, DocumentClashTests oDCT, DataGridViewRow row)
        {           
            ClashTest t = tparam;
            SavedItemLst.Clear();
            SavedItemLst1.Clear();
            List<SavedItem> childItem = new List<SavedItem>();

            if (chkIgnoreApproved.Checked == true && chkIgnoreReviewed.Checked == true)
            {

                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            }
            else if (chkIgnoreApproved.Checked == true)
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            else if (chkIgnoreReviewed.Checked == true)
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            else
            {
                childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            }
            //-----------------------------------LOG DATA---------------------------------------------
            //File.Delete("d:\\clash_data.csv");
            //File.AppendAllText("d:\\clash_data.csv", "Clash,Distance,Centre-X,Center-Y,Center-Z,Item1-HashCode,Item2-HashCode,BBMin-X,BBMin-Y,BBMin-Z,BBMax-X,BBMax-Y,BBMax-Z,VBMin-X,VBMin-Y,VBMin-Z,VBMax-X,VBMax-Y,VBMax-Z\r\n");
            //foreach (SavedItem si in childItem.OrderByDescending(x => x.DisplayName).ToList())
            //{
            //    try
            //    {
            //        ClashResult cr = (si as ClashResult);
            //        // File.AppendAllText("d:\\clash_data.txt", "Clash:" + cr.DisplayName + "\r\nDistance:" + cr.Center.Distance.ToString() + "\r\nX=" + cr.Center.X.ToString() + "\r\nY=" + cr.Center.Y.ToString() + "\r\nZ=" + cr.Center.Z.ToString()  +"\r\n-------------------------------------------------------------------------------------\r\n");
            //        // File.AppendAllLines("d:\\clash_data.csv",Clash);
            //        File.AppendAllText("d:\\clash_data.csv", cr.DisplayName + "," + cr.Center.Distance.ToString() + "," + cr.Center.X.ToString() + "," + cr.Center.Y.ToString() + "," + cr.Center.Z.ToString() + "," + cr.Item1.InstanceHashCode + "," + cr.Item2.InstanceHashCode + "," + cr.BoundingBox.Min.X.ToString() + "," + cr.BoundingBox.Min.Y.ToString() + "," + cr.BoundingBox.Min.Z.ToString() + "," + cr.BoundingBox.Max.X.ToString() + "," + cr.BoundingBox.Max.Y.ToString() + "," + cr.BoundingBox.Max.Z.ToString() + "," + cr.ViewBounds.Min.X.ToString() + "," + cr.ViewBounds.Min.Y.ToString() + "," + cr.ViewBounds.Min.Z.ToString() + "," + cr.ViewBounds.Max.X.ToString() + "," + cr.ViewBounds.Max.Y.ToString() + "," + cr.ViewBounds.Max.Z.ToString() + "\r\n");
            //    }
            //    catch (Exception ex)
            //    {


            //    }

            //}
            //-----------------------------------END OF LOG DATA---------------------------------------------

            SavedItemLst.AddRange(childItem);
            SavedItemLst1.AddRange(childItem);
            int progresscnt = 0;
            int total = SavedItemLst1.Count;
            totalElementProcessed += total;
            grpcnt = 0;
            string groupName = null;
            if (rdBtnBoundingBox.Checked == true)
            {
                SavedItemLst.ToList().ForEach((x) =>
                {
                    List<SavedItem> gadha = SavedItemLst1.Where(y => (Math.Abs((x as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();                    
                    if (gadha.Count > 1)
                    {
                        grpcnt += 1;
                        CreateGroup(t, gadha, grpcnt);
                        SavedItemLst1 = SavedItemLst1.Except(gadha).ToList();
                        SavedItemLst = SavedItemLst.Except(gadha).ToList();
                    }
                    int step = Convert.ToInt32(((SavedItemLst1.Count * 100) / total));
                    progressBar1.Value = 100 - step;
                });
            }
            else if (rdBtnCategory.Checked == true)
            {
                if (txtPropertyValue.Text.Length > 0)
                {
                    ElementDetails.PropertyValue = txtPropertyValue.Text;
                }
                else
                {
                    ElementDetails.PropertyValue = cmbPropertyValue.SelectedItem.ToString();
                }
                SavedItemLst.ToList().ForEach((x) =>
                {
                    List<ModelItem> itemListOne = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item1).ToList();
                    List<ModelItem> itemListTwo = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item2).ToList();
                    for (int i = 0; i < itemListOne.Count(); i++)
                    {
                        try
                        {
                            string propertyValue1 = itemListOne[i].PropertyCategories.Cast<PropertyCategory>().Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
                                           Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault().Value.ToDisplayString();
                            string propertyValue2 = itemListTwo[i].PropertyCategories.Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
                            Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault().Value.ToDisplayString();
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
                            string str = ex.ToString();
                        }
                    }                  
                });                                    
            }
            else
            {
            }
            progressBar1.Value = 100;
            UpdateTest(t, row);
        }
        private void UI_Display_Load(object sender, EventArgs e)
        {
            this.dataClashGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataClashGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataClashGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataClashGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataClashGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            lblCategoryName.Enabled = false;
            lblParameterName.Enabled = false;
            lblParameterValue.Enabled = false;
            txtCategoryName.Enabled = false;
            cmbPropertyName.Enabled = false;
            cmbPropertyValue.Enabled = false;
            cmbTabName.Enabled = false;
            txtPropertyName.Enabled = false;
            txtPropertyValue.Enabled = false;         
            foreach (Document doc in Autodesk.Navisworks.Api.Application.Documents)
            {
                lstAvailFiles.Items.Add(doc.Title);
            }
            if (lstAvailFiles.Items.Count > 0)
             LoadTest(lstAvailFiles.Items[0].ToString());
        }

      
        public Bitmap GetImage(string imageinfo)
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream(imageinfo);
            Bitmap image = new Bitmap(myStream);
            return image;
        }

        public void FuncCall()
        {
            DatabaseManager.SetTime();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            _tolerence = Convert.ToDouble(trackTolerence.Value);
            dataClashGrid.Enabled = false;
            numTolerence.Enabled = false;
            btnOptimise.Enabled = false;
            btnReset.Enabled = false;
            chkIgnoreApproved.Enabled = false;
            chkIgnoreReviewed.Enabled = false;
            totalElementProcessed = 0;
            foreach (DataGridViewRow row in dataClashGrid.SelectedRows)
            {
                try
                {
                    progressBar1.Value = 0;
                    string str = row.Cells["colClash"].Value.ToString();
                    this.Invoke(new Action(() => lblTest.Text = str));
                    SavedItem si = oDCT.Tests.Where(x => x.DisplayName == str).FirstOrDefault();
                    lblTest.Text = str;
                    dataClashGrid.Rows[dataClashGrid.Rows.IndexOf(row)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    dataClashGrid.Refresh();
                    this.Refresh();
                    OptimiseClash(si as ClashTest, oDCT, row);                 
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
                Thread.Sleep(1);              
            }
            if (chkArrange.Checked)
            {
                oDCT.TestsSortTests(ClashTestSortMode.ResultsNumberSort, ClashSortDirection.SortDescending);
            }
            dataClashGrid.Enabled = true;
            numTolerence.Enabled = true;
            btnOptimise.Enabled = true;
            btnReset.Enabled = true;
            chkIgnoreApproved.Enabled = true;
            chkIgnoreReviewed.Enabled = true;
            string filname;

            if (totalElementProcessed > 0)
            {
                if (lstAvailFiles.SelectedIndex == -1)
                {
                    filname = lstAvailFiles.Items[0].ToString();
                }
                else
                {
                    filname = lstAvailFiles.Items[lstAvailFiles.SelectedIndex].ToString();
                }
               ToolSupport.InsertUsage(Utility.Model.ElementDetails.Document, totalElementProcessed);               
            }
        }

        private void btnOptimise_Click(object sender, EventArgs e)
        {
            FuncCall();
        }

        /// <summary>
        /// Background worker to ask the user to confirm whether user wants to modify the data or not after 2 seconds of the form show
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event argument</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(10000);
        }


        /// <summary>
        /// After completion of 2 seconds of the UI form show, a message box will appear and ask user that user wants to modify the data or not,
        /// if yes then button will appear for user to modify the data
        /// if not then this will get closed and proceed further to check
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event argument</param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MessageBox.Show("Do you want to modify the data?", "Confirmation For Modification", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnOptimise.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                Close();
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {            
            if (rdBtnBoundingBox.Checked == true)
            {
                foreach (DataGridViewRow r in dataClashGrid.SelectedRows)
                {
                    ClashTest clash = oDCT.Tests.Where(x => x.DisplayName == r.Cells["colClash"].Value.ToString()).Cast<ClashTest>().FirstOrDefault();
                    int totalGroup = clash.Children.Where(x => x.DisplayName.Contains("Group") == true).Count();
                    List<SavedItem> saveitemLst = clash.Children.Where(x => x.DisplayName.Contains("Group") == true).Cast<SavedItem>().ToList();
                    dataClashGrid.Rows[dataClashGrid.Rows.IndexOf(r)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    dataClashGrid.Refresh();
                    this.Refresh();
                    int grpcnt = 0;
                    saveitemLst.ToList().ForEach((x) =>
                    {
                        string display = x.DisplayName;
                        if (x.DisplayName.Contains("Group") == true)
                        {
                            ClashResultGroup gr = x as ClashResultGroup;
                            gr.Children.ToList().ForEach((y) =>
                                {
                                    oDCT.TestsMove(gr, 0, clash, 0);
                                });

                            grpcnt++;
                            int step = Convert.ToInt32(((grpcnt * 100) / totalGroup));
                            progressBar1.Value = step;
                        }
                        Thread.Sleep(1);
                    });
                    clash.Children.Where(x => x.DisplayName.Contains("Group") == true).ToList().ForEach((x) => oDCT.TestsRemove(clash, x));
                    UpdateTest(clash, r);
                }
            }
            else if (rdBtnCategory.Checked == true)
            {
                foreach (DataGridViewRow r in dataClashGrid.SelectedRows)
                {
                    ClashTest clash = oDCT.Tests.Where(x => x.DisplayName == r.Cells["colClash"].Value.ToString()).Cast<ClashTest>().FirstOrDefault();
                    int totalGroup = clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).Count();
                    List<SavedItem> saveitemLst = clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).Cast<SavedItem>().ToList();
                    dataClashGrid.Rows[dataClashGrid.Rows.IndexOf(r)].Cells["colImg"].Value = GetImage("ClashOptimiser.Images.cycle-sm.png");
                    dataClashGrid.Refresh();
                    this.Refresh();
                    int grpcnt = 0;
                    saveitemLst.ToList().ForEach((x) =>
                    {
                        string display = x.DisplayName;
                        if (x.DisplayName.Contains("vs") == true)
                        {
                            ClashResultGroup gr = x as ClashResultGroup;
                            gr.Children.ToList().ForEach((y) =>
                            {
                                oDCT.TestsMove(gr, 0, clash, 0);
                            });
                            grpcnt++;
                            int step = Convert.ToInt32(((grpcnt * 100) / totalGroup));
                            progressBar1.Value = step;
                        }
                        Thread.Sleep(1);
                    });
                    clash.Children.Where(x => x.DisplayName.Contains("Number of Clashes") == true).ToList().ForEach((x) => oDCT.TestsRemove(clash, x));
                    UpdateTest(clash, r);
                }
            }
        }

        private void lstAvailFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTest(lstAvailFiles.Items[lstAvailFiles.SelectedIndex].ToString());
        }

      

        private void dataClashGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataClashGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataClashGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void lblTest_Click(object sender, EventArgs e)
        {

        }

    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdBtnCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnCategory.Checked==true)
            {
                txtCategoryName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
                lblParameterName.Enabled = true;
                lblCategoryName.Enabled = true;
                lblParameterValue.Enabled = true;
                cmbTabName.Enabled = true;
                txtPropertyName.Enabled = true;
                txtPropertyValue.Enabled = true;
                cmbTabName.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbPropertyName.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbPropertyValue.DropDownStyle = ComboBoxStyle.DropDownList;
                CategoryName = txtCategoryName.Text;

                UtilityClass.ExtractionOfTabNameForWindows(cmbTabName, Utility.Model.ElementDetails.ModelItemCollection);
            }
            else
            {
                txtCategoryName.Enabled = false;
                cmbPropertyName.Enabled = false;
                cmbPropertyValue.Enabled = false;
                lblParameterName.Enabled = false;
                lblCategoryName.Enabled = false;
                lblParameterValue.Enabled = false;
                cmbTabName.Enabled = false;
                txtPropertyName.Enabled = false;
                txtPropertyValue.Enabled = false;
            }
        }

        

        private void cmbPropertyName_SelectedIndexChanged(object sender, EventArgs e)
        {                                
            Utility.Model.ElementDetails.PropertyName = cmbPropertyName.SelectedItem.ToString();
            PopulatePropertyValue();
        }
        private void PopulatePropertyValue()
        {
            List<string> parameterValueList = UtilityClass.ParameterValue(Utility.Model.ElementDetails.CategoryName, Utility.Model.ElementDetails.PropertyName);
            cmbPropertyValue.DataSource = null;
            cmbPropertyValue.DataSource = parameterValueList;
        }
        private void rdBtnBoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnBoundingBox.Checked == true)
            {
                txtCategoryName.Enabled = false;
                cmbPropertyName.Enabled = false;
                cmbPropertyValue.Enabled = false;
                lblParameterName.Enabled = false;
                lblCategoryName.Enabled = false;
                lblParameterValue.Enabled = false;
                cmbTabName.Enabled = false;
                txtPropertyName.Enabled = false;
                txtPropertyValue.Enabled = false;
                
            }
            else
            {
                txtCategoryName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
                lblParameterName.Enabled = true;
                lblCategoryName.Enabled = true;
                lblParameterValue.Enabled = true;
                cmbTabName.Enabled = true;
                txtPropertyName.Enabled = true;
                txtPropertyValue.Enabled = true;
            }
        }

   

        private void txtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.Model.ElementDetails.CategoryName = txtCategoryName.Text.Trim();
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                UtilityClass.ParameterNamesWindows(Utility.Model.ElementDetails.CategoryName, Utility.Model.ElementDetails.Document, cmbPropertyName);
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbTabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.Model.ElementDetails.CategoryName = cmbTabName.SelectedItem.ToString();
            UtilityClass.ParameterNamesWindowsForSelected(Utility.Model.ElementDetails.CategoryName, cmbPropertyName, Utility.Model.ElementDetails.ModelItemCollection);
        }

        private void txtPropertyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPropertyName.TextLength > 0 && cmbPropertyName.Enabled == true)
            {
                cmbPropertyName.Enabled = false;
                Utility.Model.ElementDetails.PropertyName = txtPropertyName.Text;
                PopulatePropertyValue();
            }
            else
            {
                cmbPropertyName.Enabled = true;
            }
        }
    }

    class CompareBoundingBox : IEqualityComparer<SavedItem>
    {
        public bool Equals(SavedItem x, SavedItem y)
        {
            if (x == null)
                return y == null;
            else if (y == null)
                return false;
            else
                return CompareBoundingBx((x as ClashResult).BoundingBox, (y as ClashResult).BoundingBox);
        }

        public int GetHashCode(SavedItem obj)
        {
            return obj.GetHashCode();
        }
        public bool CompareBoundingBx(BoundingBox3D bb1, BoundingBox3D bb2)
        {
            double bbMinXres = Math.Abs(bb1.Min.X - bb2.Min.X);
            double bbMinYres = Math.Abs(bb1.Min.Y - bb2.Min.Y);
            double bbMinZres = Math.Abs(bb1.Min.Z - bb2.Min.Z);
            double bbMaxXres = Math.Abs(bb1.Max.X - bb2.Max.X);
            double bbMaxYres = Math.Abs(bb1.Max.Y - bb2.Max.Y);
            double bbMaxZres = Math.Abs(bb1.Max.Z - bb2.Max.Z);
            if (bbMinXres <= 20 && bbMinYres <= 20 && bbMinZres <= 20 && bbMaxXres <= 20 && bbMaxYres <= 20 && bbMaxZres <= 20)
            {
                return true;
            }
            return false;
        }
    }

   
}
