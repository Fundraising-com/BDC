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
    public partial class OrderStep_Selection : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();

            // Initialize control
            ctrlOrderStep_Selection.UserId = this.UserID;
        }

        private void SetHeader() {
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Add New Order:";
            this.Header.PageText = "STEP 2 - Form Selection";
            this.Header.InstructionText = "To place an order for this Account, select an order form from the list below.";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}