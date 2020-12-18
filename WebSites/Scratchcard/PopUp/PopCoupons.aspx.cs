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

namespace GA.BDC.WEB.ScratchcardWeb.PopUp
{
	/// <summary>
	/// Summary description for PopCoupons.
	/// </summary>
	public class PopCoupons : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image imgCoupon;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string coupon = Request.QueryString["coupon"];
			
			imgCoupon.ImageUrl = "../Resources/images/_ScratchcardWeb_/_classic_/en-US/coupons/c_" + coupon + ".gif";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
