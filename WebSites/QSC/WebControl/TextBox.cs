using System;
using System.Web.UI.WebControls;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for TextBox.
	/// </summary>
	public class TextBox : System.Web.UI.WebControls.TextBox
	{
		protected override void OnTextChanged(EventArgs e)
		{
			this.Text = this.Text.Replace("'", "''");
			base.OnTextChanged (e);
		}
	}
}
