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
	public class ContactToPhoneListIntegrity : RulesBase
	{
		public ContactToPhoneListIntegrity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			ContactDataSet.ContactRow cRow = row as ContactDataSet.ContactRow;
			PhoneList phList;

			if(cRow != null) 
			{
				if(cRow.PhoneListID != 0) 
				{
					phList = new PhoneList();
					if(this.CurrentTransaction != null) 
					{
						phList.CurrentTransaction = this.CurrentTransaction;
					}

					phList.GetOneByID(cRow.PhoneListID);

					if(phList.dataSet.PhoneList.Rows.Count == 0)
					{
						CurrentMessageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
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

					cRow.PhoneListID = phList.dataSet.PhoneList[0].ID;
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
