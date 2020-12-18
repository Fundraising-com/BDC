using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSPForm.Common;
using dataDef = QSPForm.Common.DataDef.CreditApplicationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CreditApplicationForm.
    /// </summary>
    public partial class CreditApplicationForm : BaseWebFormControl {
        protected dataDef dtsCreditApplication;
        protected System.Web.UI.WebControls.CompareValidator compVal_CCExpDate;
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
            this.custVal_CCExpDate.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.custVal_CCExpDate_ServerValidate);

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
            trAccountingSection.Visible = false;
            trDocumentSection.Visible = false;

            if (radBtnList_CreditAppType.SelectedValue == "1") //Credit appication
			{
                trCreditApp.Visible = true;
                trAccountingSection.Visible = true;
            }
            else if (radBtnList_CreditAppType.SelectedValue == "2") //Credit Card
			{
                trCreditCard.Visible = true;
                trAccountingSection.Visible = true;
            }
            else if (radBtnList_CreditAppType.SelectedValue == "3") //FSM Authorization
			{
                trQSPSalesRep.Visible = true;
                trAccountingSection.Visible = true;
            }

            if (dtsCreditApplication.CreditDocument.Rows.Count > 0) //No Credit App Required
            //else if (radBtnList_CreditAppType.SelectedValue == "4") //No Credit App Required
			{
                trDocumentSection.Visible = true;
            }

            //This check bo is only enabled for Role Admin
            chkBoxApproved.Enabled = (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER);
            txtApproveCode.Enabled = (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER);
        }

        public bool ValidateForm() {
            trValSumAccountInfo.Visible = false;
            trValSumCreditAppInfo.Visible = false;
            trValSumCreditCardInfo.Visible = false;
            trValSumDocumentSection.Visible = false;

            CommonUtility clsUtil = new CommonUtility();

            if (radBtnList_CreditAppType.SelectedValue == "1") {
                if (!IsValid(tblAccountInfo.Controls)) {
                    trValSumAccountInfo.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitleAccountInfo);
                    this.Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
            }

            if (radBtnList_CreditAppType.SelectedValue == "1") {
                if (!IsValid(trCreditApp.Controls)) {
                    trValSumCreditAppInfo.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitleCreditAppInfo);
                    this.Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
            }
            else if (radBtnList_CreditAppType.SelectedValue == "2") {
                //Assign the Credit Card Type
                switch (radBtnLstCreditCardType.SelectedValue) {
                    case "1": CCVal_CCNumber.AcceptedCardTypes = "MasterCard";
                        break;
                    case "2": CCVal_CCNumber.AcceptedCardTypes = "VISA";
                        break;
                    case "3": CCVal_CCNumber.AcceptedCardTypes = "Discover";
                        break;
                    case "4": CCVal_CCNumber.AcceptedCardTypes = "Amex";
                        break;
                }

                if (!IsValid(trCreditCard.Controls)) {

                    trValSumCreditCardInfo.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitleCreditCardInfo);
                    this.Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
            }
            //For document reception
            //All case except credit app type = 4 (No Credit Check required))
            if (trDocumentSection.Visible) {
                if (!IsValid(trDocumentSection.Controls)) {

                    trValSumDocumentSection.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitleDocumentSection);
                    this.Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
            }

            //if everything have been ok
            return true;
        }

        public bool UpdateDataSource() {
            PostalAddressSystem addSys = new PostalAddressSystem();
            DataView DVAddress = new DataView(dtsCreditApplication.PostalAddress);
            DataRow row;
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);
                int creditAppTypeID = Convert.ToInt32(radBtnList_CreditAppType.SelectedValue);
                clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_TYPE_ID, creditAppTypeID.ToString());
                clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_SOCIAL_SECURITY_NUMBER, txtSSN.Text);
                clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_CREDIT_LIMIT, txtCreditLimit.Text);
                clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_OFFICER_NAME, txtOfficerName.Text);

                if (creditAppTypeID == 1) {
                    //-------------------------------------------------
                    //Section 2 -- Individual Responsible for payment
                    //-------------------------------------------------

                    row = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                        QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                        creditAppID,
                                        QSPForm.Common.PostalAddressType.TYPE_BILLING);
                    if (row != null) {
                        //'Table Mapping  
                        //verification of all value before replacement                    
                        //entity (Account_ID, Order_ID, Organization_ID, etc...)
                        clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_ID, creditAppID.ToString());
                        //entity type (Account, Order, Organization, etc...)
                        clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, EntityType.TYPE_CREDIT_APPLICATION.ToString());
                        //Address type (Billing, Shipping)
                        clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_TYPE, QSPForm.Common.PostalAddressType.TYPE_BILLING.ToString());
                        //Contact Name
                        clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_FIRST_NAME, txtFirstName.Text);
                        //Contact Name
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
                        DataRow newRow = dtsCreditApplication.PostalAddress.NewRow();
                        newRow[PostalAddressEntityTable.FLD_ENTITY_ID] = creditAppID.ToString();
                        //entity type (Account, Order, Organization, etc...)
                        newRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_CREDIT_APPLICATION.ToString();
                        //Address type (Billing, Shipping)
                        newRow[PostalAddressEntityTable.FLD_TYPE] = QSPForm.Common.PostalAddressType.TYPE_BILLING.ToString();
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
                        dtsCreditApplication.PostalAddress.Rows.Add(newRow);
                    }
                }
                //-------------------------------------------------
                //Section 3 -- Credit Card Information
                //-------------------------------------------------
                if (creditAppTypeID == 2) {
                    if (dtsCreditApplication.CreditCard.Rows.Count > 0) {
                        DataRow ccRow = dtsCreditApplication.CreditCard.Rows[0];
                        int creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
                        clsUtil.UpdateRow(ccRow, CreditCardTable.FLD_CREDIT_CARD_NUMBER, txtCCNumber.Text.Trim());
                        clsUtil.UpdateRow(ccRow, CreditCardTable.FLD_CREDIT_CARD_NAME, txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim());
                        clsUtil.UpdateRow(ccRow, CreditCardTable.FLD_CREDIT_CARD_TYPE_ID, radBtnLstCreditCardType.SelectedValue);
                        string sValueDate = "";
                        RegExpVal_CCExpDate.Validate();
                        if (RegExpVal_CCExpDate.IsValid) {
                            try {
                                string[] sExpDate = txtCCExpDate.Text.Trim().Split('/');
                                int expMonth = Convert.ToInt32(sExpDate[0]);
                                int expYear = Convert.ToInt32(sExpDate[1]);
                                expYear = expYear + 2000;
                                DateTime expDate = new DateTime(expYear, expMonth, 1);
                                sValueDate = expDate.ToShortDateString();
                            }
                            catch (Exception ex) {
                            }
                        }
                        clsUtil.UpdateRow(ccRow, CreditCardTable.FLD_CREDIT_CARD_EXPIRATION_DATE, sValueDate);

                        if (ccRow.RowState == DataRowState.Added)
                            ccRow[CreditCardTable.FLD_CREATE_USER_ID] = Page.UserID;
                        else
                            clsUtil.UpdateRow(ccRow, CreditCardTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());

                        row = addSys.FindRow(dtsCreditApplication.PostalAddress,
                            QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                            creditCardID,
                            QSPForm.Common.PostalAddressType.TYPE_BILLING);
                        if (row != null) {
                            //'Table Mapping  
                            //verification of all value before replacement                    
                            //entity (Account_ID, Order_ID, Organization_ID, etc...)
                            clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_ID, creditCardID.ToString());
                            //entity type (Account, Order, Organization, etc...)
                            clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, EntityType.TYPE_CREDIT_CARD.ToString());
                            //Address type (Billing, Shipping)
                            clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_TYPE, QSPForm.Common.PostalAddressType.TYPE_BILLING.ToString());
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


                            if (row.RowState == DataRowState.Added)
                                row[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                            else
                                clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());
                        }
                        else {
                            DataRow newRow = dtsCreditApplication.PostalAddress.NewRow();
                            newRow[PostalAddressEntityTable.FLD_ENTITY_ID] = creditCardID.ToString();
                            //entity type (Account, Order, Organization, etc...)
                            newRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_CREDIT_CARD.ToString();
                            //Address type (Billing, Shipping)
                            newRow[PostalAddressEntityTable.FLD_TYPE] = QSPForm.Common.PostalAddressType.TYPE_BILLING.ToString();
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

                            newRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                            dtsCreditApplication.PostalAddress.Rows.Add(newRow);
                        }
                    }
                } //End of Credit Card
                else {  //if not credit card and there is an existing row
                    if (dtsCreditApplication.CreditCard.Rows.Count > 0) {
                        DataRow ccRow = dtsCreditApplication.CreditCard.Rows[0];
                        int creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
                        if (ccRow.RowState == DataRowState.Added) {
                            ccRow.Delete();
                            DataRow crdAddrrow = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                                creditCardID,
                                QSPForm.Common.PostalAddressType.TYPE_BILLING);
                            if (crdAddrrow != null) {
                                crdAddrrow.Delete();
                            }
                        }
                        else {
                            ccRow.RejectChanges();
                            DataRow crdAddrrow = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                                creditCardID,
                                QSPForm.Common.PostalAddressType.TYPE_BILLING);
                            if (crdAddrrow != null) {
                                crdAddrrow.RejectChanges();
                            }
                        }
                    }
                }
                //Update Phone of the Credt App
                UpdatePhoneNumber();

                //----------------------------------------------------------------
                //Section 5 -- Internal Accounting department Approval Section
                //----------------------------------------------------------------
                if (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER) {
                    clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_APPROVED, chkBoxApproved.Checked.ToString());
                    clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_APPROVE_CODE, txtApproveCode.Text);
                }

                if (crdappRow.RowState == DataRowState.Added)
                    crdappRow[CreditApplicationTable.FLD_CREATE_USER_ID] = Page.UserID;
                else
                    clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());

                //----------------------------------------------------------------
                //Section 6 -- Document Approval Section
                //----------------------------------------------------------------
                if (trDocumentSection.Visible) {
                    //If a credit check is required
                    //We need a credit application form document
                    if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                        //Update of the Documentation
                        if (dtsCreditApplication.CreditDocument.Rows.Count > 0) {
                            DataRow docRow = dtsCreditApplication.CreditDocument.Rows[0];
                            if (docRow.RowState == DataRowState.Deleted)
                                docRow.RejectChanges();

                            int docID = Convert.ToInt32(docRow[DocumentEntityTable.FLD_PKID]);
                            clsUtil.UpdateRow(docRow, DocumentEntityTable.FLD_RECEIVED_DATE, txtDocReceivedDate.Text);
                            clsUtil.UpdateRow(docRow, DocumentEntityTable.FLD_APPROVED, chkBoxDocumentApproved.Checked.ToString());

                            if (docRow.RowState == DataRowState.Added)
                                docRow[DocumentEntityTable.FLD_CREATE_USER_ID] = Page.UserID;
                            else
                                clsUtil.UpdateRow(docRow, DocumentEntityTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());
                        }
                    }
                }
                //Maintain globally the update information
                if (crdappRow.RowState == DataRowState.Added)
                    crdappRow[CreditApplicationTable.FLD_CREATE_USER_ID] = Page.UserID;
                else
                    clsUtil.UpdateRow(crdappRow, CreditApplicationTable.FLD_UPDATE_USER_ID, Page.UserID.ToString());

            }

            return true;
        }

        private bool UpdatePhoneNumber() {
            PhoneNumberEntityTable dTblPhoneNumber = dtsCreditApplication.PhoneNumber;
            PhoneNumberSystem phoneSys = new PhoneNumberSystem();
            //-----------------------------------------
            // CREDIT APPLICATION
            //-----------------------------------------
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);

                //-----------------------------------------
                // Credit App Phone
                //-----------------------------------------
                DataRow row = phoneSys.FindRow(dtsCreditApplication.PhoneNumber,
                                                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                    creditAppID,
                                                    QSPForm.Common.PhoneNumberType.TYPE_BILLING_PHONE);

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
                        newRow[PhoneNumberEntityTable.FLD_TYPE] = QSPForm.Common.PhoneNumberType.TYPE_BILLING_PHONE; //Corporate
                        newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtPhoneNumber.Text.Trim();
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = creditAppID;
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_CREDIT_APPLICATION;
                        newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblPhoneNumber.Rows.Add(newRow);
                    }
                }
                //--------------------------------------		
                // Credit App Home Phone
                //--------------------------------------
                row = phoneSys.FindRow(dtsCreditApplication.PhoneNumber,
                                        QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                        creditAppID,
                                        QSPForm.Common.PhoneNumberType.TYPE_HOME_PHONE_NUMBER);
                if (row != null) {
                    if (txtHomePhoneNumber.Text.Trim().Length > 0) {
                        clsUtil.UpdateRow(row, PhoneNumberEntityTable.FLD_PHONE_NUMBER, txtHomePhoneNumber.Text.Trim());
                    }
                    else {
                        row.Delete();
                    }
                }
                else {
                    if (txtHomePhoneNumber.Text.Trim().Length > 0) {
                        DataRow newRow = dTblPhoneNumber.NewRow();
                        newRow[PhoneNumberEntityTable.FLD_TYPE] = QSPForm.Common.PhoneNumberType.TYPE_HOME_PHONE_NUMBER; //Corporate
                        newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = txtHomePhoneNumber.Text.Trim();
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = creditAppID;
                        newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_CREDIT_APPLICATION;
                        newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = this.Page.UserID;
                        dTblPhoneNumber.Rows.Add(newRow);
                    }

                }
            }

            return true;
        }

        public void BindFormAccountInfo() {
            if (dtsCreditApplication.Account.Rows.Count > 0) {
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

                //-------------------------------------------------
                //Section 4 -- QSP Sale Representative Information
                //-------------------------------------------------
                lblFMName.Text = accRow[AccountTable.FLD_FM_NAME].ToString();
                lblFMID.Text = accRow[AccountTable.FLD_FM_ID].ToString();
            }
        }

        public void BindFormCreditAppInfo() {
            //----------------------------------
            //  Credit Application
            //----------------------------------							
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);

                //SSN
                txtSSN.Text = crdappRow[CreditApplicationTable.FLD_SOCIAL_SECURITY_NUMBER].ToString();
                if (!crdappRow.IsNull(CreditApplicationTable.FLD_CREDIT_LIMIT)) {
                    txtCreditLimit.Text = Convert.ToDecimal(crdappRow[CreditApplicationTable.FLD_CREDIT_LIMIT]).ToString("F2");
                }
                //Officer Name
                txtOfficerName.Text = crdappRow[CreditApplicationTable.FLD_OFFICER_NAME].ToString();
                //Credit Application Type
                if (!crdappRow.IsNull(CreditApplicationTable.FLD_TYPE_ID)) {
                    radBtnList_CreditAppType.ClearSelection();
                    ListItem lstItem = radBtnList_CreditAppType.Items.FindByValue(crdappRow[CreditApplicationTable.FLD_TYPE_ID].ToString());
                    if (lstItem != null) {
                        lstItem.Selected = true;
                    }
                }
            }
        }

        public void BindFormCreditApp_AddressInfo() {
            //-------------------------------------------------
            //Section 2 -- Individual Responsible for payment
            //-------------------------------------------------
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                PostalAddressSystem addSys = new PostalAddressSystem();
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                int creditAppID = Convert.ToInt32(crdappRow[CreditApplicationTable.FLD_PKID]);
                DataRow rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                    creditAppID,
                    QSPForm.Common.PostalAddressType.TYPE_BILLING);
                if (rowAddress != null) {
                    //'Table Mapping                      			
                    txtFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                    txtLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                    txtAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                    txtAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                    txtCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                    txtCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                    ddlState.ClearSelection();
                    if (!rowAddress.IsNull(PostalAddressEntityTable.FLD_SUBDIVISION_CODE)) {
                        ddlState.ClearSelection();
                        ListItem lstItem = ddlState.Items.FindByValue(rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString());
                        if (lstItem != null) {
                            lstItem.Selected = true;
                        }
                    }
                    txtZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
                }
            }
        }

        public void BindFormCreditCard() {
            //-------------------------------------------------
            //Section 3 -- Credit Card Information
            //-------------------------------------------------
            if (dtsCreditApplication.CreditCard.Rows.Count > 0) {
                DataRow ccRow = dtsCreditApplication.CreditCard.Rows[0];
                txtCCNumber.Text = ccRow[CreditCardTable.FLD_CREDIT_CARD_NUMBER].ToString();
                if (!ccRow.IsNull(CreditCardTable.FLD_CREDIT_CARD_TYPE_ID)) {
                    radBtnLstCreditCardType.ClearSelection();
                    ListItem lstItem = radBtnLstCreditCardType.Items.FindByValue(ccRow[CreditCardTable.FLD_CREDIT_CARD_TYPE_ID].ToString());
                    if (lstItem != null) {
                        lstItem.Selected = true;
                    }
                }
                if (!ccRow.IsNull(CreditCardTable.FLD_CREDIT_CARD_EXPIRATION_DATE))
                    txtCCExpDate.Text = Convert.ToDateTime(ccRow[CreditCardTable.FLD_CREDIT_CARD_EXPIRATION_DATE]).ToString("MM/yy");
            }
        }

        public void BindFormCreditCard_AddressInfo() {
            //-------------------------------------------------
            //Section 3 -- Credit Card Information
            //-------------------------------------------------
            if (dtsCreditApplication.CreditCard.Rows.Count > 0) {
                PostalAddressSystem addSys = new PostalAddressSystem();
                DataRow crdappRow = dtsCreditApplication.CreditCard.Rows[0];
                int creditCardID = Convert.ToInt32(crdappRow[CreditCardTable.FLD_PKID]);
                DataRow rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                    QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                    creditCardID,
                    QSPForm.Common.PostalAddressType.TYPE_BILLING);
                if (rowAddress != null) {
                    //'Table Mapping                      			
                    txtCCFirstName.Text = rowAddress[PostalAddressEntityTable.FLD_FIRST_NAME].ToString();
                    txtCCLastName.Text = rowAddress[PostalAddressEntityTable.FLD_LAST_NAME].ToString();
                    txtCCAddressLine1.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS1].ToString();
                    txtCCAddressLine2.Text = rowAddress[PostalAddressEntityTable.FLD_ADDRESS2].ToString();
                    txtCCCity.Text = rowAddress[PostalAddressEntityTable.FLD_CITY].ToString();
                    txtCCCounty.Text = rowAddress[PostalAddressEntityTable.FLD_COUNTY].ToString();
                    ddlCCState.ClearSelection();
                    if (!rowAddress.IsNull(PostalAddressEntityTable.FLD_SUBDIVISION_CODE)) {
                        ddlCCState.ClearSelection();
                        ListItem lstItem = ddlCCState.Items.FindByValue(rowAddress[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString());
                        if (lstItem != null) {
                            lstItem.Selected = true;
                        }
                    }
                    txtCCZip.Text = rowAddress[PostalAddressEntityTable.FLD_ZIP].ToString();
                }
            }
        }

        public void BindFormApprovalInfo() {
            //----------------------------------------------------------------
            //Section 5 -- Internal Accounting department Approval Section
            //----------------------------------------------------------------
            if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                DataRow crdappRow = dtsCreditApplication.CreditApplication.Rows[0];
                if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVED)) {
                    chkBoxApproved.Checked = Convert.ToBoolean(crdappRow[CreditApplicationTable.FLD_APPROVED]);

                    if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVE_USER_NAME))
                        lblApprovedBy.Text = crdappRow[CreditApplicationTable.FLD_APPROVE_USER_NAME].ToString();

                    if (!crdappRow.IsNull(CreditApplicationTable.FLD_APPROVE_DATE))
                        lblApprovalDate.Text = Convert.ToDateTime(crdappRow[CreditApplicationTable.FLD_APPROVE_DATE]).ToShortDateString()
                            + "&nbsp;" + Convert.ToDateTime(crdappRow[CreditApplicationTable.FLD_APPROVE_DATE]).ToShortTimeString();
                    txtApproveCode.Text = crdappRow[CreditApplicationTable.FLD_APPROVE_CODE].ToString();
                }
            }
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

                if (!docRow.IsNull(DocumentEntityTable.FLD_RECEIVED_DATE))
                    txtDocReceivedDate.Text = Convert.ToDateTime(docRow[DocumentEntityTable.FLD_RECEIVED_DATE]).ToShortDateString();
            }
        }

        public new void BindForm() {
            FillDataTableForDropDownList();

            BindFormAccountInfo();

            BindFormCreditAppInfo();
            BindFormCreditApp_AddressInfo();
            BindFormPhoneNumber();

            BindFormCreditCard();
            BindFormCreditCard_AddressInfo();

            //----------------------------------------------------------------
            //Section 5 -- Internal Accounting department Approval Section
            //----------------------------------------------------------------
            BindFormApprovalInfo();

            //----------------------------------------------------------------
            //Section 6 -- Document Approval Section
            //----------------------------------------------------------------
            BindFormDocumentInfo();
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
                    txtPhoneNumber.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }
                //--------------------------------------		
                // Credit App Home Phone
                //--------------------------------------
                rowPhone = phoneSys.FindRow(dtsCreditApplication.PhoneNumber,
                                                    QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                    creditAppID,
                                                    QSPForm.Common.PhoneNumberType.TYPE_HOME_PHONE_NUMBER);
                if (rowPhone != null) {
                    txtHomePhoneNumber.Text = rowPhone[PhoneNumberEntityTable.FLD_PHONE_NUMBER].ToString();
                }
            }

            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //State
                DataTable tblState = comSys.SelectAllUSState();
                DataRow row = tblState.NewRow();
                row[0] = "";
                row[1] = "--SELECT--";
                tblState.Rows.InsertAt(row, 0);
                ddlState.DataSource = tblState;
                ddlState.DataBind();

                ddlCCState.DataSource = tblState;
                ddlCCState.DataBind();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void radBtnList_CreditAppType_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (radBtnList_CreditAppType.SelectedValue == "2") {
                if (dtsCreditApplication.CreditCard.Rows.Count == 0) {
                    DataRow newRow = dtsCreditApplication.CreditCard.NewRow();
                    dtsCreditApplication.CreditCard.Rows.Add(newRow);
                }
                //Check for the postal address information
                int creditCardID = Convert.ToInt32(dtsCreditApplication.CreditCard.Rows[0][CreditCardTable.FLD_PKID]);
                PostalAddressSystem addSys = new PostalAddressSystem();
                DataRow rowAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                    QSPForm.Common.EntityType.TYPE_CREDIT_CARD,
                    creditCardID,
                    QSPForm.Common.PostalAddressType.TYPE_BILLING);

                if (rowAddress == null) {
                    int creditAppID = Convert.ToInt32(dtsCreditApplication.CreditApplication.Rows[0][CreditApplicationTable.FLD_PKID]);
                    DataRow rowToCopyAddress;
                    rowToCopyAddress = addSys.FindRow(dtsCreditApplication.PostalAddress,
                                                        QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION,
                                                        creditAppID,
                                                        QSPForm.Common.PostalAddressType.TYPE_BILLING);
                    if (rowToCopyAddress != null) {
                        //We copy the Credit App Billing Information to the Credit Card
                        addSys.CopyToEntity(dtsCreditApplication.PostalAddress, this.Page.UserID,
                            EntityType.TYPE_CREDIT_APPLICATION, creditAppID, QSPForm.Common.PostalAddressType.TYPE_BILLING,
                            EntityType.TYPE_CREDIT_CARD, creditCardID, QSPForm.Common.PostalAddressType.TYPE_BILLING);
                    }
                    else {
                        int AccountID = Convert.ToInt32(dtsCreditApplication.Account.Rows[0][AccountTable.FLD_PKID]);
                        //We copy the Account Billing Information to the Credit Card
                        addSys.CopyToEntity(dtsCreditApplication.PostalAddress, this.Page.UserID,
                            EntityType.TYPE_ACCOUNT, AccountID, QSPForm.Common.PostalAddressType.TYPE_BILLING,
                            EntityType.TYPE_CREDIT_CARD, creditCardID, QSPForm.Common.PostalAddressType.TYPE_BILLING);
                    }

                    BindFormCreditCard_AddressInfo();
                }
            }
        }

        protected void ddlState_DataBinding(object sender, System.EventArgs e) {
        }

        private void custVal_CCExpDate_ServerValidate(object source, ServerValidateEventArgs args) {
            RegExpVal_CCExpDate.Validate();
            if (RegExpVal_CCExpDate.IsValid) {
                string[] sExpDate = txtCCExpDate.Text.Trim().Split('/');
                int expMonth = Convert.ToInt32(sExpDate[0]);
                int expYear = Convert.ToInt32(sExpDate[1]);
                expYear = expYear + 2000;
                DateTime expDate = new DateTime(expYear, expMonth, 1);
                DateTime compDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                args.IsValid = (expDate >= compDate);
            }
        }
    }
}