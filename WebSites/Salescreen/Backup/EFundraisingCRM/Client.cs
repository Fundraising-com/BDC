using System;
using System.Xml;
using efundraising.efundraisingCore;
using System.Runtime.Serialization;


namespace efundraising.EFundraisingCRM 
{
	[Serializable]
	public class Client: EFundraisingCRMDataObject, ISerializable
	{
		private string clientSequenceCode;
		private int clientId;
		private string organizationClassCode;
		private short groupTypeId;
		private string channelCode;
		private int promotionId;
		private int leadId;
		private short divisionId;
		private int csrConsultantId;
		private short titleId;
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
		private bool interestedInAgent;
		private bool interestedInOnline;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string otherPhone;
		private string otherPhoneExt;
		private ClientAddress clientBillingAddress;
		private ClientAddress clientShippingAddress;


		
		#region ISerializable Members

		//Deserialization constructor.
		public Client(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			LoadXml((String)info.GetValue("fileXML", typeof(string)));
			LoadXMLBillingClientAddress((String)info.GetValue("fileXMLBillingClientAddress", typeof(string)));
			LoadXMLShippingClientAddress((String)info.GetValue("fileXMLShippingClientAddress", typeof(string)));
		}
        
		//Serialization function.
		public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			info.AddValue("fileXML", GenerateXML());
			info.AddValue("fileXMLBillingClientAddress", GenerateBillingClientAddressXML());
			info.AddValue("fileXMLShippingClientAddress", GenerateShippingClientAddressXML());
		}


		#endregion

