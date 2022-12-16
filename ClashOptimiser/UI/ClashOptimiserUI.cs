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
        #region VariableDeclaration
        private delegate void EventHandle();
        public int progressCnt = 0;
        public static DateTime StartDateTime { get; set; }
        public static DateTime EndDateTime { get; set; }
        public static int sec { get; set; }
        public UIOperationClassOptimiser uIOperationClassOptimiser { get; set; } 
        #endregion

        /// <summary>
        /// This is the constructor class of ClashOptimiserUI
        /// </summary>
        public ClashOptimiserUI()
        {
            InitializeComponent();           
            ElementDetails.Document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            ElementDetails.ModelItemCollection = ElementDetails.Document.CurrentSelection.SelectedItems;
            uIOperationClassOptimiser = new UIOperationClassOptimiser(this);
            StartDateTime = new DateTime();
            EndDateTime = new DateTime();
            sec = 0;
        }

        /// <summary>
        /// This is the combobox for the TabName Selected Index Changed
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void cmbTabNameSelectedIndexChanged(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.TabNameSelectedIndexChanged();
        }

        /// <summary>
        /// This is the method for the PropertyName combobox selected index changed and
        /// its corresponding Property value changes
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void cmbPropertyNameSelectedIndexChanged(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.PropertyNameSelectedIndexChanged();
        }

        /// <summary>
        /// This method is to enabled and disabled the controls in the UI as per
        /// the radiobutton bounding box selection changed
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void rdBtnBoundingBoxCheckedChanged(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.BoundingBoxCheckChanged();
        }

        /// <summary>
        /// This method is to populate the data in the controls dependent on the category name key press
        /// text 
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void txtCategoryNameKeyPress(object sender, KeyPressEventArgs e)
        {
            uIOperationClassOptimiser.TextCategoryNameKeyPress(e);
        }

        /// <summary>
        /// This method is to populate the data in the controls dependent on the Property name key press
        /// text typed
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void txtPropertyNameKeyPress(object sender, KeyPressEventArgs e)
        {
            uIOperationClassOptimiser.TextPropertyNameKeyPress();
        }
        public void OptimiseClashBackup(ClashTest tparam, DocumentClashTests oDCT, DataGridViewRow row)
        {
            //ClashTest t = tparam;
            //SavedItemLst.Clear();
            //SavedItemLst1.Clear();
            //List<SavedItem> childItem = new List<SavedItem>();

            //if (chkIgnoreApproved.Checked == true && chkIgnoreReviewed.Checked == true)
            //{

            //    childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();
            //}
            //else if (chkIgnoreApproved.Checked == true)
            //{
            //    childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Approved && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            //}
            //else if (chkIgnoreReviewed.Checked == true)
            //{
            //    childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Reviewed && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            //}
            //else
            //{
            //    childItem = t.Children.Where(x => ((x as ClashResultGroup) == null) && (x as ClashResult).Status != ClashResultStatus.Resolved).ToList();

            //}
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

            //SavedItemLst.AddRange(childItem);
            //SavedItemLst1.AddRange(childItem);
            //int progresscnt = 0;
            //int total = SavedItemLst1.Count;
            //totalElementProcessed += total;
            //grpcnt = 0;
            //string groupName = null;
            //if (rdBtnBoundingBox.Checked == true)
            //{
            //    SavedItemLst.ToList().ForEach((x) =>
            //    {
            //        List<SavedItem> gadha = SavedItemLst1.Where(y => (Math.Abs((x as ClashResult).ViewBounds.Min.Z - (y as ClashResult).ViewBounds.Min.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Max.Z - (y as ClashResult).ViewBounds.Max.Z) <= 2) && (Math.Abs((x as ClashResult).ViewBounds.Min.X - (y as ClashResult).ViewBounds.Min.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Min.Y - (y as ClashResult).ViewBounds.Min.Y) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.X - (y as ClashResult).ViewBounds.Max.X) <= _tolerence) && (Math.Abs((x as ClashResult).ViewBounds.Max.Y - (y as ClashResult).ViewBounds.Max.Y) <= _tolerence)).ToList();                    
            //        if (gadha.Count > 1)
            //        {
            //            grpcnt += 1;
            //            CreateGroup(t, gadha, grpcnt);
            //            SavedItemLst1 = SavedItemLst1.Except(gadha).ToList();
            //            SavedItemLst = SavedItemLst.Except(gadha).ToList();
            //        }
            //        int step = Convert.ToInt32(((SavedItemLst1.Count * 100) / total));
            //        progressBar1.Value = 100 - step;
            //    });
            //}
            //else if (rdBtnCategory.Checked == true)
            //{
            //    if (txtPropertyValue.Text.Length > 0)
            //    {
            //        ElementDetails.PropertyValue = txtPropertyValue.Text;
            //    }
            //    else
            //    {
            //        ElementDetails.PropertyValue = cmbPropertyValue.SelectedItem.ToString();
            //    }
            //    SavedItemLst.ToList().ForEach((x) =>
            //    {
            //        List<ModelItem> itemListOne = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item1).ToList();
            //        List<ModelItem> itemListTwo = SavedItemLst1.Select(x1 => (x1 as ClashResult).Item2).ToList();
            //        for (int i = 0; i < itemListOne.Count(); i++)
            //        {
            //            try
            //            {
            //string propertyValue1 = itemListOne[i].PropertyCategories.Cast<PropertyCategory>().Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
            //               Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault().Value.ToDisplayString();
            //string propertyValue2 = itemListTwo[i].PropertyCategories.Where(x1 => x1.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Cast<DataProperty>().
            //Where(x1 => x1.DisplayName == ElementDetails.PropertyName).FirstOrDefault().Value.ToDisplayString();
            //string TempGroup = string.Empty;
            //string TempReverseGroup = string.Empty;
            //string ExistingGroup = string.Empty;
            //if (propertyValue1 == ElementDetails.PropertyValue || propertyValue2 == ElementDetails.PropertyValue)
            //{
            //    TempGroup = string.Format("{0} vs {1}", propertyValue1, propertyValue2);
            //    TempReverseGroup = string.Format("{0} vs {1}", propertyValue2, propertyValue1);
            //    ExistingGroup = ClashItemInformationList.Where(x1 => x1.GroupName == TempGroup || x1.GroupName == TempReverseGroup).Select(x1 => x1.GroupName).FirstOrDefault();
            //    if (ExistingGroup == null)
            //    {
            //        ClashItemInformationList.Add(new ClashItemInformation
            //        {
            //            ModelItemOne = itemListOne[i],
            //            ModelItemTwo = itemListTwo[i],
            //            SavedModelItem = SavedItemLst1[i],
            //            PropertyValueOne = propertyValue1,
            //            PropertyValueTwo = propertyValue2,
            //            PropertyName = ElementDetails.PropertyName,
            //            CategoryName = CategoryName,
            //            GroupName = string.Format("{0} vs {1}", propertyValue1, propertyValue2),
            //            ReverseGroupName = string.Format("{0} vs {1}", propertyValue2, propertyValue1)
            //        });
            //    }
            //    else
            //        {
            //            ClashItemInformationList.Add(new ClashItemInformation
            //            {
            //                ModelItemOne = itemListOne[i],
            //                ModelItemTwo = itemListTwo[i],
            //                SavedModelItem = SavedItemLst1[i],
            //                PropertyValueOne = propertyValue1,
            //                PropertyValueTwo = propertyValue2,
            //                PropertyName = ElementDetails.PropertyName,
            //                CategoryName = CategoryName,
            //                GroupName = ExistingGroup,
            //                ReverseGroupName = (ExistingGroup == TempGroup) ? TempReverseGroup : TempGroup
            //            });
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string str = ex.ToString();
            //}
            //        }                  
            //    });                                    
            //}
            //else
            //{
            //}
            //progressBar1.Value = 100;
            //UpdateTest(t, row);
        }


        /// <summary>
        /// This method is to initialize all the data when the UI loads
        /// </summary>
      /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void UIDisplayLoad(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.Load();
        }

        /// <summary>
        /// This method is to do the process of grouping the clah test by removing the redundant data 
        /// and counting as unique clash test
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void btnOptimiseClick(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.FuncCall();
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

        /// <summary>
        /// This event triggers on the reset button click and reset all the 
        /// grouped clash test into ungrouped as individual clash test
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void btnResetClick(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.ResetButtonClick();
        }

        /// <summary>
        /// This method is to avail all the clash test name and accordingly data available in the
        /// controls at per the selected index 
        /// </summary>
        /// <param name="sender">Not Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void lstAvailFilesSelectedIndexChanged(object sender, EventArgs e)
        {
            uIOperationClassOptimiser.ListAvailSelectedIndexChanged();
        }
          
        /// <summary>
        /// This event is to trigger the category check changed and populate its related 
        /// data accordingly in the controls which are dependent on category such as 
        /// property Name
        /// </summary>
        /// <param name="sender">Not  Implemented</param>
        /// <param name="e">Not Implemented</param>
        private void rdBtnCategoryCheckedChanged(object sender, EventArgs e)
        {
           uIOperationClassOptimiser.CategoryCheckChanged();
        }

        private void dataClashGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

  

   
}
