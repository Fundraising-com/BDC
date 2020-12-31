using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class ProductContractIDFactory
	{
		private static ProductContractIDFactory singletonInstance;

		private ProductContractIDFactory() { }

		public static ProductContractIDFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductContractIDFactory();
				}
				
				return singletonInstance;
			}
		}

		public ProductContractID GetProductContractID(int magPriceInstanceGST, int magPriceInstanceHST, ProductContractType productContractType) 
		{
			ProductContractID productContractID = null;

			switch(productContractType) 
			{
				case ProductContractType.Single : 
				{
					productContractID = new ProductContractIDSingle(magPriceInstanceGST);
					break;
				}
				case ProductContractType.GST_HST : 
				{
					productContractID = new ProductContractIDGST_HST(magPriceInstanceGST, magPriceInstanceHST);
					break;
				}
			}

			return productContractID;
		}
	}
}
