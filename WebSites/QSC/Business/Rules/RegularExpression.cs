using System;
using System.Data;
using Common;
using System.Text.RegularExpressions;


namespace Business.Rules
{
	/// <summary>
	/// Summary description for RegularExpression.
	/// </summary>
	public class RegularExpression : RulesBase
	{
		public RegularExpression(Message messageManager) : base(messageManager) { }
		
		public override bool Validate(DataRow row) 
		{
			DataColumnExtended dcbColumn;
			bool IsValid = true;

			for(int i = 0; i < row.ItemArray.Length; i++) 
			{
				dcbColumn = row.Table.Columns[i] as DataColumnExtended;
				
				if(dcbColumn == null)
					throw new System.ArgumentException("The validation system expects columns to be of DataColumnExtended type.", row.Table.Columns[i].ColumnName);

				if(dcbColumn.RegularExpression != string.Empty) 
				{
					Regex RegValidator = new Regex(dcbColumn.RegularExpression);
				
					if(RegValidator.IsMatch(row[i].ToString()))
					{
						IsValid = true;
					}
					else
					{
						if(dcbColumn.ShowErrorMessage) 
						{
							CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_REGULAR_EXPRESSION_VAR_1,new string[]{dcbColumn.Caption}));
						}
						IsValid = false;
					}
				}
			}

			return IsValid;
		}
	}
}
