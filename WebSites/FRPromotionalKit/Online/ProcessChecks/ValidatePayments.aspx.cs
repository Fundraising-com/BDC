using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Linq;




namespace efundraising.EFundraisingCRMWeb.Online.ProcessChecks
{
	/// <summary>
	/// Code behind for Payment Validation page
	/// </summary>
	public partial class ValidatePayments : EFundraisingCrmOnlineBasePage, IPage, INoQuickToolBar
	{
        protected Components.User.PaymentInformation.PaymentInformation PaymentInformation1;
        protected Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection datagridCollection;
        private Dictionary<string, bool> sortDatagrid = new Dictionary<string, bool>();

		#region Constants
		protected readonly string historyTransfer = "../Payments/Default.aspx?".ToString() + Components.Server.UrlParam.UrlKeyOnlineEventID + "=";
		protected readonly string[] donotInsertPaymentStatus ={"Cashed"};
		private readonly string[] donotShow = {"Cashed","Void","Sent"};
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{

                 this.DisplayPaymentButton.Click += new EventHandler(DisplayPaymentButton_Click);
                this.GenerateFileButton.Click += new EventHandler(GenerateFileButton_Click);
                this.PaymentInformation1.Reissued += new efundraising.EFundraisingCRMWeb.Components.User.PaymentInformation.ReissueEventHandler(PaymentInfo_Reissue);
                this.RejectBatchtButton.Click += new EventHandler(RejectBatchtButton_Click);
                this.BatchDisplayDropdownlist.SelectedIndexChanged += new EventHandler(BatchDisplayDropdownlist_SelectedIndexChanged);
                this.ApproveBatchButton.Click += new EventHandler(ApproveBatchButton_Click);

                if (getDisplayMode() == int.MinValue)
                {
                    setDisplayMode(Accordion1.SelectedIndex);
                    DoRefresh();
                    PaymentToProcessDataGrid.SelectedIndex = -1;
                    PaymentToProcessDataGrid.CurrentPageIndex = 0;
                    this.DataBind();
                    PaymentInformationPanel.Visible = false;
                }
                else if (getDisplayMode() != Accordion1.SelectedIndex)
                {
                    setDisplayMode(Accordion1.SelectedIndex);
                    DoRefresh();
                    PaymentToProcessDataGrid.SelectedIndex = -1;
                    PaymentToProcessDataGrid.CurrentPageIndex = 0;
                    this.DataBind();
                    PaymentInformationPanel.Visible = false;
                }
           

			try
			{
				PaymentToProcessDataGrid.PageSize = Int16.Parse(Global.GetDataGridPageSize());
			}
			catch (Exception)
			{
				PaymentToProcessDataGrid.PageSize = 10;
			}
			if(!IsPostBack) 
			{
				
				// hiding payment information panel
				PaymentInformationPanel.Visible = false;
			
			
			}
                 
			// putting some element in the user control into read-only state
			PaymentInformation1.UsedOnPage = efundraising.EFundraisingCRMWeb.Components.User.PaymentInformation.PageUsage.PaymentToProcess;
		}

        void ApproveBatchButton_Click(object sender, EventArgs e)
        {
            if (BatchDisplayDropdownlist.SelectedValue != "-1")
            {
                int batch_id = int.MinValue;
                try
                {
                    batch_id = Int32.Parse(BatchDisplayDropdownlist.SelectedValue);
                    efundraising.ESubsGlobal.Payment.PaymentBatch batch = efundraising.ESubsGlobal.Payment.PaymentBatch.GetPaymentBatchByID(batch_id);
                    if (batch != null)
                    {
                        batch.CancelledDate = DateTime.MinValue;
                        batch.ConfirmationDate = DateTime.Now;
                        efundraising.ESubsGlobal.Payment.PaymentBatch.UpdatePaymentBatch(batch);
                    }
                }
                catch (Exception ex)
                {
                    Diagnostics.Logger.LogError(String.Format("Failed insert confirmation Payment Batch Table for batch {0}" , batch_id), ex);
                }
            }
            
        }

