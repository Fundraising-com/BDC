using System;
using System.Web.UI;
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Summary description for QSPControl.
	/// </summary>
    public class QSPUserControl : System.Web.UI.UserControl
	{
		public new QSPPage Page
		{
			get
			{
				return (QSPPage) base.Page;
			}
			set
			{
				base.Page = value;
			}
		}
	}
}
