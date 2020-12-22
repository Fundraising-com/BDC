using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.WarehouseData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for WarehouseForm.
    /// </summary>
    public partial class WarehouseInfo : BaseWebFormControl {
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
            DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];

            lblWarehouseID.Text = wareRow[WarehouseTable.FLD_PKID].ToString();

            if (wareRow.IsNull(WarehouseTable.FLD_FULF_WAREHOUSE_ID))
                lblFulfWarehouseID.Text = "New Warehouse";
            else
                lblFulfWarehouseID.Text = wareRow[WarehouseTable.FLD_FULF_WAREHOUSE_ID].ToString();

            //Warehouse Status			
            lblWarehouseStatus.Text = wareRow[WarehouseTable.FLD_WAREHOUSE_STATUS_NAME].ToString();
            lblWarehouseStatusColor.BackColor = Color.FromName(wareRow[WarehouseTable.FLD_WAREHOUSE_STATUS_COLOR_CODE].ToString());

            lblWarehouseName.Text = wareRow[WarehouseTable.FLD_NAME].ToString();
            lblVendorName.Text = wareRow[WarehouseTable.FLD_VENDOR_NAME].ToString();
            if (!wareRow.IsNull(WarehouseTable.FLD_PICK_UP)) {
                chkPickUp.Checked = Convert.ToBoolean(wareRow[WarehouseTable.FLD_PICK_UP]);
            }
            PostalAddressSystem addSys = new PostalAddressSystem();
            DataRow rowAddress = addSys.FindRow(dtsWarehouse.PostalAddress,
                                                QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                                c_WarehouseID,
                                                QSPForm.Common.PostalAddressType.TYPE_BILLING);
            if (rowAddress != null) {
                lblCompanyName.Text = rowAddress[PostalAddressEntityTable.FLD_NAME].ToString();
                lblFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                lblLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                lblAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                lblAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                lblCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                lblCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                lblState.Text = rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1].ToString();
                lblZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
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
                    lblPhoneNumber.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }
                //--------------------------------------		
                // General Fax Number-
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                                            QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                            c_WarehouseID,
                                            QSPForm.Common.PhoneNumberType.TYPE_FAX_NUMBER);
                if (rowPhone != null) {
                    lblFaxNumber.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }

                //--------------------------------------		
                // General Fax Number-
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsWarehouse.PhoneNumber,
                                            QSPForm.Common.EntityType.TYPE_WAREHOUSE,
                                            c_WarehouseID,
                                            QSPForm.Common.PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER);
                if (rowPhone != null) {
                    lblReceivingPhoneNumber.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
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
                    lblEmailAddress.Text = rowEmail[EmailEntityTable.FLD_EMAIL_ADDRESS].ToString();
                }
            }

            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }
    }
}