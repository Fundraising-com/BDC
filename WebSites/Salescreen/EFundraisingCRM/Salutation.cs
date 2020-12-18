using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Salutation: EFundraisingCRMDataObject {

		private short salutationId;
		private string salutationDesc;


		public Salutation() : this(short.MinValue) { }
		public Salutation(short salutationId) : this(salutationId, null) { }
		public Salutation(short salutationId, string salutationDesc) {
			this.salutationId = salutationId;
			this.salutationDesc = salutationDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Salutation>\r\n" +
			"	<SalutationId>" + salutationId + "</SalutationId>\r\n" +
			"	<SalutationDesc>" + System.Web.HttpUtility.HtmlEncode(salutationDesc) + "</SalutationDesc>\r\n" +
			"</Salutation>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salutationId")) {
					SetXmlValue(ref salutationId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salutationDesc")) {
					SetXmlValue(ref salutationDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Salutation[] GetSalutations() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalutations();
		}

		/*
		public static Salutation GetSalutationByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalutationByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalutation(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalutation(this);
		}*/
		#endregion

		#region Properties
		public short SalutationId {
			set { salutationId = value; }
			get { return salutationId; }
		}

		public string SalutationDesc {
			set { salutationDesc = value; }
			get { return salutationDesc; }
		}

		#endregion
	}
}
