using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;
using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class AddressEdit : System.Web.UI.UserControl
    {
        private AddressData Address;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadStateValues();
            }

            this.SetValuesToForm();
        }

        private void LoadStateValues()
        {
            SubdivisionSystem subdivisionSystem = new SubdivisionSystem();
            List<QSP.OrderExpress.Business.Entity.Subdivision> subdivisionListList = subdivisionSystem.GetSubdivisionCodesByCountryCode("US");

            this.ddlState.Items.Clear();
            foreach (QSP.OrderExpress.Business.Entity.Subdivision subdivision in subdivisionListList)
            {
                this.ddlState.Items.Add(new ListItem(string.Format("{0} - {1}", subdivision.SubdivisionCode, subdivision.SubdivisionName1), subdivision.SubdivisionCode));
            }
        }
        private void SetValuesToForm()
        {
            if (this.Address != null)
            {
                this.tbAddressName.Text = this.Address.Name ?? "";
                this.tbFirstName.Text = this.Address.FirstName ?? "";
                this.tbLastName.Text = this.Address.LastName ?? "";
                this.tbAddressLine1.Text = this.Address.Address1 ?? "";
                this.tbAddressLine2.Text = this.Address.Address2 ?? "";
                this.tbCity.Text = this.Address.City ?? "";
                this.tbCounty.Text = this.Address.County ?? "";
                this.tbZipCode.Text = this.Address.Zip ?? "";
                this.cbResidential.Checked = this.Address.IsResidentialArea;

                if (this.Address.Subdivision != null)
                {
                    ListItem typeItem = this.ddlState.Items.FindByValue(this.Address.Subdivision.Code.ToString());
                    this.ddlState.SelectedIndex = this.ddlState.Items.IndexOf(typeItem);
                }

                this.tbPhone.Text = this.Address.Phone ?? "";
                this.tbFax.Text = this.Address.Fax ?? "";
                this.tbEmail.Text = this.Address.Email ?? "";
            }
        }
        private void GetValuesFromForm()
        {
            if (this.Address == null)
            {
                this.Address = new AddressData();
            }
            if (this.Address.Subdivision == null)
            {
                this.Address.Subdivision = new SubdivisionData();
            }

            this.Address.Name = this.tbAddressName.Text;
            this.Address.FirstName = this.tbFirstName.Text;
            this.Address.LastName = this.tbLastName.Text;
            this.Address.Address1 = this.tbAddressLine1.Text;
            this.Address.Address2 = this.tbAddressLine2.Text;
            this.Address.City = this.tbCity.Text;
            this.Address.County = this.tbCounty.Text;
            this.Address.Zip = this.tbZipCode.Text;
            this.Address.Subdivision.Code = this.ddlState.SelectedValue;
            this.Address.IsResidentialArea = this.cbResidential.Checked;
            this.Address.Phone = this.tbPhone.Text;
            this.Address.Fax = this.tbFax.Text;
            this.Address.Email = this.tbEmail.Text;
        }

        public void SetValueForAddress(AddressData address)
        {
            this.Address = address;
        }
        public AddressData GetValueFromAddress()
        {
            this.GetValuesFromForm();

            return this.Address;
        }
    }
}