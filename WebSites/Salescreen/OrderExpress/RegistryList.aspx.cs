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
    public partial class RegistryList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            SetHeader();
            SetSearchCriteria();
        }

        private void SetHeader() {
            this.Header.InstructionText = "To Locate a Registry line, you can enter a criteria and/or use the User Role filter.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Admin";
            this.Header.PageText = "Registry List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlRegistryList.SearchCriteria.Items.Clear();
            this.ctrlRegistryList.SearchCriteria.Items.Add(new ListItem("User Name", "1", true));
        }
    } 
}