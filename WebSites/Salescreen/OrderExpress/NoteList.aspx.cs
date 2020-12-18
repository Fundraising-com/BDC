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
    public partial class NoteList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            CtrlBusinessNotificationList.AppItem = string.Empty;
            if (!IsPostBack) {
                SetSearchCriteria();
                SetHeader();
            }
        }

        private void SetSearchCriteria() {
            this.CtrlBusinessNotificationList.SearchCriteria.Items.Clear();
            this.CtrlBusinessNotificationList.SearchCriteria.Items.Add(new ListItem("Note Subject", "1", true));
            this.CtrlBusinessNotificationList.SearchCriteria.Items.Add(new ListItem("User Name", "2"));
            this.CtrlBusinessNotificationList.SearchCriteria.Items.FindByValue("1").Selected = true;

            this.CtrlBusinessNotificationList.SearchName.Text = "Note Subject";
            this.CtrlBusinessNotificationList.SearchName = abcNote;
        }

        private void SetHeader() {
            this.Header.InstructionText = "Click on Add button to enter New Note.  Use the Search features and click on Refresh button to filter data.  This list presents all Note assigned to all user.";
            this.Header.IconImage = "~/images/icon/icon_todo.gif";
            this.Header.SectionText = "Note:";
            this.Header.PageText = "Note List";
            this.LabelMessage = this.Master.LabelMessage1;
        }
    } 
}