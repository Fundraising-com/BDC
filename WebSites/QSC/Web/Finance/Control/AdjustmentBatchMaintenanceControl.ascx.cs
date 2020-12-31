using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.Objects;
using Common;
using Common.TableDef;

namespace QSPFulfillment.Finance.Control
{
	public delegate void SelectAdjustmentBatchEventHandler(object sender, int adjustmentBatchID);
	/// <summary>
	///		Summary description for AdjustmentBatchMaintenanceControl.
	/// </summary>
	public class AdjustmentBatchMaintenanceControl : FinanceControl
	{
		protected QSPFulfillment.Finance.Control.AdjustmentTypeDropDownList ddlAdjustmentType;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateTo;
		protected System.Web.UI.WebControls.Button btnPreview;
		protected System.Web.UI.WebControls.Button btnGenerate;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected QSPFulfillment.Finance.Control.AdjustmentListControl ctrlAdjustmentListControl;

		public event SelectAdjustmentBatchEventHandler AdjustmentBatchSaved;
		public event System.EventHandler AdjustmentBatchCancelled;

		private void AdjustmentBatchMaintenanceControl_Init(object sender, EventArgs e)
		{
			try 
			{
				LoadStatelessData();
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.AdjustmentBatchMaintenanceControl_Init);

		}
		#endregion

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			try 
			{
				PreviewAdjustmentBatch();
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			try 
			{
				int adjustmentBatchID = GenerateAdjustmentBatch();

				this.ctrlAdjustmentListControl.Visible = false;

				if(AdjustmentBatchSaved != null) 
				{
					AdjustmentBatchSaved(this, adjustmentBatchID);
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.ctrlAdjustmentListControl.Visible = false;

			if(AdjustmentBatchCancelled != null) 
			{
				AdjustmentBatchCancelled(this, EventArgs.Empty);
			}
		}

		#region Fields

		private int AdjustmentType 
		{
			get 
			{
				return this.ddlAdjustmentType.Value;
			}
			set 
			{
				this.ddlAdjustmentType.Value = value;
			}
		}

		private DateTime DateFrom 
		{
			get 
			{
				return this.dteDateFrom.Date;
			}
			set 
			{
				this.dteDateFrom.Date = value;
			}
		}

		private DateTime DateTo 
		{
			get 
			{
				return this.dteDateTo.Date;
			}
			set 
			{
				this.dteDateTo.Date = value;
			}
		}

		#endregion
		
		private void LoadStatelessData() 
		{
			this.ddlAdjustmentType.DataBind();
		}

		private void PreviewAdjustmentBatch() 
		{
			AdjustmentBatchList adjustmentBatchList = new AdjustmentBatchList();
			adjustmentBatchList.Preview(AdjustmentType, DateFrom, DateTo);

			this.ctrlAdjustmentListControl.DataSource = adjustmentBatchList.dataSet;
			this.ctrlAdjustmentListControl.DataBind();
			this.ctrlAdjustmentListControl.Visible = true;
		}

		private int GenerateAdjustmentBatch() 
		{
			AdjustmentBatch adjustmentBatch = new AdjustmentBatch();
			return adjustmentBatch.Generate(AdjustmentType, DateFrom, DateTo, Convert.ToInt32(this.Page.UserID));
		}
	}
}