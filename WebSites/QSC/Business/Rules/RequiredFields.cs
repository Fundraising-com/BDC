using System;
using System.Data;
using Common;

namespace Business.Rules
{
	/// <summary>
	/// Validates required fields
	/// </summary>
	public class RequiredFields : RulesBase
	{
		public RequiredFields(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			DataColumnExtended dcbColumn;
			bool IsValid = true;
			
			for(int i = 0; i < row.ItemArray.Length; i++) 
			{
				dcbColumn = row.Table.Columns[i] as DataColumnExtended;

				if(dcbColumn == null)
					throw new System.ArgumentException("The business rules validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

				if(dcbColumn.IsRequired)
				{
					if(row[i] == System.DBNull.Value || ((dcbColumn.IsNumeric && Convert.ToDouble(row[i]) == Convert.ToDouble(dcbColumn.NullValue)) ||
						(dcbColumn.IsDateTime && Convert.ToDateTime(row[i]) == Convert.ToDateTime(dcbColumn.NullValue)) ||
						(dcbColumn.IsBoolean && Convert.ToBoolean(row[i]) == Convert.ToBoolean(dcbColumn.NullValue)) ||
						row[i].ToString() == dcbColumn.NullValue.ToString()))
					{
						//
						// Mark the field as invalid
						//
						if(dcbColumn.ShowErrorMessage) 
						{
							CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,new string[]{dcbColumn.Caption}));
						}
						IsValid = false;
					}
				}
			}

			/*if(!IsValid) 
			{
				errorType = ExceptionType.RequiredFields;
			}*/

			return IsValid;
		}
	}
}
