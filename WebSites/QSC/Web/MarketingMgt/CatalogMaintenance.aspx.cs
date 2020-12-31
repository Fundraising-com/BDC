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
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.MarketingMgt.Control;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for CatalogMaintenance.
	/// </summary>
	public partial class CatalogMaintenance : MarketingMgtPage
	{

		protected System.Web.UI.WebControls.Label lblWait;
		protected CatalogMaintenanceStepsMenuControl ctrlCatalogMaintenanceStepsMenuControl;
		protected CatalogMaintenanceStepsContainerControl ctrlCatalogMaintenanceStepsContainerControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}
		
 
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
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

		}
		#endregion

	}
}
