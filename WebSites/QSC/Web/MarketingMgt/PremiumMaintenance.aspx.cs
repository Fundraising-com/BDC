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
using QSPFulfillment.MarketingMgt.Control;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class PremiumMaintenance : MarketingMgtPage
	{
		protected PremiumSearchControl ctrlPremiumSearchControl;
		protected PremiumMaintenanceControl ctrlPremiumMaintenanceControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		protected void PremiumMaintenance_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					this.ctrlPremiumSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlPremiumSearchControl.Visible = true;
					this.ctrlPremiumMaintenanceControl.Visible = false;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					this.ctrlPremiumMaintenanceControl.DataBind();
					this.lblInstructions.Visible = false;
					this.ctrlPremiumSearchControl.Visible = false;
					this.ctrlPremiumMaintenanceControl.Visible = true;
					this.btnCreateNew.Visible = false;
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

 		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlPremiumSearchControl.SelectPremiumClick += new SelectPremiumEventHandler(ctrlPremiumSearchControl_SelectPremiumClick);
			this.ctrlPremiumMaintenanceControl.PremiumSaved += new SelectPremiumEventHandler(ctrlPremiumMaintenanceControl_PremiumSaved);
			this.ctrlPremiumMaintenanceControl.PremiumCancelled += new EventHandler(ctrlPremiumMaintenanceControl_PremiumCancelled);
			InitializeComponent();
			//this.hidDataBind.ServerChange +=new EventHandler(hidDataBind_ServerChange);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new System.EventHandler(this.PremiumMaintenance_PreRender);

		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlPremiumMaintenanceControl.PremiumInfo = null;
				this.ctrlPremiumMaintenanceControl.DataBind();

				this.EditMode = true;
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlPremiumSearchControl_SelectPremiumClick(object sender, SelectPremiumClickedArgs e)
		{
			this.ctrlPremiumMaintenanceControl.PremiumInfo = e.PremiumInfo;
			this.ctrlPremiumMaintenanceControl.DataBind();

			this.EditMode = true;
		}

		private void ctrlPremiumMaintenanceControl_PremiumSaved(object sender, SelectPremiumClickedArgs e)
		{
			this.EditMode = false;
		}

		private void ctrlPremiumMaintenanceControl_PremiumCancelled(object sender, EventArgs e)
		{
			this.EditMode = false;
		}

		private bool EditMode 
		{
			get 
			{
				if(this.ViewState["EditMode"] == null)
					this.ViewState["EditMode"] = false;

				return Convert.ToBoolean(this.ViewState["EditMode"]);
			}
			set 
			{
				this.ViewState["EditMode"] = value;
			}
		}
	}
}
