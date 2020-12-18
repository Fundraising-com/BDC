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
    public partial class AccountList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate an Account, use the Search and Filter features and click on Refresh button. <br><br> An Account can be the entire Organization, i.e. ABC Middle School [when there's only <b>one</b> QSP Program], or a group within an Organization, i.e. ABC Middle School 7th Grade, ABC Middle School Music Club, etc [when there are <b>multiple</b> QSP Programs].  Every Account is assigned a unique EDS account number based on the <b>type</b> of program, i.e. DM, Food, Gift, MMB, etc.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Directory:";
            this.Header.PageText = "Account List";
            this.LabelMessage = this.Master.LabelMessage1;

        }

        private void SetSearchCriteria() {
            this.ctrlAccountList.SearchCriteria.Items.Clear();
            this.ctrlAccountList.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlAccountList.SearchCriteria.Items.Add(new ListItem("City", "6"));
            this.ctrlAccountList.SearchCriteria.Items.Add(new ListItem("EDS Account #", "2"));
            this.ctrlAccountList.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "7"));
            this.ctrlAccountList.SearchCriteria.Items.Add(new ListItem("Zip Code", "5"));
            this.ctrlAccountList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlAccountList.SearchName.Text = "Account Name";
            this.ctrlAccountList.SearchName = abcNote;
        }
    }
    
}