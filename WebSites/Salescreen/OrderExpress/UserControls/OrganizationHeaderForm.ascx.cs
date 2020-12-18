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
    public partial class OrganizationHeaderForm : BaseWebFormControl {
        private int c_OrgID = 0;
        protected dataDef dtblOrganization = new dataDef();
        protected DataTable dtblOrgType = new DataTable();
        protected DataTable dtblOrgLevel = new DataTable();
        private QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (!IsPostBack) {
                //clsUtil.SetJScriptForOpenSelector(imgBtnSelect,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector , 0,0);
                //clsUtil.SetJScriptForOpenSelector(imgBtnSelectMDR,txtMDRPID, QSPForm.Business.AppItem.MDRSchoolSelector ,0,0);

                clsUtil.SetJScriptForOpenSelector(imgBtnSelect, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
                clsUtil.SetJScriptForOpenSelector(imgBtnSelectMDR, txtMDRPID, null, "MDRSchoolSelector.aspx", "MDRSchoolSelector", 0, 0, null);
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
                txtOrgID.Text = row[dataDef.FLD_PKID].ToString();
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

                if (row[dataDef.FLD_ORG_LEVEL_ID] != System.DBNull.Value) {
                    lstItem = ddlLevel.Items.FindByValue(row[dataDef.FLD_ORG_LEVEL_ID].ToString());
                    if (lstItem != null) {
                        ddlLevel.ClearSelection();
                        lstItem.Selected = true;
                    }
                }

                if (row[dataDef.FLD_FM_ID] != System.DBNull.Value) {
                    txtFMID.Text = row[dataDef.FLD_FM_ID].ToString();
                    //clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.FM_Detail,"FMID",txtFMID.Text.Trim(),700,600);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetail, "FMInfo.aspx?", "FMID", txtFMID.Text.Trim(), 700, 600);
                    if (row[dataDef.FLD_FM_NAME] != System.DBNull.Value) {
                        txtFMName.Text = row[dataDef.FLD_FM_NAME].ToString();
                    }
                }
                if (row[dataDef.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                    txtTaxExemptionNumber.Text = row[dataDef.FLD_TAX_EXEMPTION_NO].ToString();
                if (row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                    txtTaxExemptionNumber.Text = Convert.ToDateTime(row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                if (row[dataDef.FLD_MDRPID] != System.DBNull.Value)
                    txtMDRPID.Text = row[dataDef.FLD_MDRPID].ToString();
                //clsUtil.SetJScriptForOpenDetail(imgBtnDetailMDR, QSPForm.Business.AppItem.MDR_Detail,"MDRPID",txtMDRPID.Text.Trim(),700,600);
                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailMDR, "MDRSchoolInfo.aspx?", "MDRPID", txtMDRPID.Text.Trim(), 700, 600);

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
            DataRow row = dtblOrganization.Rows[0];
            row[dataDef.FLD_ORG_TYPE_ID] = ddlType.SelectedValue;
            row[dataDef.FLD_ORG_LEVEL_ID] = ddlLevel.SelectedValue;
            row[dataDef.FLD_NAME] = txtName.Text;
            row[dataDef.FLD_FM_ID] = txtFMID.Text;
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
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Org Type
                dtblOrgType = comSys.SelectAllOrganizationType();
                ddlType.DataSource = dtblOrgType;
                ddlType.DataBind();
                //Org Level
                dtblOrgLevel = comSys.SelectAllOrganizationLevel();
                ddlLevel.DataSource = dtblOrgLevel;
                ddlLevel.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //		private void SetFmTool(string sID)
        //		{
        //			
        //			if (sID.Length >0)
        //				clsUtil.SetJScriptForOpenDetail(imgBtnDetail, "FMDetail","FMID",sID,700,600);
        //				//imgBtnDetail.Attributes.Add("OnClick", "window.open('FMDetail.aspx?FMID=" + sID + "','','toobars=yes,scrollbars=yes,width=700,height=600,resizable=yes');return false;");
        //			clsUtil.SetJScriptForOpenSelector(imgBtnSelect,txtFMID,txtFMName,"FMSelector",0,0);
        ////			imgBtnSelect.Attributes.Add("OnClick", "OpenFMSelector('" + txtFMID.ClientID + "', '" + txtFMName.ClientID + "');return false;");
        //		
        //		}
    }
}