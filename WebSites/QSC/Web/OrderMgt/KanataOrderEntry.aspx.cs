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
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Data;
using QSPFulfillment.DataAccess.Common.TableDef;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.CustomerService;
using QSP.WebControl;
using System.Web.Mail;
using System.Text;

namespace QSPFulfillment.OrderMgt
{
	public enum ProductReplacementStep 
	{
		SelectCampaign,
		SelectProducts,
		ConfirmOrder,
		ViewOrder
	}

	/// <summary>
	/// Summary description for KanataOrderEntry.
	/// </summary>
	public partial class KanataOrderEntry :   QSPFulfillment.CustomerService.CustomerServicePage, IOnloadJSEvent
	{
		protected ControlerCampaignsForProductReplacement ctrlControlerCampaignsForKanataOrder;
		protected ControlerProductMultiSelectForKanata ctrlControlerProductMultiSelectForKanata;
		protected System.Web.UI.HtmlControls.HtmlContainerControl BodyTag;
		private QSPFulfillment.DataAccess.Business.KanataOEBusiness bKanataOEBusiness;
		protected ControlerKanataProductConfirmation ctrlControlerKanataProductConfirmation;
		protected ControlerKanataProductConfirmation ctrlControlerKanataProductView;
		protected int currentOrderID;
		private string accountName;
		private string currentCatalog;
      private int currentCatalogType;

      protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				if (!IsPostBack)
				{
					this.CurrentBatch = new Batch();
					this.CurrentBatch.OrderID = Convert.ToInt32(Request.QueryString["OrderID"].ToString());
					this.BusProduct.SelectOrderDetailInBatch(this.CurrentBatch);
					if (this.CurrentBatch.OrderID <= 0)
					{
						this.CurrentBatch.OrderHeaders.Add( new OrderHeader());
					}
				}

