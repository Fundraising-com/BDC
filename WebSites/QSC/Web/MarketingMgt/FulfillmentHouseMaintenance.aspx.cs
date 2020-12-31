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
using QSPFulfillment.MarketingMgt.Control;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSP.WebControl;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class FulfillmentHouseMaintenance : MarketingMgtPage, IOnloadJSEvent
	{
		protected FulfillmentHouseSearchControl ctrlFulfillmentHouseSearchControl;
		protected FulfillmentHouseMaintenanceControl ctrlFulfillmentHouseMaintenanceControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void FulfillmentHouseMaintenance_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					this.ctrlFulfillmentHouseSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlFulfillmentHouseSearchControl.Visible = true;
					this.ctrlFulfillmentHouseMaintenanceControl.Visible = false;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					this.lblInstructions.Visible = false;
					this.ctrlFulfillmentHouseSearchControl.Visible = false;
					this.ctrlFulfillmentHouseMaintenanceControl.Visible = true;
					this.btnCreateNew.Visible = false;
				}

				this.onload_script += "; window_onunload();";
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
			this.ctrlFulfillmentHouseSearchControl.SelectFulfillmentHouseClick += new SelectFulfillmentHouseEventHandler(ctrlFulfillmentHouseSearchControl_SelectFulfillmentHouseClick);
			this.ctrlFulfillmentHouseMaintenanceControl.FulfillmentHouseSaved += new SelectFulfillmentHouseEventHandler(ctrlFulfillmentHouseMaintenanceControl_FulfillmentHouseSaved);
			this.ctrlFulfillmentHouseMaintenanceControl.FulfillmentHouseCancelled += new System.EventHandler(ctrlFulfillmentHouseMaintenanceControl_FulfillmentHouseCancelled);
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
			this.PreRender += new System.EventHandler(this.FulfillmentHouseMaintenance_PreRender);

		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlFulfillmentHouseMaintenanceControl.FulfillmentHouseInfo = null;
				this.ctrlFulfillmentHouseMaintenanceControl.DataBind();

				this.EditMode = true;
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlFulfillmentHouseSearchControl_SelectFulfillmentHouseClick(object sender, SelectFulfillmentHouseClickedArgs e)
		{
			this.ctrlFulfillmentHouseMaintenanceControl.AddressHygiened = false;
			this.ctrlFulfillmentHouseMaintenanceControl.AddressHygieneStatusLabel.Text = String.Empty;

			this.ctrlFulfillmentHouseMaintenanceControl.FulfillmentHouseInfo = e.FulfillmentHouseInfo;
			this.ctrlFulfillmentHouseMaintenanceControl.DataBind();

			this.EditMode = true;
		}

		private void ctrlFulfillmentHouseMaintenanceControl_FulfillmentHouseSaved(object sender, SelectFulfillmentHouseClickedArgs e) 
		{
			this.ctrlFulfillmentHouseSearchControl.FulfillmentHouseInfo = e.FulfillmentHouseInfo;		
	
			this.EditMode = false;
		}
		private void ctrlFulfillmentHouseMaintenanceControl_FulfillmentHouseCancelled(object sender, System.EventArgs e) 
		{
			this.EditMode = false;
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
