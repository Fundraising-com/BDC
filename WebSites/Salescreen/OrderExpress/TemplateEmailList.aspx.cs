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
    public partial class TemplateEmailList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "The Template Email is used to build and compose email for specific needs.  You can personnalize by example the email send when a new order have been made for a specific Order form like \"WFC Warehouse Stock\".";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Admin:";
            this.Header.PageText = "Template Email List";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}