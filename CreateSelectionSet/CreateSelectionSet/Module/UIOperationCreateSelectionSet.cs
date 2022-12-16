using Autodesk.Navisworks.Api;
using MoreLinq;
using PivdcNavisworksSupportModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Utility.Model;
using Utility.Module;
using Application = Autodesk.Navisworks.Api.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CreateSelectionSet
{
    public class UIOperationCreateSelectionSet
    {
        #region Declaration
        public UICreateSelectionSet UICreateSelectionSetObj { get; set; }
        #endregion

        /// <summary>
        /// This is the parametric constructor class of UIOperationCreateSelectionSet and
        /// contains the UICreateSelectionSet as the argument
        /// </summary>
        /// <param name="uICreateSelectionSet">This is the object of UICreateSelectionSet class</param>
        public UIOperationCreateSelectionSet(UICreateSelectionSet uICreateSelectionSet)
        {
            UICreateSelectionSetObj = uICreateSelectionSet;
        }

        /// <summary>
        /// This method is to create the selection set when the button create selection set is clicked
        /// on the basis of the input property name
        /// </summary>
        /// <param name="txtPropertyName">Property Name Textbox</param>
        /// <param name="cmbPropertyName">Property Name Combobox</param>
        public  void CreationOfSelectionSet(TextBox txtPropertyName, ComboBox cmbPropertyName)
        {
            DatabaseManager.SetTime();
            if (txtPropertyName.Text.Length > 0)
            {
                ElementDetails.PropertyName = txtPropertyName.Text;
            }
            else
            {
                try
                {
                    ElementDetails.PropertyName = cmbPropertyName.SelectedItem.ToString();
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
            try
            {
                ElementDetails.Document = Application.ActiveDocument;
                Search search = new Search();
                SearchCondition searchCondition = SearchCondition.HasPropertyByDisplayName(ElementDetails.CategoryName, ElementDetails.PropertyName);
                search.SearchConditions.Add(searchCondition);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                ModelItemCollection modelItems = search.FindAll(ElementDetails.Document, true);
                if (modelItems.Count == 0)
                {
                    MessageBox.Show("No items found having a Revit property named " + ElementDetails.PropertyName);
                    return;
                }
                Dictionary<string, List<ModelItem>> modelItemGroups = new Dictionary<string, List<ModelItem>>();
                foreach (ModelItem modelItem in modelItems)
                {
                    try
                    {
                        ElementDetails.PropertyValue = modelItem.PropertyCategories.Where(x => x.DisplayName == ElementDetails.CategoryName).FirstOrDefault().Properties.Where(x => x.DisplayName == ElementDetails.PropertyName).FirstOrDefault().Value.ToDisplayString();
                        if (modelItemGroups.Where(x => x.Key == ElementDetails.PropertyValue).Count() == 0)
                        {
                            modelItemGroups.Add(ElementDetails.PropertyValue, new List<ModelItem>() { modelItem });
                        }
                        else
                        {
                            modelItemGroups.Where(x => x.Key == ElementDetails.PropertyValue).FirstOrDefault().Value.Add(modelItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                    }
                }
                PrepareSelectionSet(modelItemGroups);
                MessageBox.Show(modelItemGroups.Count + " selection sets created");
                if (modelItems.Count > 0)
                {
                    ToolSupport.InsertUsage(ElementDetails.Document, modelItems.Count);
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This method is to populate the PropertyName data on the TabName
        /// Selected Index Changed
        /// </summary>
        public void TabNameSelectedIndexChangedButton()
        {
            ElementDetails.CategoryName = UICreateSelectionSetObj.cmbTabName.SelectedItem.ToString();
            ParameterNamesWindowsForSelected(ElementDetails.CategoryName, UICreateSelectionSetObj.cmbPropertyName,
             UICreateSelectionSetObj.txtPropertyName);
        }

        /// <summary>
        /// This method is to populate the Property Name when the Category Name text
        /// is changed
        /// </summary>
        /// <param name="e">KePressEventArgs which triggers when any key is pressed</param>
        public void CategoryNameTextOrKeyPress(KeyPressEventArgs e)
        {
            ElementDetails.CategoryName = UICreateSelectionSetObj.txtCategoryName.Text.Trim();
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                UICreateSelectionSetObj.cmbPropertyName.Items.Clear();
                UtilityClass.ParameterNamesWindows(ElementDetails.CategoryName, ElementDetails.Document,
                    UICreateSelectionSetObj.cmbPropertyName);
            }
        }

        /// <summary>
        /// This method enables or disables the property name data on the 
        /// combobox or textbox
        /// </summary>
        public void PropertyNameTextOrKeyPress()
        {
            if (UICreateSelectionSetObj.txtPropertyName.TextLength > 0 && UICreateSelectionSetObj.cmbPropertyName.Enabled == true)
            {
                UICreateSelectionSetObj.cmbPropertyName.Enabled = false;
            }
            else
            {
                UICreateSelectionSetObj.cmbPropertyName.Enabled = true;
            }
        }

        /// <summary>
        /// This method is to populate the data of the category name with textbox input or 
        /// selection of element to populate the data of combobox category name
        /// </summary>
        public  void ExtractionOfTabName(ComboBox cmbTabName, TextBox txtCategoryName)
        {
            // modelItemCollection = document.CurrentSelection.SelectedItems;
            if (ElementDetails.ModelItemCollection.Any())
            {
                ElementDetails.ModelItemCollection.ToList().ForEach((modelItem) =>
                {
                    try
                    {
                        PropertyCategoryCollection propertyCategoryCollection = modelItem.PropertyCategories;
                        cmbTabName.Items.AddRange(propertyCategoryCollection.DistinctBy(x => x.DisplayName).Select(x => x.DisplayName).ToArray());
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                });
                var itemArry = cmbTabName.Items.Cast<object>().Distinct().ToArray();
                cmbTabName.Items.Clear();
                cmbTabName.Items.AddRange(itemArry);
                UICreateSelectionSetObj.txtCategoryName.Enabled = false;
            }
            else
            {
                ElementDetails.CategoryName = txtCategoryName.Text;
                UICreateSelectionSetObj.cmbTabName.Enabled = false;
            }
        }

        /// <summary>
        /// This method is to populate the Parameter Names and TabName in the combobox as per the
        /// selected model items
        /// </summary>
        /// <param name="TabOrCategoryName">TabName</param>
        /// <param name="cmbPropertyName">ComboBox Property Name</param>
        /// <param name="txtPropertyName">TextBox Property Name</param>
        public  void ParameterNamesWindowsForSelected(string TabOrCategoryName, ComboBox cmbPropertyName, TextBox txtPropertyName)
        {
            try
            {
                if (ElementDetails.ModelItemCollection.Any())
                {

                    ElementDetails.ModelItemCollection.ToList().ForEach((modelItem) =>
                    {
                        try
                        {
                            var category = modelItem.PropertyCategories.Where(x => x.DisplayName == TabOrCategoryName).FirstOrDefault();
                            cmbPropertyName.Items.AddRange(category.Properties.DistinctBy(x => x.DisplayName).Select(x => x.DisplayName).ToArray());
                        }
                        catch (Exception ex)
                        {
                            string str = ex.ToString();
                        }
                    });
                    var itemArry = cmbPropertyName.Items.Cast<object>().Distinct().ToArray();
                    cmbPropertyName.Items.Clear();
                    cmbPropertyName.Items.AddRange(itemArry);
                    cmbPropertyName.DisplayMember = "DisplayName";

                }
                else
                {
                    ElementDetails.PropertyName = txtPropertyName.Text;
                    cmbPropertyName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// This method is to create the selection set from the list of the model items
        /// </summary>
        /// <param name="modelItemGroups">Model Items group which follow the condition</param>
        public void PrepareSelectionSet(Dictionary<string, List<ModelItem>> modelItemGroups)
        {
            foreach (var modelItemGroup in modelItemGroups)
            {
                try
                {
                    ModelItemCollection modelItemCollection = new ModelItemCollection();
                    foreach (ModelItem modelItem in modelItemGroup.Value)
                    {
                        try
                        {
                            modelItemCollection.Add(modelItem);
                        }
                        catch (Exception ex)
                        {
                            ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                        }
                    }
                    SavedItem si = new SelectionSet(modelItemCollection);
                    si.DisplayName = modelItemGroup.Key;
                    ElementDetails.Document.SelectionSets.AddCopy(si);
                }
                catch (Exception ex)
                {
                    ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
    }
}
