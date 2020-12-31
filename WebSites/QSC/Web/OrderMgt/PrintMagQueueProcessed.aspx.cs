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

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for PrintMagQueueProcessed.
	/// </summary>
	public partial class PrintMagQueueProcessed : QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if (!IsPostBack) 
			{
				
				string sString = (string)Session["QSPF_OrderList"];			
				string[] oOrderId = sString.Split(new char[] {','});			
			
				Repeater1.DataSource = oOrderId;
				Repeater1.DataBind();

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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("PrintMagQueue.aspx", false);
		}
	}
}
