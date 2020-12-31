using System;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for ProductMaintenanceControlFactory.
	/// </summary>
	public class ProductContractMaintenanceControlFactory
	{
		private static ProductContractMaintenanceControlFactory singletonInstance;

		private ProductContractMaintenanceControlFactory() { }

		public static ProductContractMaintenanceControlFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductContractMaintenanceControlFactory();
				}

				return singletonInstance;
			}
		}

		public string GetProductContractMaintenanceControlPath(ProductType productType) 
		{
			ProductContractType productContractType;
			string path = "Control\\";

			switch(productType) 
			{
				case ProductType.Magazine :
				{
					path += "MagazineContractMaintenanceControl.ascx";
					break;
				}
				case ProductType.FieldSupplies : 
				{
					path += "FieldSuppliesContractMaintenanceControl.ascx";
					break;
				}
				default : 
				{
					productContractType = ProductContractTypeFactory.Instance.GetProductContractType(productType);

					switch(productContractType) 
					{
						case ProductContractType.GST_HST : 
						{
							path += "DefaultGST_HSTContractMaintenanceControl.ascx";
							break;
						}
						case ProductContractType.Single : 
						{
							path += "DefaultSingleContractMaintenanceControl.ascx";
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
