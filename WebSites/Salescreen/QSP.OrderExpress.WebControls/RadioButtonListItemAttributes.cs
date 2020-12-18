using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.IO;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for RadioButtonListItemAttributes.
	/// </summary>
	public class RadioButtonListItemAttributes : RadioButtonList
	{
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			string strAttributes;
			int idx = 0;
			int index = 0;
			int endIndex;
			HtmlTextWriter liHtmlTW;
			StringWriter liSW;
			StringBuilder liSB = new StringBuilder();
			string myHTML;
			StringBuilder SB = new StringBuilder();
			StringWriter SW = new StringWriter(SB);
			HtmlTextWriter htmlTW = new HtmlTextWriter(SW);

			base.Render(htmlTW);
			myHTML = SB.ToString();

			liSW = new StringWriter(liSB);
			liHtmlTW = new HtmlTextWriter(liSW);

			while (idx < Items.Count) 
			{
				if (Items[idx].Attributes["Visible"] == "False") 
				{
					index = myHTML.IndexOf("<tr>", index);
					endIndex = (myHTML.IndexOf("</tr>", index) + "</tr>".Length);
					myHTML = myHTML.Remove(index, (endIndex - index));
				}
				else
				{
					index = (myHTML.IndexOf("<input ", index) + "<input ".Length);
					liSB.Remove(0, liSB.Length);
					Items[idx].Attributes.Render(liHtmlTW);
					strAttributes = (liSB.ToString() + " ");
					myHTML = myHTML.Insert(index, strAttributes);
				}
				idx++;
			}
			output.Write(myHTML);
		}
	}
}
