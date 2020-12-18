//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_Promo_LogoDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'Promo_LogoDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.Promo_LogoData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class Promo_LogoDetail : BasePromo_LogoDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        private const string PROMO_LOGO_DATA = "Promo_Logo_data";
        //private int _LogoID = 0;
        protected dataDef dtsPromo_Logo;

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

        //		public int Promo_LogoID
        //		{
        override public int Promo_LogoID {
            get {
                try {
                    if (this.ViewState[PROMO_LOGO_ID] == null) {
                        this.ViewState[PROMO_LOGO_ID] = Convert.ToInt32(this.Request[PROMO_LOGO_ID].ToString());
                    }
                    return Convert.ToInt32(this.ViewState[PROMO_LOGO_ID].ToString());
                }
                catch {
                    return 0;
                }
            }
            set {
                ViewState[PROMO_LOGO_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[PROMO_LOGO_DATA] = dtsPromo_Logo;
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

            ctrlPromo_LogoHeaderForm.DataBind();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.Promo_LogoSystem prdSys = new QSPForm.Business.Promo_LogoSystem();
                //ToDo insert row when c_User_ID=0
                if (this.Promo_LogoID == 0) {
                    dtsPromo_Logo = prdSys.InitializePromo_Logo(this.Page.UserID);
                }
                else {
                    dtsPromo_Logo = prdSys.SelectAllDetail(this.Promo_LogoID);
                }

                //this.ViewState[LOGO_ID] = LogoID;
                this.ViewState[PROMO_LOGO_DATA] = dtsPromo_Logo;
            }
            else {
                //LogoID = Convert.ToInt32(this.ViewState[LOGO_ID]);
                dtsPromo_Logo = (dataDef)this.ViewState[PROMO_LOGO_DATA];
            }

            ctrlPromo_LogoHeaderForm.Promo_logoID = Promo_LogoID;
            ctrlPromo_LogoHeaderForm.DataSource = dtsPromo_Logo;
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                Boolean blnValid = true;
                blnValid = ctrlPromo_LogoHeaderForm.IsValid();
                if (!blnValid) {
                    return;
                }

                blnValid = ctrlPromo_LogoHeaderForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.Promo_LogoSystem prmSys = new QSPForm.Business.Promo_LogoSystem();
                blnValid = prmSys.UpdateAllDetail(dtsPromo_Logo);

                if ((blnValid) && (Promo_LogoID == 0)) {
                    ctrlPromo_LogoHeaderForm.CreateImageAndPreview();
                    Promo_LogoID = Convert.ToInt32(this.dtsPromo_Logo.Promo_logo.Rows[0][Promo_LogoTable.FLD_PKID].ToString());
                }

                if (blnValid) {
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Promo_LogoDetailInfo, Promo_LogoDetailInfo.PROMO_LOGO_ID, Promo_LogoID.ToString());
                    string url = "~/Promo_LogoDetailInfo.aspx?" + Promo_LogoDetailInfo.PROMO_LOGO_ID + "=" + Promo_LogoID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Error while saving Promo_Logo : " + ex.Message);
            }
        }
    }
}