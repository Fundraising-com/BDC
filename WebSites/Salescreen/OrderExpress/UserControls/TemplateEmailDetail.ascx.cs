//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_TemplateEmailDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'TemplateEmailDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.TemplateEmailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class TemplateEmailDetail : BaseTemplateEmailDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int iTemplateEmailID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        private const string TEMPLATE_EMAIL_DATA = "TemplateEmailData";
        private const string ERR01 = "This template can not be erased";
        private const string ERR02 = "Multiple row are selected to be deleted";
        protected dataDef dtblTemplateEmail;

        protected void Page_Load(object sender, System.EventArgs e) {
            //this.QSPToolBar.ShowDeleteButton = false;
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                }
                if (this.AppTemplateEmailID == 0) {
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
            base.OnInit(e);
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.QSPToolBar.SaveClick += new EventHandler(QSPToolBar_SaveClick);
            this.QSPToolBar.DeleteClick += new EventHandler(QSPToolBar_DeleteClick);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        //		public int AppTemplateEmailID
        //		{
        override public int AppTemplateEmailID {
            get {
                return iTemplateEmailID;
            }
            set {
                iTemplateEmailID = value;
                ViewState[TEMPLATE_EMAIL_ID] = iTemplateEmailID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.ViewState[TEMPLATE_EMAIL_DATA] = dtblTemplateEmail;
        }

        private void QSPToolBar_DeleteClick(object sender, EventArgs e) {
            Delete();
        }

        private void QSPToolBar_SaveClick(object sender, EventArgs e) {
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

                QSPForm.Business.TemplateEmailSystem teSys = new QSPForm.Business.TemplateEmailSystem();
                if (iTemplateEmailID == 0)
                    blnValid = teSys.Insert(dtblTemplateEmail);
                else
                    blnValid = teSys.Update(dtblTemplateEmail);
                if (blnValid) {
                    iTemplateEmailID = Convert.ToInt32(dtblTemplateEmail.Rows[0][dataDef.FLD_PKID]);
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.TemplateEmailDetailInfo, TemplateEmailDetailInfo.TEMPLATE_EMAIL_ID, iTemplateEmailID.ToString());
                    string url = "~/TemplateEmailDetailInfo.aspx?" + TemplateEmailDetailInfo.TEMPLATE_EMAIL_ID + "=" + iTemplateEmailID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void SetFormParameter() {
            if (Request[TEMPLATE_EMAIL_ID] != null) {
                iTemplateEmailID = Convert.ToInt32(Request[TEMPLATE_EMAIL_ID].ToString());
            }
            else {
                iTemplateEmailID = 0;
            }
            ViewState[TEMPLATE_EMAIL_ID] = iTemplateEmailID;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.TemplateEmailSystem teSys = new QSPForm.Business.TemplateEmailSystem();
                //ToDo insert row when c_TEMPLATE_EMAIL_ID=0
                //ToDo insert row when c_User_ID=0
                if (iTemplateEmailID == 0) {
                    dtblTemplateEmail = new TemplateEmailTable();
                    DataRow newRow = dtblTemplateEmail.NewRow();
                    dtblTemplateEmail.Rows.Add(newRow);
                }
                else {
                    dtblTemplateEmail = teSys.SelectOne(iTemplateEmailID);
                }

                this.ViewState[TEMPLATE_EMAIL_ID] = iTemplateEmailID;
                this.ViewState[TEMPLATE_EMAIL_DATA] = dtblTemplateEmail;
            }
            else {
                iTemplateEmailID = Convert.ToInt32(this.ViewState[TEMPLATE_EMAIL_ID]);
                dtblTemplateEmail = (dataDef)this.ViewState[TEMPLATE_EMAIL_DATA];
            }

            HeaderDetail.TemplateEmailID = iTemplateEmailID;
            HeaderDetail.DataSource = dtblTemplateEmail;
        }

        private new void Delete() {
            try {
                if (AppTemplateEmailID != 0) {
                    QSPForm.Business.TemplateEmailSystem teSys = new QSPForm.Business.TemplateEmailSystem();
                    DataRow tmpEmailRow = dtblTemplateEmail.Rows[0];
                    if (teSys.IsValid_Unicity(tmpEmailRow)) {
                        tmpEmailRow[dataDef.FLD_UPDATE_USER_ID] = this.Page.UserID;
                        tmpEmailRow.Delete();
                        teSys.Delete(dtblTemplateEmail);
                        this.Page.RegisterClientScriptBlock("", "<script>window.opener.RefreshPage();window.close();</script>");
                    }
                }
                else {
                    this.Page.SetPageError(new Exception(ERR01));
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(new Exception(ERR01 + "(" + ex.Message + ")"));
            }
        }
    }
}