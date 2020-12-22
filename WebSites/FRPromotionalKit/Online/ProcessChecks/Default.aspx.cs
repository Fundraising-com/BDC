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
using System.Text;

using efundraising.EFundraisingCRM;
using System.Diagnostics;
using efundraising.ESubsUSCheckSystem.BusinessEntity;
using efundraising.ESubsUSCheckSystem.BusinessLogic;

namespace efundraising.EFundraisingCRMWeb.Online.ProcessChecks
{

	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : EFundraisingCrmOnlineBasePage, IPage, INoQuickToolBar
	{
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected efundraising.Web.UI.InputControls.DateTextBox DateTextBox1;
		protected efundraising.Web.UI.InputControls.DateTextBox DateTextBox2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button DPCButton;
      	protected System.Web.UI.WebControls.TextBox EventIdsTextBox;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox paymentTextBox;
		protected System.Web.UI.WebControls.TextBox MonthTextBox;
		protected System.Web.UI.WebControls.TextBox eventTextBox;
		protected System.Web.UI.WebControls.TextBox YearTextBox;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button ProcessButton;
	
		private void Page_Load(object sender, System.EventArgs e)
		{

			if (!IsPostBack)
			{
            

                
				Label1.Text = string.Empty;

				Button2.Text = "CA";
				Button2.ForeColor = System.Drawing.Color.Red;
				Session["Country"] = "CA";

				DateTextBox1.Text = "Saturday, November 1, 2010";
				DateTextBox2.Text = "Sunday, November 30, 2010";
			}

			/*SetStartDateAsPaymentPeriodEndDate();
			DateTime dateEndProcess = Convert.ToDateTime(DateTextBox1.Text);
			dateEndProcess= dateEndProcess.AddDays(1);
			DateTextBox2.Text = dateEndProcess.ToShortDateString();*/
		}

		private DateTime SetStartDateAsPaymentPeriodEndDate()
		{
			efundraising.ESubsGlobal.Payment.PaymentPeriod paymentPeriod = efundraising.ESubsGlobal.Payment.PaymentPeriod.GetLatestPaymentPeriod();
			paymentPeriod.EndDate = paymentPeriod.EndDate.AddDays(1);
			DateTime paymentStartDate = new DateTime(paymentPeriod.EndDate.Year, paymentPeriod.EndDate.Month, paymentPeriod.EndDate.Day);
			DateTextBox1.Text = paymentStartDate.ToShortDateString();
			return paymentStartDate;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{  
			this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            this.validateButton.Click += new System.EventHandler(this.ValidateButton_Click);
			this.DPCButton.Click += new System.EventHandler(this.DPCButton_Click);
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        private void ValidateButton_Click(object sender, System.EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTextBox1.Text);
            DateTime endDate = Convert.ToDateTime(DateTextBox2.Text);
            string error = "";

            //get item from paymetn file
            System.IO.StreamReader r = System.IO.File.OpenText("c://test/efr730628dpc01051001.txt");
            while (r.Peek() != -1) {
               string s = r.ReadLine();
               string id = s.Substring(340, 6);
               string amt = s.Substring(350, 5);
               string cent = s.Substring(355, 2);
               amt = amt + "." + cent;
               decimal fullAmt = Convert.ToDecimal(amt);
               int paymentId = Convert.ToInt32(id);

            
               //compare weach payment to the proc es_get_order_detail_by_event_id

               ESubsGlobal.Payment.Payment p = ESubsGlobal.Payment.Payment.GetPaymentByID(paymentId);
               if (p != null)
               {
                   int paymentInfoID = p.PaymentInfoId;
                   ESubsGlobal.Payment.PaymentInfo pi = ESubsGlobal.Payment.PaymentInfo.GetPaymentInfoByID(paymentInfoID);

                   
                  ESubsGlobal.Payment.Order[] orders =
                                 ESubsGlobal.Payment.Order.GetOrderDetailByEventId(pi.EventID,startDate,endDate, true);

                  //add up
                  decimal total = 0;
                  decimal usRate = Convert.ToDecimal(0.4);
                  decimal canRate = Convert.ToDecimal(0.37);

                  foreach (ESubsGlobal.Payment.Order o in orders)
                  {
                      total += Convert.ToDecimal(o.OrderDetailAmount);
                      if (o.FulfillmentCharge > 0)
                      {
                          total -= Convert.ToDecimal(o.FulfillmentCharge);
                      }
                      
                  }
                   //compare
                  if (Session["Country"] == "US")
                  {
                      total = total * usRate;
                  }
                  else
                  {
                      total = total * canRate;
                  }

                   //will catch partner at 50%
                  if (fullAmt != total)
                  {
                      error = "Payments do dot match. " + p.PaymentId + "  event: " +  pi.EventID;
                  }



               }
               else
               {
                  error = "Payment not found";
               }
            
                
     
            }

            r.Close();


            

        }


		private void ProcessButton_Click(object sender, System.EventArgs e)
		{

			Label1.Text = string.Empty;
			DateTime startDate = Convert.ToDateTime(DateTextBox1.Text);
			DateTime endDate = Convert.ToDateTime(DateTextBox2.Text);
			string[] eventIds = null;
			try
			{
				Session["SingleEvent"] = "0";
				if (EventIdsTextBox.Text.Trim() != string.Empty)
				{
					Session["SingleEvent"] = EventIdsTextBox.Text;
					eventIds = EventIdsTextBox.Text.Trim().Split(',');
					for (int i= 0; i < eventIds.Length; i++)
						int.Parse(eventIds[i]);
				}
			}
			catch (Exception)
			{
				throw new Exception("Event Ids must be separated by ,");
			}
			DoProcess(startDate, endDate, eventIds);
			clearCurrentSession();
		}
		private efundraising.CheckSystemLib.BusinessLogic.CheckSystemController controller = null;

		private void DoProcess(DateTime startDate, DateTime endDate, string[] eventIds)
		{
			efundraising.CheckSystemLib.BusinessEntity.CheckData checkData = new ESubsCheckData();
			if (eventIds != null && eventIds.Length > 0 )
				(checkData as ESubsCheckData).SetEventsToBeProcessed(eventIds);
			try
			{
				checkData.period.StartDate = startDate;
				checkData.period.EndDate = endDate;
				 bool bflag = checkData.LoadAccounts(); //no qsp
				if (!bflag)
				{
					Label1.Text = string.Format("There is no payment on the period between: {0} and {1}"
						, startDate.ToLongDateString(), endDate.ToLongDateString());
					return;
				}
			 
				controller = new ESubsCheckSystemController();
				checkData.OnFinish +=new efundraising.CheckSystemLib.BusinessEntity.Finish(checkData_OnFinish);
				controller.ProcessModules(checkData);
				Label1.Text = string.Format("Successful to process payments on the period between: {0} and {1}"
					, startDate.ToLongDateString(), endDate.ToLongDateString());
			
				SetStartDateAsPaymentPeriodEndDate();
			}
			catch (Exception ex)
			{
				throw new EFundraisingCRMException("Process Payments Errors", ex, checkData);
			}
		}

		#region IPage Members

		public string PageInformation {
			get {
				return "Start Processing Profit Checks";
			}
		}

		public string PageDescription {
			get {
				return "This section provide the wizard to create the checks";
			}
		}

		#endregion

		private void checkData_OnFinish(efundraising.CheckSystemLib.IModule module)
		{
//			if (controller != null)
//			{
//				ESubsCheckSystemController esubscontroller =(ESubsCheckSystemController)controller;
//				esubscontroller.Stop();
//			}
		}

		
		private void clearCurrentSession()
		{
			// removing collection from the session
			Components.Server.CurrentWorkingObject.Remove(Session, "PAYMENT_PROCESS");	
			Components.Server.CurrentWorkingObject.Remove(Session, "PAYMENT_EXCEPTIONS_SESSION_KEY");	
			
		}

  
		private void DPCButton_Click(object sender, System.EventArgs e)
		{
			//BEFORE PROCESSING
			//Always run The PRocess Checks more than once for thew multiple groups
			//CHECK FOR NO QSP


		/*	ESubsGlobal.Payment.Payment p = ESubsGlobal.Payment.Payment.GetPaymentByID(39734);
			p.ChequeNumber = 39734;
			p.Update();
			*/
			
			//pps = new ESubsGlobal.Payment.PaymentPaymentStatus();//--.GetLastPaymentPaymentStatusByPaymentID(37189);
			//ESubsGlobal.Payment.PaymentPaymentStatus pps = ESubsGlobal.Payment.PaymentPaymentStatus.GetLastPaymentPaymentStatusByPaymentID(36445);
			/*ESubsGlobal.Payment.PaymentPaymentStatus pps = new ESubsGlobal.Payment.PaymentPaymentStatus.GetLastPaymentPaymentStatusByPaymentID();
			pps.PaymentId = 39733;
			pps.PaymentStatusId = 1;
			pps.CreateDate = DateTime.Now;
			pps.Update();*/
			
			efundraising.ESubsUSCheckSystem.BusinessLogic.ESubsDPostCardCheckOutput DPostCC = new efundraising.ESubsUSCheckSystem.BusinessLogic.ESubsDPostCardCheckOutput();
			string country = Session["country"].ToString();
			if (DPostCC.OutputToFileFromWeb(DateTime.Now, country))
			{
				clearCurrentSession();
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		//////////////////////////////////////////
///           //FOR REISSUE
///        First, clear all payments that were processes    
           //2- SET Status To CANCEL 
			  //3- Regenerate PAyment for the same Period as the sale (check shoud come out w/ same amt)
			       //DONT run the whole period, just the event ID   (spceially for rpeiods before the new system was in use)
			//Verfiy periodes des vente de aout 1st a sept 15th par exemple
			 
			
			//check number(payment id)  //verify that older checks have the same payment id as checkid
			
			string[] payments = paymentTextBox.Text.Split(',');
            //Session["Period"] = "651";
			//int[] payments = {47835};
      
			//one month at a time only

			int month = Convert.ToInt32(MonthTextBox.Text);
			int year  = Convert.ToInt32(YearTextBox.Text); 
			int lastDay = 31;
			if (month == 4 || month == 6 || month == 9 || month == 11)
			{
				lastDay = 30;
			}
			else if (month == 2)
			{
				lastDay = 28;
			}

			string startDate = month +  "-1-" + year;
			string endDate = month + "-" + lastDay + "-" + year;
			//DateTime dateStart = Convert.ToDateTime("4-16-2006");
			//DateTime dateEnd = Convert.ToDateTime("5-15-2006");

			DateTime dateStart = Convert.ToDateTime(startDate);
		    DateTime dateEnd = Convert.ToDateTime(endDate);

			foreach(string id in payments)
			//for (int id = 52737; id <= 53148;id++)
			{
      			ESubsGlobal.Payment.PaymentPaymentStatus p = new ESubsGlobal.Payment.PaymentPaymentStatus();
				p.PaymentId = Convert.ToInt32(id);
				p.PaymentStatusId = 9;  //  cancel
				p.CreateDate = DateTime.Now;
			    p.Insert();


			/*	ESubsGlobal.Payment.Payment pa = ESubsGlobal.Payment.Payment.GetPaymentByID(p.PaymentId);
				pa.ChequeNumber = 1;
				pa.Update();*/


				//get check period et event i

				//process pre-payments
				//DoProcess(startDate, endDate, eventId);	
		

				//to get the event through the check no, we must look into event_id in payment info
				//string[] events = efundraising.ESubsGlobal.Event.GetEventsByPaymentID(id);
				
	        			
				//DoProcess(dateStart, dateEnd, events);
           }
			//"1087680""1089833"1089613
			

			//string[] events = {"1109395"};
			string t = eventTextBox.Text.Replace('"',' ');
            string[] events = eventTextBox.Text.Split(',');
		    DoProcess(dateStart, dateEnd, events);
/*
			--get check period
--get check period
SELECT  CONVERT(varchar(50),pp.start_date,101) as start_date,  CONVERT(varchar(50),pp.end_date,101) as end_date
		  	, p.cheque_number
			,pi.event_id
                         , p.paid_amount
			, [pi].payment_name
			, [pi].payment_info_id as PaymentInfoID
			,  p.payment_id as PaymentID
	FROM  dbo.payment_info [pi] INNER JOIN
          dbo.payment p ON [pi].payment_info_id = p.payment_info_id INNER JOIN
          dbo.payment_period pp ON p.payment_period_id = pp.payment_period_id
	WHERE p.cheque_number in (37808,38710,38903,38520,39197,39372,38520,38808,39410,39585,38662,37284,39606,36941,39501,38983)
*/
			
/////////////////////////////////////////////
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (Button2.Text == "CA")
			{
				Button2.Text = "US";
				Button2.ForeColor = System.Drawing.Color.Blue;
				Session["Country"] = "US";
			}
			else
			{
				Button2.Text = "CA";
				Button2.ForeColor = System.Drawing.Color.Red;
				Session["Country"] = "CA";
			}
		}

      
        
	}
}
