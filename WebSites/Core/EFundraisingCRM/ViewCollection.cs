using System;
using System.Collections;
using GA.BDC.Core.EFundraisingCRM;
namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Class represent the coolection of generic objects
	/// of CRMCollectionBase, requires by ViewsFilter class
	/// for internal manipulations
	/// </summary>
	public class ViewCollection:EFundraisingCRMCollectionBase
	{
		private ArrayList col = null;
		public ViewCollection()
		{
			col = new ArrayList();
		}
		#region Public Functions
		public int Add(EFundraisingCRMDataObject o)
		{
			return col.Add(o);
		}
		public void Remove (EFundraisingCRMDataObject o)
		{
			col.Remove(o);
		}
		public bool Contains(EFundraisingCRMDataObject o)
		{
			return (bool) col.Contains(o);
		}
		#endregion
		#region Public Propeties
		public ArrayList Collection
		{
			get { return col;}
			set{ col= value;}
		}
		#endregion
	}
}
