using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectPublisherContactClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.PublisherContact pcPublisherContact;

		public SelectPublisherContactClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.PublisherContact PublisherContactInfo)
		{
			pcPublisherContact = PublisherContactInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.PublisherContact PublisherContactInfo
		{
			get{return pcPublisherContact;}
		}
	}
}
