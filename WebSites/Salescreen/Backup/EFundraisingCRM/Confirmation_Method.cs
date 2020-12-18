using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ConfirmationMethod: EFundraisingCRMDataObject {

		private int confirmationMethodID;
		private string description;


		public ConfirmationMethod() : this(int.MinValue) { }
		public ConfirmationMethod(int confirmationMethodID) : this(confirmationMethodID, null) { }
		public ConfirmationMethod(int confirmationMethodID, string description) {
			this.confirmationMethodID = confirmationMethodID;
			this.description = description;
		}

		#region Static Data
		public static ConfirmationMethod SignedAgreement {
			get { return new ConfirmationMethod(1, "Signed agreement"); }
		}

		public static ConfirmationMethod PO {
			get { return new ConfirmationMethod(2, "PO"); }
		}

		public static ConfirmationMethod CreditCard {
			get { return new ConfirmationMethod(3, "Credit card"); }
		}

		public static ConfirmationMethod AuthorizedByManagement {
			get { return new ConfirmationMethod(4, "Authorized by management"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ConfirmationMethod>\r\n" +
			"	<ConfirmationMethodID>" + confirmationMethodID + "</ConfirmationMethodID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ConfirmationMethod>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("confirmationMethodId")) {
					SetXmlValue(ref confirmationMethodID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ConfirmationMethod[] GetConfirmationMethods() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConfirmationMethods();
		}

		public static ConfirmationMethod GetConfirmationMethodByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConfirmationMethodByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConfirmationMethod(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConfirmationMethod(this);
		}
		#endregion

		#region Properties
		public int ConfirmationMethodID {
			set { confirmationMethodID = value; }
			get { return confirmationMethodID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
