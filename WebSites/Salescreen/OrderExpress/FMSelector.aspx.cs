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
    public partial class FMSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate an FSM, use the Search features and click on Refresh button.  Click on Select button next to the appropriate FSM.";
            this.Header.IconImage = "~/images/title_fm_selector.gif";
            this.Header.SectionText = "Field Sales Manager Selector";
            this.Header.PageText = "FSM Selector";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlFMSelector.SearchCriteria.Items.Clear();
            this.ctrlFMSelector.SearchCriteria.Items.Add(new ListItem("FSM ID", "1"));
            this.ctrlFMSelector.SearchCriteria.Items.Add(new ListItem("FSM Name", "2", true));
            this.ctrlFMSelector.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}