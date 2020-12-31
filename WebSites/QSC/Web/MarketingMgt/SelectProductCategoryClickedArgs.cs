using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectProductCategoryClickedArgs:System.EventArgs
	{
		private QSPFulfillment.DataAccess.Common.ActionObject.ProductCategory pcProductCategory;

		public SelectProductCategoryClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.ProductCategory ProductCategoryInfo)
		{
			pcProductCategory = ProductCategoryInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.ProductCategory ProductCategoryInfo
		{
			get{return pcProductCategory;}
		}
	}
}
