using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for SubdivisionCollection.
	/// </summary>
	public class SubdivisionCollection:EFundraisingCRMCollectionBase
	{
		public SubdivisionCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllSubdivisions() 
		{
			foreach(Subdivision s in Subdivision.GetSubdivisions()) 
			{
				List.Add(s);
			}

		}
		public void GetSubdivisionsByCountryCode(string country)
		{
			foreach(Subdivision s in Subdivision.GetSubdivisionsByCountryCode(country))
			{
				List.Add(s);
			}
		}
		#endregion
	}
}
