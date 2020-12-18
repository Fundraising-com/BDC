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
    public partial class OrgStep_MDRSchool : BaseWebFormStep {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "You can choose an organization from the MDR School List by clicking on the Select Button.  The System will get the informations from the selected MDR School.  Otherwise, let the MDR School info empty, and you will be able to enter the information manually at the next Step.";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.PageText = "STEP 1 - Search for an MDR School";
        }
    } 
}