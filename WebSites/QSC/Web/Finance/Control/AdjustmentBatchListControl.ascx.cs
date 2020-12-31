namespace QSPFulfillment.Finance.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using Business.Objects;
	using Common;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for AccountListControl.
	/// </summary>
	public class AdjustmentBatchListControl : FinanceControl
	{
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label5;
		protected QSP.WebControl.TextBoxInteger tbxAdjustmentBatchID;
		protected System.Web.UI.WebControls.Label Label2;
		protected QSPFulfillment.Finance.Control.AdjustmentTypeDropDownList ddlAdjustmentType;
		protected System.Web.UI.WebControls.Label Label7;
		protected QSP.WebControl.DropDownListInteger ddlStatus;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected DBauer.Web.UI.WebControls.HierarGrid dtgMain;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateTo;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPage;

		public event SelectAdjustmentBatchEventHandler AdjustmentBatchDeleted;
		public event SelectAdjustmentBatchEventHandler AdjustmentBatchRestored;

		private string sSortExpression = "";

		private void AdjustmentBatchListControl_Init(object sender, EventArgs e)
		{
			try 
			{
				LoadStateLessData();
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.dtgMain.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.dtgMain_TemplateSelection);
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);
			this.dtgMain.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dtgMain_SortCommand);
			this.ctrlControlerConfirmationPage.Confirmed += new QSPFulfillment.CustomerService.ConfirmEventHandler(ctrlControlerConfirmationPage_Confirmed);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.AdjustmentBatchListControl_Init);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.dtgMain.CurrentPageIndex = 0;

				DataBindResults();
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			switch(e.Row.Table.TableName.ToUpper())
			{
				case "ADJUSTMENT":
					e.TemplateFilename = "Control/AdjustmentListControl" + ".ascx";
					
					break;
			}
		}

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			try 
			{
				if(e.NewPageIndex >= 0 && e.NewPageIndex < this.dtgMain.PageCount) 
				{
					this.dtgMain.CurrentPageIndex = e.NewPageIndex;
					DataBindResults();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_SortCommand(object source, DataGridSortCommandEventArgs e)
		{
			try 
			{
				sSortExpression = e.SortExpression;
				DataBindResults();
			} 
			catch(MessageException ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "DeleteBatch") 
				{
					AdjustmentBatchIDNextToDelete = Convert.ToInt32(e.CommandArgument);
					this.ctrlControlerConfirmationPage.ShowConfirmationWindow();
				} 
				else if(e.CommandName == "RestoreBatch") 
				{
					RestoreAdjustmentBatch(Convert.ToInt32(e.CommandArgument));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerConfirmationPage_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				if(AdjustmentBatchIDNextToDelete != 0) 
				{
					DeleteAdjustmentBatch(AdjustmentBatchIDNextToDelete);
					AdjustmentBatchIDNextToDelete = 0;
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private int AdjustmentBatchIDNextToDelete 
		{
			get 
			{
				int adjustmentBatchIDNextToDelete = 0;

				if(ViewState["AdjustmentBatchIDNextToDelete"] != null) 
				{
					adjustmentBatchIDNextToDelete = Convert.ToInt32(ViewState["AdjustmentBatchIDNextToDelete"]);
				}

				return adjustmentBatchIDNextToDelete;
			}
			set 
			{
				ViewState["AdjustmentBatchIDNextToDelete"] = value;
			}
		}

		#region Fields

		private int AdjustmentBatchIDSearch
		{
			get 
			{
				return this.tbxAdjustmentBatchID.Value;
			}
			set 
			{
				this.tbxAdjustmentBatchID.Value = value;
			}
		}

		private int AdjustmentTypeSearch
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

		private int AdjustmentBatchStatusSearch 
		{
			get 
			{
				return this.ddlStatus.Value;
			}
			set 
			{
				this.ddlStatus.Value = value;
			}
		}

		private DateTime DateFromSearch
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

		private DateTime DateToSearch
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

		public override void DataBind()
		{
			LoadDataSearch();

			DataBindResults();
		}

		private void LoadDataSearch() 
		{
			LoadDataDDL();
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLStatus();
		}

		private void LoadDataDDLStatus() 
		{
			CodeDetail codeDetail = new CodeDetail(CodeHeaderInstance.AdjustmentBatchStatus);

			this.ddlStatus.DataSource = codeDetail.dataSet;
			this.ddlStatus.DataMember = codeDetail.dataSet.CodeDetail.TableName;
			this.ddlStatus.DataTextField = codeDetail.dataSet.CodeDetail.DescriptionColumn.ColumnName;
			this.ddlStatus.DataValueField = codeDetail.dataSet.CodeDetail.InstanceColumn.ColumnName;
			this.ddlStatus.DataBind();
		}

		private void LoadStateLessData() 
		{
			this.ddlAdjustmentType.DataBind();
		}

		public void DataBindResults() 
		{
			this.dtgMain.RowExpanded.CollapseAll();

			LoadDataResults();
		}

		private void LoadDataResults() 
		{
			try 
			{
				AdjustmentBatchList adjustmentBatchList = new AdjustmentBatchList();

				adjustmentBatchList.Search(AdjustmentBatchIDSearch, AdjustmentTypeSearch, AdjustmentBatchStatusSearch, DateFromSearch, DateToSearch);

				adjustmentBatchList.dataSet.AdjustmentBatch.DefaultView.Sort = sSortExpression;

				this.dtgMain.DataSource = adjustmentBatchList.dataSet.AdjustmentBatch.DefaultView;
				this.dtgMain.DataBind();
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void DeleteAdjustmentBatch(int adjustmentBatchID) 
		{
			AdjustmentBatch adjustmentBatch = new AdjustmentBatch();
			adjustmentBatch.UpdateStatus(adjustmentBatchID, AdjustmentBatchStatus.Canceled, Convert.ToInt32(this.Page.UserID));

			DataBindResults();

			if(AdjustmentBatchDeleted != null) 
			{
				AdjustmentBatchDeleted(this, adjustmentBatchID);
			}
		}

		private void RestoreAdjustmentBatch(int adjustmentBatchID) 
		{
			AdjustmentBatch adjustmentBatch = new AdjustmentBatch();
			adjustmentBatch.UpdateStatus(adjustmentBatchID, AdjustmentBatchStatus.Approved, Convert.ToInt32(this.Page.UserID));

			DataBindResults();

			if(AdjustmentBatchRestored != null) 
			{
				AdjustmentBatchRestored(this, adjustmentBatchID);
			}
		}
	}
}