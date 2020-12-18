using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSPForm.Common;
using dataRef = QSPForm.Common.DataDef.ProgramAgreementData;
using dataReffer = QSPForm.Common.DataDef.ProgramAgreementCatalogTable;
using QSP.OrderExpress.Web.Code;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProgramAgreementForm_Step1.
    /// </summary>
    public partial class ProgramAgreementHeaderDetailForm : BaseWebFormControl {
        protected ProgramAgreementData dtsProgramAgreement;
        protected AccountData dtsAccount;
        protected System.Web.UI.WebControls.DropDownList ddlProgramAgreementForm;
        protected System.Web.UI.WebControls.ImageButton imgBtnDeliveryDate;
        private CommonUtility clsUtil = new CommonUtility();
        public event System.EventHandler AddressHygieneConfirmed;
        protected ProgramAgreementCatalogTable ProgramAgreementCatalog;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.Load += new EventHandler(this.Page_Load);
            this.DualAddressFormControl.AddressHygieneConfirmed += new EventHandler(DualAddressFormControl_AddressHygieneConfirmed);
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (!IsPostBack)
            //{
                this.SetStartAndEndDateConstraints();
                //this.FillCatalogTypeList();
            //}

            CampaignTable dTblCamp = this.DataSource.Campaign;
            if (dTblCamp.Rows.Count > 0)
            {
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                DataRow campRow = dTblCamp.Rows[0];
                int accountFiscalYear = Convert.ToInt32(campRow[CampaignTable.FLD_FISCAL_YEAR]);
                int currentFiscalYear = calSys.GetFiscalYear();

                if (accountFiscalYear < currentFiscalYear)
                {
                    accountFiscalYear = currentFiscalYear;
                }

            }

            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkHolidayStartDate, txtHolidayStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkHolidayEndDate, txtHolidayEndDate);

            trAccountStatus.Visible = false;// (Page.Role > AuthSystem.ROLE_FM);
        }
        protected void Page_PreRender(object sender, System.EventArgs e) {
        }
        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
        protected void DualAddressFormControl_AddressHygieneConfirmed(object sender, EventArgs e)
        {
            if (AddressHygieneConfirmed != null)
            {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        public ProgramAgreementData DataSource
        {
            get
            {
                return dtsProgramAgreement;
            }
            set
            {
                dtsProgramAgreement = value;
            }
        }
        public int ProgramAgreementID
        {
            get
            {
                int programAgreementID = 0;

                if (ViewState["ProgramAgreementID"] != null)
                {
                    programAgreementID = Convert.ToInt32(ViewState["ProgramAgreementID"]);
                }
                else if (DataSource != null && dtsProgramAgreement.ProgramAgreement.Rows.Count > 0)
                {
                    programAgreementID = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);
                    ViewState["ProgramAgreementID"] = programAgreementID;
                }

                return programAgreementID;
            }
            set
            {
                ViewState["ProgramAgreementID"] = value;
            }
        }
        public bool SectionAccount_Visible
        {
            get
            {
                return tblRowSectionAccount.Visible;
            }
            set
            {
                tblRowSectionAccount.Visible = value;
            }
        }
        public bool SectionProgramAgreement_Visible
        {
            get
            {
                return tblRowSectionProgramAgreement.Visible;
            }
            set
            {
                tblRowSectionProgramAgreement.Visible = value;
            }
        }

        public void SetStartAndEndDateConstraints()
        {
            int formId = this.GetFormId();

            FormSystem formSystem = new FormSystem();
            LinqEntity.Form form = formSystem.SelectById(formId);

            this.compVal_StartDate.Text = "<br />Start date must be greater or equal than " + form.StartDate.Value.ToShortDateString();
            this.compVal_StartDate.ErrorMessage = "Start date must be greater or equal than " + form.StartDate.Value.ToShortDateString();
            this.compVal_StartDate.ValueToCompare = form.StartDate.Value.ToShortDateString();

            this.compVal_EndDate.Text = "<br />End date must be less or equal than " + form.EndDate.Value.ToShortDateString();
            this.compVal_EndDate.ErrorMessage = "End date must be less or equal than " + form.EndDate.Value.ToShortDateString();
            this.compVal_EndDate.ValueToCompare = form.EndDate.Value.ToShortDateString();
        }
        public int GetFormId()
        {
            int formId = 0;

            DataRow row = dtsProgramAgreement.ProgramAgreement.Rows[0];
            if (!row.IsNull(ProgramAgreementTable.FLD_FORM_ID))
            {
                formId = Convert.ToInt32(row[ProgramAgreementTable.FLD_FORM_ID]);
            }

            return formId;
        }
        public void InitializeControls() 
        {
            if (dtsProgramAgreement.Campaign.Rows.Count > 0) {
                DualAddressFormControl.BillingParentID = (int)dtsProgramAgreement.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID];
                DualAddressFormControl.BillingParentType = EntityType.TYPE_ACCOUNT;
            }
            else {
                DualAddressFormControl.BillingParentID = ProgramAgreementID;
                DualAddressFormControl.BillingParentType = EntityType.TYPE_PROGRAM_AGREEMENT;
            }
            DualAddressFormControl.ShippingParentID = ProgramAgreementID;
            DualAddressFormControl.ShippingParentType = EntityType.TYPE_PROGRAM_AGREEMENT;
            DualAddressFormControl.DataSource = DataSource;
        }
        public bool ValidateForm()
        {
            bool isValid = true;
            ProgramAgreementSystem programAgreementSystem;

            trValSumProgramAgreementInfo.Visible = false;
            trValSumProgramInformationInfo.Visible = false;

            isValid = (isValid && DualAddressFormControl.IsValid());

            if (isValid && !IsValid(tblProgramAgreementInfo.Controls))
            {
                trValSumProgramAgreementInfo.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                clsUtil.RenderStartUpScroll(lblTitleProgramAgreementInfo);
                isValid = false;
            }

            if (isValid && !IsValid(tblProgramTermInfo.Controls))
            {
                trValSumProgramTermInfo.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                clsUtil.RenderStartUpScroll(lblTitleProgramTermInfo);
                isValid = false;
            }
            if (isValid && !IsValid(tblProgramInformationInfo.Controls))
            {
                trValSumProgramInformationInfo.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                clsUtil.RenderStartUpScroll(lblTitleProgramInformationInfo);
                isValid = false;
            }

            if (this.txtHolidayStartDate.Text.Trim().Length > 0)
            {
                if (this.txtHolidayEndDate.Text.Trim().Length > 0)
                {
                    // We have valid dates
                    this.txtHolidayEndDateResult.Visible = false;
                }
                else
                {
                    // We are mising end date
                    this.txtHolidayEndDateResult.Visible = true;
                    isValid = false;
                }
            }

            if (isValid)
            {
                try
                {
                    programAgreementSystem = new ProgramAgreementSystem();

                    isValid = programAgreementSystem.ValidateDuplicatePA(dtsProgramAgreement, Convert.ToDateTime(txtStartDate.Text));
                }
                catch (QSPFormValidationException ex)
                {
                    isValid = false;

                    Page.SetPageError(ex);
                }
            }

            return isValid;
        }
        public void ResetStatus()
        {
            DualAddressFormControl.ResetStatus();
        }
        public bool UpdateDataSource()
        {
            CommonUtility clsUtil = new CommonUtility();
            DataRow row = dtsProgramAgreement.ProgramAgreement.Rows[0];
            int PrgID = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);
            //---PROGRAM_AGREEMENT CONTACT INFORMATION ---------------------
            DualAddressFormControl.Update();

            //Program Agreement Terms	
            compVal_StartDate.Validate();
            if (compVal_StartDate.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_START_DATE, txtStartDate.Text);

            if (compVal_EndDate.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_END_DATE, txtEndDate.Text);

            if (compVal_HolidayStartDate.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_HOLIDAY_START_DATE, txtHolidayStartDate.Text);

            if (compVal_HolidayEndDate.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_HOLIDAY_END_DATE, txtHolidayEndDate.Text);


            if (compVal_EstimatedAmount.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS, txtEstimatedAmount.Text);

            if (compVal_Enrollment.IsValid)
                clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_ENROLLMENT, txtEnrollment.Text);

            float profit = 0;
            if (radBtnLstProfitRate.SelectedIndex > -1)
            {
                profit = Convert.ToSingle(radBtnLstProfitRate.SelectedValue);
            }
            clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_PROFIT_RATE, profit.ToString());

            string priced = string.Empty;
            if (RadioButtonList1.SelectedIndex >= -1)
            {
                priced = RadioButtonList1.SelectedValue;
            }
            clsUtil.UpdateRow(row, ProgramAgreementTable.FLD_PRICED, priced);

            if (CheckBoxList1.SelectedIndex > -1)
            {
                ProgramAgreementCatalog = dtsProgramAgreement.ProgramAgreementCatalog;
                DataView dv = new DataView(ProgramAgreementCatalog);
                dv.Sort = dataReffer.FLD_PKID;

                for (int iCount = 0; iCount < CheckBoxList1.Items.Count; iCount++)
                {
                    string datavalue = CheckBoxList1.Items[iCount].Value;
                    if (CheckBoxList1.Items[iCount].Selected == true)
                    {
                        bool selected = false;
                        if (ProgramAgreementCatalog.GetChanges(DataRowState.Deleted) != null || ProgramAgreementCatalog.GetChanges(DataRowState.Added) != null)
                        {
                            dv.RowStateFilter = DataViewRowState.CurrentRows;
                        }
                        //foreach (DataRow dataRow in ProgramAgreementCatalog.Select("", "", DataViewRowState.CurrentRows))
                        foreach (DataRowView dvRow in dv)
                        {
                            DataRow dataRow = dvRow.Row;
                            if (dataRow[ProgramAgreementCatalogTable.FLD_CATALOG_ID].ToString() == datavalue)
                                selected = true;
                        }
                        if (selected == false)
                        {
                            DataRow newRow = ProgramAgreementCatalog.NewRow();
                            newRow[ProgramAgreementCatalogTable.FLD_PROGRAM_AGREEMENT_ID] = PrgID;
                            newRow[ProgramAgreementCatalogTable.FLD_CATALOG_ID] = datavalue;
                            newRow[ProgramAgreementCatalogTable.FLD_ENTITY_ID] = PrgID;
                            ProgramAgreementCatalog.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        // for (int i = 0; i < ProgramAgreementCatalog.Rows.Count; i++)
                        if (ProgramAgreementCatalog.GetChanges(DataRowState.Deleted) != null || ProgramAgreementCatalog.GetChanges(DataRowState.Added) != null)
                        {
                            dv.RowStateFilter = DataViewRowState.CurrentRows;// ModifiedCurrent;
                        }
                        //foreach (DataRow dataRow in ProgramAgreementCatalog.Select("", "", DataViewRowState.CurrentRows))
                        foreach (DataRowView dvRow in dv)
                        {
                            DataRow catalogRow = dvRow.Row;// ProgramAgreementCatalog.Rows[i];
                            if (catalogRow[ProgramAgreementCatalogTable.FLD_CATALOG_ID].ToString() == datavalue)
                            {
                                catalogRow.Delete();
                                //ProgramAgreementCatalog.Rows[i].Delete();
                                //  ProgramAgreementCatalog.AcceptChanges();                              
                            }
                        }
                    }
                }
                dtsProgramAgreement.Merge(ProgramAgreementCatalog);
            }

            return true;
        }
        public override void BindForm()
        {
            this.FillCatalogTypeList();
            this.FillProfitRateList();

            ProgramAgreementTable dtblProgramAgreement = dtsProgramAgreement.ProgramAgreement;
            DataRow prgRow = dtblProgramAgreement.Rows[0];
            DataRow prgCampRow = dtsProgramAgreement.ProgramAgreementCampaign.Rows[0];
            int programAgreementID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);
            int formId = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_FORM_ID]);
            DateTime programAgreementDate = DateTime.Now;

            if (programAgreementID != 0)
            {
                programAgreementDate = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_START_DATE]);
            }
            clsUtil.SetProgramAgreementCatalogs(CheckBoxList1, formId, programAgreementDate);

            //ProgramAgreementCatalogSystem catalogSystem = new ProgramAgreementCatalogSystem();
            ProgramAgreementCatalog = dtsProgramAgreement.ProgramAgreementCatalog;  //catalogSystem.SelectAllByProgramAgreementID(programAgreementID);

            if (ProgramAgreementCatalog.Rows.Count > 0)
            {
                for (int i = 0; i < ProgramAgreementCatalog.Rows.Count; i++)
                {
                    DataRow catalogRow = ProgramAgreementCatalog.Rows[i];
                    ListItem item = CheckBoxList1.Items.FindByValue(catalogRow[ProgramAgreementCatalogTable.FLD_CATALOG_ID].ToString());
                    if (item != null)
                        item.Selected = true;
                }
            }

            if (!prgCampRow.IsNull(ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID))
            {
                int CampaignID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
                AccountSystem accSys = new AccountSystem();
                dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

                if (dtsAccount.Campaign.Rows.Count > 0)
                {
                    //Campaign Information
                    DataRow campRow = dtsAccount.Campaign.Rows[0];

                    lblProgramTypeName.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();
                    //Trade Class - ReadOnly
                    if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                        lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
                    else
                        lblTradeClass.Text = "None";
                }
                if (dtsAccount.Account.Rows.Count > 0)
                {
                    //Account Information
                    DataRow accRow = dtsAccount.Account.Rows[0];

                    //----------------------------------
                    //   Tax Information
                    //----------------------------------

                    if (!accRow.IsNull(AccountTable.FLD_TAX_EXEMPTION_NO))
                        lblTaxExemptionNumber.Text = accRow[AccountTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!accRow.IsNull(AccountTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(accRow[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();

                    lblAccID.Text = accRow[AccountTable.FLD_PKID].ToString();

                    if (!accRow.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                        lblEDSAccID.Text = accRow[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
                    else
                        lblEDSAccID.Text = "New Account";

                    lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();

                    //Account Status					
                    lblAccountStatus.Text = accRow[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString();
                    lblAccountStatus_ShortDescription.Text = accRow[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
                    lblAccountStatusColor.BackColor = Color.FromName(accRow[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());

                    //FM in the Account
                    lblAccountFMInfo.Text = accRow[AccountTable.FLD_FM_ID].ToString() + " - " + accRow[AccountTable.FLD_FM_NAME].ToString();

                    lblAccountComment.Text = accRow[AccountTable.FLD_COMMENTS].ToString();

                    //Organization Information
                    DataRow OrgRow = dtsAccount.Organization.Rows[0];
                    //Look if the definition of the Organization it's override
                    //In the AccountX Table
                    if (dtsAccount.AccountX.Rows.Count == 0)
                    {
                        //When read-only mode
                        //Organization Type
                        lblOrgType.Text = OrgRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();
                        //When read-only mode
                        //Organization Level
                        lblOrgLevel.Text = OrgRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();
                    }
                    else
                    {
                        DataRow accXRow = dtsAccount.AccountX.Rows[0];
                        //When read-only mode
                        //Organization Type
                        lblOrgType.Text = accXRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();

                        //When read-only mode
                        //Organization Level
                        lblOrgLevel.Text = accXRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();
                    }
                }
            }

            ////Program Agreement 
            if (prgRow.RowState == DataRowState.Added)
                lblProgramAgreementID.Text = "New Program Agreement";
            else
                lblProgramAgreementID.Text = prgRow[ProgramAgreementTable.FLD_PKID].ToString();

            //EDS ProgramAgreement #
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID))
                lblEDSProgramAgreementID.Text = prgRow[ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID].ToString();
            else
                lblEDSProgramAgreementID.Text = "New Program Agreement";

            if (prgRow.RowState == DataRowState.Added)
            {
                lblProgramAgreementStatus_ShortDescription.Text = "New Program Agreement";
                lblProgramAgreementStatusColor.BackColor = Color.White;
                //lblProgramAgreementStatus_Description.Text = "";
            }
            else
            {
                lblProgramAgreementStatus_ShortDescription.Text = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_SHORT_DESCRIPTION].ToString();
                lblProgramAgreementStatusColor.BackColor = Color.FromName(prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_COLOR_CODE].ToString());
                //lblProgramAgreementStatus_Description.Text = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_DESCRIPTION].ToString();				
            }

            float profit = 0;
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_PROFIT_RATE))
            {
                profit = Convert.ToSingle(prgRow[ProgramAgreementTable.FLD_PROFIT_RATE]);
            }
            radBtnLstProfitRate.SelectedIndex = radBtnLstProfitRate.Items.IndexOf(radBtnLstProfitRate.Items.FindByValue(profit.ToString()));

            string priced = string.Empty;
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_PRICED))
            {
                priced = prgRow[ProgramAgreementTable.FLD_PRICED].ToString();
                RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue(priced.ToLower()));
            }

            DualAddressFormControl.DataBind();

            //Program Agreement Terms	
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_START_DATE))
                txtStartDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_START_DATE]).ToShortDateString();
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_END_DATE))
                txtEndDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_END_DATE]).ToShortDateString();
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_HOLIDAY_START_DATE))
                txtHolidayStartDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_HOLIDAY_START_DATE]).ToShortDateString();
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_HOLIDAY_END_DATE))
                txtHolidayEndDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_HOLIDAY_END_DATE]).ToShortDateString();

            //Init
            txtEstimatedAmount.Text = "0";
            txtEnrollment.Text = "0";

            if (!prgRow.IsNull(ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS))
                txtEstimatedAmount.Text = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS]).ToString();
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_ENROLLMENT))
                txtEnrollment.Text = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_ENROLLMENT]).ToString();

            SetBusinessMessage();
        }
        private void FillCatalogTypeList()
        {
            CatalogSystem catalogSystem = new CatalogSystem();

            this.RadioButtonList1.Items.Clear();
            if (catalogSystem.IsCatalogAvailable(this.GetFormId(), true))
            {
                this.RadioButtonList1.Items.Add(new ListItem("Priced", "true"));
            }
            if (catalogSystem.IsCatalogAvailable(this.GetFormId(), false))
            {
                this.RadioButtonList1.Items.Add(new ListItem("Un priced", "false"));
            }
        }
        private void FillProfitRateList()
        {
            int formId = this.GetFormId();

            if (formId > 0)
            {
                List<double> profitRateList = QSP.Business.Fulfillment.FormProfitRate.GetProfitRateList(formId);

                foreach (double profitRate in profitRateList)
                {
                    radBtnLstProfitRate.Items.Add(new ListItem((profitRate * 100).ToString() + " %", profitRate.ToString()));
                }
            }
        }
        private void SetBusinessMessage()
        {
            int FormID = 0;
            if (!dtsProgramAgreement.ProgramAgreement.Rows[0].IsNull(ProgramAgreementTable.FLD_FORM_ID))
                FormID = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_FORM_ID]);

            clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.ProgramAgreementForm_Step3, FormID);
            trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args) {
            args.IsValid = (CheckBoxList1.SelectedIndex > -1);
        }
    }
}