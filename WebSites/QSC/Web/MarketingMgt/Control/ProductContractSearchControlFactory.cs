using System;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for ProductMaintenanceControlFactory.
	/// </summary>
	public class ProductContractSearchControlFactory
	{
		private static ProductContractSearchControlFactory singletonInstance;

		private ProductContractSearchControlFactory() { }

		public static ProductContractSearchControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductContractSearchControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetProductContractSearchControlPath(ProductType productType) 
		{
			ProductContractType productContractType;
			string path = String.Empty;

			switch(productType)
			{
				case ProductType.Magazine :
				{
					path = "MagazineContractSearchControl.ascx";
					break;
				}
				case ProductType.FieldSupplies : 
				{
					path = "FieldSuppliesContractSearchControl.ascx";
					break;
				}
				case ProductType.None : 
				{
					path = "AllTypesContractSearchControl.ascx";
					break;
				}
				default :
				{
					productContractType = ProductContractTypeFactory.Instance.GetProductContractType(productType);

					switch(productContractType) 
					{
						case ProductContractType.GST_HST : 
						{
							path = "DefaultGST_HSTContractSearchControl.ascx";
							break;
						}
						case ProductContractType.Single : 
						{
							path = "DefaultSingleContractSearchControl.ascx";
							break;
						}
					}

					break;
				}
			}

			return path;
		}
	}
}
