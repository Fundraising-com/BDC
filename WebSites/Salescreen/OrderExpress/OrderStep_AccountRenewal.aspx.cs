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
    public partial class OrderStep_AccountRenewal : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            this.Master.ValSummaryVisibility = false;
        }

        private void SetHeader() {
            this.Header.InstructionText = "This account have to be renewed before. Please Review the account information to be able to proceed new Order";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Add New Order:";
            this.Header.PageText = "STEP 1.1 - Account Renewal";
        }
    } 
}