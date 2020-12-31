using System;
using QSPFulfillment.DataAccess;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for ISearch.
	/// </summary>
	public interface ISearch
	{
		 ParameterValueList GetParameterValue(string StartParameterName);
	}
}