				//----
				//  Check if existing order is being edited, ensuring that if user is FM, that it's their own order
				//----
				if (this.CurrentBatch.OrderID != -1 && !IsPostBack
					&& ((!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM
					&& QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999")
					|| QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == this.CurrentBatch.Campaign.FMID))
				{

					if (this.CurrentBatch.Status == 40002) //status = 'In Process', therefore edit
					{
						SetValueStepEditOrder();
					}
					else //view order only
					{
						SetValueStepViewOrder();
					}
				}
				else
				{
					if(FirstTimeCustomerService)
					{
						ShowStep1 = true;
						ShowStep3 = false;
						ShowDefaultSearch = true;
					}
					if(!IsPostBack) 
					{
						SetValueProductType();
						SetValueStepSelectCampaign();
						lblPageTitle.Text = "Rapid Order Entry - New Order";
					}
					else if(CurrentStep == ProductReplacementStep.ConfirmOrder) 
					{
						this.ctrlControlerKanataProductConfirmation.CurrentBatch = this.CurrentBatch;
						this.ctrlControlerKanataProductConfirmation.CurrentOrderHeader = this.CurrentBatch.OrderHeaders[0];
					}
					else if(CurrentStep == ProductReplacementStep.SelectProducts) 
					{
						this.ctrlControlerProductMultiSelectForKanata.InitProductSelect();
					}

					SetValueEmptyConfirmation();
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
		public KanataOEBusiness BusKanataOE
		{
			get
			{
				if(bKanataOEBusiness == null)
					bKanataOEBusiness = new KanataOEBusiness (base.MessageManager); 

				return bKanataOEBusiness;
			}
				
		}
		public string AccountName
		{
			get
			{
				if (accountName == null)
				{
					CAccountTable cAccountTable = new CAccountTable();
					CAccountData cAccountDataAccess = new CAccountData();
					cAccountDataAccess.SelectOne(cAccountTable, this.CurrentBatch.Campaign.AccountID);
					AccountName = cAccountTable.Rows[0]["name"].ToString();
				}
				return accountName;
			}
			set
			{
				accountName = value;
			}
		}
		private void SetValueProductType() 
		{
			this.ctrlControlerCampaignsForKanataOrder.ProductType = this.ProductType;
			this.ctrlControlerProductMultiSelectForKanata.ProductType = this.ProductType.SearchID;
		}

		private ProductReplacementType ProductType
		{
			get 
			{
				return ProductReplacementTypeFactory.Instance.GetProductReplacementType(Request.QueryString["ProductType"]);
			}
		}
		public string onload_script
		{
			get 
			{
				if(BodyTag.Attributes["onload"] == null)
					BodyTag.Attributes["onload"] = "";

				return BodyTag.Attributes["onload"];
			}
			set 
			{
				BodyTag.Attributes["onload"] = value;
			}
		}
		private void SetValueStepSelectCampaign()
		{
			this.CurrentStep = ProductReplacementStep.SelectCampaign;

			this.ctrlControlerCampaignsForKanataOrder.Visible = true;
			this.ctrlControlerProductMultiSelectForKanata.Visible = false;
			this.ctrlControlerKanataProductConfirmation.Visible = false;
			this.ctrlControlerKanataProductView.Visible = false;

			if(IsPostBack) 
			{
				this.ctrlControlerCampaignsForKanataOrder.DataBind();
			}

			this.lblInstructions.Text = "Please select a campaign for which you wish to order product(s).";

			this.hypHelp.NavigateUrl = "javascript:window.open('../Common/Documentation/RapidOrderEntryManual.doc#SelectCampaign',null, 'status=no, location=no, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";
		}

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			base.OnInit(e);
			InitializeComponent();
			this.ctrlControlerCampaignsForKanataOrder.SelectCampaignClick += new QSPFulfillment.CustomerService.SelectCampaignEventHandler(ctrlControlerCampaignsForKanataOrder_SelectCampaignClick);
			this.ctrlControlerProductMultiSelectForKanata.StepBackClicked += new EventHandler(ctrlControlerProductMultiSelectForKanata_StepBackClicked);		
			this.ctrlControlerProductMultiSelectForKanata.ProductItemsSelected += new SelectProductItemsEventHandler(ctrlControlerProductMultiSelectForKanata_ProductItemsSelected);
			//this.ctrlControlerProductReplacement.ConfirmedClicked += new EventHandler(ctrlControlerProductReplacement_ConfirmedClicked);
			this.ctrlControlerKanataProductConfirmation.ConfirmedClicked += new EventHandler(ctrlControlerKanataProductConfirmation_ConfirmedClicked);
			this.ctrlControlerKanataProductConfirmation.SaveClicked += new EventHandler(ctrlControlerKanataProductConfirmation_SaveClicked);
			this.ctrlControlerKanataProductConfirmation.StepBackClicked += new EventHandler(ctrlControlerKanataProductConfirmation_StepBackClicked);		
		}
	
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void ctrlControlerCampaignsForKanataOrder_SelectCampaignClick(object sender, Campaign campaign)
		{
			StartNewBatch();
			this.CurrentBatch.Campaign = campaign;
			SetValueStepSelectProducts();
			SetValueAccountCampaignOrder();
		}

		private void SetValueStepSelectProducts() 
		{
			this.CurrentStep = ProductReplacementStep.SelectProducts;

			this.ctrlControlerCampaignsForKanataOrder.Visible = false;
			this.ctrlControlerProductMultiSelectForKanata.Visible = true;
			this.ctrlControlerKanataProductConfirmation.Visible = false;
			
			this.ctrlControlerProductMultiSelectForKanata.CampaignID = this.CurrentBatch.Campaign.CampaignID;
			this.ctrlControlerProductMultiSelectForKanata.IsFMAccount = this.CurrentBatch.Campaign.IsFMAccount;
			this.ctrlControlerProductMultiSelectForKanata.DataBind();

			this.lblInstructions.Text = "Please select the product(s) you wish to order.";
			//this.hypHelp.NavigateUrl = "javascript:window.open('../Common/HelpPage.aspx?HelpTextID=2&IsNewWindow=true',null, 'height=200, width=400, status=yes, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";
			this.hypHelp.NavigateUrl = "javascript:window.open('../Common/Documentation/RapidOrderEntryManual.doc#SelectProducts',null, 'status=no, location=no, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";
		}

		private ProductReplacementStep CurrentStep 
		{
			get 
			{
				if(this.ViewState["CurrentStep"] == null)
					this.ViewState["CurrentStep"] = ProductReplacementStep.SelectCampaign;

				return (ProductReplacementStep) this.ViewState["CurrentStep"];
			}
			set 
			{
				this.ViewState["CurrentStep"] = value;
			}
		}


		private Batch CurrentBatch 
		{
			get 
			{
				return (Batch) ViewState["Batch"];
			}
			set 
			{
				ViewState["Batch"] = value;
			}
		}

		private void StartNewBatch() 
		{
			this.CurrentBatch = new Batch();
			//this.CurrentBatch.OrderQualifierID = OrderQualifierFactory.Instance.GetOrderQualifierForKanataDE(this.ProductType);
			this.ctrlControlerProductMultiSelectForKanata.CurrentOrderHeader = new OrderHeader();
		}
		private void ctrlControlerProductMultiSelectForKanata_StepBackClicked(object sender, EventArgs e)
		{
			this.SetValueStepSelectCampaign();
		}

		private void ctrlControlerKanataProductConfirmation_StepBackClicked(object sender, EventArgs e)
		{
			this.SetValueStepSelectProducts();
		}

		private void ctrlControlerProductMultiSelectForKanata_ProductItemsSelected(object sender, OrderHeader orderHeader)
		{
			if(!this.CurrentBatch.OrderHeaders.Contains(orderHeader)) 
			{
				this.CurrentBatch.OrderHeaders.Add(orderHeader);
			
			} 
			else 
			{
				this.CurrentBatch.OrderHeaders[this.CurrentBatch.OrderHeaders.IndexOf(orderHeader)] = orderHeader;
			}

			OrderQualifier OrderQualifierID = OrderQualifierFactory.Instance.GetOrderQualifierFromCatalogName(this.ctrlControlerProductMultiSelectForKanata.DDLCatalogType);
			if (OrderQualifierID != OrderQualifier.None || this.CurrentBatch.OrderQualifierID == OrderQualifier.WFCSigningBonus)
				this.CurrentBatch.OrderQualifierID = OrderQualifierFactory.Instance.GetOrderQualifierFromCatalogName(this.ctrlControlerProductMultiSelectForKanata.DDLCatalogType);
			
			this.ctrlControlerKanataProductConfirmation.CurrentCatalog = CurrentCatalog;
         this.ctrlControlerKanataProductConfirmation.CurrentCatalogType = CurrentCatalogType;

         SetValueStepConfirmOrder();
		}

		private void SetValueStepConfirmOrder() 
		{
			
			this.CurrentStep = ProductReplacementStep.ConfirmOrder;

			this.ctrlControlerCampaignsForKanataOrder.Visible = false;
			this.ctrlControlerProductMultiSelectForKanata.Visible = false;
			this.ctrlControlerKanataProductConfirmation.Visible = true;
			this.ctrlControlerKanataProductView.Visible = false;
		
			this.ctrlControlerKanataProductConfirmation.CurrentBatch=CurrentBatch;
			this.ctrlControlerKanataProductConfirmation.CurrentOrderHeader=this.CurrentBatch.OrderHeaders[0];

			this.ctrlControlerKanataProductConfirmation.DataBind();
            bool accountUpdated = this.ctrlControlerKanataProductConfirmation.UpdateAccountInfoForPrizeOrder();
            if (accountUpdated)
                this.SetValueAccountCampaignOrder();
            this.ctrlControlerKanataProductConfirmation.SetInitialShipToInfo(true, accountUpdated);
			this.ctrlControlerKanataProductConfirmation.SetInitialBillToInfo(true);
			this.ctrlControlerKanataProductConfirmation.SetDDLOrderQualifier(true);
			this.ctrlControlerKanataProductConfirmation.SetInitialDeliveryDate(true);

			this.lblInstructions.Text = "Please enter the shipment information, verify the order, and select either Save or Submit to Warehouse.";	
			this.hypHelp.NavigateUrl = "javascript:window.open('../Common/Documentation/RapidOrderEntryManual.doc#SelectShipping',null, 'status=no, location=no, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";
		}

		private void SetValueStepEditOrder() 
		{
			ShowStep1 = false;
			ShowStep3 = false;

			this.CurrentStep = ProductReplacementStep.SelectProducts;

			this.ctrlControlerCampaignsForKanataOrder.Visible = false;
			this.ctrlControlerProductMultiSelectForKanata.Visible = true;
			this.ctrlControlerKanataProductConfirmation.Visible = false;
			this.ctrlControlerKanataProductView.Visible = false;
							
			this.ctrlControlerProductMultiSelectForKanata.BtnBackTop = false;
			this.ctrlControlerProductMultiSelectForKanata.BtnBackBottom = false;
			this.ctrlControlerProductMultiSelectForKanata.CurrentOrderHeader = this.CurrentBatch.OrderHeaders[0];
			this.ctrlControlerProductMultiSelectForKanata.InitProductSelect();
			this.ctrlControlerProductMultiSelectForKanata.DataBind();

			SetValueAccountCampaignOrder();
			
			this.ctrlControlerProductMultiSelectForKanata.DDLCatalogType = CurrentCatalog;

			lblPageTitle.Text = "Rapid Order Entry - Edit Order";

			this.lblInstructions.Text = "Please select the product(s) you wish to order.";
			this.hypHelp.NavigateUrl = "javascript:window.open('../Common/Documentation/RapidOrderEntryManual.doc#ModifyOrder',null, 'status=no, location=no, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";

		}

		private void SetValueStepViewOrder() 
		{
			ShowStep1 = false;
			ShowStep3 = true;
			ShowDefaultSearch = true;
			lblPageTitle.Text = "Rapid Order Entry - View Order";
	
			this.CurrentStep = ProductReplacementStep.ConfirmOrder;
						
			this.ctrlControlerCampaignsForKanataOrder.Visible = false;
			this.ctrlControlerKanataProductConfirmation.Visible = false;
			this.ctrlControlerProductMultiSelectForKanata.Visible = false;

			this.ctrlControlerKanataProductView.AddressValidateBtnEnabled = false;
			this.ctrlControlerKanataProductView.BtnBackTop = false;
			this.ctrlControlerKanataProductView.BtnBackBottom = false;
			this.ctrlControlerKanataProductView.Visible = true;
			this.ctrlControlerKanataProductView.CurrentBatch=this.CurrentBatch;
			this.ctrlControlerKanataProductView.CurrentOrderHeader = this.CurrentBatch.OrderHeaders[0];
			this.ctrlControlerKanataProductView.BtnConfirm = false;
			this.ctrlControlerKanataProductView.BtnSave = false;
			this.ctrlControlerKanataProductView.CurrentCatalog = CurrentCatalog;
			this.ctrlControlerKanataProductView.SetInitialShipToInfo(false, false);
			this.ctrlControlerKanataProductView.SetInitialBillToInfo(false);
			this.ctrlControlerKanataProductView.SetInitialDeliveryDate(false);
			this.ctrlControlerKanataProductView.DataBind();

			SetValueEmptyConfirmation();
			SetValueAccountCampaignOrder();

			this.ctrlControlerKanataProductView.SetDDLOrderQualifier(false);

			this.lblInstructions.Text = "This order has already been submitted. <a href=\"mailto:Ellie_Snow@rd.com, Craig_Rettinger@rd.com?subject=Order #" + this.CurrentBatch.OrderID + " Modification/Cancellation \">Click Here</a>" + " if you need to modify or cancel this order.";
			this.hypHelp.NavigateUrl = "javascript:window.open('../Common/Documentation/RapidOrderEntryManual.doc#OrderHistorySubmittedOrder',null, 'status=no, location=no, toolbar=no, menubar=no, location=no, scrollbars=1, resizable=1'); void('')";

			this.Menu1.Visible = false;

			this.ctrlControlerKanataProductView.HypPrintVisible = true;
		}
				
		private void SetValueEmptyConfirmation() 
		{
			this.lblConfirmation.Visible = false;
		}

		private void ctrlControlerKanataProductConfirmation_SaveClicked(object sender, EventArgs e)
		{
			SaveOrder(true);
		}

		private void ctrlControlerKanataProductConfirmation_ConfirmedClicked(object sender, EventArgs e)
		{
			SaveOrder(false);
		}

		private void SaveOrder(bool bSaveOnly)
		{
			int retVal = SaveCurrentBatch(bSaveOnly);
			if (retVal > 0)
			{
				SetValueConfirmation(bSaveOnly, retVal);

				this.ctrlControlerKanataProductConfirmation.InitField();
				StartNewBatch();
				SetValueStepSelectCampaign();
			}
		}

		private int SaveCurrentBatch(bool bSaveOnly) 
		{
			//Depending on Order Qualifier, set the Price override
            if (this.CurrentBatch.OrderQualifierID == OrderQualifier.KanataPSolver || this.CurrentBatch.OrderQualifierID == OrderQualifier.BookProblemSolver)
				SetPriceOverrideAllItems(45005); //Replacement
			else
				SetPriceOverrideAllItems(45004); //None

			KanataOrderEntry kOE =(KanataOrderEntry)this.Page;
			int retVal = kOE.BusKanataOE.KanataOrderEntry(this.CurrentBatch,
											this.ctrlControlerKanataProductConfirmation.RblBillTo, 
											this.ctrlControlerKanataProductConfirmation.RblShipTo,
											this.ctrlControlerKanataProductConfirmation.FirstNameTbx,
											this.ctrlControlerKanataProductConfirmation.LastNameTbx,
											this.ctrlControlerKanataProductConfirmation.EmailTbx,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.Address1,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.Address2,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.County,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.City,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.StateProvince,  
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.PostalCode,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.PostalCode2,
											this.ctrlControlerKanataProductConfirmation.AddressCtrl.Country,
											QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance,
											bSaveOnly);				

		
			if (retVal == 1) //above threshold, order has been set to in process, must now email HO
			{
				SendMail(this.CurrentBatch.OrderID, QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FullName);
			}
			
			return retVal;
		}

		private void SetPriceOverrideAllItems(int priceOverride)
		{
			foreach (ProductItem productItem in this.CurrentBatch.OrderHeaders[0].ProductItems)
			{
				productItem.PriceOverrideReason = priceOverride;
			}
		}

		private void SetValueConfirmation(bool saveOnly, int overThreshold) 
		{
			//string ordertype;
			string creation;

			if(this.CurrentBatch != null) 
			{
				/*if (this.CurrentBatch.Campaign.IsFMAccount==1)
				{
					ordertype = "FM";
				}
				else
				{
					ordertype = "Group";
				}*/

				if (saveOnly)
				{
					creation = "has been saved for future modification.";
				}
				else
				{
					if (overThreshold == 1)
					{
						creation = "has been flagged. Home Office will approve or disapprove the order.";
					}
					else
					{
						creation = "has been sent to the warehouse for fulfillment. <a href=KanataOrderEntry.aspx?OrderID=" + this.CurrentBatch.OrderID.ToString() + ">Click Here to View/Print Order.";
					}
				}

				this.lblConfirmation.Text =  " Kanata order "+ this.CurrentBatch.OrderID.ToString() + " for " + AccountName + " (Campaign: " + this.CurrentBatch.Campaign.CampaignID.ToString() + ") " + creation;
				this.lblConfirmation.Visible = true;
			}
			
			else 
			{
				SetValueEmptyConfirmation();
			}
		}

		private void SetValueAccountCampaignOrder()
		{
			if(this.CurrentBatch != null)
			{
				string orderInfo = "";
				if (this.CurrentBatch.OrderID > 0)
					orderInfo = "\t Order: " + this.CurrentBatch.OrderID;

				if (this.CurrentBatch.Campaign.CampaignID > 0)
				{
					DataTable c = new DataTable();
					CampaignData cData = new CampaignData();
					cData.SelectOne(c, this.CurrentBatch.Campaign.CampaignID);
					this.CurrentBatch.Campaign.EstimatedGrossSales = Convert.ToDouble(c.Rows[0][CampaignTable.FLD_ESTIMATEDGROSS].ToString());
                    this.CurrentBatch.Campaign.IncentivesBillToID = Convert.ToInt32(c.Rows[0][CampaignTable.FLD_INCENTIVESBILLTOID].ToString());
				}
				
				this.lblAccountCampaignInfo.Text = "Account: " + AccountName + "\t Campaign: " + this.CurrentBatch.Campaign.CampaignID + orderInfo + "\t Estimated Gross Sales: $" + this.CurrentBatch.Campaign.EstimatedGrossSales;
				this.lblAccountCampaignInfo.Visible = true;
			}
			else
			{
				this.lblAccountCampaignInfo.Visible = false;
			}
		}

		private void SendMail(int orderID, string userName)
		{
			MailMessage Message = new MailMessage();

			Message.From = "QSP_Fulfillment@rd.com";
			
			Message.BodyFormat = MailFormat.Html;
			
			Message.To = "carmine_moscardini@rd.com, michelle_staniforth@rd.com";
			
			Message.Cc = "ellie_snow@rd.com";

			Message.Bcc = "jeff_miles@rd.com";

			Message.Subject = "Kanata Order Over Threshold";

			StringBuilder SB = new StringBuilder();
			CreateBody(orderID,SB, userName);
			Message.Body = SB.ToString();

			SmtpMail.SmtpServer = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebSmtp;
			try
			{
				System.Web.Mail.SmtpMail.Send(Message);
			}
			catch(Exception ex)
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
			}
		}

