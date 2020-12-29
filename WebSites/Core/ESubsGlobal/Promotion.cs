using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for Promotion.
	/// </summary>
	public class Promotion
	{
		#region Fields
		private int _promotionID = int.MinValue;
		#endregion

		#region Constructors
		public Promotion()
		{
			
		}

		public Promotion(int promotionID) {
			_promotionID = promotionID;
		}

		#endregion

		public static Promotion CreateSelfRegistrationPromotion(Partner partner)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetSelfRegisteredPromotionByPartner(partner);			
		}

		#region Properties
		public int PromotionID
		{
			get { return _promotionID; }
			set { _promotionID = value; }
		}
		#endregion	
	}
}
