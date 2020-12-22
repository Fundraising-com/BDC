using System;
using System.Collections.Generic;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for Profit_Range.
	/// </summary>
	public class ProfitRange
	{

		private int profitRangeID;
		private int itemNbrMin;
		private int itemNbrMax;
		private decimal profitPercentage;

		public ProfitRange()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region Data Source Methods
		
		public static List<ProfitRange> GetProfitRangeByProductBusinessRuleID(int productBusinessRuleID)
		{
			//check if product id is individually in the table, if not the product class is used
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProfitRangeByProductBusinessRuleID(productBusinessRuleID);
			
		}



		#endregion

		#region Properties
		public int ProfitRangeID
		{
			set { profitRangeID = value; }
			get { return profitRangeID; }
		}

		public int ItemNbrMin
		{
			set { itemNbrMin = value; }
			get { return itemNbrMin; }
		}

		public int ItemNbrMax
		{
			set { itemNbrMax = value; }
			get { return itemNbrMax; }
		}

		public decimal ProfitPercentage
		{
			set { profitPercentage = value; }
			get { return profitPercentage; }
		}
		#endregion
	}
}
