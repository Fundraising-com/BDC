using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectCatalogSectionClickedArgs:System.EventArgs
	{
	
		
		private QSPFulfillment.DataAccess.Common.ActionObject.CatalogSection csCatalogSection;

		public SelectCatalogSectionClickedArgs(QSPFulfillment.DataAccess.Common.ActionObject.CatalogSection CatalogSectionInfo)
		{
			csCatalogSection = CatalogSectionInfo;
		}
		
		public QSPFulfillment.DataAccess.Common.ActionObject.CatalogSection CatalogSectionInfo
		{
			get{return csCatalogSection;}
		}
	}
}
