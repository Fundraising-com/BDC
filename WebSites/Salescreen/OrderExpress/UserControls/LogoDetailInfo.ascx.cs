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
using dataDef = QSPForm.Common.DataDef.LogoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for UserDetail.
    /// </summary>
    public partial class LogoDetailInfo : BaseWebFormControl {
        protected dataDef dtblLogo;
        public const string LOGO_ID = "logo_id";

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack)
                DataBind();
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

        #region Event

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.LogoDetail, BaseLogoDetail.LOGO_ID, LogoID.ToString());
            string url = "~/LogoDetail.aspx?" + BaseLogoDetail.LOGO_ID + "=" + LogoID.ToString();
            Response.Redirect(url);
        }

        #endregion Event

        #region Property

        private string LogoID {
            get {
                if (Request[LOGO_ID] != null) {
                    return Request[LOGO_ID].ToString();
                }
                else {
                    return "0";
                }
            }
        }

        #endregion Property

        public override void DataBind() {
            try {
                LoadData();
                SetValue();
                this.lblError.Text = "";
                if ((this.Page.Role < QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) &&
                    Convert.ToBoolean(this.dtblLogo.Rows[0][QSPForm.Common.DataDef.LogoTable.FLD_NATIONAL].ToString()))
                    this.imgBtnEdit.Visible = false;
            }
            catch (Exception ex) {
                this.lblError.Text = ex.Message;
            }
        }

        protected override void LoadData() {
            QSPForm.Business.LogoSystem promoSys = new QSPForm.Business.LogoSystem();
            this.dtblLogo = promoSys.SelectOne(Convert.ToInt32(this.LogoID));
        }

        private void SetValue() {
            this.ctrlLogoInfo.DataSource = this.dtblLogo;
            this.ctrlLogoInfo.DataBind();
        }
    }
}