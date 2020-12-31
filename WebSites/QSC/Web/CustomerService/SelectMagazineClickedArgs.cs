using System;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectMagazineClickedArgs:System.EventArgs
	{
	
		
		private QSPFulfillment.DataAccess.Common.ActionObject.Magazine mMagazine;

		public SelectMagazineClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Magazine MagazineInfo)
		{
			mMagazine = MagazineInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Magazine MagazineInfo
		{
			get{return mMagazine;}
		}

		
		
			
		
	}
}
