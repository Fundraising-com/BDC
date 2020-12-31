using System;
using System.Data;
using QSPFulfillment.DataAccess.Common;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess;

namespace QSPFulfillment.MarketingMgt.Control
{
	public delegate void StepCompletedEventHandler(object sender, StepCompletedArgs e);
	/// <summary>
	/// Summary description for CustomerServiceControl.
	/// </summary>
	public class CatalogMaintenanceStepControl : MarketingMgtControl
	{
		public event StepCompletedEventHandler StepCompleted;

		public CatalogMaintenanceStepControl()
		{
			
		}
		private void Page_PreRender(object sender, EventArgs e)
		{

		}
        public virtual void Initialize()
        {

        }
        #region Web Form Designer generated code
		protected override void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e);							
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			
			this.Page.PreRender +=new EventHandler(Page_PreRender);
			
		}
		#endregion

		public Step StepControl;

		protected virtual void OnStepCompleted(object sender, StepCompletedArgs args) 
		{
			if(StepCompleted != null)
			{
				StepCompleted(sender, args);
			}
		}
	}
}
