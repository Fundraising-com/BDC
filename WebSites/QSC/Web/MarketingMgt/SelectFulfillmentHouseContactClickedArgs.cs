using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectFulfillmentHouseContactClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouseContact fhcFulfillmentHouseContact;

		public SelectFulfillmentHouseContactClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouseContact FulfillmentHouseContactInfo)
		{
			fhcFulfillmentHouseContact = FulfillmentHouseContactInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouseContact FulfillmentHouseContactInfo
		{
			get{return fhcFulfillmentHouseContact;}
		}
	}
}
