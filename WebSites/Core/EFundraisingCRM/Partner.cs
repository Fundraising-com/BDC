using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public enum PartnerStatus {
		Ok,
		Error
	}

	public enum PartnerSort {
		PartnerID,
		Name,
		Path,
		PhoneNumber
	}

	public class Partner: EFundraisingCRMDataObject {
		public const int FundraisingPartnerID = 686;
		public const int ScratchCardPartnerID = 500;

		private int partnerId;
		private short partnerGroupTypeId;
		private short partnerSubgroupTypeId;
		private string partnerName;
		private string partnerPath;
		private string esubsUrl;
		private string estoreUrl;
		private string freeKitUrl;
		private string logo;
		private string phoneNumber;
		private string emailExt;
		private string url;
		private string guid;
		private int prizeEligible;
		private int hasCollectionSite;

		private PartnerSort partnerSort = PartnerSort.PartnerID;
		private bool sortAssending = true;

		public Partner() : this(int.MinValue) { }
		public Partner(int partnerId) : this(partnerId, short.MinValue) { }
		public Partner(int partnerId, short partnerGroupTypeId) : this(partnerId, partnerGroupTypeId, short.MinValue) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, phoneNumber, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber, string emailExt) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, phoneNumber, emailExt, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber, string emailExt, string url) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, phoneNumber, emailExt, url, null) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber, string emailExt, string url, string guid) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, phoneNumber, emailExt, url, guid, int.MinValue) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber, string emailExt, string url, string guid, int prizeEligible) : this(partnerId, partnerGroupTypeId, partnerSubgroupTypeId, partnerName, partnerPath, esubsUrl, estoreUrl, freeKitUrl, logo, phoneNumber, emailExt, url, guid, prizeEligible, int.MinValue) { }
		public Partner(int partnerId, short partnerGroupTypeId, short partnerSubgroupTypeId, string partnerName, string partnerPath, string esubsUrl, string estoreUrl, string freeKitUrl, string logo, string phoneNumber, string emailExt, string url, string guid, int prizeEligible, int hasCollectionSite) {
			this.partnerId = partnerId;
			this.partnerGroupTypeId = partnerGroupTypeId;
			this.partnerSubgroupTypeId = partnerSubgroupTypeId;
			this.partnerName = partnerName;
			this.partnerPath = partnerPath;
			this.esubsUrl = esubsUrl;
			this.estoreUrl = estoreUrl;
			this.freeKitUrl = freeKitUrl;
			this.logo = logo;
			this.phoneNumber = phoneNumber;
			this.emailExt = emailExt;
			this.url = url;
			this.guid = guid;
			this.prizeEligible = prizeEligible;
			this.hasCollectionSite = hasCollectionSite;
		}

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Partner>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PartnerGroupTypeId>" + partnerGroupTypeId + "</PartnerGroupTypeId>\r\n" +
			"	<PartnerSubgroupTypeId>" + partnerSubgroupTypeId + "</PartnerSubgroupTypeId>\r\n" +
			"	<PartnerName>" + System.Web.HttpUtility.HtmlEncode(partnerName) + "</PartnerName>\r\n" +
			"	<PartnerPath>" + System.Web.HttpUtility.HtmlEncode(partnerPath) + "</PartnerPath>\r\n" +
			"	<EsubsUrl>" + System.Web.HttpUtility.HtmlEncode(esubsUrl) + "</EsubsUrl>\r\n" +
			"	<EstoreUrl>" + System.Web.HttpUtility.HtmlEncode(estoreUrl) + "</EstoreUrl>\r\n" +
			"	<FreeKitUrl>" + System.Web.HttpUtility.HtmlEncode(freeKitUrl) + "</FreeKitUrl>\r\n" +
			"	<Logo>" + System.Web.HttpUtility.HtmlEncode(logo) + "</Logo>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<EmailExt>" + System.Web.HttpUtility.HtmlEncode(emailExt) + "</EmailExt>\r\n" +
			"	<Url>" + System.Web.HttpUtility.HtmlEncode(url) + "</Url>\r\n" +
			"	<Guid>" + guid + "</Guid>\r\n" +
			"	<PrizeEligible>" + prizeEligible + "</PrizeEligible>\r\n" +
			"	<HasCollectionSite>" + hasCollectionSite + "</HasCollectionSite>\r\n" +
			"</Partner>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerGroupTypeId")) {
					SetXmlValue(ref partnerGroupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerSubgroupTypeId")) {
					SetXmlValue(ref partnerSubgroupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerName")) {
					SetXmlValue(ref partnerName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerPath")) {
					SetXmlValue(ref partnerPath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("esubsUrl")) {
					SetXmlValue(ref esubsUrl, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("estoreUrl")) {
					SetXmlValue(ref estoreUrl, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("freeKitUrl")) {
					SetXmlValue(ref freeKitUrl, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("logo")) {
					SetXmlValue(ref logo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumber")) {
					SetXmlValue(ref phoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailExt")) {
					SetXmlValue(ref emailExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("url")) {
					SetXmlValue(ref url, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("guid")) {
					SetXmlValue(ref guid, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("prizeEligible")) {
					SetXmlValue(ref prizeEligible, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("hasCollectionSite")) {
					SetXmlValue(ref hasCollectionSite, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Partner[] GetPartners() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartners();
		}
		public static Partner GetPartnerByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerByLeadID(leadID);
		}
		public static Partner[] GetPartnersByName(string name) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnersByName(name);
		}

		public static Partner GetPartnerByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerByID(id);
		}

		public PartnerStatus Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.InsertPartner(this);
			switch(returnValue) {
				case 1:
					return PartnerStatus.Ok;
			}
			return PartnerStatus.Error;

		}

		public PartnerStatus Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.UpdatePartner(this);
			switch(returnValue) {
				case 1:
					return PartnerStatus.Ok;
			}
			return PartnerStatus.Error;
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short PartnerGroupTypeId {
			set { partnerGroupTypeId = value; }
			get { return partnerGroupTypeId; }
		}

		public short PartnerSubgroupTypeId {
			set { partnerSubgroupTypeId = value; }
			get { return partnerSubgroupTypeId; }
		}

		public string PartnerName {
			set { partnerName = value; }
			get { return partnerName; }
		}

		public string PartnerPath {
			set { partnerPath = value; }
			get { return partnerPath; }
		}

		public string EsubsUrl {
			set { esubsUrl = value; }
			get { return esubsUrl; }
		}

		public string EstoreUrl {
			set { estoreUrl = value; }
			get { return estoreUrl; }
		}

		public string FreeKitUrl {
			set { freeKitUrl = value; }
			get { return freeKitUrl; }
		}

		public string Logo {
			set { logo = value; }
			get { return logo; }
		}

		public string PhoneNumber {
			set { phoneNumber = value; }
			get { return phoneNumber; }
		}

		public string EmailExt {
			set { emailExt = value; }
			get { return emailExt; }
		}

		public string Url {
			set { url = value; }
			get { return url; }
		}

		public string Guid {
			set { guid = value; }
			get { return guid; }
		}

		public int PrizeEligible {
			set { prizeEligible = value; }
			get { return prizeEligible; }
		}

		public int HasCollectionSite {
			set { hasCollectionSite = value; }
			get { return hasCollectionSite; }
		}

		public PartnerSort SortType {
			get { return partnerSort; }
			set { partnerSort = value; }
		}

		public bool SortAssending {
			get { return sortAssending; }
			set { sortAssending = value; }
		}

		#endregion

		#region IComparable Members

		public override int CompareTo(object obj) {
			Partner partner1 = this;
			Partner partner2 = (Partner)obj;
			if(!sortAssending) {
				Partner tmp = partner2;
				partner2 = this;
				partner1 = tmp;
			}
			switch(partnerSort) {
				case PartnerSort.PartnerID:
					return partner1.PartnerId.CompareTo(partner2.PartnerId);
				case PartnerSort.Name:
					return partner1.PartnerName.CompareTo(partner2.PartnerName);
				case PartnerSort.Path:
					if(partner1.partnerPath == null) {
						return 0;
					}
					if(partner2.partnerPath == null) {
						return 1;
					}
					return partner1.PartnerPath.CompareTo(partner2.PartnerPath);
				case PartnerSort.PhoneNumber:
					if(partner1.phoneNumber == null) {
						return 0;
					}
					if(partner2.phoneNumber == null) {
						return 1;
					}
					return partner1.PhoneNumber.CompareTo(partner2.PhoneNumber);
			}
			return 0;
		}

		#endregion
	}
}
