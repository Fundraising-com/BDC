using System;
using System.Collections;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for PackageDisplayOrderComparer.
	/// </summary>
	public class ProductDisplayOrderComparer : IComparer
	{
		public ProductDisplayOrderComparer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int Compare(object x, object y) 
		{
			return ((Product)x).ProductDescription.DisplayOrder.CompareTo(((Product)y).ProductDescription.DisplayOrder);
		}
	}
}
