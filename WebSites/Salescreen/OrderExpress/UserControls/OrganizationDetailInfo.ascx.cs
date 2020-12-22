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
    /// Summary description for OrganizationForm.
    /// </summary>
    public partial class OrganizationDetailInfo : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_OrgID;
        protected System.Web.UI.WebControls.DataList dtLstProductDetail;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        public const string ORG_ID = "OrgID";
        protected System.Web.UI.WebControls.ImageButton imgBtnDelete;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected System.Web.UI.WebControls.HyperLink hypLnkCancel;
        private const string ORG_DATA = "OrgData";
        protected dataDef dtsOrganization;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
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
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);

        }
        #endregion

        protected void SetFormParameter() {
            if (Request[ORG_ID] != null) {
                c_OrgID = Convert.ToInt32(Request[ORG_ID].ToString());
            }
            else {
                c_OrgID = 0;
            }
            ViewState[ORG_ID] = c_OrgID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ORG_DATA] = dtsOrganization;
        }

        public override void BindForm() {
            OrganizationInfo_Org.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
                dtsOrganization = orgSys.SelectAllDetail(c_OrgID);

                //this.ViewState[ORG_DATA] = dtsOrganization;
                this.ViewState[ORG_ID] = c_OrgID;
            }
            else {
                c_OrgID = Convert.ToInt32(ViewState[ORG_ID]);
                //dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            }

            OrganizationInfo_Org.OrganizationID = c_OrgID;
            OrganizationInfo_Org.DataSource = dtsOrganization;
        }

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrganizationDetail, OrganizationDetail.ORG_ID, c_OrgID.ToString());
            Response.Redirect("OrganizationDetail.aspx?OrgID=" + c_OrgID.ToString(), true);
        }
    }
}