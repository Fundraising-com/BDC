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
    public partial class AccountStep_Search : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To add a New Account, an Organization Must Exist.  Click on QSP Organization Directory or MDR Directory and Refresh button.<br>  If NO Organization Found, Click on Create NEW Organization.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Add New Account:";
            this.Header.PageText = "STEP 1 - Search for an Organization";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}