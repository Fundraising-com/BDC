using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AddressForm.
    /// </summary>
    public partial class AddressControlForm : BaseWebUserControl {
        private int c_ParentID = 0;
        private int c_AddressID = 0;
        private int c_ParentType = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
        protected System.Data.DataTable tblState = new DataTable();
        int c_FilterTypeAddress = 0;
        private CommonUtility clsUtil = new CommonUtility();
        protected DataView DVAddress;
        protected DataSet dtsAddress;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegExpVal_AddrL2;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegExpVal_AddrL1;
        protected bool enabled = true;
        //private bool allowPostalBox = true;

        public event System.EventHandler AddressHygieneConfirmed;
        public event System.EventHandler AddressHygieneSkipped;

        protected void Page_Load(object sender, EventArgs e) {

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
            this.AddressHygieneControl.AddressHygieneConfirmed += new System.EventHandler<AddressHygieneConfirmArgs>(this.AddressHygieneControl_AddressHygieneConfirmed);
            this.AddressHygieneControl.AddressHygieneSkipped += new System.EventHandler(this.AddressHygieneControl_AddressHygieneSkipped);
            this.AddressHygieneControl.SuggestionListItemSelected += new System.EventHandler<AddressHygieneConfirmArgs>(this.AddressHygieneControl_SuggestionListItemSelected);
            this.AddressHygieneControl.AddressHygieneServerConfirmed += new EventHandler<AddressHygieneConfirmArgs>(AddressHygieneControl_AddressHygieneServerConfirmed);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.CustVal_AddrL1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_AddrL1_ServerValidate);
            this.CustVal_AddrL2.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_AddrL2_ServerValidate);
        }
        #endregion

        protected override void OnDataBinding(System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                //BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void ddlState_DataBinding(object sender, System.EventArgs e) {
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                htmlTblAccountAddressForm.Disabled = !this.enabled;
                txtName.Enabled = this.enabled;
                txtFirstName.Enabled = this.enabled;
                txtLastName.Enabled = this.enabled;
                txtAddressLine1.Enabled = this.enabled;
                txtAddressLine2.Enabled = this.enabled;
                txtCity.Enabled = this.enabled;
                txtCounty.Enabled = this.enabled;
                ddlState.Enabled = this.enabled;
                txtZip.Enabled = this.enabled;
                txtPhoneNumber.Enabled = this.enabled;
                txtFaxNumber.Enabled = this.enabled;
                txtEmailAddress.Enabled = this.enabled;

                if (FilterTypeAddress == PostalAddressType.TYPE_BILLING) {
                    txtName.BackColor = Color.LightGray;
                }
                else {
                    txtName.BackColor = Color.Empty;
                }

                if (OrganizationNameRequired) {
                    ReqFldVal_OrgName.ErrorMessage = "The " + LabelOrgNameText + " is required.";
                }
            }
            catch (Exception ex) {
                //throw ex;
            }
        }

        public DataSet DataSource {
            get {
                return dtsAddress;
            }
            set {
                dtsAddress = value;
            }
        }

        public bool Enabled {
            get {
                return enabled;
            }
            set {
                enabled = value;
            }
        }

        public bool AllowPostalBox {
            get {
                return !(CustVal_AddrL1.Enabled && CustVal_AddrL1.Enabled);
            }
            set {
                //Disabled the CustVal if allow
                CustVal_AddrL1.Enabled = !value;
                CustVal_AddrL2.Enabled = !value;
            }
        }

        /// <summary>
        /// Defines whether the address should be validated and hygiened.
        /// </summary>
        public bool HygieneAddress {
            get {
                bool hygieneAddress = false;

                if (ViewState["HygieneAddress"] != null) {
                    hygieneAddress = Convert.ToBoolean(ViewState["HygieneAddress"]);
                }

                return hygieneAddress;
            }
            set {
                ViewState["HygieneAddress"] = value;
            }
        }

        /// <summary>
        /// Keeps track of whether an address was confirmed, which will then
        /// skip future validations.
        /// </summary>
        private bool AddressHygieneConfirmedIndicator {
            get {
                bool addressHygieneConfirmedIndicator = false;

                if (ViewState["AddressHygieneConfirmedIndicator"] != null) {
                    addressHygieneConfirmedIndicator = Convert.ToBoolean(ViewState["AddressHygieneConfirmedIndicator"]);
                }

                return addressHygieneConfirmedIndicator;
            }
            set {
                ViewState["AddressHygieneConfirmedIndicator"] = value;
            }
        }

        protected void AddressHygieneControl_AddressHygieneConfirmed(object sender, AddressHygieneConfirmArgs e) {
            AddressHygieneConfirmedIndicator = true;

            SetFieldsFromHygienedAddress(e.Address);

            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        protected void AddressHygieneControl_AddressHygieneSkipped(object sender, EventArgs e) {
            AddressHygieneConfirmedIndicator = true;

            if (AddressHygieneSkipped != null) {
                AddressHygieneSkipped(this, EventArgs.Empty);
            }
        }

        protected void AddressHygieneControl_SuggestionListItemSelected(object sender, AddressHygieneConfirmArgs e) {
            SetFieldsFromHygienedAddress(e.Address);

            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        protected void AddressHygieneControl_AddressHygieneServerConfirmed(object sender, AddressHygieneConfirmArgs e) {
            SetFieldsFromHygienedAddress(e.Address);
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dtAddress = addrSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            if (dtsAddress != null) {
                FillDataTableForDropDownList();
                PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dtsAddress.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
                DVAddress = new DataView(dtAddress);
                DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString()
                    + " AND " + PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
                if (DVAddress.Count > 0) {
                    DataRow row = DVAddress[0].Row;
                    //'Table Mapping                      			
                    txtName.Text = row[PostalAddressEntityTable.FLD_NAME].ToString();
                    txtFirstName.Text = row[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                    txtLastName.Text = row[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                    txtAddressLine1.Text = row[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                    txtAddressLine2.Text = row[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                    txtCity.Text = row[PostalAddressEntityTable.FLD_CITY].ToString();
                    txtCounty.Text = row[PostalAddressEntityTable.FLD_COUNTY].ToString();
                    ddlState.ClearSelection();
                    if (row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] != System.DBNull.Value) {
                        ListItem lstItem = ddlState.Items.FindByValue(row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString());
                        if (lstItem != null) {
                            lstItem.Selected = true;
                        }
                    }
                    CommonUtility clsUtil = new CommonUtility();
                    if (row[PostalAddressEntityTable.FLD_ZIP].ToString().Length > 0)
                        txtZip.Text = clsUtil.FormatZipCode(row[PostalAddressEntityTable.FLD_ZIP].ToString());
                    else
                        txtZip.Text = "";
                    //txtZip4.Text = row[PostalAddressEntityTable.FLD_ZIP4].ToString();
                    if (!row.IsNull(PostalAddressEntityTable.FLD_RESIDENTIAL_AREA)) {
                        chkBoxResidentialArea.Checked = Convert.ToBoolean(row[PostalAddressEntityTable.FLD_RESIDENTIAL_AREA]);
                    }
                }
                BindFormPhoneNumber();
                BindFormEmailAddress();
            }
        }

        private bool BindFormPhoneNumber() {
            CommonUtility clsUtil = new CommonUtility();
            string phoneNumber = "";
            PhoneNumberEntityTable dTblPhoneNumber = (PhoneNumberEntityTable)dtsAddress.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY];
            DataView dvPhone = new DataView(dTblPhoneNumber);
            // Phone
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_PHONE + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_PHONE + " AND " +
                        PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                        + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString(); ;
            }

            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                phoneNumber = "";
                if (!row.IsNull(PhoneNumberEntityTable.FLD_PHONE_NUMBER)) {
                    phoneNumber = clsUtil.FormatPhoneNumber(row[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString());
                }

                txtPhoneNumber.Text = phoneNumber;
            }

            //Fax
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                phoneNumber = "";
                DataRow row = dvPhone[0].Row;
                if (!row.IsNull(PhoneNumberEntityTable.FLD_PHONE_NUMBER)) {
                    phoneNumber = clsUtil.FormatPhoneNumber(row[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString());
                }
                txtFaxNumber.Text = phoneNumber;
            }

            return true;
        }

        private bool BindFormEmailAddress() {
            EmailEntityTable dTblEmailAddress = (EmailEntityTable)dtsAddress.Tables[EmailEntityTable.TBL_EMAIL_ENTITY];
            DataView dvEmail = new DataView(dTblEmailAddress);
            //Corporate Email Address
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_BILLING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_SHIPPING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }

            if (dvEmail.Count > 0) {
                DataRow row = dvEmail[0].Row;
                txtEmailAddress.Text = row[EmailEntityTable.FLD_EMAIL_ADDRESS].ToString();
            }

            return true;
        }

        public int AddressID {
            get {
                return c_AddressID;
            }
            set {
                c_AddressID = value;
            }
        }

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public int ParentType {
            //Identify on wich we have to do our operation
            //0= Nothing (direct to the postal address table)
            //1= Organization
            //2= Account
            //3= Campaign
            //4= Order billing
            //5= Order shipping
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public int FilterTypeAddress {
            get {
                return c_FilterTypeAddress;
            }
            set {
                c_FilterTypeAddress = value;
            }
        }

        //public string HiddenListID
        //{
        //    get{return this.hidFieldID.ClientID;}
        //}

        public HtmlTable AddressTable {
            get {
                return this.htmlTblAccountAddressForm;
            }
        }

        public TextBox TextBoxOrgName {
            get {
                return this.txtName;
            }
        }

        public string LabelOrgNameText {
            get {
                return this.lblOrgName.Text;
            }
            set {
                this.lblOrgName.Text = value;
            }
        }

        public bool OrganizationNameRequired {
            get {
                return ReqFldVal_OrgName.Enabled;
            }
            set {
                ReqFldVal_OrgName.Enabled = value;
                ReqFldVal_OrgName.Visible = value;
                OrgNameRequiredIndicatorLabel.Visible = value;
            }
        }

        private void FillDataTableForDropDownList() {
            try {
                clsUtil.SetUSSubdivisionDropDownList(ddlState, true);
                //				QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //				//State
                //				tblState = comSys.SelectAllUSState();
                //				DataRow row = tblState.NewRow();
                //				row[0] = "";
                //				row[1] = "Not Specified";
                //				tblState.Rows.InsertAt(row, 0);
                //				ddlState.DataSource = tblState;
                //				ddlState.DataBind();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public override bool IsValid() {
            bool isValid = base.IsValid();
            QSPForm.Business.com.ses.ws.AddressHygieneService.Address address;

            if (isValid && HygieneAddress && !AddressHygieneConfirmedIndicator) {
                address = GetAddressHygieneAddress();

                isValid = (AddressHygieneControl.ValidateAddress(address) == AddressHygieneResult.ValidUnchanged);
            }

            return isValid;
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dtsAddress.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];

                DVAddress = new DataView(dtAddress);
                DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString() + " AND " +
                    PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
                if (DVAddress.Count > 0) {
                    DataRow row = DVAddress[0].Row;
                    //'Table Mapping  
                    //verification of all value before replacement                    
                    //entity (Account_ID, Order_ID, Organization_ID, etc...)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_ID, c_ParentID.ToString());
                    //entity type (Account, Order, Organization, etc...)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, c_ParentType.ToString());
                    //Address type (Billing, Shipping)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_TYPE, c_FilterTypeAddress.ToString());
                    //Contact Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_NAME, txtName.Text.Trim());
                    //Contact Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_FIRST_NAME, txtFirstName.Text.Trim());
                    //Contact Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_LAST_NAME, txtLastName.Text.Trim());
                    //Address Line 1
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS1, txtAddressLine1.Text.Trim());
                    //Address Line 2
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS2, txtAddressLine2.Text.Trim());
                    //City
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_CITY, txtCity.Text.Trim());
                    //County
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_COUNTY, txtCounty.Text.Trim());
                    //State
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_CODE, ddlState.SelectedItem.Value.Trim());
                    //State Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1, ddlState.SelectedItem.Text.Trim());
                    //Zip Code
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ZIP, txtZip.Text.Trim());
                    //					//Zip4 Code
                    //					clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ZIP4, txtZip4.Text.Trim());
                    //Residential area
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_RESIDENTIAL_AREA, chkBoxResidentialArea.Checked.ToString());


                    if (row.RowState != DataRowState.Unchanged) {
                        if (row.RowState == DataRowState.Added)
                            row[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                        else
                            row[PostalAddressEntityTable.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }
                else {
                    DataRow newRow = dtAddress.NewRow();
                    newRow[PostalAddressEntityTable.FLD_ENTITY_ID] = c_ParentID.ToString();
                    //entity type (Account, Order, Organization, etc...)
                    newRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = c_ParentType.ToString();
                    //Address type (Billing, Shipping)
                    newRow[PostalAddressEntityTable.FLD_TYPE] = c_FilterTypeAddress.ToString();
                    //Contact Name
                    newRow[PostalAddressEntityTable.FLD_NAME] = txtName.Text.Trim();
                    //Contact Name
                    newRow[PostalAddressEntityTable.FLD_FIRST_NAME] = txtFirstName.Text.Trim();
                    //Contact Name
                    newRow[PostalAddressEntityTable.FLD_LAST_NAME] = txtLastName.Text.Trim();
                    //Address Line 1
                    newRow[PostalAddressEntityTable.FLD_ADDRESS1] = txtAddressLine1.Text.Trim();
                    //Address Line 2
                    newRow[PostalAddressEntityTable.FLD_ADDRESS2] = txtAddressLine2.Text.Trim();
                    //City
                    newRow[PostalAddressEntityTable.FLD_CITY] = txtCity.Text.Trim();
                    //County
                    newRow[PostalAddressEntityTable.FLD_COUNTY] = txtCounty.Text.Trim();
                    //State
                    newRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = ddlState.SelectedItem.Value.Trim();
                    //State Name
                    newRow[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1] = ddlState.SelectedItem.Text.Trim();
                    //Zip Code
                    newRow[PostalAddressEntityTable.FLD_ZIP] = txtZip.Text.Trim();
                    //					//Zip4 Code
                    //					newRow[PostalAddressEntityTable.FLD_ZIP4] = txtZip4.Text.Trim();					
                    //Residential area
                    newRow[PostalAddressEntityTable.FLD_RESIDENTIAL_AREA] = chkBoxResidentialArea.Checked;

                    newRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                    dtAddress.Rows.Add(newRow);
                }

                blnValid = true;

                if (blnValid) {
                    blnValid = UpdatePhoneNumber();
                    if (blnValid) {
                        blnValid = UpdateEmailAddress();
                    }

                    AddressHygieneConfirmedIndicator = false;
                }
            }
            catch (Exception ex) {
                blnValid = false;
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private bool UpdatePhoneNumber() {
            PhoneNumberEntityTable dTblPhoneNumber = (PhoneNumberEntityTable)dtsAddress.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY];
            DataView dvPhone = new DataView(dTblPhoneNumber);
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_PHONE + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_PHONE + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                if (txtPhoneNumber.Text.Trim().Length > 0) {
                    clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtPhoneNumber.Text.Trim());
                }
                else {
                    row.Delete();
                }
            }
            else {
                if (txtPhoneNumber.Text.Length > 0) {
                    int phoneType = 0;
                    if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
                        phoneType = PhoneNumberType.TYPE_BILLING_PHONE;
                    else
                        phoneType = PhoneNumberType.TYPE_SHIPPING_PHONE;
                    DataRow newRow = dTblPhoneNumber.NewRow();
                    newRow[PhoneNumberEntityTable.FLD_TYPE] = phoneType; //Corporate
                    newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtPhoneNumber.Text;
                    newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = c_ParentID;
                    newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = c_ParentType;
                    newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                    dTblPhoneNumber.Rows.Add(newRow);
                }
            }

            //Fax
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                if (txtFaxNumber.Text.Trim().Length > 0) {
                    clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtFaxNumber.Text.Trim());
                }
                else {
                    row.Delete();
                }
            }
            else {
                if (txtFaxNumber.Text.Length > 0) {
                    int phoneType = 0;
                    if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
                        phoneType = PhoneNumberType.TYPE_BILLING_FAX;
                    else
                        phoneType = PhoneNumberType.TYPE_SHIPPING_FAX;
                    DataRow newRow = dTblPhoneNumber.NewRow();
                    newRow[PhoneNumberEntityTable.FLD_TYPE] = phoneType; //Corporate
                    newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtFaxNumber.Text;
                    newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = c_ParentID;
                    newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = c_ParentType;
                    newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                    dTblPhoneNumber.Rows.Add(newRow);
                }
            }

            return true;
        }

        private bool UpdateEmailAddress() {
            EmailEntityTable dTblEmailAddress = (EmailEntityTable)dtsAddress.Tables[EmailEntityTable.TBL_EMAIL_ENTITY];
            DataView dvEmail = new DataView(dTblEmailAddress);
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_BILLING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_SHIPPING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvEmail.Count > 0) {
                DataRow row = dvEmail[0].Row;
                if (txtEmailAddress.Text.Trim().Length > 0) {
                    clsUtil.UpdateRow(row, EmailEntityTable.FLD_EMAIL_ADDRESS, txtEmailAddress.Text.Trim());
                }
                else {
                    row.Delete();
                }
            }
            else {
                if (txtEmailAddress.Text.Trim().Length > 0) {
                    int emailType = 0;
                    if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
                        emailType = EmailType.TYPE_BILLING;
                    else
                        emailType = EmailType.TYPE_SHIPPING;

                    DataRow newRow = dTblEmailAddress.NewRow();
                    newRow[EmailEntityTable.FLD_TYPE] = emailType; //Billing
                    newRow[EmailEntityTable.FLD_EMAIL_ADDRESS] = txtEmailAddress.Text;
                    newRow[EmailEntityTable.FLD_ENTITY_ID] = c_ParentID;
                    newRow[EmailEntityTable.FLD_ENTITY_TYPE_ID] = c_ParentType;
                    newRow[EmailEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                    dTblEmailAddress.Rows.Add(newRow);
                }
            }

            return true;
        }

        public bool DeleteDataSource() {
            bool blnValid = false;

            try {
                PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dtsAddress.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];

                DVAddress = new DataView(dtAddress);
                if (c_FilterTypeAddress > 0)
                    DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString() + " AND " +
                        PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                        + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();

                if (DVAddress.Count > 0) {
                    DataRow row = DVAddress[0].Row;
                    if (row.RowState != DataRowState.Added)
                        row[PostalAddressEntityTable.FLD_UPDATE_USER_ID] = Page.UserID;
                    row.Delete();
                }

                blnValid = true;

                if (blnValid) {
                    blnValid = DeletePhoneNumber();
                    if (blnValid) {
                        blnValid = DeleteEmailAddress();
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private bool DeletePhoneNumber() {
            PhoneNumberEntityTable dTblPhoneNumber = (PhoneNumberEntityTable)dtsAddress.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY];
            DataView dvPhone = new DataView(dTblPhoneNumber);
            //Phone
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_PHONE + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_PHONE + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                row.Delete();
            }

            //Fax
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_BILLING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvPhone.RowFilter = PhoneNumberEntityTable.FLD_TYPE + " = " + PhoneNumberType.TYPE_SHIPPING_FAX + " AND " +
                    PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                row.Delete();
                txtPhoneNumber.Text = "";
            }

            return true;
        }

        private bool DeleteEmailAddress() {
            EmailEntityTable dTblEmailAddress = (EmailEntityTable)dtsAddress.Tables[EmailEntityTable.TBL_EMAIL_ENTITY];
            DataView dvEmail = new DataView(dTblEmailAddress);
            //Corporate Email Address
            if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_BILLING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
			{
                dvEmail.RowFilter = EmailEntityTable.FLD_TYPE + " = " + EmailType.TYPE_SHIPPING + " AND " +
                    EmailEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + EmailEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvEmail.Count > 0) {
                DataRow row = dvEmail[0].Row;
                row.Delete();
                txtEmailAddress.Text = "";
            }

            return true;
        }

        public bool CopyAddressFrom(int TypeAddress, int EntityType, int EntityID) {
            bool blnValid = false;

            try {
                PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dtsAddress.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
                addrSys.CopyToEntity(dtAddress, this.Page.UserID, EntityType, EntityID, TypeAddress, c_ParentType, c_ParentID, c_FilterTypeAddress);

                PhoneNumberEntityTable dTblPhone = (PhoneNumberEntityTable)dtsAddress.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY];
                PhoneNumberSystem phoneSys = new PhoneNumberSystem();

                if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
				{
                    phoneSys.CopyToEntity(dTblPhone, this.Page.UserID, EntityType, EntityID, PhoneNumberType.TYPE_BILLING_PHONE,
                                            c_ParentType, c_ParentID, PhoneNumberType.TYPE_BILLING_PHONE);

                    phoneSys.CopyToEntity(dTblPhone, this.Page.UserID, EntityType, EntityID, PhoneNumberType.TYPE_BILLING_FAX,
                                            c_ParentType, c_ParentID, PhoneNumberType.TYPE_BILLING_FAX);
                }
                else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
				{
                    phoneSys.CopyToEntity(dTblPhone, this.Page.UserID, EntityType, EntityID, PhoneNumberType.TYPE_BILLING_PHONE,
                        c_ParentType, c_ParentID, PhoneNumberType.TYPE_SHIPPING_PHONE);

                    phoneSys.CopyToEntity(dTblPhone, this.Page.UserID, EntityType, EntityID, PhoneNumberType.TYPE_BILLING_FAX,
                        c_ParentType, c_ParentID, PhoneNumberType.TYPE_SHIPPING_FAX);
                }

                EmailEntityTable dTblEmail = (EmailEntityTable)dtsAddress.Tables[EmailEntityTable.TBL_EMAIL_ENTITY];
                EmailAddressSystem emailSys = new EmailAddressSystem();

                if (c_FilterTypeAddress == PostalAddressType.TYPE_BILLING) //Billing
				{
                    emailSys.CopyToEntity(dTblEmail, this.Page.UserID, EntityType, EntityID, EmailType.TYPE_BILLING,
                        c_ParentType, c_ParentID, EmailType.TYPE_BILLING);
                }
                else if (c_FilterTypeAddress == PostalAddressType.TYPE_SHIPPING) //Shipping
				{
                    emailSys.CopyToEntity(dTblEmail, this.Page.UserID, EntityType, EntityID, EmailType.TYPE_BILLING,
                        c_ParentType, c_ParentID, EmailType.TYPE_SHIPPING);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        //private void WriteFieldID()
        //{
        //    string listID;
        //    listID = this.txtAddressLine1.ClientID;
        //    listID += "," + this.txtAddressLine2.ClientID;
        //    listID += "," + this.txtCity.ClientID;
        //    listID += "," + this.txtCounty.ClientID;
        //    listID += "," + this.txtEmailAddress.ClientID;
        //    listID += "," + this.txtFaxNumber.ClientID;
        //    listID += "," + this.txtFirstName.ClientID;
        //    listID += "," + this.txtLastName.ClientID;
        //    listID += "," + this.txtName.ClientID;
        //    listID += "," + this.txtPhoneNumber.ClientID;
        //    listID += "," + this.txtZip.ClientID;

        //    this.hidFieldID.Value = listID;
        //}

        private void CustVal_AddrL1_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) {
            args.IsValid = IsValidAddressLine(args.Value);
        }

        private void CustVal_AddrL2_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) {
            args.IsValid = IsValidAddressLine(args.Value);
        }

        private bool IsValidAddressLine(string sAddress) {
            string[] arrInvalidAddress = { "Postal Box", "Post Box", "Po Box", "p.b" };
            bool IsFound = false;

            //			if (!allowPostalBox)
            //			{
            for (int iCount = 0; iCount < arrInvalidAddress.Length; iCount++) {
                string sInvalidAddress = arrInvalidAddress[iCount].ToUpper();
                int iIndex = sAddress.ToUpper().IndexOf(sInvalidAddress);
                if (iIndex > -1) {
                    IsFound = true;
                    break;
                }

            }
            //			}

            return !IsFound;
        }

        /// <summary>
        /// Returns the entered address in the Address Hygiene's address class.
        /// </summary>
        /// <returns>Address Hygiene's address</returns>
        public QSPForm.Business.com.ses.ws.AddressHygieneService.Address GetAddressHygieneAddress() {
            QSPForm.Business.com.ses.ws.AddressHygieneService.Address address = new QSPForm.Business.com.ses.ws.AddressHygieneService.Address();

            address.Address1 = txtAddressLine1.Text.Trim();
            address.Address2 = txtAddressLine2.Text.Trim();
            address.City = txtCity.Text.Trim();
            address.County = txtCounty.Text.Trim();
            if (!ddlState.SelectedValue.Equals(string.Empty))
                address.Region = ddlState.SelectedValue.Substring(3);

            if (txtZip.Text.Contains("-")) {
                address.PostCode = txtZip.Text.Substring(0, txtZip.Text.IndexOf("-"));
                address.PostCode2 = txtZip.Text.Substring(txtZip.Text.IndexOf("-") + 1).Trim();
            }
            else {
                address.PostCode = txtZip.Text.Trim();
                address.PostCode2 = String.Empty;
            }
            if (!ddlState.SelectedValue.Equals(string.Empty))
                address.Country = ddlState.SelectedValue.Substring(0, 2);

            return address;
        }

        /// <summary>
        /// Sets fields with address from the Address Hygiene Web Service.
        /// </summary>
        /// <param name="address">Address to set the fields with</param>
        private void SetFieldsFromHygienedAddress(QSPForm.Business.com.ses.ws.AddressHygieneService.Address address) {
            txtAddressLine1.Text = address.Address1;
            txtAddressLine2.Text = address.Address2;
            txtCity.Text = address.City;
            txtCounty.Text = address.County;
            // Only US for Order Express
            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue("US-" + address.Region));
            txtZip.Text = address.PostCode + (address.PostCode2 != String.Empty ? ("-" + address.PostCode2) : String.Empty);
        }

        public QSPForm.Business.com.qsp.ws.AccountFinderService.Address GetMatchingAccountsAddress() {
            QSPForm.Business.com.qsp.ws.AccountFinderService.Address address = new QSPForm.Business.com.qsp.ws.AccountFinderService.Address();

            address.Address1 = txtAddressLine1.Text.Trim();
            address.Address2 = txtAddressLine2.Text.Trim();
            address.City = txtCity.Text.Trim();
            address.County = txtCounty.Text.Trim();
            address.Region = ddlState.SelectedValue.Substring(3);

            if (txtZip.Text.Contains("-")) {
                address.PostCode = txtZip.Text.Substring(0, txtZip.Text.IndexOf("-"));
                address.PostCode2 = txtZip.Text.Substring(txtZip.Text.IndexOf("-") + 1).Trim();
            }
            else {
                address.PostCode = txtZip.Text.Trim();
                address.PostCode2 = String.Empty;
            }
            address.Country = ddlState.SelectedValue.Substring(0, 2);

            return address;
        }

        public void ResetStatus() {
            AddressHygieneConfirmedIndicator = false;
        }
    }
}