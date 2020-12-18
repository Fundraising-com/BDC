using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AdvertiserPartner: EFundraisingCRMDataObject {

		private int advertiserPartnerID;
		private int advertiserID;
		private string description;


		public AdvertiserPartner() : this(int.MinValue) { }
		public AdvertiserPartner(int advertiserPartnerID) : this(advertiserPartnerID, int.MinValue) { }
		public AdvertiserPartner(int advertiserPartnerID, int advertiserID) : this(advertiserPartnerID, advertiserID, null) { }
		public AdvertiserPartner(int advertiserPartnerID, int advertiserID, string description) {
			this.advertiserPartnerID = advertiserPartnerID;
			this.advertiserID = advertiserID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdvertiserPartner>\r\n" +
			"	<AdvertiserPartnerID>" + advertiserPartnerID + "</AdvertiserPartnerID>\r\n" +
			"	<AdvertiserID>" + advertiserID + "</AdvertiserID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</AdvertiserPartner>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserPartnerId")) {
					SetXmlValue(ref advertiserPartnerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserId")) {
					SetXmlValue(ref advertiserID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdvertiserPartner[] GetAdvertiserPartners() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertiserPartners();
		}

		public static AdvertiserPartner GetAdvertiserPartnerByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertiserPartnerByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertiserPartner(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertiserPartner(this);
		}
		#endregion

		#region Properties
		public int AdvertiserPartnerID {
			set { advertiserPartnerID = value; }
			get { return advertiserPartnerID; }
		}

		public int AdvertiserID {
			set { advertiserID = value; }
			get { return advertiserID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
