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
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for FAQ_viewer.
    /// </summary>
    public partial class FAQ_viewer : BasePage {

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                int FaqID = Convert.ToInt32(Request["FAQ"]);
                AppItemData appItem = new AppItemData();
                ContentManagerSystem contSys = new ContentManagerSystem();
                appItem = contSys.SelectOneFAQ(FaqID);
                DataRow drw = appItem.Tables[AppItemFAQTable.TBL_FAQ_ITEMS].Rows[0];
                lblFAQ.Text = drw[AppItemFAQTable.FLD_FAQ].ToString();
                lblAnswer.Text = drw[AppItemFAQTable.FLD_ANSWER].ToString();
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

        }
        #endregion

        protected override void InitControl() {
            this.AppItem = 0;
            base.LabelMessage = lblMessage;
            base.InitControl();
        }
    }
}