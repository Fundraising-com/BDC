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
using Common;
using QSPFulfillment.AcctMgt.Control;
using QSP.WebControl;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for AccountList1.
	/// </summary>
	public partial class AccountList : AcctMgtPage, IOnloadJSEvent
	{
		protected QSPFulfillment.AcctMgt.Control.AccountListControl ctrlAccountListControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void AccountList_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				try 
				{
					this.Menu1.Visible = !this.IsNewWindow;
					this.ctrlAccountListControl.SelectionMode = this.IsNewWindow;
					this.ctrlAccountListControl.AccountID = this.AccountIDSearch;
					this.ctrlAccountListControl.ParentControlName = this.ParentControlName;
					this.ctrlAccountListControl.DataBind();
				} 
				catch (Exception ex) 
				{
					ApplicationError.ManageError(ex);
				}
			}

			this.onload_script += "; window_onunload();";
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
			this.PreRender += new System.EventHandler(this.AccountList_PreRender);

		}
		#endregion

		public string onload_script 
		{
			get 
			{
				if(BodyTag.Attributes["onload"] == null)
					BodyTag.Attributes["onload"] = "";

				return BodyTag.Attributes["onload"];
			}
			set 
			{
				BodyTag.Attributes["onload"] = value;
			}
		}

		private int AccountIDSearch 
		{
			get 
			{
				int accountIDSearch = 0;

				try 
				{
					accountIDSearch = Convert.ToInt32(Request.QueryString["AccountIDSearch"]);
				} 
				catch 
				{
				}

				return accountIDSearch;
			}
		}

		private string ParentControlName 
		{
			get 
			{
				if(Request.QueryString["ParentControlName"] == null)
					return "";

				return Request.QueryString["ParentControlName"].ToString();
			}
		}
	}
}
