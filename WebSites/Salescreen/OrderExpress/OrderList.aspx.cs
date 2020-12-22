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
    public partial class OrderList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate an Order, use the Search and Filter features and click on Refresh button.";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.SectionText = "Order:";
            this.Header.PageText = "Order List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlOrderList.SearchCriteria.Items.Clear();
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("Account Name", "1", true));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("City", "6"));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("EDS Account #", "5"));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("EDS Order #", "9"));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("QSP Account ID #", "7"));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("QSP Order ID", "8"));
            this.ctrlOrderList.SearchCriteria.Items.Add(new ListItem("Zip Code", "4"));
            this.ctrlOrderList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlOrderList.SearchName.Text = "Account Name";
            this.ctrlOrderList.SearchName = abcNote;
        }
    } 
}