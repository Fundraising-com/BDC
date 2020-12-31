using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for ProductTypeValidationNamingConvention.
	/// </summary>
	internal class ProductTypeValidationNamingConvention
	{
		private static ProductTypeValidationNamingConvention singletonInstance;

		private ProductTypeValidationNamingConvention() { }

		internal static ProductTypeValidationNamingConvention Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductTypeValidationNamingConvention();
				}

				return singletonInstance;
			}
		}

		internal string GetValidationStrategyClassName(ProductType productType) 
		{
			return this.GetType().Namespace + "." + productType.ToString() + "ValidationStrategy";
		}
	}
}
