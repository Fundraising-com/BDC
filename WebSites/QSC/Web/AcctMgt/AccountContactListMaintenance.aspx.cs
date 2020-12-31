using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSP.WebControl;

namespace QSPFulfillment.AcctMgt
{
	///<summary>Contact Maintenance WebPage.</summary>
	public partial class AccountContactListMaintenance : AcctMgtPage, IOnloadJSEvent
	{
		protected QSPFulfillment.AcctMgt.Control.AccountContactListMaintenanceControl ctrlAccountContactListMaintenanceControl;

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				DataBind();
			}
		}

		private void AccountContactListMaintenance_PreRender(object sender, EventArgs e)
		{
			this.onload_script += "; window_onunload();";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlAccountContactListMaintenanceControl.AccountContactsSaved += new EventHandler(ctrlAccountContactListMaintenanceControl_AccountContactsSaved);
			this.ctrlAccountContactListMaintenanceControl.AccountContactsCancelled += new EventHandler(ctrlAccountContactListMaintenanceControl_AccountContactsCancelled);
			this.ctrlAccountContactListMaintenanceControl.AccountContactsDeleted += new EventHandler(ctrlAccountContactListMaintenanceControl_AccountContactsDeleted);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(AccountContactListMaintenance_PreRender);
		}
		#endregion

		private void ctrlAccountContactListMaintenanceControl_AccountContactsSaved(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("ConfirmCloseReload", "<script language=\"javascript\"> window.opener.pleasewait(); window.opener.DataBind(); window.opener.Refresh(); </script>");
		}

		private void ctrlAccountContactListMaintenanceControl_AccountContactsCancelled(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("ConfirmCloseReload", "<script language=\"javascript\"> self.close(); window.parent.focus(); </script>");
		}

		private void ctrlAccountContactListMaintenanceControl_AccountContactsDeleted(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("ConfirmCloseReload", "<script language=\"javascript\"> window.opener.pleasewait(); window.opener.DataBind(); window.opener.Refresh(); </script>");
		}

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

		private int AccountID 
		{
			get 
			{
				int iAccountID = 0;

				try 
				{
					iAccountID = Convert.ToInt32(Request.QueryString["AccountID"]);
				}
				catch { }

				return iAccountID;
			}
		}

		public override void DataBind()
		{
			this.ctrlAccountContactListMaintenanceControl.AccountID = this.AccountID;

			this.ctrlAccountContactListMaintenanceControl.DataBind();
		}
	}
}
