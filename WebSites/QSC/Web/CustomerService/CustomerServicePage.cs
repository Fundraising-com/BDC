using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using QSPFulfillment.DataAccess.Common;
using System.Data;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Business;
using System.Security.Permissions;
using Business.ReportExecution;
using Business.Objects;

namespace QSPFulfillment.CustomerService

{
	/// <summary>
	/// Summary description for CustomerServicePage.
	/// </summary>
	/// 
	//[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "CustomerService")]
	public class CustomerServicePage: QSPFulfillment.CommonWeb.QSPPage
	{
		private const string CURRENTINFOSESSION = "CurrentInfoSession";
		private const string SESSION_USER_ID = "current_user_id";
		private const string SESSION_INCIDENTID= "session_incident_id";
		private Message messageManager = new Message(true);
		public event SelectResultEventHandler SelectResultClicked;
		private ProductBusiness bProductBusiness;
		private QSPFulfillment.DataAccess.Business.ActionBusiness bActionBusiness;
		private QSPFulfillment.DataAccess.Business.CampaignProgramBusiness bCampaignProgramBusiness;
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
		private QSPFulfillment.DataAccess.Business.ProblemCodeBusiness bProblemCodeBusiness;
		private QSPFulfillment.DataAccess.Business.KanataOEBusiness bKanataOEBusiness;
		private QSPFulfillment.DataAccess.Business.SearchBusiness bSearchBusiness;
		private QSPFulfillment.DataAccess.Business.ShipmentOrderBusiness bShipmentOrderBusiness;
		private InactiveMagazineLetterBatch bSwitchLetterBatchBusiness;
		private QSPFulfillment.DataAccess.Business.IncidentStatusBusiness bIncidentStatusBusiness;
		private QSPFulfillment.DataAccess.Business.AccountBusiness bAccountBusiness;
		private QSPFulfillment.DataAccess.Business.INVOICEBusiness bINVOICEBusiness;
		private QSPFulfillment.DataAccess.Business.PAYMENTBusiness bPAYMENTBusiness;
		private QSPFulfillment.DataAccess.Business.CustomerBusiness bCustomerBusiness;
		private QSPFulfillment.DataAccess.Business.LeadBusiness bLeadBusiness;
		private QSPFulfillment.DataAccess.Business.CreditCardBusiness bCreditCardBusiness;
		private QSPFulfillment.DataAccess.Business.ReportRequestBatch_OrderEntryFollowupReportBusiness bReportRequestBatch_OrderEntryFollowupReportBusiness;

