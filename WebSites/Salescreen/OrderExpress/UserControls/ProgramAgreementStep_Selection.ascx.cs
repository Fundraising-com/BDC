using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSP.Business.Fulfillment;
using QSP.OrderExpress.Web.Code;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class ProgramAgreementStep_Selection : BaseWebFormControl {
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Image imgTitle;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRefresh;

        public const string CAMPAIGN_ID = "CampID";
        public const string ACCOUNT_ID = "AccID";
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.HtmlControls.HtmlTableRow trFormInfoTitle;
        int CampaignID = 0;
        int AccountID = 0;
        int ProgramTypeID = 0;
        int userId = 0;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.dtgForm.ItemDataBound += new DataGridItemEventHandler(dtgForm_ItemDataBound);
            this.dtgForm.SelectedIndexChanged += new EventHandler(dtgForm_SelectedIndexChanged);
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e) 
        {
        }
        protected void Page_PreRender(object sender, System.EventArgs e) 
        {
        }
        protected void dtgForm_SelectedIndexChanged(object sender, System.EventArgs e) 
        {
            if (dtgForm.SelectedIndex > -1) 
            {
                string formId = dtgForm.DataKeys[dtgForm.SelectedIndex].ToString();

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramAgreementForm_Step3);
                string url = String.Format("~/ProgramAgreementForm_Step.aspx?FormID={0}&CampID={1}", formId, CampaignID.ToString());
                Response.Redirect(url);
            }
        }
        protected void dtgForm_ItemDataBound(object sender, DataGridItemEventArgs e) 
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
            {
                if (e.Item.DataItem != null) 
                {
                    ImageButton imgBtnDetail = (ImageButton)e.Item.FindControl("imgBtnDetail");

                    if (imgBtnDetail != null) 
                    {
                        string sCtrlID = imgBtnDetail.ClientID;
                        e.Item.Attributes.Add("OnClick", "document.getElementById('" + sCtrlID + "').click();");
                        //this.Page.RegisterRequiresPostBack(e.Item.Parent.Parent);
                    }
                }
            }
        }
        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramAgreementForm_Step1);

            string url = String.Format("~/ProgramAgreementStep_Search.aspx?AccID={0}", AccountID.ToString());
            Response.Redirect(url);
        }

        override protected void OnLoad(EventArgs e) 
        {
            if (!IsPostBack) 
            {
                GetQueryParam();
                BindForm();
            }
            else 
            {
                CampaignID = Convert.ToInt32(ViewState[CAMPAIGN_ID]);
                AccountID = Convert.ToInt32(ViewState[ACCOUNT_ID]);
            }

            //Load Information Page
            //And InitProgramAgreementData (create new row automatically)
            base.OnLoad(e);
        }
        private void GetQueryParam() 
        {
            if (Request[CAMPAIGN_ID] != null) 
            {
                CampaignID = Convert.ToInt32(Request[CAMPAIGN_ID]);
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                QSPForm.Common.DataDef.AccountTable dTblAcc = accSys.SelectAllByCampaignID(CampaignID);

                if (dTblAcc.Rows.Count > 0) 
                {
                    DataRow accRow = dTblAcc.Rows[0];
                    lblAccountNumber.Text = accRow[AccountTable.FLD_PKID].ToString();
                    lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
                    AccountID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
                }

                ViewState[CAMPAIGN_ID] = CampaignID;
                ViewState[ACCOUNT_ID] = AccountID;
            }
        }
        private new void BindForm() 
        {
            //Get the information about the account
            QSPForm.Business.FormSystem formSystem = new QSPForm.Business.FormSystem();
            List<LinqEntity.Form> formList = formSystem.SelectByEntityTypeAndUserPermissions(EntityTypeEnum.ProgramAgreement, this.UserId, this.AccountID);

            // Bind to the grid
            dtgForm.DataSource = formList;
            dtgForm.DataBind();
        }
        public int UserId {
            get { return this.userId; }
            set { this.userId = value; }
        }

    }
}