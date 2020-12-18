using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for PackageCollection.
	/// </summary>

	public class PackageCollection: EFundraisingCRMCollectionBase
	{
		public PackageCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void GetAllPackages() 
		{
			Package[] pks = Package.GetPackages() ;
			for (int i=0; i< pks.Length; i++)
			{
				List.Add(pks[i]);
			}
		}

		



	}
}
