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
    public partial class MyNoteList : BaseWebForm // System.Web.UI.Page
{
        private string appItem = String.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            AppItem = "mynotelist";
            CtrlBusinessNotificationList.AppItem = AppItem;

            if (!IsPostBack) {
                // SetFormParameter();

                if (AppItem == "mynotelist") {
                    SetSearchCriteriaMyNoteList();
                    SetHeaderMyNoteList();
                }
                //else
                //{
                //    SetSearchCriteria();
                //    SetHeader();
                //}
            }
        }

        //protected void SetFormParameter()
        //{
        //    if (Request["AppItem"] != null)
        //    {
        //        appItem = Request["AppItem"].ToString();
        //    }
        //    else
        //    {
        //        // Sample RNKORDER
        //        appItem = string.Empty;
        //    }
        //}

        public string AppItem {
            get { return appItem; }
            set { appItem = value; }
        }

        private void SetHeaderMyNoteList() {
            // TODO: find right condition 
            this.Header.InstructionText = "Use the Search features and click on Refresh button to filter data.";
            this.Header.IconImage = "~/images/icon/icon_todo.gif";
            this.Header.SectionText = "Note:";
            this.Header.PageText = "My Note List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteriaMyNoteList() {
            this.CtrlBusinessNotificationList.SearchCriteria.Items.Clear();
            this.CtrlBusinessNotificationList.SearchCriteria.Items.Add(new ListItem("Note Subject", "1", true));
            this.CtrlBusinessNotificationList.SearchName.Text = "Note Subject";
            this.CtrlBusinessNotificationList.SearchName = abcNote;
        }
    } 
}