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
    public partial class AccountDetailInfo : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Account:";
            this.Header.PageText = "Account Detail";
            this.Header.InstructionText = "Please verify the account information below and click on Edit button to access edit fields and modify data. Here’s a tip! By clicking on the forward arrow button below, you can access the Organization Detail for this account that includes an Account List. By clicking on an account within the list, you can access an Order List and see the status of every order for that account.";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Master.ValSummaryVisibility = false;
        }
    } 
}