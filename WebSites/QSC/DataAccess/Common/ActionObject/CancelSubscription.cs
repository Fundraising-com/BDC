using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Cancel.
	/// </summary>
	public class CancelSubscription:ActionObjectCommon
	{
	
		

		public CancelSubscription(int COHInstance,int TransID,string UserID)
		{
			
			this.iTransID = TransID;
			this.iCOHInstance = COHInstance;
			this.sUserID = UserID;
		}
		
		/*public int Status
		{
			get
			{
				return iStatus;
			}
		}
*/


		
	}
}
