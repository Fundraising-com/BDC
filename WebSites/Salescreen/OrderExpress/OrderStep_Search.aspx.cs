using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    public partial class OrderStep_Search : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Add New Order:";
            this.Header.PageText = "STEP 1 - Account  Search";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Header.InstructionText = "To place an order, select the Account; then use the Search and Filter features and click on Refresh button.";
        }
    } 
}