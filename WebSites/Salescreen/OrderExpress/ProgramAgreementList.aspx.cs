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
    public partial class ProgramAgreementList : BaseWebForm {
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a Program Agreement, use the Search and Filter features and click on Refresh button.";
            this.Header.SectionText = "Directory:";
            this.Header.PageText = "Program Agreement List";
            this.Header.IconImage = "~/images/icon_Account.gif";
            this.Header.IconImageVisiblilty = true;
            this.LabelMessage = this.Master.LabelMessage1;
        }
        private void SetSearchCriteria() {
            this.ctrlProgramAgreementList.SearchCriteria.Items.Clear();
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("City", "6"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("EDS Account #", "2"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("EDS PA #", "4"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "7"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("QSP PA ID #", "3"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.Add(new ListItem("Zip Code", "5"));
            this.ctrlProgramAgreementList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlProgramAgreementList.SearchName.Text = "Account Name";
        }
    } 
}