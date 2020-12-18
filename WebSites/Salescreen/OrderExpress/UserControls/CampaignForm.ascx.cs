using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CampaignTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountDetail.
    /// </summary>
    public partial class CampaignForm : BaseWebFormControl {
        private int c_CampID = 0;
        protected dataDef dtblCampaign = new dataDef();
        protected DataTable dtblProgType = new DataTable();
        QSPForm.Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                clsUtil.SetJScriptForOpenSelector(imgBtnSelect, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
                clsUtil.SetJScriptForOpenSelector(imgBtnSelectAcc, txtAccountID, txtFULFAccountID, QSPForm.Business.AppItem.AccountSelector, 0, 0);
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
            //			dtblCampaign = campSys.SelectOne(c_CampID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList				
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int CampaignID {
            get {
                return c_CampID;
            }
            set {
                c_CampID = value;
                ViewState["CampID"] = c_CampID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblCampaign;
            }
            set {
                dtblCampaign = value;
            }
        }

        public override void BindForm() {
            FillList();
            if (dtblCampaign.Rows.Count > 0) {
                DataRow row;
                row = dtblCampaign.Rows[0];
                txtCampID.Text = row[dataDef.FLD_PKID].ToString();
                txtName.Text = row[dataDef.FLD_NAME].ToString();
                if (row[dataDef.FLD_START_DATE] != System.DBNull.Value)
                    txtStartDate.Text = Convert.ToDateTime(row[dataDef.FLD_START_DATE]).ToShortDateString();
                if (row[dataDef.FLD_END_DATE] != System.DBNull.Value)
                    txtEndDate.Text = Convert.ToDateTime(row[dataDef.FLD_END_DATE]).ToShortDateString();
                if (row[dataDef.FLD_FISCAL_YEAR] != System.DBNull.Value)
                    txtFiscalYear.Text = row[dataDef.FLD_FISCAL_YEAR].ToString();
                if (row[dataDef.FLD_ACCOUNT_ID] != System.DBNull.Value) {
                    txtAccountID.Text = row[dataDef.FLD_ACCOUNT_ID].ToString();
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetailAcc,QSPForm.Business.AppItem.AccountDetail,AccountDetail.ACC_ID,txtAccountID.Text, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailAcc, "AccountDetail.aspx?", AccountDetail.ACC_ID, txtAccountID.Text, 0, 0);
                    if (row[AccountTable.FLD_FULF_ACCOUNT_ID] != System.DBNull.Value)
                        txtFULFAccountID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
                }

                ListItem lstItem;
                if (row[dataDef.FLD_PROG_TYPE_ID] != System.DBNull.Value) {
                    lstItem = ddlType.Items.FindByValue(row[dataDef.FLD_PROG_TYPE_ID].ToString());
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
                if (row[dataDef.FLD_ENROLLMENT] != System.DBNull.Value)
                    txtEnrollment.Text = row[dataDef.FLD_ENROLLMENT].ToString();
                if (row[dataDef.FLD_GOAL_ESTIMATED_GROSS] != System.DBNull.Value)
                    txtEstimatedAmount.Text = Convert.ToDecimal(row[dataDef.FLD_GOAL_ESTIMATED_GROSS]).ToString("F2");
                if (row[dataDef.FLD_WAREHOUSE_ID] != System.DBNull.Value)
                    txtWarehouse.Text = row[dataDef.FLD_WAREHOUSE_ID].ToString();
                if (row[dataDef.FLD_COMMENTS] != System.DBNull.Value)
                    txtComments.Text = row[dataDef.FLD_COMMENTS].ToString();
            }
            else {
                Page.SetPageMessage("This campaign have been deleted");
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            // get edited row values in grid
            DataRow row = dtblCampaign.Rows[0];

            row[dataDef.FLD_PROG_TYPE_ID] = ddlType.SelectedValue;
            row[dataDef.FLD_NAME] = txtName.Text;

            //Fiscal Year
            if (txtFiscalYear.Text.Length > 0)
                row[dataDef.FLD_FISCAL_YEAR] = Convert.ToInt32(txtFiscalYear.Text);
            else
                row[dataDef.FLD_FISCAL_YEAR] = System.DBNull.Value;

            //Warehouse			
            if (txtWarehouse.Text.Length > 0)
                row[dataDef.FLD_WAREHOUSE_ID] = Convert.ToInt32(txtWarehouse.Text);
            else
                row[dataDef.FLD_WAREHOUSE_ID] = System.DBNull.Value;

            //Start Date
            if (txtStartDate.Text.Length > 0)
                row[dataDef.FLD_START_DATE] = Convert.ToDateTime(txtStartDate.Text);
            else
                row[dataDef.FLD_START_DATE] = System.DBNull.Value;

            //End Date
            if (txtEndDate.Text.Length > 0)
                row[dataDef.FLD_END_DATE] = Convert.ToDateTime(txtEndDate.Text);
            else
                row[dataDef.FLD_END_DATE] = System.DBNull.Value;

            //FM
            row[dataDef.FLD_FM_ID] = txtFMID.Text;

            //Tax Exemption
            row[dataDef.FLD_TAX_EXEMPTION_NO] = txtTaxExemptionNumber.Text;
            if (txtTaxExemptionExpirationDate.Text.Length > 0)
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = Convert.ToDateTime(txtTaxExemptionExpirationDate.Text);
            else
                row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] = System.DBNull.Value;

            //Enrollment
            if (txtEnrollment.Text.Length > 0)
                row[dataDef.FLD_ENROLLMENT] = Convert.ToInt32(txtEnrollment.Text);
            else
                row[dataDef.FLD_ENROLLMENT] = System.DBNull.Value;

            //Gross Estimated Amount
            if (txtEstimatedAmount.Text.Length > 0)
                row[dataDef.FLD_GOAL_ESTIMATED_GROSS] = Convert.ToDecimal(txtEstimatedAmount.Text);
            else
                row[dataDef.FLD_GOAL_ESTIMATED_GROSS] = System.DBNull.Value;

            row[dataDef.FLD_COMMENTS] = txtComments.Text;

            if (c_CampID <= 0) {
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
                dtblProgType = comSys.SelectAllProgramType();
                ddlType.DataSource = dtblProgType;
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