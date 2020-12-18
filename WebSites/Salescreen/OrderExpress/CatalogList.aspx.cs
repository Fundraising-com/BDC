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
    public partial class CatalogList : BaseWebForm {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetSearchCriteria();
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "To Locate a catalog, you can enter a criteria and/or use the Catalog Group filter.";
            this.Header.IconImage = "~/images/icon/icon_admin.gif";
            this.Header.SectionText = "Catalog";
            this.Header.PageText = "Catalog List";
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void SetSearchCriteria() {
            this.ctrlCatalogList.SearchCriteria.Items.Clear();
            this.ctrlCatalogList.SearchCriteria.Items.Add(new ListItem("Catalog Code", "2"));
            this.ctrlCatalogList.SearchCriteria.Items.Add(new ListItem("Catalog Name", "1", true));
            this.ctrlCatalogList.SearchCriteria.Items.FindByValue("1").Selected = true;
            this.ctrlCatalogList.SearchName.Text = "Catalog Name";
            this.ctrlCatalogList.SearchName = abcNote;
        }
    } 
}