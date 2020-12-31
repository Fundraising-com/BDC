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
	public class CampaignToPhoneListIntegrity : RulesBase
	{
		public CampaignToPhoneListIntegrity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			CampaignDataSet.CampaignRow caRow = row as CampaignDataSet.CampaignRow;
			PhoneList phList;

			if(caRow != null) 
			{
				if(caRow.PhoneListID != Convert.ToInt32(((CampaignDataSet) caRow.Table.DataSet).Campaign.PhoneListIDColumn.DefaultValue))
				{
					phList = new PhoneList();
					if(this.CurrentTransaction != null) 
					{
						phList.CurrentTransaction = this.CurrentTransaction;
					}

					phList.GetOneByID(caRow.PhoneListID);

					if(phList.dataSet.PhoneList.Rows.Count == 0)
					{
						if(((CampaignDataSet) row.Table.DataSet).Campaign.PhoneListIDColumn.ShowErrorMessage) 
						{
							CurrentMessageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
						}
						IsValid = false;
					} 
				} 
				else 
				{
					phList = new PhoneList();
					if(this.CurrentTransaction != null) 
					{
						phList.CurrentTransaction = this.CurrentTransaction;
					}

					phList.dataSet.PhoneList.AddPhoneListRow(DateTime.Now, false);
					phList.Save();

					caRow.PhoneListID = phList.dataSet.PhoneList[0].ID;
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
