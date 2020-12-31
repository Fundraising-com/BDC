using System;
using System.Data;
using Common;

namespace Business.Rules
{
	/// <summary>
	/// Validates required fields
	/// </summary>
	public class ConditionalRequiredFields : RulesBase
	{
		public ConditionalRequiredFields(Message messageManager) : base(messageManager) { }

		/// <summary>
		/// Validates required fields
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			DataColumnExtended dcbColumn;
			DataColumnExtended dcbColumnConditional;
			object conditionalValue;
			bool IsValid = true;
			
			for(int i = 0; i < row.ItemArray.Length; i++) 
			{
				dcbColumn = row.Table.Columns[i] as DataColumnExtended;

				if(dcbColumn == null)
					throw new System.ArgumentException("The business rules validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

				if(dcbColumn.IsRequiredConditional)
				{
					dcbColumnConditional = dcbColumn.Table.Columns[dcbColumn.ConditionalField] as DataColumnExtended;

					if(dcbColumnConditional == null)
						throw new System.ArgumentException("The business rules validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

					conditionalValue = row[dcbColumnConditional] == System.DBNull.Value ? dcbColumnConditional.NullValue : row[dcbColumnConditional];

					if ((dcbColumnConditional.IsNumeric && Convert.ToDouble(conditionalValue) == Convert.ToDouble(dcbColumn.ConditionalValue)) ||
						(dcbColumnConditional.IsDateTime && Convert.ToDateTime(conditionalValue) == Convert.ToDateTime(dcbColumn.ConditionalValue)) ||
						(dcbColumnConditional.IsBoolean && Convert.ToBoolean(conditionalValue) == Convert.ToBoolean(dcbColumn.ConditionalValue)) ||
						conditionalValue.ToString() == dcbColumn.ConditionalValue.ToString())
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
			}

			return IsValid;
		}
	}
}
