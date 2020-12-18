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
using dataDef = QSPForm.Common.DataDef.TemplateEmailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoDetailInfo: Read only ToDo item information</summary>
    public partial class TemplateEmailDetailInfo : BaseWebFormControl {
        protected System.Web.UI.WebControls.HyperLink hypLnkClose;
        protected dataDef dtblTemplateEmail;

        //private int c_UserID;
        //private int cTemplateEmailID = 0;
        public const string TEMPLATE_EMAIL_ID = "template_email_id";
        private const string TEMPLATE_EMAIL_DATA = "TemplateEmailData";

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_READ;
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick);

        }
        #endregion auto-generated code

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                if (!IsPostBack) {
                    if (Request[TEMPLATE_EMAIL_ID] != null) {
                        iTemplateEmailID = Convert.ToInt32(Request[TEMPLATE_EMAIL_ID].ToString());
                    }
                }
                LoadData();
                this.ctrlTemplateEmailInfo.DataSource = dtblTemplateEmail;
                this.ctrlTemplateEmailInfo.DataBind();

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.TemplateEmailDetail, BaseTemplateEmailDetail.TEMPLATE_EMAIL_ID, iTemplateEmailID.ToString());
            string url = "~/TemplateEmailDetail.aspx?" + BaseTemplateEmailDetail.TEMPLATE_EMAIL_ID + "=" + iTemplateEmailID.ToString();
            Response.Redirect(url);
        }

        public int iTemplateEmailID {
            get {
                int iTEMPLATE_EMAIL_ID = 0;
                if (ViewState[TEMPLATE_EMAIL_ID] != null) {
                    iTEMPLATE_EMAIL_ID = Convert.ToInt32(ViewState[TEMPLATE_EMAIL_ID]);
                }
                return iTEMPLATE_EMAIL_ID;
            }
            set {
                ViewState[TEMPLATE_EMAIL_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void LoadData() {
            QSPForm.Business.TemplateEmailSystem teSys = new QSPForm.Business.TemplateEmailSystem();
            dtblTemplateEmail = teSys.SelectOne(iTemplateEmailID);
        }
    }
}