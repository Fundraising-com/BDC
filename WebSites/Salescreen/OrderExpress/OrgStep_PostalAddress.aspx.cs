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
    public partial class OrgStep_PostalAddress : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "If you have pre-selected an MDR School, these informations have been taken from the MDR School information.  Validate or enter the information of the postal Address of the Organization.";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.PageText = "Organization Easy Step -- STEP 3  (Postal Address)";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}