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
	/// Summary description for switchletter.
	/// </summary>
	public class switchletter : CustomerServicePage
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblDirections;
		protected ControlerSwitchLetter ctrlControlerSwitchLetter;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//CreateJavaScript();
			if(!IsPostBack)
			{
				ctrlControlerSwitchLetter.DataBind();
				this.ValidationSummary1.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void CreateJavaScript()
		{
			//btnGenerateSwitchLetter.Attributes.Add("onclick","javascript:void(0);window.open('generateswitchletter.aspx','','toolbar = no,status=no,width=500,height=300')");
		}
		

	}
}
