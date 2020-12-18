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
    public partial class OrderStep_Personalization : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To personalize this order, <u>select</u> the first product in the list below and <u>complete</u> ALL Label Components for selected product.  Repeat this process until all products are personalized.  Use the 'Quick Fill' menu option to copy a completed label from one product to another.  Use the 'Clear All Components' menu option to personalize a new label that should be different from another label.<br><br><b>Note:  Every label must be previewed before it can be saved ; otherwise, the order will remain in 'Incomplete PE' status until this is done.</b><br><br>";
            this.Header.SectionText = "Add New Order";
            this.Header.PageText = "STEP 9 - Personalization";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}