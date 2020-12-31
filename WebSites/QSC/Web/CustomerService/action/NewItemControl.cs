using System;

namespace QSPFulfillment.CustomerService.action
{
	/// <summary>
	/// Summary description for NewItemControl.
	/// </summary>
	public class NewItemControl : CustomerServiceActionControl
	{
		protected virtual bool Step1Completed
		{
			get 
			{
				if(ViewState["Step1Completed"] == null)
					return false;

				return Convert.ToBoolean(ViewState["Step1Completed"]);
			}
			set 
			{
				ViewState["Step1Completed"] = value;
			}
		}

		protected virtual bool IsBack
		{
			get 
			{
				if(ViewState["IsBack"] == null)
					return false;

				return Convert.ToBoolean(ViewState["IsBack"]);
			}
			set 
			{
				ViewState["IsBack"] = value;
			}
		}
	}
}
