using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CreditApplicationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CreditApplicationForm.
    /// </summary>
    public partial class CreditApplicationInfo : BaseWebFormControl {
        protected dataDef dtsCreditApplication;
        private int c_AccID = 0;
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

        public int AccountID {
            get {
                return c_AccID;
            }
            set {
                c_AccID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtsCreditApplication;
            }
            set {
                dtsCreditApplication = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            trCreditApp.Visible = false;
            trCreditCard.Visible = false;
            trQSPSalesRep.Visible = false;

            int creditAppTypeID = 0;
            DataRow row = dtsCreditApplication.CreditApplication.Rows[0];
            creditAppTypeID = Convert.ToInt32(row[CreditApplicationTable.FLD_TYPE_ID]);

            if (creditAppTypeID == 1) //Credit application
			{
                trCreditApp.Visible = true;
            }
            else if (creditAppTypeID == 2) //Credit Card
			{
                trCreditCard.Visible = true;
            }
            else if (creditAppTypeID == 3) //FSM Authorization
			{
                trQSPSalesRep.Visible = true;
            }
            if (dtsCreditApplication.CreditDocument.Rows.Count > 0) //No Credit App Required
			{
                trDocumentSection.Visible = true;
            }
        }

        public new void BindForm() {
            DataRow accRow = dtsCreditApplication.Account.Rows[0];
            //First section -- Account Name and Billing Address
            lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
            PostalAddressSystem addSys = new PostalAddressSystem();
            DataRow rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                                QSPForm.Common.EntityType.TYPE_ACCOUNT,
                                                c_AccID,
                                                QSPForm.Common.PostalAddressType.TYPE_BILLING);
            if (rowAddress != null) {
                lblAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                lblAddressline2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                lblCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                lblCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                lblState.Text = rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1].ToString();
                lblZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
            }
            //----------------------------------
            //  Credit Application
            //----------------------------------
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);

                lblSSN.Text = crdappRow[CreditApplicationTable.FLD_SOCIAL_SECURITY_NUMBER].ToString();
                if (!crdappRow.IsNull(CreditApplicationTable.FLD_CREDIT_LIMIT)) {
                    lblCreditLimit.Text = Convert.ToDecimal(crdappRow[CreditApplicationTable.FLD_CREDIT_LIMIT]).ToString("C");

                }
                lblOfficerName.Text = crdappRow[CreditApplicationTable.FLD_OFFICER_NAME].ToString();
                lblTypeName.Text = crdappRow[CreditApplicationTable.FLD_TYPE_NAME].ToString();

                //-------------------------------------------------
                //Section 2 -- Individual Responsible for payment
                //-------------------------------------------------
                rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                    creditAppID,
                                                    QSPForm.Common.PostalAddressType.TYPE_BILLING);
                if (rowAddress != null) {
                    //'Table Mapping                      			
                    lblAppFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                    lblAppLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                    lblAppAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                    lblAppAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                    lblAppCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                    lblAppCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                    lblAppState.Text = rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1].ToString();
                    lblAppZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
                }
                //-------------------------------------------------
                //Section 3 -- Credit Card Information
                //-------------------------------------------------
                if (dtsCreditApplication.CreditCard.Rows.Count > 0) {
                    int creditCardID = Convert.ToInt32(crdappRow[CreditCardTable.FLD_PKID]);
                    rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                                        QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                                                        creditCardID,
                                                        QSPForm.Common.PostalAddressType.TYPE_BILLING);
                    if (rowAddress != null) {
                        //'Table Mapping                      			
                        lblCCFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                        lblCCLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                        lblCCAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                        lblCCAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                        lblCCCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                        lblCCCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                        lblCCState.Text = rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1].ToString();
                        lblCCZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();

                    }
                    DataRow ccRow = dtsCreditApplication.CreditCard.Rows[0];
                    lblCCNumber.Text = ccRow[CreditCardTable.FLD_CREDIT_CARD_NUMBER].ToString();
                    lblCCTypeName.Text = ccRow[CreditCardTable.FLD_CREDIT_CARD_TYPE_NAME].ToString();
                    if (!ccRow.IsNull(CreditCardTable.FLD_CREDIT_CARD_EXPIRATION_DATE))
                        lblCCExpire.Text = Convert.ToDateTime(ccRow[CreditCardTable.FLD_CREDIT_CARD_EXPIRATION_DATE]).ToString("MM/yy");
                }

                BindFormPhoneNumber();

                //----------------------------------------------------------------
                //Section 5 -- Internal Accounting department Approval Section
                //----------------------------------------------------------------
                //This check bo is only enabled for Role Admin
                //chkBoxApproved.Checked = false;
                if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVED)) {
                    chkBoxApproved.Checked = Convert.ToBoolean(crdappRow[CreditApplicationTable.FLD_APPROVED]);

                    if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVE_USER_NAME))
                        lblApprovedBy.Text = crdappRow[CreditApplicationTable.FLD_APPROVE_USER_NAME].ToString();

                    if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVE_DATE))
                        lblApprovalDate.Text = Convert.ToDateTime(crdappRow[CreditApplicationTable.FLD_APPROVE_DATE]).ToShortDateString()
                            + "&nbsp;" + Convert.ToDateTime(crdappRow[CreditApplicationTable.FLD_APPROVE_DATE]).ToShortTimeString();
                    lblApproveCode.Text = crdappRow[CreditApplicationTable.FLD_APPROVE_CODE].ToString();
                }

                //----------------------------------------------------------------
                //Section 6 -- Document Approval Section
                //----------------------------------------------------------------
                BindFormDocumentInfo();
            }

            //-------------------------------------------------
            //Section 4 -- QSP Sale Representative Information
            //-------------------------------------------------
            lblFMName.Text = accRow[AccountTable.FLD_FM_NAME].ToString();
            lblFMID.Text = accRow[AccountTable.FLD_FM_ID].ToString();
        }

        private bool BindFormPhoneNumber() {
            DataView dvPhone = new DataView(dtsCreditApplication.PhoneNumber);
            //-----------------------------------------
            // CREDIT APPLICATION
            //-----------------------------------------
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);

                PhoneNumberEntityTable dTblPhoneNumber = dtsCreditApplication.PhoneNumber;
                PhoneNumberSystem phoneSys = new PhoneNumberSystem();

                //-----------------------------------------
                // Credit App Phone
                //-----------------------------------------
                DataRow rowPhone = phoneSys.FindRow(dtsCreditApplication.PhoneNumber,
                                                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                    creditAppID,
                                                    QSPForm.Common.PhoneNumberType.TYPE_BILLING_PHONE);
                if (rowPhone != null) {
                    lblAppPhone.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }
                //--------------------------------------		
                // Credit App Home Phone
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsCreditApplication.PhoneNumber,
                                                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                    creditAppID,
                                                    QSPForm.Common.PhoneNumberType.TYPE_HOME_PHONE_NUMBER);
                if (rowPhone != null) {
                    lblAppHomePhone.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }
            }

            return true;
        }

        public void BindFormDocumentInfo() {
            //----------------------------------------------------------------
            //Section 6 -- Document Reception Approval Section
            //----------------------------------------------------------------
            if (dtsCreditApplication.CreditDocument.Rows.Count > 0) {
                DataRow docRow = dtsCreditApplication.CreditDocument.Rows[0];
                if (!docRow.IsNull(DocumentEntityTable.FLD_APPROVED)) {
                    chkBoxDocumentApproved.Checked = Convert.ToBoolean(docRow[DocumentEntityTable.FLD_APPROVED]);

                    if (!docRow.IsNull(DocumentEntityTable.FLD_APPROVED_USER_NAME))
                        lblDocumentApprovedBy.Text = docRow[DocumentEntityTable.FLD_APPROVED_USER_NAME].ToString();

                    if (!docRow.IsNull(DocumentEntityTable.FLD_APPROVED_DATE))
                        lblDocumentApprovedDate.Text = Convert.ToDateTime(docRow[DocumentEntityTable.FLD_APPROVED_DATE]).ToShortDateString()
                            + "&nbsp;" + Convert.ToDateTime(docRow[DocumentEntityTable.FLD_APPROVED_DATE]).ToShortTimeString();
                }
            }
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }
    }
}