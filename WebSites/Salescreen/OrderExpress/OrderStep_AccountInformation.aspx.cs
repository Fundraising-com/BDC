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
    public partial class OrderStep_AccountInformation : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "<p>Before placing an order, review Account and Order Information below.  In Add New Order, modifications to Account Information can <u>only</u> be made to the `Ship To' and will <u>only</u> impact <u>this</u> order.  To permanently update the `Bill To' and/or `Ship To' in the system for this Account, please go to Account List, under Directory [Menu Bar] and modify the data accordingly.</p>";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Add New Order:";
            this.Header.PageText = "STEP 3 - Account Information";
        }
    } 
}