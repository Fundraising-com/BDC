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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class ProgramAgreementStep_Search : BaseWebFormControl {
        protected System.Web.UI.WebControls.ImageButton imgBtnBack;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label lblFormCode;
        protected System.Web.UI.WebControls.Label lblFormName;
        protected System.Web.UI.HtmlControls.HtmlTableRow trCampInfoTitle;
        protected System.Web.UI.HtmlControls.HtmlTableRow trFormInfoTitle;

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        override protected void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step1);
                string url = "~/AccountStep_Search.aspx";
                hypLnkNewAccount.NavigateUrl = url;
            }

            //Load Information Page
            //And InitProgramAgreementData (create new row automatically)
            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            this.InitControl();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AccountList_AddProgStep.SelectedIndexChanged += new EventHandler(AccountList_AddProgStep_SelectedIndexChanged);

        }
        #endregion

        private void InitControl() {
            AccountList_AddProgStep.SearchAppItem = QSPForm.Business.AppItem.AccountList;
            AccountList_AddProgStep.EntityTypeID = QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT;
        }

        private void GetQueryParam() {
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public void GoToNextStep() {
            if (AccountList_AddProgStep.SelectedValue > -1) {
                int AccID = AccountList_AddProgStep.SelectedValue;

                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                QSPForm.Common.DataDef.AccountTable accTable = new AccountTable();
                accTable = accSys.SelectOne(Convert.ToInt32(AccountList_AddProgStep.SelectedValue));
                if (accTable.Rows.Count > 0) {
                    DataRow row = accTable.Rows[0];
                    string sCampID = "0";
                    if (!row.IsNull(AccountTable.FLD_LAST_CAMPAIGN_ID)) {
                        sCampID = row[AccountTable.FLD_LAST_CAMPAIGN_ID].ToString();
                    }
                    if (sCampID != "0") {
                        string url = "~/ProgramAgreementStep_Selection.aspx?";
                        Response.Redirect(url + "&CampID=" + sCampID);
                    }
                    else {
                        string url = "~/AccountStep_Search.aspx?";
                        Response.Redirect(url + "&AccID=" + AccID.ToString());
                    }
                }
            }
        }

        private void AccountList_AddProgStep_SelectedIndexChanged(object sender, EventArgs e) {
            GoToNextStep();
        }
    }
}