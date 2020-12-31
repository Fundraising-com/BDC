using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectPhoneClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.Phone pPhone;

		public SelectPhoneClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Phone PhoneInfo)
		{
			pPhone = PhoneInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Phone PhoneInfo
		{
			get{return pPhone;}
		}
	}
}
