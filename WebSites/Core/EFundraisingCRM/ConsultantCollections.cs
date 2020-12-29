using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ConsultantCollections.
	/// </summary>
	public class ConsultantCollections: EFundraisingCRMCollectionBase
	{
		public ConsultantCollections()
		{
			//
			// TODO: Add constructor logic here
			//
		}



		public Consultant SearchByConsultantID(int ctId)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				(this.List[i] as Consultant).sortByPropertyName = "ConsultantId";
			}
			int index = this.InnerList.BinarySearch(ctId);
			return List[index] as Consultant;
		}


		public Consultant SearchByConsultantName(string ctName)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				(this.List[i] as Consultant).sortByPropertyName = "Name";
			}

			int index = this.InnerList.BinarySearch(ctName);
			return List[index] as Consultant;
		}

	}
}
