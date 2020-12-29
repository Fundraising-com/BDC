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
	[ToolboxData("<{0}:Content runat=server></{0}:Content>")]
	public class Content : System.Web.UI.WebControls.Panel
	{
		#region Fields
		private string contentPlaceHolderID = "";
		#endregion

		#region Constructors
		public Content() 
		{
			base.BackColor = Color.WhiteSmoke;
			base.Width = new Unit("100%");
		}
		#endregion

		#region Methods
		public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) {}
		public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) {}
		#endregion

		#region Properties
		[Category("Content"), Description("The matching ContentPlaceHolder ID to place this content.")] 
		public string ContentPlaceHolderID 
		{
			get { return contentPlaceHolderID; }
			set { contentPlaceHolderID = value; }
		}
		#endregion
	}
}
