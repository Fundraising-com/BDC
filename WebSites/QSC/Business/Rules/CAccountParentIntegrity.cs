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
	public class CAccountParentIntegrity : RulesBase
	{
		public CAccountParentIntegrity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool bIsValid = true;
			CAccountDataSet.CAccountRow aRow = row as CAccountDataSet.CAccountRow;
			CAccount oCAccount;

			if(aRow != null && aRow.ParentID != 0) 
			{
				if(aRow.ParentID == aRow.Id) 
				{
					this.CurrentMessageManager.Add(Message.ERRMSG_ACCOUNT_PARENT_ID_0);

					bIsValid = false;
				} 
				else 
				{
					oCAccount = new CAccount(aRow.ParentID, this.CurrentTransaction);

					if(oCAccount.dataSet.CAccount.Count == 0) 
					{
						this.CurrentMessageManager.Add(Message.ERRMSG_ACCOUNT_PARENT_INTEGRITY_0);

						bIsValid = false;
					} 
					else 
					{
						if(aRow.Id == oCAccount.dataSet.CAccount[0].ParentID) 
						{
							this.CurrentMessageManager.Add(Message.ERRMSG_ACCOUNT_PARENT_SELF_0);

							bIsValid = false;
						}
					}
				}
			}

			return bIsValid;
		}
	}
}
