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
    public partial class VendorSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a Vendor, use the Search features and click on Refresh button.  Click on Select to confirm your Vendor choice.";
            this.Header.SectionText = "Vendor";
            this.Header.PageText = "Vendor Selector";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Header.IconImageVisiblilty = false;
        }

        private void SetSearchCriteria() {
            this.ctrlVendorSelector.SearchCriteria.Items.Clear();
            this.ctrlVendorSelector.SearchCriteria.Items.Add(new ListItem("City", "2"));
            this.ctrlVendorSelector.SearchCriteria.Items.Add(new ListItem("Vendor Name", "1", true));
            this.ctrlVendorSelector.SearchCriteria.Items.Add(new ListItem("Zip Code", "3"));
        }
    } 
}