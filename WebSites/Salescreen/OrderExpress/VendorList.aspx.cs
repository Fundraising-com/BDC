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
    public partial class VendorList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To Locate a vendor, you can enter a criteria and/or use the State filter.";
            this.Header.SectionText = "Admin";
            this.Header.PageText = "Vendor List";
            this.Header.IconImage = "~/images/spacer.gif";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlVendorList.SearchCriteria.Items.Clear();
            this.ctrlVendorList.SearchCriteria.Items.Add(new ListItem("City", "2"));
            this.ctrlVendorList.SearchCriteria.Items.Add(new ListItem("Vendor Name", "1", true));
            this.ctrlVendorList.SearchCriteria.Items.Add(new ListItem("Zip Code", "3"));
            this.ctrlVendorList.SearchCriteria.Items.FindByValue("1").Selected = true;
        }
    } 
}