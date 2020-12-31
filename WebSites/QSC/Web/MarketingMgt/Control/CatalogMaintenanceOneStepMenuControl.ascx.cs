namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepMenuControl.
	/// </summary>
	public partial class CatalogMaintenanceOneStepMenuControl : MarketingMgtControl
	{


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
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void btnStep_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Page.CurrentStep = this.StepControl;
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public string StepTitle 
		{
			get 
			{
				return btnStep.Text;
			}
			set 
			{
				btnStep.Text = value;
			}
		}

		public Step StepControl;

		public bool ShowSeparator 
		{
			get 
			{
				return listSeparator.Visible;
			}
			set 
			{
				listSeparator.Visible = value;
			}
		}

		public bool Enabled 
		{
			get 
			{
				return this.btnStep.Enabled;
			}
			set 
			{
				this.btnStep.Enabled = value;
			}
		}
	}
}
