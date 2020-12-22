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
using dataDef = QSPForm.Common.DataDef.OrganizationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for OrganizationDetail.
    /// </summary>
    public partial class OrganizationDetail : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_OrgID = 0;
        private int c_CampaignID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        protected System.Web.UI.WebControls.HyperLink hypLnkCancel;

        public const string ORG_ID = "OrgID";
        private const string ORG_DATA = "OrgData";
        protected dataDef dtsOrganization;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                HeaderDetail.InitializeControls();
                if (!IsPostBack) {
                    BindForm();
                }
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
            this.Page.MaintainScrollPositionOnPostBack = true;

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
            this.imgBtnCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnCancel_Click);
            this.HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
        }

        #endregion

        protected void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            Save();
        }

        public int OrgID {
            get {
                return c_OrgID;
            }
            set {
                c_OrgID = value;
                ViewState[ORG_ID] = c_OrgID;
            }
        }

        public int CampaignID {
            get {
                return c_CampaignID;
            }
            set {
                c_CampaignID = value;
                ViewState[ORG_ID] = c_CampaignID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ORG_DATA] = dtsOrganization;
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        protected void SetFormParameter() {
            if (Request[ORG_ID] != null) {
                c_OrgID = Convert.ToInt32(Request[ORG_ID].ToString());
            }
            else {
                c_OrgID = 0;
            }
            ViewState[ORG_ID] = c_OrgID;

        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
                dtsOrganization = orgSys.SelectAllDetail(c_OrgID);

                this.ViewState[ORG_ID] = c_OrgID;
                this.ViewState[ORG_DATA] = dtsOrganization;
            }
            else {
                c_OrgID = Convert.ToInt32(this.ViewState[ORG_ID]);
                dtsOrganization = (dataDef)this.ViewState[ORG_DATA];
            }

            HeaderDetail.OrganizationID = c_OrgID;
            HeaderDetail.DataSource = dtsOrganization;
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                HeaderDetail.ResetStatus();

                Save();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void imgBtnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrganizationDetailInfo, OrganizationDetail.ORG_ID, c_OrgID.ToString());
            string url = "~/OrganizationDetailInfo.aspx?" + OrganizationDetail.ORG_ID + "=" + c_OrgID.ToString();
            Response.Redirect(url);
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
            QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
            blnValid = orgSys.UpdateAllDetail(dtsOrganization);

            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrganizationDetailInfo, OrganizationDetail.ORG_ID, c_OrgID.ToString());
            string url = "~/OrganizationDetailInfo.aspx?" + OrganizationDetail.ORG_ID + "=" + c_OrgID.ToString();
            Response.Redirect(url);
        }
    }
}