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
    public partial class LogoList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            // TODO: find the right condition
            this.Header.InstructionText = "To locate a Logo, use the Search and Filter features and click on Refresh button.<br><br>NOTE: ONLY those logos [limited to 25] with ADD TO FAVORITES button are displayed in the PE application.<br>To modify Favorite List, click on REMOVE FROM FAVORITES button and it will automatically change to ADD TO FAVORITES.<br>To ADD a new logo to Logo List, click on ADD button below.";
            this.Header.SectionText = "Logo";
            this.Header.PageText = "Logo List";
            this.Header.IconImage = "~/images/Spacer.gif";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;

            this.Header.InstructionText = "To locate a Logo, use the Search and Filter features and click on Refresh button.<br><br>NOTE: Use Field Sales Manager filter below to key an order for an FSM. Click on Select button and follow the instructions. After an FSM is selected, click on Refresh button. To modify logos in 'Favorites' list [displayed in PE application] for this FSM, use 'QSP Images' or 'Favorites' filters and click on Refresh button. Then use the 'Add To Favorites' or 'Remove From Favorites' buttons and click on Refresh button. Now use the 'Favorites' filter and the new logo[s] will be displayed in this list. To select a new FSM, click on Reset button.<br><br>To ADD a new logo to Logo List, click on ADD button below.";
            this.Header.SectionText = "Logo";
            this.Header.PageText = "Logo List";
        }

        private void SetSearchCriteria() {
            // TODO: find the right condition
            this.ctrlLogoList.SearchCriteria.Items.Clear();


            this.ctrlLogoList.SearchCriteria.Items.Add(new ListItem("Description", "2"));
            this.ctrlLogoList.SearchCriteria.Items.Add(new ListItem("FSM ID", "3"));
            this.ctrlLogoList.SearchCriteria.Items.Add(new ListItem("FSM Name", "4"));
            this.ctrlLogoList.SearchCriteria.Items.Add(new ListItem("Logo ID", "5"));
            this.ctrlLogoList.SearchCriteria.Items.Add(new ListItem("Logo Name", "1"));

            this.ctrlLogoList.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}