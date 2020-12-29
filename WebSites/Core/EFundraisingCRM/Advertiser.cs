using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Advertiser: EFundraisingCRMDataObject {

		private int advertiserID;
		private int advertismentTypeID;
		private int contactID;
		private string advertiserName;


		public Advertiser() : this(int.MinValue) { }
		public Advertiser(int advertiserID) : this(advertiserID, int.MinValue) { }
		public Advertiser(int advertiserID, int advertismentTypeID) : this(advertiserID, advertismentTypeID, int.MinValue) { }
		public Advertiser(int advertiserID, int advertismentTypeID, int contactID) : this(advertiserID, advertismentTypeID, contactID, null) { }
		public Advertiser(int advertiserID, int advertismentTypeID, int contactID, string advertiserName) {
			this.advertiserID = advertiserID;
			this.advertismentTypeID = advertismentTypeID;
			this.contactID = contactID;
			this.advertiserName = advertiserName;
		}

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Advertiser>\r\n" +
			"	<AdvertiserID>" + advertiserID + "</AdvertiserID>\r\n" +
			"	<AdvertismentTypeID>" + advertismentTypeID + "</AdvertismentTypeID>\r\n" +
			"	<ContactID>" + contactID + "</ContactID>\r\n" +
			"	<AdvertiserName>" + System.Web.HttpUtility.HtmlEncode(advertiserName) + "</AdvertiserName>\r\n" +
			"</Advertiser>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserId")) {
					SetXmlValue(ref advertiserID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertismentTypeId")) {
					SetXmlValue(ref advertismentTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contactId")) {
					SetXmlValue(ref contactID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserName")) {
					SetXmlValue(ref advertiserName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Advertiser[] GetAdvertisers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisers();
		}

		public static Advertiser GetAdvertiserByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertiserByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertiser(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertiser(this);
		}
		#endregion

		#region Properties
		public int AdvertiserID {
			set { advertiserID = value; }
			get { return advertiserID; }
		}

		public int AdvertismentTypeID {
			set { advertismentTypeID = value; }
			get { return advertismentTypeID; }
		}

		public int ContactID {
			set { contactID = value; }
			get { return contactID; }
		}

		public string AdvertiserName {
			set { advertiserName = value; }
			get { return advertiserName; }
		}

		#endregion
	}
}
