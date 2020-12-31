using System;

namespace QSPFulfillment.CustomerService.action
{
	/// <summary>
	/// Summary description for NewItemStepControl.
	/// </summary>
	public class NewItemStepControl : CustomerServiceActionControl
	{
		public NewItemControl NewItemParent 
		{
			get 
			{
				return (NewItemControl) this.Parent;
			}	
		}
	}
}
