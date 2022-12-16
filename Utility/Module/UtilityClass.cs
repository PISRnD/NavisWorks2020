using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MoreLinq;


namespace Utility.Module
{
    public static class UtilityClass
    {             
        /// <summary>
        /// This method is to populate the data of the category name with textbox input or 
        /// selection of element to populate the data of combobox category name
        /// </summary>
        public static void ExtractionOfTabNameForWindows(System.Windows.Forms.ComboBox cmbTabName, ModelItemCollection modelItemCollection)
        {
            // modelItemCollection = document.CurrentSelection.SelectedItems;
            if (modelItemCollection.Any())
            {            
                modelItemCollection.ToList().ForEach((modelItem) =>
                {
                    try
                    {
                        PropertyCategoryCollection propertyCategoryCollection = modelItem.PropertyCategories;
                        cmbTabName.Items.AddRange( propertyCategoryCollection.DistinctBy(x => x.DisplayName).Select(x => x.DisplayName).ToArray());
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                });
                var itemArry = cmbTabName.Items.Cast<object>().Distinct().ToArray();
                cmbTabName.Items.Clear();
                cmbTabName.Items.AddRange(itemArry);
              
            }
            
        }
        public static void ParameterNames(string TabOrCategoryName, Document document,ComboBox cmbPropertyName)
        {
            try
            {
                
                cmbPropertyName.Items.Clear();
                Search search = new Search();
                SearchCondition sc1 = SearchCondition.HasCategoryByDisplayName(TabOrCategoryName);
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;
                ModelItem modelItems = search.FindFirst(document, true);
                var category = modelItems.PropertyCategories.Where(x => x.DisplayName == TabOrCategoryName).FirstOrDefault();
                cmbPropertyName.ItemsSource = (category.Properties.Cast<Object>().ToArray());
                cmbPropertyName.DisplayMemberPath = "DisplayName";
            }
            catch (Exception ex)
            {

                
            }
        }

        public static void ParameterNamesWindows(string TabOrCategoryName, Document document, System.Windows.Forms.ComboBox cmbPropertyName)
        {
            try
            {
                cmbPropertyName.Items.Clear();
                Search search = new Search();
                SearchCondition sc1 = SearchCondition.HasCategoryByDisplayName(TabOrCategoryName);
                search.SearchConditions.Add(sc1);
                search.Selection.SelectAll();
                search.Locations = SearchLocations.DescendantsAndSelf;

                ModelItem modelItems = search.FindFirst(document, true);
                var category = modelItems.PropertyCategories.Where(x => x.DisplayName == TabOrCategoryName).FirstOrDefault();
                cmbPropertyName.DataSource = category.Properties.DistinctBy(x => x.DisplayName).Select(x => x.DisplayName).ToArray();
                cmbPropertyName.DisplayMember = "DisplayName";
            }
            catch (Exception)
            {


            }
        }

       

        public static void ParameterNamesWindowsForSelected(string TabOrCategoryName, System.Windows.Forms.ComboBox cmbPropertyName,ModelItemCollection selectedModelItems)
        {
            try
            {
                if (selectedModelItems.Any())
                {
                    cmbPropertyName.Items.Clear();
                    selectedModelItems.ToList().ForEach((modelItem) =>
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
                    
                }                
            }
            catch (Exception)
            {


            }
        }

        public static List<string> ParameterValue(string CategoryName, string PropertyName)
        {
          
            List<string> propertyvalues = FetchCategories(CategoryName, PropertyName);
            return propertyvalues;
        }

        public static  List<string> FetchCategories(string CategoryName, string PropertyName)
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
            categories = categories.Distinct().OrderBy(x => x).ToList();
            return categories;
        }

        
    }
}
