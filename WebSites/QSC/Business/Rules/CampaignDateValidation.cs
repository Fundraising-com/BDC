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
	public class CampaignDateValidation : RulesBase
	{
		private const int MAXIMUM_DAYS_ALLOWED = 30;

		public CampaignDateValidation(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool bIsValid = true;
			CampaignDataSet.CampaignRow caRow = row as CampaignDataSet.CampaignRow;

			if(caRow != null) 
			{
				if(caRow.StartDate > caRow.EndDate) 
				{
					if(((CampaignDataSet) row.Table.DataSet).Campaign.StartDateColumn.ShowErrorMessage &&
						((CampaignDataSet) row.Table.DataSet).Campaign.EndDateColumn.ShowErrorMessage)
					this.CurrentMessageManager.Add(this.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CANNOT_BE_HIGHER_2, new string[] {((CampaignDataSet) row.Table.DataSet).Campaign.StartDateColumn.Caption, ((CampaignDataSet) row.Table.DataSet).Campaign.EndDateColumn.Caption}));

					bIsValid = false;
				}
				//MS Jun 18, 2007
				if(caRow.DateSubmitted.AddDays(1) > caRow.EndDate) 
				{
					if(((CampaignDataSet) row.Table.DataSet).Campaign.DateSubmittedColumn.ShowErrorMessage &&((CampaignDataSet) row.Table.DataSet).Campaign.EndDateColumn.ShowErrorMessage)
						this.CurrentMessageManager.Add(Message.ERRMSG_CAMPAIGN_END_DATE_0);

					bIsValid = false;
				}


				/*if(caRow.StartDate < DateTime.Now.AddDays(MAXIMUM_DAYS_ALLOWED)) 
				{
					this.CurrentMessageManager.Add(this.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CAMPAIGN_START_DATE_1, MAXIMUM_DAYS_ALLOWED.ToString()));

					bIsValid = false;
				}*/

				//TODO: Business Rule for FS date

			}

			return bIsValid;
		}
	}
}
