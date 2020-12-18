using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrganizationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrganizationDetail.
    /// </summary>
    public partial class OrgStep_Detail : BaseOrganizationFormStep {
        private int c_OrgID = 0;
        protected dataDef dtblOrganization = new dataDef();
        protected DataTable dtblOrgType = new DataTable();
        protected System.Web.UI.WebControls.Label lblLabelOrgID;
        protected System.Web.UI.WebControls.TextBox txtOrgID;
        QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                string NoMenu = Convert.ToInt32(QSPForm.Business.AppItem.FMSelector).ToString();
                clsUtil.SetJScriptForOpenSelector(imgBtnSelect, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitControl();
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

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrgForm_Step1;
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step2;
            this.NextAppItem = QSPForm.Business.AppItem.OrgForm_Step3;
            this.ImageButtonNext = imgBtnNext;
            this.ImageButtonBack = imgBtnBack;

        }

        private void LoadData() {
            //			dtblOrganization = orgSys.SelectOne(c_OrgID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db					
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int OrganizationID {
            get {
                return c_OrgID;
            }
            set {
                c_OrgID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtblOrganization;
            }
            set {
                dtblOrganization = value;
            }
        }

        public override void BindForm() {
            LoadData();
            FillList();
            if (dtblOrganization.Rows.Count > 0) {
                DataRow row;
                row = dtblOrganization.Rows[0];

                if (row[dataDef.FLD_NAME] != System.DBNull.Value)
                    txtName.Text = row[dataDef.FLD_NAME].ToString();

                ListItem lstItem;
                if (row[dataDef.FLD_ORG_TYPE_ID] != System.DBNull.Value) {
                    lstItem = ddlType.Items.FindByValue(row[dataDef.FLD_ORG_TYPE_ID].ToString());
                    if (lstItem != null) {
                        ddlType.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
                if (row[dataDef.FLD_FM_ID] != System.DBNull.Value) {
                    txtFMID.Text = row[dataDef.FLD_FM_ID].ToString();
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.FM_Detail,"FMID",txtFMID.Text.Trim(),700,600);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetail, "FMInfo.aspx?", "FMID", txtFMID.Text.Trim(), 700, 600);

                    if (row["fm_name"] != System.DBNull.Value) {
                        txtFMName.Text = row["fm_name"].ToString();
                    }
                }
                if (row[dataDef.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                    txtTaxExemptionNumber.Text = row[dataDef.FLD_TAX_EXEMPTION_NO].ToString();
                if (row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                    txtTaxExemptionNumber.Text = Convert.ToDateTime(row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                if (row[dataDef.FLD_COMMENTS] != System.DBNull.Value)
                    txtComments.Text = row[dataDef.FLD_COMMENTS].ToString();
            }
        }

        public override bool Update() {
            bool IsSuccess = false;
            // get edited row values in grid
            DataRow row = dtblOrganization.Rows[0];
            row[dataDef.FLD_ORG_TYPE_ID] = ddlType.SelectedValue;
            row[dataDef.FLD_ORG_TYPE_NAME] = ddlType.SelectedItem.Text;
            row[dataDef.FLD_NAME] = txtName.Text;
            row[dataDef.FLD_FM_ID] = txtFMID.Text;
            row[dataDef.FLD_FM_NAME] = txtFMName.Text;
            row[dataDef.FLD_TAX_EXEMPTION_NO] = txtTaxExemptionNumber.Text;
            TextBox txt = txtTaxExemptionExpirationDate;
            if (txt.Text.Length > 0)
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = Convert.ToDateTime(txt.Text);
            else
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = System.DBNull.Value;

            row[dataDef.FLD_COMMENTS] = txtComments.Text;

            if (c_OrgID <= 0) {
                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
            }
            else {
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            }

            IsSuccess = true;

            return IsSuccess;
        }

        private void FillList() {
            try {
                //Campaign Type
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                dtblOrgType = comSys.SelectAllOrganizationType();
                ddlType.DataSource = dtblOrgType;
                ddlType.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            txtFMID.Enabled = (this.Page.Role > 2);
            imgBtnDetail.Visible = (this.Page.Role > 2);
            imgBtnSelect.Visible = (this.Page.Role > 2);
        }
    }
}