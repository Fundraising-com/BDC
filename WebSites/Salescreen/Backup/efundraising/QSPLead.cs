using System;
using efundraising.efundraisingCore.DataAccess;

namespace efundraising.efundraisingCore
{
	/// <summary>
	/// Summary description for QSPLead.
	/// </summary>
	public class QSPLead
	{
		int leadGenId = int.MinValue;
		string firstName = null;
		string lastName = null;
		string title = null;
		string organization = null;
		string address1 = null;
		string address2 = null;
		string city = null;
		string state = null;
        string country = null; //adding country to class to fix CA - US problems in crm lead database
		string zip = null;
		string dayPhone = null;
		string eveningPhone = null;
		string fax = null;
		string email = null;
		string goalOfFundraisers = null;
		int noOfFundraisers = int.MinValue;
		int noOfYears = int.MinValue;
		string timePeriod = null;
		string messageToRep = null;
		string comment = null;
		byte status = byte.MinValue;
		DateTime dateEntered = DateTime.MinValue;
		string origin = null;
		string internalTrackingId = null;
		string externalTrackingId = null;
		DateTime createDate = DateTime.MinValue;
		DateTime modifyDate = DateTime.MinValue;
		string modifiedBy = null;
		bool deletedTF = false;


		public QSPLead()
		{
			
		}
		
		public static void InsertLeadGen_Lead(int leadGenId, int leadId, DateTime createDate)
		{
			EFundDatabase dbo = new EFundDatabase();
			dbo.InsertLeadGen_Lead(leadGenId, leadId, createDate);
		}

		// gets all the leads in the QSPEcommerce database (LeadGen_Info table) that are not
		// yet replicated into efundraisingprod Lead table
		public static QSPLead[] GetQSPLeadsToTransfer()
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetQSPLeadsToTransfer();
		}


		public string Address1
		{
			get { return this.address1; }
			set { this.address1 = value; }
		}

		public string Address2
		{
			get { return this.address2; }
			set { this.address2 = value; }
		}

		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		public string Comment
		{
			get { return this.comment; }
			set { this.comment = value; }
		}

		public System.DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}

		public System.DateTime DateEntered
		{
			get { return this.dateEntered; }
			set { this.dateEntered = value; }
		}

		public string DayPhone
		{
			get { return this.dayPhone; }
			set { this.dayPhone = value; }
		}

		public bool DeletedTF
		{
			get { return this.deletedTF; }
			set { this.deletedTF = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public string EveningPhone
		{
			get { return this.eveningPhone; }
			set { this.eveningPhone = value; }
		}

		public string ExternalTrackingId
		{
			get { return this.externalTrackingId; }
			set { this.externalTrackingId = value; }
		}

		public string Fax
		{
			get { return this.fax; }
			set { this.fax = value; }
		}

		public string FirstName
		{
			get { return this.firstName; }
			set { this.firstName = value; }
		}

		public string GoalOfFundraisers
		{
			get { return this.goalOfFundraisers; }
			set { this.goalOfFundraisers = value; }
		}

		public string InternalTrackingId
		{
			get { return this.internalTrackingId; }
			set { this.internalTrackingId = value; }
		}

		public string LastName
		{
			get { return this.lastName; }
			set { this.lastName = value; }
		}

		public int LeadGenId
		{
			get { return this.leadGenId; }
			set { this.leadGenId = value; }
		}

		public string MessageToRep
		{
			get { return this.messageToRep; }
			set { this.messageToRep = value; }
		}

		public string ModifiedBy
		{
			get { return this.modifiedBy; }
			set { this.modifiedBy = value; }
		}

		public System.DateTime ModifyDate
		{
			get { return this.modifyDate; }
			set { this.modifyDate = value; }
		}

		public int NoOfFundraisers
		{
			get { return this.noOfFundraisers; }
			set { this.noOfFundraisers = value; }
		}

		public int NoOfYears
		{
			get { return this.noOfYears; }
			set { this.noOfYears = value; }
		}

		public string Organization
		{
			get { return this.organization; }
			set { this.organization = value; }
		}

		public string Origin
		{
			get { return this.origin; }
			set { this.origin = value; }
		}

		public string State
		{
			get { return this.state; }
			set { this.state = value; }
		}

        public string Country
        {
            get { return this.country; }
            set { this.country = value; }
        }

		public byte Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public string TimePeriod
		{
			get { return this.timePeriod; }
			set { this.timePeriod = value; }
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		public string Zip
		{
			get { return this.zip; }
			set { this.zip = value; }
		}
	}
}
