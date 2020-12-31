using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectCatalogClickedArgs:System.EventArgs
	{
	
		
		private QSPFulfillment.DataAccess.Common.ActionObject.Catalog cCatalog;

		public SelectCatalogClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.Catalog CatalogInfo)
		{
			cCatalog = CatalogInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.Catalog CatalogInfo
		{
			get{return cCatalog;}
		}
	}
}
