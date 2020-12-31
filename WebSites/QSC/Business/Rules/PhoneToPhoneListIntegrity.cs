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
	public class PhoneToPhoneListIntegrity : RulesBase
	{
		public PhoneToPhoneListIntegrity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			bool IsValid = true;
			PhoneDataSet.PhoneRow phRow = row as PhoneDataSet.PhoneRow;
			PhoneList phList;

			if(phRow != null) 
			{
				if(phRow.PhoneListID != 0) 
				{
					phList = new PhoneList();
					if(this.CurrentTransaction != null) 
					{
						phList.CurrentTransaction = this.CurrentTransaction;
					}

					phList.GetOneByID(phRow.PhoneListID);

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

					foreach(PhoneDataSet.PhoneRow phRowNew in ((PhoneDataSet) phRow.Table.DataSet).Phone) 
					{
						if(phRowNew.PhoneListID == 0) 
						{
							phRowNew.PhoneListID = phList.dataSet.PhoneList[0].ID;
						}
					}
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
