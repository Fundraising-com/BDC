using System;
using System.Data;
using Common;

namespace Business.Rules
{
	/// <summary>
	/// Validates string field lengths
	/// </summary>
	public class Range : RulesBase
	{
		public Range(Message messageManager) : base(messageManager) { }
		/// <summary>
		/// Validates string field lengths
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
					throw new System.ArgumentException("The validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

				if(dcbColumn.MinValue != null && dcbColumn.MaxValue != null) {
					if(dcbColumn.IsNumeric) 
					{
						double dMinValue = Convert.ToDouble(dcbColumn.MinValue);
						double dMaxValue = Convert.ToDouble(dcbColumn.MaxValue);

						if(dMinValue <= dMaxValue) 
						{
							if(Convert.ToDouble(row[i]) < dMinValue ||
								Convert.ToDouble(row[i]) > dMaxValue)
							{
								//
								// Mark the field as invalid
								//
								if(dcbColumn.ShowErrorMessage) 
								{
									CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_RANGE_VAR_3,new string[]{dcbColumn.Caption, dMinValue.ToString(), dMaxValue.ToString()} ));
								}
								IsValid = false;
							}
						}
					} 
					else if(dcbColumn.IsDateTime)
					{
						DateTime dtMinValue = Convert.ToDateTime(dcbColumn.MinValue);
						DateTime dtMaxValue = Convert.ToDateTime(dcbColumn.MaxValue);

						if(dtMinValue <= dtMaxValue) 
						{
							if(Convert.ToDateTime(row[i]) < dtMinValue ||
								Convert.ToDateTime(row[i]) > dtMaxValue) 
							{
								//
								// Mark the field as invalid
								//
								if(dcbColumn.ShowErrorMessage) 
								{
									CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_RANGE_VAR_3,new string[]{dcbColumn.Caption, dtMinValue.ToString(), dtMaxValue.ToString()} ));
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
