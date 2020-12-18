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
    public partial class ProgramAgreementStatusChangeList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "This page contains historical information for this program agreement.";
            this.Header.SectionText = "Program Agreement:";
            this.Header.PageText = "History";
            this.Header.IconImage = "~/images/spacer.gif";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}