using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {
	/*
	 * This object is used to auto-create a group by using the efundraising lead information.
	 * The stored proc that is called checks if the group and the sponsor already exists
	 * in eSubs.  If so, the event partcipation id, group id, sponsor id and partner id won't
	 * be null.
	 */
    [Serializable]
	public class LeadInformation : GA.BDC.Core.BusinessBase.BusinessBase {
		private const string SESSION_KEY = "_LEAD_INFORMATION_KEY_";

		private int _eventParticipationID = int.MinValue;
        private int _groupID = int.MinValue;
        private int _sponsorID = int.MinValue;
        private int _partnerID = int.MinValue;
        private int _leadID = int.MinValue;
		private string _name;
		private string _groupName;
		private string _address;
		private string _city;
		private string _subdivisionCode;
		private string _countryCode;
		private string _zipCode;
		private string _dayPhone;
		private string _email;
        private int _expectedMembership = int.MinValue;
		private string _groupUrl;
        private int _consultantID;

		#region Contructors
		public LeadInformation() : this(int.MinValue) { }
		public LeadInformation(int eventParticipationID) : this(eventParticipationID, int.MinValue) { }
		public LeadInformation(int eventParticipationID, int groupID) : this(eventParticipationID, groupID, int.MinValue) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID) : this(eventParticipationID, groupID, sponsorID, int.MinValue) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID) : this(eventParticipationID, groupID, sponsorID, partnerID, int.MinValue) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, countryCode, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode, string zipCode) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, countryCode, zipCode, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode, string zipCode, string dayPhone) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, countryCode, zipCode, dayPhone, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode, string zipCode, string dayPhone, string email) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, countryCode, zipCode, dayPhone, email, int.MinValue) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode, string zipCode, string dayPhone, string email, int expectedMembership) : this(eventParticipationID, groupID, sponsorID, partnerID, leadID, name, groupName, address, city, subdivisionCode, countryCode, zipCode, dayPhone, email, expectedMembership, null) { }
		public LeadInformation(int eventParticipationID, int groupID, int sponsorID, int partnerID, int leadID, string name, string groupName, string address, string city, string subdivisionCode, string countryCode, string zipCode, string dayPhone, string email, int expectedMembership, string groupUrl) {
			_eventParticipationID = eventParticipationID;
			_groupID = groupID;
			_sponsorID = sponsorID;
			_partnerID = partnerID;
			_leadID = leadID;
			_name = name;
			_groupName = groupName;
			_address = address;
			_city = city;
			_subdivisionCode = subdivisionCode;
			_countryCode = countryCode;
			_zipCode = zipCode;
			_dayPhone = dayPhone;
			_email = email;
			_expectedMembership = expectedMembership;
			_groupUrl = groupUrl;
		}
		#endregion

		public void Save(System.Web.SessionState.HttpSessionState session) {
			session[SESSION_KEY] = this;
		}

		public static LeadInformation Create(System.Web.SessionState.HttpSessionState session) {
			if(session[SESSION_KEY] != null) {
				return (LeadInformation)session[SESSION_KEY];
			}
			return null;
		}

		#region Retreive Lead Information Methods

		public static LeadInformation GetLeadInformation(int leadID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLeadInformation(leadID);
		}

		#endregion

		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			return "<LeadInformation>\r\n" +
				"	<EventParticipationID>" + _eventParticipationID + "</EventParticipationID>\r\n" +
				"	<GroupID>" + _groupID + "</GroupID>\r\n" +
				"	<SponsorID>" + _sponsorID + "</SponsorID>\r\n" +
				"	<PartnerID>" + _partnerID + "</PartnerID>\r\n" +
				"	<LeadID>" + _leadID + "</LeadID>\r\n" +
				"	<Name>" + _name + "</Name>\r\n" +
				"	<GroupName>" + _groupName + "</GroupName>\r\n" +
				"	<Address>" + _address + "</Address>\r\n" +
				"	<City>" + _city + "</City>\r\n" +
				"	<SubdivisionCode>" + _subdivisionCode + "</SubdivisionCode>\r\n" +
				"	<CountryCode>" + _countryCode + "</CountryCode>\r\n" +
				"	<ZipCode>" + _zipCode + "</ZipCode>\r\n" +
				"	<DayPhone>" + _dayPhone + "</DayPhone>\r\n" +
				"	<Email>" + _email + "</Email>\r\n" +
				"	<ExpectedMembership>" + _expectedMembership + "</ExpectedMembership>\r\n" +
				"	<GroupUrl>" + _groupUrl + "</GroupUrl>\r\n" +
                "	<Consultant>" + _consultantID + "</Consultant>\r\n" +
				"</LeadInformation>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = val;
		}
		
		private void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "eventparticipationid") {
					SetXmlValue(ref _eventParticipationID, node.InnerText);
				}
				if(node.Name.ToLower() == "groupid") {
					SetXmlValue(ref _groupID, node.InnerText);
				}
				if(node.Name.ToLower() == "sponsorid") {
					SetXmlValue(ref _sponsorID, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerid") {
					SetXmlValue(ref _partnerID, node.InnerText);
				}
				if(node.Name.ToLower() == "leadid") {
					SetXmlValue(ref _leadID, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref _name, node.InnerText);
				}
				if(node.Name.ToLower() == "groupname") {
					SetXmlValue(ref _groupName, node.InnerText);
				}
				if(node.Name.ToLower() == "address") {
					SetXmlValue(ref _address, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref _city, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisioncode") {
					SetXmlValue(ref _subdivisionCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countrycode") {
					SetXmlValue(ref _countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "zipcode") {
					SetXmlValue(ref _zipCode, node.InnerText);
				}
				if(node.Name.ToLower() == "dayphone") {
					SetXmlValue(ref _dayPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref _email, node.InnerText);
				}
				if(node.Name.ToLower() == "expectedmembership") {
					SetXmlValue(ref _expectedMembership, node.InnerText);
				}
				if(node.Name.ToLower() == "groupurl") {
					SetXmlValue(ref _groupUrl, node.InnerText);
				}
                if (node.Name.ToLower() == "consultantid")
                {
                    SetXmlValue(ref _consultantID, node.InnerText);
                }
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Properties
		public int EventParticipationID {
			set { _eventParticipationID = value; }
			get { return _eventParticipationID; }
		}

		public int GroupID {
			set { _groupID = value; }
			get { return _groupID; }
		}

		public int SponsorID {
			set { _sponsorID = value; }
			get { return _sponsorID; }
		}

		public int PartnerID {
			set { _partnerID = value; }
			get { return _partnerID; }
		}

		public int LeadID {
			set { _leadID = value; }
			get { return _leadID; }
		}

		public string Name {
			set { _name = value; }
			get { return _name; }
		}

		public string GroupName {
			set { _groupName = value; }
			get { return _groupName; }
		}

		public string Address {
			set { _address = value; }
			get { return _address; }
		}

		public string City {
			set { _city = value; }
			get { return _city; }
		}

		public string SubdivisionCode {
			set { _subdivisionCode = value; }
			get { return _subdivisionCode; }
		}

		public string CountryCode {
			set { _countryCode = value; }
			get { return _countryCode; }
		}

		public string ZipCode {
			set { _zipCode = value; }
			get { return _zipCode; }
		}

		public string DayPhone {
			set { _dayPhone = value; }
			get { return _dayPhone; }
		}

		public string Email {
			set { _email = value; }
			get { return _email; }
		}

		public int ExpectedMembership {
			set { _expectedMembership = value; }
			get { return _expectedMembership; }
		}

		public string GroupUrl {
			set { _groupUrl = value; }
			get { return _groupUrl; }
		}

        public int ConsultantID
        {
            set { _consultantID = value; }
            get { return _consultantID; }
        }

		#endregion
	}
}
