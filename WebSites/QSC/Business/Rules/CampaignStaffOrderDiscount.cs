using System;
using System.Data;
using Common;
using Business.Objects;
using Common.TableDef;

namespace Business.Rules
{
	/// <summary>
	/// Validates required fields
	/// </summary>
	public class CampaignStaffOrderDiscount : RulesBase
	{
		public CampaignStaffOrderDiscount(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			CampaignDataSet.CampaignRow caRow = row as CampaignDataSet.CampaignRow;

			if(caRow != null) 
			{
				if(caRow.IsStaffOrder) 
				{
					caRow.StaffOrderDiscount = Convert.ToDecimal(00.00);
				}
				else 
				{
					caRow.StaffOrderDiscount = Convert.ToDecimal(0.00);
				}
			}

			return true;
		}
	}
}
