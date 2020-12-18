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
    public partial class WarehouseList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "Use the Search features and click on Refresh button to filter data. To access specific Warehouse Detail Information and Product Inventory, click on any field within the row for that Warehouse.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Warehouse:";
            this.Header.PageText = "Warehouse List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlWarehouseList.SearchCriteria.Items.Clear();
            this.ctrlWarehouseList.SearchCriteria.Items.Add(new ListItem("City", "2"));
            this.ctrlWarehouseList.SearchCriteria.Items.Add(new ListItem("EDS WH #", "5"));
            this.ctrlWarehouseList.SearchCriteria.Items.Add(new ListItem("QSP WH ID #", "4"));
            this.ctrlWarehouseList.SearchCriteria.Items.Add(new ListItem("Warehouse Name", "1", true));
            this.ctrlWarehouseList.SearchCriteria.Items.Add(new ListItem("Zip Code", "3"));
            this.ctrlWarehouseList.SearchCriteria.Items.FindByText("Warehouse Name").Selected = true;
            this.ctrlWarehouseList.SearchName.Text = "Warehouse Name";
            this.ctrlWarehouseList.SearchName = abcNote;
        }
    } 
}