//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_UserDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'UserDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class UserDetail : BaseUserDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_UserID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        protected System.Web.UI.WebControls.ImageButton imgBtnDelete;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected System.Web.UI.WebControls.HyperLink hypLnkCancel;

        private const string USER_DATA = "UserData";
        protected dataDef dtblUser;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                }
                if (this.AppUserID == 0) {
                    this.QSPToolBar.DeleteButton.Visible = false;
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
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.QSPToolBar.SaveClick += new EventHandler(this.imgBtnSave_Click);
            this.QSPToolBar.DeleteClick += new EventHandler(this.imgBtnDelete_Click);
            this.QSPToolBar.DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this user ?');");
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        #region Property
        //		public int AppUserID
        //		{
        override public int AppUserID {
            get {
                return c_UserID;
            }
            set {
                c_UserID = value;
                ViewState[USER_ID] = c_UserID;
            }
        }
        #endregion Property

        #region Event
        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[USER_DATA] = dtblUser;
        }

        private void imgBtnDelete_Click(object sender, EventArgs e) {
            DeleteUser();
        }

        private void imgBtnSave_Click(object sender, EventArgs e) {
            SaveUser();
        }
        #endregion Event

        protected void SetFormParameter() {
            if (Request[USER_ID] != null) {
                c_UserID = Convert.ToInt32(Request[USER_ID].ToString());
            }
            else {
                c_UserID = 0;
            }
            ViewState[USER_ID] = c_UserID;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                //ToDo insert row when c_User_ID=0
                if (c_UserID == 0) {
                    dtblUser = new UserTable();
                    DataRow newRow = dtblUser.NewRow();
                    dtblUser.Rows.Add(newRow);
                }
                else {
                    dtblUser = userSys.SelectOne(c_UserID);
                }

                this.ViewState[USER_ID] = c_UserID;
                this.ViewState[USER_DATA] = dtblUser;
            }
            else {
                c_UserID = Convert.ToInt32(this.ViewState[USER_ID]);
                dtblUser = (dataDef)this.ViewState[USER_DATA];
            }

            HeaderDetail.iUserID = c_UserID;
            HeaderDetail.DataSource = dtblUser;
        }

        private void SaveUser() {
            try {
                Boolean blnValid = true;

                blnValid = HeaderDetail.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = HeaderDetail.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                if (c_UserID == 0) {
                    blnValid = userSys.Insert(dtblUser);
                }
                else
                    blnValid = userSys.Update(dtblUser);
                if (blnValid) {
                    c_UserID = Convert.ToInt32(dtblUser.Rows[0][dataDef.FLD_PKID]);
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.UserDetailInfo, UserDetailInfo.USER_ID, c_UserID.ToString());
                    string url = "~/UserDetailInfo.aspx?" + UserDetailInfo.USER_ID + "=" + c_UserID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void DeleteUser() {
            try {
                QSPForm.Business.UserSystem userSys = new QSPForm.Business.UserSystem();
                UserTable dtbl = userSys.SelectOne(Convert.ToInt32(this.ViewState[USER_ID].ToString()));
                if (dtbl.Rows.Count > 0) {
                    dtbl.Rows[0].Delete();
                    userSys.Delete(dtbl);
                    this.Page.RegisterClientScriptBlock("close", "<script>alert('this user has been deleted');window.opener.RefreshPage();window.close();</script>");
                }
                else {
                    this.Page.SetPageMessage("This user can not be deleted");
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }
    }
}