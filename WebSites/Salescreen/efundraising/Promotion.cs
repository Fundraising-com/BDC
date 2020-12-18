using System;
using efundraising.Core;
using efundraising.efundraisingCore.DataAccess;

namespace efundraising.efundraisingCore
{

	/// <summary>
	/// Summary description for Promotion.
	/// </summary>
	public class Promotion : BusinessBase
	{
		#region Constructors
		public Promotion()
		{
		}
		#endregion

		#region Fields
		private int promotionId = int.MinValue;
		private string promotionTypeCode = null;
		private string description = "";
		private string visibility = null;
		private string contactName = null;
		private string trackingSerial = null;
		private int nbImpressionBought = int.MinValue;
		private bool isActive = false;
		private int targetedMarketId = int.MinValue;
		private int advertisingSupportId = int.MinValue;
		private int advertisementId = int.MinValue;
		private int partnerId = int.MinValue;
		private string cookieContent = null;
		private int grabberId = int.MinValue;
		private bool isPredictive = false;
		private int advertiserId = int.MinValue;
		private string keyword = null;
		private string scriptName = null;
		private int advertismentTypeId = int.MinValue;
		private int destinationId = int.MinValue;
		private int advertiserPartnerId = int.MinValue;			
		#endregion

		#region Methods
		public static Promotion GetPromotion(int promotionId)
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetPromotion(promotionId);
		}

		public static Promotion GetPromotion(string scriptName)
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetPromotion(scriptName);
		}

		#endregion

		#region Properties
		public int PromotionId
		{
			get { return this.promotionId; }
			set { this.promotionId = value; }
		}

		public string PromotionTypeCode
		{
			get { return this.promotionTypeCode; }
			set { this.promotionTypeCode = value; }
		}

		public int TargetedMarketId
		{
			get { return this.targetedMarketId; }
			set { this.targetedMarketId = value; }
		}

		public int AdvertisingSupportId
		{
			get { return this.advertisingSupportId; }
			set { this.advertisingSupportId = value; }
		}

		public int AdvertisementId
		{
			get { return this.advertisementId; }
			set { this.advertisementId = value; }
		}

		public int PartnerId
		{
			get { return this.partnerId; }
			set { this.partnerId = value; }
		}

		public int AdvertiserId
		{
			get { return this.advertiserId; }
			set { this.advertiserId = value; }
		}

		public int AdvertismentTypeId
		{
			get { return this.advertismentTypeId; }
			set { this.advertismentTypeId = value; }
		}

		public int DestinationId
		{
			get { return this.destinationId; }
			set { this.destinationId = value; }
		}

		public int AdvertiserPartnerId
		{
			get { return this.advertiserPartnerId; }
			set { this.advertiserPartnerId = value; }
		}

		public int GrabberId
		{
			get { return this.grabberId; }
			set { this.grabberId = value; }
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public string ScriptName
		{
			get { return this.scriptName; }
			set { this.scriptName = value; }
		}

		public string ContactName
		{
			get { return this.contactName; }
			set { this.contactName = value; }
		}

		public string Visibility
		{
			get { return this.visibility; }
			set { this.visibility = value; }
		}

		public string TrackingSerial
		{
			get { return this.trackingSerial; }
			set { this.trackingSerial = value; }
		}

		public int NbImpressionBought
		{
			get { return this.nbImpressionBought; }
			set { this.nbImpressionBought = value; }
		}

		public bool IsActive
		{
			get { return this.isActive; }
			set { this.isActive = value; }
		}

		public string CookieContent
		{
			get { return this.cookieContent; }
			set { this.cookieContent = value; }
		}

		public bool IsPredictive
		{
			get { return this.isPredictive; }
			set { this.isPredictive = value; }
		}

		public string Keyword
		{
			get { return this.keyword; }
			set { this.keyword = value; }
		}

		#endregion
	}	
}
