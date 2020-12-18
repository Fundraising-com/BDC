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
    public partial class PromoList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "This is list to consult promotions.";
            this.Header.SectionText = "Promotion";
            this.Header.PageText = "Promo List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlPromoList.SearchCriteria.Items.Clear();
            this.ctrlPromoList.SearchCriteria.Items.Add(new ListItem("Description", "2"));
            this.ctrlPromoList.SearchCriteria.Items.Add(new ListItem("FSM ID", "3"));
            this.ctrlPromoList.SearchCriteria.Items.Add(new ListItem("FSM Name", "4"));
            this.ctrlPromoList.SearchCriteria.Items.Add(new ListItem("Promo Name", "1", true));
        }
    } 
}