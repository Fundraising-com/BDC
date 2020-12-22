//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_LogoDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'LogoDetail.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
using dataDef = QSPForm.Common.DataDef.LogoData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class LogoDetail : BaseLogoDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;

        protected System.Web.UI.WebControls.Image imgTitle;

        private const string LOGO_DATA = "Logo_data";
        //private int _LogoID = 0;
        protected dataDef dtsLogo;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
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
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
        }
        #endregion

        //		public int LogoID
        //		{
        override public int LogoID {
            get {
                try {
                    if (this.ViewState[LOGO_ID] == null) {
                        this.ViewState[LOGO_ID] = Convert.ToInt32(this.Request[LOGO_ID].ToString());
                    }
                    return Convert.ToInt32(this.ViewState[LOGO_ID].ToString());
                }
                catch {
                    return 0;
                }
            }
            set {
                ViewState[LOGO_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[LOGO_DATA] = dtsLogo;
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        protected void SetFormParameter() {
            /*
            if (Request[LOGO_ID] != null)
            {
                LogoID = Convert.ToInt32(Request[LOGO_ID].ToString());
            }
            else
            {
                LogoID = 0;
            }
            ViewState[LOGO_ID] = LogoID;	
            */
        }

        public override void BindForm() {

            ctrlLogoHeaderForm.DataBind();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.LogoSystem prdSys = new QSPForm.Business.LogoSystem();
                //ToDo insert row when c_User_ID=0
                if (this.LogoID == 0) {
                    dtsLogo = prdSys.InitializeLogo(this.Page.UserID);
                }
                else {
                    dtsLogo = prdSys.SelectAllDetail(this.LogoID);
                }

                //this.ViewState[LOGO_ID] = LogoID;
                this.ViewState[LOGO_DATA] = dtsLogo;
            }
            else {
                //LogoID = Convert.ToInt32(this.ViewState[LOGO_ID]);
                dtsLogo = (dataDef)this.ViewState[LOGO_DATA];
            }

            ctrlLogoHeaderForm.LogoID = LogoID;
            ctrlLogoHeaderForm.DataSource = dtsLogo;
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                Boolean blnValid = true;
                blnValid = ctrlLogoHeaderForm.IsValid();
                if (!blnValid) {
                    return;
                }

                blnValid = ctrlLogoHeaderForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.LogoSystem prmSys = new QSPForm.Business.LogoSystem();
                blnValid = prmSys.UpdateAllDetail(dtsLogo);

                if ((blnValid) && (LogoID == 0)) {
                    ctrlLogoHeaderForm.CreateImageAndPreview();
                    LogoID = Convert.ToInt32(this.dtsLogo.Logo.Rows[0][LogoTable.FLD_PKID].ToString());
                }

                if (blnValid) {
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.LogoDetailInfo, LogoDetailInfo.LOGO_ID, LogoID.ToString());
                    string url = "~/LogoDetailInfo.aspx?" + LogoDetailInfo.LOGO_ID + "=" + LogoID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Error while saving Logo : " + ex.Message);
            }
        }
    }
}