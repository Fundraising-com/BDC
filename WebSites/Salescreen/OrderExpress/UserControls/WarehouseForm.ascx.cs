using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.WarehouseData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for WarehouseForm.
    /// </summary>
    public partial class WarehouseForm : BaseWebFormControl {
        protected dataDef dtsWarehouse;
        private int c_WarehouseID = 0;
        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                //FillDataTableForDropDownList();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //			InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int WarehouseID {
            get {
                return c_WarehouseID;
            }
            set {
                c_WarehouseID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtsWarehouse;
            }
            set {
                dtsWarehouse = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public new void BindForm() {
            if (!IsPostBack)
                FillDataTableForDropDownList();

            DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];
            //First section -- Account Name and Billing Address
            if (wareRow.RowState == DataRowState.Added)
                lblWarehouseID.Text = "New Warehouse";
            else
                lblWarehouseID.Text = wareRow[WarehouseTable.FLD_PKID].ToString();

            if (wareRow.IsNull(WarehouseTable.FLD_FULF_WAREHOUSE_ID))
                lblFulfWarehouseID.Text = "New Warehouse";
            else
                lblFulfWarehouseID.Text = wareRow[WarehouseTable.FLD_FULF_WAREHOUSE_ID].ToString();

            //Warehouse Status
            if (wareRow.RowState == DataRowState.Added) {
                lblWarehouseStatusColor.BackColor = Color.White;
                lblWarehouseStatus.Text = "New Warehouse";
            }
            else {
                //Already Existing Warehouse					
                lblWarehouseStatus.Text = wareRow[WarehouseTable.FLD_WAREHOUSE_STATUS_NAME].ToString();
                lblWarehouseStatusColor.BackColor = Color.FromName(wareRow[WarehouseTable.FLD_WAREHOUSE_STATUS_COLOR_CODE].ToString());
            }

            txtWarehouseName.Text = wareRow[WarehouseTable.FLD_NAME].ToString();

            if (!wareRow.IsNull(WarehouseTable.FLD_VENDOR_ID)) {
                ListItem lstItem = ddlVendor.Items.FindByValue(wareRow[WarehouseTable.FLD_VENDOR_ID].ToString());
                if (lstItem != null) {
                    ddlVendor.ClearSelection();
                    lstItem.Selected = true;
                }
            }
            if (!wareRow.IsNull(WarehouseTable.FLD_PICK_UP)) {
                chkPickUp.Checked = Convert.ToBoolean(wareRow[WarehouseTable.FLD_PICK_UP]);
            }

            PostalAddressSystem addSys = new PostalAddressSystem();
            DataRow rowAddress = addSys.FindRow(dtsWarehouse.PostalAddress,
                QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                c_WarehouseID,
                QSPForm.Common.PostalAddressType.TYPE_BILLING);
            if (rowAddress != null) {
                txtCompanyName.Text = rowAddress[PostalAddressEntityTable.FLD_NAME].ToString();
                txtFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                txtLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                txtAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                txtAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                txtCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                txtCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                if (!rowAddress.IsNull(PostalAddressEntityTable.FLD_SUBDIVISION_CODE)) {
                    ListItem lstItem = ddlState.Items.FindByValue(rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString());
                    if (lstItem != null) {
                        ddlState.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
                txtZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
            }

            BindFormPhoneNumber();
            BindFormEmail();
        }

        private bool BindFormPhoneNumber() {
            //-----------------------------------------
            // WAREHOUSE PHONE NUMBER
            //-----------------------------------------
            if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                PhoneNumberEntityTable dTblPhoneNumber = dtsWarehouse.PhoneNumber;
                PhoneNumberSystem phoneSys = new PhoneNumberSystem();

                //-----------------------------------------
                // General Phone Number-
                //-----------------------------------------
                DataRow rowPhone = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    c_WarehouseID,
                    QSPForm.Common.PhoneNumberType.TYPE_PHONE_NUMBER);
                if (rowPhone != null) {
                    string phoneNumber = "";
                    phoneNumber = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                    if (phoneNumber.Length > 0)
                        txtPhoneNumber.Text = clsUtil.FormatPhoneNumber(phoneNumber);
                }
                //--------------------------------------		
                // General Fax Number-
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    c_WarehouseID,
                    QSPForm.Common.PhoneNumberType.TYPE_FAX_NUMBER);
                if (rowPhone != null) {
                    string phoneNumber = "";
                    phoneNumber = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                    if (phoneNumber.Length > 0)
                        txtFaxNumber.Text = clsUtil.FormatPhoneNumber(phoneNumber);
                }

                //--------------------------------------		
                // General Fax Number-
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    c_WarehouseID,
                    QSPForm.Common.PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER);
                if (rowPhone != null) {
                    string phoneNumber = "";
                    phoneNumber = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                    if (phoneNumber.Length > 0)
                        txtReceivingPhoneNumber.Text = clsUtil.FormatPhoneNumber(phoneNumber);
                }
            }

            return true;
        }

        private bool BindFormEmail() {
            //-----------------------------------------
            // WAREHOUSE PHONE NUMBER
            //-----------------------------------------
            if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                EmailEntityTable dTblEmailAddress = dtsWarehouse.EmailAddress;
                EmailAddressSystem emailSys = new EmailAddressSystem();
                //-----------------------------------------
                // General Email Address
                //-----------------------------------------
                DataRow rowEmail = emailSys.FindRow(dTblEmailAddress,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    c_WarehouseID,
                    QSPForm.Common.EmailType.TYPE_CORPORATIVE);
                if (rowEmail != null) {
                    txtEmailAddress.Text = rowEmail[EmailEntityTable.FLD_EMAIL_ADDRESS].ToString();
                }
            }

            return true;
        }

        public bool ValidateForm() {
            if (!IsValid()) {
                return false;
            }

            //if everything have been ok
            return true;
        }

        public bool UpdateDataSource() {
            PostalAddressSystem addSys = new PostalAddressSystem();
            DataView DVAddress = new DataView(dtsWarehouse.PostalAddress);
            DataRow row;
            if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];
                //-------------------------------------------------
                //		Wrehouse Information
                //-------------------------------------------------

                int WareID = Convert.ToInt32(wareRow[WarehouseTable.FLD_PKID]);

                clsUtil.UpdateRow(wareRow, WarehouseTable.FLD_NAME, txtWarehouseName.Text);
                string sVendorId = "";
                if (ddlVendor.SelectedIndex > 0)
                    sVendorId = ddlVendor.SelectedValue;
                clsUtil.UpdateRow(wareRow, WarehouseTable.FLD_VENDOR_ID, sVendorId);
                clsUtil.UpdateRow(wareRow, WarehouseTable.FLD_PICK_UP, chkPickUp.Checked.ToString());

                //-------------------------------------------------
                //		Postal Address
                //-------------------------------------------------

                row = addSys.FindRow(dtsWarehouse.PostalAddress,
                                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                    WareID,
                                    QSPForm.Common.PostalAddressType.TYPE_BILLING);
                if (row != null) {
                    //'Table Mapping  
                    //verification of all value before replacement                    
                    //entity (Account_ID, Order_ID, Organization_ID, etc...)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_ID, WareID.ToString());
                    //entity type (Account, Order, Organization, etc...)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, EntityType.TYPE_WAREHOUSE.ToString());
                    //Address type (Billing, Shipping)
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_TYPE, PostalAddressType.TYPE_BILLING.ToString());
                    //Company Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_NAME, txtCompanyName.Text);
                    //Contact First Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_FIRST_NAME, txtFirstName.Text);
                    //Contact Last Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_LAST_NAME, txtLastName.Text);
                    //Address Line 1
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS1, txtAddressLine1.Text);
                    //Address Line 2
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS2, txtAddressLine2.Text);
                    //City
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_CITY, txtCity.Text);
                    //County
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_COUNTY, txtCounty.Text);
                    //State
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_CODE, ddlState.SelectedItem.Value);
                    //State Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1, ddlState.SelectedItem.Text);
                    //Zip Code
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ZIP, txtZip.Text.Trim());

                    if (row.RowState == DataRowState.Added)
                        row[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                    else
                        clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());
                }
                else {
                    DataRow newRow = dtsWarehouse.PostalAddress.NewRow();
                    newRow[PostalAddressEntityTable.FLD_ENTITY_ID] = WareID.ToString();
                    //entity type (Account, Order, Organization, etc...)
                    newRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE.ToString();
                    //Address type (Billing, Shipping)
                    newRow[PostalAddressEntityTable.FLD_TYPE] = PostalAddressType.TYPE_BILLING.ToString();
                    //Company Name
                    newRow[PostalAddressEntityTable.FLD_NAME] = txtCompanyName.Text;
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
                    newRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = ddlState.SelectedItem.Value;
                    //State Name
                    newRow[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1] = ddlState.SelectedItem.Text;
                    //Zip Code
                    newRow[PostalAddressEntityTable.FLD_ZIP] = txtZip.Text.Trim();

                    newRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                    dtsWarehouse.PostalAddress.Rows.Add(newRow);
                }

                //Update Phone of the Warehouse
                UpdatePhoneNumber();

                //Update Phone of the Warehouse
                UpdateEmailAddress();

                //Maintain globally the update information
                if (wareRow.RowState == DataRowState.Added)
                    wareRow[WarehouseTable.FLD_CREATE_USER_ID] = Page.UserID;
                else
                    clsUtil.UpdateRow(wareRow, WarehouseTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());

            }

            return true;
        }

        private bool UpdatePhoneNumber() {
            PhoneNumberEntityTable dTblPhoneNumber = dtsWarehouse.PhoneNumber;
            PhoneNumberSystem phoneSys = new PhoneNumberSystem();
            //-----------------------------------------
            // CREDIT APPLICATION
            //-----------------------------------------
            if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];
                int wareID = Convert.ToInt32(wareRow[WarehouseTable.FLD_PKID]);

                //-----------------------------------------
                // Warehouse Phone Number
                //-----------------------------------------
                DataRow row = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                                                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                                    wareID,
                                                    QSPForm.Common.PhoneNumberType.TYPE_PHONE_NUMBER);

                if (row != null) {
                    if (txtPhoneNumber.Text.Trim().Length > 0) {
                        clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtPhoneNumber.Text.Trim());
                    }
                    else {
                        row.Delete();
                    }
                }
                else {
                    if (txtPhoneNumber.Text.Trim().Length > 0) {
                        DataRow newRow = dTblPhoneNumber.NewRow();
                        newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_PHONE_NUMBER; //Corporate
                        newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtPhoneNumber.Text.Trim();
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = wareID;
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
                        newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblPhoneNumber.Rows.Add(newRow);
                    }
                }
                //--------------------------------------		
                // Warehouse Fax Number
                //--------------------------------------
                row = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                                        QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                        wareID,
                                        QSPForm.Common.PhoneNumberType.TYPE_FAX_NUMBER);
                if (row != null) {
                    if (txtFaxNumber.Text.Trim().Length > 0) {
                        clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtFaxNumber.Text.Trim());
                    }
                    else {
                        row.Delete();
                    }
                }
                else {
                    if (txtFaxNumber.Text.Trim().Length > 0) {
                        DataRow newRow = dTblPhoneNumber.NewRow();
                        newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_FAX_NUMBER; //Corporate
                        newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtFaxNumber.Text.Trim();
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = wareID;
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
                        newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblPhoneNumber.Rows.Add(newRow);
                    }
                }

                //--------------------------------------		
                // Warehouse Receiving Phone Number
                //--------------------------------------
                row = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    wareID,
                    QSPForm.Common.PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER);
                if (row != null) {
                    if (txtReceivingPhoneNumber.Text.Trim().Length > 0) {
                        clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtReceivingPhoneNumber.Text.Trim());
                    }
                    else {
                        row.Delete();
                    }
                }
                else {
                    if (txtReceivingPhoneNumber.Text.Trim().Length > 0) {
                        DataRow newRow = dTblPhoneNumber.NewRow();
                        newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER; //Corporate
                        newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtReceivingPhoneNumber.Text.Trim();
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = wareID;
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
                        newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblPhoneNumber.Rows.Add(newRow);
                    }
                }
            }

            return true;
        }

        private bool UpdateEmailAddress() {
            EmailEntityTable dTblEmail = dtsWarehouse.EmailAddress;
            EmailAddressSystem emailSys = new EmailAddressSystem();
            //-----------------------------------------
            // CREDIT APPLICATION
            //-----------------------------------------
            if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];
                int wareID = Convert.ToInt32(wareRow[WarehouseTable.FLD_PKID]);

                //-----------------------------------------
                // General Email Address
                //-----------------------------------------
                DataRow row = emailSys.FindRow(dtsWarehouse.EmailAddress,
                    QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                    wareID,
                    QSPForm.Common.EmailType.TYPE_CORPORATIVE);

                if (row != null) {
                    if (txtEmailAddress.Text.Trim().Length > 0) {
                        clsUtil.UpdateRow(row, EmailEntityTable.FLD_EMAIL_ADDRESS, txtEmailAddress.Text.Trim());
                    }
                    else {
                        row.Delete();
                    }
                }
                else {
                    if (txtEmailAddress.Text.Trim().Length > 0) {
                        DataRow newRow = dTblEmail.NewRow();
                        newRow[EmailEntityTable.FLD_TYPE] = EmailType.TYPE_CORPORATIVE; //Corporate
                        newRow[EmailEntityTable.FLD_EMAIL_ADDRESS] = txtEmailAddress.Text.Trim();
                        newRow[EmailEntityTable.FLD_ENTITY_ID] = wareID;
                        newRow[EmailEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
                        newRow[EmailEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblEmail.Rows.Add(newRow);
                    }
                }
            }

            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }

        private void FillDataTableForDropDownList() {
            try {
                CommonUtility clsUtil = new CommonUtility();
                clsUtil.SetVendorDropDownList(ddlVendor, true);
                clsUtil.SetUSStateDropDownList(ddlState, true);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}