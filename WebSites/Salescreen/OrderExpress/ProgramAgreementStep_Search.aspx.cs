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
    public partial class ProgramAgreementStep_Search : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate an Account, use the Search and Filter features and click on Refresh button.";
            this.Header.SectionText = "Add New Program Agreement :";
            this.Header.PageText = "STEP 1 - Search for Account";
            this.Header.IconImage = "~/images/icon_Account.gif";
            this.Header.IconImageVisiblilty = true;
        }
    } 
}