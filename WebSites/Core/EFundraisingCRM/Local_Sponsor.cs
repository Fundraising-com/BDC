using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LocalSponsor: EFundraisingCRMDataObject {

		private int brandID;
		private int localSponsorID;
		private string salutation;
		private string firstName;
		private string middleInitial;
		private string lastName;
		private string title;
		private string streetAddress;
		private string cityName;
		private string stateCode;
		private string zipCode;
		private string countryCode;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string eveningTimeCall;
		private string alternatePhone;
		private string faxNumber;
		private string email;
		private DateTime approvalDate;
		private string comment;
		private int sponsorConsultantID;
		private DateTime lastContact;
		private int localSponsorStepsId;


		public LocalSponsor() : this(int.MinValue) { }
		public LocalSponsor(int brandID) : this(brandID, int.MinValue) { }
		public LocalSponsor(int brandID, int localSponsorID) : this(brandID, localSponsorID, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation) : this(brandID, localSponsorID, salutation, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName) : this(brandID, localSponsorID, salutation, firstName, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial) : this(brandID, localSponsorID, salutation, firstName, middleInitial, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, email, DateTime.MinValue) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email, DateTime approvalDate) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, email, approvalDate, null) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email, DateTime approvalDate, string comment) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, email, approvalDate, comment, int.MinValue) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email, DateTime approvalDate, string comment, int sponsorConsultantID) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, email, approvalDate, comment, sponsorConsultantID, DateTime.MinValue) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email, DateTime approvalDate, string comment, int sponsorConsultantID, DateTime lastContact) : this(brandID, localSponsorID, salutation, firstName, middleInitial, lastName, title, streetAddress, cityName, stateCode, zipCode, countryCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, alternatePhone, faxNumber, email, approvalDate, comment, sponsorConsultantID, lastContact, int.MinValue) { }
		public LocalSponsor(int brandID, int localSponsorID, string salutation, string firstName, string middleInitial, string lastName, string title, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string alternatePhone, string faxNumber, string email, DateTime approvalDate, string comment, int sponsorConsultantID, DateTime lastContact, int localSponsorStepsId) {
			this.brandID = brandID;
			this.localSponsorID = localSponsorID;
			this.salutation = salutation;
			this.firstName = firstName;
			this.middleInitial = middleInitial;
			this.lastName = lastName;
			this.title = title;
			this.streetAddress = streetAddress;
			this.cityName = cityName;
			this.stateCode = stateCode;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.dayPhone = dayPhone;
			this.dayTimeCall = dayTimeCall;
			this.eveningPhone = eveningPhone;
			this.eveningTimeCall = eveningTimeCall;
			this.alternatePhone = alternatePhone;
			this.faxNumber = faxNumber;
			this.email = email;
			this.approvalDate = approvalDate;
			this.comment = comment;
			this.sponsorConsultantID = sponsorConsultantID;
			this.lastContact = lastContact;
			this.localSponsorStepsId = localSponsorStepsId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LocalSponsor>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<LocalSponsorID>" + localSponsorID + "</LocalSponsorID>\r\n" +
			"	<Salutation>" + System.Web.HttpUtility.HtmlEncode(salutation) + "</Salutation>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<MiddleInitial>" + System.Web.HttpUtility.HtmlEncode(middleInitial) + "</MiddleInitial>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<CityName>" + System.Web.HttpUtility.HtmlEncode(cityName) + "</CityName>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<DayTimeCall>" + System.Web.HttpUtility.HtmlEncode(dayTimeCall) + "</DayTimeCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<EveningTimeCall>" + System.Web.HttpUtility.HtmlEncode(eveningTimeCall) + "</EveningTimeCall>\r\n" +
			"	<AlternatePhone>" + System.Web.HttpUtility.HtmlEncode(alternatePhone) + "</AlternatePhone>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<ApprovalDate>" + approvalDate + "</ApprovalDate>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"	<SponsorConsultantID>" + sponsorConsultantID + "</SponsorConsultantID>\r\n" +
			"	<LastContact>" + lastContact + "</LastContact>\r\n" +
			"	<LocalSponsorStepsId>" + localSponsorStepsId + "</LocalSponsorStepsId>\r\n" +
			"</LocalSponsor>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorId")) {
					SetXmlValue(ref localSponsorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salutation")) {
					SetXmlValue(ref salutation, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("middleInitial")) {
					SetXmlValue(ref middleInitial, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cityName")) {
					SetXmlValue(ref cityName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayTimeCall")) {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhone")) {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningTimeCall")) {
					SetXmlValue(ref eveningTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("alternatePhone")) {
					SetXmlValue(ref alternatePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxNumber")) {
					SetXmlValue(ref faxNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("approvalDate")) {
					SetXmlValue(ref approvalDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorConsultantId")) {
					SetXmlValue(ref sponsorConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastContact")) {
					SetXmlValue(ref lastContact, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorStepsId")) {
					SetXmlValue(ref localSponsorStepsId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LocalSponsor[] GetLocalSponsors() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsors();
		}

		public static LocalSponsor GetLocalSponsorByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLocalSponsor(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLocalSponsor(this);
		}
		#endregion

		#region Properties
		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public int LocalSponsorID {
			set { localSponsorID = value; }
			get { return localSponsorID; }
		}

		public string Salutation {
			set { salutation = value; }
			get { return salutation; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string MiddleInitial {
			set { middleInitial = value; }
			get { return middleInitial; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string CityName {
			set { cityName = value; }
			get { return cityName; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
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

		public string AlternatePhone {
			set { alternatePhone = value; }
			get { return alternatePhone; }
		}

		public string FaxNumber {
			set { faxNumber = value; }
			get { return faxNumber; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public DateTime ApprovalDate {
			set { approvalDate = value; }
			get { return approvalDate; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public int SponsorConsultantID {
			set { sponsorConsultantID = value; }
			get { return sponsorConsultantID; }
		}

		public DateTime LastContact {
			set { lastContact = value; }
			get { return lastContact; }
		}

		public int LocalSponsorStepsId {
			set { localSponsorStepsId = value; }
			get { return localSponsorStepsId; }
		}

		#endregion
	}
}
