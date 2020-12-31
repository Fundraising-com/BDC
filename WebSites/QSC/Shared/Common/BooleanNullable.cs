using System;

namespace Common
{
	/// <summary>
	/// Summary description for BooleanNullable.
	/// </summary>
	public class BooleanNullable
	{
		private int internalValue = 0;

		public BooleanNullable(object value) 
		{
			Value = value;
		}

		public BooleanNullable(bool boolValue) 
		{
			BoolValue = boolValue;
		}

		public BooleanNullable(int intValue) 
		{
			IntValue = intValue;
		}

		public bool IsDBNull 
		{
			get 
			{
				return internalValue == -1;
			}
		}

		public object Value 
		{
			get 
			{
				object value = DBNull.Value;

				if(internalValue != -1) 
				{
					value = Convert.ToBoolean(internalValue);
				}

				return value;
			}
			set 
			{
				if(value is DBNull) 
				{
					internalValue = -1;
				} 
				else if(value is bool) 
				{
					internalValue = Convert.ToInt32(value);
				} 
				else 
				{
					throw new InvalidCastException();
				}
			}
		}

		public bool BoolValue 
		{
			get 
			{
				if(internalValue != -1) 
				{
					return Convert.ToBoolean(internalValue);
				} 
				else 
				{
					throw new InvalidCastException();
				}
			}
			set 
			{
				internalValue = Convert.ToInt32(value);
			}
		}

		public int IntValue 
		{
			get 
			{
				return internalValue;
			}
			set 
			{
				if(value == -1 || value == 0 || value == 1) 
				{
					internalValue = value;
				} 
				else 
				{
					throw new InvalidCastException();
				}
			}
		}
	}
}
