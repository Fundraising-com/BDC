using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Promotion: EFundraisingCRMDataObject {

		private int promotionId;
		private string promotionTypeCode;
		private int targetedMarketId;
		private int advertisingSupportId;
		private int advertisementId;
		private int partnerId;
		private int advertiserId;
		private short transferStatusId;
		private int advertismentTypeId;
		private int destinationId;
		private int advertiserPartnerId;
		private int grabberId;
		private string description;
		private string scriptName;
		private string contactName;
		private string visibility;
		private string trackingSerial;
		private int nbImpressionBought;
		private int isActive;
		private string cookieContent;
		private int isPredictive;
		private string keyword;
		private int isDisplayable;


		public Promotion() : this(int.MinValue) { }
		public Promotion(int promotionId) : this(promotionId, null) { }
		public Promotion(int promotionId, string promotionTypeCode) : this(promotionId, promotionTypeCode, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId) : this(promotionId, promotionTypeCode, targetedMarketId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, short.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, nbImpressionBought, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought, int isActive) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, nbImpressionBought, isActive, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought, int isActive, string cookieContent) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, nbImpressionBought, isActive, cookieContent, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought, int isActive, string cookieContent, int isPredictive) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, nbImpressionBought, isActive, cookieContent, isPredictive, null) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought, int isActive, string cookieContent, int isPredictive, string keyword) : this(promotionId, promotionTypeCode, targetedMarketId, advertisingSupportId, advertisementId, partnerId, advertiserId, transferStatusId, advertismentTypeId, destinationId, advertiserPartnerId, grabberId, description, scriptName, contactName, visibility, trackingSerial, nbImpressionBought, isActive, cookieContent, isPredictive, keyword, int.MinValue) { }
		public Promotion(int promotionId, string promotionTypeCode, int targetedMarketId, int advertisingSupportId, int advertisementId, int partnerId, int advertiserId, short transferStatusId, int advertismentTypeId, int destinationId, int advertiserPartnerId, int grabberId, string description, string scriptName, string contactName, string visibility, string trackingSerial, int nbImpressionBought, int isActive, string cookieContent, int isPredictive, string keyword, int isDisplayable) {
			this.promotionId = promotionId;
			this.promotionTypeCode = promotionTypeCode;
			this.targetedMarketId = targetedMarketId;
			this.advertisingSupportId = advertisingSupportId;
			this.advertisementId = advertisementId;
			this.partnerId = partnerId;
			this.advertiserId = advertiserId;
			this.transferStatusId = transferStatusId;
			this.advertismentTypeId = advertismentTypeId;
			this.destinationId = destinationId;
			this.advertiserPartnerId = advertiserPartnerId;
			this.grabberId = grabberId;
			this.description = description;
			this.scriptName = scriptName;
			this.contactName = contactName;
			this.visibility = visibility;
			this.trackingSerial = trackingSerial;
			this.nbImpressionBought = nbImpressionBought;
			this.isActive = isActive;
			this.cookieContent = cookieContent;
			this.isPredictive = isPredictive;
			this.keyword = keyword;
			this.isDisplayable = isDisplayable;
		}

		#region Static Data 
		public Promotion PartnerFundraising {
			get { 
				return new Promotion(686, "AD", int.MinValue, int.MinValue, int.MinValue,	0, 461, 1, int.MinValue, int.MinValue, int.MinValue,int.MinValue, "Youth Ministry Source Book", null, null, null, null, 0, 1, null, 0, null, 1); 
			}
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Promotion>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<PromotionTypeCode>" + System.Web.HttpUtility.HtmlEncode(promotionTypeCode) + "</PromotionTypeCode>\r\n" +
			"	<TargetedMarketId>" + targetedMarketId + "</TargetedMarketId>\r\n" +
			"	<AdvertisingSupportId>" + advertisingSupportId + "</AdvertisingSupportId>\r\n" +
			"	<AdvertisementId>" + advertisementId + "</AdvertisementId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<AdvertiserId>" + advertiserId + "</AdvertiserId>\r\n" +
			"	<TransferStatusId>" + transferStatusId + "</TransferStatusId>\r\n" +
			"	<AdvertismentTypeId>" + advertismentTypeId + "</AdvertismentTypeId>\r\n" +
			"	<DestinationId>" + destinationId + "</DestinationId>\r\n" +
			"	<AdvertiserPartnerId>" + advertiserPartnerId + "</AdvertiserPartnerId>\r\n" +
			"	<GrabberId>" + grabberId + "</GrabberId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<ScriptName>" + System.Web.HttpUtility.HtmlEncode(scriptName) + "</ScriptName>\r\n" +
			"	<ContactName>" + System.Web.HttpUtility.HtmlEncode(contactName) + "</ContactName>\r\n" +
			"	<Visibility>" + System.Web.HttpUtility.HtmlEncode(visibility) + "</Visibility>\r\n" +
			"	<TrackingSerial>" + System.Web.HttpUtility.HtmlEncode(trackingSerial) + "</TrackingSerial>\r\n" +
			"	<NbImpressionBought>" + nbImpressionBought + "</NbImpressionBought>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<IsPredictive>" + isPredictive + "</IsPredictive>\r\n" +
			"	<Keyword>" + System.Web.HttpUtility.HtmlEncode(keyword) + "</Keyword>\r\n" +
			"	<IsDisplayable>" + isDisplayable + "</IsDisplayable>\r\n" +
			"</Promotion>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionTypeCode")) {
					SetXmlValue(ref promotionTypeCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetedMarketId")) {
					SetXmlValue(ref targetedMarketId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisementId")) {
					SetXmlValue(ref advertisementId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserId")) {
					SetXmlValue(ref advertiserId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transferStatusId")) {
					SetXmlValue(ref transferStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertismentTypeId")) {
					SetXmlValue(ref advertismentTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("destinationId")) {
					SetXmlValue(ref destinationId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertiserPartnerId")) {
					SetXmlValue(ref advertiserPartnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("grabberId")) {
					SetXmlValue(ref grabberId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scriptName")) {
					SetXmlValue(ref scriptName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contactName")) {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("visibility")) {
					SetXmlValue(ref visibility, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isPredictive")) {
					SetXmlValue(ref isPredictive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("keyword")) {
					SetXmlValue(ref keyword, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDisplayable")) {
					SetXmlValue(ref isDisplayable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Promotion[] GetPromotions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotions();
		}
		public static Promotion GetPromotionByLeadID(int leadid)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionByLeadID(leadid);
		}
		public static Promotion GetPromotionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPromotion(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePromotion(this);
		}
		#endregion

		#region Properties
		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public string PromotionTypeCode {
			set { promotionTypeCode = value; }
			get { return promotionTypeCode; }
		}

		public int TargetedMarketId {
			set { targetedMarketId = value; }
			get { return targetedMarketId; }
		}

		public int AdvertisingSupportId {
			set { advertisingSupportId = value; }
			get { return advertisingSupportId; }
		}

		public int AdvertisementId {
			set { advertisementId = value; }
			get { return advertisementId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int AdvertiserId {
			set { advertiserId = value; }
			get { return advertiserId; }
		}

		public short TransferStatusId {
			set { transferStatusId = value; }
			get { return transferStatusId; }
		}

		public int AdvertismentTypeId {
			set { advertismentTypeId = value; }
			get { return advertismentTypeId; }
		}

		public int DestinationId {
			set { destinationId = value; }
			get { return destinationId; }
		}

		public int AdvertiserPartnerId {
			set { advertiserPartnerId = value; }
			get { return advertiserPartnerId; }
		}

		public int GrabberId {
			set { grabberId = value; }
			get { return grabberId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string ScriptName {
			set { scriptName = value; }
			get { return scriptName; }
		}

		public string ContactName {
			set { contactName = value; }
			get { return contactName; }
		}

		public string Visibility {
			set { visibility = value; }
			get { return visibility; }
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

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public int IsPredictive {
			set { isPredictive = value; }
			get { return isPredictive; }
		}

		public string Keyword {
			set { keyword = value; }
			get { return keyword; }
		}

		public int IsDisplayable {
			set { isDisplayable = value; }
			get { return isDisplayable; }
		}

		#endregion
	}
}
