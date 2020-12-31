using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductReplacementType.
	/// </summary>
	public interface ProductReplacementType
	{
		string ProductTypeName 
		{
			get;
		}

		int SearchID
		{
			get;
		}

		int OrderQualifierID 
		{
			get;
		}
	}
}
