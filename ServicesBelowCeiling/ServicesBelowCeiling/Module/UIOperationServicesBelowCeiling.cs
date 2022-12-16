using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;
using Utility.Module;

namespace ServicesBelowCeiling
{
    public class UIOperationServicesBelowCeiling
    {
       public UIServicesBelowCeiling UIServicesBelowCeilingObj { get; set; }

        public UIOperationServicesBelowCeiling(UIServicesBelowCeiling uIServicesBelowCeiling)
        {
            UIServicesBelowCeilingObj = uIServicesBelowCeiling;
        }
       public void PropertyNameSelectedIndexChanged()
        {
            ElementDetails.PropertyName = UIServicesBelowCeilingObj.cmbPropertyName.SelectedItem.ToString();
            PopulatePropertyValue();
        }

        public void TextBoxPropertyNameTextOrKeyPress()
        {
            if (UIServicesBelowCeilingObj.txtBoxPropertyName.TextLength > 0 && UIServicesBelowCeilingObj.cmbPropertyName.Enabled == true)
            {
                UIServicesBelowCeilingObj.cmbPropertyName.Enabled = false;
                ElementDetails.PropertyName = UIServicesBelowCeilingObj.txtBoxPropertyName.Text;
                PopulatePropertyValue();
            }
            else
            {
                UIServicesBelowCeilingObj.cmbPropertyName.Enabled = true;
            }
        }

        private void PopulatePropertyValue()
        {
            List<string> parameterValueList = UtilityClass.ParameterValue(ElementDetails.CategoryName, ElementDetails.PropertyName);
            UIServicesBelowCeilingObj.cmbPropertyValue.DataSource = null;
            UIServicesBelowCeilingObj.cmbPropertyValue.DataSource = parameterValueList;
        }

        public void OtherButtonCheckedChanged()
        {
            if (UIServicesBelowCeilingObj.rdBtnOther.Checked == true)
            {
                UIServicesBelowCeilingObj.listBox_categories.Enabled = false;
                UIServicesBelowCeilingObj.lblCategoryName.Enabled = true;
                UIServicesBelowCeilingObj.lblPropertyName.Enabled = true;
                UIServicesBelowCeilingObj.lblPropertyValue.Enabled = true;
                UIServicesBelowCeilingObj.txtCategoryDisplayName.Enabled = true;
                UIServicesBelowCeilingObj.cmbPropertyName.Enabled = true;
                UIServicesBelowCeilingObj.cmbPropertyValue.Enabled = true;
                UIServicesBelowCeilingObj.txtBoxPropertyName.Enabled = true;
                UIServicesBelowCeilingObj.txtPropertyValue.Enabled = true;
                UIServicesBelowCeilingObj.lblUnit.Enabled = false;
                UIServicesBelowCeilingObj.lblFloorHeight.Enabled = false;
                UIServicesBelowCeilingObj.cmbLevel.Enabled = false;
                UIServicesBelowCeilingObj.cmbUnit.Enabled = false;
                UIServicesBelowCeilingObj.lblLevel.Enabled = false;
                UIServicesBelowCeilingObj.numFloorHeight.Enabled = false;


            }
            else
            {
                UIServicesBelowCeilingObj.listBox_categories.Enabled = true;
                UIServicesBelowCeilingObj.lblCategoryName.Enabled = false;
                UIServicesBelowCeilingObj.lblPropertyName.Enabled = false;
                UIServicesBelowCeilingObj.lblPropertyValue.Enabled = false;
                UIServicesBelowCeilingObj.txtCategoryDisplayName.Enabled = false;
                UIServicesBelowCeilingObj.cmbPropertyName.Enabled = false;
                UIServicesBelowCeilingObj.cmbPropertyValue.Enabled = false;
                UIServicesBelowCeilingObj.txtBoxPropertyName.Enabled = false;
                UIServicesBelowCeilingObj.txtPropertyValue.Enabled = false;
                UIServicesBelowCeilingObj.lblUnit.Enabled = false;
                UIServicesBelowCeilingObj.lblFloorHeight.Enabled = false;
                UIServicesBelowCeilingObj.cmbLevel.Enabled = false;
                UIServicesBelowCeilingObj.cmbUnit.Enabled = false;
                UIServicesBelowCeilingObj.lblLevel.Enabled = false;
                UIServicesBelowCeilingObj.numFloorHeight.Enabled = false;

            }
        }

        public void RevitButtonCheckChanged()
        {
            if (UIServicesBelowCeilingObj.rdBtnRevit.Checked == true)
            {
                UIServicesBelowCeilingObj.listBox_categories.Enabled = true;
                UIServicesBelowCeilingObj.lblCategoryName.Enabled = false;
                UIServicesBelowCeilingObj.lblPropertyName.Enabled = false;
                UIServicesBelowCeilingObj.lblPropertyValue.Enabled = false;
                UIServicesBelowCeilingObj. txtCategoryDisplayName.Enabled = false;
                UIServicesBelowCeilingObj.cmbPropertyName.Enabled = false;
                UIServicesBelowCeilingObj.cmbPropertyValue.Enabled = false;
                txtBoxPropertyName.Enabled = false;
                txtPropertyValue.Enabled = false;
                lblUnit.Enabled = false;
                lblFloorHeight.Enabled = false;
                cmbLevel.Enabled = false;
                cmbUnit.Enabled = false;
                lblLevel.Enabled = false;
                numFloorHeight.Enabled = false;
                PopulateListBox();
            }
            else
            {
                listBox_categories.Enabled = false;
                lblCategoryName.Enabled = true;
                lblPropertyName.Enabled = true;
                lblPropertyValue.Enabled = true;
                txtCategoryDisplayName.Enabled = true;
                cmbPropertyName.Enabled = true;
                cmbPropertyValue.Enabled = true;
                txtBoxPropertyName.Enabled = true;
                txtPropertyValue.Enabled = true;
                lblUnit.Enabled = false;
                lblFloorHeight.Enabled = false;
                cmbLevel.Enabled = false;
                cmbUnit.Enabled = false;
                lblLevel.Enabled = false;
                numFloorHeight.Enabled = false;
            }
        }
    }
}
