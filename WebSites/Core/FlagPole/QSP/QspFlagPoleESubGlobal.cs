using System;

namespace GA.BDC.Core.Flagpole.QSP
{
    using GA.BDC.Core.ESubsGlobal.Payment;

	/// <summary>
	/// Summary description for QspFlagPoleESubGlobal.
	/// </summary>	

	internal class QspFlagPoleESubGlobal : IQspFlagPole
	{
		public QspFlagPoleESubGlobal()
		{
		}
		#region IQspFlagPole Members

		public bool Match(int Id)
		{
			// TODO:  Add QspFlagPoleESubGlobal.Match implementation
			return PaymentInfo.Match(Id);
		}

		#endregion
	}
}
