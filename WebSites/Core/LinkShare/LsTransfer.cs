using System;
using GA.BDC.Core.LinkShare.DataAccess;

namespace GA.BDC.Core.LinkShare
{
	/// <summary>
	/// Summary description for LsTrans.
	/// </summary>
	public class LsTransfer
	{
		public LsTransfer()
		{

		}

		#region Methods
		// Generate LsTrans report
		public LsTransEntryCollection GetReport()
		{
			LinkShareDatabase lsDb = new LinkShareDatabase();
			return lsDb.GetReport();
		}

		#endregion
	}
}
