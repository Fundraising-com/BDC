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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for QSPForm.WebTemp.
    /// </summary>
    public partial class ComingSoon : BasePage {
        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
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
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);

        }
        #endregion


        protected override void InitControl() {
            this.AppItem = QSPForm.Business.AppItem.ComingSoon;
            base.LabelInstruction = lblInstruction;
            base.LabelMessage = lblMessage;
            base.InitControl();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect("Default.aspx");
        }
    }
}