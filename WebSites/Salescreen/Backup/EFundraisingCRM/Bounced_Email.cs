using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class BouncedEmail: EFundraisingCRMDataObject {

		private string email;


		public BouncedEmail() : this(null) { }
		public BouncedEmail(string email) {
			this.email = email;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BouncedEmail>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"</BouncedEmail>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static BouncedEmail[] GetBouncedEmails() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBouncedEmails();
		}

		public static BouncedEmail GetBouncedEmailByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBouncedEmailByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBouncedEmail(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBouncedEmail(this);
		}*/
		#endregion

		#region Properties
		public string Email {
			set { email = value; }
			get { return email; }
		}

		#endregion
	}
}
