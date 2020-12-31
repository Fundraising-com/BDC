using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectContactProductClickedArgs:System.EventArgs
	{
	
		
		private int iContactProductID = 0;

		public SelectContactProductClickedArgs(int contactProductID)
		{
			iContactProductID = contactProductID;
		}
		
		public int ContactProductID
		{
			get{return iContactProductID;}
		}

		
		
			
		
	}
}
