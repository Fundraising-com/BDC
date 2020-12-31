using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectFulfillmentHouseClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouse fhFulfillmentHouse;

		public SelectFulfillmentHouseClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouse FulfillmentHouseInfo)
		{
			fhFulfillmentHouse = FulfillmentHouseInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouse FulfillmentHouseInfo
		{
			get{return fhFulfillmentHouse;}
		}
	}
}
