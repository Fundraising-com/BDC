using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EfundraisingCRM.Sales
{
	public partial class DatePicker : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.CacheControl = "no-cache";
			Response.AddHeader("Pragma", "no-cache");
			Response.Expires = -1;

			if (!IsPostBack)
			{
				DateTime dateValue = DateTime.TryParse(Request.QueryString["dateValue"], out dateValue) ? dateValue : DateTime.Now.Date;
				cDatePicker.VisibleDate = dateValue;
				cDatePicker.SelectedDate = dateValue;
			}
		}

		protected void cDatePicker_SelectionChanged(object sender, EventArgs e)
		{
			ClientScript.RegisterClientScriptBlock(this.GetType(), "returnDate", string.Format("window.returnValue = \"{0}\";window.close();", cDatePicker.SelectedDate.ToString("d/MM/yyyy")), true);
		}
	}
}
