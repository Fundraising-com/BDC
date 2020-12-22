using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.AccountTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountDetail.
    /// </summary>
    public partial class AccountHeaderForm : BaseWebFormControl {
        private int c_AccID = 0;
        protected dataDef dtblAccount = new dataDef();
        protected DataTable dtblAccType = new DataTable();
        QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                clsUtil.SetJScriptForOpenSelector(imgBtnSelect, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
                clsUtil.SetJScriptForOpenSelector(imgBtnSelectOrg, txtOrganizationID, txtOrganizationName, QSPForm.Business.AppItem.OrganizationSelector, 0, 0);
            }
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

        protected override void LoadData() {
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

        public int AccountID {
            get {
                return c_AccID;
            }
            set {
                c_AccID = value;
                ViewState["AccID"] = c_AccID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblAccount;
            }
            set {
                dtblAccount = value;
            }
        }

        public override void BindForm() {
            FillList();
            if (dtblAccount.Rows.Count > 0) {
                DataRow row;
                row = dtblAccount.Rows[0];
                txtAccID.Text = row[dataDef.FLD_PKID].ToString();
                if (row[dataDef.FLD_FULF_ACCOUNT_ID] != System.DBNull.Value)
                    txtEDSAccount.Text = row[dataDef.FLD_FULF_ACCOUNT_ID].ToString();
                if (row[dataDef.FLD_ORG_ID] != System.DBNull.Value) {
                    txtOrganizationID.Text = row[dataDef.FLD_ORG_ID].ToString();
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetailOrg, QSPForm.Business.AppItem.OrganizationDetail, OrganizationDetail.ORG_ID,txtOrganizationID.Text, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailOrg, "OrganizationDetail.aspx?", OrganizationDetail.ORG_ID, txtOrganizationID.Text, 0, 0);
                    if (row[OrganizationTable.FLD_NAME] != System.DBNull.Value)
                        txtOrganizationName.Text = row[OrganizationTable.FLD_NAME].ToString();
                }

                ListItem lstItem;
                if (row[dataDef.FLD_ACCOUNT_TYPE_ID] != System.DBNull.Value) {
                    lstItem = ddlType.Items.FindByValue(row[dataDef.FLD_ACCOUNT_TYPE_ID].ToString());
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
                if (row[dataDef.FLD_CREDIT_LIMIT] != System.DBNull.Value)
                    txtCreditLimit.Text = Convert.ToDecimal(row[dataDef.FLD_CREDIT_LIMIT]).ToString("F");
                if (row[dataDef.FLD_COMMENTS] != System.DBNull.Value)
                    txtComments.Text = row[dataDef.FLD_COMMENTS].ToString();
            }
            else {
                Page.SetPageMessage("This organization doesn't exist anymore");
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            // get edited row values in grid
            DataRow row = dtblAccount.Rows[0];
            row[dataDef.FLD_ACCOUNT_TYPE_ID] = ddlType.SelectedValue;
            if (txtEDSAccount.Text.Length > 0)
                row[dataDef.FLD_FULF_ACCOUNT_ID] = Convert.ToInt32(txtEDSAccount.Text);
            row[dataDef.FLD_FM_ID] = txtFMID.Text;
            row[dataDef.FLD_TAX_EXEMPTION_NO] = txtTaxExemptionNumber.Text;
            if (txtTaxExemptionExpirationDate.Text.Length > 0)
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = Convert.ToDateTime(txtTaxExemptionExpirationDate.Text);
            else
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = System.DBNull.Value;
            row[dataDef.FLD_CREDIT_LIMIT] = Convert.ToDecimal(txtCreditLimit.Text);
            row[dataDef.FLD_COMMENTS] = txtComments.Text;

            if (c_AccID <= 0) {
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
                dtblAccType = comSys.SelectAllAccountType();
                ddlType.DataSource = dtblAccType;
                ddlType.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    DataView dv = new DataView(dt);
                    dv.Sort = dt.Columns[0].ColumnName;
                    iIndex = dv.Find(sValue);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }
    }
}