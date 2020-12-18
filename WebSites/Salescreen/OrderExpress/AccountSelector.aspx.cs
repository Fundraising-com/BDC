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
    public partial class AccountSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.PageText = "Account Selector";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlAccountSelector.SearchCriteria.Items.Clear();
            this.ctrlAccountSelector.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlAccountSelector.SearchCriteria.Items.Add(new ListItem("City", "6"));
            this.ctrlAccountSelector.SearchCriteria.Items.Add(new ListItem("EDS Account #", "2"));
            this.ctrlAccountSelector.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "7"));
            this.ctrlAccountSelector.SearchCriteria.Items.Add(new ListItem("Zip Code", "5"));
            this.ctrlAccountSelector.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}