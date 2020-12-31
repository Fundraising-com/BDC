namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Microsoft.Web.UI.WebControls;

	public enum SearchMultiPage {None,Order,Shipment,Group,Subscription,CreditCard};
	/// <summary>
	///		Summary description for searchmodule.
	/// </summary>
	public partial class searchmodule : CustomerServiceControl
	{
	
		private const string MUPAGE_GROUP = "pavSearchGroup";
		private const string MUPAGE_SHIPPEMENT ="pavSearchShippement";
		private const string MUPAGE_ORDER ="pavSearchOrder";
		private const string MUPAGE_SUBSCRIBER ="pavSearchSubscriber";
		protected PageSearchShippement ctrlPageSearchShippement;
		protected PageSearchGroupOrder ctrlPageSearchGroupOrder;
		protected PageSearchSubscription ctrlPageSearchSubscription;
		protected PageSearchCreditCard ctrlPageSearchCreditCard;

		public event SearchEventHandler SearchClicked;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				this.ValidationSummary1.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ctrlPageSearchShippement.SearchClicked += new SearchEventHandler(ctrlPageSearch_SearchClicked);
			ctrlPageSearchGroupOrder.SearchClicked += new SearchEventHandler(ctrlPageSearch_SearchClicked);;
			ctrlPageSearchSubscription.SearchClicked += new SearchEventHandler(ctrlPageSearch_SearchClicked);
			ctrlPageSearchCreditCard.SearchClicked += new SearchEventHandler(ctrlPageSearch_SearchClicked);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void ctrlPageSearch_SearchClicked(object sender,SearchClickedArgs e)
		{
			if(Validate(e.ResultType))
			{
				if(SearchClicked != null)
					SearchClicked(this,new SearchClickedArgs(e.ResultType,e.List,e.Filter,e.ItemType));
			}
			else
			{
				
				this.Page.SetPageError();
			}
		}

		private bool Validate(SearchMultiPage ResultType)
		{
			bool IsValid = true;
			
			if(ResultType == SearchMultiPage.Subscription)
				IsValid &= this.ctrlPageSearchSubscription.Validate();
			else if(ResultType ==  SearchMultiPage.Order)
				IsValid &= this.ctrlPageSearchGroupOrder.Validate();

			else if (ResultType ==  SearchMultiPage.Shipment)
				IsValid &= this.ctrlPageSearchShippement.Validate();
			
			return IsValid;

		}
		
	}
}
