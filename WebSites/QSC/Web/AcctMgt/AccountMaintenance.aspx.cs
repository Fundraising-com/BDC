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
using QSPFulfillment.AcctMgt.Control;
using QSP.WebControl;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for AccountMaintenance.
	/// </summary>
	public partial class AccountMaintenance : AcctMgtPage, IOnloadJSEvent
	{
		public QSPFulfillment.AcctMgt.Control.AccountMaintenanceControl ctrlAccountMaintenanceControl;

		protected void Page_Load(object sender, EventArgs e)
		{
			AddJavaScript();
		}

		protected void AccountMaintenance_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				this.ctrlAccountMaintenanceControl.AccountID = this.AccountID;
				this.ctrlAccountMaintenanceControl.DataBind();
			} 
			else if(this.hidDataBind.Value == "1") 
			{
				this.ctrlAccountMaintenanceControl.DataBind();
				this.hidDataBind.Value = "0";
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
			this.PreRender += new System.EventHandler(this.AccountMaintenance_PreRender);

		}
		#endregion

		private void ctrlAccountMaintenanceControl_AccountSaved(object sender, EventArgs e)
		{

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
				int accountID = 0;

				try 
				{
					accountID = Convert.ToInt32(Request.QueryString["AccountID"]);
				} 
				catch 
				{
				}

				return accountID;
			}
		}

		protected override void AddJavaScript()
		{
			string script;

			base.AddJavaScript ();

			script  = "<script language=\"javascript\">\n";
			script += "  function DataBind() {\n";
			script += "    document.getElementById(\"" + this.hidDataBind.ClientID + "\").value = 1;\n";
			script += "  }\n";
			script += "</script>\n";

			this.RegisterClientScriptBlock("DataBind", script);

			AddJavaScriptDisableSubmitButton();
			AddJavaScriptEnableSubmitButton();
		}

		private void AddJavaScriptDisableSubmitButton() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function DisableSubmitButton() {\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSubmitBottom.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSaveNewBottom.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnNewCampaignBottom.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSubmitTop.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSaveNewTop.ClientID + "\").disabled = true;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnNewCampaignTop.ClientID + "\").disabled = true;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("DisableSubmitButton", script);
		}

		private void AddJavaScriptEnableSubmitButton() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function EnableSubmitButton() {\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSubmitBottom.ClientID + "\").disabled = false;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSaveNewBottom.ClientID + "\").disabled = false;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnNewCampaignBottom.ClientID + "\").disabled = false;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSubmitTop.ClientID + "\").disabled = false;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnSaveNewTop.ClientID + "\").disabled = false;\n";
			script += "    document.getElementById(\"" + this.ctrlAccountMaintenanceControl.btnNewCampaignTop.ClientID + "\").disabled = false;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("EnableSubmitButton", script);
		}
	}
}
