using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductSearch.
	/// </summary>
	public partial class ProductSearch : MarketingMgtPage
	{
		protected QSPFulfillment.MarketingMgt.Control.MagazineSearchControl ctrlMagazineSearchControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				this.ctrlMagazineSearchControl.ShowSearch = this.ShowSearch;
				this.ctrlMagazineSearchControl.ShowSelect = true;
				this.ctrlMagazineSearchControl.ShowEdit = false;

				this.ctrlMagazineSearchControl.DataBindInitialData();

				this.ctrlMagazineSearchControl.PublisherSearch = this.PublisherID;
				this.ctrlMagazineSearchControl.FulfillmentHouseSearch = this.FulfillmentHouseID;

				this.ctrlMagazineSearchControl.DataBind();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlMagazineSearchControl.SelectProductClick += new QSPFulfillment.MarketingMgt.Control.SelectProductEventHandler(ctrlMagazineSearchControl_SelectProductClick);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void ctrlMagazineSearchControl_SelectProductClick(object sender, SelectProductClickedArgs e)
		{
			if(IsNewWindow) 
			{
				AddJavaScriptSelect(e.ProductInfo);
			}
		}

		public bool ShowSearch 
		{
			get 
			{
				if(Request.QueryString["ShowSearch"] == null)
					return true;

				return Convert.ToBoolean(Request.QueryString["ShowSearch"]);
			}
		}

		public string ParentControlName
		{
			get 
			{
				if(Request.QueryString["ParentControlName"] == null)
					return "";

				return Request.QueryString["ParentControlName"].ToString();
			}
		}

		public int PublisherID
		{
			get 
			{
				if(Request.QueryString["PublisherID"] == null)
					return 0;

				return Convert.ToInt32(Request.QueryString["PublisherID"]);
			}
		}

		public int FulfillmentHouseID
		{
			get 
			{
				if(Request.QueryString["FulfillmentHouseID"] == null)
					return 0;

				return Convert.ToInt32(Request.QueryString["FulfillmentHouseID"]);
			}
		}

		private void AddJavaScriptSelect(Product product) 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  window.opener.document.getElementById('" + this.ParentControlName + "').value = '" + product.ProductCode + "'; self.close();\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SelectProductCode", script);
		}
	}
}
