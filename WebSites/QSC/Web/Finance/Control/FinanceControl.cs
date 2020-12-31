using System;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Finance.Control
{
	/// <summary>
	/// Summary description for FinanceControl.
	/// </summary>
	public class FinanceControl : QSPUserControl
	{
		public new FinancePage Page 
		{
			get 
			{
				return (FinancePage) base.Page;
			}
			set 
			{
				base.Page = value;
			}
		}
	}
}
