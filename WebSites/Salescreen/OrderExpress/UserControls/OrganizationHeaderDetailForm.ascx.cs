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
using dataRef = QSPForm.Common.DataDef.OrganizationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrganizationHeaderDetailForm : BaseWebFormControl {
        protected OrganizationData dtsOrganization;
        private CommonUtility util = new CommonUtility();
        CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.CheckBox chkBoxShipSameAddress;
        private int c_OrgID = 0;
        protected System.Web.UI.WebControls.Label lblAccID;
        protected System.Web.UI.WebControls.Label Label3;
        private int c_CampID = 0;
        public event System.EventHandler AddressHygieneConfirmed;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			

            if (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER)
                clsUtil.SetJScriptForOpenCalendar(hypLnkTaxExemptionExpirationDate, txtTaxExemptionExpirationDate);

            //	clsUtil.SetJScriptForOpenSelector(imgBtnSelectMDR,txtMDRPID,QSPForm.Business.AppItem.MDRSchoolSelector ,0,0);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectMDR, txtMDRPID, null, "MDRSchoolSelector.aspx", "MDRSchoolSelector", 0, 0, null);
            //Manage when the organziation name is entered and copied 
            //into the Postal Address Billing Org_name 
            clsUtil.SetJScriptForCopyValueFromCtrlToCtrl(txtOrganizationName, DualAddressFormControl.EntityNameTextBox);
            DualAddressFormControl.EntityNameTextBox.ReadOnly = true;
            DualAddressFormControl.EntityNameTextBox.Attributes.Add("onfocus", "javascript:window.focus();");
            DualAddressFormControl.EntityNameTextBox.BackColor = Color.LightGray;
        }

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
            this.DualAddressFormControl.AddressHygieneConfirmed += new EventHandler(DualAddressFormControl_AddressHygieneConfirmed);
        }

        #endregion

        void DualAddressFormControl_AddressHygieneConfirmed(object sender, EventArgs e) {
            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        public int OrganizationID {
            get {
                return c_OrgID;
            }
            set {
                c_OrgID = value;
                ViewState["OrgID"] = c_OrgID;
            }
        }

        public int CampaignID {
            get {
                return c_CampID;
            }
            set {
                c_CampID = value;
            }
        }

        public OrganizationData DataSource {
            get {
                return dtsOrganization;
            }
            set {
                dtsOrganization = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //Display management of the Tax Info an Fm
            tblTaxInfoEdit.Visible = (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER);
            tblTaxInfoReadOnly.Visible = !tblTaxInfoEdit.Visible;
            //Display the MDR Selector only when the Role Is at least Field Support
            trMDREdit.Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);
            trMDRInfo.Visible = !trMDREdit.Visible;
        }

        public void InitializeControls() {
            DualAddressFormControl.BillingParentID = OrganizationID;
            DualAddressFormControl.ShippingParentID = OrganizationID;
            DualAddressFormControl.BillingParentType = EntityType.TYPE_ORGANIZATION;
            DualAddressFormControl.ShippingParentType = EntityType.TYPE_ORGANIZATION;
            DualAddressFormControl.DataSource = DataSource;
        }

        public override void BindForm() {
            if (!IsPostBack) {
                FillList();
            }
            //OrganizationTable 
            OrganizationTable dtblOrganization = dtsOrganization.Organization;
            if (dtblOrganization.Rows.Count > 0) {
                DataRow row = dtblOrganization.Rows[0];
                ListItem lstItem;

                if (row.RowState == DataRowState.Added)
                    lblOrgID.Text = "New Organization";
                else
                    lblOrgID.Text = row[OrganizationTable.FLD_PKID].ToString();

                txtOrganizationName.Text = row[OrganizationTable.FLD_NAME].ToString();
                txtComment.Text = row[OrganizationTable.FLD_COMMENTS].ToString();

                //Display Organization Information
                //Organization Type
                int OrgTypeID = 0;
                if (row[OrganizationTable.FLD_ORG_TYPE_ID] != DBNull.Value)
                    OrgTypeID = Convert.ToInt32(row[OrganizationTable.FLD_ORG_TYPE_ID]);
                if (OrgTypeID > 0) {
                    lstItem = ddlOrgType.Items.FindByValue(OrgTypeID.ToString());
                    if (lstItem != null) {
                        ddlOrgType.ClearSelection();
                        lstItem.Selected = true;
                    }
                }

                //Organization Level
                int OrgLevelID = 0;
                if (row[OrganizationTable.FLD_ORG_LEVEL_ID] != DBNull.Value)
                    OrgLevelID = Convert.ToInt32(row[OrganizationTable.FLD_ORG_LEVEL_ID]);
                if (OrgLevelID > 0) {
                    lstItem = ddlOrgLevel.Items.FindByValue(OrgLevelID.ToString());
                    if (lstItem != null) {
                        ddlOrgLevel.ClearSelection();
                        lstItem.Selected = true;
                    }
                }

                //MDR School Information
                if (row[OrganizationTable.FLD_MDRPID] != System.DBNull.Value)
                    txtMDRPID.Text = row[OrganizationTable.FLD_MDRPID].ToString();

                //----------------------------------
                //   Tax Information --  from Organization
                //----------------------------------
                if (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER) {
                    if (!row.IsNull(OrganizationTable.FLD_TAX_EXEMPTION_NO))
                        txtTaxExemptionNumber.Text = row[OrganizationTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!row.IsNull(OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        txtTaxExemptionExpirationDate.Text = Convert.ToDateTime(row[OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                }
                else {
                    if (!row.IsNull(OrganizationTable.FLD_TAX_EXEMPTION_NO))
                        lblTaxExemptionNumber.Text = row[OrganizationTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!row.IsNull(OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(row[OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                }

            }

            DualAddressFormControl.DataBind();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        private void FillList() {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable dt;
            dt = comSys.SelectAllOrganizationType();
            ddlOrgType.DataSource = dt;
            ddlOrgType.DataBind();

            dt = comSys.SelectAllOrganizationLevel();
            ddlOrgLevel.DataSource = dt;
            ddlOrgLevel.DataBind();
        }

        public bool UpdateDataSource() {
            //Organization
            DataRow row = dtsOrganization.Organization.Rows[0];

            clsUtil.UpdateRow(row, OrganizationTable.FLD_NAME, txtOrganizationName.Text.Trim());
            //Org Type
            row[OrganizationTable.FLD_ORG_TYPE_ID] = ddlOrgType.SelectedValue;
            row[OrganizationTable.FLD_ORG_TYPE_NAME] = ddlOrgType.SelectedItem.Text;
            //Org Level
            row[OrganizationTable.FLD_ORG_LEVEL_ID] = ddlOrgLevel.SelectedValue;
            row[OrganizationTable.FLD_ORG_LEVEL_NAME] = ddlOrgLevel.SelectedItem.Text;

            //Org MDR PID
            row[OrganizationTable.FLD_MDRPID] = txtMDRPID.Text.ToString().Trim();

            //----------------------------------------------
            //     Tax Information -- Only Admin can change that
            //----------------------------------------------
            if (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER) {
                if (txtTaxExemptionNumber.Text.Trim().Length > 0)
                    clsUtil.UpdateRow(row, OrganizationTable.FLD_TAX_EXEMPTION_NO, txtTaxExemptionNumber.Text.Trim());

                compVal_TaxExemptionExpirationDate.Validate();
                if (compVal_TaxExemptionExpirationDate.IsValid && (txtTaxExemptionExpirationDate.Text.Trim().Length > 0))
                    clsUtil.UpdateRow(row, OrganizationTable.FLD_TAX_EXEMPTION_EXP_DATE, txtTaxExemptionExpirationDate.Text.Trim());
            }
            row[OrganizationTable.FLD_COMMENTS] = txtComment.Text;

            DualAddressFormControl.Update();

            return true;
        }

        public bool ValidateForm() {
            bool isValid = true;

            trValSumOrganizationInfo.Visible = false;

            if (isValid && !IsValid(tblOrganizationInfo.Controls)) {
                trValSumOrganizationInfo.Visible = true;
                clsUtil.RenderStartUpScroll(lblTitleOrganizationInfo);
                this.Page.MaintainScrollPositionOnPostBack = false;
                isValid = false;
            }

            isValid = (isValid && DualAddressFormControl.IsValid());

            return isValid;
        }

        private void ddlOrgLevel_DataBinding(object sender, System.EventArgs e) {
        }

        public void ResetStatus() {
            DualAddressFormControl.ResetStatus();
        }
    }
}