using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductContractID.
	/// </summary>
	[Serializable]
	public abstract class ProductContractID
	{
		private const int EMPTY_VALUE = 0;

		public abstract int MagPriceInstanceGST
		{
			get;
			set;
		}

		public abstract int MagPriceInstanceHST
		{
			get;
			set;
		}

		public static int EmptyValue 
		{
			get 
			{
				return EMPTY_VALUE;
			}
		}
	}
}
