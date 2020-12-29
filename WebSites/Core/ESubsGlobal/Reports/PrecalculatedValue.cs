using System;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
	/// <summary>
	/// Summary description for PrecalculatedValue.
	/// </summary>
	public class PrecalculatedValue
	{
		private decimal grandTotalRaised = 200000;
 
		public PrecalculatedValue()	{

		}

		public static PrecalculatedValue CreatePrecalculatedValue() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrecalculatedValueReport();
		}

		public decimal GrandTotalRaised {
			get { return grandTotalRaised; }
			set { grandTotalRaised = value; }
		}
	}
}
