namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using QSPFulfillment.DataAccess.Business;

	

	
	/// <summary>
	///		Summary description for MainSearchResult.
	/// </summary>
	public partial class MainSearchResult : ControlerResult//,IGrid
	{
		
		protected ResultOrder ctrlResultOrder;
		protected ResultSubscription ctrlResultSubscription;
		protected ResultShipment ctrlResultShipment;
		protected ResultHeaderCreditCard ctrlResultHeaderCreditCard;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.PreRender += new System.EventHandler(this.MainSearchResult_PreRender);

		}
		#endregion

		
        
		protected void MainSearchResult_PreRender(object sender, EventArgs e)
		{
			
			if(this.Page.ResultType == SearchMultiPage.Order)
			{					
				ctrlResultOrder.ItemType = ItemType;
				ctrlResultOrder.List = this.List;
				ctrlResultOrder.Filter = this.Filter;
				ctrlResultOrder.Visible = true;
				ctrlResultSubscription.Visible = false;
				ctrlResultOrder.Visible = true;
				ctrlResultHeaderCreditCard.Visible = false;
				ctrlResultShipment.Visible = false;
				//if(this.Page.NewSearch || this.Page.PageChanged || !this.Page.ResultSelected) 
				//{
					ctrlResultOrder.DataBind();
				//}
			}
			else if(this.Page.ResultType == SearchMultiPage.Subscription)
			{
				ctrlResultSubscription.ItemType =(int) ProductType.Magazine;
				ctrlResultSubscription.List = this.List;
				ctrlResultOrder.Visible = false;
				ctrlResultShipment.Visible = false;
				ctrlResultSubscription.Visible = true;
				ctrlResultHeaderCreditCard.Visible = false;
				if(this.Page.NewSearch || this.Page.PageChanged || !this.Page.ResultSelected) 
				{
					ctrlResultSubscription.DataBindNotNested();
				}
			}
			else if(this.Page.ResultType == SearchMultiPage.Shipment)
			{
				ctrlResultShipment.ItemType = (int)ProductType.Magazine;
				ctrlResultShipment.List = this.List;
				ctrlResultOrder.Visible = false;
				ctrlResultShipment.Visible = true;
				ctrlResultSubscription.Visible = false;
				ctrlResultHeaderCreditCard.Visible = false;
				//if(this.Page.NewSearch || this.Page.PageChanged || !this.Page.ResultSelected) 
				//{
					ctrlResultShipment.DataBind();
				//}
			}
			else if(this.Page.ResultType == SearchMultiPage.CreditCard)
			{
				
				ctrlResultHeaderCreditCard.ItemType = (int)ProductType.Magazine;
				ctrlResultHeaderCreditCard.List = this.List;
				ctrlResultOrder.Visible = false;
				ctrlResultShipment.Visible = false;
				ctrlResultSubscription.Visible = false;
				ctrlResultHeaderCreditCard.Visible = true;
				//if(this.Page.NewSearch || this.Page.PageChanged || !this.Page.ResultSelected) 
				//{
					ctrlResultHeaderCreditCard.DataBind();
				//}
			}
		}
	
		private void btnSelectMultipleSubscription_Click(object sender, EventArgs e)
		{
			this.Page.FireEventSelect(new SelectResultClickedArgs(true));
		}
	}
}
/**/