		public Client() : this(null) { }
		public Client(string clientSequenceCode) : this(clientSequenceCode, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId) : this(clientSequenceCode, clientId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode) : this(clientSequenceCode, clientId, organizationClassCode, short.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, short.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, int.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, short.MinValue) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, false) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, false) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent, bool interestedInOnline) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent, bool interestedInOnline, string dayPhoneExt) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent, bool interestedInOnline, string dayPhoneExt, string eveningPhoneExt) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, eveningPhoneExt, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent, bool interestedInOnline, string dayPhoneExt, string eveningPhoneExt, string otherPhone) : this(clientSequenceCode, clientId, organizationClassCode, groupTypeId, channelCode, promotionId, leadId, divisionId, csrConsultantId, titleId, salutation, firstName, lastName, title, organization, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, extraComment, interestedInAgent, interestedInOnline, dayPhoneExt, eveningPhoneExt, otherPhone, null) { }
		public Client(string clientSequenceCode, int clientId, string organizationClassCode, short groupTypeId, string channelCode, int promotionId, int leadId, short divisionId, int csrConsultantId, short titleId, string salutation, string firstName, string lastName, string title, string organization, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, string extraComment, bool interestedInAgent, bool interestedInOnline, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string otherPhoneExt) {
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

		protected string GenerateBillingClientAddressXML()
		{
			if (ClientBillingAddress != null)
				return ClientBillingAddress.GenerateXML();
			else
				return string.Empty;
		}
		protected string GenerateShippingClientAddressXML()
		{
			if (ClientShippingAddress != null)
				return ClientShippingAddress.GenerateXML();
			else
				return string.Empty;
		}

		protected void LoadXMLBillingClientAddress(string xmlText)
		{
			if (xmlText != null && xmlText.Trim() != string.Empty)
			{
				if (clientBillingAddress == null)
					clientBillingAddress = new ClientAddress();
				clientBillingAddress.LoadXml(xmlText);
			}
		}
		
		protected void LoadXMLShippingClientAddress(string xmlText)
		{
			if (xmlText != null && xmlText.Trim() != string.Empty)
			{
				if (clientShippingAddress == null)
					clientShippingAddress = new ClientAddress();
				clientShippingAddress.LoadXml(xmlText);
			}
		}


		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if( ToLowerCase(node.Name) == ToLowerCase("clientsequencecode")) {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientid")) {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationclasscode")) {
					SetXmlValue(ref organizationClassCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("grouptypeid")) {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channelcode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionid")) {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadid")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionid")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("csrconsultantid")) {
					SetXmlValue(ref csrConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("titleid")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salutation")) {
					SetXmlValue(ref salutation, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstname")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastname")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organization")) {
					SetXmlValue(ref organization, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayphone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("daytimecall")) {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningphone")) {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningtimecall")) {
					SetXmlValue(ref eveningTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("extracomment")) {
					SetXmlValue(ref extraComment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("interestedinagent")) {
					SetXmlValue(ref interestedInAgent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("interestedinonline")) {
					SetXmlValue(ref interestedInOnline, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayphoneext")) {
					SetXmlValue(ref dayPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningphoneext")) {
					SetXmlValue(ref eveningPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherphone")) {
					SetXmlValue(ref otherPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherphoneext")) {
					SetXmlValue(ref otherPhoneExt, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Client[] GetClients() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClients();
		}

		
		public static Client GetClientByID(int id, string clientSeqCode) {
			if (id == int.MinValue)
				return null;
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientByID(id, clientSeqCode);
		}

		public static Client GetClientByLeadID(int leadID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientByLeadID(leadID);
		}
		
		
		public static Client GetClientByLeadIDAndSequenceCode(int id, string clientSeqCode) 
		{
			if (id == int.MinValue)
				return null;
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientByLeadIDAndSequenceCode(id, clientSeqCode);
		}

		

		
		public static ClientAddress GetBillingClientAddressByID(int id, string clientSeqCode) 
		{
			if (id == int.MinValue)
				return null;

			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressByIdSequenceAddressType(id, clientSeqCode, "bt");
		}
		
		public static ClientAddress GetShippingClientAddressByID(int id, string clientSeqCode) 
		{
			if (id == int.MinValue)
				return null;

			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressByIdSequenceAddressType(id, clientSeqCode, "st");
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClient(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateClient(this);
		}

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

		public short GroupTypeId {
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

		public short DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public int CsrConsultantId {
			set { csrConsultantId = value; }
			get { return csrConsultantId; }
		}

		public short TitleId {
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

		public bool InterestedInAgent {
			set { interestedInAgent = value; }
			get { return interestedInAgent; }
		}

		public bool InterestedInOnline {
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

		public ClientAddress ClientBillingAddress
		{
			get
			{
				return clientBillingAddress;
			}
			set
			{
				clientBillingAddress = value;
			}
		}

		public ClientAddress ClientShippingAddress
		{
			get
			{
				return clientShippingAddress;
			}
			set
			{
				clientShippingAddress = value;
			}
		}

		#endregion
		
		#region Methods
		
		public string ToHumanReadableString ()
		{
			string clientString = "";
			
			clientString += "LeadID : \t\t" + this.LeadId + "\r\n";
			clientString += "First Name : \t\t" + this.FirstName + "\r\n";
			clientString += "Last Name : \t\t" + this.LastName + "\r\n";
			clientString += "Title : \t\t" + this.Title + "\r\n";
			clientString += "Organization : \t\t" + this.Organization + "\r\n";
			clientString += "Day Phone : \t\t" + this.DayPhone + " ex:" +this.DayPhoneExt + "\r\n";
			clientString += "Evening Phone : \t\t" + this.EveningPhone + " ex:" +this.EveningPhoneExt + "\r\n";
			clientString += "Time to Call : \t\t" + this.DayTimeCall + "\r\n";
			if (this.Fax != null)
				clientString += "Fax : \t\t" + this.Fax + "\r\n";
			clientString += "Email : \t\t" + this.Email + "\r\n";
			
			return clientString;
		}
		
		#endregion
		
		public static explicit operator Client(Lead lead)
		{
			Client client = new Client();
			
			client.GroupTypeId = lead.GroupTypeId;
			client.ChannelCode = lead.ChannelCode;
			client.PromotionId = lead.PromotionId;
			client.LeadId = lead.LeadId;
			client.DivisionId = lead.DivisionId;
			client.TitleId = lead.TitleId;
			client.Salutation = lead.Salutation;
			client.FirstName = lead.FirstName;
			client.LastName = lead.LastName;
			client.Organization	= lead.Organization;
			client.DayPhone = lead.DayPhone;
			client.DayTimeCall = lead.DayTimeCall;
			client.EveningPhone = lead.EveningPhone;
			client.EveningTimeCall = lead.EveningTimeCall;
			client.Fax = lead.Fax;
			client.Email = lead.Email;
			client.DayPhoneExt = lead.DayPhoneExt;
			client.EveningPhoneExt = lead.EveningPhoneExt;
			client.OtherPhone = lead.OtherPhone;
						
			return client;
		}
	}
}