		public CustomerServicePage()
		{
			
			
		}
		private void Page_Load(object sender, EventArgs e)
		{
			
			if(FirstTimeCustomerService)
			{
				ShowStep1 = true;
				ShowStep3 = false;
				ShowDefaultSearch = true;
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
			try 
			{
				InitializeComponent();
				base.OnInit(e);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
		
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			try 
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
			catch(Exception ex) 
			{
				ManageError(ex);
			}
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

		public void SetPageError(Common.MessageException ex)
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
		
		public CurrentOrderInfo OrderInfo
		{
			get
			{
				return (CurrentOrderInfo) ViewState[CURRENTINFOSESSION];
				
			}
			set
			{
				ViewState[CURRENTINFOSESSION]= value;
			}
		}

		public string StudentName 
		{
			get 
			{
				if(ViewState["StudentName"] == null)
					ViewState["StudentName"] = "";

				return ViewState["StudentName"].ToString();
			}
			set 
			{
				ViewState["StudentName"] = value;
			}
		}

		public string RecipientName 
		{
			get 
			{
				if(ViewState["RecipientName"] == null)
					ViewState["RecipientName"] = "";

				return ViewState["RecipientName"].ToString();
			}
			set 
			{
				ViewState["RecipientName"] = value;
			}
		}

		public QSPFulfillment.DataAccess.Common.ActionObject.Customer CustomerInfo
		{
			get
			{
				if(ViewState["CustomerInfo"] == null)
					ViewState["CustomerInfo"]= new QSPFulfillment.DataAccess.Common.ActionObject.Customer();
					

				return (QSPFulfillment.DataAccess.Common.ActionObject.Customer) ViewState["CustomerInfo"];
			
			}
			set
			{
				ViewState["CustomerInfo"] = value;
			}
		}
		public int  IncidentID
		{
			get
			{
				if(ViewState[SESSION_INCIDENTID] == null)
					return -1;

				return (int)ViewState[SESSION_INCIDENTID];
			}
			set
			{
				ViewState[SESSION_INCIDENTID]= value;
			}
		}

		public int ProblemCode 
		{
			get 
			{
				int problemCode = 0;

				if(ViewState["ProblemCode"] != null) 
				{
					problemCode = Convert.ToInt32(ViewState["ProblemCode"]);
				}

				return problemCode;
			}
			set 
			{
				ViewState["ProblemCode"] = value;
			}
		}

		public int CommunicationChannelInstance 
		{
			get 
			{
				int communicationChannelInstance = 0;

				if(ViewState["CommunicationChannelInstance"] != null) 
				{
					communicationChannelInstance = Convert.ToInt32(ViewState["CommunicationChannelInstance"]);
				}

				return communicationChannelInstance;
			}
			set 
			{
				ViewState["CommunicationChannelInstance"] = value;
			}
		}

		public int CommunicationSourceInstance 
		{
			get 
			{
				int communicationSourceInstance = 0;

				if(ViewState["CommunicationSourceInstance"] != null) 
				{
					communicationSourceInstance = Convert.ToInt32(ViewState["CommunicationSourceInstance"]);
				}

				return communicationSourceInstance;
			}
			set 
			{
				ViewState["CommunicationSourceInstance"] = value;
			}
		}
		
		public bool ResultSelected
		{
			get
			{	
				if(ViewState["ActionCanBePermformed"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["ActionCanBePermformed"]);
			}
			set
			{
				this.ViewState["ActionCanBePermformed"] = value;
			}
		}
	
		public bool IsMagazine 
		{
			get 
			{
				if(ViewState["IsMagazine"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["IsMagazine"]);
			}
			set 
			{
				this.ViewState["IsMagazine"] = value;
			}
		}

		public virtual bool IsMagazineBeforeRemit
		{
			get 
			{
				if(ViewState["IsMagazineBeforeRemit"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["IsMagazineBeforeRemit"]);
			}
			set 
			{
				this.ViewState["IsMagazineBeforeRemit"] = value;
			}
		}

		public bool ActionReasonEntered
		{
			get
			{	
				if(ViewState["ActionReasonEntered"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["ActionReasonEntered"]);
			}
			set
			{
				this.ViewState["ActionReasonEntered"] = value;
			}
		}

		public void FireEventSelect(SelectResultClickedArgs e)
		{
			this.OrderInfo = e.OrderInfo;
		

			if(SelectResultClicked != null)
			{
				SelectResultClicked(this,e);
			}
		}
		private string GetScriptError(string ErrorMessage)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("<script language=\"javascript\">\n");

            sb.Append("s=encodeURI(\"" + ErrorMessage + "\");\n");
						
			sb.Append("</script>\n");
			return sb.ToString();


		}
		
		public bool ShowStep1
		{
			/*get
			{
				try
				{
					return Convert.ToBoolean(Context.Request.Cookies["step1_state"].Value);
				}
				catch
				{
					return true;
				}
			}*/
			set
			{
				try
				{
					if(value == false)
					{
						string ss = "<script language=javascript>communicationstep1=true; showstep1= false;</script>";
						this.RegisterClientScriptBlock("commication1",ss);
					
					} 
					else 
					{
					
						string ss = "<script language=javascript>communicationstep1=true; showstep1= true;</script>";
						this.RegisterClientScriptBlock("commication1",ss);
					}
				}
				catch
				{
				}
				
			}
		}
		
		public bool ShowStep3
		{
			/*get
			{
				try
				{
					return Convert.ToBoolean(Context.Request.Cookies["step3_state"].Value);
				}
				catch
				{
					return true;
				}
			}*/
			set
			{
				try
				{
					if(value == false)
					{
						string ss = "<script language=javascript>communicationstep3=true; showstep3= false;</script>";
						this.RegisterClientScriptBlock("commication3",ss);
					
					} 
					else 
					{
					
						string ss = "<script language=javascript>communicationstep3=true; showstep3= true;</script>";
						this.RegisterClientScriptBlock("commication3",ss);
					
					

				
					}
				}
				catch{}
			}
		
		}
		public bool ShowDefaultSearch
		{
			
			set
			{
				try
				{
					if(value == false)
					{
						string ss = "<script language=javascript>newsession=false;</script>";
						this.RegisterClientScriptBlock("ShowDefaultSearch",ss);
					
					} 
					else 
					{
					
						string ss = "<script language=javascript>newsession=true;</script>";
						this.RegisterClientScriptBlock("ShowDefaultSearch",ss);
				
					}
				}
				catch{}
			}
		
		}
		public Message MessageManager
		{
			get
			{
				

				return messageManager;
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

		public void AddPageIndexSubNested(string controlName, int pageIndex) 
		{
			ViewState["PageIndexSubNested" + controlName] = pageIndex;
		}

		public int GetPageIndexSubNested(string controlName) 
		{
			if(this.PageChanged || ViewState["PageIndexSubNested" + controlName] == null) 
			{
				return 0;
			} 
			else 
			{
				return Convert.ToInt32(ViewState["PageIndexSubNested" + controlName]);
			}
		}

		protected virtual void AddJavaScript()
		{
		
		}

		[Bindable(true),Category("Property"),DefaultValue("")]
		public SearchMultiPage ResultType
		{
			get
			{
				if(ViewState["EnumResult"]!= null)
					return (SearchMultiPage)ViewState["EnumResult"];
				

				return SearchMultiPage.None;
			}
			set
			{
				ViewState["EnumResult"]=value;
			}
		}
		protected bool FirstTimeCustomerService
		{
			get
			{
				if(ViewState["FistTimeCustomerService"] == null)
				{
					ViewState["FistTimeCustomerService"] = false;
					return true;
				}

				return false;
			}
		}

		#region business
		public ProductBusiness BusProduct
		{
			get
			{
				if(bProductBusiness == null)
					bProductBusiness = new ProductBusiness(messageManager);

				return bProductBusiness;
			}
				
		}
		public KanataOEBusiness KanataOEBusiness
		{
			get
			{
				if(bKanataOEBusiness == null)
					bKanataOEBusiness = new KanataOEBusiness(messageManager);

				return bKanataOEBusiness;
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
		public InactiveMagazineLetterBatch BusSwitchLetterBatch
		{
			get
			{
				if(bSwitchLetterBatchBusiness == null)
					bSwitchLetterBatchBusiness = new InactiveMagazineLetterBatch();

				return bSwitchLetterBatchBusiness;
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
		
			public LeadBusiness BusLead
			{
				get
				{
					if(bLeadBusiness == null)
						bLeadBusiness = new LeadBusiness(messageManager);

					return bLeadBusiness;
				}
				
			}

		public ReportRequestBatch_OrderEntryFollowupReportBusiness BusReportRequestBatch_OrderEntryFollowupReport
		{
			get 
			{
				if(bReportRequestBatch_OrderEntryFollowupReportBusiness == null)
					 bReportRequestBatch_OrderEntryFollowupReportBusiness = new ReportRequestBatch_OrderEntryFollowupReportBusiness(messageManager);

				return bReportRequestBatch_OrderEntryFollowupReportBusiness;
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

		public override void ManageError(Exception ex) 
		{
			if(ex is ThreadAbortException) 
			{
				throw ex;
			}

			Common.MessageException exMessage = ex as Common.MessageException;

			if(exMessage == null) 
			{
				ExceptionFulf exFulf = ex as ExceptionFulf;

				if(exFulf == null)
				{
					ApplicationError.ManageError(ex);

					exFulf = new ExceptionFulf(Message.ERRMSG_SYSTEM_VAR_0, Message.ERRMSG_SYSTEM_VAR_0);
				}

				this.SetPageError(exFulf);
			} 
			else 
			{
				this.SetPageError(exMessage);
			}
		}

		public override int SavePageSwitchState()
		{
			int pageSwitchStateID = base.SavePageSwitchState ();

			this.PageSwitchState[pageSwitchStateID][CURRENTINFOSESSION] = OrderInfo;
			this.PageSwitchState[pageSwitchStateID]["CustomerInfo"] = CustomerInfo;
			this.PageSwitchState[pageSwitchStateID]["IsMagazine"] = IsMagazine;
			this.PageSwitchState[pageSwitchStateID]["IsMagazineBeforeRemit"] = IsMagazineBeforeRemit;
			this.PageSwitchState[pageSwitchStateID]["session_incident_id"] = IncidentID;
			this.PageSwitchState[pageSwitchStateID]["ProblemCode"] = ProblemCode;
			this.PageSwitchState[pageSwitchStateID]["CommunicationChannelInstance"] = CommunicationChannelInstance;
			this.PageSwitchState[pageSwitchStateID]["CommunicationSourceInstance"] = CommunicationSourceInstance;

			return pageSwitchStateID;
		}
	}
}