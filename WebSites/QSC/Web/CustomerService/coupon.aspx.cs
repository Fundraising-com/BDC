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
	/// Summary description for coupon.
	/// </summary>
	public partial class coupon : CustomerServicePageCoupon
	{
		protected System.Web.UI.WebControls.Image imgHeader;
		protected System.Web.UI.WebControls.Label lblControlStep;
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				this.ValidationSummary1.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;	
		}		  

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.OnInit(e,this.btnBack,this.btnNext,this.lblMessage,this.lblHeader,this.lblErrorMessage);
			
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
