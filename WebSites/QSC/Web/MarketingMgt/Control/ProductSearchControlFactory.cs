using System;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for ProductMaintenanceControlFactory.
	/// </summary>
	public class ProductSearchControlFactory
	{
		private static ProductSearchControlFactory singletonInstance;

		private ProductSearchControlFactory() { }

		public static ProductSearchControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductSearchControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetProductSearchControlPath(ProductType productType) 
		{
			string path = "Control\\";

			switch(productType) 
			{
				case ProductType.Magazine :
				{
					path += "MagazineSearchControl.ascx";
					break;
				}
				case ProductType.IncentiveMag :
				case ProductType.IncentiveGift : 
				{
					path += "IncentivesCumulativeSearchControl.ascx";
					break;
				}
				case ProductType.None : 
				{
					path += "AllTypesProductSearchControl.ascx";
					break;
				}
				default : 
				{
					path += "DefaultProductSearchControl.ascx";
					break;
				}
			}

			return path;
		}
	}
}
