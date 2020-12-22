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
    public partial class AccountTransfer : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "This is FM Transfer Account. Select FM and click on transfer account to transfer selected accounts.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Directory:";
            this.Header.PageText = "FM Transfer Account";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlAccountTransfer.SearchCriteria.Items.Clear();
            this.ctrlAccountTransfer.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlAccountTransfer.SearchCriteria.Items.Add(new ListItem("City", "6"));
            this.ctrlAccountTransfer.SearchCriteria.Items.Add(new ListItem("EDS Account #", "2"));
            this.ctrlAccountTransfer.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "7"));
            this.ctrlAccountTransfer.SearchCriteria.Items.Add(new ListItem("Zip Code", "5"));
            this.ctrlAccountTransfer.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlAccountTransfer.SearchName.Text = "Account Name";
            this.ctrlAccountTransfer.SearchName = abcNote;
        }
    } 
}