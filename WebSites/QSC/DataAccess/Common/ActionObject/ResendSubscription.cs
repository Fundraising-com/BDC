using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Cancel.
	/// </summary>
	public class ResendSubscription:ActionObjectCommon
	{
		public ResendSubscription(int COHInstance, int TransID, string UserID)
		{
			this.iTransID = TransID;
			this.iCOHInstance = COHInstance;
			this.sUserID = UserID;
		}
	}
}
