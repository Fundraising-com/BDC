//
// 2004-11-30 - Stephen Lim - New class.
//

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GA.BDC.Core.Web.UI.MasterPages
{
	[ToolboxData("<{0}:ContentPlaceHolder runat=server></{0}:ContentPlaceHolder>")]
	public class ContentPlaceHolder : System.Web.UI.WebControls.Panel
	{
		public ContentPlaceHolder() 
		{
			base.BackColor = Color.WhiteSmoke;
			base.Width = new Unit("100%");
		}

		public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) {}
		public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) {}
	}
}
