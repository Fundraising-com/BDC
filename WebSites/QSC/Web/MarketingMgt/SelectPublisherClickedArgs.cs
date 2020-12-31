using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectPublisherClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.Publisher pPublisher;

		public SelectPublisherClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Publisher PublisherInfo)
		{
			pPublisher = PublisherInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Publisher PublisherInfo
		{
			get{return pPublisher;}
		}
	}
}
