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
    ///<summary>UserDetail - form to edit a user</summary>
    public partial class CreditApplicationDetailInfo : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_AccID;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected dataDef dtsCreditApplication;
        public const string ACC_ID = "AccID";
        private const string CREDIT_APP_DATA = "CreditAppData";

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                LoadData();
                if (!IsPostBack) {
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
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);

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
        }

        public override void BindForm() {
            CreditAppInfo.BindForm();
        }

        private new void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.CreditApplicationSystem credAppSys = new QSPForm.Business.CreditApplicationSystem();
                dtsCreditApplication = credAppSys.SelectAllDetailByAccountID(c_AccID);
                if (dtsCreditApplication.CreditApplication.Rows.Count == 0) {
                    //Go direclty to edit mode to add a new credit app.					
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CreditApplicationDetail, BaseCreditApplicationDetail.ACC_ID, c_AccID.ToString());
                    string url = "~/CreditApplicationDetail.aspx?" + BaseCreditApplicationDetail.ACC_ID + "=" + c_AccID.ToString();
                    Response.Redirect(url);
                }
                else {
                    if (this.Page.Role < QSPForm.Business.AuthSystem.ROLE_SUPER_USER) {
                        DataRow crdAppRow = dtsCreditApplication.CreditApplication.Rows[0];
                        bool isApproved = Convert.ToBoolean(crdAppRow[CreditApplicationTable.FLD_APPROVED]);
                        imgBtnEdit.Visible = !isApproved;
                        if (isApproved) {
                            this.Page.SetPageMessage("This Credit Application is not editable");
                        }
                    }
                }
                this.ViewState[ACC_ID] = c_AccID;

                CreditAppInfo.AccountID = c_AccID;
                CreditAppInfo.DataSource = dtsCreditApplication;
                ///this.ViewState[CREDIT_APP_DATA] = dtsCreditApplication;
            }
            else {
                c_AccID = Convert.ToInt32(this.ViewState[ACC_ID]);
                //dtsCreditApplication = (dataDef)this.ViewState[CREDIT_APP_DATA];
            }
        }

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CreditApplicationDetail, BaseCreditApplicationDetail.ACC_ID, c_AccID.ToString());
            string url = "~/CreditApplicationDetail.aspx?" + BaseCreditApplicationDetail.ACC_ID + "=" + c_AccID.ToString();
            Response.Redirect(url);
        }
    }
}