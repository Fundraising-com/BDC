namespace QSPFulfillment.Finance.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for CampaignListControl.
	/// </summary>
	public partial class AdjustmentListControl : FinanceControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
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
			this.DataBinding += new System.EventHandler(this.AdjustmentListControl_DataBinding);
			this.dtgMain.PageIndexChanged += new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
		}
		#endregion

		protected void AdjustmentListControl_DataBinding(object sender, EventArgs e)
		{
			if(IsNested) 
			{
				DataGridItem dgi = (DataGridItem) this.BindingContainer;

				if(!(dgi.DataItem is DataSet))
					throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the HierarGrid declaration");

				AdjustmentBatchListDataSet ds = (AdjustmentBatchListDataSet) dgi.DataItem;
				DataSource = ds;
			}

			BindChildGrid();
		}

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
			{
				this.dtgMain.CurrentPageIndex = e.NewPageIndex;

				BindChildGrid();
			}
		}

		public bool IsNested 
		{
			get 
			{
				bool isNested = true;

				if(ViewState["IsNested"] != null) 
				{
					isNested = Convert.ToBoolean(ViewState["IsNested"]);
				}

				return isNested;
			}
			set 
			{
				ViewState["IsNested"] = value;
			}
		}

		public bool ShowAdjustmentID 
		{
			get 
			{
				return this.dtgMain.Columns[0].Visible;
			}
			set 
			{
				this.dtgMain.Columns[0].Visible = value;
			}
		}

		private string SESSIONKEY_DATASOURCE
		{
			get { return this.UniqueID + "_DataSource"; }
		}

		public AdjustmentBatchListDataSet DataSource 
		{
			get 
			{
				return (AdjustmentBatchListDataSet) Session[SESSIONKEY_DATASOURCE];
			}
			set 
			{
				Session[SESSIONKEY_DATASOURCE] = value;
			}
		}

		private void BindChildGrid() 
		{
			dtgMain.DataSource = DataSource;
			dtgMain.DataMember = DataSource.ADJUSTMENT.TableName;
			dtgMain.DataBind();
		}
	}
}