		private void CreateBody(int orderID, StringBuilder SB, string userName)
		{
			SB.Append("The following Kanata order has exceeded the allowed threshold. Please approve or disapprove this order in the QSP Fulfillment System. <br><br>");
			SB.Append("Order ID: " + Convert.ToString(orderID) + "<br>");
			SB.Append("Account Name: " + AccountName + "<br>");
			SB.Append("Account ID: " + this.CurrentBatch.Campaign.AccountID + "<br>");
			SB.Append("Campaign ID: " + this.CurrentBatch.Campaign.CampaignID + "<br>");
			SB.Append("User: " + userName + "<br>");
			foreach (ProductItem item in this.CurrentBatch.OrderHeaders[0].ProductItems)
			{
				SB.Append("Item: " + item.Product_sort_name + "  Quantity: " + item.Quantity + "<br>");
			}
			SB.Append("Total Quantity: " + this.CurrentBatch.OrderHeaders[0].ProductItems.GetTotalQuantityofProducts());
		}

		private string CurrentCatalog
		{
			get
			{
				if (currentCatalog == null)
				{
					//only look at an item that is not soft deleted in DB
					ProductItemCollectionFilter productItemCollectionFilter = new ProductItemCollectionFilter();
					ProductItemCollection productItemCollectionFiltered;
					productItemCollectionFiltered = productItemCollectionFilter.Filter(this.CurrentBatch.OrderHeaders[0].ProductItems, 1);
					ProductItem productItem = new ProductItem();
					productItem = productItemCollectionFiltered[0];

					DataTable productContractTable = new DataTable();
					ProductContractData productContractData = new ProductContractData();
					productContractData.SelectOneSingle(productContractTable, productItem.MagPrice_instance);
					int programSectionID = Convert.ToInt32(productContractTable.Rows[0]["ProgramSectionID"].ToString());

					DataTable catalogTable = new DataTable();
					CatalogData catalogData = new CatalogData();
					catalogData.CatalogSelectByCatalogSectionID(catalogTable, programSectionID);
					currentCatalog = catalogTable.Rows[0]["Program_Type"].ToString();
				}

				return currentCatalog;
			}
		}
      private int CurrentCatalogType
      {
         get
         {
            if (currentCatalogType == null || currentCatalogType == 0)
            {
               //only look at an item that is not soft deleted in DB
               ProductItemCollectionFilter productItemCollectionFilter = new ProductItemCollectionFilter();
               ProductItemCollection productItemCollectionFiltered;
               productItemCollectionFiltered = productItemCollectionFilter.Filter(this.CurrentBatch.OrderHeaders[0].ProductItems, 1);
               ProductItem productItem = new ProductItem();
               productItem = productItemCollectionFiltered[0];

               DataTable productContractTable = new DataTable();
               ProductContractData productContractData = new ProductContractData();
               productContractData.SelectOneSingle(productContractTable, productItem.MagPrice_instance);
               int programSectionID = Convert.ToInt32(productContractTable.Rows[0]["ProgramSectionID"].ToString());

               DataTable catalogTable = new DataTable();
               CatalogData catalogData = new CatalogData();
               catalogData.CatalogSelectByCatalogSectionID(catalogTable, programSectionID);
               currentCatalogType = Convert.ToInt32(catalogTable.Rows[0]["SubType"].ToString());
            }

            return currentCatalogType;
         }
      }
   }
}
