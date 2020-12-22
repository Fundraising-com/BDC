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
using dataDef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for UserDetail.
    /// </summary>
    public partial class UserDetailInfo : BaseWebFormControl {
        private int c_UserID = 0;
        public const string USER_ID = "UserID";
        private const string USER_DATA = "UserData";
        protected dataDef dtblUser;

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
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_READ;
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick);

        }
        #endregion

        protected void SetFormParameter() {
            if (Request[USER_ID] != null) {
                c_UserID = Convert.ToInt32(Request[USER_ID].ToString());
            }
            else {
                c_UserID = 0;
            }
            ViewState[USER_ID] = c_UserID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override void BindForm() {
            UserInfo1.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                dtblUser = userSys.SelectOne(c_UserID);
            }
            else {
                c_UserID = Convert.ToInt32(ViewState[USER_ID]);
                //dtblUser = (dataDef)this.ViewState[USER_DATA];
            }

            //UserInfo1.uiUserID = c_UserID;
            UserInfo1.DataSource = dtblUser;
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.UserDetail, BaseUserDetail.USER_ID, c_UserID.ToString());
            string url = "~/UserDetail.aspx?" + BaseUserDetail.USER_ID + "=" + c_UserID.ToString();
            Response.Redirect(url);
        }
    }
}