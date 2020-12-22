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
    public partial class OrderDetail : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Edit Sponsor Information, Postal Address, Phone Numbers Email Addresses and/or Products.  'Bill To' Information can easily be copied over to 'Ship To' Information.";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Order:";
            this.Header.PageText = "Order Detail";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}