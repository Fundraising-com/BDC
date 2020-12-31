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
	/// Summary description for CampaignContactListMaintenance.
	/// </summary>
	public partial class CampaignContactListMaintenance : AcctMgtPage, IOnloadJSEvent
	{
		protected QSPFulfillment.AcctMgt.Control.CampaignContactListMaintenanceControl ctrlCampaignContactListMaintenanceControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				DataBind();
			}
		}

		private void CampaignContactListMaintenance_PreRender(object sender, EventArgs e)
		{
			this.onload_script += "; window_onunload();";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlCampaignContactListMaintenanceControl.CampaignContactsSaved += new EventHandler(ctrlCampaignContactListMaintenanceControl_CampaignContactsSaved);
			this.ctrlCampaignContactListMaintenanceControl.CampaignContactsCancelled += new EventHandler(ctrlCampaignContactListMaintenanceControl_CampaignContactsCancelled);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(CampaignContactListMaintenance_PreRender);

		}
		#endregion

		private void ctrlCampaignContactListMaintenanceControl_CampaignContactsSaved(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("ConfirmCloseReload", "<script language=\"javascript\"> window.opener.pleasewait(); window.opener.DataBind(); window.opener.Refresh();</script>");
		}

		private void ctrlCampaignContactListMaintenanceControl_CampaignContactsCancelled(object sender, EventArgs e)
		{
			this.Page.RegisterClientScriptBlock("ConfirmCloseReload", "<script language=\"javascript\"> self.close(); window.parent.focus(); </script>");
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

		private int CampaignID 
		{
			get 
			{
				int iCampaignID = 0;

				try 
				{
					iCampaignID = Convert.ToInt32(Request.QueryString["CampaignID"]);
				} 
				catch { }

				return iCampaignID;
			}
		}

		private int ShipToCampaignContactID 
		{
			get 
			{
				int iShipToCampaignContactID = 0;

				try 
				{
					iShipToCampaignContactID = Convert.ToInt32(Request.QueryString["ShipToCampaignContactID"]);
				}
				catch { }

				return iShipToCampaignContactID;
			}
		}

		private int BillToCampaignContactID 
		{
			get 
			{
				int iBillToCampaignContactID = 0;

				try 
				{
					iBillToCampaignContactID = Convert.ToInt32(Request.QueryString["BillToCampaignContactID"]);
				}
				catch { }

				return iBillToCampaignContactID;
			}
		}

		public override void DataBind()
		{
			this.ctrlCampaignContactListMaintenanceControl.AccountID = this.AccountID;
			this.ctrlCampaignContactListMaintenanceControl.CampaignID = this.CampaignID;
			this.ctrlCampaignContactListMaintenanceControl.ShipToCampaignContactID = this.ShipToCampaignContactID;
			this.ctrlCampaignContactListMaintenanceControl.BillToCampaignContactID = this.BillToCampaignContactID;

			this.ctrlCampaignContactListMaintenanceControl.DataBind();
		}
	}
}
