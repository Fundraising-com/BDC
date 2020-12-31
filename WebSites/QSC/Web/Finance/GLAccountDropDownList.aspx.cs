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
	public partial class GLAccountDropDownList : QSPFulfillment.CommonWeb.QSPPage
	{
		protected InvoiceListDataAccess aInvoiceDataAccess;

		public GLAccountDropDownList()
		{
			aInvoiceDataAccess = new InvoiceListDataAccess();
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ddl_GLAccount.DataBind();
				ddl_GLAccount.Items.Insert(0, new ListItem("Choose...", "-1"));
			}
			lblGLAccount.Text = "Select GL Description";
		}

		public DataTable GetGLAccounts()
		{
			DataSet ds = null;
			ds = aInvoiceDataAccess.GetGLAccounts();
			return ds.Tables[0];
		}

        public string GetGLAccountNumber(int GLAccountID)
        {
            return aInvoiceDataAccess.GetGLAccountNumber(GLAccountID);
        }

		public void SetGLAccount(Object sender, EventArgs e)
		{
            string GLAccountIDString = ddl_GLAccount.SelectedItem.Value.ToString();
            int GLAccountID = Convert.ToInt32(GLAccountIDString);
            string GLAccountNumber = GetGLAccountNumber(GLAccountID);
            string GLAccountDesc = ddl_GLAccount.SelectedItem.Text.ToString();

			string strjscript = "<script language='javascript'>";
			//Number
			strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountNumber"] + ".value = '";
			strjscript += GLAccountNumber + "';";
            strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountNumberValue"] + ".value = '";
            strjscript += GLAccountNumber + "';";
			//Description
            strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountDesc"] + ".value = '";
            strjscript += GLAccountDesc + "';";
            strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountDescValue"] + ".value = '";
            strjscript += GLAccountDesc + "';";
            //AccountID
            strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountID"] + ".value = '";
            strjscript += GLAccountIDString + "';";
            strjscript += "window.opener.document.forms(0)." + HttpContext.Current.Request.QueryString["CtrlGLAccountIDValue"] + ".value = '";
            strjscript += GLAccountIDString + "';";

			strjscript += "window.close()</script>";
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

		}
		#endregion
	}
}
