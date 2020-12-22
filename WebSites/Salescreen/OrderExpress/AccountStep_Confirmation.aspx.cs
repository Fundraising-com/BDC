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
    public partial class AccountStep_Confirmation : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To verify and/or edit Account Information, click on Account List button below and select the account in the list.  If you're ready to place an Order, click on Add Order button below.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Add New Account:";
            this.Header.PageText = "STEP 5 - Account Confirmation";
        }
    } 
}