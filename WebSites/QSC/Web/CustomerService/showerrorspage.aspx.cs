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

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for showerrorspage.
	/// </summary>
	public partial class showerrorspage : CustomerServicePage
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Image imgTitle;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["Message"]!= null)
			{
                string decodedValue;
                decodedValue = Server.UrlDecode(Request.QueryString["Message"]);
                while (decodedValue != Server.UrlDecode(decodedValue))
                {
                    decodedValue = Server.UrlDecode(decodedValue);
                }
                this.Label1.Text = decodedValue;
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

		protected override void AddJavaScript()
		{
			this.btnClose.Attributes.Add("onclick","javascript:self.close();");
		}

	}
}
