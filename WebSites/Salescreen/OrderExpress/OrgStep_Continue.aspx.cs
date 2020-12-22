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
    public partial class OrgStep_Continue : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "All information have been saved.  You may continue to Add Account related to this Organization or finish the process and return later";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.PageText = "Organization Easy Step -- STEP 7  (Continue)";
        }
    } 
}