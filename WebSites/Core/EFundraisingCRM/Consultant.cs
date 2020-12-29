using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Consultant: EFundraisingCRMDataObject {

		private int consultantId;
		private short divisionId;
		private int clientId;
		private string clientSequenceCode;
		private int departmentId;
		private int partnerId;
		private short consultantTransferStatusId;
		private int territoryId;
		private int extConsultantId;
		private string name;
		private int isAgent;
		private int isActive;
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
		private int csrConsultant;
		private float objectives;
		private int isAvailable;
		private string password;
		private int kitPaid;
		private int isFm;
		private DateTime createDate;

		protected static System.Collections.Hashtable PropertyInfoHashtable = new System.Collections.Hashtable();
		protected static object PROPERTYINFO_OBJECT_CACHE_LOCK = new Object();


		public Consultant() : this(int.MinValue) { }
		public Consultant(int consultantId) : this(consultantId, short.MinValue) { }
		public Consultant(int consultantId, short divisionId) : this(consultantId, divisionId, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId) : this(consultantId, divisionId, clientId, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode) : this(consultantId, divisionId, clientId, clientSequenceCode, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, short.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, float.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, objectives, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives, int isAvailable) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, objectives, isAvailable, null) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives, int isAvailable, string password) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, objectives, isAvailable, password, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives, int isAvailable, string password, int kitPaid) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, objectives, isAvailable, password, kitPaid, int.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives, int isAvailable, string password, int kitPaid, int isFm) : this(consultantId, divisionId, clientId, clientSequenceCode, departmentId, partnerId, consultantTransferStatusId, territoryId, extConsultantId, name, isAgent, isActive, ntLogin, phoneExtension, emailAddress, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, defaultProposalText, csrConsultant, objectives, isAvailable, password, kitPaid, isFm, DateTime.MinValue) { }
		public Consultant(int consultantId, short divisionId, int clientId, string clientSequenceCode, int departmentId, int partnerId, short consultantTransferStatusId, int territoryId, int extConsultantId, string name, int isAgent, int isActive, string ntLogin, string phoneExtension, string emailAddress, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string defaultProposalText, int csrConsultant, float objectives, int isAvailable, string password, int kitPaid, int isFm, DateTime createDate) {
			this.consultantId = consultantId;
			this.divisionId = divisionId;
			this.clientId = clientId;
			this.clientSequenceCode = clientSequenceCode;
			this.departmentId = departmentId;
			this.partnerId = partnerId;
			this.consultantTransferStatusId = consultantTransferStatusId;
			this.territoryId = territoryId;
			this.extConsultantId = extConsultantId;
			this.name = name;
			this.isAgent = isAgent;
			this.isActive = isActive;
			this.ntLogin = ntLogin;
			this.phoneExtension = phoneExtension;
			this.emailAddress = emailAddress;
			this.homePhone = homePhone;
			this.workPhone = workPhone;
			this.faxNumber = faxNumber;
			this.tollFreePhone = tollFreePhone;
			this.mobilePhone = mobilePhone;
			this.pagerPhone = pagerPhone;
			this.defaultProposalText = defaultProposalText;
			this.csrConsultant = csrConsultant;
			this.objectives = objectives;
			this.isAvailable = isAvailable;
			this.password = password;
			this.kitPaid = kitPaid;
			this.isFm = isFm;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Consultant>\r\n" +
			"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<ClientId>" + clientId + "</ClientId>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<DepartmentId>" + departmentId + "</DepartmentId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<ConsultantTransferStatusId>" + consultantTransferStatusId + "</ConsultantTransferStatusId>\r\n" +
			"	<TerritoryId>" + territoryId + "</TerritoryId>\r\n" +
			"	<ExtConsultantId>" + extConsultantId + "</ExtConsultantId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<IsAgent>" + isAgent + "</IsAgent>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<NtLogin>" + System.Web.HttpUtility.HtmlEncode(ntLogin) + "</NtLogin>\r\n" +
			"	<PhoneExtension>" + System.Web.HttpUtility.HtmlEncode(phoneExtension) + "</PhoneExtension>\r\n" +
			"	<EmailAddress>" + System.Web.HttpUtility.HtmlEncode(emailAddress) + "</EmailAddress>\r\n" +
			"	<HomePhone>" + System.Web.HttpUtility.HtmlEncode(homePhone) + "</HomePhone>\r\n" +
			"	<WorkPhone>" + System.Web.HttpUtility.HtmlEncode(workPhone) + "</WorkPhone>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<TollFreePhone>" + System.Web.HttpUtility.HtmlEncode(tollFreePhone) + "</TollFreePhone>\r\n" +
			"	<MobilePhone>" + System.Web.HttpUtility.HtmlEncode(mobilePhone) + "</MobilePhone>\r\n" +
			"	<PagerPhone>" + System.Web.HttpUtility.HtmlEncode(pagerPhone) + "</PagerPhone>\r\n" +
			"	<DefaultProposalText>" + System.Web.HttpUtility.HtmlEncode(defaultProposalText) + "</DefaultProposalText>\r\n" +
			"	<CsrConsultant>" + csrConsultant + "</CsrConsultant>\r\n" +
			"	<Objectives>" + objectives + "</Objectives>\r\n" +
			"	<IsAvailable>" + isAvailable + "</IsAvailable>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"	<KitPaid>" + kitPaid + "</KitPaid>\r\n" +
			"	<IsFm>" + isFm + "</IsFm>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Consultant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientId")) {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientSequenceCode")) {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("departmentId")) {
					SetXmlValue(ref departmentId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantTransferStatusId")) {
					SetXmlValue(ref consultantTransferStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("territoryId")) {
					SetXmlValue(ref territoryId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("extConsultantId")) {
					SetXmlValue(ref extConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isAgent")) {
					SetXmlValue(ref isAgent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ntLogin")) {
					SetXmlValue(ref ntLogin, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneExtension")) {
					SetXmlValue(ref phoneExtension, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailAddress")) {
					SetXmlValue(ref emailAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("homePhone")) {
					SetXmlValue(ref homePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("workPhone")) {
					SetXmlValue(ref workPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxNumber")) {
					SetXmlValue(ref faxNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tollFreePhone")) {
					SetXmlValue(ref tollFreePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mobilePhone")) {
					SetXmlValue(ref mobilePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("pagerPhone")) {
					SetXmlValue(ref pagerPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("defaultProposalText")) {
					SetXmlValue(ref defaultProposalText, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("csrConsultant")) {
					SetXmlValue(ref csrConsultant, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("objectives")) {
					SetXmlValue(ref objectives, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isAvailable")) {
					SetXmlValue(ref isAvailable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("kitPaid")) {
					SetXmlValue(ref kitPaid, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isFm")) {
					SetXmlValue(ref isFm, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("createDate")) {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Consultant[] GetConsultants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultants();
		}

		public static ConsultantCollections GetCollectionConsultants() 
		{
			ConsultantCollections cntCollection = new ConsultantCollections();
			Consultant[] cts = GetConsultants();
			for (int i=0; i< cts.Length; i++)
			{
				cntCollection.Add(cts[i]);
			}
			return cntCollection;
		}

		public static Consultant GetConsultantByID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantByID(id);
		}

		public static Consultant GetConsultantByNtLogin(string ntLogin) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantByNtLogin(ntLogin);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConsultant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConsultant(this);
		}
		#endregion

		#region Properties
		public int ConsultantId {
			set { consultantId = value; }
			get { return consultantId; }
		}

		public short DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public int ClientId {
			set { clientId = value; }
			get { return clientId; }
		}

		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public int DepartmentId {
			set { departmentId = value; }
			get { return departmentId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short ConsultantTransferStatusId {
			set { consultantTransferStatusId = value; }
			get { return consultantTransferStatusId; }
		}

		public int TerritoryId {
			set { territoryId = value; }
			get { return territoryId; }
		}

		public int ExtConsultantId {
			set { extConsultantId = value; }
			get { return extConsultantId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public int IsAgent {
			set { isAgent = value; }
			get { return isAgent; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public string NtLogin {
			set { ntLogin = value; }
			get { return ntLogin; }
		}

		public string PhoneExtension {
			set { phoneExtension = value; }
			get { return phoneExtension; }
		}

		public string EmailAddress {
			set { emailAddress = value; }
			get { return emailAddress; }
		}

		public string HomePhone {
			set { homePhone = value; }
			get { return homePhone; }
		}

		public string WorkPhone {
			set { workPhone = value; }
			get { return workPhone; }
		}

		public string FaxNumber {
			set { faxNumber = value; }
			get { return faxNumber; }
		}

		public string TollFreePhone {
			set { tollFreePhone = value; }
			get { return tollFreePhone; }
		}

		public string MobilePhone {
			set { mobilePhone = value; }
			get { return mobilePhone; }
		}

		public string PagerPhone {
			set { pagerPhone = value; }
			get { return pagerPhone; }
		}

		public string DefaultProposalText {
			set { defaultProposalText = value; }
			get { return defaultProposalText; }
		}

		public int CsrConsultant {
			set { csrConsultant = value; }
			get { return csrConsultant; }
		}

		public float Objectives {
			set { objectives = value; }
			get { return objectives; }
		}

		public int IsAvailable {
			set { isAvailable = value; }
			get { return isAvailable; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public int KitPaid {
			set { kitPaid = value; }
			get { return kitPaid; }
		}

		public int IsFm {
			set { isFm = value; }
			get { return isFm; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion


	}
}
