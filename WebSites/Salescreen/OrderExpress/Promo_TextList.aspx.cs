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
    public partial class Promo_TextList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }
        private void SetHeader() {
            this.Header.InstructionText = "To Locate a promotion text, you can enter a criteria and/or use filter.";
            this.Header.SectionText = "Promotion";
            this.Header.PageText = "Promotion Text List";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlPromoTextList.SearchCriteria.Items.Clear();
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("Description", "1"));
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("FSM ID", "2"));
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("FSM Name", "3"));
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("Promotion Text Name", "5", true));
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("Promotion Text Code", "4"));
            this.ctrlPromoTextList.SearchCriteria.Items.Add(new ListItem("Vendor Name", "6"));
        }
    } 
}