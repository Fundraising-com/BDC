using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOSupporterEmailSent: EFundraisingCRMDataObject {

		private int supporterEmailSentID;
		private int emailTypeID;
		private int supporterID;
		private DateTime dateSent;


		public EFOSupporterEmailSent() : this(int.MinValue) { }
		public EFOSupporterEmailSent(int supporterEmailSentID) : this(supporterEmailSentID, int.MinValue) { }
		public EFOSupporterEmailSent(int supporterEmailSentID, int emailTypeID) : this(supporterEmailSentID, emailTypeID, int.MinValue) { }
		public EFOSupporterEmailSent(int supporterEmailSentID, int emailTypeID, int supporterID) : this(supporterEmailSentID, emailTypeID, supporterID, DateTime.MinValue) { }
		public EFOSupporterEmailSent(int supporterEmailSentID, int emailTypeID, int supporterID, DateTime dateSent) {
			this.supporterEmailSentID = supporterEmailSentID;
			this.emailTypeID = emailTypeID;
			this.supporterID = supporterID;
			this.dateSent = dateSent;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOSupporterEmailSent>\r\n" +
			"	<SupporterEmailSentID>" + supporterEmailSentID + "</SupporterEmailSentID>\r\n" +
			"	<EmailTypeID>" + emailTypeID + "</EmailTypeID>\r\n" +
			"	<SupporterID>" + supporterID + "</SupporterID>\r\n" +
			"	<DateSent>" + dateSent + "</DateSent>\r\n" +
			"</EFOSupporterEmailSent>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("supporterEmailSentId")) {
					SetXmlValue(ref supporterEmailSentID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailTypeId")) {
					SetXmlValue(ref emailTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supporterId")) {
					SetXmlValue(ref supporterID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateSent")) {
					SetXmlValue(ref dateSent, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOSupporterEmailSent[] GetEFOSupporterEmailSents() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSupporterEmailSents();
		}

		public static EFOSupporterEmailSent GetEFOSupporterEmailSentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSupporterEmailSentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOSupporterEmailSent(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOSupporterEmailSent(this);
		}
		#endregion

		#region Properties
		public int SupporterEmailSentID {
			set { supporterEmailSentID = value; }
			get { return supporterEmailSentID; }
		}

		public int EmailTypeID {
			set { emailTypeID = value; }
			get { return emailTypeID; }
		}

		public int SupporterID {
			set { supporterID = value; }
			get { return supporterID; }
		}

		public DateTime DateSent {
			set { dateSent = value; }
			get { return dateSent; }
		}

		#endregion
	}
}
