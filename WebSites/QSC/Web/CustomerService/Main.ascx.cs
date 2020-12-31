namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Microsoft.Web.UI.WebControls;
	using System.Web.UI;
	

	public enum MainMultiPage {Result = 1,Subscription=3, Address =2,Order=4,History=5,CreditCard=6,Shipment=7};
	public enum MainControl 
	{
		ctrlMainSearchResult, 
		ctrlMainAddressInfo,
		ctrlMainOrderInfo,
		ctrlMainSubscriberInfo,
		ctrlMainPaymentInfo,
		ctrlMainHistoryInfo,
		ctrlMainIncidentActionInfo,
	}
	/// <summary>
	///		Summary description for main.
	/// </summary>
	public partial class Main : CustomerServiceControl
	{
				
		
	
		private const string MUPAGE_RESULT = "pavMainSearchResult";
        private const string MUPAGE_ADDRESS ="pavMainAddressInfo";
		private const string MUPAGE_ORDER ="pavMainOrderInfo";
		private const string MUPAGE_SUBSCRIBER ="pavMainSubscriberInfo";
		private const string MUPAGE_PAYMENT ="pavMainPaymentInfo";
		private const string MUPAGE_HISTORY ="pavMainHistoryInfo";
		protected System.Web.UI.WebControls.PlaceHolder plhMainCtrl;
		private const string MUPAGE_INCIDENT ="pavMainIncidentActionInfo";
		private const string MUPAGE_SHIPMENT = "pavMainShipmentInfo";
		protected MainSearchResult ctrlMainSearchResult;
		protected MainAddressInfo ctrlMainAddressInfo;
		protected MainOrderInfo ctrlMainOrderInfo;
		//protected MainCreditCardInfo ctrlMainCreditCardInfo;
		//protected MainSubscriberInfo ctrlMainSubscriberInfo;
		protected MainPaymentInfo ctrlMainPaymentInfo;
		protected MainHistoryInfo ctrlMainHistoryInfo;
		protected MainSubscriptionInfo ctrlMainSubscriptionInfo;
		protected MainShipmentInfo ctrlMainShipmentInfo;
		protected Microsoft.Web.UI.WebControls.Tab iewcSearchResult;
		protected Microsoft.Web.UI.WebControls.Tab iewcAddressInformation;
		protected Microsoft.Web.UI.WebControls.Tab iewcOrderInformation;
		protected Microsoft.Web.UI.WebControls.Tab iewcSubscriberInformation;
		protected Microsoft.Web.UI.WebControls.Tab iewcPaymentInformation;
		protected Microsoft.Web.UI.WebControls.Tab iewcHistory;
		protected Microsoft.Web.UI.WebControls.Tab iewcShipment;
		protected Microsoft.Web.UI.WebControls.Tab iewcSubscriptionInfo;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			hypPrint.Visible = false;
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.Page.SelectResultClicked +=new SelectResultEventHandler(ctrlMainSearchResult_SelectResultClicked);
			
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

		public MainMultiPage SelectedPage
		{
			get
			{
				return (MainMultiPage)this.mupMainPage.SelectedIndex;
			}
			set
			{
				
				this.mupMainPage.SelectedIndex = (int)value-1;
				this.tbsMainPage.SelectedIndex = (int)value-1;
			}
		}
		public PageView GetPageView(MainMultiPage Value)
		{
			string ViewPageName = "";
			switch(Value)
			{
				case MainMultiPage.Address:
					ViewPageName = MUPAGE_ADDRESS;
					break;
				case MainMultiPage.History:
					ViewPageName = MUPAGE_HISTORY;
					break;
				case MainMultiPage.Shipment:
					ViewPageName = MUPAGE_INCIDENT;
					break;
				case MainMultiPage.Order:
					ViewPageName = MUPAGE_ORDER;
					break;
				case MainMultiPage.Result:
					ViewPageName = MUPAGE_RESULT;
					break;
			}
				
			
			return (PageView)mupMainPage.FindControl(ViewPageName);
		}

		public MainSearchResult ControlMainSearchResult
		{
			get{return ctrlMainSearchResult;}
		}

		public MainAddressInfo ControlMainAddressInfo
		{
			get{return ctrlMainAddressInfo;}
		}
		public MainPaymentInfo ControlMainPaymentInfo
		{
			get{return ctrlMainPaymentInfo;}
		}
		public MainOrderInfo ControlMainOrderInfo
		{
			get{return ctrlMainOrderInfo;}
		}
		/*public MainIncidentActionInfo ControlMainIncidentActionInfo
		{
			get{return ctrlMainIncidentActionInfo;}
		}*/
		public MainHistoryInfo ControlMainHistoryInfo
		{
			get{return ctrlMainHistoryInfo;}
		}
		
		public MainSubscriptionInfo ControlMainSubscriptionInfo
		{
			get{return ctrlMainSubscriptionInfo;}
		}

		public void DisabledAllTabStrip()
		{
			tbsMainPage.Enabled = false;
		}
		public bool EnabledSearchResult
		{
			set
			{
				this.iewcSearchResult.Enabled = value;
			}
		}
		public void EnabledForSearch(bool Value)
		{
			iewcAddressInformation.Enabled = Value;
			iewcHistory.Enabled = Value;
			iewcOrderInformation.Enabled = Value;
			iewcSubscriptionInfo.Enabled = Value;
			hypPrint.Visible = Value;
			
			

			if(Value)
			{
				iewcAddressInformation.DefaultImageUrl = "images/_addressinfo_off.gif";
				iewcHistory.DefaultImageUrl = "images/_history_off.gif";
				iewcOrderInformation.DefaultImageUrl = "images/_orderinfo_off.gif";
				iewcSubscriptionInfo.DefaultImageUrl = "images/_subscriptioninfo_off.gif";
				iewcSearchResult.DefaultImageUrl = "images/_searchresult_off.gif";
				Ajax.Utility.RegisterTypeForAjax(typeof(CustomerServicePage));
			}
			else
			{
				iewcSearchResult.DefaultImageUrl = "images/_searchresult_off_disabled.gif";
				iewcAddressInformation.DefaultImageUrl = "images/_addressinfo_off_disabled.gif";
				iewcHistory.DefaultImageUrl = "images/_history_off_disabled.gif";
				iewcOrderInformation.DefaultImageUrl = "images/_orderinfo_off_disabled.gif";
				
				iewcSubscriptionInfo.DefaultImageUrl = "images/_subscriptioninfo_off_disabled.gif";
				
				
			}
			
		
		}
		public void EnabledForSearchShipment(bool Value)
		{
			iewcShipment.Enabled = Value;
			
			if(Value)
			{
				iewcShipment.DefaultImageUrl = "images/_shipment_off.gif";
				
			}
			else
			{
				iewcShipment.DefaultImageUrl  = "images/_shipmentinfo_off_disabled.gif";
						
				
			}
			
		
		}
		public void EnabledForSearchPaymentInformation(bool Value)
		{
			iewcPaymentInformation.Enabled = Value;
			
			if(Value)
			{
				iewcPaymentInformation.DefaultImageUrl = "images/_creditcardinfo_off.gif";
				
			}
			else
			{
				iewcPaymentInformation.DefaultImageUrl  = "images/_creditcardinfo_off_disabled.gif";
						
				
			}
			
		
		}

		private void ctrlMainSearchResult_SelectResultClicked(object sender, SelectResultClickedArgs e)
		{
			if(this.Page.ResultType == SearchMultiPage.CreditCard)
			{
				SelectedPage = MainMultiPage.CreditCard;
				this.Page.ResultSelected = true;
				this.iewcPaymentInformation.Enabled = true;
				iewcPaymentInformation.DefaultImageUrl = "images/_creditcardinfo_off.gif";
				EnabledForSearch(false);
				iewcSearchResult.DefaultImageUrl = "images/_searchresult_off.gif";
				this.ctrlMainPaymentInfo.DataBind();
			}
			else if(this.Page.ResultType != SearchMultiPage.Shipment && this.Page.ResultType != SearchMultiPage.CreditCard)
			{
				Bind(e);
				EnabledForSearch(true);
				SelectedPage = MainMultiPage.Address;
				this.Page.ResultSelected= true;
				iewcShipment.DefaultImageUrl =  "images/_shipmentinfo_off_disabled.gif";
				iewcShipment.Enabled = false;
				iewcPaymentInformation.Enabled = true;
				iewcPaymentInformation.DefaultImageUrl = "images/_creditcardinfo_off.gif";

			}
			else if(this.Page.ResultType == SearchMultiPage.Shipment)
			{
				
				SelectedPage = MainMultiPage.Shipment;
				this.Page.ResultSelected = true;
				this.iewcShipment.Enabled = true;
				iewcShipment.DefaultImageUrl = "images/_shipmentinfo_off.gif";
				EnabledForSearch(false);
				iewcSearchResult.DefaultImageUrl = "images/_searchresult_off.gif";
				iewcPaymentInformation.Enabled = false;
				iewcPaymentInformation.DefaultImageUrl =  "images/_creditcardinfo_off_disabled.gif";

				this.ctrlMainShipmentInfo.DataBind();
			}
			
		
		}
		
		private void Bind(SelectResultClickedArgs e)
		{
			this.ctrlMainAddressInfo.DataBind();
			this.ctrlMainSubscriptionInfo.DataBind();
			this.ctrlMainHistoryInfo.DataBind();
			this.ctrlMainOrderInfo.DataBind();
			this.ctrlMainPaymentInfo.DataBind();
		}
	



		
	}
}
