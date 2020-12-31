using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for GiftReplacement.
	/// </summary>
	[Serializable]
	public class CookieDoughReplacement : ProductReplacementType
	{
		private const string PRODUCT_TYPE_NAME = "CookieDough";
		private const int SEARCH_ID = 3;
		private const int ORDER_QUALIFIER_ID = 39019;

		public string ProductTypeName
		{
			get
			{
				return PRODUCT_TYPE_NAME;
			}
		}

		public int SearchID
		{
			get
			{
				return SEARCH_ID;
			}
		}

		public int OrderQualifierID
		{
			get
			{
				return ORDER_QUALIFIER_ID;
			}
		}
	}
}
