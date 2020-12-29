using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class SalutationDesc: eFundraisingStoreDataObject {

		private short salutationId;
		private string cultureCode;
		private string description;


		public SalutationDesc() : this(short.MinValue) { }
		public SalutationDesc(short salutationId) : this(salutationId, null) { }
		public SalutationDesc(short salutationId, string cultureCode) : this(salutationId, cultureCode, null) { }
		public SalutationDesc(short salutationId, string cultureCode, string description) {
			this.salutationId = salutationId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalutationDesc>\r\n" +
			"	<SalutationId>" + salutationId + "</SalutationId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</SalutationDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "salutationId") {
					SetXmlValue(ref salutationId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalutationDesc[] GetSalutationDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSalutationDescs();
		}

		public static SalutationDesc GetSalutationDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSalutationDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertSalutationDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateSalutationDesc(this);
		}
		#endregion

		#region Properties
		public short SalutationId {
			set { salutationId = value; }
			get { return salutationId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
