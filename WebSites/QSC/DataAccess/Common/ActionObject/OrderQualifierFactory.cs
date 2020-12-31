using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductReplacementTypeFactory.
	/// </summary>
	public class OrderQualifierFactory
	{
		private static OrderQualifierFactory singletonInstance;

		private OrderQualifierFactory() { }

		public static OrderQualifierFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new OrderQualifierFactory();
				}
				
				return singletonInstance;
			}
		}

		public OrderQualifier GetOrderQualifierForProductReplacement(ProductReplacementType productReplacementType) 
		{
			OrderQualifier orderQualifier = OrderQualifier.GiftPSolver;

			if(productReplacementType is GiftReplacement) 
			{
				orderQualifier = OrderQualifier.GiftPSolver;
			} 
			else if(productReplacementType is KanataReplacement)
			{
				orderQualifier = OrderQualifier.KanataPSolver;
			}

			return orderQualifier;
		}
		public OrderQualifier GetOrderQualifierForKanataDE(ProductReplacementType productReplacementType) 
		{
			OrderQualifier orderQualifier = OrderQualifier.Kanata;

			return orderQualifier;
		}

		public OrderQualifier GetOrderQualifierFromCatalogName(string catalogName)
		{
			if (catalogName.IndexOf("WFC") >= 0)
				return OrderQualifier.WFCSigningBonus;

			//else if (catalogName.Substring(0,3) == "Kan")
			//	return OrderQualifier.Kanata;

			else
				return OrderQualifier.None;
		}
	}
}
