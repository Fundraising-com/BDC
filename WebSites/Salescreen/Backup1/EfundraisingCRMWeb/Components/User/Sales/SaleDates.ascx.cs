namespace EFundraisingCRMWeb.Components.User.Sales
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using efundraising.EFundraisingCRM;

	/// <summary>
	///		Summary description for SaleDates.
	/// </summary>
	public partial class SaleDates : System.Web.UI.UserControl
	{
		protected Components.User.PickDay saleDatePickDay;
		protected Components.User.PickDay paymentDueDatePickday;
		protected Components.User.PickDay schDeliveryPickday;
		protected Components.User.PickDay schShipPickday;
		protected Components.User.PickDay actDeliveryDatePickday;
		protected Components.User.PickDay actShipDatePickday;
		protected Components.User.PickDay boxReturnDatePickday;
		protected Components.User.PickDay boxReshipDatePickday;
		protected Components.User.PickDay confirmedPickday;

		private DateTime scheduledDeliveryDate;
        private string Hour;
        private string Min;
        private string Pm;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            if (!IsPostBack){
       
                //fill boxes
                cboSchedTimePM.Items.Add("AM");
                cboSchedTimePM.Items.Add("PM");
       
                for (int i = 1; i <= 12; i++)
                {
                    string hour = i.ToString();
                    cboSchedTimeHour.Items.Add(hour);

                }
                for (int j = 0; j <= 59; j = j + 5)
                {
                    string min = j.ToString();
                    if (j < 10)
                    {
                        min = "0" + min;
                    }
                    cboSchedTimeMin.Items.Add(min.ToString());
                }

                //set default values
                if (Hour == null)
                {
                    cboSchedTimeHour.SelectedValue = "12";
                }
                else
                {
                    cboSchedTimeHour.SelectedValue = Hour;
                }
                
                cboSchedTimeMin.SelectedValue = Min;
                cboSchedTimePM.SelectedValue = Pm;


            }

		}

		public void SetInfo(Sale s)
		{
			saleDatePickDay.ReadOnly = true;
			if (s != null)
			{
				saleDatePickDay.Date = s.SalesDate;
				paymentDueDatePickday.Date = s.PaymentDueDate;
             	schDeliveryPickday.Date = s.ScheduledDeliveryDate;
                string date = s.ScheduledDeliveryDate.ToUniversalTime().ToString();
                Pm = "AM";
                int posPM = date.IndexOf("PM");
                if (posPM > 1)
                {
                    Pm = "PM";
                }

                Hour = s.ScheduledDeliveryDate.Hour.ToString();
                if (Hour == "0")
                {
                    Hour = "12";
                }
                else if (Convert.ToInt32(Hour) > 12)
                {
                    Hour = (Convert.ToInt32(Hour) - 12).ToString();
                }
                Min = s.ScheduledDeliveryDate.Minute.ToString();
       

				schShipPickday.Date = s.ScheduledShipDate;
				actDeliveryDatePickday.Date = s.ActualDeliveryDate;
				actShipDatePickday.Date = s.ActualShipDate;
				boxReturnDatePickday.Date = s.BoxReturnDate.Date;
				boxReshipDatePickday.Date = s.ReshipDate;
				confirmedPickday.Date = s.ConfirmedDate;
			}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


		public DateTime ScheduledDeliveryDate
		{
			get{ return Convert.ToDateTime(schDeliveryPickday.Date);}
			set{ schDeliveryPickday.Date = value;}
		}

		public DateTime SaleDate
		{
			get{ return Convert.ToDateTime(saleDatePickDay.Date);}
			set{ saleDatePickDay.Date = value;}
		}
		
		public DateTime PaymentDueDate
		{
			get{ return Convert.ToDateTime(paymentDueDatePickday.Date);}
			set{ paymentDueDatePickday.Date = value;}
		}
		
		public DateTime ActualDeliveryDate
		{
			get{ return Convert.ToDateTime( actDeliveryDatePickday.Date);}
			set{ actDeliveryDatePickday.Date = value;}
		}
		
		public DateTime ScheduledShipDate
		{
			get{ return Convert.ToDateTime( schShipPickday.Date);}
			set{ schShipPickday.Date = value;}
		}

        public string ScheduledShipTimeHour
        {
            get { return cboSchedTimeHour.SelectedValue.ToString()     ; }
            set { cboSchedTimeHour.SelectedValue = value; }
        }
        public string ScheduledShipTimeMin
        {
            get { return cboSchedTimeMin.SelectedValue.ToString(); }
            set { cboSchedTimeMin.SelectedValue = value; }
        }
        public string ScheduledShipTimePM
        {
            get { return cboSchedTimePM.SelectedValue.ToString(); }
            set { cboSchedTimePM.SelectedValue = value; }
        }
		public DateTime ActualShipDate
		{
			get{ return Convert.ToDateTime(actShipDatePickday.Date);}
			set{ actShipDatePickday.Date = value;}
		}

		public DateTime BoxReturnDate
		{
			get{ return Convert.ToDateTime(boxReturnDatePickday.Date);}
			set{ boxReturnDatePickday.Date = value;}
		}

		public DateTime BoxReshipDate
		{
			get{ return Convert.ToDateTime(boxReshipDatePickday.Date);}
			set{ boxReshipDatePickday.Date = value;}
		}

		public DateTime ConfirmedDate
		{
			get{ return Convert.ToDateTime(confirmedPickday.Date);}
			set{ confirmedPickday.Date = value;}
		}

		public void DisableForConsultants()
		{
			paymentDueDatePickday.ReadOnly = true;
			saleDatePickDay.ReadOnly = true;
            confirmedPickday.ReadOnly = true;
		    actShipDatePickday.ReadOnly = true;
			boxReshipDatePickday.ReadOnly = true;
			boxReturnDatePickday.ReadOnly = true;
			actDeliveryDatePickday.ReadOnly = true;
			schShipPickday.ReadOnly = true;
			//schDeliveryPickday.ReadOnly =true;
		}

		
		public void DisableEverything()
		{
			saleDatePickDay.ReadOnly = true;
			paymentDueDatePickday.ReadOnly = true;
			confirmedPickday.ReadOnly = true;
			actShipDatePickday.ReadOnly = true;
			boxReshipDatePickday.ReadOnly = true;
			boxReturnDatePickday.ReadOnly = true;
			actDeliveryDatePickday.ReadOnly = false;
			schShipPickday.ReadOnly = true;
			schDeliveryPickday.ReadOnly =true;
         /*   Components.Server.ManageSaleScreen.MakeReadOnly(cboSchedTimeHour);
            Components.Server.ManageSaleScreen.MakeReadOnly(cboSchedTimeMin);
            Components.Server.ManageSaleScreen.MakeReadOnly(cboSchedTimePM);*/

		}
	}
}
