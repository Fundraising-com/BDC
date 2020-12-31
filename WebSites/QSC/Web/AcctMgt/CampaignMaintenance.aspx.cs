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
using QSP.WebControl;

namespace QSPFulfillment.AcctMgt
{
	/// <summary>
	/// Summary description for CampaignMaintenance.
	/// </summary>
	public partial class CampaignMaintenance : AcctMgtPage, IOnloadJSEvent
	{
		protected QSPFulfillment.AcctMgt.Control.CampaignMaintenanceControl ctrlCampaignMaintenanceControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		protected void CampaignMaintenance_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				this.ctrlCampaignMaintenanceControl.CampaignID = this.CampaignID;
				this.ctrlCampaignMaintenanceControl.AccountID = this.AccountID;
				this.ctrlCampaignMaintenanceControl.DataBind();
			} 
			else if(this.hidDataBind.Value == "1") 
			{
				this.ctrlCampaignMaintenanceControl.DataBind();
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
			this.ctrlCampaignMaintenanceControl.CampaignSaved += new EventHandler(ctrlCampaignMaintenanceControl_CampaignSaved);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new System.EventHandler(this.CampaignMaintenance_PreRender);

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

		private int CampaignID 
		{
			get 
			{
				int iCampaignID = 0;

				try 
				{
					iCampaignID = Convert.ToInt32(Request.QueryString["CampaignID"]);
				} 
				catch 
				{
				}

				return iCampaignID;
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
				catch 
				{
				}

				return iAccountID;
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
		}

		private void ctrlCampaignMaintenanceControl_CampaignSaved(object sender, EventArgs e)
		{

		}
	}
}
