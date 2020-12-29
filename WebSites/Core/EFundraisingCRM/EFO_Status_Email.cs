using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOStatusEmail: EFundraisingCRMDataObject {

		private int emailTypeID;
		private int statusID;


		public EFOStatusEmail() : this(int.MinValue) { }
		public EFOStatusEmail(int emailTypeID) : this(emailTypeID, int.MinValue) { }
		public EFOStatusEmail(int emailTypeID, int statusID) {
			this.emailTypeID = emailTypeID;
			this.statusID = statusID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOStatusEmail>\r\n" +
			"	<EmailTypeID>" + emailTypeID + "</EmailTypeID>\r\n" +
			"	<StatusID>" + statusID + "</StatusID>\r\n" +
			"</EFOStatusEmail>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("emailTypeId")) {
					SetXmlValue(ref emailTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("statusId")) {
					SetXmlValue(ref statusID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOStatusEmail[] GetEFOStatusEmails() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOStatusEmails();
		}

		public static EFOStatusEmail GetEFOStatusEmailByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOStatusEmailByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOStatusEmail(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOStatusEmail(this);
		}
		#endregion

		#region Properties
		public int EmailTypeID {
			set { emailTypeID = value; }
			get { return emailTypeID; }
		}

		public int StatusID {
			set { statusID = value; }
			get { return statusID; }
		}

		#endregion
	}
}
