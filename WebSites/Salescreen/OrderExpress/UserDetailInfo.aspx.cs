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
    public partial class UserDetailInfo : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "Please verify the user's  information below and click on Edit button to access edit fields and modify data.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "User:";
            this.Header.PageText = "User Detail";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}