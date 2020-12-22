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
    public partial class UserSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To select a user from the list, you just need to click on the select button.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "User:";
            this.Header.PageText = "User Selector";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlUserSelector.SearchCriteria.Items.Clear();
            this.ctrlUserSelector.SearchCriteria.Items.Add(new ListItem("User Name", "1", true));
        }
    } 
}