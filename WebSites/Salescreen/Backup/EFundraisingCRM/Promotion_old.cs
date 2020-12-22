using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PromotionOld: EFundraisingCRMDataObject {

		private int promotionID;
		private string promotionTypeCode;
		private string description;
		private string visibility;
		private string contactName;
		private string trackingSerial;
		private int nbImpressionBought;
		private int isActive;
		private int targetedMarketID;
		private int advertisingSupportID;
		private int advertisementId;
		private int partnerID;
		private string cookieContent;
		private int grabberId;
		private int isPredictive;
		private int advertiserID;
		private string keyword;
		private string scriptName;
		private int advertismentTypeID;
		private int destinationID;
		private int advertiserPartnerID;


		public PromotionOld() : this(int.MinValue) { }
		public PromotionOld(int promotionID) : this(promotionID, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode) : this(promotionID, promotionTypeCode, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description) : this(promotionID, promotionTypeCode, description, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility) : this(promotionID, promotionTypeCode, description, visibility, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName) : this(promotionID, promotionTypeCode, description, visibility, contactName, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, advertiserID, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID, string keyword) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, advertiserID, keyword, null) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID, string keyword, string scriptName) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, advertiserID, keyword, scriptName, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID, string keyword, string scriptName, int advertismentTypeID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, advertiserID, keyword, scriptName, advertismentTypeID, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID, string keyword, string scriptName, int advertismentTypeID, int destinationID) : this(promotionID, promotionTypeCode, description, visibility, contactName, trackingSerial, nbImpressionBought, isActive, targetedMarketID, advertisingSupportID, advertisementId, partnerID, cookieContent, grabberId, isPredictive, advertiserID, keyword, scriptName, advertismentTypeID, destinationID, int.MinValue) { }
		public PromotionOld(int promotionID, string promotionTypeCode, string description, string visibility, string contactName, string trackingSerial, int nbImpressionBought, int isActive, int targetedMarketID, int advertisingSupportID, int advertisementId, int partnerID, string cookieContent, int grabberId, int isPredictive, int advertiserID, string keyword, string scriptName, int advertismentTypeID, int destinationID, int advertiserPartnerID) {
			this.promotionID = promotionID;
			this.promotionTypeCode = promotionTypeCode;
			this.description = description;
			this.visibility = visibility;
			this.contactName = contactName;
			this.trackingSerial = trackingSerial;
			this.nbImpressionBought = nbImpressionBought;
			this.isActive = isActive;
			this.targetedMarketID = targetedMarketID;
			this.advertisingSupportID = advertisingSupportID;
			this.advertisementId = advertisementId;
			this.partnerID = partnerID;
			this.cookieContent = cookieContent;
			this.grabberId = grabberId;
			this.isPredictive = isPredictive;
			this.advertiserID = advertiserID;
			this.keyword = keyword;
			this.scriptName = scriptName;
			this.advertismentTypeID = advertismentTypeID;
			this.destinationID = destinationID;
			this.advertiserPartnerID = advertiserPartnerID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PromotionOld>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Visibility>" + System.Web.HttpUtility.HtmlEncode(visibility) + "</Visibility>\r\n" +
			"	<ContactName>" + System.Web.HttpUtility.HtmlEncode(contactName) + "</ContactName>\r\n" +
			"	<TrackingSerial>" + System.Web.HttpUtility.HtmlEncode(trackingSerial) + "</TrackingSerial>\r\n" +
			"	<NbImpressionBought>" + nbImpressionBought + "</NbImpressionBought>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<TargetedMarketID>" + targetedMarketID + "</TargetedMarketID>\r\n" +
			"	<AdvertisingSupportID>" + advertisingSupportID + "</AdvertisingSupportID>\r\n" +
			"	<AdvertisementId>" + advertisementId + "</AdvertisementId>\r\n" +
			"	<PartnerID>" + partnerID + "</PartnerID>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<GrabberId>" + grabberId + "</GrabberId>\r\n" +
			"	<IsPredictive>" + isPredictive + "</IsPredictive>\r\n" +
			"	<AdvertiserID>" + advertiserID + "</AdvertiserID>\r\n" +
			"	<Keyword>" + System.Web.HttpUtility.HtmlEncode(keyword) + "</Keyword>\r\n" +
			"	<ScriptName>" + System.Web.HttpUtility.HtmlEncode(scriptName) + "</ScriptName>\r\n" +
			"	<AdvertismentTypeID>" + advertismentTypeID + "</AdvertismentTypeID>\r\n" +
			"	<DestinationID>" + destinationID + "</DestinationID>\r\n" +
			"	<AdvertiserPartnerID>" + advertiserPartnerID + "</AdvertiserPartnerID>\r\n" +
			"</PromotionOld>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("visibility")) {
					SetXmlValue(ref visibility, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contactName")) {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("trackingSerial")) {
					SetXmlValue(ref trackingSerial, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbImpressionBought")) {
					SetXmlValue(ref nbImpressionBought, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetedMarketId")) {
					SetXmlValue(ref targetedMarketID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisementId")) {
					SetXmlValue(ref advertisementId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("grabberId")) {
					SetXmlValue(ref grabberId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isPredictive")) {
					SetXmlValue(ref isPredictive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserId")) {
					SetXmlValue(ref advertiserID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("keyword")) {
					SetXmlValue(ref keyword, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scriptName")) {
					SetXmlValue(ref scriptName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertismentTypeId")) {
					SetXmlValue(ref advertismentTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("destinationId")) {
					SetXmlValue(ref destinationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserPartnerId")) {
					SetXmlValue(ref advertiserPartnerID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionOld[] GetPromotionOlds() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionOlds();
		}

		public static PromotionOld GetPromotionOldByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionOldByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotionOld(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotionOld(this);
		}
		#endregion

		#region Properties
		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string Visibility {
			set { visibility = value; }
			get { return visibility; }
		}

		public string ContactName {
			set { contactName = value; }
			get { return contactName; }
		}

		public string TrackingSerial {
			set { trackingSerial = value; }
			get { return trackingSerial; }
		}

		public int NbImpressionBought {
			set { nbImpressionBought = value; }
			get { return nbImpressionBought; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public int TargetedMarketID {
			set { targetedMarketID = value; }
			get { return targetedMarketID; }
		}

		public int AdvertisingSupportID {
			set { advertisingSupportID = value; }
			get { return advertisingSupportID; }
		}

		public int AdvertisementId {
			set { advertisementId = value; }
			get { return advertisementId; }
		}

		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public int GrabberId {
			set { grabberId = value; }
			get { return grabberId; }
		}

		public int IsPredictive {
			set { isPredictive = value; }
			get { return isPredictive; }
		}

		public int AdvertiserID {
			set { advertiserID = value; }
			get { return advertiserID; }
		}

		public string Keyword {
			set { keyword = value; }
			get { return keyword; }
		}

		public string ScriptName {
			set { scriptName = value; }
			get { return scriptName; }
		}

		public int AdvertismentTypeID {
			set { advertismentTypeID = value; }
			get { return advertismentTypeID; }
		}

		public int DestinationID {
			set { destinationID = value; }
			get { return destinationID; }
		}

		public int AdvertiserPartnerID {
			set { advertiserPartnerID = value; }
			get { return advertiserPartnerID; }
		}

		#endregion
	}
}
