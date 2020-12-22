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
    public partial class OrganizationDetailInfo : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Please verify the Organization Information below and click on Edit button to modify data.";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.SectionText = "Organization:";
            this.Header.PageText = "Organization Detail";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}