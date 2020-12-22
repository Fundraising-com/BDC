using System;
using efundraising.Core;
using efundraising.Data.Sql;

namespace efundraising.EFundraisingCRM 
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	
	

	public class CreditCheckRequest : EFundraisingCRMDataObject
	{

        private int leadID;
		private int creditCheckID;
		private int consultantID;
        private DateTime requestDate;
		private DateTime orderDate;
		private double amountRequested;
		private int creditStatusID;
        private int creditScore;
		private double amountApproved;
		private string lastName;
		private string firstName;
		private string midInit;
		private string address;
		private string city;
		private string state;
		private string zip;
		private string ssn;
		private DateTime resultDate;
		private DateTime resultConfirmationDate;
        private int reason;
		private string creditReport;
	
		public CreditCheckRequest() : this(int.MinValue) { }
		public CreditCheckRequest(int leadID) : this(leadID, int.MinValue) { }
		public CreditCheckRequest(int leadID, int creditCheckID) : this(leadID, creditCheckID, int.MinValue) { }
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID) : this(leadID, creditCheckID, consultantID, DateTime.MinValue) { }
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate) : this(leadID, creditCheckID, consultantID, requestDate, DateTime.MinValue) { }		
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, double.MinValue) { }		
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, int.MinValue) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, int.MinValue) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, double.MinValue) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, null) { }		
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, null) { }		
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, null) { }		
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, null) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, null) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, null) { }		
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, null) { }	
	    public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, zip, null) { }	
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip, string ssn) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, zip, ssn, DateTime.MinValue) { }	
        public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip, string ssn, DateTime resultDate) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, zip, ssn, resultDate, DateTime.MinValue) { }	
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip, string ssn, DateTime resultDate, DateTime resultConfirmationDate) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, zip, ssn, resultDate, resultConfirmationDate, int.MinValue) { }	
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip, string ssn, DateTime resultDate, DateTime resultConfirmationDate, int reason) : this(leadID, creditCheckID, consultantID, requestDate, orderDate, amountRequested, creditStatusID, creditScore, amountApproved, lastName, firstName, midInit, address, city, state, zip, ssn, resultDate, resultConfirmationDate, reason, null) { }	
		public CreditCheckRequest(int leadID, int creditCheckID, int consultantID, DateTime requestDate, DateTime orderDate, double amountRequested, int creditStatusID, int creditScore, double amountApproved, string lastName, string firstName, string midInit, string address, string city, string state, string zip, string ssn, DateTime resultDate, DateTime resultConfirmationDate, int reason, string creditReport) 
		{
			this.LeadID = leadID;
			this.creditCheckID = creditCheckID;
			this.ConsultantID = consultantID;
			this.RequestDate = requestDate;
			this.orderDate = orderDate;
			this.AmountRequested = AmountRequested;
			this.CreditStatusID = CreditStatusID;
			this.CreditScore = CreditScore;
			this.AmountApproved = AmountApproved;
			this.LastName = lastName;
			this.FirstName = firstName;
			this.midInit = midInit;
			this.address = address;
			this.city = city;
			this.State = state;
			this.zip = zip;
			this.ssn = ssn;
			this.ResultDate = ResultDate;
			this.resultConfirmationDate = resultConfirmationDate;
			this.reason	= reason;
			this.creditReport = creditReport;
		}


		#region Data Source Methods
	 	public static int UpdateCreditCheckRequest(CreditCheckRequest ccr) 
		{
      		DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return  db.UpdateCreditCheckRequest(ccr);
		}
		

		public static CreditCheckRequest[] GetCreditCheckRequestByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return  db.GetCreditCheckRequestByLeadID(leadID);
		}
        public static CreditCheckRequest GetCreditCheckRequestByLeadIDLast(int leadID)
        {
            DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
            return db.GetCreditCheckRequestByLeadIDLast(leadID);
        }

		public static CreditCheckRequest GetCreditCheckRequestByID(int creditCheckID)
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return  db.GetCreditCheckRequestByID(creditCheckID);
		}

		public static CreditCheckRequest[] GetCreditCheckRequestUnconfirmed()
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return  db.GetCreditCheckRequestUnconfirmed();
		}

		public static CreditCheckRequest[] GetCreditCheckRequestAwaitingOrder()
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return db.GetCreditCheckRequestAwaitingOrder();
		}

		public static CreditCheckRequest[] GetCreditCheckRequestProcessed()
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return  db.GetCreditCheckRequestProcessed();
		}

		public int Insert()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCreditCheckRequest(this);
		}
		#endregion

		
		#region Properties


		public int CreditCheckID
		{
			set { creditCheckID = value; }
			get { return creditCheckID; }
		}

		public int LeadID 
		{
			set { leadID = value; }
			get { return leadID; }
		}

		public int ConsultantID 
		{
			set { consultantID = value; }
			get { return consultantID; }
		}


		public DateTime RequestDate
		{
			set { requestDate = value; }
			get { return requestDate; }
		}

		public DateTime OrderDate
		{
			set { orderDate = value; }
			get { return orderDate; }
		}

		public double AmountRequested
		{
			set { amountRequested = value; }
			get { return amountRequested; }
		}

		public int CreditStatusID 
		{
			set { creditStatusID = value; }
			get { return creditStatusID; }
		}

		public int CreditScore
		{
			set { creditScore = value; }
			get { return creditScore; }
		}

		public double AmountApproved
		{
			set { amountApproved = value; }
			get { return amountApproved; }
		}

		public string LastName
		{
			set { lastName = value; }
			get { return lastName; }
		}

		public string FirstName
		{
			set { firstName = value; }
			get { return firstName; }
		}

		public string MidInit
		{
			set { midInit = value; }
			get { return midInit; }
		}

		public string Address
		{
			set { address = value; }
			get { return address; }
		}

		public string City 
		{
			set { city = value; }
			get { return city; }
		}

		public string State 
		{
			set { state = value; }
			get { return state; }
		}

	
		public string Zip 
		{
			set { zip = value; }
			get { return zip; }
		}


		public string SSN
		{
			set { ssn = value; }
			get { return ssn; }
		}

		public DateTime ResultDate
		{
			set { resultDate = value; }
			get { return resultDate; }
		}

		public DateTime ResultConfirmationDate
		{
			set { resultConfirmationDate = value; }
			get { return resultConfirmationDate; }
		}
		
		public int Reason
		{
			set { reason = value; }
			get { return reason; }
		}
		
		public string CreditReport
		{
			set { creditReport = value; }
			get { return creditReport; }
		}

		#endregion

	}
}
