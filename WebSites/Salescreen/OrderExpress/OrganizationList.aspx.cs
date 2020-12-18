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
    public partial class OrganizationList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate an Organization, use the Search and Filter features and click on Refresh button.  <br><br>  An Organization represents the main entity or fund-raising organization.  It can be a school, sorority, etc.  While an Organization may have one or  more Accounts, there can only be one Organization for an account[s].  Every Organization is assigned a unique QSP ID Number that is tied to the EDS Account Numbers associated with the Organization.";
            this.Header.IconImage = "~/images/icon/icon_organization.gif";
            this.Header.SectionText = "Directory:";
            this.Header.PageText = "Organization List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlOrganizationList.SearchCriteria.Items.Clear();
            this.ctrlOrganizationList.SearchCriteria.Items.Add(new ListItem("City", "5"));
            this.ctrlOrganizationList.SearchCriteria.Items.Add(new ListItem("Organization Name", "1", true));
            this.ctrlOrganizationList.SearchCriteria.Items.Add(new ListItem("QSP Organization ID", "6"));
            this.ctrlOrganizationList.SearchCriteria.Items.Add(new ListItem("Zip Code", "4"));
            this.ctrlOrganizationList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlOrganizationList.SearchName.Text = "Organization Name";
            this.ctrlOrganizationList.SearchName = abcNote;
        }
    } 
}