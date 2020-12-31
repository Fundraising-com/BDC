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
	public partial class PublisherMaintenance : MarketingMgtPage, IOnloadJSEvent
	{
		protected PublisherSearchControl ctrlPublisherSearchControl;
		protected PublisherMaintenanceControl ctrlPublisherMaintenanceControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void PublisherMaintenance_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					this.ctrlPublisherSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlPublisherSearchControl.Visible = true;
					this.ctrlPublisherMaintenanceControl.Visible = false;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					this.lblInstructions.Visible = false;
					this.ctrlPublisherSearchControl.Visible = false;
					this.ctrlPublisherMaintenanceControl.Visible = true;
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
			this.ctrlPublisherSearchControl.SelectPublisherClick += new SelectPublisherEventHandler(ctrlPublisherSearchControl_SelectPublisherClick);
			this.ctrlPublisherMaintenanceControl.PublisherSaved += new SelectPublisherEventHandler(ctrlPublisherMaintenanceControl_PublisherSaved);
			this.ctrlPublisherMaintenanceControl.PublisherCancelled += new System.EventHandler(ctrlPublisherMaintenanceControl_PublisherCancelled);
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
			this.PreRender += new System.EventHandler(this.PublisherMaintenance_PreRender);

		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlPublisherMaintenanceControl.PublisherInfo = null;
				this.ctrlPublisherMaintenanceControl.DataBind();

				this.EditMode = true;
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlPublisherSearchControl_SelectPublisherClick(object sender, SelectPublisherClickedArgs e)
		{
			this.ctrlPublisherMaintenanceControl.AddressHygiened = false;
			this.ctrlPublisherMaintenanceControl.AddressHygieneStatusLabel.Text = String.Empty;

			this.ctrlPublisherMaintenanceControl.PublisherInfo = e.PublisherInfo;
			this.ctrlPublisherMaintenanceControl.DataBind();

			this.EditMode = true;
		}

		private void ctrlPublisherMaintenanceControl_PublisherSaved(object sender, SelectPublisherClickedArgs e) 
		{
			this.EditMode = false;
		}
		private void ctrlPublisherMaintenanceControl_PublisherCancelled(object sender, System.EventArgs e) 
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
