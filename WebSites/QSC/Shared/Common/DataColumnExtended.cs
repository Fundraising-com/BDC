using System;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace Common
{
	/// <summary>
	/// Summary description for DataColumnExtended.
	/// </summary>
	[Editor]
	[ToolboxItem (false)]
	[DefaultMember ("Item")]
	[DefaultProperty ("ColumnName")]
	[DesignTimeVisible (false)]
	public class DataColumnExtended : DataColumn
	{		
		#region Constructors

		public DataColumnExtended() : base()
		{
		}

		//TODO: Ctor init vars directly
		public DataColumnExtended(string columnName): base(columnName)
		{
			//ColumnName = columnName;
		}

		public DataColumnExtended(string columnName, Type dataType): base(columnName, dataType)
		{
			/*if(dataType == null) 
			{
				throw new ArgumentNullException("dataType can't be null.");
			}
			
			DataType = dataType;*/
		}

		public DataColumnExtended( string columnName, Type dataType, 
			string expr): base(columnName, dataType, expr)
		{
			//if (expr != null) Expression = expr;
		}

		public DataColumnExtended(string columnName, Type dataType, 
			string expr, MappingType type): base(columnName, dataType, expr, type)
		{
			//ColumnMapping = type;
		}
		#endregion

		#region Properties

		//[DataCategory ("Data")]
		[DataSysDescription ("Indicates whether empty values are allowed in this column.")]
		[DefaultValue (false)]
		public bool IsRequired 
		{
			get 
			{
				if(this.ExtendedProperties["IsRequired"] == null)
					return false;

				return Convert.ToBoolean(this.ExtendedProperties["IsRequired"]);
			}
			set 
			{
				// Validation happens in the Business Logic Tier
				this.ExtendedProperties["IsRequired"] = value;
			}
		}

		//[DataCategory ("Data")]
		[DataSysDescription ("Indicates whether empty values are allowed in this column with a condition.")]
		[DefaultValue (false)]
		public bool IsRequiredConditional 
		{
			get 
			{
				if(this.ExtendedProperties["IsRequiredConditional"] == null)
					return false;

				return Convert.ToBoolean(this.ExtendedProperties["IsRequiredConditional"]);
			}
			set 
			{
				// Validation happens in the Business Logic Tier
				this.ExtendedProperties["IsRequiredConditional"] = value;
			}
		}

		//[DataCategory ("Data")]
		[DataSysDescription ("Indicates the value considered as empty in this column.")]
		[DefaultValue (null)]
		public object NullValue 
		{
			get 
			{
				if(this.ExtendedProperties["nullValue"] == null) 
				{
					if(this.IsNumeric) 
					{
						return -1;
					} 
					else if(this.IsString) 
					{
						return String.Empty;
					}
					else if(this.IsDateTime) 
					{
						return new DateTime(1995, 1, 1);
					}
					else 
					{
						return null;
					}
				} 
				else 
				{
					return this.ExtendedProperties["nullValue"];
				}
			}
			set 
			{
				this.ExtendedProperties["nullValue"] = value;
			}
		}

		public bool IsDBNullOnNullValue 
		{
			get 
			{
				bool isDBNullOnNullValue = false;

				try 
				{
					isDBNullOnNullValue = Convert.ToBoolean(this.ExtendedProperties["isDBNullOnNullValue"]);
				}
				catch { }

				return isDBNullOnNullValue;
			}
			set 
			{
				this.ExtendedProperties["isDBNullOnNullValue"] = value;
			}
		}

		//[DataCategory ("Data")]
		[DataSysDescription ("Indicates the minimum length of the value this column allows.")]
		[DefaultValue (-1)]
		public int MinLength 
		{
			get 
			{
				//Default == -1 no min length
				if(this.ExtendedProperties["MinLength"] == null)
					return -1;

				return Convert.ToInt32(this.ExtendedProperties["MinLength"]);
			}
			set 
			{
				//only applies to string columns
				if(this.IsString) 
				{
					this.ExtendedProperties["MinLength"] = value;
				} 
				else 
				{
					this.ExtendedProperties["MinLength"] = -1;
				}
			}
		}

		[DataSysDescription ("Indicates the minimum value of the value this column allows.")]
		public object MinValue
		{
			get 
			{
				return this.ExtendedProperties["MinValue"];
			}
			set 
			{
				this.ExtendedProperties["MinValue"] = value;
			}
		}

		[DataSysDescription ("Indicates the maximum value of the value this column allows.")]
		public object MaxValue
		{
			get 
			{
				return this.ExtendedProperties["MaxValue"];
			}
			set 
			{
				this.ExtendedProperties["MaxValue"] = value;
			}
		}

		public string ConditionalField 
		{
			get 
			{
				if(this.ExtendedProperties["ConditionalField"] == null)
					return "";

				return this.ExtendedProperties["ConditionalField"].ToString();
			}
			set 
			{
				this.ExtendedProperties["ConditionalField"] = value;
			}
		}

		public object ConditionalValue 
		{
			get 
			{
				return this.ExtendedProperties["ConditionalValue"];
			}
			set 
			{
				this.ExtendedProperties["ConditionalValue"] = value;
			}
		}

		public string RegularExpression
		{
			get
			{
				if (this.ExtendedProperties["RegularExpression"] == null)
				{
					return String.Empty;
				}
				else
				{
					return this.ExtendedProperties["RegularExpression"].ToString();
				}

			}
			set
			{
				this.ExtendedProperties["RegularExpression"] = value;
			}
		}

		public bool ShowErrorMessage 
		{
			get 
			{
				bool showErrorMessage = true;

				if(this.ExtendedProperties["ShowErrorMessage"] != null) 
				{
					try 
					{
						showErrorMessage = Convert.ToBoolean(this.ExtendedProperties["ShowErrorMessage"]);
					} 
					catch { }
				}

				return showErrorMessage;
			}
			set 
			{
				this.ExtendedProperties["ShowErrorMessage"] = value;
			}
		}

		public bool IsString 
		{
			get 
			{
				return (Type.GetTypeCode(this.DataType) == TypeCode.String);
			}
		}

		public bool IsNumeric 
		{
			get 
			{
				return (Type.GetTypeCode(this.DataType) == TypeCode.Byte	||
						Type.GetTypeCode(this.DataType) == TypeCode.Decimal ||
						Type.GetTypeCode(this.DataType) == TypeCode.Double	||
						Type.GetTypeCode(this.DataType) == TypeCode.Int16	||
						Type.GetTypeCode(this.DataType) == TypeCode.Int32	||
						Type.GetTypeCode(this.DataType) == TypeCode.Int64	||
						Type.GetTypeCode(this.DataType) == TypeCode.SByte	||
						Type.GetTypeCode(this.DataType) == TypeCode.Single	||
						Type.GetTypeCode(this.DataType) == TypeCode.UInt16	||
						Type.GetTypeCode(this.DataType) == TypeCode.UInt32	||
						Type.GetTypeCode(this.DataType) == TypeCode.UInt64);
			}
		}

		public bool IsBoolean 
		{
			get 
			{
				return (Type.GetTypeCode(this.DataType) == TypeCode.Boolean);
			}
		}

		public bool IsDateTime
		{
			get 
			{
				return (Type.GetTypeCode(this.DataType) == TypeCode.DateTime);
			}
		}

		#endregion // Properties

	}
}