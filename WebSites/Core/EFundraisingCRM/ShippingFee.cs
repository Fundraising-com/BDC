using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ShippingFee.
	/// </summary>
	public class ShippingFee
	{

		private int shippingFeeID;
		private int saleAmtMin;
		private int saleAmtMax;
		private decimal shippingFee;

		public ShippingFee()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Data Source Methods
		
		public static ShippingFee GetShippingFeeByProductIDAndTotalAmout(int productID, decimal totalAmount)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			//check if product id is in business table, if not we use the product class
			EFundraisingCRM.ProductBusinessRules br = EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(productID);
			
			if (br != null)
			{
				return dbo.GetShippingFeeByBusinessRuleIDAndTotalAmount(br.ProductBusinessRuleID,totalAmount);
			}
			else
			{
				return null;
			}
			
		}
		#endregion


		#region Properties
		public int ShippingFeeID
		{
			set { shippingFeeID = value; }
			get { return shippingFeeID; }
		}

		public int SaleAmtMin
		{
			set { saleAmtMin = value; }
			get { return saleAmtMin; }
		}

		public int SaleAmtMax
		{
			set { saleAmtMax = value; }
			get { return saleAmtMax; }
		}

		public decimal _ShippingFee
		{
			set { shippingFee = value; }
			get { return shippingFee; }
		}

#endregion
	}
}
