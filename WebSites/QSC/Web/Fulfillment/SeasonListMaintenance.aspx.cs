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

namespace QSPFulfillment.Fulfillment
{
	/// <summary>
	/// ASPX page containing SeasonListMaintenance control
	/// </summary>
	/// <remarks>
	///		Created on 2006-06-28
	///		Created by Madina Saitakhmetova
	/// </remarks>
	public partial class SeasonListMaintenance : FulfillmentPage
	{
		protected QSP.WebControl.EnhancedSmartNavigationControl ctrlEnhancedSmartNavigationControl;
		protected QSPFulfillment.Fulfillment.Control.SeasonListMaintenanceControl ctrlSeasonListMaintenanceControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				DataBind();
			}			
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

		/// <summary>
		/// Bind user control ctrlSeasonListMaintenanceControl
		/// </summary>
		public override void DataBind()
		{
			this.ctrlSeasonListMaintenanceControl.DataBind();
		}
	}
}
