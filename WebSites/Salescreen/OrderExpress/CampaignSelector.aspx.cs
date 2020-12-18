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
    public partial class CampaignSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.IconImage = "~/images/title_campaign_selector.gif";
            this.Header.PageText = "Campaign Selector";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlCampaignSelector.SearchCriteria.Items.Clear();
            this.ctrlCampaignSelector.SearchCriteria.Items.Add(new ListItem("Campaign ID", "1"));
            this.ctrlCampaignSelector.SearchCriteria.Items.Add(new ListItem("Campaign Name", "0", true));
            this.ctrlCampaignSelector.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}