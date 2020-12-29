using System;
using GA.BDC.Core.efundraisingCore.DataAccess;

namespace GA.BDC.Core.efundraisingCore {

	
	/// <summary>
	/// Summary description for Consultant.
	/// </summary>
	public class Consultant
	{
		#region Constructors
		public Consultant()
		{
		}
		#endregion

		#region Fields
		private int consultantId;
		private byte divisionId;
		private int clientId;
		private string clientSequenceCode;
		private int departmentId;
		private int partnerId;
		private byte consultantTransferStatusId;
		private short territoryId;
		private int extConsultantId;
		private string name;
		private bool isAgent;
		private bool isActive;
		private string ntLogin;
		private string phoneExtension;
		private string emailAddress;
		private string homePhone;
		private string workPhone;
		private string faxNumber;
		private string tollFreePhone;
		private string mobilePhone;
		private string pagerPhone;
		private string defaultProposalText;
		private bool csrConsultant;
		private double objectives;
		private bool isAvailable;
		private string password;
		private bool kitPaid;
		private bool isFm;
		#endregion

		#region Methods

		public static Consultant GetConsultant(int id)
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetConsultant(id);
		}
		

		#endregion

		#region Properties
		public int ConsultantId
		{
			get { return this.consultantId; }
			set { this.consultantId = value; }
		}

		public byte DivisionId
		{
			get { return this.divisionId; }
			set { this.divisionId = value; }
		}

		public int ClientId
		{
			get { return this.clientId; }
			set { this.clientId = value; }
		}

		public string ClientSequenceCode
		{
			get { return this.clientSequenceCode; }
			set { this.clientSequenceCode = value; }
		}

		public int DepartmentId
		{
			get { return this.departmentId; }
			set { this.departmentId = value; }
		}

		public int PartnerId
		{
			get { return this.partnerId; }
			set { this.partnerId = value; }
		}

		public byte ConsultantTransferStatusId
		{
			get { return this.consultantTransferStatusId; }
			set { this.consultantTransferStatusId = value; }
		}

		public short TerritoryId
		{
			get { return this.territoryId; }
			set { this.territoryId = value; }
		}

		public int ExtConsultantId
		{
			get { return this.extConsultantId; }
			set { this.extConsultantId = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public bool IsAgent
		{
			get { return this.isAgent; }
			set { this.isAgent = value; }
		}

		public bool IsActive
		{
			get { return this.isActive; }
			set { this.isActive = value; }
		}

		public string NtLogin
		{
			get { return this.ntLogin; }
			set { this.ntLogin = value; }
		}

		public string PhoneExtension
		{
			get { return this.phoneExtension; }
			set { this.phoneExtension = value; }
		}

		public string EmailAddress
		{
			get { return this.emailAddress; }
			set { this.emailAddress = value; }
		}

		public string HomePhone
		{
			get { return this.homePhone; }
			set { this.homePhone = value; }
		}

		public string WorkPhone
		{
			get { return this.workPhone; }
			set { this.workPhone = value; }
		}

		public string FaxNumber
		{
			get { return this.faxNumber; }
			set { this.faxNumber = value; }
		}

		public string TollFreePhone
		{
			get { return this.tollFreePhone; }
			set { this.tollFreePhone = value; }
		}

		public string MobilePhone
		{
			get { return this.mobilePhone; }
			set { this.mobilePhone = value; }
		}

		public string PagerPhone
		{
			get { return this.pagerPhone; }
			set { this.pagerPhone = value; }
		}

		public string DefaultProposalText
		{
			get { return this.defaultProposalText; }
			set { this.defaultProposalText = value; }
		}

		public bool CsrConsultant
		{
			get { return this.csrConsultant; }
			set { this.csrConsultant = value; }
		}

		public double Objectives
		{
			get { return this.objectives; }
			set { this.objectives = value; }
		}

		public bool IsAvailable
		{
			get { return this.isAvailable; }
			set { this.isAvailable = value; }
		}

		public string Password
		{
			get { return this.password; }
			set { this.password = value; }
		}

		public bool KitPaid
		{
			get { return this.kitPaid; }
			set { this.kitPaid = value; }
		}

		public bool IsFm
		{
			get { return this.isFm; }
			set { this.isFm = value; }
		}

		#endregion
	}

}
