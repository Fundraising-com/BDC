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
	public class CampaignProgramGroupProfitRange : RulesBase
	{
		public CampaignProgramGroupProfitRange(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			CampaignProgramDataSet.ProgramRow pRow;
			CampaignProgramDataSet.CampaignProgramRow cpRow = row as CampaignProgramDataSet.CampaignProgramRow;

			if(cpRow != null) 
			{
				pRow = (CampaignProgramDataSet.ProgramRow) cpRow.GetParentRow(cpRow.Table.DataSet.Relations[0]);

				if(pRow.MinProfit != 0 && cpRow.GroupProfit < pRow.MinProfit) 
				{
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_MIN_PROFIT_2, new string[] {pRow.Name, pRow.MinProfit.ToString() }));
					IsValid = false;
				} 
				else if(pRow.MaxProfit != 0 && cpRow.GroupProfit > pRow.MaxProfit) 
				{
					CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_MAX_PROFIT_2, new string[] {pRow.Name, pRow.MaxProfit.ToString() }));
					IsValid = false;
				}
			}

			return IsValid;
		}
	}
}
