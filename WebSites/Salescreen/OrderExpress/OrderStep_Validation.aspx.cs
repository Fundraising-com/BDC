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
    public partial class OrderStep_Validation : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Before completing the order, carefully review ALL the information, especially any notations in the Important Information section. Use Back button to review previous steps and to make corrections, if necessary.";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Add New Order:";
            this.Header.PageText = "STEP 6 - Order Validation";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}