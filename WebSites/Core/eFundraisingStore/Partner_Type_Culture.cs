using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerTypeCulture: eFundraisingStoreDataObject {

		private int partnerTypeId;
		private string cultureCode;
		private string name;
		private DateTime createDate;


		public PartnerTypeCulture() : this(int.MinValue) { }
		public PartnerTypeCulture(int partnerTypeId) : this(partnerTypeId, null) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode) : this(partnerTypeId, cultureCode, null) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode, string name) : this(partnerTypeId, cultureCode, name, DateTime.MinValue) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode, string name, DateTime createDate) {
			this.partnerTypeId = partnerTypeId;
			this.cultureCode = cultureCode;
			this.name = name;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerTypeCulture>\r\n" +
			"	<PartnerTypeId>" + partnerTypeId + "</PartnerTypeId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PartnerTypeCulture>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerTypeId") {
					SetXmlValue(ref partnerTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerTypeCulture[] GetPartnerTypeCultures() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerTypeCultures();
		}

		public static PartnerTypeCulture GetPartnerTypeCultureByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerTypeCultureByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerTypeCulture(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerTypeCulture(this);
		}
		#endregion

		#region Properties
		public int PartnerTypeId {
			set { partnerTypeId = value; }
			get { return partnerTypeId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
