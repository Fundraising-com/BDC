using System;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for ISearch.
	/// </summary>
	public interface ISearch
	{
		
		string ParameterName
		{
			get;
			set;
		}
		string Value
		{
			get;
		
		}
		string ContentType
		{
			get;
			set;
		}

		bool Validation
		{
			get;
			set;
		}
	}
}
