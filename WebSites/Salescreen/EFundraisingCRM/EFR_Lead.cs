using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFRLead: EFundraisingCRMDataObject {

		private int leadID;
		private string firstName;
		private string lastName;
		private string organizationName;
		private string promotionDescription;
		private string leadActivityDetail;
		private string leadComment;
		private DateTime activityScheduledDate;
		private int consultantID;
		private int consultantExt;
		private int isDone;
		private string phoneNumber;
		private string phoneExtension;
		private string promotionType;
		private string secondPhoneNumber;
		private string secondPhoneExtension;


		public EFRLead() : this(int.MinValue) { }
		public EFRLead(int leadID) : this(leadID, null) { }
		public EFRLead(int leadID, string firstName) : this(leadID, firstName, null) { }
		public EFRLead(int leadID, string firstName, string lastName) : this(leadID, firstName, lastName, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName) : this(leadID, firstName, lastName, organizationName, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription) : this(leadID, firstName, lastName, organizationName, promotionDescription, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, DateTime.MinValue) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, int.MinValue) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, int.MinValue) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, int.MinValue) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, isDone, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone, string phoneNumber) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, isDone, phoneNumber, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone, string phoneNumber, string phoneExtension) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, isDone, phoneNumber, phoneExtension, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone, string phoneNumber, string phoneExtension, string promotionType) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, isDone, phoneNumber, phoneExtension, promotionType, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone, string phoneNumber, string phoneExtension, string promotionType, string sencondPhoneNumber) : this(leadID, firstName, lastName, organizationName, promotionDescription, leadActivityDetail, leadComment, activityScheduledDate, consultantID, consultantExt, isDone, phoneNumber, phoneExtension, promotionType, sencondPhoneNumber, null) { }
		public EFRLead(int leadID, string firstName, string lastName, string organizationName, string promotionDescription, string leadActivityDetail, string leadComment, DateTime activityScheduledDate, int consultantID, int consultantExt, int isDone, string phoneNumber, string phoneExtension, string promotionType, string sencondPhoneNumber, string sencondPhoneExtension) {
			this.leadID = leadID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.organizationName = organizationName;
			this.promotionDescription = promotionDescription;
			this.leadActivityDetail = leadActivityDetail;
			this.leadComment = leadComment;
			this.activityScheduledDate = activityScheduledDate;
			this.consultantID = consultantID;
			this.consultantExt = consultantExt;
			this.isDone = isDone;
			this.phoneNumber = phoneNumber;
			this.phoneExtension = phoneExtension;
			this.promotionType = promotionType;
			this.secondPhoneNumber = sencondPhoneNumber;
			this.secondPhoneExtension = sencondPhoneExtension;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFRLead>\r\n" +
			"	<LeadID>" + leadID + "</LeadID>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<OrganizationName>" + System.Web.HttpUtility.HtmlEncode(organizationName) + "</OrganizationName>\r\n" +
			"	<PromotionDescription>" + System.Web.HttpUtility.HtmlEncode(promotionDescription) + "</PromotionDescription>\r\n" +
			"	<LeadActivityDetail>" + System.Web.HttpUtility.HtmlEncode(leadActivityDetail) + "</LeadActivityDetail>\r\n" +
			"	<LeadComment>" + System.Web.HttpUtility.HtmlEncode(leadComment) + "</LeadComment>\r\n" +
			"	<ActivityScheduledDate>" + activityScheduledDate + "</ActivityScheduledDate>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<ConsultantExt>" + consultantExt + "</ConsultantExt>\r\n" +
			"	<IsDone>" + isDone + "</IsDone>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<PhoneExtension>" + System.Web.HttpUtility.HtmlEncode(phoneExtension) + "</PhoneExtension>\r\n" +
			"	<PromotionType>" + System.Web.HttpUtility.HtmlEncode(promotionType) + "</PromotionType>\r\n" +
			"	<2ndPhoneNumber>" + System.Web.HttpUtility.HtmlEncode(secondPhoneNumber) + "</2ndPhoneNumber>\r\n" +
			"	<2ndPhoneExtension>" + System.Web.HttpUtility.HtmlEncode(secondPhoneExtension) + "</2ndPhoneExtension>\r\n" +
			"</EFRLead>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationName")) {
					SetXmlValue(ref organizationName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionDescription")) {
					SetXmlValue(ref promotionDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadActivityDetail")) {
					SetXmlValue(ref leadActivityDetail, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadComment")) {
					SetXmlValue(ref leadComment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("activityScheduledDate")) {
					SetXmlValue(ref activityScheduledDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantExt")) {
					SetXmlValue(ref consultantExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDone")) {
					SetXmlValue(ref isDone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumber")) {
					SetXmlValue(ref phoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneExtension")) {
					SetXmlValue(ref phoneExtension, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionType")) {
					SetXmlValue(ref promotionType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("2ndphoneNumber")) {
					SetXmlValue(ref secondPhoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("2ndphoneExtension")) {
					SetXmlValue(ref secondPhoneExtension, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFRLead[] GetEFRLeads() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFRLeads();
		}

		public static EFRLead GetEFRLeadByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFRLeadByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFRLead(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFRLead(this);
		}
		#endregion

		#region Properties
		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string OrganizationName {
			set { organizationName = value; }
			get { return organizationName; }
		}

		public string PromotionDescription {
			set { promotionDescription = value; }
			get { return promotionDescription; }
		}

		public string LeadActivityDetail {
			set { leadActivityDetail = value; }
			get { return leadActivityDetail; }
		}

		public string LeadComment {
			set { leadComment = value; }
			get { return leadComment; }
		}

		public DateTime ActivityScheduledDate {
			set { activityScheduledDate = value; }
			get { return activityScheduledDate; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public int ConsultantExt {
			set { consultantExt = value; }
			get { return consultantExt; }
		}

		public int IsDone {
			set { isDone = value; }
			get { return isDone; }
		}

		public string PhoneNumber {
			set { phoneNumber = value; }
			get { return phoneNumber; }
		}

		public string PhoneExtension {
			set { phoneExtension = value; }
			get { return phoneExtension; }
		}

		public string PromotionType {
			set { promotionType = value; }
			get { return promotionType; }
		}

		public string SecondPhoneNumber {
			set { secondPhoneNumber = value; }
			get { return secondPhoneNumber; }
		}

		public string SecondPhoneExtension {
			set { secondPhoneExtension = value; }
			get { return secondPhoneExtension; }
		}

		#endregion
	}
}
