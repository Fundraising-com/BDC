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
    public partial class DocumentEntityList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To acknowledge the receipt of a document for an account, click on the text box in the Received column.  Use the Search features and click on Refresh button to filter data.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Admin:";
            this.Header.PageText = "Document List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlDocumentEntityList.SearchCriteria.Items.Clear();
            this.ctrlDocumentEntityList.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlDocumentEntityList.SearchCriteria.Items.Add(new ListItem("EDS Account #", "3"));
            this.ctrlDocumentEntityList.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "4"));
            this.ctrlDocumentEntityList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlDocumentEntityList.SearchName.Text = "Account Name";
            this.ctrlDocumentEntityList.SearchName = abcNote;
        }
    } 
}