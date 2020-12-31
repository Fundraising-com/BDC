using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.Design;
using System.Web;
using System.Web.UI;


namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for AlphaSearchDesigner.
	/// </summary>
	internal class AlphaSearchDesigner:System.Web.UI.Design.ControlDesigner
	{
		
		public override string GetDesignTimeHtml() 
		{
			// Component is the control instance, defined in the base
			// designer
			AlphaSearch ctl = (AlphaSearch ) Component;

			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);

			HyperLink placeholderLink = new HyperLink();
				
			ctl.RenderBeginTag(tw);
			ctl.RenderControl(tw);
			ctl.RenderEndTag(tw);
	

			return (sw.ToString());
				
		
		}


	}
}
