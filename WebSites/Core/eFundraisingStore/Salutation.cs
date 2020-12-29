using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Salutation: eFundraisingStoreDataObject {

		private short salutationId;
		private string description;


		public Salutation() : this(short.MinValue) { }
		public Salutation(short salutationId) : this(salutationId, null) { }
		public Salutation(short salutationId, string description) {
			this.salutationId = salutationId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Salutation>\r\n" +
			"	<SalutationId>" + salutationId + "</SalutationId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</Salutation>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "salutationId") {
					SetXmlValue(ref salutationId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Salutation[] GetSalutations() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSalutations();
		}

		public static Salutation GetSalutationByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSalutationByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertSalutation(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateSalutation(this);
		}
		#endregion

		#region Properties
		public short SalutationId {
			set { salutationId = value; }
			get { return salutationId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
