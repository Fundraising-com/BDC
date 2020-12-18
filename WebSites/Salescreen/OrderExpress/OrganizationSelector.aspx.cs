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
    public partial class OrganizationSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.PageText = "Organization Selector";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlOrganizationSelector.SearchCriteria.Items.Clear();
            this.ctrlOrganizationSelector.SearchCriteria.Items.Add(new ListItem("City", "5"));
            this.ctrlOrganizationSelector.SearchCriteria.Items.Add(new ListItem("Organization Name", "1", true));
            this.ctrlOrganizationSelector.SearchCriteria.Items.Add(new ListItem("QSP Organization ID", "6"));
            this.ctrlOrganizationSelector.SearchCriteria.Items.Add(new ListItem("Zip Code", "4"));
            this.ctrlOrganizationSelector.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}