using System;
using System.Data;
using Common;

namespace Business.Rules
{
	/// <summary>
	/// Validates string field lengths
	/// </summary>
	public class StringLength : RulesBase
	{
		public StringLength(Message messageManager) : base(messageManager) { }
		/// <summary>
		/// Validates string field lengths
		/// </summary>
		/// <param name="row">The row to validate</param>
		/// <returns></returns>
		public override bool Validate(DataRow row) 
		{
			DataColumnExtended dcbColumn;
			bool IsValid = true;
			int minLength = -1;
			int maxLength = -1;

			for(int i = 0; i < row.ItemArray.Length; i++) 
			{
				dcbColumn = row.Table.Columns[i] as DataColumnExtended;
				
				if(dcbColumn == null)
					throw new System.ArgumentException("The validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

				if(dcbColumn.IsString) 
				{
					minLength = dcbColumn.MinLength;
					maxLength = dcbColumn.MaxLength;
				
					if(minLength <= maxLength) 
					{
						if( (minLength != -1 &&
							row[i].ToString().Trim().Length < minLength) ||
							(maxLength != -1 &&
							row[i].ToString().Trim().Length > maxLength))
						{
							//
							// Mark the field as invalid
							//
							if(minLength == maxLength) 
							{
								if(dcbColumn.ShowErrorMessage) 
								{
									CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2,new string[]{dcbColumn.Caption, maxLength.ToString()} ));
								}
								IsValid = false;
							} 
							else 
							{
								if(dcbColumn.ShowErrorMessage) 
								{
									CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_LENGTH_RANGE_VAR_3,new string[]{dcbColumn.Caption, minLength == -1 ? "0" : minLength.ToString(), maxLength.ToString()} ));
								}
								IsValid = false;
							}
						}
					}
				}
			}

			/*if(!IsValid) 
			{
				errorType = ExceptionType.MaxLength;
			}*/

			return IsValid;
		}
	}
}
