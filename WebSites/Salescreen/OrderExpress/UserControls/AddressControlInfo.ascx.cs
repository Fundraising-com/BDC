using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using QSPForm.Common;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AddressControlInfo.
    /// </summary>
    public partial class AddressControlInfo : BaseWebUserControl {
        private int c_ParentID = 0;
        private int c_AddressID = 0;
        private int c_ParentType = 0;
        private QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
        protected System.Data.DataTable tblState = new DataTable();
        protected System.Data.DataTable tblTypeAddress = new DataTable();
        bool c_HideTypeAddress = true;
        bool c_HideTitleAddress = true;
        int c_FilterTypeAddress = 0;
        protected DataView DVAddress;
        protected DataSet dtsAddress;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here								
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindDataList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dtAddress = addrSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindDataList() {
            if (dtsAddress != null) {

                PostalAddressEntityTable dTblAddress = (PostalAddressEntityTable)dtsAddress.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
                DVAddress = new DataView(dTblAddress);
                DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString() + " AND " +
                    PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + c_ParentType.ToString()
                    + " AND " + PostalAddressEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
                if (DVAddress.Count > 0) {
                    DataRow row = DVAddress[0].Row;
                    //'Table Mapping  
                    lblTitleItemNo.Text = "";
                    lblOrgName.Text = row[PostalAddressEntityTable.FLD_NAME].ToString();
                    lblFirstName.Text = row[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                    lblLastName.Text = row[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                    lblAddressLine1.Text = row[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                    lblAddressLine2.Text = row[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                    lblCity.Text = row[PostalAddressEntityTable.FLD_CITY].ToString();
                    lblCounty.Text = row[PostalAddressEntityTable.FLD_COUNTY].ToString();
                    lblState.Text = row[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1].ToString();
                    CommonUtility clsUtil = new CommonUtility();
                    if (row[PostalAddressEntityTable.FLD_ZIP].ToString().Length > 0)
                        lblZip.Text = clsUtil.FormatZipCode(row[PostalAddressEntityTable.FLD_ZIP].ToString());
                    else
                        lblZip.Text = "";
                    if (!row.IsNull(PostalAddressEntityTable.FLD_RESIDENTIAL_AREA))
                        chkBoxResidentialArea.Checked = Convert.ToBoolean(row[PostalAddressEntityTable.FLD_RESIDENTIAL_AREA]);
                }
                BindFormPhoneNumber();
                BindFormEmailAddress();
            }
        }

        private bool BindFormPhoneNumber() {
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
                    + " AND " + PhoneNumberEntityTable.FLD_ENTITY_ID + " = " + c_ParentID.ToString();
            }
            if (dvPhone.Count > 0) {
                DataRow row = dvPhone[0].Row;
                lblPhoneNumber.Text = row[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
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
                lblFaxNumber.Text = row[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();

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
                lblEmailAddress.Text = row[EmailEntityTable.FLD_EMAIL_ADDRESS].ToString();

            }

            return true;
        }

        public DataSet DataSource {
            get {
                return dtsAddress;
            }
            set {
                dtsAddress = value;
            }
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
            //4= Order
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public string LabelOrgNameText {
            get {
                return this.lblLabelOrgName.Text;
            }
            set {
                this.lblLabelOrgName.Text = value;
            }
        }

        public bool HideTypeAddress {
            get {
                return c_HideTypeAddress;
            }
            set {
                c_HideTypeAddress = value;
            }
        }

        public bool HideTitleAddress {
            get {
                return c_HideTitleAddress;
            }
            set {
                c_HideTitleAddress = value;
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

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                if (htmlTblRowTypeAddress != null) {
                    htmlTblRowTypeAddress.Visible = !c_HideTypeAddress;
                }

                if (htmlTblRowTitleAddress != null) {
                    htmlTblRowTitleAddress.Visible = !c_HideTitleAddress;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}