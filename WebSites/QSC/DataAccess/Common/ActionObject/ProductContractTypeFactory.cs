using System;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	[Serializable]
	public enum ProductContractType 
	{
		GST_HST,
		Single
	}

	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class ProductContractTypeFactory
	{
		private static ProductContractTypeFactory singletonInstance;

		private ProductContractTypeFactory() { }

		public static ProductContractTypeFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductContractTypeFactory();
				}
				
				return singletonInstance;
			}
		}

		public ProductContractType GetProductContractType(ProductType productType) 
		{
			ProductContractType productContractType = ProductContractType.Single;

			if(productType == ProductType.Magazine ||
				productType == ProductType.Books ||
				productType == ProductType.Video) 
			{
				productContractType = ProductContractType.GST_HST;
			} 
			else if(productType == ProductType.Gift ||
				productType == ProductType.WFC ||
				productType == ProductType.FieldSupplies ||
				productType == ProductType.Food ||
				productType == ProductType.Incentives ||
				productType == ProductType.IncentiveMag ||
				productType == ProductType.IncentiveGift ||
            productType == ProductType.GiftCard)
			{
				productContractType = ProductContractType.Single;
			} 

			return productContractType;
		}
	}
}
