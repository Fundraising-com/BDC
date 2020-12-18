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
    public partial class OrgStep_Confirmation : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Pleaser review the organization informations entered and press confirm once you are satisfied.";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.PageText = "Organization Easy Step -- STEP 6  (Confirmation)";
        }
    } 
}