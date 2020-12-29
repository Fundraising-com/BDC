using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Class helps to convert small integer values
	/// to boolean-style string value
	/// </summary>
	public class IsValidated:EFundraisingCRMObject
	{
		private string valid;
		public IsValidated(int input)
		{
			setValue(input);
		}
		public string Valid
		{
			get{return valid;}
			set
			{
				setValue(Convert.ToInt32(value));				
			}
		}
		public static int getIntValue(string input)
		{
			switch(input)
			{
				case "Undefined":
					return (Int32)int.MinValue;
				case "False":
					return (Int32)0;
				case "True":
					return (Int32)1;
				case "Maybe":
					return (Int32)2;
				default:
					throw new System.ArgumentException("Invalid Argument");

			}
		}
		private void setValue(int input)
		{
			switch(input)
			{
				case int.MinValue:
					valid = "Undefined";
					break;
				case 0:
					valid ="False";
					break;
				case 1:
					valid ="True";
					break;
				case 2:
					valid = "Maybe";
					break;
				default:
					throw new System.ArgumentException("Invalid Argument");

			}
		}
	}
}
