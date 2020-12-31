using System;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class StepCompletedArgs : System.EventArgs
	{
		private Step stepCompleted;

		public StepCompletedArgs(Step stepCompleted)
		{
			this.stepCompleted = stepCompleted;
		}
		
		public Step StepCompleted
		{
			get
			{
				return stepCompleted;
			}
		}
	}
}