       void BatchDisplayDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ESubsGlobal.Payment.CheckSystemPayment> temp = this.GetPaymentsToProcess().PaymentToProcessList;
            if (BatchDisplayDropdownlist.SelectedValue != "-1")
            {
                int tmp = Int32.Parse(BatchDisplayDropdownlist.SelectedValue);
                temp = temp.Where(p => p.payment.PaymentBatchID == tmp).ToList();
            }
            PaymentToProcessDataGrid.DataSource = temp;
            PaymentToProcessDataGrid.CurrentPageIndex = 0;
            PaymentToProcessDataGrid.SelectedIndex = -1;
            PaymentToProcessDataGrid.DataBind();
            PaymentInformationPanel.Visible = false; 
        }

        void RejectBatchtButton_Click(object sender, EventArgs e)
        {
            if (BatchDisplayDropdownlist.SelectedValue != "-1")
            {
                try
                {
                    int batch_id = Int32.Parse(BatchDisplayDropdownlist.SelectedValue);
                    efundraising.ESubsGlobal.Payment.PaymentBatch batch = efundraising.ESubsGlobal.Payment.PaymentBatch.GetPaymentBatchByID(batch_id);
                    if (batch != null)
                    {
                        batch.CancelledDate = DateTime.Now;
                        batch.ConfirmationDate = DateTime.MinValue;
                        efundraising.ESubsGlobal.Payment.PaymentBatch.UpdatePaymentBatch(batch);

                        List<efundraising.ESubsGlobal.Payment.Payment> temp = efundraising.ESubsGlobal.Payment.Payment.GetPaymentsByPaymentBatchID(batch_id);
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (efundraising.ESubsGlobal.Payment.Payment pp in temp)
                            {
                                ESubsGlobal.Payment.PaymentPaymentStatus p = new ESubsGlobal.Payment.PaymentPaymentStatus();
                                p.PaymentId = pp.PaymentId;
                                p.PaymentStatusId = 9;  //  cancel
                                p.CreateDate = DateTime.Now;
                                try
                                {
                                    p.Insert();
                                }
                                catch (Exception ex)
                                {
                                    Diagnostics.Logger.LogError(String.Format("Failed insert Payment Payment status Table for payment {0} , status {1}", p.PaymentId, p.PaymentStatusId), ex);
                                    continue;
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

		#region Helper Methods

		/// <summary>
		/// this method is responsible for databinding of datagrid
		/// </summary>
		private new void DataBind()
		{
          
                // get collection to bind to datagrid
             datagridCollection = this.GetPaymentsToProcess();

             bindDropDownList(PartnerTypeDropDown, datagridCollection.PartnerList);
             bindDropDownList(PaymentPeriodDropDown, datagridCollection.PaymentPeriodList);
             bindDropDownList(GroupIdDropdownlist, datagridCollection.GroupIDList);
             bindDropDownList(PaymentStatusDropdownlist, datagridCollection.PaymentStatusList);
             bindDropDownList(PaymentTodropdown, datagridCollection.PayableToList);
             bindDropDownList(FileName, datagridCollection.FileNameBatch);
             bindDropDownList(BatchDisplayDropdownlist, datagridCollection.FileNameBatch);

			 SetGeneralInformation(datagridCollection);
			 PaymentToProcessDataGrid.SelectedIndex = -1;
             PaymentToProcessDataGrid.DataSource = datagridCollection.PaymentToProcessList;
			 PaymentToProcessDataGrid.DataBind();
		}

		private void SetGeneralInformation(Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection listItemCollection)
		{
			if (listItemCollection == null || listItemCollection.Count <1)
				return;

			decimal totalPayment = Decimal.Zero;
			int iTotalCount = listItemCollection.Count;
			int iPaymentToParter = 0;
			foreach (Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItem item in listItemCollection)
			{
				totalPayment += item.PaymentAmount;
				if (!item.PayableToGroup)
					iPaymentToParter++;

			}
			InformationLabel.Text = string.Format("Number of payments: {0} - Total Amount: {1} - Payable To Partner:{2}"
					, iTotalCount, totalPayment.ToString("C"), iPaymentToParter);
		}

        private bool isSortDescending(string header)
        {
            return Session["SORT_DATAGRID"] != null && (Session["SORT_DATAGRID"] as Dictionary<string, bool>).ContainsKey(header) && (Session["SORT_DATAGRID"] as Dictionary<string, bool>)[header];
        }

        private void updateSortDescending(string header)
        {
            if (Session["SORT_DATAGRID"] != null)
            {
                if (Session["SORT_DATAGRID"] is Dictionary<string, bool>)
                {
                    if((Session["SORT_DATAGRID"] as Dictionary<string, bool>).ContainsKey(header) )
                    {
                        (Session["SORT_DATAGRID"] as Dictionary<string, bool>)[header] = !(Session["SORT_DATAGRID"] as Dictionary<string, bool>)[header];
                    }

                }
            }
        }

		#endregion

		#region IPage Members

		public string PageInformation 
		{
			get 
			{
				return "Start Validation Profit Checks";
			}
		}

		public string PageDescription 
		{
			get 
			{
				return "This section provide the wizard to create the checks";
			}
		}

		#endregion

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.PaymentToProcessDataGrid.ItemCommand += new DataGridCommandEventHandler(PaymentToProcessDataGrid_ItemCommand);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PaymentToProcessDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PaymentToProcessDataGrid_OnSelect);
			this.PaymentToProcessDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PaymentToProcessDataGrid_PageIndexChanged);
			this.PaymentToProcessDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.PaymentToProcessDataGrid_ItemDataBound);
            //this.ResetButton.Click += new EventHandler(this.ResetButton_Click);
          //this.ResetButton.Click += new    EventHandler(ResetButton_Click);
		}

        void GenerateFileButton_Click(object sender, EventArgs e)
        {
            //efundraising.ESubsUSCheckSystem.BusinessLogic.ESubsDPostCardCheckOutput DPostCC = new efundraising.ESubsUSCheckSystem.BusinessLogic.ESubsDPostCardCheckOutput();
            //if (DPostCC.OutputToFileFromWeb(DateTime.Now, CountryDisplayDropdownlist.SelectedValue))
            //{
            //    clearCurrentSession();
            //}

            efundraising.ESubsUSCheckSystem.BusinessLogic.EsubsSAPCheckOutput postSAPChecks = new efundraising.ESubsUSCheckSystem.BusinessLogic.EsubsSAPCheckOutput();
            if (postSAPChecks.OutputToFileFromWeb(DateTime.Now, CountryDisplayDropdownlist.SelectedValue))
            {
                clearCurrentSession();
            }
        }

        void DisplayPaymentButton_Click(object sender, EventArgs e)
        {
            DoRefresh();
            PaymentToProcessDataGrid.DataSource = this.GetPaymentsToProcess().PaymentToProcessList;
            PaymentToProcessDataGrid.SelectedIndex = -1;
            PaymentToProcessDataGrid.DataBind();
        }

        private void PaymentInfo_Reissue(object sender, EventArgs e)
        {
            DoRefresh();
            PaymentToProcessDataGrid.DataSource = this.GetPaymentsToProcess().PaymentToProcessList;
            PaymentToProcessDataGrid.DataBind();
        }

		#endregion

		#region Event Handlers

		private void PaymentToProcessDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
            if (Accordion1.SelectedIndex == 2)
            {
                DoLinqFilter(e.NewPageIndex);
            }
            else if (Accordion1.SelectedIndex == 1 && BatchDisplayDropdownlist.SelectedValue != "-1")
            {
                List<ESubsGlobal.Payment.CheckSystemPayment> temp = this.GetPaymentsToProcess().PaymentToProcessList;
                int tmp = Int32.Parse(BatchDisplayDropdownlist.SelectedValue);
                PaymentToProcessDataGrid.DataSource = temp.Where(p => p.payment.PaymentBatchID == tmp).ToList();
                PaymentToProcessDataGrid.CurrentPageIndex = e.NewPageIndex;
                PaymentToProcessDataGrid.DataBind();
            }
            else
            {
                PaymentToProcessDataGrid.DataSource = this.GetPaymentsToProcess().PaymentToProcessList;
                PaymentToProcessDataGrid.CurrentPageIndex = e.NewPageIndex;
                PaymentToProcessDataGrid.DataBind();

            }
          
		}
		private void PaymentToProcessDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			//this.DataBind();
			// check if the event has been identified as being throw by the header
			if(e.Item.ItemType == ListItemType.Header) 
			{
				// get the link button that has been raised
				LinkButton linkButton = (LinkButton)e.CommandSource;
				
				//get the list of payments to process
				Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection payments = this.GetPaymentsToProcess();

				

					// sort by selected field
					switch(linkButton.Text) 
					{
						case "ID":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.payment.PaymentId).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.payment.PaymentId).ToList();
                            }
                            updateSortDescending(linkButton.Text);
							break;
						case "Payment Period":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.checkSystemPaymentPeriod.StartDate).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.checkSystemPaymentPeriod.StartDate).ToList();
                            }
                            updateSortDescending(linkButton.Text);
							break;
						case "Group Name":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.group.Name).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.group.Name).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
						case "Group Status":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.groupStatus.Description).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.groupStatus.Description).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
						case "Partner Name":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.partner.Name).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.partner.Name).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
						case "Payment Type":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.paymentType.PaymentTypeName).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.paymentType.PaymentTypeName).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
                        //case "Total Payment":
                        //    if (isSortDescending(linkButton.Text))
                        //    {
                        //        payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.TotalPayment).ToList();
                        //    }
                        //    else
                        //    {
                        //        payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.TotalPayment).ToList();
                        //    }
                        //    updateSortDescending(linkButton.Text);
                        //    break;
						case "Payment Amount":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.payment.PaidAmount).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.payment.PaidAmount).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
                        case "Validated":
                            if (isSortDescending(linkButton.Text))
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderByDescending(item => item.payment.IsValidated).ToList();
                            }
                            else
                            {
                                payments.PaymentToProcessList = payments.PaymentToProcessList.OrderBy(item => item.payment.IsValidated).ToList();
                            }
                            updateSortDescending(linkButton.Text);
                            break;
					}

                    PaymentToProcessDataGrid.DataSource = payments.PaymentToProcessList;
                    PaymentToProcessDataGrid.DataBind();
				
			}
		}
		private void PaymentToProcessDataGrid_OnSelect(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Select")
			{
				this.initPaymentInformation(e.CommandArgument.ToString());
				
			}
		}
		protected void UpdateButton_Click(object sender, System.EventArgs e)
		{
			// getting payment id of the current payment
			int payID = PaymentInformation1.GetPaymentIDTextBox();
            if (payID != int.MinValue)
            {
                Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection collection = this.GetPaymentsToProcess();

                ESubsGlobal.Payment.CheckSystemPayment currentObject = null;
                foreach (ESubsGlobal.Payment.CheckSystemPayment p in collection.PaymentToProcessList)
                {
                    if (p.payment.PaymentId == payID)
                    {
                        currentObject = p;
                        break;
                    }

                }
              
                if (currentObject != null)
                {
                    bool removecurrentObjectFromSeesion = false;
                    // getting group object
                    ESubsGlobal.GroupGroupStatus groupToInsert = ESubsGlobal.GroupGroupStatus.GetGroupGroupStatusByID(currentObject.group.GroupID);
                    int ggStatusIdWebControl = PaymentInformation1.GetGroupStatus();
                    if (groupToInsert != null && groupToInsert.GroupStatusId != ggStatusIdWebControl)
                    {
                        // getting group id
                        groupToInsert.GroupStatusId = ggStatusIdWebControl;
                        // Remove from the Session List if GroupStatus is invalid
                        switch (ggStatusIdWebControl)
                        {
                            case (int)ESubsGlobal.GroupStatusCategory.Fraud:
                            case (int)ESubsGlobal.GroupStatusCategory.OnHold:
                            case (int)ESubsGlobal.GroupStatusCategory.Closed:
                                removecurrentObjectFromSeesion = true;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        groupToInsert = null;
                    }
                    // getting payment_payment_status object
                    ESubsGlobal.Payment.PaymentPaymentStatus paymentStatusToInsert = ESubsGlobal.Payment.PaymentPaymentStatus.GetLastPaymentPaymentStatusByPaymentID(currentObject.payment.PaymentId);
                    int ppStatusIDWebControl = PaymentInformation1.GetPaymentStatus();
                    if (paymentStatusToInsert != null && paymentStatusToInsert.PaymentStatusId != ppStatusIDWebControl)
                    {
                        // getting payment_status object
                        ESubsGlobal.Payment.PaymentStatus newStatus = ESubsGlobal.Payment.PaymentStatus.GetPaymentStatusByID(PaymentInformation1.GetPaymentStatus());
                        foreach (string s in donotInsertPaymentStatus)
                        {
                            // check if the selected payment status is disallowed to insert
                            if (s.ToUpper() == newStatus.Description.ToUpper())
                            {
                                // if yes, then we will abort the update operation with warning notification
                                Response.Write("<script>window.alert('This type of payment status cannot be inserted! Press OK to continue')</script>");
                                return;
                            }
                        }
                        // getting group status id
                        paymentStatusToInsert.PaymentStatusId = ppStatusIDWebControl;
                        // Remove from the Session List if PaymentStatus is no more longer In Process
                        if (ppStatusIDWebControl != (int)ESubsGlobal.Payment.PaymentStatusCategory.InProcess)
                            removecurrentObjectFromSeesion = true;
                    }
                    else
                    {
                        paymentStatusToInsert = null;
                    }
                    // getting payment_info object
                    ESubsGlobal.Payment.Payment paymentToBeUpdated = ESubsGlobal.Payment.Payment.GetPaymentByID(payID);
                    if (paymentToBeUpdated != null)
                    {

                        paymentToBeUpdated.Name = PaymentInformation1.GetAttentionOfTextBox();
                        paymentToBeUpdated.Address1 = PaymentInformation1.GetAddress1TextBox();
                        paymentToBeUpdated.Address2 = PaymentInformation1.GetAddress2TextBox();
                        paymentToBeUpdated.City = PaymentInformation1.GetCityTextBox();
                        paymentToBeUpdated.ZipCode = PaymentInformation1.GetZipTextBox().ToString();
                        paymentToBeUpdated.SubdivisionCode = PaymentInformation1.GetSubdivisionCode();
                        // reading the first two letters of subdivision code which is country
                        string country = PaymentInformation1.GetSubdivisionCode().Substring(0, 2);
                        paymentToBeUpdated.CountryCode = ESubsGlobal.Common.CountryCode.CreateByName(country).Code;
                        if (paymentToBeUpdated.IsValidated != PaymentInformation1.GetValidatedCheckBox())
                        {
                            paymentToBeUpdated.IsValidated = PaymentInformation1.GetValidatedCheckBox();
                            removecurrentObjectFromSeesion = true;
                        }
                    }

                    // update is performed in the transaction
                    ESubsGlobal.TransactionController tk = new efundraising.ESubsGlobal.TransactionController();
                    // getting result of the transaction (tables that are affected are: group_group_status (update), payemnt_payment_status (update), payment_info (insert), payment (update)
                    ESubsGlobal.PaymentGroupPaymentInfoStatus result = tk.UpdatePaymentGroupPaymentInfo(paymentStatusToInsert, groupToInsert, paymentToBeUpdated);
                    if (result == ESubsGlobal.PaymentGroupPaymentInfoStatus.OK)
                    {
                        Components.Server.User.CrmUser crmUser =
                            Components.Server.User.CrmUser.Create(Session);
                        if (crmUser != null)
                            efundraising.Diagnostics.Logger.LogInfo(string.Format("ValidatePayments:This user {0} is updating the payment {1} ", crmUser.ID, paymentToBeUpdated.PaymentId));

                        // Refresh 
                        if (removecurrentObjectFromSeesion)
                            collection.PaymentToProcessList.Remove(currentObject);
                        DoRefresh();
                        // removing collection from the session
                        PaymentToProcessDataGrid.DataSource = this.GetPaymentsToProcess().PaymentToProcessList;
                        PaymentToProcessDataGrid.DataBind();
                    }

                }
            }
			
			
		}


		protected void ViewHistoricButton_Click(object sender, System.EventArgs e)
		{
			
			if(PaymentInformation1.GetPaymentIDTextBox()!= int.MinValue)
			{
				// pointer the single object in collection 
                ESubsGlobal.Payment.CheckSystemPayment currentObject = null;
				// getting the current collection from session
                Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection tempCol = this.GetPaymentsToProcess();
				// looking for object in the collection that contains info corresponding to the data for payment info in the user control
                foreach (ESubsGlobal.Payment.CheckSystemPayment s in tempCol.PaymentToProcessList)
				{
					if(s.payment.PaymentId == PaymentInformation1.GetPaymentIDTextBox())
					{
						// if such object is found we redirect the pointer 
						currentObject = s;
						break;
					}
				}
				if(currentObject.paymentInfo.EventID != int.MinValue)
				{
					// we are transfering control to the history page with event id as url parameter
                    Server.Transfer(historyTransfer + currentObject.paymentInfo.EventID.ToString());
				}
			}
		}
		#endregion

		#region Helper Methods
		// Make a method that uses Components.Server.CurrentWorkingObject to save collection object
		// into the session or cache so that the database do not get hit at every postback
        private Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection GetPaymentsToProcess() 
		{
			// get the traditional partners from the current working object manager
            Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection sourceCollection = Components.Server.CurrentWorkingObject.Get(Session, "PAYMENT_PROCESS") as Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection;
                //(sourceCollection, Session, "PAYMENT_PROCESS", null);

			// if null, then load it from the datasource
			// if first time this page is hit, reload it because it might be updated 
			// by "More" pages 
			if(sourceCollection == null) 
			{
				// instantiate the object
				sourceCollection = new Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection();

				
			
                // get all payments to process
                if (getDisplayMode() == int.MinValue || getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Validate)
                {
                    sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Validate, CountryDisplayDropdownlist.SelectedValue);
                }
                else if (getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Approve)
                {
                    sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Approve, string.Empty);
                }
                else if (getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Search)
                {
                    sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Search, CountryDropdownlist.SelectedValue);
                }
                else
                {
                    throw new Exception("Unexpected Display Mode in the GetPaymentToProcess function".ToString());
                }
    	

				// save it for futher usage to the current working object
				Components.Server.CurrentWorkingObject.Save(sourceCollection, Session, "PAYMENT_PROCESS", null);
			}
			
			return sourceCollection;
		}

        private void setDisplayMode(int input)
        {
            Components.Server.DataGrid.PaymentToProcess.DisplayMode currentMode = (Components.Server.DataGrid.PaymentToProcess.DisplayMode)input;
            Components.Server.CurrentWorkingObject.Save(currentMode, Session, "DISPLAY_MODE", null);
        }

        private int getDisplayMode()
        {
            if (Session["DISPLAY_MODE"] == null)
            {
                return int.MinValue;
            }
            return (int)Components.Server.CurrentWorkingObject.Get(Session, "DISPLAY_MODE");
        }

		private void DoRefresh()
		{
            // instantiate the object
            Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection sourceCollection = new Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection();
                        
            // get all payments to process
            if (getDisplayMode() == int.MinValue || getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Validate)
            {
                sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Validate, CountryDisplayDropdownlist.SelectedValue);
            }
            else if (getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Approve)
            {
                sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Approve, string.Empty);
            }
            else if (getDisplayMode() == (int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Search)
            {
                sourceCollection.LoadPaymentsToProcess((int)Components.Server.DataGrid.PaymentToProcess.DisplayMode.Search, CountryDropdownlist.SelectedValue);
            }
            else
            {
                throw new Exception("Unexpected Display Mode in the GetPaymentToProcess function".ToString());
            }
    	
		   // save it for futher usage to the current working object
		  Components.Server.CurrentWorkingObject.Save(sourceCollection, Session, "PAYMENT_PROCESS", null);
		}

        private void bindDropDownList(DropDownList ddl, System.Collections.Generic.Dictionary<int, string> dic)
        {
            if(!dic.ContainsKey(-1))
            {
            dic.Add(-1, "ALL".ToString());
            }
             
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>(dic);
            temp.Sort(delegate(KeyValuePair<int, string> first, KeyValuePair<int, string> second)
            {
                return first.Value.CompareTo(second.Value);
            }
            );

            ddl.DataSource = temp;
           ddl.DataTextField = "Value";
           ddl.DataValueField = "Key";
           ddl.SelectedValue="-1".ToString();
           ddl.DataBind();
        }
		/// <summary>
		///  this function setting values for controls in the payment information user control
		/// </summary>
		/// <param name="id">payment id of the selected row</param>
		private void initPaymentInformation(string id)
		{
			//  creating pointer representing a single object in the collection
            ESubsGlobal.Payment.CheckSystemPayment currentPayment = null;
			// getting current collection from the session
            Components.Server.DataGrid.PaymentToProcess.PaymentToProcessListItemCollection tempCollection  = this.GetPaymentsToProcess();
			
			// looking for particular object in the collection that represent the selected row
            foreach (ESubsGlobal.Payment.CheckSystemPayment p in tempCollection.PaymentToProcessList)
			{
				if(p.payment.PaymentId == Convert.ToInt32(id))
				{
					// if found, redirecting pointer to the object
					currentPayment = p;

					break;
				}
			}
			if(currentPayment != null)
			{
				// this section setting values for controls in the user control
				PaymentInformation1.SetPaymentIDTextBox(currentPayment.payment.PaymentId);
				// if payment is for the group, we are displaying event name, event id
				// if payment is for partner, we are displaying partner name, partner id  
                ESubsGlobal.Payment.PartnerPaymentConfig partnerPaymentConf = ESubsGlobal.Payment.PartnerPaymentConfig.GetPartnerPaymentConfigByID(currentPayment.partner.PartnerID);

                if (partnerPaymentConf != null)
                {
                    if (partnerPaymentConf.PaymentTo== 0)
                    {
                        PaymentInformation1.SetEventPartnerNameTextBox(currentPayment.group.Name);
                        PaymentInformation1.SetEventPartnerIDTextBox(currentPayment.paymentInfo.EventID);
                    }
                    else
                    {
                        PaymentInformation1.SetEventPartnerNameTextBox(currentPayment.payment.Name);
                        PaymentInformation1.SetEventPartnerIDTextBox(currentPayment.partner.PartnerID);
                    }
                }// Get the current year payment
				DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
				double currentYearPayment = 
					Convert.ToDouble(ESubsGlobal.Payment.Payment.GetTotalPayment(currentPayment.payment.PaymentId, currentPayment.paymentInfo.EventID, startDate, DateTime.Now, null));

				PaymentInformation1.SetPartner(currentPayment.partner.PartnerID);
				PaymentInformation1.SetPaymentType(currentPayment.payment.PaymentTypeId);
				PaymentInformation1.SetPaymentPeriod(currentPayment.checkSystemPaymentPeriod.PaymentPeriodId);
				//PaymentInformation1.SetTotalPaymentTextBox(Convert.ToDouble(currentPayment.TotalPayment));
				PaymentInformation1.SetCurrentYearPayment(currentYearPayment);
				PaymentInformation1.SetPaymentAmountTextBox(Convert.ToDouble(currentPayment.payment.PaidAmount));
				PaymentInformation1.SetPaymentStatus(currentPayment.paymentStatus.PaymentStatusId);
				PaymentInformation1.SetGroupStatus(currentPayment.groupStatus.GroupStatusId);
                if (currentPayment.groupStatus.GroupStatusId < 0)
					PaymentInformation1.EnableGroupStatusDropdown(false);
				else
					PaymentInformation1.EnableGroupStatusDropdown(true);

				//PaymentInformation1.SetPayabaleToGroup(currentPayment.PayableToGroup);
				ESubsGlobal.Payment.Payment paymentInDB = ESubsGlobal.Payment.Payment.GetPaymentByID(currentPayment.payment.PaymentId);
				if (paymentInDB != null)
				{
					PaymentInformation1.SetAttentionOfTextBox(paymentInDB.Name);
					PaymentInformation1.SetAddress1TextBox(paymentInDB.Address1);
					PaymentInformation1.SetAddress2TextBox(paymentInDB.Address2);
					PaymentInformation1.SetCityTextBox(paymentInDB.City);
					PaymentInformation1.SetZipTextBox(paymentInDB.ZipCode);
					PaymentInformation1.SetSubdivisionCode(paymentInDB.SubdivisionCode);
				}
				//
				
				PaymentInformation1.SetGroupId(currentPayment.group.GroupID);
                PaymentInformation1.SetCheckNumber(currentPayment.payment.ChequeNumber);
                PaymentInformation1.SetValidatedCheckBox(currentPayment.payment.IsValidated);
				PaymentInformation1.Refresh();
			}
            PaymentInformation1.ShowReissueButton();
            PaymentInformation1.ShowValidatedCheckBox();
			// making control visible
			PaymentInformationPanel.Visible= true;
		}

		private void clearCurrentSession()
		{
			// removing collection from the session
			
			Components.Server.CurrentWorkingObject.Remove(Session, "PAYMENT_PROCESS");	
		}

        private void PaymentToProcessDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
            if (Session["SORT_DATAGRID"] == null)
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    foreach (TableCell  tc in e.Item.Cells)
                    {
                       foreach(Control c in tc.Controls)
                        {
                            if (c is LinkButton)
                            {
                                if(!sortDatagrid.ContainsKey((c as LinkButton).Text))
                                    sortDatagrid.Add((c as LinkButton).Text, false);
                            }
                        }
                    }
                }

                if (sortDatagrid.Count > 0)
                {
                    Session["SORT_DATAGRID"] = sortDatagrid;
                }
            }
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//
				HtmlInputHidden inputHidden = e.Item.FindControl("hpInfoActive") as HtmlInputHidden;
				if (inputHidden != null && inputHidden.Value.ToString().Trim().ToLower() == "false")
				{
					e.Item.Attributes["style"]= "color:red";
				}
			}
		}

        private void DoLinqFilter(int index)
        {

            List<ESubsGlobal.Payment.CheckSystemPayment> temp = this.GetPaymentsToProcess().PaymentToProcessList;

            
                   if (CheckTextBox.Text != string.Empty)
                   {
                       try
                       {
                           int tmp = Int32.Parse(CheckTextBox.Text);
                           temp = temp.Where(p => p.payment.ChequeNumber == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Check Number: ".ToString() + CheckTextBox.Text + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (GroupIdDropdownlist.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(GroupIdDropdownlist.SelectedValue);
                           temp = temp.Where(p => p.group.GroupID == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Group ID: ".ToString() + GroupIdDropdownlist.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (FileName.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(FileName.SelectedValue);
                           temp = temp.Where(p => p.payment.PaymentBatchID == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of File Name/ Batch ID: ".ToString() + FileName.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (PartnerTypeDropDown.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(PartnerTypeDropDown.SelectedValue);
                           temp = temp.Where(p => p.partner.PartnerID == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Partner ID: ".ToString() + PartnerTypeDropDown.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (PaymentPeriodDropDown.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(PaymentPeriodDropDown.SelectedValue);
                           temp = temp.Where(p => p.payment.PaymentPeriodId == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Payment Period: ".ToString() + PaymentPeriodDropDown.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (PaymentStatusDropdownlist.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(PaymentStatusDropdownlist.SelectedValue);
                           temp = temp.Where(p => p.paymentStatus.PaymentStatusId == tmp).ToList();
                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Payment Status: ".ToString() + PaymentStatusDropdownlist.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }

                   if (PaymentTodropdown.SelectedValue != "-1")
                   {
                       try
                       {
                           int tmp = Int32.Parse(PaymentTodropdown.SelectedValue);
                           temp = temp.Where(p => p.PaymentTo == tmp).ToList();

                       }
                       catch (Exception ex)
                       {
                           EFundraisingWebBasePage.StaticLog("Validate Payment Search: Error in conversion of Payment To Status: ".ToString() + PaymentTodropdown.SelectedValue + Environment.NewLine + ex.InnerException.ToString(), null, LogType.Warning);
                       }
                   }


                   PaymentToProcessDataGrid.DataSource = temp.Where(p => p.payment.CountryCode == CountryDropdownlist.SelectedValue).Where(p => p.payment.IsValidated == ValidatedCheckBox.Checked).ToList();
                   if (index < 0)
                   {
                       PaymentToProcessDataGrid.CurrentPageIndex = 0;
                       PaymentToProcessDataGrid.SelectedIndex = -1;
                   }
                   else
                   {
                       PaymentToProcessDataGrid.CurrentPageIndex = index;
                   }

                                
           
            PaymentToProcessDataGrid.DataBind();

            PaymentInformationPanel.Visible = false;

        }

		protected void ShowButton_Click(object sender, System.EventArgs e)
		{
		     DoLinqFilter(-1);
		}

       protected void ResetButton_Click(object sender, System.EventArgs e)
        {
            CountryDropdownlist.SelectedValue = "US".ToString();
            PaymentStatusDropdownlist.SelectedValue = GroupIdDropdownlist.SelectedValue = PaymentTodropdown.SelectedValue = FileName.SelectedValue = 
            PaymentPeriodDropDown.SelectedValue = PartnerTypeDropDown.SelectedValue = "-1".ToString();
            CheckTextBox.Text = string.Empty;
            ValidatedCheckBox.Checked = true;
        }

        #endregion

    }
}
