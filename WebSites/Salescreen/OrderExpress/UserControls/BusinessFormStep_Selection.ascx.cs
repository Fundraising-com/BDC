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
using dataDef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for BusinessFormStep_Selection.
    /// </summary>
    public partial class BusinessFormStep_Selection : BaseWebFormControl {

        protected void Page_Load(object sender, System.EventArgs e) {
        }
        
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitControl();
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAccount.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAccount_Click);
            this.imgBtnOrder.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnOrder_Click);
            this.imgBtnCreditApplication.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnCreditApplication_Click);

        }
        #endregion

        private void InitControl() {
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        private void GoToNextStep(int BaseFormID) {
            string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.BusinessForm_Step2);
            //  string url = "~/BusinessForm_Step.aspx??NoMenu=111";
            if (BaseFormID > 0)
                Response.Redirect(url + "&BaseFormID=" + BaseFormID);
            else
                Response.Redirect(url);
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            GoToNextStep(0);
        }

        private void imgBtnAccount_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPForm.Business.FormSystem fmrSys = new QSPForm.Business.FormSystem();
            GoToNextStep(fmrSys.GetCurrentBaseFormID(QSPForm.Common.EntityType.TYPE_ACCOUNT));
        }

        private void imgBtnOrder_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPForm.Business.FormSystem fmrSys = new QSPForm.Business.FormSystem();
            GoToNextStep(fmrSys.GetCurrentBaseFormID(QSPForm.Common.EntityType.TYPE_ORDER_BILLING));
        }

        private void imgBtnCreditApplication_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPForm.Business.FormSystem fmrSys = new QSPForm.Business.FormSystem();
            GoToNextStep(fmrSys.GetCurrentBaseFormID(QSPForm.Common.EntityType.TYPE_CREDIT_APPLICATION));
        }
    }
}