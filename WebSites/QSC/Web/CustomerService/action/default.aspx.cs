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
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.CustomerService.action
{
	/// <summary>
	/// Summary description for action.
	/// </summary>
	public partial class _default : CustomerServiceActionPage
	{
		

		
		protected System.Web.UI.WebControls.Label Label1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavascript();
			if(!IsPostBack)
				this.ValidationSummary1.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			this.LoadsPageSwitchState = true;
			InitializeComponent();
			
			base.OnInit(e,this.btnConfirm,this.btnCancel,this.lblMessage,this.tbxComment,lblHeader,this.lblComments,this.lblHeaderTitle,lblAction);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
			
		
		#region event 
		protected void btnConfirm_Click(object sender, System.EventArgs e)
		{
			FireEventConfirm(this);
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			FireEventCancel(this);
		}
		#endregion

		private void AddJavascript()
		{
			this.btnCancel.Attributes.Add("onclick","javascript:CloseCancel();");
			this.btnConfirm.Attributes.Add("onclick","javascript:toggleDivConfirmVisibility();");
		}
	}
}
