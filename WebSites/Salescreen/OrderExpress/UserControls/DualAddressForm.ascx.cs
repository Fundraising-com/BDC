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
using QSP.OrderExpress.Web;
using QSPForm.Common;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    public partial class DualAddressForm : System.Web.UI.UserControl {
        CommonUtility clsUtil = new CommonUtility();
        DataSet dataSource = null;

        public event System.EventHandler AddressHygieneConfirmed;

        public string EntityName {
            get {
                return BillingAddressControlForm.LabelOrgNameText;
            }
            set {
                BillingAddressControlForm.LabelOrgNameText = value;
                ShippingAddressControlForm.LabelOrgNameText = value;
            }
        }

        public TextBox EntityNameTextBox {
            get {
                return BillingAddressControlForm.TextBoxOrgName;
            }
        }

        public bool BillToAdressNote {
            set {
                trBillToAdressLabel.Visible = value;
            }
        }

        public bool HygieneAddress {
            get {
                return BillingAddressControlForm.HygieneAddress;
            }
            set {
                BillingAddressControlForm.HygieneAddress = value;
                ShippingAddressControlForm.HygieneAddress = value;
            }
        }

        public DataSet BillingDataSource {
            get {
                return BillingAddressControlForm.DataSource;
            }
            set {
                BillingAddressControlForm.DataSource = value;
            }
        }

        public DataSet ShippingDataSource {
            get {
                return ShippingAddressControlForm.DataSource;
            }
            set {
                ShippingAddressControlForm.DataSource = value;
            }
        }

        public bool BillingEnabled {
            get {
                return BillingAddressControlForm.Enabled;
            }
            set {
                BillingAddressControlForm.Enabled = value;
            }
        }

        public bool ShippingEnabled {
            get {
                return ShippingAddressControlForm.Enabled;
            }
            set {
                ShippingAddressControlForm.Enabled = value;
            }
        }

        public int BillingParentID {
            get {
                int billingParentID = 0;

                if (ViewState["BillingParentID"] != null) {
                    billingParentID = Convert.ToInt32(ViewState["BillingParentID"]);
                }

                return billingParentID;
            }
            set {
                ViewState["BillingParentID"] = value;
            }
        }

        public int ShippingParentID {
            get {
                int shippingParentID = 0;

                if (ViewState["ShippingParentID"] != null) {
                    shippingParentID = Convert.ToInt32(ViewState["ShippingParentID"]);
                }

                return shippingParentID;
            }
            set {
                ViewState["ShippingParentID"] = value;
            }
        }

        public int BillingParentType {
            get {
                int billingParentType = 0;

                if (ViewState["BillingParentType"] != null) {
                    billingParentType = Convert.ToInt32(ViewState["BillingParentType"]);
                }

                return billingParentType;
            }
            set {
                ViewState["BillingParentType"] = value;
            }
        }

        public int ShippingParentType {
            get {
                int shippingParentType = 0;

                if (ViewState["ShippingParentType"] != null) {
                    shippingParentType = Convert.ToInt32(ViewState["ShippingParentType"]);
                }

                return shippingParentType;
            }
            set {
                ViewState["ShippingParentType"] = value;
            }
        }

        public DataSet DataSource {
            get {
                return dataSource;
            }
            set {
                dataSource = value;
            }
        }

        private bool IsSameInvalidAddress {
            get {
                bool isSameInvalidAddress = false;

                if (ViewState["IsSameInvalidAddress"] != null) {
                    isSameInvalidAddress = Convert.ToBoolean(ViewState["IsSameInvalidAddress"]);
                }

                return isSameInvalidAddress;
            }
            set {
                ViewState["IsSameInvalidAddress"] = value;
            }
        }

        protected override void OnInit(EventArgs e) {
            this.imgBtnCopyAddress.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnCopyAddress_Click);
            this.BillingAddressControlForm.AddressHygieneConfirmed += new EventHandler(AddressControlForm_AddressHygieneConfirmed);
            this.BillingAddressControlForm.AddressHygieneSkipped += new EventHandler(AddressControlForm_AddressHygieneSkipped);
            this.ShippingAddressControlForm.AddressHygieneConfirmed += new EventHandler(AddressControlForm_AddressHygieneConfirmed);
            this.ShippingAddressControlForm.AddressHygieneSkipped += new EventHandler(AddressControlForm_AddressHygieneSkipped);
            base.OnInit(e);
        }

        protected void AddressControlForm_AddressHygieneConfirmed(object sender, EventArgs e) {
            if (BillingEnabled && IsSameInvalidAddress) {
                CopyBillingToShipping();
            }

            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        protected void AddressControlForm_AddressHygieneSkipped(object sender, EventArgs e) {
            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        private void imgBtnCopyAddress_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            CopyBillingToShipping();

            this.Page.MaintainScrollPositionOnPostBack = true;
        }

        private void TrimTextInDataSource() {
            try {
                foreach (DataRow row in DataSource.Tables[QSPForm.Common.DataDef.PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY].Rows) {

                    if (row[7] != null) {
                        row[7] = row[7].ToString().Trim();
                    }

                    if (row[8] != null) {
                        row[8] = row[8].ToString().Trim();
                    }

                    if (row[9] != null) {
                        row[9] = row[9].ToString().Trim();
                    }

                    if (row[10] != null) {
                        row[10] = row[10].ToString().Trim();
                    }

                    if (row[11] != null) {
                        row[11] = row[11].ToString().Trim();
                    }

                    if (row[12] != null) {
                        row[12] = row[12].ToString().Trim();
                    }

                    if (row[14] != null) {
                        row[14] = row[14].ToString().Trim();
                    }

                    if (row[15] != null) {
                        row[15] = row[15].ToString().Trim();
                    }

                    if (row[17] != null) {
                        row[17] = row[17].ToString().Trim();
                    }

                }
            }
            catch (Exception ex) {
            }
        }

        private void InitializeControls() {
            this.TrimTextInDataSource();

            BillingAddressControlForm.ParentID = BillingParentID;
            BillingAddressControlForm.ParentType = BillingParentType;
            BillingAddressControlForm.FilterTypeAddress = PostalAddressType.TYPE_BILLING;
            BillingAddressControlForm.DataSource = DataSource;

            ShippingAddressControlForm.ParentID = ShippingParentID;
            ShippingAddressControlForm.ParentType = ShippingParentType;
            ShippingAddressControlForm.FilterTypeAddress = PostalAddressType.TYPE_SHIPPING;
            ShippingAddressControlForm.DataSource = DataSource;
        }

        public void DataBind() {
            InitializeControls();

            if (DataSource != null) {
                BillingAddressControlForm.BindForm();
                ShippingAddressControlForm.BindForm();
            }
        }

        public bool IsValid() {
            bool isValid = true;

            trValSumAddressInfo.Visible = false;

            if (BillingEnabled && !BillingAddressControlForm.IsValid()) {
                IsSameInvalidAddress = BillingAddressControlForm.GetAddressHygieneAddress().Equals(ShippingAddressControlForm.GetAddressHygieneAddress());

                trValSumAddressInfo.Visible = true;
                clsUtil.RenderStartUpScroll(ValSumAddressInfo);
                this.Page.MaintainScrollPositionOnPostBack = false;
                isValid = false;
            }

            if (ShippingEnabled && isValid && !IsSameInvalidAddress && !ShippingAddressControlForm.IsValid()) {
                trValSumAddressInfo.Visible = true;
                clsUtil.RenderStartUpScroll(ValSumAddressInfo);
                this.Page.MaintainScrollPositionOnPostBack = false;
                isValid = false;
            }

            return isValid;
        }

        public bool Update() {
            bool isValid = true;

            InitializeControls();

            if (BillingEnabled) {
                isValid &= BillingAddressControlForm.UpdateDataSource();
            }

            if (ShippingEnabled) {
                isValid &= ShippingAddressControlForm.UpdateDataSource(); ;
            }

            return isValid;
        }

        public bool Delete() {
            bool isValid = true;

            InitializeControls();

            if (BillingEnabled) {
                isValid &= BillingAddressControlForm.DeleteDataSource();
            }

            if (ShippingEnabled) {
                isValid &= ShippingAddressControlForm.DeleteDataSource();
            }

            return isValid;
        }

        private void CopyBillingToShipping() {
            InitializeControls();

            BillingAddressControlForm.UpdateDataSource();

            ShippingAddressControlForm.CopyAddressFrom(PostalAddressType.TYPE_BILLING, BillingParentType, BillingParentID);
            ShippingAddressControlForm.BindForm();
        }

        public QSPForm.Business.com.qsp.ws.AccountFinderService.Address GetMatchingAccountsShippingAddress() {
            return ShippingAddressControlForm.GetMatchingAccountsAddress();
        }

        public void ResetStatus() {
            BillingAddressControlForm.ResetStatus();
            ShippingAddressControlForm.ResetStatus();
        }
    } 
}