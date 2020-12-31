using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for EnhancedSmartNavigationControl.
	/// </summary>
	/// 
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:EnhancedSmartNavigationControl runat=server></{0}:EnhancedSmartNavigationControl>")]
	public class EnhancedSmartNavigationControl : System.Web.UI.WebControls.WebControl
	{
		private new IOnloadJSEvent Page 
		{
			get 
			{
				return (IOnloadJSEvent) base.Page;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(Enabled) 
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder(1000);
				string nl = Environment.NewLine;

				sb.Append("<input type=\"hidden\" name=\"scrollXLeft\" id=\"scrollXLeft\" value=\"" + Convert.ToString(Context.Request.Form["scrollXLeft"]) + "\" />" + nl);
				sb.Append("<input type=\"hidden\" name=\"scrollYTop\" id=\"scrollYTop\" value=\"" + Convert.ToString(Context.Request.Form["scrollYTop"]) + "\" />" + nl);
				sb.Append("<script language=\"javascript\">");
				sb.Append(String.Concat(nl, "<!--", nl));
				sb.Append("function SmartScroller_GetCoords() {");
				sb.Append(String.Concat(nl, "var scrollX, scrollY;", nl));
				sb.Append("if (document.all) {");
				sb.Append(String.Concat(nl, "if (!document.documentElement.scrollLeft)", nl));
				sb.Append("scrollX = document.body.scrollLeft;");
				sb.Append(String.Concat(nl, "else", nl));
				sb.Append("scrollX = document.documentElement.scrollLeft;");
				sb.Append(String.Concat(nl, "if (!document.documentElement.scrollTop)", nl));
				sb.Append("scrollY = document.body.scrollTop;");
				sb.Append(String.Concat(nl, "else", nl));
				sb.Append("scrollY = document.documentElement.scrollTop; }");
				sb.Append(String.Concat(nl, "else {", nl));
				sb.Append("scrollX = window.pageXOffset; scrollY = window.pageYOffset; }");
				sb.Append(String.Concat(nl, nl));
				sb.Append("document.getElementById('scrollXLeft').value = scrollX;");
				sb.Append(nl);
				sb.Append("document.getElementById('scrollYTop').value = scrollY;");
				sb.Append(String.Concat(nl, "}", nl));

				sb.Append(String.Concat(nl, "function SmartScroller_Scroll() {", nl));
				sb.Append("var x = document.getElementById('scrollXLeft').value;");
				sb.Append(String.Concat(nl, "var y = document.getElementById('scrollYTop').value;", nl));
				sb.Append("window.scrollTo(x, y); }");

				sb.Append(String.Concat(nl, "window.onscroll = SmartScroller_GetCoords;", nl));
				sb.Append("window.onclick = SmartScroller_GetCoords; window.onkeypress = SmartScroller_GetCoords;");
				sb.Append(String.Concat(nl, "// -->", nl));
				sb.Append("</script>");

				base.Page.RegisterClientScriptBlock("scrollCode", sb.ToString());
				this.Page.onload_script += "; SmartScroller_Scroll();";
			}

			base.OnPreRender (e);
		}
	}
}
