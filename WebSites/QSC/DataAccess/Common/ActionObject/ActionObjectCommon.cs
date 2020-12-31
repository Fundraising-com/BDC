using System;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ActionObjectCommon.
	/// </summary>
	/// 
	[Serializable]
	public class ActionObjectCommon
	{
		protected string sUserID;
		protected int iCOHInstance;
		protected int iTransID;

		public ActionObjectCommon()
		{
			
		}
		public string UserID
		{
			get{return sUserID;}
			
		}
		public int CustomerOrderHeaderInstance
		{
			get
			{
				return iCOHInstance;
			}
			set 
			{
				iCOHInstance = value;
			}
		}
		public int TransID
		{
			get
			{
				return iTransID;
			}
			set 
			{
				iTransID = value;
			}
		}
	}

}
