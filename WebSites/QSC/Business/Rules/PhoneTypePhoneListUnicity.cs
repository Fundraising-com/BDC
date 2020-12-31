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
	public class PhoneTypePhoneListUnicity : RulesBase
	{
		public PhoneTypePhoneListUnicity(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(BusinessSystem entity, DataRowState state)
		{
			bool bIsValid = true;
			PhoneDataSet oPhoneDataSet = entity.baseDataSet.GetChanges(state) as PhoneDataSet;
			DataView dv;

			if(oPhoneDataSet != null) 
			{
				foreach(PhoneDataSet.PhoneRow row in oPhoneDataSet.Phone) 
				{
					if(row.RowState != DataRowState.Deleted) 
					{
						dv = new DataView(oPhoneDataSet.Phone, oPhoneDataSet.Phone.TypeColumn.ColumnName + " = " + row.Type.ToString(), "", DataViewRowState.CurrentRows);

						if(dv.Count > 1) 
						{
							this.CurrentMessageManager.Add(Message.ERRMSG_PHONE_TYPE_PHONE_LIST_UNICITY_0);

							bIsValid = false;
							break;
						}
					}
				}
			}

			return bIsValid;
		}
	}
}
