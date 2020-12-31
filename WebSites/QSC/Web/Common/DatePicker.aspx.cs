using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QSPFulfillment.CommonWeb
{
	///<summary>ASP.NET calendar pop up page</summary>
	///<remarks>
	/// this page purposefully inherits from 
	/// the default system page instead of the project
	/// base page
	///</remarks>
	public partial class DatePicker : System.Web.UI.Page
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
		}
		#endregion auto-generated code

		#region Item Declarations
		#endregion Item Declarations

		protected void Page_Load(object s, System.EventArgs e)
		{
			CallingControl.Value = Request.QueryString["caller"].ToString();
			CallingControlButton.Value = Request.QueryString["callerButton"].ToString();
		}

		protected void Change_Date(object s, EventArgs e)
		{
            string JavaScriptStr = "<script>window.opener.document.getElementById('" + CallingControl.Value + "').value = '";
			JavaScriptStr += CalDate.SelectedDate.ToString("MM/dd/yyyy");

            JavaScriptStr += "';window.opener.document.getElementById('" + CallingControlButton.Value + "').click()";

			JavaScriptStr += ";self.close()</script>";
			RegisterClientScriptBlock("anything", JavaScriptStr);

		}
	}
}







