using System;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.DataAccess.Business
{
	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class ProductBusinessFactory
	{
		private static ProductBusinessFactory singletonInstance;

		private ProductBusinessFactory() { }

		public static ProductBusinessFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductBusinessFactory();
				}
				
				return singletonInstance;
			}
		}

		public ProductBusiness GetProductBusiness(ProductType productType, Message messageManager) 
		{
			ProductBusiness productBusiness = null;

			switch(productType) 
			{
				case ProductType.Magazine : 
				{
					productBusiness = new MagazineBusiness(messageManager);
					break;
				}
				case ProductType.IncentiveMag :
				case ProductType.IncentiveGift :
				{
					productBusiness = new IncentivesCumulativeBusiness(messageManager);
					break;
				}
				default : 
				{
					productBusiness = new ProductBusiness(messageManager);
					break;
				}
			}

			return productBusiness;
		}
	}
}
