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
    public partial class BusinessFormStep_Selection : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To create a new business form, please select on what is form is based on:";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Add a New Form";
            this.Header.PageText = "STEP 1 - Select a Form Type";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}