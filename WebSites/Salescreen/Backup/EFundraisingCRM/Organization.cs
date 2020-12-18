using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Organization: EFundraisingCRMDataObject {

		private int organizationID;
		private int fSMID;
		private int flagPoleID;
		private string organizationName;
		private int organizationStatusID;
		private string address;
		private string city;
		private int organizationTypeID;
		private string zip;
		private int numberOfMembers;
		private int numberOfClassRooms;
		private string stateCode;
		private string countryCode;
		private int agentID;


		public Organization() : this(int.MinValue) { }
		public Organization(int organizationID) : this(organizationID, int.MinValue) { }
		public Organization(int organizationID, int fSMID) : this(organizationID, fSMID, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID) : this(organizationID, fSMID, flagPoleID, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName) : this(organizationID, fSMID, flagPoleID, organizationName, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, zip, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip, int numberOfMembers) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, zip, numberOfMembers, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip, int numberOfMembers, int numberOfClassRooms) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, zip, numberOfMembers, numberOfClassRooms, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip, int numberOfMembers, int numberOfClassRooms, string stateCode) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, zip, numberOfMembers, numberOfClassRooms, stateCode, null) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip, int numberOfMembers, int numberOfClassRooms, string stateCode, string countryCode) : this(organizationID, fSMID, flagPoleID, organizationName, organizationStatusID, address, city, organizationTypeID, zip, numberOfMembers, numberOfClassRooms, stateCode, countryCode, int.MinValue) { }
		public Organization(int organizationID, int fSMID, int flagPoleID, string organizationName, int organizationStatusID, string address, string city, int organizationTypeID, string zip, int numberOfMembers, int numberOfClassRooms, string stateCode, string countryCode, int agentID) {
			this.organizationID = organizationID;
			this.fSMID = fSMID;
			this.flagPoleID = flagPoleID;
			this.organizationName = organizationName;
			this.organizationStatusID = organizationStatusID;
			this.address = address;
			this.city = city;
			this.organizationTypeID = organizationTypeID;
			this.zip = zip;
			this.numberOfMembers = numberOfMembers;
			this.numberOfClassRooms = numberOfClassRooms;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.agentID = agentID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Organization>\r\n" +
			"	<OrganizationID>" + organizationID + "</OrganizationID>\r\n" +
			"	<FSMID>" + fSMID + "</FSMID>\r\n" +
			"	<FlagPoleID>" + flagPoleID + "</FlagPoleID>\r\n" +
			"	<OrganizationName>" + System.Web.HttpUtility.HtmlEncode(organizationName) + "</OrganizationName>\r\n" +
			"	<OrganizationStatusID>" + organizationStatusID + "</OrganizationStatusID>\r\n" +
			"	<Address>" + System.Web.HttpUtility.HtmlEncode(address) + "</Address>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<OrganizationTypeID>" + organizationTypeID + "</OrganizationTypeID>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<NumberOfMembers>" + numberOfMembers + "</NumberOfMembers>\r\n" +
			"	<NumberOfClassRooms>" + numberOfClassRooms + "</NumberOfClassRooms>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<AgentID>" + agentID + "</AgentID>\r\n" +
			"</Organization>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizationId")) {
					SetXmlValue(ref organizationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fsmId")) {
					SetXmlValue(ref fSMID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("flagPoleId")) {
					SetXmlValue(ref flagPoleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationName")) {
					SetXmlValue(ref organizationName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationStatusId")) {
					SetXmlValue(ref organizationStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("address")) {
					SetXmlValue(ref address, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("numberOfMembers")) {
					SetXmlValue(ref numberOfMembers, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("numberOfClassRooms")) {
					SetXmlValue(ref numberOfClassRooms, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("agentId")) {
					SetXmlValue(ref agentID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Organization[] GetOrganizations() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizations();
		}

		public static Organization GetOrganizationByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetOrganizationByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertOrganization(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateOrganization(this);
		}
		#endregion

		#region Properties
		public int OrganizationID {
			set { organizationID = value; }
			get { return organizationID; }
		}

		public int FSMID {
			set { fSMID = value; }
			get { return fSMID; }
		}

		public int FlagPoleID {
			set { flagPoleID = value; }
			get { return flagPoleID; }
		}

		public string OrganizationName {
			set { organizationName = value; }
			get { return organizationName; }
		}

		public int OrganizationStatusID {
			set { organizationStatusID = value; }
			get { return organizationStatusID; }
		}

		public string Address {
			set { address = value; }
			get { return address; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public int OrganizationTypeID {
			set { organizationTypeID = value; }
			get { return organizationTypeID; }
		}

		public string Zip {
			set { zip = value; }
			get { return zip; }
		}

		public int NumberOfMembers {
			set { numberOfMembers = value; }
			get { return numberOfMembers; }
		}

		public int NumberOfClassRooms {
			set { numberOfClassRooms = value; }
			get { return numberOfClassRooms; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public int AgentID {
			set { agentID = value; }
			get { return agentID; }
		}

		#endregion
	}
}
