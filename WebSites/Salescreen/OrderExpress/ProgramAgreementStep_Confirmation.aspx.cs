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
    public partial class ProgramAgreementStep_Confirmation : BaseWebForm {
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To verify and/or edit Program Agreement Information, click on Program Agreement List button below and select the program agreement in the list.";
            this.Header.SectionText = "Add New Program Agreement :";
            this.Header.PageText = "STEP 5 - PA Confirmation";
            this.Header.IconImage = "~/images/icon_Account.gif";
            this.Header.IconImageVisiblilty = true;
        }
    } 
}