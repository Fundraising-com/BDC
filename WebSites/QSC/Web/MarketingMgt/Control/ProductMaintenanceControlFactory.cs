using System;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for ProductMaintenanceControlFactory.
	/// </summary>
	public class ProductMaintenanceControlFactory
	{
		private static ProductMaintenanceControlFactory singletonInstance;

		private ProductMaintenanceControlFactory() { }

		public static ProductMaintenanceControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductMaintenanceControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetProductMaintenanceControlPath(ProductType productType) 
		{
			string path = "Control\\";

			switch(productType) 
			{
				case ProductType.Magazine : 
				{
					path += "MagazineMaintenanceControl.ascx";
					break;
				}
				case ProductType.Books :
				case ProductType.Video : 
				{
					path += "MediaProductMaintenanceControl.ascx";
					break;
				}
				case ProductType.IncentiveMag :
				case ProductType.IncentiveGift : 
				{
					path += "IncentivesCumulativeMaintenanceControl.ascx";
					break;
				}
				default : 
				{
					path += "DefaultProductMaintenanceControl.ascx";
					break;
				}
			}

			return path;
		}
	}
}
