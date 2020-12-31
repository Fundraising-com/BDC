using System;
using System.Web.UI;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.ActionObject;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using QSPFulfillment.DataAccess.Business;
using System.Security.Permissions;
using Business.ReportExecution;

namespace QSPFulfillment.MarketingMgt

{
	[Serializable]
	public enum Step 
	{
		[Description("Select Catalog")]
		SelectCatalog = 0,
		[Description("Catalog Informations Maintenance")]
		CatalogInformations = 1,
		[Description("Catalog Sections Maintenance")]
		CatalogSections = 2,
		[Description("Include Products")]
		IncludeProducts = 3
	}

	/// <summary>
	/// Summary description for MarketingMgtPage.
	/// </summary>
	/// 
	//[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "CustomerService")]
	public class MarketingMgtPage: QSPFulfillment.CommonWeb.QSPPage
	{
		private const string CURRENTINFOSESSION = "CurrentInfoSession";
		private const string SESSION_USER_ID = "current_user_id";
		private const string SESSION_INCIDENTID= "session_incident_id";
		public const string DROPDOWNLIST_BLANK_ENTRY = "Please select...";
		private Message messageManager = new Message(true);
		private ProductBusiness bProductBusiness;
		private QSPFulfillment.DataAccess.Business.ActionBusiness bActionBusiness;
		private QSPFulfillment.DataAccess.Business.AdPageSizeBusiness bAdPageSizeBusiness;
		private QSPFulfillment.DataAccess.Business.CampaignProgramBusiness bCampaignProgramBusiness;
		private QSPFulfillment.DataAccess.Business.CatalogBusiness bCatalogBusiness;
		private QSPFulfillment.DataAccess.Business.CatalogSectionBusiness bCatalogSectionBusiness;
		private QSPFulfillment.DataAccess.Business.FulfillmentHouseBusiness bFulfillmentHouseBusiness;
		private QSPFulfillment.DataAccess.Business.FulfillmentHouseContactBusiness bFulfillmentHouseContactBusiness;
		private QSPFulfillment.DataAccess.Business.FulfillmentHouseContactProductBusiness bFulfillmentHouseContactProductBusiness;
		private QSPFulfillment.DataAccess.Business.PublisherBusiness bPublisherBusiness;
		private QSPFulfillment.DataAccess.Business.PublisherContactBusiness bPublisherContactBusiness;
		private QSPFulfillment.DataAccess.Business.PublisherContactProductBusiness bPublisherContactProductBusiness;
		private QSPFulfillment.DataAccess.Business.CodeDetailBusiness bCodeDetailBusiness;
		private QSPFulfillment.DataAccess.Business.CommunicationChannelBusiness bCommunicationChannelBusiness;
		private QSPFulfillment.DataAccess.Business.CommunicationSourceBusiness bCommunicationSourceBusiness;
		private QSPFulfillment.DataAccess.Business.CustomerOrderDetailBusiness bCustomerOrderDetailBusiness;
		private QSPFulfillment.DataAccess.Business.CustomerOrderDetailRemitHistoryBusiness bCustomerOrderDetailRemitHistoryBusiness;
		private QSPFulfillment.DataAccess.Business.CustomerOrderHeaderBusiness bCustomerOrderHeaderBusiness;
		//private QSPFulfillment.DataAccess.Business.GiftCardOutputBusiness bGiftCardOutputBusiness;
		//private QSPFulfillment.DataAccess.Business.GiftCardRemitBatchBusiness bGiftCardRemitBatchBusiness;
		private QSPFulfillment.DataAccess.Business.IncidentActionBusiness bIncidentActionBusiness;
		private QSPFulfillment.DataAccess.Business.IncidentBusiness bIncidentBusiness;
		private QSPFulfillment.DataAccess.Business.ListingLevelBusiness bListingLevelBusiness;
		private QSPFulfillment.DataAccess.Business.ProblemCodeBusiness bProblemCodeBusiness;
		private QSPFulfillment.DataAccess.Business.ProductTypeBusiness bProductTypeBusiness;
		private QSPFulfillment.DataAccess.Business.ProgramBusiness bProgramBusiness;
		private QSPFulfillment.DataAccess.Business.SearchBusiness bSearchBusiness;
		private QSPFulfillment.DataAccess.Business.ShipmentOrderBusiness bShipmentOrderBusiness;
		private QSPFulfillment.DataAccess.Business.SwitchLetterBatchBusiness bSwitchLetterBatchBusiness;
		private QSPFulfillment.DataAccess.Business.TaxRegionBusiness bTaxRegionBusiness;
		private QSPFulfillment.DataAccess.Business.IncidentStatusBusiness bIncidentStatusBusiness;
		private QSPFulfillment.DataAccess.Business.AccountBusiness bAccountBusiness;
		private QSPFulfillment.DataAccess.Business.INVOICEBusiness bINVOICEBusiness;
		private QSPFulfillment.DataAccess.Business.PAYMENTBusiness bPAYMENTBusiness;
		private QSPFulfillment.DataAccess.Business.CustomerBusiness bCustomerBusiness;
		private QSPFulfillment.DataAccess.Business.CouponBusiness bCouponBusiness;
		private QSPFulfillment.DataAccess.Business.LeadBusiness bLeadBusiness;
		private QSPFulfillment.DataAccess.Business.CreditCardBusiness bCreditCardBusiness;
		public MarketingMgtPage()
		{
			
			
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack && this.CompletedCatalog != "") 
			{
				this.CurrentStep = Step.SelectCatalog;
			}
		}
		private void Page_PreRender(object sender, EventArgs e)
		{
			AddJavaScript();
		}
		/*private void Page_Error(object sender, EventArgs e)
		{	
			Exception ex = Server.GetLastError();
			if(ex is QSPFulfillment.DataAccess.Common.ExceptionFulf)
			{
				SetPageError((QSPFulfillment.DataAccess.Common.ExceptionFulf)ex);
				Server.ClearError();
			}
		}*/

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			
				InitializeComponent();
				base.OnInit(e);							
		}
		
		

		
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine("<script id=clientEventHandlersJS language=javascript>");
			writer.WriteLine("<!--");
			writer.WriteLine("var span,s;\n");
			writer.WriteLine("s ='';");
			writer.WriteLine("function window_onunload() ");
			writer.WriteLine("{");            
			writer.WriteLine("if(s !='')");
            writer.WriteLine("{");
            writer.WriteLine("window.scrollTo(0, 0);");
            writer.WriteLine("s = encodeURI(s);");            
			writer.WriteLine("openErrorWindow('/QSPFulfillment/customerservice/showerrorspage.aspx?Message='+s);");
            writer.WriteLine("}");
            writer.WriteLine("}");
			writer.WriteLine("//-->");
			writer.WriteLine("</script>");
			base.RenderChildren (writer);
		}
		public override void RegisterClientScriptBlock(string key, string script)
		{
			
			if(key == "ValidatorIncludeScript")
			{
				script = "\r\n<script language=\"javascript\" src=\"/QSPFulfillment/CustomerService/CSWebUIValidation.js\"></script>";
						
			}
			base.RegisterClientScriptBlock (key, script);
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			//this.Page.Error +=new EventHandler(Page_Error);			
			this.Page.Load +=new EventHandler(Page_Load);
			this.Page.PreRender +=new EventHandler(Page_PreRender);
		}
		#endregion

		public void SetPageError(ExceptionFulf ex)
		{
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(ex.HTMLMessage));
		}
		public void SetPageError()
		{
			this.messageManager.PrepareErrorMessage();
			this.Page.RegisterClientScriptBlock("ErrorMessage",GetScriptError(this.messageManager.ErrorHTMLMessage));
		}
		
		
		public string UserID
		{	
			
			get
			{   
				return QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance.ToString();
			}
			
		}
		
		private string GetScriptError(string ErrorMessage)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("<script language=\"javascript\">\n");
			
			sb.Append("s=\""+ErrorMessage+"\";\n");
						
			sb.Append("</script>\n");
			return sb.ToString();


		}
		
		public Message MessageManager
		{
			get
			{
				

				return messageManager;
			}
		
		}

		public bool CreateNew 
		{
			get 
			{
				return Convert.ToBoolean(Request.QueryString["CreateNew"]);
			}
		}

		public string CompletedCatalog
		{
			get 
			{
				if(Request.QueryString["CompletedCatalog"] == null)
					return "";

				return Request.QueryString["CompletedCatalog"].ToString();
			}
		}

			/*public Step CurrentStep 
			{
				get 
				{
					if(this.ViewState["CurrentStep"] == null)
					{
						if(this.CreateNew) 
						{
							this.ViewState["CurrentStep"] = Step.CatalogInformations.ToString();
						} 
						else 
						{
							this.ViewState["CurrentStep"] = Step.SelectCatalog.ToString();
						}
					}

					return (Step) Enum.Parse(typeof(Step), this.ViewState["CurrentStep"].ToString());
				}
				set 
				{
					this.ViewState["CurrentStep"] = value.ToString();
				}
			}*/

		public Step CurrentStep 
		{
			get 
			{
				if(ViewState["CurrentStep"] == null)
				{
					if(this.CreateNew) 
					{
						ViewState["CurrentStep"] = Step.CatalogInformations;
					} 
					else 
					{
						ViewState["CurrentStep"] = Step.SelectCatalog;
					}
				}

				return (Step) ViewState["CurrentStep"];
			}
			set 
			{
				ViewState["CurrentStep"] = value;
			}
		}

		public Catalog CatalogInfo 
		{
			get 
			{
				return (Catalog) Session["CatalogInfo"];
			}
			set 
			{
				Session["CatalogInfo"] = value;
			}
		}

		public CatalogSection CatalogSectionInfo 
		{
			get 
			{
				return (CatalogSection) Session["CatalogSectionInfo"];
			}
			set 
			{
				Session["CatalogSectionInfo"] = value;
			}
		}
		
		public bool NewSearch
		{
			get
			{
				if(ViewState["NewSearch"] == null)
					return false;

				return Convert.ToBoolean(ViewState["NewSearch"]);
			}
			set{ViewState["NewSearch"] = value;}
		}
		
		public bool PageChanged 
		{
			get 
			{
				if(ViewState["PageChanged"] == null) 
				{
					return false;
				} 
				else 
				{
					return Convert.ToBoolean(ViewState["PageChanged"]);
				}
			}
			set 
			{
				ViewState["PageChanged"] = value;
			}
		}

		public bool NestedPageChanged
		{
			get 
			{
				if(ViewState["NestedPageChanged"] == null)
					return false;

				return Convert.ToBoolean(ViewState["NestedPageChanged"]);
			}
			set 
			{
				ViewState["NestedPageChanged"] = value;
			}
		}
		
		public int PageIndexSubNested
		{
			get
			{
					if(ViewState["PageIndexSubNested"] == null)
					return 0;
				return (int)ViewState["PageIndexSubNested"];
			}
			set
			{
				ViewState["PageIndexSubNested"] = value;
			}
		}

		protected virtual void AddJavaScript()
		{
		
		}
		#region business

		public AdPageSizeBusiness BusAdPageSize 
		{
			get 
			{
				if(bAdPageSizeBusiness == null) 
				{
					bAdPageSizeBusiness = new AdPageSizeBusiness(messageManager);
				}

				return bAdPageSizeBusiness;
			}
		}

		public ProductBusiness BusProduct
		{
			get
			{
				if(bProductBusiness == null)
					bProductBusiness = new ProductBusiness(messageManager);

				return bProductBusiness;
			}
				
		}

		public CatalogBusiness BusCatalog
		{
			get
			{
				if(bCatalogBusiness == null)
					bCatalogBusiness = new CatalogBusiness(messageManager);

				return bCatalogBusiness;
			}
		}

		public CatalogSectionBusiness BusCatalogSection 
		{
			get 
			{
				if(bCatalogSectionBusiness == null)
					bCatalogSectionBusiness = new CatalogSectionBusiness(messageManager);

				return bCatalogSectionBusiness;
			}
		}

		public FulfillmentHouseBusiness BusFulfillmentHouse 
		{
			get 
			{
				if(bFulfillmentHouseBusiness == null) 
				{
					bFulfillmentHouseBusiness = new FulfillmentHouseBusiness(messageManager);
				}

				return bFulfillmentHouseBusiness;
			}
		}

		public FulfillmentHouseContactBusiness BusFulfillmentHouseContact 
		{
			get 
			{
				if(bFulfillmentHouseContactBusiness == null) 
				{
					bFulfillmentHouseContactBusiness = new FulfillmentHouseContactBusiness(messageManager);
				}

				return bFulfillmentHouseContactBusiness;
			}
		}

		public FulfillmentHouseContactProductBusiness BusFulfillmentHouseContactProduct
		{
			get
			{
				if(bFulfillmentHouseContactProductBusiness == null)
					bFulfillmentHouseContactProductBusiness = new FulfillmentHouseContactProductBusiness(messageManager);

				return bFulfillmentHouseContactProductBusiness;
			}
		}

		public ListingLevelBusiness BusListingLevel 
		{
			get 
			{
				if(bListingLevelBusiness == null) 
				{
					bListingLevelBusiness = new ListingLevelBusiness(messageManager);
				}

				return bListingLevelBusiness;
			}
		}

		public PublisherBusiness BusPublisher
		{
			get
			{
				if(bPublisherBusiness == null) 
				{
					bPublisherBusiness = new PublisherBusiness(messageManager);
				}

				return bPublisherBusiness;
			}
		}

		public PublisherContactBusiness BusPublisherContact 
		{
			get 
			{
				if(bPublisherContactBusiness == null) 
				{
					bPublisherContactBusiness = new PublisherContactBusiness(messageManager);
				}

				return bPublisherContactBusiness;
			}
		}

		public PublisherContactProductBusiness BusPublisherContactProduct 
		{
			get 
			{
				if(bPublisherContactProductBusiness == null) 
				{
					bPublisherContactProductBusiness = new PublisherContactProductBusiness(messageManager);
				}

				return bPublisherContactProductBusiness;
			}
		}

		public ProblemCodeBusiness BusProblemCode
		{
			get
			{
				if(bProblemCodeBusiness == null)
					bProblemCodeBusiness = new ProblemCodeBusiness(messageManager);

				return bProblemCodeBusiness;
			}
				
		}

		public ProductTypeBusiness BusProductType 
		{
			get 
			{
				if(bProductTypeBusiness == null) 
				{
					bProductTypeBusiness = new ProductTypeBusiness(messageManager);
				}

				return bProductTypeBusiness;
			}
		}

		public ProgramBusiness BusProgram 
		{
			get 
			{
				if(bProgramBusiness == null)
				{
					bProgramBusiness = new ProgramBusiness(messageManager);
				}

				return bProgramBusiness;
			}
		}

		public SearchBusiness BusSearch
		{
			get
			{
				if(bSearchBusiness == null)
					bSearchBusiness = new SearchBusiness(messageManager);

				return bSearchBusiness;
			}
				
		}
		public ShipmentOrderBusiness BusShipmentOrder
		{
			get
			{
				if(bShipmentOrderBusiness == null)
					bShipmentOrderBusiness = new ShipmentOrderBusiness(messageManager);

				return bShipmentOrderBusiness;
			}
				
		}
		public SwitchLetterBatchBusiness BusSwitchLetterBatch
		{
			get
			{
				if(bSwitchLetterBatchBusiness == null)
					bSwitchLetterBatchBusiness = new SwitchLetterBatchBusiness(messageManager);

				return bSwitchLetterBatchBusiness;
			}
				
		}

		public TaxRegionBusiness BusTaxRegion 
		{
			get 
			{
				if(bTaxRegionBusiness == null) 
				{
					bTaxRegionBusiness = new TaxRegionBusiness(messageManager);
				}

				return bTaxRegionBusiness;
			}
		}

		public CampaignProgramBusiness BusCampaignProgram
		{
			get
			{
				if(bCampaignProgramBusiness == null)
					bCampaignProgramBusiness = new CampaignProgramBusiness(messageManager);

				return bCampaignProgramBusiness;
			}
				
		}
		public CodeDetailBusiness BusCodeDetail
		{
			get
			{
				if(bCodeDetailBusiness == null)
					bCodeDetailBusiness = new CodeDetailBusiness(messageManager);

				return bCodeDetailBusiness;
			}
				
		}
		public CommunicationChannelBusiness BusCommunicationChannel
		{
			get
			{
				if(bCommunicationChannelBusiness == null)
					bCommunicationChannelBusiness = new CommunicationChannelBusiness(messageManager);

				return bCommunicationChannelBusiness;
			}
				
		}
		public CommunicationSourceBusiness BusCommunicationSource
		{
			get
			{
				if(bCommunicationSourceBusiness == null)
					bCommunicationSourceBusiness = new CommunicationSourceBusiness(messageManager);

				return bCommunicationSourceBusiness;
			}
				
		}
		public CreditCardBusiness BusCreditCard
		{
			get 
			{
				if(bCreditCardBusiness == null)
					bCreditCardBusiness = new CreditCardBusiness(messageManager);

				return bCreditCardBusiness;
			}
		}
			public CustomerOrderDetailBusiness BusCustomerOrderDetail
		{
			get
			{
				if(bCustomerOrderDetailBusiness == null)
					bCustomerOrderDetailBusiness = new CustomerOrderDetailBusiness(messageManager);

				return bCustomerOrderDetailBusiness;
			}
				
		}
		public CustomerOrderDetailRemitHistoryBusiness BusCustomerOrderDetailRemitHistory
		{
			get
			{
				if(bCustomerOrderDetailRemitHistoryBusiness == null)
					bCustomerOrderDetailRemitHistoryBusiness = new CustomerOrderDetailRemitHistoryBusiness(messageManager);

				return bCustomerOrderDetailRemitHistoryBusiness;
			}
				
		}
		public CustomerOrderHeaderBusiness BusCustomerOrderHeader
		{
			get
			{
				if(bCustomerOrderHeaderBusiness == null)
					bCustomerOrderHeaderBusiness = new CustomerOrderHeaderBusiness(messageManager);

				return bCustomerOrderHeaderBusiness;
			}
				
		}
		public IncidentBusiness BusIncident
		{
			get
			{
				if(bIncidentBusiness == null)
					bIncidentBusiness = new IncidentBusiness(messageManager);

				return bIncidentBusiness;
			}
				
		}
		public IncidentActionBusiness BusIncidentAction
		{
			get
			{
				if(bIncidentActionBusiness == null)
					bIncidentActionBusiness = new IncidentActionBusiness(messageManager);

				return bIncidentActionBusiness;
			}
				
		}
		public IncidentStatusBusiness BusIncidentStatus
		{
			get
			{
				if(bIncidentStatusBusiness == null)
					bIncidentStatusBusiness = new IncidentStatusBusiness(messageManager);

				return bIncidentStatusBusiness;
			}
				
		}
		public ActionBusiness BusAction
		{
			get
			{
				if(bActionBusiness == null)
					bActionBusiness = new ActionBusiness(messageManager);

				return bActionBusiness;
			}
				
		}
		public AccountBusiness BusAccount
		{
			get
			{
				if(bAccountBusiness == null)
					bAccountBusiness = new AccountBusiness(messageManager);

				return bAccountBusiness;
			}
				
		}
		public INVOICEBusiness BusInvoice
		{
			get
			{
				if(bINVOICEBusiness == null)
					bINVOICEBusiness = new INVOICEBusiness(messageManager);

				return bINVOICEBusiness;
			}
				
		}
		public PAYMENTBusiness BusPayment
		{
			get
			{
				if(bPAYMENTBusiness == null)
					bPAYMENTBusiness = new PAYMENTBusiness(messageManager);

				return bPAYMENTBusiness;
			}
				
		}
		public CustomerBusiness BusCustomer
		{
			get
			{
				if(bCustomerBusiness == null)
					bCustomerBusiness = new CustomerBusiness(messageManager);

				return bCustomerBusiness;
			}
				
		}

		public CouponBusiness BusCoupon
		{
			get
			{
				if(bCouponBusiness == null)
					bCouponBusiness = new CouponBusiness(this.MessageManager);

				return bCouponBusiness;
			}
				
		}
		
			public LeadBusiness BusLead
			{
				get
				{
					if(bLeadBusiness == null)
						bLeadBusiness = new LeadBusiness(messageManager);

					return bLeadBusiness;
				}
				
			}
		#endregion

		protected ParameterValue[] GetInitParameterValue(ReportParameter[] ReportValues)
		{
			int i = 0;
			ParameterValue[] Values = new ParameterValue[ReportValues.Length];
			
			foreach(ReportParameter rp in ReportValues)
			{
				Values[i] = new ParameterValue();
				Values[i].Name = rp.Name;

				/*if(rp.DefaultValues.Length > 0)
					Values[i].Value = rp.DefaultValues[0];*/

				i++;	
			}
			return Values;
		
		}
	
		public object ObjectCopy(object obj)
		{
			//copies original object to stream then 
			//deserializes that stream and returns the output
			//to create clone (copy) of object

			object objectCopy;

			MemoryStream objMemStream = new MemoryStream(5000);
			BinaryFormatter objBinaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));

			objBinaryFormatter.Serialize(objMemStream, obj);

			objMemStream.Seek(0, SeekOrigin.Begin);

			objectCopy = objBinaryFormatter.Deserialize(objMemStream);

			objMemStream.Close();

			return objectCopy;
		}

		public override void ManageError(Exception ex) 
		{
			if(ex is ThreadAbortException) 
			{
				throw ex;
			}

			ExceptionFulf exceptionFulf = ex as ExceptionFulf;

			if(exceptionFulf == null)
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);

				exceptionFulf = new ExceptionFulf(Message.ERRMSG_SYSTEM_VAR_0, Message.ERRMSG_SYSTEM_VAR_0);
			}

			this.SetPageError(exceptionFulf);
		}
	}
}