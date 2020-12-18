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
    public partial class promo_TextSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.SectionText = "Personalization";
            this.Header.PageText = "";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Clear();
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("Description", "3"));
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("FSM ID", "4"));
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("FSM Name", "5"));
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("Promotion Text Code", "2"));
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("Promotion Text Name", "1", true));
            this.ctrlpromo_TextSelector.SearchCriteria.Items.Add(new ListItem("Vendor Name", "6"));
        }
    } 
}