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
    public partial class CampaignList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.IconImage = "~/images/title_campaign_list.gif";
            this.Header.PageText = "Campaign List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlCampaignList.SearchCriteria.Items.Clear();
            this.ctrlCampaignList.SearchCriteria.Items.Add(new ListItem("Campaign Name", "1", true));
            this.ctrlCampaignList.SearchCriteria.Items.Add(new ListItem("EDS Account #", "2"));
            this.ctrlCampaignList.SearchCriteria.Items.Add(new ListItem("FSM ID", "3"));
            this.ctrlCampaignList.SearchCriteria.Items.Add(new ListItem("FSM Name", "4"));
            this.ctrlCampaignList.SearchCriteria.Items.Add(new ListItem("Zip Code", "5"));

        }
    } 
}