using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSPForm.Business;
using QSPForm.Common;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class VendorInfo : System.Web.UI.UserControl {
        private VendorSystem vendorSys = new VendorSystem();
        private PhoneNumberSystem phoneSys = new PhoneNumberSystem();
        private PostalAddressSystem postalSys = new PostalAddressSystem();
        private EmailAddressSystem emailSys = new EmailAddressSystem();
        private QSPForm.Common.DataDef.VendorTable dtblVendor;
        private QSPForm.Common.DataDef.PhoneNumberEntityTable dtblPhone;
        private QSPForm.Common.DataDef.PhoneNumberEntityTable dtblFax;
        private QSPForm.Common.DataDef.PostalAddressEntityTable dtblPostal;
        private QSPForm.Common.DataDef.EmailEntityTable dtblEmail;
        private string postalID, phoneID, emailID, faxID;

        protected void Page_Load(object sender, EventArgs e) {
            AdjustGUI();
        }

        public bool EditMode {
            get {
                try {
                    if (this.ViewState["EditMode"] != null)
                        return Convert.ToBoolean(this.ViewState["EditMode"].ToString());
                    else
                        return false;
                }
                catch { return false; }

            }
            set {
                this.ViewState["EditMode"] = value;
            }
        }

        public int VendorID {
            get {
                if (this.ViewState["VendorID"] != null)
                    return Convert.ToInt32(this.ViewState["VendorID"].ToString());
                else
                    return 0;
            }
            set {
                this.ViewState["VendorID"] = value;
            }
        }

        #region Property
        private string VendorName {
            get {
                if (EditMode)
                    return txtVendorName.Text;
                else
                    return lblVendorName.Text;
            }
            set {
                if (EditMode)
                    txtVendorName.Text = value;
                else
                    lblVendorName.Text = value;
            }
        }
        //private string Type
        //{
        //    get
        //    {
        //        if (EditMode)
        //            return txtType.Text;
        //        else
        //            return lblType.Text;
        //    }
        //    set
        //    {
        //        if (EditMode)
        //            txtType.Text = value;
        //        else
        //            lblType.Text = value;
        //    }
        //}
        private string OracleCode {
            get {
                if (EditMode)
                    return txtOracleCode.Text;
                else
                    return lblOracleCode.Text;
            }
            set {
                if (EditMode)
                    txtOracleCode.Text = value;
                else
                    lblOracleCode.Text = value;
            }
        }
        private string PhoneNumber {
            get {
                if (EditMode)
                    return txtPhoneNumber.Text;
                else
                    return lblPhoneNumber.Text;
            }
            set {
                if (EditMode)
                    txtPhoneNumber.Text = value;
                else
                    lblPhoneNumber.Text = value;
            }
        }
        private string FaxNumber {
            get {
                if (EditMode)
                    return txtFaxNumber.Text;
                else
                    return lblFaxNumber.Text;
            }
            set {
                if (EditMode)
                    txtFaxNumber.Text = value;
                else
                    lblFaxNumber.Text = value;
            }
        }
        private string EmailAddress {
            get {
                if (EditMode)
                    return txtEmailAddress.Text;
                else
                    return lblEmailAddress.Text;
            }
            set {
                if (EditMode)
                    txtEmailAddress.Text = value;
                else
                    lblEmailAddress.Text = value;
            }
        }
        private string AddressLine1 {
            get {
                if (EditMode)
                    return txtAddressLine1.Text;
                else
                    return lblAddressLine1.Text;
            }
            set {
                if (EditMode)
                    txtAddressLine1.Text = value;
                else
                    lblAddressLine1.Text = value;
            }
        }
        private string AddressLine2 {
            get {
                if (EditMode)
                    return txtAddressLine2.Text;
                else
                    return lblAddressLine2.Text;
            }
            set {
                if (EditMode)
                    txtAddressLine2.Text = value;
                else
                    lblAddressLine2.Text = value;
            }
        }
        private string City {
            get {
                if (EditMode)
                    return txtCity.Text;
                else
                    return lblCity.Text;
            }
            set {
                if (EditMode)
                    txtCity.Text = value;
                else
                    lblCity.Text = value;
            }
        }
        private string County {
            get {
                if (EditMode)
                    return txtCounty.Text;
                else
                    return lblCounty.Text;
            }
            set {
                if (EditMode)
                    txtCounty.Text = value;
                else
                    lblCounty.Text = value;
            }
        }
        private string FirstName {
            get {
                if (EditMode)
                    return txtFirstName.Text;
                else
                    return lblFirstName.Text;
            }
            set {
                if (EditMode)
                    txtFirstName.Text = value;
                else
                    lblFirstName.Text = value;
            }
        }
        private string LastName {
            get {
                if (EditMode)
                    return txtLastName.Text;
                else
                    return lblLastName.Text;
            }
            set {
                if (EditMode)
                    txtLastName.Text = value;
                else
                    lblLastName.Text = value;
            }
        }
        private string State {
            get {
                if (EditMode)
                    return txtState.Text;
                else
                    return lblState.Text;
            }
            set {
                if (EditMode)
                    txtState.Text = value;
                else
                    lblState.Text = value;
            }
        }
        private string Zip {
            get {
                if (EditMode)
                    return txtZip.Text;
                else
                    return lblZip.Text;
            }
            set {
                if (EditMode)
                    txtZip.Text = value;
                else
                    lblZip.Text = value;
            }
        }

        #endregion End Property

        private void AdjustGUI() {
            lblAddressLine1.Visible = !EditMode;
            lblAddressLine2.Visible = !EditMode;
            lblCity.Visible = !EditMode;
            lblCounty.Visible = !EditMode;
            lblEmailAddress.Visible = !EditMode;
            lblFaxNumber.Visible = !EditMode;
            lblFirstName.Visible = !EditMode;
            lblLabelZip.Visible = !EditMode;
            lblLastName.Visible = !EditMode;
            lblOracleCode.Visible = !EditMode;
            lblPhoneNumber.Visible = !EditMode;
            lblState.Visible = !EditMode;
            //lblType.Visible = !EditMode;
            lblVendorName.Visible = !EditMode;
            lblZip.Visible = !EditMode;

            txtAddressLine1.Visible = EditMode;
            txtAddressLine2.Visible = EditMode;
            txtCity.Visible = EditMode;
            txtCounty.Visible = EditMode;
            txtEmailAddress.Visible = EditMode;
            txtFaxNumber.Visible = EditMode;
            txtFirstName.Visible = EditMode;
            txtLabelZip.Visible = EditMode;
            txtLastName.Visible = EditMode;
            txtOracleCode.Visible = EditMode;
            txtPhoneNumber.Visible = EditMode;
            txtState.Visible = EditMode;
            //txtType.Visible = EditMode;
            txtVendorName.Visible = EditMode;
            txtZip.Visible = EditMode;
        }

        public override void DataBind() {
            if (VendorID > 0) {
                LoadData();
                SetValue();
            }
        }

        private void LoadData() {
            //Load Vendor
            dtblVendor = vendorSys.SelectOne(this.VendorID);

            postalID = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_POSTAL_ADDRESS_ID].ToString();
            phoneID = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_PHONE_NUMBER_ID].ToString();
            faxID = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_FAX_NUMBER_ID].ToString();
            emailID = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_EMAIL_ID].ToString();

            //Load Address
            if (postalID != String.Empty)
                dtblPostal = postalSys.SelectOne(Convert.ToInt32(postalID));

            //Load Phone and fax
            if (phoneID != String.Empty)
                dtblPhone = phoneSys.SelectOne(Convert.ToInt32(phoneID));
            if (faxID != String.Empty)
                dtblFax = phoneSys.SelectOne(Convert.ToInt32(faxID));

            //Load Email
            if (emailID != String.Empty)
                dtblEmail = emailSys.SelectOne(Convert.ToInt32(emailID));
        }

        private void SetValue() {
            VendorName = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_NAME].ToString();
            OracleCode = dtblVendor.Rows[0][QSPForm.Common.DataDef.VendorTable.FLD_ORACLE_VENDOR_CODE].ToString();

            if (postalID != String.Empty) {
                DataRow address = dtblPostal.Rows[0];
                AddressLine1 = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                AddressLine2 = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                City = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_CITY].ToString();
                County = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_COUNTY].ToString();
                FirstName = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                LastName = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                State = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString();
                Zip = address[QSPForm.Common.DataDef.PostalAddressEntityTable.FLD_ZIP].ToString();
            }

            if (phoneID != String.Empty)
                PhoneNumber = dtblPhone.Rows[0][QSPForm.Common.DataDef.PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
            if (faxID != String.Empty)
                FaxNumber = dtblFax.Rows[0][QSPForm.Common.DataDef.PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
            if (emailID != String.Empty)
                EmailAddress = dtblEmail.Rows[0][QSPForm.Common.DataDef.EmailEntityTable.FLD_EMAIL_ADDRESS].ToString();
        }
    } 
}