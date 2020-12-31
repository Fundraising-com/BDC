using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class ProductReplacementTypeFactory
	{
		private static ProductReplacementTypeFactory singletonInstance;

		private ProductReplacementTypeFactory() { }

		public static ProductReplacementTypeFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new ProductReplacementTypeFactory();
				}
				
				return singletonInstance;
			}
		}

		public ProductReplacementType GetProductReplacementType(string ProductTypeName) 
		{
			ProductReplacementType productReplacementType;

			switch(ProductTypeName) 
			{
				case "Gift" : 
				{
					productReplacementType = new GiftReplacement();
					break;
				}
				case "Kanata" : 
				{
					productReplacementType = new KanataReplacement();
					break;
				}
                case "CookieDough":
                {
                    productReplacementType = new CookieDoughReplacement();
                    break;
                }
                default: 
				{
					productReplacementType = new GiftReplacement();
					break;
				}
			}

			return productReplacementType;
		}
	}
}
