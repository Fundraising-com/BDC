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
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using efundraising.ESubsGlobal;
using efundraising.ESubsGlobal.Payment;
using efundraising.ESubsGlobal.Users;

using efundraising.EFundraisingCRM;

using efundraising.EFundraisingCRMWeb.Components.Server;


namespace efundraising.EFundraisingCRMWeb.Online.Payments
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
    public partial class _Default : EFundraisingCrmOnlineBasePage, IPage, INoQuickToolBar
	{
		private const string PAYMENT_HISTORY_SESSION_KEY = "_PAYMENT_HISTORY_SESSION_KEY_";
        private const string GF_PAYMENT_ITEMS = "_GF_PAYMENT_ITEMS";
		public Components.User.OnlineEvent.EventInformation EventInformation1;
        //public List<PaymentItem> PaymentItemCollection;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) {
				if(onlineEventID == int.MinValue)
                {
					throw new ArgumentException("OnlineEventID is mandatory.", "OnlineEventID");
				} 
                else 
                {
					/*
					 * get the group/event and payment information and
					 * display the information has read only to the user
					 * control.
					 */
					
					// get the current event
					Event ev = Event.GetEventByEventID(onlineEventID);

					// get the current group
					Group group = Group.LoadByGroupID(ev.GroupID);

					// get the sponsor information
					eSubsGlobalUser sponsor = Sponsor.LoadByHierarchyID(group.SponsorID);

					// get the current payment information
					PaymentInfo paymentInformation = PaymentInfo.LoadPaymentInfoBySponsorID(sponsor.HierarchyID);
					if(paymentInformation == null) {
						paymentInformation = PaymentInfo.LoadPaymentInfoByEventID(group.GroupID, ev.EventID);
					}
                  
					// set information to the group/event/payment user control
					EventInformation1.SetAddressLabel(paymentInformation.PostalAddress.Address1);
					EventInformation1.SetCityLabel(paymentInformation.PostalAddress.City);
					EventInformation1.SetCountryLabel(paymentInformation.PostalAddress.CountryCode.Code);
					EventInformation1.SetGroupNameLabel(ev.Name);
					EventInformation1.SetPayableNameLabel(paymentInformation.PaymentName);
					EventInformation1.SetPhoneNumberLabel(paymentInformation.PhoneNumber.FormattedPhoneNumber);
					EventInformation1.SetSponserEmailHyperLink(ConfigurationManager.AppSettings["CurrentHost"] + "/".ToString()+ ev.Redirect);
                    EventInformation1.SetSponserEmailHyperLinkNavigateUrl(ConfigurationManager.AppSettings["CurrentHost"] + "/".ToString()+ ev.Redirect);
					EventInformation1.SetSponsorEmail(eSubsGlobalUser.LoadByHierarchyID(group.SponsorID).Email.ToString());
					EventInformation1.SetSponsorLabel(sponsor.CompleteName);
					EventInformation1.SetStateLabel(paymentInformation.PostalAddress.SubDivisionCode);
					EventInformation1.SetZipLabel(paymentInformation.PostalAddress.ZipCode);

                    //set Grand Father section of the Page
                    GrandFatherInfo.SetEventName(ev.Name);
                    GrandFatherInfo.SetCurrentProfit(ev.ProfitCalculated.ToString()+"%".ToString());
                    if (ev.ProfitGroupID != int.MinValue)
                    {
                        eFundraisingCommon.Profit pf = eFundraisingCommon.Profit.GetProfitByProfitGroupID(ev.ProfitGroupID).Where(p => p.QspCatalogTypeID == int.MinValue).Take(1).FirstOrDefault();
#if DEBUG
                        // to be removed
                      pf.ProfitId = 8;
                        
#endif
                        List<eFundraisingCommon.ProfitRange> profitRangeList = eFundraisingCommon.ProfitRange.GetProfitRangeByProfitID(pf.ProfitId);
                        if (profitRangeList != null && profitRangeList.Count > 0)
                        {
                            double profitSum =  profitRangeList.Sum(s => s.ProfitRangePercentage);
                            eFundraisingCommon.Profit currentProfit = eFundraisingCommon.Profit.GetProfitByProfitGroupID(ev.ProfitGroupID).Where(p => p.QspCatalogTypeID == int.MinValue).Take(1).FirstOrDefault();
                            if (currentProfit != null)
                            {
#if DEBUG
                                ev.ProfitCalculated = 37.0;
#endif
                                if (ev.ProfitCalculated != currentProfit.ProfitPercentage + profitSum)
                                {
                                    GrandFatherInfo.ShowGFOption = true;
                                }
                            }
                       
                            GrandFatherInfo.GetProfitRangeDataGrid().DataSource = profitRangeList;
                            GrandFatherInfo.GetProfitRangeDataGrid().DataBind();
                        }
                    }

					/*
					 * Get all payments that has already be done to this
					 * group and display summary of these payments into a 
					 * date grid control.
					 */

					DataBind();
				}
			}
		}

		// get the current event id from the url parameters
		private int onlineEventID {
			get {
				UrlParam urlParam = new UrlParam(Request);
				return urlParam.OnlineEventID;
				/* return 1008386; */
			}
		}

		private int paymentID {
			get {
				if(CurrentWorkingObject.Get(Session, "_PAYMENT_ID_") != null) {
					return int.Parse(CurrentWorkingObject.Get(Session, "_PAYMENT_ID_").ToString());
				}
				return int.MinValue;
			}
			set {
				CurrentWorkingObject.Save(value, Session,"_PAYMENT_ID_", null);
			}
		}

        public List<PaymentItem> paymentItemCollection
         {
             get
             {
                 return (CurrentWorkingObject.Get(Session, GF_PAYMENT_ITEMS) as List<PaymentItem>);
             }

             set
             {
                 CurrentWorkingObject.Save(value, Session, GF_PAYMENT_ITEMS, null);
             }
         }
		// Uses Components.Server.CurrentWorkingObject to save collection object
		// into the session or cache so that the database do not get hit at every postback
		private Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection GetPaymentHistory() {
			// get the traditional partners from the current working object manager
			Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection onlinePaymentListItemCollection  =
				(Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection) Components.Server.CurrentWorkingObject.Get(Session, PAYMENT_HISTORY_SESSION_KEY);

			// if null, then load it from the datasource
			// if first time this page is hit, reload it because it might be updated 
			// by "More" pages 
			if(onlinePaymentListItemCollection == null || !IsPostBack) {
				// instanciate the object
				efundraising.ESubsGlobal.Payment.Payment[] payments = 
					efundraising.ESubsGlobal.Payment.Payment.GetPaymentByEventID(onlineEventID);

				onlinePaymentListItemCollection = 
					new Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection();

				foreach(ESubsGlobal.Payment.Payment payment in payments) {
					onlinePaymentListItemCollection.AddPaymentListItemCollection(payment);
				}

				// save it for futher usage to the current working object
				Components.Server.CurrentWorkingObject.Save(onlinePaymentListItemCollection, Session, PAYMENT_HISTORY_SESSION_KEY, null);
			}

			return onlinePaymentListItemCollection;
		}

		// take over the DataBind method and bind collection objects to data grids
		private new void DataBind() {
			// get the traditional partners from the current working object manager
			Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection onlinePaymentListItemCollection = 
				GetPaymentHistory();

			// set data source and bind the data to the grid view
			PaymentHistoryDataGrid.DataSource = onlinePaymentListItemCollection;
			PaymentHistoryDataGrid.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
            GrandFatherInfo.ShowPaymentItemGrid += new EventHandler(GrandFatherInfo_ShowPaymentItemGrid);
            GrandFatherInfo.GrandFatherItemDataBind += new DataGridItemEventHandler(GrandFatherInfo_GrandFatherItemDataBind);
            GrandFatherInfo.GridDatabind += new EventHandler(GrandFatherInfo_GridDatabind);
			base.OnInit(e);
		}

        void GrandFatherInfo_GridDatabind(object sender, EventArgs e)
        {
            gfDatabind();
        }

        void GrandFatherInfo_GrandFatherItemDataBind(object sender, DataGridItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        void GrandFatherInfo_ShowPaymentItemGrid(object sender, EventArgs e)
        {
            if (onlineEventID != int.MinValue)
            {
                Event ev = Event.GetEventByEventID(onlineEventID);
                if (ev.ProfitGroupID != int.MinValue)
                {
                    eFundraisingCommon.Profit pr = eFundraisingCommon.Profit.GetProfitByProfitGroupID(ev.ProfitGroupID).Where(p => p.QspCatalogTypeID == int.MinValue).Take(1).FirstOrDefault();
                    List<PaymentItem> pi = PaymentItem.GetProcessedPaymentItemsByEventId(onlineEventID);
                    if (pi != null && pi.Count > 0)
                    {
                        pi = pi.Where(p => p.ProfitId == pr.ProfitId).ToList<PaymentItem>();
                        if (pi != null && pi.Count > 0)
                        {
                            paymentItemCollection = pi;
                            gfDatabind();
                        }
                    }
               

                }
            }

        }
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PaymentHistoryDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentHistoryDataGrid_ItemCommand);
			this.PaymentHistoryDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PaymentHistoryDataGrid_PageIndexChanged);
			this.PaymentHistoryDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentHistoryDataGrid_CancelCommand);
			this.PaymentHistoryDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentHistoryDataGrid_EditCommand);
			this.PaymentHistoryDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentHistoryDataGrid_UpdateCommand);

		}
		#endregion

		#region IPage Members

		public string PageInformation {
			get {
				return "Payment History";
			}
		}

		public string PageDescription {
			get {
				return "This is the history of payments made of an event.";
			}
		}

		public new void Search(string searchQuery) {
			base.Search(searchQuery);
		}

		public new void Create(string redirection) {
			base.Create(redirection);
		}

		#endregion

		private void PaymentHistoryDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			PaymentHistoryDataGrid.EditItemIndex = e.Item.ItemIndex;
			DataBind();
		}

		private void PaymentHistoryDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			// check if the event has been identified as being throw by the header
			if(e.Item.ItemType == ListItemType.Header) {
				// get the link button that has been raised
				LinkButton linkButton = (LinkButton)e.CommandSource;
				
				//get the list of partners
				Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemCollection onlinePaymentListItemCollection =
					GetPaymentHistory();

				// reverse the sort direction
				onlinePaymentListItemCollection.SortAssending = !onlinePaymentListItemCollection.SortAssending;

				// sort by selected field
				switch(linkButton.ID) {
					case "PaymentIDLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.PaymentID);
						break;
					case "PaymentPeriodLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.PaymentPeriod);
						break;
					case "PaymentTypeLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.PaymentType);
						break;
					case "TotalPaymentLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.TotalPayment);
						break;
					case "PaymentStatusLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.PaymentStatus);
						break;
					case "PaymentDateLinkButton":
						onlinePaymentListItemCollection.Sort(
							Components.Server.DataGrid.OnlinePayment.OnlinePaymentListItemSort.PaymentDate);
						break;
				}

				// rebind the data
				DataBind();
			} else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				if(e.CommandSource is Button) {
					Button cmd = (Button)e.CommandSource;
					if(cmd.ID == "SeeCommentButton") {
						paymentID = int.Parse(cmd.ToolTip);
						PaymentComment[] paymentComment = PaymentComment.GetPaymentCommentsByPaymentID(paymentID);
						if(paymentComment.Length == 0) {
							PaymentCommentDataGrid.Visible = false;
						} else {
							PaymentCommentDataGrid.DataSource = paymentComment;
							PaymentCommentDataGrid.DataBind();
						}
					}
				}
			}
		}

		private void PaymentHistoryDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
		
		}

		private void PaymentHistoryDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) {
			PaymentHistoryDataGrid.CurrentPageIndex = e.NewPageIndex;
			DataBind();
		}

		private void PaymentHistoryDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			PaymentHistoryDataGrid.EditItemIndex = -1;
			DataBind();
		}

        private void gfDatabind()
        {
            GrandFatherInfo.ShowPaymentItemDataGrid = true;
            GrandFatherInfo.GetPaymentItemDataGrid().DataSource = paymentItemCollection;
            GrandFatherInfo.DataBind();
        }
	}
}
