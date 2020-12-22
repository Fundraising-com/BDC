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
    public partial class BusinessNotificationDetail : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.SectionText = "Note:";
            this.Header.PageText = "Note Detail";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Header.IconImageVisiblilty = false;
        }
    } 
}