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
    public partial class WarehouseDetailInfo : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "The following product inventory is based on `real-time' data. However, if other orders are released to this warehouse before this order is processed, this inventory will be reduced.  Therefore, it cannot be 100% accurate.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Warehouse:";
            this.Header.PageText = "Warehouse Detail";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}