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

namespace QSPFulfillment.Fulfillment
{
	/// <summary>
	/// Summary description for showYesNoConfirmationPage.
	/// </summary>
	public partial class showYesNoConfirmationPage : FulfillmentPage
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Image imgTitle;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["Message"]!= null)
			{
				this.lblMessage.Text = Request.QueryString["Message"];
			}

			if(Request.QueryString["ParentHidden"] != null && Request.QueryString["ParentHidden"] != String.Empty) 
			{
				this.btnYes.Attributes.Add("onclick", "window.parent.document.getElementById('dwindow').style.display='none'; window.parent.pleasewait(); parent.document.getElementById('" + Request.QueryString["ParentHidden"].ToString() + "').value = 2; parent.document.forms[0].submit();");
				this.btnNo.Attributes.Add("onclick", "window.parent.document.getElementById('dwindow').style.display='none'; window.parent.pleasewait(); parent.document.getElementById('" + Request.QueryString["ParentHidden"].ToString() + "').value = 1; parent.document.forms[0].submit();");
			}
			else 
			{
				this.btnYes.Visible = false;
				this.btnNo.Value = "OK";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
