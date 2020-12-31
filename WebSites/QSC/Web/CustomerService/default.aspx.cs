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
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public partial class _default : CustomerServicePage
	{

		protected Main ctrlMain;
		protected searchmodule ctrlSearchModule;
		protected ControlerListAction ctrlControlerListAction;
		protected ControlerAction ctrlControlerAction;
		protected System.Web.UI.WebControls.Label lblWait;
		protected ActionReason ctrlActionReason;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*if(!IsPostBack)
				hidDataBind.Value = "0";*/
			
			ctrlControlerListAction.ActionType = ListActionType.Resume;
			
			if(ActionEntered)
			{

                if (ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.ChangeNameAddress || ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.MissingNameAddess
                    || ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.IssueCustomerRefund || ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.CancelCustomerRefund)
				{
					this.ctrlMain.ControlMainAddressInfo.DataBind();
				}
                if (ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.CreditCard)
				{
					this.ctrlMain.ControlMainPaymentInfo.DataBind();
				}
                if (ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.NewSub ||
                    ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.NewItem ||
                    ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.NewSubToInvoice ||
                    ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.NewItemToInvoice ||
                    ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.ChangeNameAddress ||
                    ActionNameEntered == QSPFulfillment.DataAccess.Business.Action.CreditCard) 
				{
					this.NewSearch = true;
				}

				this.ctrlMain.ControlMainSubscriptionInfo.DataBind();
				hidDataBind.Value = "0";
			}
			
		}
		
 
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ctrlSearchModule.SearchClicked +=new SearchEventHandler(ctrlSearchModule_SearchClicked);
			this.SelectResultClicked +=new SelectResultEventHandler(_default_SelectResultClicked);
			//this.hidDataBind.ServerChange +=new EventHandler(hidDataBind_ServerChange);
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

		private void SetDisabledTabStrip()
		{
			this.ctrlMain.DisabledAllTabStrip();
		}

		private void ctrlSearchModule_SearchClicked(object sender, SearchClickedArgs e)
		{
				this.ctrlMain.SelectedPage = MainMultiPage.Result;
				this.ResultType = e.ResultType; 
			    if(e.ResultType == SearchMultiPage.Order)
                		this.ctrlMain.ControlMainSearchResult.ItemType = e.ItemType;
				
				this.ctrlMain.ControlMainSearchResult.List = e.List;
				this.ctrlMain.ControlMainSearchResult.Filter = e.Filter;
				this.NewSearch = true;
				this.PageIndexSubNested = 0;
				this.ResultSelected = false;
				this.ActionReasonEntered = false;
				this.ShowStep3 = false;
				this.ShowStep1 = true;
				this.ctrlMain.EnabledSearchResult = true;
				this.ctrlMain.EnabledForSearch(false);
				this.ctrlMain.EnabledForSearchShipment(false);
				this.ctrlMain.EnabledForSearchPaymentInformation(false);
				this.OrderInfo = null;
		}

		private void _default_SelectResultClicked(object sender, SelectResultClickedArgs e)
		{

			this.ResultSelected = true;
			this.NewSearch = false;

			if(ResultType != SearchMultiPage.Shipment)
			{
				this.ShowStep1 = false;
				this.ShowStep3 = true;
				this.ActionReasonEntered = false;
				this.IncidentID = -1;
				this.ctrlControlerListAction.DataBind();
				
				
			}
			else
			{
				this.ShowStep1 = false;
				this.ShowStep3 = false;
				
			}
			
			
	
		}
		private bool ActionEntered
		{
			get
			{
				if(hidDataBind.Value != "0")
					return true;

				return false;
			}
			
		
		}
		private QSPFulfillment.DataAccess.Business.Action ActionNameEntered
		{
			get
			{
                return (QSPFulfillment.DataAccess.Business.Action)Convert.ToInt32(hidDataBind.Value);
			}
		}

		/*private void hidDataBind_ServerChange(object sender, EventArgs e)
		{
			if(ActionEntered)
			{
				
			}
		}*/
	}
}
