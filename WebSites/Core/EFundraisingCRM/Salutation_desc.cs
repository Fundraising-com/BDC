using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SalutationDesc: EFundraisingCRMDataObject {

		private short salutationId;
		private short languageId;
		private string description;


		public SalutationDesc() : this(short.MinValue) { }
		public SalutationDesc(short salutationId) : this(salutationId, short.MinValue) { }
		public SalutationDesc(short salutationId, short languageId) : this(salutationId, languageId, null) { }
		public SalutationDesc(short salutationId, short languageId, string description) {
			this.salutationId = salutationId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalutationDesc>\r\n" +
			"	<SalutationId>" + salutationId + "</SalutationId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</SalutationDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salutationId")) {
					SetXmlValue(ref salutationId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalutationDesc[] GetSalutationDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalutationDescs();
		}

		/*
		public static SalutationDesc GetSalutationDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalutationDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalutationDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalutationDesc(this);
		}*/
		#endregion

		#region Properties
		public short SalutationId {
			set { salutationId = value; }
			get { return salutationId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
