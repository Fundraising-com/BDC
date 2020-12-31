using System;
using System.Reflection;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for ProductTypeValidationFactory.
	/// </summary>
	/// <remarks>
	///		Instance of the Simple Factory Pattern
	///		Implements the Singleton Pattern
	/// </remarks>
	internal class ProductTypeValidationFactory
	{
		private static ProductTypeValidationFactory singletonInstance;

		private ProductTypeValidationFactory() { }

		internal static ProductTypeValidationFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductTypeValidationFactory();
				}

				return singletonInstance;
			}
		}

		internal ProductTypeValidationStrategy GetProductTypeValidationStrategy(ProductType productType) 
		{
			ProductTypeValidationStrategy strategy;

			try 
			{
				strategy = (ProductTypeValidationStrategy) System.Activator.CreateInstance(null, ProductTypeValidationNamingConvention.Instance.GetValidationStrategyClassName(productType), false, BindingFlags.Default, null, new object[] {}, null, null, null).Unwrap();

				if(strategy == null) 
				{
					throw new Exception("The Product Type validation strategy does not exist.");
				}
			} 
			catch
			{
				throw new ArgumentException("The naming convention has not been respected and the strategy cannot be loaded.");
			}

			return strategy;
		}
	}
}
