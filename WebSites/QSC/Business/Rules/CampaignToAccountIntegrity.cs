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
	public class CampaignToAccountIntegrity : RulesBase
	{
		public CampaignToAccountIntegrity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			CampaignDataSet.CampaignRow caRow = row as CampaignDataSet.CampaignRow;
			CAccount oCAccount;

			if(caRow != null) 
			{
				oCAccount = new CAccount();
				if(this.CurrentTransaction != null) 
				{
					oCAccount.CurrentTransaction = this.CurrentTransaction;
				}

				oCAccount.GetOneById(caRow.ShipToAccountID);

				if(oCAccount.dataSet.CAccount.Rows.Count == 0) 
				{
					if(((CampaignDataSet) row.Table.DataSet).Campaign.ShipToAccountIDColumn.ShowErrorMessage)
					{
						CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ACCOUNT_DOES_NOT_EXIST_1, "Ship To"));
					}
					IsValid = false;
				} 

				oCAccount = new CAccount();
				if(this.CurrentTransaction != null) 
				{
					oCAccount.CurrentTransaction = this.CurrentTransaction;
				}

				oCAccount.GetOneById(caRow.BillToAccountID);

				if(oCAccount.dataSet.CAccount.Rows.Count == 0) 
				{
					if(((CampaignDataSet) row.Table.DataSet).Campaign.BillToAccountIDColumn.ShowErrorMessage) 
					{
						CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ACCOUNT_DOES_NOT_EXIST_1, "Bill To"));
					}
					IsValid = false;
				}
			}
			else 
			{
				IsValid = false;
			}

			return IsValid;
		}
	}
}
