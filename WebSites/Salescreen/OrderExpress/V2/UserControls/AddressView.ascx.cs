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
    public partial class AddressView : System.Web.UI.UserControl
    {
        private AddressData Address;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForAddress(AddressData address)
        {
            this.Address = address;
        }
        public void SetValuesToForm()
        {
            if (this.Address != null)
            {
                this.lblAddressName.Text = this.Address.Name ?? "";
                this.lblFirstName.Text = this.Address.FirstName ?? "";
                this.lblLastName.Text = this.Address.LastName ?? "";
                this.lblAddressLine1.Text = this.Address.Address1 ?? "";
                this.lblAddressLine2.Text = this.Address.Address2 ?? "";
                this.lblCity.Text = this.Address.City ?? "";
                this.lblCounty.Text = this.Address.County ?? "";
                this.lblZipCode.Text = this.Address.Zip ?? "";
                this.lblZipCode.Text += this.Address.Zip4 ?? "";
                this.cbResidential.Checked = this.Address.IsResidentialArea;

                if (this.Address.Subdivision != null)
                {
                    if (this.Address.Subdivision.Code != null && this.Address.Subdivision.Name1 != null)
                    {
                        this.lblState.Text = String.Format("{0} {1}", this.Address.Subdivision.Code.Trim(), this.Address.Subdivision.Name1.Trim());
                    }
                }

                this.lblPhone.Text = this.Address.Phone ?? "";
                this.lblFax.Text = this.Address.Fax ?? "";
                this.lblEmail.Text = this.Address.Email ?? "";
            }
        }
    }
}