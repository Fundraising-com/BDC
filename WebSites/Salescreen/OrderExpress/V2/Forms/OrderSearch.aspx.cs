using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web.V2.Forms
{
    public partial class OrderSearch : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.ucOrderSearch.SearchClick += new EventHandler(Page_SearchClick);

            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }

            this.SetPageTitle();
        }
        protected void Page_SearchClick(object sender, EventArgs e)
        {
            OrderSearchEventArgs oe = (OrderSearchEventArgs)e;

            this.ucOrderSearchResults.SetSearchParameters(oe.Parameters);
            this.ucOrderSearchResults.DoSearch();
        }

        private void SetPageTitle()
        {
            string title;

            title = "Search orders - Order Express";

            this.Title = title;
        }
    }
}
