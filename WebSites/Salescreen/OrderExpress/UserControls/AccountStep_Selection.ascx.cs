using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountStep_Selection.
    /// </summary>
    public partial class AccountStep_Selection : BaseWebFormControl {
        protected System.Web.UI.HtmlControls.HtmlTableRow trCampInfoTitle;
        private int CreationMode = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            GetQueryParam();
            if (!IsPostBack) {
                SetTitle();
                BindForm();
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
        }
        #endregion

        private void InitControl() {

        }

        private void GetQueryParam() {
            if (Request["OrgID"] != null) {
                CreationMode = 0;
            }
            else if (Request["MDRPID"] != null) {
                CreationMode = 1;
            }
            else {
                CreationMode = 2;
            }
        }

        private void SetTitle() {
            if (CreationMode == 0) {

                string sOrgId = Request["OrgID"].ToString();
                int OrgId = Convert.ToInt32(Request["OrgID"]);
                QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
                QSPForm.Common.DataDef.OrganizationTable dTblOrg = orgSys.SelectOne(OrgId);
                if (dTblOrg.Rows.Count > 0) {
                    DataRow orgRow = dTblOrg.Rows[0];
                    lblOrgNumber.Text = orgRow[OrganizationTable.FLD_PKID].ToString();
                    lblOrgName.Text = orgRow[OrganizationTable.FLD_NAME].ToString();
                }
            }
            else if (CreationMode == 1) {
                //MDR School
                string sMDRPID = Request["MDRPID"].ToString();
                QSPForm.Business.MDRSystem mdrSys = new QSPForm.Business.MDRSystem();
                QSPForm.Common.DataDef.CMDRTable dTblMDR = mdrSys.SelectOne(sMDRPID);
                if (dTblMDR.Rows.Count > 0) {
                    DataRow mdrRow = dTblMDR.Rows[0];
                    lblOrgNumber.Text = mdrRow[CMDRTable.FLD_PKID].ToString();
                    lblOrgName.Text = mdrRow[CMDRTable.FLD_NAME].ToString();
                }
            }
            else if (CreationMode == 2) {
                lblOrgNumber.Text = "0000";
                lblOrgName.Text = "New Organization";
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        private new void BindForm() {
            //Get the information about the account
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            DataTable dTbl = new DataTable();
            dTbl = formSys.SelectByEntityType(QSPForm.Common.EntityType.TYPE_ACCOUNT);
            dtgForm.DataSource = dTbl;
            dtgForm.DataBind();
        }

        private void GoToNextStep() {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step3);
            string url = "~/AccountForm_Step.aspx?NoMenu=42";
            if (dtgForm.SelectedIndex > -1) {
                string sFormID = dtgForm.DataKeys[dtgForm.SelectedIndex].ToString();
                if (CreationMode == 0) {

                    string sOrgId = Request["OrgID"].ToString();
                    Response.Redirect(url + "&OrgID=" + sOrgId + "&FormID=" + sFormID);
                }
                else if (CreationMode == 1) {
                    //MDR School
                    string sMDRPID = Request["MDRPID"].ToString();
                    Response.Redirect(url + "&MDRPID=" + sMDRPID + "&FormID=" + sFormID);
                }
                else if (CreationMode == 2) {
                    //Brand New Organization
                    Response.Redirect(url + "&FormID=" + sFormID);

                }
            }
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            GoToNextStep();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step1);
            string url = "AccountStep_Search.aspx";
            Response.Redirect(url);
        }

        protected void dtgForm_SelectedIndexChanged(object sender, System.EventArgs e) {
            GoToNextStep();
        }

        protected void dtgForm_ItemDataBound(object sender, DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                if (e.Item.DataItem != null) {
                    ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");
                    if (imgBtnDetail != null) {
                        string sCtrlID = imgBtnDetail.ClientID;
                        e.Item.Attributes.Add("OnClick", "document.getElementById('" + sCtrlID + "').click();");
                        //this.Page.RegisterRequiresPostBack(e.Item.Parent.Parent);                    
                    }
                }
            }
        }
    }
}