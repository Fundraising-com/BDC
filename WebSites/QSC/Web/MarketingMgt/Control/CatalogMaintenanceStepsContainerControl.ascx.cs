namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;
	using System.Reflection;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for CatalogMaintenanceStepsContainerControl.
	/// </summary>
	public partial class CatalogMaintenanceStepsContainerControl : MarketingMgtControl
	{
		protected CatalogMaintenanceStepControl ctrlCatalogMaintenanceStepControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

        protected void OnPreLoad(object sender, System.EventArgs e)
        {
            LoadControl();
            this.ctrlCatalogMaintenanceStepControl.StepCompleted += new StepCompletedEventHandler(ctrlCatalogMaintenanceStepControl_StepCompleted);
        }
		protected void CatalogMaintenanceStepsContainerControl_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(this.Page.CurrentStep != this.ctrlCatalogMaintenanceStepControl.StepControl) 
				{
					this.lblStep.Controls.Clear();
					LoadControl();
				}
			
				this.lblStepTitle.Text = GetEnumValueDescription(this.Page.CurrentStep);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			if(!IsPostBack) 
			{
				this.Page.CatalogInfo = new Catalog();
				this.Page.CatalogSectionInfo = null;
				
				if(this.Page.CreateNew) 
				{
					this.Page.CurrentStep = Step.CatalogInformations;
				} 
				else 
				{
					this.Page.CurrentStep = Step.SelectCatalog;
				}
			}

			//LoadControl();
			//this.ctrlCatalogMaintenanceStepControl.StepCompleted += new StepCompletedEventHandler(ctrlCatalogMaintenanceStepControl_StepCompleted);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.CatalogMaintenanceStepsContainerControl_PreRender);
            this.Page.PreLoad += new System.EventHandler(this.OnPreLoad);
		}
		#endregion

		private void ctrlCatalogMaintenanceStepControl_StepCompleted(object sender, StepCompletedArgs e)
		{
			if(e.StepCompleted != Step.IncludeProducts) 
			{
				this.Page.CurrentStep += 1;
			} 
			else 
			{
				Response.Redirect("CatalogMaintenance.aspx?CompletedCatalog=" + this.Page.CatalogInfo.CatalogCode);

				this.Page.CatalogInfo = null;
				this.Page.CatalogSectionInfo = null;
			}
		}

		private void LoadControl() 
		{
			ctrlCatalogMaintenanceStepControl = (CatalogMaintenanceStepControl)LoadControl("Step" + this.Page.CurrentStep.ToString() + "Control.ascx");
			ctrlCatalogMaintenanceStepControl.ID = "ctrlCatalogMaintenanceStepControl";
			this.lblStep.Controls.Add(ctrlCatalogMaintenanceStepControl);
            this.ctrlCatalogMaintenanceStepControl.Initialize();
		}

		public static string GetEnumValueDescription(object value)
		{
			Type pobjType = value.GetType();

			FieldInfo pobjFieldInfo = pobjType.GetField(Enum.GetName(pobjType, value));

			DescriptionAttribute pobjAttribute = (DescriptionAttribute) (pobjFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]);

			return pobjAttribute.Description;
		}
	}
}
