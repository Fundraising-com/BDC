using System;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.DataAccess.Business
{
	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class ProductContractBusinessFactory
	{
		private static ProductContractBusinessFactory singletonInstance;

		private ProductContractBusinessFactory() { }

		public static ProductContractBusinessFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductContractBusinessFactory();
				}
				
				return singletonInstance;
			}
		}

		public ProductContractBusiness GetProductContractBusiness(ProductType productType, Message messageManager) 
		{
			ProductContractType productContractType;
			ProductContractBusiness productContractBusiness = null;

			switch(productType) 
			{
				case ProductType.Magazine : 
				{
					productContractBusiness = new MagazineContractBusiness(messageManager);
					break;
				}
				case ProductType.FieldSupplies : 
				{
					productContractBusiness = new FieldSuppliesContractBusiness(messageManager);
					break;
				}
				case ProductType.None : 
				{
					productContractBusiness = new ProductContractAllBusiness(messageManager);
					break;
				}
				default : 
				{
					productContractType = ProductContractTypeFactory.Instance.GetProductContractType(productType);

					switch(productContractType) 
					{
						case ProductContractType.GST_HST : 
						{
							productContractBusiness = new ProductContractGST_HSTBusiness(messageManager);
							break;
						}
						case ProductContractType.Single : 
						{
							productContractBusiness = new ProductContractSingleBusiness(messageManager);
							break;
						}
					}

					break;
				}
			}

			return productContractBusiness;
		}
	}
}
