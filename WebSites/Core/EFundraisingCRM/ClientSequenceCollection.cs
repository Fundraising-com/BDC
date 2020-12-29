using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ClientSequenceCollection.
	/// </summary>

	public class ClientSequenceCollection: EFundraisingCRMCollectionBase
	{
		public ClientSequenceCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void GetAllClientSequences() 
		{
			ClientSequence[] clSeqs = ClientSequence.GetClientSequences() ;
			for (int i=0; i< clSeqs.Length; i++)
			{
				List.Add(clSeqs[i]);
			}
		}

	}
}
