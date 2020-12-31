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

namespace QSPFulfillment.Finance
{
	/// <summary>
	/// Summary description for AdjustmentBatchMaintenance.
	/// </summary>
	public partial class AdjustmentBatchMaintenance : FinancePage
	{
		private const string BATCH_GENERATED_MESSAGE = "Adjustment Batch ID [AdjustmentBatchID] has been generated successfully.";
		private const string BATCH_DELETED_MESSAGE = "Adjustment Batch ID [AdjustmentBatchID] has been deleted successfully.";
		private const string BATCH_RESTORED_MESSAGE = "Adjustment Batch ID [AdjustmentBatchID] has been restored successfully.";

		protected QSPFulfillment.Finance.Control.AdjustmentBatchListControl ctrlAdjustmentBatchListControl;
		protected QSPFulfillment.Finance.Control.AdjustmentBatchMaintenanceControl ctrlAdjustmentBatchMaintenanceControl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				this.ctrlAdjustmentBatchListControl.DataBind();
			}
		}

		private void AdjustmentBatchMaintenance_PreRender(object sender, EventArgs e)
		{
			SetLayout();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlAdjustmentBatchListControl.AdjustmentBatchDeleted += new QSPFulfillment.Finance.Control.SelectAdjustmentBatchEventHandler(ctrlAdjustmentBatchListControl_AdjustmentBatchDeleted);
			this.ctrlAdjustmentBatchListControl.AdjustmentBatchRestored += new QSPFulfillment.Finance.Control.SelectAdjustmentBatchEventHandler(ctrlAdjustmentBatchListControl_AdjustmentBatchRestored);
			this.ctrlAdjustmentBatchMaintenanceControl.AdjustmentBatchSaved += new QSPFulfillment.Finance.Control.SelectAdjustmentBatchEventHandler(ctrlAdjustmentBatchMaintenanceControl_AdjustmentBatchSaved);
			this.ctrlAdjustmentBatchMaintenanceControl.AdjustmentBatchCancelled += new EventHandler(ctrlAdjustmentBatchMaintenanceControl_AdjustmentBatchCancelled);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(AdjustmentBatchMaintenance_PreRender);
		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			EditMode = true;
		}

		private void ctrlAdjustmentBatchListControl_AdjustmentBatchDeleted(object sender, int adjustmentBatchID)
		{
			this.lblTaskStatus.Text = BATCH_DELETED_MESSAGE.Replace("[AdjustmentBatchID]", adjustmentBatchID.ToString());
			ShowTaskStatus = true;
		}

		private void ctrlAdjustmentBatchListControl_AdjustmentBatchRestored(object sender, int adjustmentBatchID)
		{
			this.lblTaskStatus.Text = BATCH_RESTORED_MESSAGE.Replace("[AdjustmentBatchID]", adjustmentBatchID.ToString());
			ShowTaskStatus = true;
		}

		private void ctrlAdjustmentBatchMaintenanceControl_AdjustmentBatchSaved(object sender, int adjustmentBatchID)
		{
			this.ctrlAdjustmentBatchListControl.DataBindResults();
			this.lblTaskStatus.Text = BATCH_GENERATED_MESSAGE.Replace("[AdjustmentBatchID]", adjustmentBatchID.ToString());
			ShowTaskStatus = true;

			this.EditMode = false;
		}

		private void ctrlAdjustmentBatchMaintenanceControl_AdjustmentBatchCancelled(object sender, EventArgs e)
		{
			this.EditMode = false;
		}

		private bool EditMode 
		{
			get 
			{
				bool editMode = false;

				if(ViewState["EditMode"] != null) 
				{
					editMode = Convert.ToBoolean(ViewState["EditMode"]);
				}

				return editMode;
			}
			set 
			{
				ViewState["EditMode"] = value;
			}
		}

		#region Fields

		private string TaskStatus 
		{
			get 
			{
				return this.lblTaskStatus.Text;
			}
			set 
			{
				this.lblTaskStatus.Text = value;
			}
		}

		#endregion

		private bool ShowTaskStatus 
		{
			get 
			{
				return this.divTaskStatus.Visible;
			}
			set 
			{
				this.divTaskStatus.Visible = value;
			}
		}

		private void SetLayout() 
		{
			this.ctrlAdjustmentBatchListControl.Visible = !EditMode;
			this.btnCreateNew.Visible = !EditMode;
			this.ctrlAdjustmentBatchMaintenanceControl.Visible = EditMode;
		}
	}
}
