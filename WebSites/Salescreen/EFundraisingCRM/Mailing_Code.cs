using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class MailingCode: EFundraisingCRMDataObject {

		private int mailingCodeID;
		private string listName;
		private int listID;
		private string flyerCode;
		private DateTime launchDate;
		private string mailingCodeLabel;
		private int mailingNameID;


		public MailingCode() : this(int.MinValue) { }
		public MailingCode(int mailingCodeID) : this(mailingCodeID, null) { }
		public MailingCode(int mailingCodeID, string listName) : this(mailingCodeID, listName, int.MinValue) { }
		public MailingCode(int mailingCodeID, string listName, int listID) : this(mailingCodeID, listName, listID, null) { }
		public MailingCode(int mailingCodeID, string listName, int listID, string flyerCode) : this(mailingCodeID, listName, listID, flyerCode, DateTime.MinValue) { }
		public MailingCode(int mailingCodeID, string listName, int listID, string flyerCode, DateTime launchDate) : this(mailingCodeID, listName, listID, flyerCode, launchDate, null) { }
		public MailingCode(int mailingCodeID, string listName, int listID, string flyerCode, DateTime launchDate, string mailingCodeLabel) : this(mailingCodeID, listName, listID, flyerCode, launchDate, mailingCodeLabel, int.MinValue) { }
		public MailingCode(int mailingCodeID, string listName, int listID, string flyerCode, DateTime launchDate, string mailingCodeLabel, int mailingNameID) {
			this.mailingCodeID = mailingCodeID;
			this.listName = listName;
			this.listID = listID;
			this.flyerCode = flyerCode;
			this.launchDate = launchDate;
			this.mailingCodeLabel = mailingCodeLabel;
			this.mailingNameID = mailingNameID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<MailingCode>\r\n" +
			"	<MailingCodeID>" + mailingCodeID + "</MailingCodeID>\r\n" +
			"	<ListName>" + System.Web.HttpUtility.HtmlEncode(listName) + "</ListName>\r\n" +
			"	<ListID>" + listID + "</ListID>\r\n" +
			"	<FlyerCode>" + System.Web.HttpUtility.HtmlEncode(flyerCode) + "</FlyerCode>\r\n" +
			"	<LaunchDate>" + launchDate + "</LaunchDate>\r\n" +
			"	<MailingCodeLabel>" + System.Web.HttpUtility.HtmlEncode(mailingCodeLabel) + "</MailingCodeLabel>\r\n" +
			"	<MailingNameID>" + mailingNameID + "</MailingNameID>\r\n" +
			"</MailingCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("mailingCodeId")) {
					SetXmlValue(ref mailingCodeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listName")) {
					SetXmlValue(ref listName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listId")) {
					SetXmlValue(ref listID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("flyerCode")) {
					SetXmlValue(ref flyerCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("launchDate")) {
					SetXmlValue(ref launchDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mailingCodeLabel")) {
					SetXmlValue(ref mailingCodeLabel, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mailingNameId")) {
					SetXmlValue(ref mailingNameID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static MailingCode[] GetMailingCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMailingCodes();
		}

		public static MailingCode GetMailingCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMailingCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertMailingCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateMailingCode(this);
		}
		#endregion

		#region Properties
		public int MailingCodeID {
			set { mailingCodeID = value; }
			get { return mailingCodeID; }
		}

		public string ListName {
			set { listName = value; }
			get { return listName; }
		}

		public int ListID {
			set { listID = value; }
			get { return listID; }
		}

		public string FlyerCode {
			set { flyerCode = value; }
			get { return flyerCode; }
		}

		public DateTime LaunchDate {
			set { launchDate = value; }
			get { return launchDate; }
		}

		public string MailingCodeLabel {
			set { mailingCodeLabel = value; }
			get { return mailingCodeLabel; }
		}

		public int MailingNameID {
			set { mailingNameID = value; }
			get { return mailingNameID; }
		}

		#endregion
	}
}
