namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.CustomerService;

	
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class FulfillmentHouseContactProductListControl : ContactProductListControl
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Button btnAddProduct;
		protected QSPFulfillment.MarketingMgt.Control.ProductCodePickerControl ctrlProductCodePickerControl;

		public event System.EventHandler NewContactProductPreClick;
		public event SelectContactProductEventHandler NewContactProductClicked;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e, dtgMain, lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
		}
		#endregion

		public int FulfillmentHouseID 
		{
			get 
			{
				int fulfillmentHouseID = 0;

				if(ViewState["FulfillmentHouseID"] != null) 
				{
					fulfillmentHouseID = Convert.ToInt32(ViewState["FulfillmentHouseID"]);
				}

				return fulfillmentHouseID;
			}
			set 
			{
				ViewState["FulfillmentHouseID"] = value;
			}
		}

		private void btnAddProduct_Click(object sender, System.EventArgs e)
		{
			int fulfillmentHouseContactID = 0;
			SelectContactProductClickedArgs args = null;

			try 
			{
				if(this.Page.BusFulfillmentHouseContactProduct.ValidateInsert(FulfillmentHouseID, ctrlProductCodePickerControl.ProductCode))
				{
					if(NewContactProductPreClick != null) 
					{
						NewContactProductPreClick(this, EventArgs.Empty);
					}

					fulfillmentHouseContactID = this.Page.BusFulfillmentHouseContactProduct.Insert(ContactID, ctrlProductCodePickerControl.ProductCode, Convert.ToInt32(this.Page.UserID));

					args = new SelectContactProductClickedArgs(fulfillmentHouseContactID);

					if(NewContactProductClicked != null) 
					{
						NewContactProductClicked(this, args);
					}
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public override void DataBind()
		{
			base.DataBind();

			this.ctrlProductCodePickerControl.FulfillmentHouseID = FulfillmentHouseID;
			this.ctrlProductCodePickerControl.DataBind();
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Product");

			this.Page.BusFulfillmentHouseContactProduct.SelectAllByFulfillmentHouseContactID(DataSource, ContactID);
		}

		protected override void OnDeleteContactProductClicked(object sender, SelectContactProductClickedArgs e)
		{
			this.Page.BusFulfillmentHouseContactProduct.Delete(ContactID, e.ContactProductID, true);

			base.OnDeleteContactProductClicked (sender, e);
		}

		protected override int GetContactProductID(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblID")).Text);
		}
	}
}