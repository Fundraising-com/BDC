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
    public partial class BusinessFormStep_Confirmation : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "A unique Form ID has been assigned to this Form.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Add a New Form";
            this.Header.PageText = "STEP 6 - Confirmation";
        }
    } 
}