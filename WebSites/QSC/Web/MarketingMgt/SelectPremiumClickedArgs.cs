using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectPremiumClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.Premium pPremium;

		public SelectPremiumClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Premium PremiumInfo)
		{
			pPremium = PremiumInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Premium PremiumInfo
		{
			get{return pPremium;}
		}
	}
}
