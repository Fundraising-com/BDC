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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using DAL;

namespace QSPFulfillment.Finance
{
	/// <summary>
	/// Summary description for DropDownList.
	/// </summary>
	public class AcctTypeDropDownList : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.DropDownList ddl_AccountType;
		protected InvoiceListDataAccess aInvoiceDataAccess;
		protected Label lblAcctType;
		protected Literal Literal1;

		public AcctTypeDropDownList()
		{
			aInvoiceDataAccess = new InvoiceListDataAccess();
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ddl_AccountType.DataBind();
				ddl_AccountType.Items.Insert(0, new ListItem("Choose...", "-1"));
			}
			lblAcctType.Text = "Select Account Type";
		}

		public DataTable GetAccountType()
		{
			DataSet ds = null;
			//get the filter value from the master Grid's DataKeys collection
			ds = aInvoiceDataAccess.GetInvoiceAccountTypes();
			return ds.Tables[0];
		}

		public void SetAccountType(Object sender, EventArgs e)
		{

			string strjscript = "<script language='javascript'>";
			strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlName"] + ".value = '";
			strjscript += ddl_AccountType.SelectedItem.Text.ToString()+ "';";
			strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlValue"] + ".value = '";
			strjscript += ddl_AccountType.SelectedItem.Value.ToString();
			strjscript += "';window.close()</script>";
			Literal1.Text = strjscript;
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
