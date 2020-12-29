using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Client: eFundraisingStoreDataObject {

		private string clientSequenceCode;
		private int clientId;
		private string organizationClassCode;
		private int groupTypeId;
		private string channelCode;
		private int promotionId;
		private int leadId;
		private int divisionId;
		private int csrConsultantId;
		private int titleId;
		private string salutation;
		private string firstName;
		private string lastName;
		private string title;
		private string organization;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string eveningTimeCall;
		private string fax;
		private string email;
		private string extraComment;
		private short interestedInAgent;
		private short interestedInOnline;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string otherPhone;
		private string otherPhoneExt;


		public Client() : this(null) { }
		public Client(string clientSequenceCode) : this(clientSequenceCode, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId) : this(clientSequenceCode, clientId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode) : this(clientSequenceCode, clientId, organizationClassCode, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, short.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, short.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent, short interestedInOnline) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent, short interestedInOnline, string dayPhoneExt) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent, short interestedInOnline, string dayPhoneExt, string eveningPhoneExt) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, eveningPhoneExt, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent, short interestedInOnline, string dayPhoneExt, string eveningPhoneExt, string otherPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, eveningPhoneExt, otherPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, int groupTypeId, string channelCode, int promotionId, int leadId, int divisionId, int csrConsultantId, int titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, short interestedInAgent, short interestedInOnline, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string otherPhoneExt) {
			this.clientSequenceCode = clientSequenceCode;
			this.clientId = clientId;
			this.organizationClassCode = organizationClassCode;
			this.groupTypeId = groupTypeId;
			this.channelCode = channelCode;
			this.promotionId = promotionId;
			this.leadId = leadId;
			this.divisionId = divisionId;
			this.csrConsultantId = csrConsultantId;
			this.titleId = titleId;
			this.salutation = salutation;
			this.firstName = firstName;
			this.lastName = lastName;
			this.title = title;
			this.organization = organization;
			this.dayPhone = dayPhone;
			this.dayTimeCall = dayTimeCall;
			this.eveningPhone = eveningPhone;
			this.eveningTimeCall = eveningTimeCall;
			this.fax = fax;
			this.email = email;
			this.extraComment = extraComment;
			this.interestedInAgent = interestedInAgent;
			this.interestedInOnline = interestedInOnline;
			this.dayPhoneExt = dayPhoneExt;
			this.eveningPhoneExt = eveningPhoneExt;
			this.otherPhone = otherPhone;
			this.otherPhoneExt = otherPhoneExt;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Client>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<ClientId>" + clientId + "</ClientId>\r\n" +
			"	<OrganizationClassCode>" + System.Web.HttpUtility.HtmlEncode(organizationClassCode) + "</OrganizationClassCode>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<CsrConsultantId>" + csrConsultantId + "</CsrConsultantId>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<Salutation>" + System.Web.HttpUtility.HtmlEncode(salutation) + "</Salutation>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<Organization>" + System.Web.HttpUtility.HtmlEncode(organization) + "</Organization>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<DayTimeCall>" + System.Web.HttpUtility.HtmlEncode(dayTimeCall) + "</DayTimeCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<EveningTimeCall>" + System.Web.HttpUtility.HtmlEncode(eveningTimeCall) + "</EveningTimeCall>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<ExtraComment>" + System.Web.HttpUtility.HtmlEncode(extraComment) + "</ExtraComment>\r\n" +
			"	<InterestedInAgent>" + interestedInAgent + "</InterestedInAgent>\r\n" +
			"	<InterestedInOnline>" + interestedInOnline + "</InterestedInOnline>\r\n" +
			"	<DayPhoneExt>" + System.Web.HttpUtility.HtmlEncode(dayPhoneExt) + "</DayPhoneExt>\r\n" +
			"	<EveningPhoneExt>" + System.Web.HttpUtility.HtmlEncode(eveningPhoneExt) + "</EveningPhoneExt>\r\n" +
			"	<OtherPhone>" + System.Web.HttpUtility.HtmlEncode(otherPhone) + "</OtherPhone>\r\n" +
			"	<OtherPhoneExt>" + System.Web.HttpUtility.HtmlEncode(otherPhoneExt) + "</OtherPhoneExt>\r\n" +
			"</Client>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "clientSequenceCode") {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(node.Name.ToLower() == "clientId") {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(node.Name.ToLower() == "organizationClassCode") {
					SetXmlValue(ref organizationClassCode, node.InnerText);
				}
				if(node.Name.ToLower() == "groupTypeId") {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "channelCode") {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(node.Name.ToLower() == "promotionId") {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(node.Name.ToLower() == "leadId") {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(node.Name.ToLower() == "divisionId") {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(node.Name.ToLower() == "csrConsultantId") {
					SetXmlValue(ref csrConsultantId, node.InnerText);
				}
				if(node.Name.ToLower() == "titleId") {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(node.Name.ToLower() == "salutation") {
					SetXmlValue(ref salutation, node.InnerText);
				}
				if(node.Name.ToLower() == "firstName") {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(node.Name.ToLower() == "lastName") {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(node.Name.ToLower() == "title") {
					SetXmlValue(ref title, node.InnerText);
				}
				if(node.Name.ToLower() == "organization") {
					SetXmlValue(ref organization, node.InnerText);
				}
				if(node.Name.ToLower() == "dayPhone") {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "dayTimeCall") {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(node.Name.ToLower() == "eveningPhone") {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "eveningTimeCall") {
					SetXmlValue(ref eveningTimeCall, node.InnerText);
				}
				if(node.Name.ToLower() == "fax") {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "extraComment") {
					SetXmlValue(ref extraComment, node.InnerText);
				}
				if(node.Name.ToLower() == "interestedInAgent") {
					SetXmlValue(ref interestedInAgent, node.InnerText);
				}
				if(node.Name.ToLower() == "interestedInOnline") {
					SetXmlValue(ref interestedInOnline, node.InnerText);
				}
				if(node.Name.ToLower() == "dayPhoneExt") {
					SetXmlValue(ref dayPhoneExt, node.InnerText);
				}
				if(node.Name.ToLower() == "eveningPhoneExt") {
					SetXmlValue(ref eveningPhoneExt, node.InnerText);
				}
				if(node.Name.ToLower() == "otherPhone") {
					SetXmlValue(ref otherPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "otherPhoneExt") {
					SetXmlValue(ref otherPhoneExt, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Client[] GetClients() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClients();
		}

		/*
		public static Client GetClientByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertClient(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateClient(this);
		}*/
		#endregion

		#region Properties
		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public int ClientId {
			set { clientId = value; }
			get { return clientId; }
		}

		public string OrganizationClassCode {
			set { organizationClassCode = value; }
			get { return organizationClassCode; }
		}

		public int GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public int CsrConsultantId {
			set { csrConsultantId = value; }
			get { return csrConsultantId; }
		}

		public int TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public string Salutation {
			set { salutation = value; }
			get { return salutation; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public string Organization {
			set { organization = value; }
			get { return organization; }
		}

		public string DayPhone {
			set { dayPhone = value; }
			get { return dayPhone; }
		}

		public string DayTimeCall {
			set { dayTimeCall = value; }
			get { return dayTimeCall; }
		}

		public string EveningPhone {
			set { eveningPhone = value; }
			get { return eveningPhone; }
		}

		public string EveningTimeCall {
			set { eveningTimeCall = value; }
			get { return eveningTimeCall; }
		}

		public string Fax {
			set { fax = value; }
			get { return fax; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string ExtraComment {
			set { extraComment = value; }
			get { return extraComment; }
		}

		public short InterestedInAgent {
			set { interestedInAgent = value; }
			get { return interestedInAgent; }
		}

		public short InterestedInOnline {
			set { interestedInOnline = value; }
			get { return interestedInOnline; }
		}

		public string DayPhoneExt {
			set { dayPhoneExt = value; }
			get { return dayPhoneExt; }
		}

		public string EveningPhoneExt {
			set { eveningPhoneExt = value; }
			get { return eveningPhoneExt; }
		}

		public string OtherPhone {
			set { otherPhone = value; }
			get { return otherPhone; }
		}

		public string OtherPhoneExt {
			set { otherPhoneExt = value; }
			get { return otherPhoneExt; }
		}

		#endregion
	}
}
