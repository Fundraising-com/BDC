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
    public partial class FormList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "Click on the form image to access and/or edit General Information, Business Rules, Exception and Task Templates.  Click on Add button to set-up a new form.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Admin :";
            this.Header.PageText = "Form List";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}