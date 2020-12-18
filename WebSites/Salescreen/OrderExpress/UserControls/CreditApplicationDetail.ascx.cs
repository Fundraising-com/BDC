//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_CreditApplicationDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'CreditApplicationDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.CreditApplicationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>Credit Application Detail form</summary>
    public partial class CreditApplicationDetail : BaseCreditApplicationDetail {
        private int c_AccID;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected dataDef dtsCreditApplication;

        private const string CREDIT_APP_DATA = "CreditAppData";

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                LoadData();
                if (!IsPostBack) {
                    this.Page.ValSummary.Visible = false;
                    this.Page.LabelDirectionTitle.Text = "Credit Terms";
                    BindForm();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #region auto-generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);

        }
        #endregion auto-generated code

        private void SetFormParameter() {
            if (Request[ACC_ID] != null) {
                c_AccID = Convert.ToInt32(Request[ACC_ID].ToString());
            }
            else {
                c_AccID = 0;
            }
            ViewState[ACC_ID] = c_AccID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[CREDIT_APP_DATA] = dtsCreditApplication;
            //Set the close button
            if (!IsPostBack) {
                if (dtsCreditApplication.CreditApplication.Rows.Count > 0) {
                    if (dtsCreditApplication.CreditApplication.Rows[0].RowState != DataRowState.Added) {
                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CreditApplicationDetailInfo, BaseCreditApplicationDetail.ACC_ID, c_AccID.ToString());
                        string url = "~/CreditApplicationDetailInfo.aspx?" + BaseCreditApplicationDetail.ACC_ID + "=" + c_AccID.ToString();
                        hypLnkCancel.NavigateUrl = url;
                    }
                }
            }
        }

        public override void BindForm() {
            CreditAppForm.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.CreditApplicationSystem credAppSys = new QSPForm.Business.CreditApplicationSystem();
                dtsCreditApplication = credAppSys.SelectAllDetailByAccountID(c_AccID);
                if (dtsCreditApplication.CreditApplication.Rows.Count == 0) {
                    dtsCreditApplication = credAppSys.InitializeCreditApplication(c_AccID, this.Page.UserID);
                }
                this.ViewState[ACC_ID] = c_AccID;
                this.ViewState[CREDIT_APP_DATA] = dtsCreditApplication;
            }
            else {
                c_AccID = Convert.ToInt32(this.ViewState[ACC_ID]);
                dtsCreditApplication = (dataDef)this.ViewState[CREDIT_APP_DATA];
            }
            CreditAppForm.AccountID = c_AccID;
            CreditAppForm.DataSource = dtsCreditApplication;
        }

        //
        //		//this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
        //		private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        //		{
        //			//For this opearion we just need to delete a row in the User Table
        //		}

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                bool blnValid = true;
                blnValid = CreditAppForm.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = CreditAppForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                QSPForm.Business.CreditApplicationSystem credAppSys = new QSPForm.Business.CreditApplicationSystem();

                if (dtsCreditApplication.CreditApplication.Rows[0].RowState == DataRowState.Added) {
                    blnValid = credAppSys.InsertAllDetail(dtsCreditApplication, this.Page.UserID);
                }
                else {
                    blnValid = credAppSys.UpdateAllDetail(dtsCreditApplication, this.Page.UserID);
                }

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CreditApplicationDetailInfo, BaseCreditApplicationDetail.ACC_ID, c_AccID.ToString());
                string url = "~/CreditApplicationDetailInfo.aspx?" + BaseCreditApplicationDetail.ACC_ID + "=" + c_AccID.ToString();

                Response.Redirect(url);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}