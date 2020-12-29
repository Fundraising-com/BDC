using System;
using System.Collections;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for PackageDisplayOrderComparer.
	/// </summary>
	public class PackageDisplayOrderComparer : IComparer
	{
		public PackageDisplayOrderComparer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int Compare(object x, object y) 
		{
			System.Diagnostics.Debug.Write(((Package)x).PackageDescription.DisplayOrder);
			return ((Package)x).PackageDescription.DisplayOrder.CompareTo(((Package)y).PackageDescription.DisplayOrder);
		}
	}
}
