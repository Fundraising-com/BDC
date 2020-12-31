using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectProductClickedArgs:System.EventArgs
	{
	
		
		private QSPFulfillment.DataAccess.Common.ActionObject.Product pProduct;

		public SelectProductClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Product ProductInfo)
		{
			pProduct = ProductInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Product ProductInfo
		{
			get{return pProduct;}
		}

		
		
			
		
	}
}
