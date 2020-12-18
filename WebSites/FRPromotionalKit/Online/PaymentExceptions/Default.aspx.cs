
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
using efundraising.ESubsGlobal;
using efundraising.ESubsGlobal.Payment;
using efundraising.ESubsGlobal.Common;
using efundraising.EFundraisingCRMWeb.Components.Server.DataGrid.PaymentExceptions;
using System.Linq;
using System.Configuration;



//using efundraising.EFundraisingCRM;
using System.Diagnostics;
using efundraising.ESubsUSCheckSystem.BusinessEntity;
using efundraising.ESubsUSCheckSystem.BusinessLogic;

namespace efundraising.EFundraisingCRMWeb.Online.PaymentExceptions
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public enum SearchField
    {
	    GroupID,
	    EventID,
		CheckNo,
		Nothing
    }

	public partial class _Default : EFundraisingCrmOnlineBasePage, IPage
	{
		protected EFundraisingCRMWeb.Components.User.PaymentInformation.PaymentInformation PaymentInformation1;
		protected System.Web.UI.WebControls.Button Button1;
		protected readonly string historyTransfer = "../Payments/Default.aspx?".ToString() + Components.Server.UrlParam.UrlKeyOnlineEventID + "=";

		private const string PAYMENT_EXCEPTIONS_SESSION_KEY = "_PAYMENT_EXCEPTIONS_SESSION_KEY_";
        private efundraising.CheckSystemLib.BusinessLogic.CheckSystemController controller = null;
     

		protected void Page_Load(object sender, System.EventArgs e)
		{
            Session["Country"] = CountryProcessDropDownList.SelectedValue;
            Session["Period"] = PaymentPeriodProcessDropDownList.SelectedValue;

             SingleEventLabel.Visible = false;
            SingleEventTextBox.Visible = false;

            if (maxResults.Text == "")
            {
                maxResults.Text = "25";
            }
      


            Components.Server.User.CrmUser crmUser =
            Components.Server.User.CrmUser.Create(Session);

            foreach (Components.Server.User.Role userRole in crmUser.Roles.Role)
            {
                
                if (userRole.Name == "gCAEFR_Intranet_IT")
                {
                    SingleEventLabel.Visible = true;
                    SingleEventTextBox.Visible = true;

                }
            }


            bool disableExceptions = bool.Parse(efundraising.Configuration.ApplicationSettings.GetConfig()["DOUBLEPOSTCARDCHECK", 0, "DisableExceptions"]);
		


            if (disableExceptions)
            {
                IsCorrectedButton.Enabled = false;
            }
            else
            {
                IsCorrectedButton.Enabled = true;
            }

			errorLabel.Visible = false;
			if (!(IsPostBack)){
                    ValidateErrorLabel.Text = "";
		            period.Text = "3-1-2010";
					FillDropDowns();
					DoDataBind(true);
					PaymentInformation1.UsedOnPage = Components.User.PaymentInformation.PageUsage.PaymentExceptions;
//				try
//				{
//					
//														
//				}
//				catch(Exception ex)
//				{
//					throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Error in page load of PaymentExceptions", ex, null);
//				
//				}
			}
		}

		#region Private Methods
		
		private void LoadPaymentInfoControl(int paymentID, int exceptionTypeID)
		{
				//get payment
				Payment payment = Payment.GetPaymentByID(paymentID);

                //when user updates the exception type, we need to know what the old value was to fetch it
                Session["OldExcetionTypeID"] = exceptionTypeID;

				//get payment info
				PaymentInfo pI = PaymentInfo.GetPaymentInfoByIDActiveOrNot(payment.PaymentInfoId);
			
				//get exceptionType  (tempoary set exception type)
				PaymentExceptionType paymentExceptionType = PaymentExceptionType.GetPaymentExceptionTypeByID(exceptionTypeID,payment.PaymentId);

				

				//get partner
				Group group = null;
				if (pI.GroupID != int.MinValue && pI.EventID != int.MinValue)
				{
					group = Group.LoadByGroupID(pI.GroupID);
					///Payable To Group
					PartnerPaymentConfig partnerPaymentConfig = PartnerPaymentConfig.GetPartnerPaymentConfigByID(group.PartnerID);
					//

					DateTime startDate = DateTime.MinValue.AddYears(1969); // Min Value 00:00:00.0000000, January 1, 0001
					double totalPayment = Convert.ToDouble(ESubsGlobal.Payment.Payment.GetTotalPayment(paymentID, pI.EventID, startDate, DateTime.Now, partnerPaymentConfig));
					group = Group.LoadByGroupID(pI.GroupID);
					//get eventName
					Event _event = Event.GetEventByGroupID(pI.GroupID);
					//get group status
					GroupGroupStatus groupGroupStatus = GroupGroupStatus.GetGroupGroupStatusByID(pI.GroupID);
					startDate = new DateTime(DateTime.Now.Year, 1, 1);
					double currentYearPayment = 
						Convert.ToDouble(ESubsGlobal.Payment.Payment.GetTotalPayment(paymentID, pI.EventID, startDate, DateTime.Now, partnerPaymentConfig));
					//get payment status
					PaymentPaymentStatus paymentPaymentStatus = PaymentPaymentStatus.GetLastPaymentPaymentStatusByPaymentID(payment.PaymentId);
		          
			
					//set payment values
					
					if (partnerPaymentConfig == null)
					{
						partnerPaymentConfig = PartnerPaymentConfig.GetDefaultPartnerPaymentConfig();
					}

					PaymentInformation1.SetPayabaleToGroup(true);
					PaymentInformation1.SetEventPartnerIDTextBox(_event.EventID);
					PaymentInformation1.SetEventPartnerNameTextBox(_event.Name);
		

					if (paymentExceptionType != null)//if exception is found
					{
						PaymentInformation1.SetExceptionType(paymentExceptionType.ExceptionTypeId);
					}


					PaymentInformation1.SetPaymentIDTextBox(payment.PaymentId);
					PaymentInformation1.SetPaymentType(payment.PaymentTypeId);
					PaymentInformation1.SetPaymentPeriod(payment.PaymentPeriodId);
					PaymentInformation1.SetPartner(group.PartnerID);
									
					PaymentInformation1.SetTotalPaymentTextBox(totalPayment);
					PaymentInformation1.SetCurrentYearPayment(currentYearPayment);
					PaymentInformation1.SetPaymentAmountTextBox(Convert.ToDouble(payment.PaidAmount));
					PaymentInformation1.SetPaymentStatus(paymentPaymentStatus.PaymentStatusId);
					PaymentInformation1.SetGroupStatus(groupGroupStatus.GroupStatusId);
					PaymentInformation1.SetAttentionOfTextBox(payment.Name);
					PaymentInformation1.SetAddress1TextBox(payment.Address1);
					PaymentInformation1.SetAddress2TextBox(payment.Address2);
					PaymentInformation1.SetCityTextBox(payment.City);
					PaymentInformation1.SetZipTextBox(payment.ZipCode);
					PaymentInformation1.SetSubdivisionCode(payment.SubdivisionCode);
					//
                    PaymentInformation1.HideValidatedCheckBox();
                    PaymentInformation1.HideReissueButton();
                   //PaymentInformation1.ShowReissueButton();
					PaymentInformation1.SetGroupId(pI.GroupID);
					PaymentInformation1.SetCheckNumber(payment.ChequeNumber);
					efundraising.ESubsGlobal.Payment.PaymentPeriod pp = efundraising.ESubsGlobal.Payment.PaymentPeriod.GetPaymentPeriodByID(payment.PaymentPeriodId);
					PaymentInformation1.Refresh();
			
					DoDataBind(false);
				}
				else
				{//
					PartnerPaymentConfig partnerPaymentConfig = PartnerPaymentConfig.GetPartnerPaymentConfigByPaymentID(payment.PaymentId);
					if (partnerPaymentConfig == null)
						throw new Exception("partnerPaymentConfig canot be null");
					DateTime startDate = DateTime.MinValue.AddYears(1969); // Min Value 00:00:00.0000000, January 1, 0001
					double totalPayment = Convert.ToDouble(ESubsGlobal.Payment.Payment.GetTotalPayment(paymentID, pI.EventID, startDate, DateTime.Now, partnerPaymentConfig));
					
					// Get the current year payment
					startDate = new DateTime(DateTime.Now.Year, 1, 1);
					double currentYearPayment = 
						Convert.ToDouble(ESubsGlobal.Payment.Payment.GetTotalPayment(paymentID, pI.EventID, startDate, DateTime.Now, partnerPaymentConfig));
					//get payment status
					PaymentPaymentStatus paymentPaymentStatus = PaymentPaymentStatus.GetLastPaymentPaymentStatusByPaymentID(payment.PaymentId);
		          


					//if (paymentTo == PartnerPaymentConfig.PaymentToList.Partner )
					{
						PaymentInformation1.SetPayabaleToGroup(false);
						Partner partner = Partner.LoadByID(partnerPaymentConfig.PartnerId, ESubsGlobal.Culture.EN_US);
						if (partner != null)
						{
							PaymentInformation1.SetEventPartnerNameTextBox(partner.Name);
							PaymentInformation1.SetEventPartnerIDTextBox(partner.PartnerID);
							PaymentInformation1.SetPartner(partner.PartnerID);
						}
						//
						PaymentInformation1.SetPaymentIDTextBox(payment.PaymentId);
						PaymentInformation1.SetPaymentType(payment.PaymentTypeId);
						PaymentInformation1.SetPaymentPeriod(payment.PaymentPeriodId);
										
						PaymentInformation1.SetTotalPaymentTextBox(totalPayment);
						PaymentInformation1.SetCurrentYearPayment(currentYearPayment);
						PaymentInformation1.SetPaymentAmountTextBox(Convert.ToDouble(payment.PaidAmount));
						PaymentInformation1.SetPaymentStatus(paymentPaymentStatus.PaymentStatusId);
						PaymentInformation1.SetGroupStatus(int.MinValue);
						PaymentInformation1.SetAttentionOfTextBox(payment.Name);
						PaymentInformation1.SetAddress1TextBox(payment.Address1);
						PaymentInformation1.SetAddress2TextBox(payment.Address2);
						PaymentInformation1.SetCityTextBox(payment.City);
						PaymentInformation1.SetZipTextBox(payment.ZipCode);
						PaymentInformation1.SetSubdivisionCode(payment.SubdivisionCode);
						//
						PaymentInformation1.SetGroupId(int.MinValue);
						PaymentInformation1.SetCheckNumber(payment.ChequeNumber);
						//efundraising.ESubsGlobal.Payment.PaymentPeriod pp = efundraising.ESubsGlobal.Payment.PaymentPeriod.GetPaymentPeriodByID(payment.PaymentPeriodId);
						
						if (paymentExceptionType != null)//if exception is found
						{
							PaymentInformation1.SetExceptionType(paymentExceptionType.ExceptionTypeId);
						}
						PaymentInformation1.Refresh();
					}
		



			
					DoDataBind(false);
				}
		}

		private void FillDropDowns()
		{
			// fill exception type drop down list
			ExceptionType[] exceptionTypes = ExceptionType.GetExceptionTypes();
			//ExceptionTypeDropDownList.Items.Add(new ListItem("--ALL--", int.MinValue.ToString()));
			foreach(ExceptionType exceptionType in exceptionTypes) 
			{
				ExceptionTypeDropDownList.Items.Add(new ListItem(exceptionType.Description, exceptionType.ExceptionTypeId.ToString()));
			}
					         		
			// fill payment status drop down list
			PaymentStatus[] paymentStatuss = PaymentStatus.GetPaymentStatuss();
			PaymentStatusDropDownList.Items.Add(new ListItem("--ALL--", int.MinValue.ToString()));
			foreach(PaymentStatus paymentStatus in paymentStatuss) 
			{
				PaymentStatusDropDownList.Items.Add(new ListItem(paymentStatus.Description, paymentStatus.PaymentStatusId.ToString()));
			}
			try
			{
				PaymentStatusDropDownList.SelectedValue = ((int)ESubsGlobal.Payment.PaymentStatusCategory.InProcess).ToString();
			}
			catch (Exception)
			{
			}
			// fill group status drop down list
			GroupStatus[] groupStatuss = GroupStatus.GetGroupStatuss();
			GroupStatusDropDownList.Items.Add(new ListItem("--ALL--", int.MinValue.ToString()));
			foreach(GroupStatus groupStatus in groupStatuss) 
			{
				GroupStatusDropDownList.Items.Add(new ListItem(groupStatus.Description, groupStatus.GroupStatusId.ToString()));
			}

            FillPaymentPeriodDropDown();

			
		}

		
		// take over the DataBind method and bind collection objects to data grids
		private new void DataBind() 
		{
			DoDataBind(true);
		}
		private void DoDataBind(bool newSearch) 
		{
			DoDataBind(newSearch,new SearchField[] {SearchField.Nothing}, null);
		}

		private void DoDataBind(bool newSearch, SearchField[] searchFields, Hashtable searchFieldsValue) 
		{
			string exceptionType = ExceptionTypeDropDownList.SelectedItem.Text;
			string paymentStatus = PaymentStatusDropDownList.SelectedItem.Text;
			string groupStatus = GroupStatusDropDownList.SelectedItem.Text;
			string paymentPeriod = PaymentPeriodDropDownList.SelectedItem.Text;

			// get the payment exceptions from the current working object manager
			Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection paymentExceptionListItemCollection = 
				GetPaymentExceptions(newSearch, searchFields);


        	


		//	PaymentExceptionDataGrid.SelectedIndex = -1;
			efundraising.EFundraisingCRM.ViewsFilter vf = 
				new efundraising.EFundraisingCRM.ViewsFilter(typeof(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItem),
					typeof( Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection));

			vf.AndOr = efundraising.EFundraisingCRM.Operator.AND;
			            
			if (exceptionType != "--ALL--" )
			{
				vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("ExceptionType",exceptionType));
			}
			if (paymentStatus != "--ALL--" )
			{
				vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("PaymentStatus",paymentStatus));
			}
			if (groupStatus != "--ALL--" )
			{
				vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("GroupStatus",groupStatus));
			}
			if (paymentPeriod != "--ALL--" )
			{
				vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("PaymentPeriod",paymentPeriod));
			}
			if (searchFields.Length > 1 && searchFieldsValue != null && searchFieldsValue.Keys.Count > 1)
			{
				for (int i= 0;i < searchFields.Length; i++)
				{
					if (searchFields[i] == SearchField.GroupID)
						vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("GroupId",(string)searchFieldsValue[i]));
					if (searchFields[i] == SearchField.CheckNo)
						vf.AddFilter(new efundraising.EFundraisingCRM.FilterRule("CheckNo",(string)searchFieldsValue[i]));
				}
			}
			if (paymentExceptionListItemCollection !=null && paymentExceptionListItemCollection.Count > 0)
				vf.ExecuteFilterByRef(paymentExceptionListItemCollection);
			// set data source and bind the data to the grid view
			PaymentExceptionDataGrid.DataSource = paymentExceptionListItemCollection;
         	PaymentExceptionDataGrid.DataBind();
		}


		
		
		// Uses Components.Server.CurrentWorkingObject to save collection object
		// into the session or cache so that the database do not get hit at every postback
		private Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection GetPaymentExceptions(bool newSearch) 
		{
	       return GetPaymentExceptions(newSearch, SearchField.Nothing);
		}

		private Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection 
			GetPaymentExceptions(bool newSearch, SearchField[] searchFields) 
		{
			Hashtable uniquePayment = new Hashtable();
			Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection result = new efundraising.EFundraisingCRMWeb.Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection();
			for (int i=0; i< searchFields.Length; i++)
			{
				Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection
					paymentExceptionListItemCollection = GetPaymentExceptions(newSearch, searchFields[i]);

				for (int k=0; k < paymentExceptionListItemCollection.Count; k++)
				{
					PaymentExceptionListItem item = (PaymentExceptionListItem)paymentExceptionListItemCollection[k];
					if (uniquePayment[item.PaymentID.ToString() + item.ExceptionTypeID.ToString()] == null)
					{
						uniquePayment[item.PaymentID.ToString() 
							+ item.ExceptionTypeID.ToString()] = item.PaymentID.ToString() + item.ExceptionTypeID.ToString();
						result.Add(paymentExceptionListItemCollection[k]);
					}
				}
			}

			if (result.Count > 0)
			{
				return result;
			}

			return null;
		}

        private Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection GetPaymentExceptions(bool newSearch, SearchField searchField)
        {
            Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection paymentExceptionListItemCollection = null;
            // get the exceptions from the current working object manager
            if (!(newSearch))
            {
                paymentExceptionListItemCollection =
                    (Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection)Components.Server.CurrentWorkingObject.Get(Session, PAYMENT_EXCEPTIONS_SESSION_KEY);
            }
            // if null, then load it from the datasource
            // if first time this page is hit, reload it because it might be updated 
            // by "More" pages 
            if (paymentExceptionListItemCollection == null || !IsPostBack || searchField != SearchField.Nothing)
            {

                efundraising.ESubsGlobal.Payment.PaymentExceptionType[] paymentExceptionTypes = null;
                // instanciate the object
                switch (searchField)
                {
                    case SearchField.CheckNo:
                        paymentExceptionTypes = efundraising.ESubsGlobal.Payment.PaymentExceptionType.GetPaymentExceptionTypesByCheckNo(Convert.ToInt32(CheckNoTextBox.Text));
                        break;
                    case SearchField.GroupID:
                        paymentExceptionTypes = efundraising.ESubsGlobal.Payment.PaymentExceptionType.GetPaymentExceptionTypesByGroupID(Convert.ToInt32(GroupIDTextBox.Text));
                        break;

                    default:
                        DateTime time = DateTime.Now;
                        try
                        {
                            time = Convert.ToDateTime(period.Text);
                        }
                        catch (Exception x)
                        {
                            //time = DateTime.Now;
                        }

                        paymentExceptionTypes = efundraising.ESubsGlobal.Payment.PaymentExceptionType.GetPaymentExceptionTypesUncorrected(time);
                        break;
                }

                paymentExceptionListItemCollection =
                    new Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection();


                if (paymentExceptionTypes == null)
                {
                    paymentExceptionListItemCollection.AddPaymentExceptionEmptyLine();
                }

                int i = 0;
                foreach (ESubsGlobal.Payment.PaymentExceptionType paymentExceptionType in paymentExceptionTypes)
                {

                    int typeID = Convert.ToInt32(ExceptionTypeDropDownList.SelectedItem.Value);
                    //(paymentExceptionType.CreateDate > Convert.ToDateTime(period.Text) &&
                    if (paymentExceptionType.ExceptionTypeId == typeID)
                    {
                        if (i < Convert.ToInt32(maxResults.Text))
                        {
                            i++;
                            //
                            //THIS IS THE BOTTLENECK, TAKES 1 SEC PER RECORD
                            //
                            paymentExceptionListItemCollection.AddPaymentExceptionTypeListItemCollection(paymentExceptionType);
                        }
                    }


                }

                // save it for futher usage to the current working object
                Components.Server.CurrentWorkingObject.Save(paymentExceptionListItemCollection, Session, PAYMENT_EXCEPTIONS_SESSION_KEY, null);
            }

            return paymentExceptionListItemCollection;
        }
		private void UpdateAddressInfoBK(Payment payment, PaymentInfo paymentInfo)
		{				
			//create new postal address (no update is done, its a new address)
			ESubsGlobal.Common.PostalAddress postalAddress = new ESubsGlobal.Common.PostalAddress();

			//create new payment info
			PaymentInfo paymentInfoNew = new PaymentInfo();

			paymentInfoNew = paymentInfo;
			paymentInfoNew.CreateDate = DateTime.Now;

			//create old paymentInfo object
			PaymentInfo paymentInfoOld = new PaymentInfo();
			paymentInfoOld.PaymentInfoID = paymentInfo.PaymentInfoID;
			paymentInfoOld.GroupID = paymentInfo.GroupID;
			paymentInfoOld.EventID = paymentInfo.EventID;
			paymentInfoOld.PhoneNumber = paymentInfo.PhoneNumber;
			paymentInfoOld.PostalAddress = paymentInfo.PostalAddress;
			paymentInfoOld.PostalAddressID = paymentInfo.PostalAddressID;
			paymentInfoOld.PhoneNumberID = paymentInfo.PhoneNumberID;
			paymentInfoOld.PaymentName = paymentInfo.PaymentName;
			paymentInfoOld.OnBehalfOfName = paymentInfo.OnBehalfOfName;
			paymentInfoOld.ShipToName = paymentInfo.ShipToName;
			paymentInfoOld.Ssn = paymentInfo.Ssn;
			paymentInfoOld.Active = false;
			paymentInfoOld.CreateDate = paymentInfo.CreateDate;
			
			//get info frm control
			postalAddress.Address1 = PaymentInformation1.GetAddress1TextBox();
			postalAddress.Address2 = PaymentInformation1.GetAddress2TextBox();
			postalAddress.City = PaymentInformation1.GetCityTextBox();
			postalAddress.ZipCode = PaymentInformation1.GetZipTextBox();
			postalAddress.SubDivisionCode = PaymentInformation1.GetSubdivisionCode();
			postalAddress.CountryCode = ESubsGlobal.Common.CountryCode.Create(PaymentInformation1.GetCountryCode());
			postalAddress.CreateDate = DateTime.Now;

			// update postal info by Creating a New One
			ESubsGlobal.Common.PostalAddressStatus postalAddressStatus = (ESubsGlobal.Common.PostalAddressStatus) postalAddress.Insert();

			switch(postalAddressStatus) 
			{
				case PostalAddressStatus.Ok:
					paymentInfoNew.PostalAddressID = postalAddress.Id;
					paymentInfoNew.ShipToName = PaymentInformation1.GetAttentionOfTextBox(); 
					paymentInfoNew.Active = true;
							
					//UPDATE PAYMENT INFO By cretaing a New One
					ESubsGlobal.Payment.PaymentInfoStatus paymentInfoStatus = (ESubsGlobal.Payment.PaymentInfoStatus) paymentInfoNew.Insert();

					switch(paymentInfoStatus) 
					{

						case PaymentInfoStatus.Ok:

							//update payment with new payment info\
							payment.PaymentInfoId = paymentInfoNew.PaymentInfoID;
							_PaymentStatus paymentStatus = (ESubsGlobal.Payment._PaymentStatus) payment.Update();

							switch(paymentStatus) 
							{
								case _PaymentStatus.Ok:
								break;
								default:
									throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update payment status object", null, null);
							}

							//set old payment info to Inactive
							paymentInfoStatus = (ESubsGlobal.Payment.PaymentInfoStatus) paymentInfoOld.Update();

							switch(paymentInfoStatus) 
							{
								case PaymentInfoStatus.Ok:
								break;
								default:
									throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to create payment info object", null, null);
							}

							break;
							default:
								throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to create postal address object", null, null);
						}

				   break;
				default:
					throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
			}
		}


		

        #endregion 
	
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PaymentExceptionDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentExceptionDataGrid_ItemCommand);
			this.PaymentExceptionDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PaymentExceptionDataGrid_PageIndexChanged);

		}
		#endregion

		#region IPage Members

		public string PageInformation 
		{
			get 
			{
				return "Payment Exceptions";
			}
		}

		public string PageDescription 
		{
			get 
			{
				return "This is a listing of all exceptions for all events.";
			}
		}

		public new void Search(string searchQuery) 
		{
			base.Search(searchQuery);
		}

		public new void Create(string redirection) 
		{
			base.Create(redirection);
		}

		#endregion

		#region EventHandlers

		protected void ShowButton_Click(object sender, System.EventArgs e)
		{
			//if a group id entered
			
			if (GroupIDTextBox.Text.Trim() != "" && CheckNoTextBox.Text.Trim() != "")
			{
//				try
//				{
					int.Parse(GroupIDTextBox.Text);
					int.Parse(CheckNoTextBox.Text);
					Hashtable searchFieldsValue = new Hashtable();
					searchFieldsValue[0] = GroupIDTextBox.Text.Trim();
					searchFieldsValue[1] = CheckNoTextBox.Text.Trim();
					DoDataBind(true,new SearchField[] {SearchField.GroupID, SearchField.CheckNo}, searchFieldsValue);
//				}
//				catch (Exception ex)
//				{
//					PaymentExceptionDataGrid.DataSource = null;
//					PaymentExceptionDataGrid.DataBind();
//				}
			}
			else
			{
//				try
//				{
					if (GroupIDTextBox.Text.Trim() != "")
					{
						int.Parse(GroupIDTextBox.Text);
						DoDataBind(true,new SearchField[] {SearchField.GroupID}, null);
					}
					else if (CheckNoTextBox.Text.Trim() != "")
					{
						int.Parse(CheckNoTextBox.Text);
						DoDataBind(true, new SearchField[] {SearchField.CheckNo}, null);
					}
					else if (GroupIDTextBox.Text.Trim() == "" && CheckNoTextBox.Text.Trim() == "")
					{
						DoDataBind(true);
					}
//				}
//				catch (Exception ex)
//				{
//					PaymentExceptionDataGrid.DataSource = null;
//					PaymentExceptionDataGrid.DataBind();
//				}

			}
		}

	
		private void PaymentExceptionDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// check if the event has been identified as being throw by the header
			if(e.Item.ItemType == ListItemType.Header) 
			{
				// get the link button that has been raised
				LinkButton linkButton = (LinkButton)e.CommandSource;
				
				//get the list of exceptions to process
				Components.Server.DataGrid.PaymentExceptions.PaymentExceptionListItemCollection paymentExceptions = this.GetPaymentExceptions(false);

				// reverse the sort direction
				bool asc = paymentExceptions.SortAssending;
				paymentExceptions.SortAssending = !paymentExceptions.SortAssending;

				// sort by selected field
				switch(linkButton.Text) 
				{
					case "Exception Type":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.ExceptionType);
						break;
					case "ID":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentID);
						break;
					case "Payment Period":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentPeriod);
						break;
					case "Payment Type":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentType);
						break;
					case "Partner Name":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PartnerName);
						break;
					case "Group Status":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.GroupStatus);
						break;
					case "Total Payment":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.TotalPayment);
						break;
					case "Payment Amount":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentAmount);
						break;
					case "Payment Status":
						paymentExceptions.Sort(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentStatus);
						break;
				}

				DoDataBind(false);
			}
			else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//get the payment info id from the datagrid
				int paymentID = Convert.ToInt32(e.Item.Cells[Convert.ToInt32(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.PaymentID)].Text);
				int exceptionTypeID = Convert.ToInt32(e.Item.Cells[Convert.ToInt32(Components.Server.DataGrid.PaymentExceptions.PaymentExceptionTypeListItemSort.ExceptionTypeID)].Text);

				LoadPaymentInfoControl(paymentID, exceptionTypeID);
			}

		}
		private void clearCurrentSession()
		{
			// removing collection from the session
			Components.Server.CurrentWorkingObject.Remove(Session, "PAYMENT_PROCESS");	
			Components.Server.CurrentWorkingObject.Remove(Session, "PAYMENT_EXCEPTIONS_SESSION_KEY");				
		}
		protected void UpdateButton_Click(object sender, System.EventArgs e)
		{
			//can not update check information is payment period is passed
			PaymentPeriod paymentPeriod = PaymentPeriod.GetLatestPaymentPeriod();
			DateTime endDate = paymentPeriod.EndDate;

			paymentPeriod = PaymentPeriod.GetPaymentPeriodByID(PaymentInformation1.GetPaymentPeriod());
			DateTime currentEndDate = paymentPeriod.EndDate;

			/*if (currentEndDate < endDate)
			{
				errorLabel.Text = "You can not update payment information for this period.";
				errorLabel.Visible = true;
			}
			else
			{*/
				//get payment info 
				Payment payment = Payment.GetPaymentByID(PaymentInformation1.GetPaymentIDTextBox());
				PaymentInfo pI = PaymentInfo.GetPaymentInfoByIDActiveOrNot(payment.PaymentInfoId);

			
				if (pI.GroupID != int.MinValue)
				{
					//update group status
					GroupGroupStatus groupGroupStatus = GroupGroupStatus.GetGroupGroupStatusByID(pI.GroupID);
					if (groupGroupStatus.GroupStatusId != PaymentInformation1.GetGroupStatus())
					{
						groupGroupStatus.GroupStatusId = PaymentInformation1.GetGroupStatus();
						ESubsGlobal.GroupGroupStatusStatus groupGroupStatusStatus = (ESubsGlobal.GroupGroupStatusStatus) groupGroupStatus.Update();

						switch(groupGroupStatusStatus) 
						{
							case GroupGroupStatusStatus.Ok:
								break;
							default:
								throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
						}
					}
				}
			
				//update payment status
				PaymentPaymentStatus paymentPaymentStatus = PaymentPaymentStatus.GetPaymentPaymentStatusByID(payment.PaymentId);
				if (paymentPaymentStatus.PaymentStatusId != PaymentInformation1.GetPaymentStatus())
				{
					paymentPaymentStatus.PaymentStatusId = PaymentInformation1.GetPaymentStatus();
					ESubsGlobal.Payment.PaymentPaymentStatusStatus paymentPaymentStatusStatus = (ESubsGlobal.Payment.PaymentPaymentStatusStatus) paymentPaymentStatus.Update();

					switch(paymentPaymentStatusStatus) 
					{
						case PaymentPaymentStatusStatus.Ok:
							break;
						default:
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}
				}
                
            //update payment exception
            //check if its was changed , if so correct it and insert a new one
                int oldId = Convert.ToInt32(Session["OldExcetionTypeID"]);
                if (oldId != PaymentInformation1.GetExceptionType())
                {
                    PaymentExceptionType pet = new PaymentExceptionType();
                    pet.PaymentId = PaymentInformation1.GetPaymentIDTextBox();
                    pet.ExceptionTypeId = PaymentInformation1.GetExceptionType();
                    pet.CreateDate = DateTime.Now;
                    pet.IsCorrected = false;
                    pet.Insert();

                    pet = PaymentExceptionType.GetPaymentExceptionTypeByID(oldId, PaymentInformation1.GetPaymentIDTextBox());
                    pet.IsCorrected = true;
                    pet.Update();

                }
                

				//update payment info
				payment.PaidAmount = Convert.ToDecimal(PaymentInformation1.GetPaymentAmountTextBox());
				payment.PaymentId = PaymentInformation1.GetPaymentIDTextBox();
				payment.PaymentPeriodId = PaymentInformation1.GetPaymentPeriod();
				//get info frm control
				payment.Address1 = PaymentInformation1.GetAddress1TextBox();
				payment.Address2 = PaymentInformation1.GetAddress2TextBox();
				payment.City = PaymentInformation1.GetCityTextBox();
				payment.ZipCode = PaymentInformation1.GetZipTextBox();
				payment.SubdivisionCode = PaymentInformation1.GetSubdivisionCode();
				try
				{
					payment.CountryCode = ESubsGlobal.Common.CountryCode.Create(PaymentInformation1.GetCountryCode()).Code;
				}
				catch (Exception)
				{
					payment.CountryCode = null;
				}
				payment.Name = PaymentInformation1.GetAttentionOfTextBox();

				ESubsGlobal.Payment._PaymentStatus paymentStatus = (ESubsGlobal.Payment._PaymentStatus) payment.Update();

				switch(paymentStatus) 
				{
					case _PaymentStatus.Ok:
						break;
					default:
						throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
				}
								
				Components.Server.User.CrmUser crmUser =
					Components.Server.User.CrmUser.Create(Session);
				if (crmUser != null)
					efundraising.Diagnostics.Logger.LogInfo(string.Format("Payment Exception: The user {0} is successful updating the payment {1} ", crmUser.ID, payment.PaymentId));

				//refresh
				//DoDataBind(true);
			//}
		}

		protected void IsCorrectedButton_Click(object sender, System.EventArgs e)
		{
			//update exception to Corrected
			int paymentID = PaymentInformation1.GetPaymentIDTextBox();
			if (paymentID != int.MinValue)
			{
				int paymentExceptionTypeID = PaymentInformation1.GetExceptionType();
				ESubsGlobal.Payment.PaymentExceptionType paymentExceptionType = ESubsGlobal.Payment.PaymentExceptionType.GetPaymentExceptionTypeByID(paymentExceptionTypeID,paymentID);
				paymentExceptionType.IsCorrected = true;

				//update in databse
				ESubsGlobal.Payment.PaymentExceptionTypeStatus paymentExceptionTypeStatus = (ESubsGlobal.Payment.PaymentExceptionTypeStatus) paymentExceptionType.Update();

				switch(paymentExceptionTypeStatus) 
				{
					case PaymentExceptionTypeStatus.Ok:
						break;
					default:
						throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
				}

				clearCurrentSession();
				//refresh
			//	DoDataBind(true);
			}

		}
	

		private void PaymentExceptionDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PaymentExceptionDataGrid.CurrentPageIndex = e.NewPageIndex;
			DoDataBind(false);
		}
	
		#endregion

		protected void ViewHistoricButton_Click(object sender, System.EventArgs e)
		{
			Payment payment = Payment.GetPaymentByID(PaymentInformation1.GetPaymentIDTextBox());
			PaymentInfo paymentInfo = PaymentInfo.GetPaymentInfoByIDActiveOrNot(payment.PaymentInfoId);

			// we are transfering control to the history page with event id as url parameter
			Server.Transfer(historyTransfer + paymentInfo.EventID);
			
		}

        protected void NewPeriodLinkButton_Click(object sender, EventArgs e)
        {

            PaymentPeriod pp = new PaymentPeriod();
            int id = pp.InsertNext();
            FillPaymentPeriodDropDown();


        }

        private void FillPaymentPeriodDropDown()
        {

            // fill payment period drop down list
            PaymentPeriod[] paymentPeriods = PaymentPeriod.GetLatestPaymentPeriods();
            //Array.Sort(paymentPeriods);
            PaymentPeriodDropDownList.Items.Clear();
            PaymentPeriodProcessDropDownList.Items.Clear();
            PaymentPeriodDropDownList.Items.Add(new ListItem("--ALL--", int.MinValue.ToString()));
            
            foreach (PaymentPeriod paymentPeriod in paymentPeriods)
            {
                PaymentPeriodDropDownList.Items.Add(new ListItem(paymentPeriod.StartDate.ToString("MMM-dd, yy") + " to " + paymentPeriod.EndDate.ToString("MMM-dd, yy"), paymentPeriod.PaymentPeriodId.ToString()));
                PaymentPeriodProcessDropDownList.Items.Add(new ListItem(paymentPeriod.StartDate.ToString("MMM-dd, yy") + " to " + paymentPeriod.EndDate.ToString("MMM-dd, yy"), paymentPeriod.PaymentPeriodId.ToString()));
            }

            DateTime a = new DateTime(2007,9,1);
            DateTime b = new DateTime(2007, 9, 30);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "131"));

             a = new DateTime(2009, 10, 1);
            b = new DateTime(2009, 10, 31);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "631"));


            a = new DateTime(2009, 2, 1);
            b = new DateTime(2009, 2, 28);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "756"));


            a = new DateTime(2009, 9, 1);
            b = new DateTime(2009, 9, 30);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "580"));

            a = new DateTime(2009, 6, 1);
            b = new DateTime(2009, 6, 30);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "706"));

            a = new DateTime(2009, 11, 1);
            b = new DateTime(2009, 11, 30);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "651"));

            a = new DateTime(2009, 12, 1);
            b = new DateTime(2009, 12, 31);
            PaymentPeriodProcessDropDownList.Items.Add(new ListItem(a.ToString("MMM-dd, yy") + " to " + b.ToString("MMM-dd, yy"), "697"));

        }

        protected void GeneratePaymentsButton_Click(object sender, EventArgs e)
        {
            string[] eventIds = null;
            //to do, put single event in  array


            string dateRange = PaymentPeriodProcessDropDownList.SelectedItem.Text;
            int pos = dateRange.IndexOf("to");
            string a = dateRange.Substring(0, pos - 1);
            a = dateRange.Substring(pos + 2, dateRange.Length - pos - 2);
            DateTime startDate = Convert.ToDateTime(dateRange.Substring(0, pos - 1));
            DateTime endDate = Convert.ToDateTime(dateRange.Substring(pos + 2,dateRange.Length - pos -2));
            
      
            try
            {
                Session["SingleEvent"] = "0";
                if (SingleEventTextBox.Text.Trim() != string.Empty)
                {
                    Session["SingleEvent"] = SingleEventTextBox.Text;
                    eventIds = SingleEventTextBox.Text.Trim().Split(',');
                    for (int i = 0; i < eventIds.Length; i++)
                        int.Parse(eventIds[i]);
                }
            }
            catch (Exception)
            {
                throw new Exception("Event Ids must be separated by ,");
            }

            DoProcess(startDate, endDate, eventIds);
        }

        private void DoProcess(DateTime startDate, DateTime endDate, string[] eventIds)
        {
             Session["Country"] = CountryProcessDropDownList.SelectedValue;
             Session["Period"] = PaymentPeriodProcessDropDownList.SelectedValue;

            
            efundraising.CheckSystemLib.BusinessEntity.CheckData checkData = new ESubsCheckData();
            if (eventIds != null && eventIds.Length > 0)
                (checkData as ESubsCheckData).SetEventsToBeProcessed(eventIds);
            try
            {
                checkData.period.StartDate = startDate;
                checkData.period.EndDate = endDate;
                bool bflag = checkData.LoadAccounts(); //no qsp
                if (!bflag)
                {
                   // ProcessStatusLabel.Text = string.Format("There is no payment on the period between: {0} and {1}"
                     //   , startDate.ToLongDateString(), endDate.ToLongDateString());
                    return;
                }

                controller = new ESubsCheckSystemController();
                
              //  checkData.OnFinish += new efundraising.CheckSystemLib.BusinessEntity.Finish(checkData_OnFinish);
                controller.ProcessModules(checkData);
                Label1.Text = string.Format("Successful to process payments on the period of {0}"
                    , PaymentPeriodDropDownList.SelectedItem.Text);

               
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "No Records on es_get_event_by_order_date")
                {
                    
                }
                throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Process Payments Errors", ex, checkData);
            }
        }

        protected void ValidateButton_Click(object sender, EventArgs e)
        {
            ValidateErrorLabel.ForeColor = Color.Red;

            //string countryCode = Convert.ToString(Session["Country"]);
            string countryCode = CountryValidateDropDownList.SelectedValue;
            int result = ESubsGlobal.Payment.Payment.UpdateValidations(countryCode);
            if (result > 1)
            {
                ValidateErrorLabel.Text = result.ToString() +  " Payments Updated";
                ValidateErrorLabel.ForeColor = Color.DarkGreen;
              
            }else if (result == 0){
                ValidateErrorLabel.Text = "No Payments were updated";
              
            } else{
                ValidateErrorLabel.Text = "Error while updating";
              
            }

            
        }

	}
}
