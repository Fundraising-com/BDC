using System;
using System.Runtime.Serialization;

namespace QSPFulfillment.DataAccess
{
	/// <summary>
	/// Summary description for ParameterValue.
	/// </summary>
	/// 
	[Serializable()]
	public class ParameterValue
	{
		string sParameter="";
		string sValue ="";
		

		public ParameterValue(string Parameter,string Value)
		{
			this.sParameter = Parameter;
			this.sValue = Value;
		}
		
		public string Parameter
		{
			get
			{
				return sParameter;
			}
				set
				{
				this.sParameter = value;
				}
		}

		public string Value
		{
			get
			{
				return sValue;
			}
			set
			{
				this.sValue = value;
			}
		}
	
	

	}
}
