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
	/// Summary description for removeafterprojectpage.
	/// </summary>
	public class removeafterprojectpage : CustomerServicePage
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.TextBox tbxSearchDescription;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button btnBack;
		protected System.Web.UI.HtmlControls.HtmlAnchor hypSelect;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.Button1.Attributes.Add("onclick","javascript:window.open('../AcctMgt/AddressList.aspx?AddressID=1');");
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(removeafterprojectpage));
			this.CustomerInfo = ((QSPFulfillment.DataAccess.Common.ActionObject.Customer)(resources.GetObject("$this.CustomerInfo")));
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
	}
}
