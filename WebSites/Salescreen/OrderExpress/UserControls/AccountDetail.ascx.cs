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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class AccountDetail : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_AccID = 0;
        private int c_CampaignID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        protected System.Web.UI.WebControls.ImageButton imgBtnDelete;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected System.Web.UI.WebControls.HyperLink hypLnkCancel;

        public const string ACC_ID = "AccID";
        public const string CAMP_ID = "CampID";
        private const string ACC_DATA = "AccData";
        protected dataDef dtsAccount;
        
        protected void Page_Load(object sender, System.EventArgs e) {

            try {
                // Put user code to initialize the page here	
                LoadData();
                HeaderDetail.InitializeControls();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                }

                //imgBtnSave.Attributes.Add("OnClick","document.clear();false;");
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.QSPToolBar.DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this account ?');");

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.QSPToolBar.SaveClick += new EventHandler(this.QSPToolBar_SaveClick);
            this.QSPToolBar.DeleteClick += new EventHandler(this.QSPToolBar_DeleteClick);
            HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
        }

        #endregion

        void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            Save();
        }

        public int AccID {
            get {
                return c_AccID;
            }
            set {
                c_AccID = value;
                ViewState[ACC_ID] = c_AccID;
            }
        }
        public int CampaignID {
            get {
                return c_CampaignID;
            }
            set {
                c_CampaignID = value;
                ViewState[ACC_ID] = c_CampaignID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ACC_DATA] = dtsAccount;
        }
        private void QSPToolBar_DeleteClick(object sender, EventArgs e) {
            try {
                //delete the account. 
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                accSys.Delete(this.AccID, this.Page.UserID);
                //close window
                this.Page.RegisterClientScriptBlock("close", "<script>alert('this account has been deleted');window.opener.RefreshPage();window.close();</script>");
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
        private void QSPToolBar_SaveClick(object sender, EventArgs e)
        {
            try
            {
                HeaderDetail.ResetStatus();

                Save();
            }
            catch (Exception ex)
            {
                //				if (ex is QSPForm.Common.QSPFormValidationException)
                //				{
                //					Common.QSPFormValidationException exVal = (Common.QSPFormValidationException) ex;
                //					if ((exVal.ValidationExceptionType == Common.QSPFormExceptionType.RecordIsDeleted) ||
                //					   (exVal.ValidationExceptionType == Common.QSPFormExceptionType.RecordIsModified))
                //					{
                //						String sMessage = exVal.Message;
                //						String sTitle = "Concurential Modification Error";
                //					}
                //				}
                Page.SetPageError(ex);
            }
        }

        protected void SetFormParameter() {
            if (Request[ACC_ID] != null) {
                c_AccID = Convert.ToInt32(Request[ACC_ID].ToString());
            }
            else {
                c_AccID = 0;
            }
            ViewState[ACC_ID] = c_AccID;

        }

        protected override void LoadData()
        {
            if (!IsPostBack)
            {
                SetFormParameter();
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailWithLastCampaign(c_AccID);
                if (!accSys.IsRenew(dtsAccount.Account))
                    dtsAccount = accSys.RenewAccount(c_AccID, this.Page.UserID);

                this.ViewState[ACC_ID] = c_AccID;
                this.ViewState[ACC_DATA] = dtsAccount;
            }
            else
            {
                c_AccID = Convert.ToInt32(this.ViewState[ACC_ID]);
                dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            }

            HeaderDetail.AccountID = c_AccID;
            HeaderDetail.DataSource = dtsAccount;

        }
        public override void BindForm()
        {
            HeaderDetail.BindForm();
        }

        private void Save() {
            Boolean blnValid = true;

            blnValid = HeaderDetail.ValidateForm();
            if (!blnValid) {
                return;
            }

            blnValid = HeaderDetail.UpdateDataSource();
            if (!blnValid) {
                return;
            }

            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            blnValid = accSys.UpdateAllDetail(dtsAccount, this.Page.UserID);

            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountDetailInfo, AccountDetail.ACC_ID, c_AccID.ToString());
            //Response.Redirect("AccountDetailInfo.aspx?AccID=" + c_AccID.ToString(), true);
            //Response.Redirect(url);

            Response.Redirect(string.Format("~/V2/Forms/AccountView.aspx?AccountId={0}", c_AccID));

        }
    }
}