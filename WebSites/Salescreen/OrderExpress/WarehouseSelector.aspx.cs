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
    public partial class WarehouseSelector : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To locate a Warehouse, use the Search and Filter features and click on Refresh button.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Warehouse:";
            this.Header.PageText = "Warehouse Selector";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.WarehouseSelector1.SearchCriteria.Items.Clear();
            this.WarehouseSelector1.SearchCriteria.Items.Add(new ListItem("City", "2"));
            this.WarehouseSelector1.SearchCriteria.Items.Add(new ListItem("EDS WH #", "5"));
            this.WarehouseSelector1.SearchCriteria.Items.Add(new ListItem("QSP WH ID #", "4"));
            this.WarehouseSelector1.SearchCriteria.Items.Add(new ListItem("Warehouse Name", "1", true));
            this.WarehouseSelector1.SearchCriteria.Items.Add(new ListItem("Zip Code", "3"));
        }
    } 
}