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
    public partial class ProgramAgreementDetail : BaseWebForm {
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Edit Sponsor Information, Postal Address, Phone Numbers and/or Email Addresses.  'Bill To' Information can easily be copied over to 'Ship To' Information.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Program Agreement:";
            this.Header.PageText = "Program Agreement Detail";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Master.ValSummaryVisibility = false;
        }
    } 
}