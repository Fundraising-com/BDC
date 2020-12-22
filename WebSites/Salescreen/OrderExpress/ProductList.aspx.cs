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
    public partial class ProductList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "Click on Add button to enter new product.  Use the Search features and click on Refresh button to filter data.";
            this.Header.SectionText = "Product :";
            this.Header.PageText = "Product List";
            this.Header.IconImage = "~/images/icon/icon_order.gif";
            this.Header.IconImageVisiblilty = false;
            this.LabelMessage = this.Master.LabelMessage1;

        }

        private void SetSearchCriteria() {
            this.ctrlProductList.SearchCriteria.Items.Clear();
            this.ctrlProductList.SearchCriteria.Items.Add(new ListItem("Product Code", "2"));
            this.ctrlProductList.SearchCriteria.Items.Add(new ListItem("Product Name", "1", true));
            this.ctrlProductList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlProductList.SearchName.Text = "Product Name";
            this.ctrlProductList.SearchName = abcNote;
        }
    } 
}