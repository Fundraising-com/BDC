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
    public partial class Promo_LogoList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a Logo, user the Search and Filter features and click on Refresh button Note : These logos represent graphic and/or digital images.";
            this.Header.SectionText = "Promotion";
            this.Header.PageText = "Promotion Logo List ";
            this.Header.IconImageVisiblilty = false;
        }

        private void SetSearchCriteria() {
            this.ctrlPromoLogoList.SearchCriteria.Items.Clear();
            this.ctrlPromoLogoList.SearchCriteria.Items.Add(new ListItem("Description", "4"));
            this.ctrlPromoLogoList.SearchCriteria.Items.Add(new ListItem("FSM ID", "1"));
            this.ctrlPromoLogoList.SearchCriteria.Items.Add(new ListItem("FSM Name", "2"));
            this.ctrlPromoLogoList.SearchCriteria.Items.Add(new ListItem("Logo Name", "3", true));
        }
    } 
}