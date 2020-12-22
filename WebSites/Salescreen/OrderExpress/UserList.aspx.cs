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
    public partial class UserList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a User, use the Search and Filter Features and click on Refresh button.  To edit and/or delete a User, click on User Name.  To Add a new User, click on Add button.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Admin :";
            this.Header.PageText = "User List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlUserList.SearchCriteria.Items.Clear();
            this.ctrlUserList.SearchCriteria.Items.Add(new ListItem("Phone Number", "2"));
            this.ctrlUserList.SearchCriteria.Items.Add(new ListItem("User Name", "1", true));
            this.ctrlUserList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlUserList.SearchName.Text = "User Name";
            this.ctrlUserList.SearchName = abcNote;
        }
    } 
}