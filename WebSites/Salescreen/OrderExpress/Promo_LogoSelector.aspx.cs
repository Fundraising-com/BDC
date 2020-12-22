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
    public partial class Promo_LogoSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.SectionText = "Personalization";
            this.Header.PageText = "";
            this.Header.IconImageVisiblilty = false;
        }

        private void SetSearchCriteria() {
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Clear();
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Add(new ListItem("Description", "2"));
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Add(new ListItem("FSM ID", "3"));
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Add(new ListItem("FSM Name", "4"));
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Add(new ListItem("Logo Name", "1", true));
            this.ctrlPromo_LogoSelector.SearchCriteria.Items.Add(new ListItem("Vendor Name", "5"));
        }
    } 
}