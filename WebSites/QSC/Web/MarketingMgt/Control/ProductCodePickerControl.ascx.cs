namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ProductCodePickerControl.
	/// </summary>
	public partial class ProductCodePickerControl : MarketingMgtControl
	{
		private const string HYL_ENABLED_IMAGE_URL = "../images/find.gif";
		private const string HYL_DISABLED_IMAGE_URL = "../images/find_disabled.gif";
		private const string HYL_NAVIGATE_URL_PREFIX = "javascript: OpenBig(\"ProductSearch.aspx";


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected void ProductCodePickerControl_PreRender(object sender, EventArgs e)
		{

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.ProductCodePickerControl_PreRender);

		}
		#endregion

		public bool Enabled 
		{
			get 
			{
				return !this.tbxProductCode.ReadOnly;
			}
			set 
			{
				this.tbxProductCode.ReadOnly = !value;
				this.hylPicker.Enabled = value;

				if(value) 
				{
					this.hylPicker.ImageUrl = HYL_ENABLED_IMAGE_URL;
				} 
				else 
				{
					this.hylPicker.ImageUrl = HYL_DISABLED_IMAGE_URL;
				}
			}
		}

		public bool Required 
		{
			get 
			{
				return this.tbxProductCode.Required;
			}
			set 
			{
				this.tbxProductCode.Required = value;
			}
		}

		public string ProductCode 
		{
			get 
			{
				return this.tbxProductCode.Text;
			}
			set 
			{
				this.tbxProductCode.Text = value;
			}
		}

		public int PublisherID 
		{
			get 
			{
				if(this.ViewState["PublisherID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["PublisherID"]);
			}
			set 
			{
				this.ViewState["PublisherID"] = value;
			}
		}

		public int FulfillmentHouseID 
		{
			get 
			{
				if(this.ViewState["FulfillmentHouseID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["FulfillmentHouseID"]);
			}
			set 
			{
				this.ViewState["FulfillmentHouseID"] = value;
			}
		}

		public override void DataBind()
		{
			this.hylPicker.NavigateUrl = HYL_NAVIGATE_URL_PREFIX + "?IsNewWindow=true&ShowSearch=false&ParentControlName=" + this.tbxProductCode.ClientID;

			if(this.PublisherID != 0) 
			{
				this.hylPicker.NavigateUrl += "&PublisherID=" + this.PublisherID.ToString();
			} 
			else if(this.FulfillmentHouseID != 0) 
			{
				this.hylPicker.NavigateUrl += "&FulfillmentHouseID=" + this.FulfillmentHouseID.ToString();
			}

			this.hylPicker.NavigateUrl += "\");";
		}
	}
}
