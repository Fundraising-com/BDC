namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;
	

	/// <summary>
	///		Summary description for ResultOrder.
	/// </summary>
	public partial class ResultHeaderCreditCard : ControlerResult
	{
		protected string RELATIONNAME = "CustomerOrderHeader";
		protected DataSet dtsMainM;
		private DataTable dtbCreditCardHeader;
		private DataTable dtbCreditCardDetail; 
		
		protected System.Text.RegularExpressions.Regex cardExp = new System.Text.RegularExpressions.Regex( @"(\d{4})(\d{4})(\d{4})(\d{4})" );
		protected string safeOutputExp = "$1********$4";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!ListToSearch.Columns.Contains("customerorderheaderinstance"))
				ListToSearch.Columns.Add("customerorderheaderinstance",typeof(Int32));
			
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			this.Page.PageChanged = false;
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
			this.dtgMain.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.higMain_TemplateSelection);
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.higMain_PageIndexChanged);
			this.dtgMain.ItemCommand +=new DataGridCommandEventHandler(dtgMain_ItemCommand);
			
		}
		#endregion
		public override void DataBind()
		{
			try
			{
				if(ViewState["MyData"] != null)
				{
						DataSetMain.Clear();
						ListToSearch.Clear();
				}
				LoadData();
				if(DataSetMain.Tables["CreditCardHeader"].Rows.Count !=0 ||DataSetMain.Tables["CreditCardDetail"].Rows.Count !=0  )
				{
					this.dtgMain.Visible = true;
					this.lblMessage.Visible= false;
					Bind();
				}
				else
				{
					lblMessage.Text = "No result";
					this.dtgMain.Visible = false;
					this.lblMessage.Visible= true;
					
				}
				
			}
			catch(ExceptionFulf e)
			{
				this.Page.SetPageError(e);
			}	
		}
		/// <summary>
		/// set relation of the dataset
		/// </summary>
		/// <param name="dts"></param>
		private  void SetRelation(DataSet dts)
		{
			try
			{
				
				DataRelation rel;
				
				if(dts.Relations.Count !=0)
					dts.Relations.RemoveAt(0);

				
				rel = new DataRelation("RelTier",
							   dts.Tables["CreditCardHeader"].Columns["CustomerOrderHeaderInstance"],
							   dts.Tables["CreditCardDetail"].Columns["CustomerOrderHeaderInstance"],false);

					
			
				
				dts.Relations.Add(rel);

				
			}
			catch(Exception e)
			{
				string ss = e.Message;
			}
		
		}

		#region load and create
		/// <summary>
		/// create the dataset with 2 tables
		/// </summary>
		private void CreateDataSet()
		{
			
			dtsMainM = new DataSet();
			dtbCreditCardHeader = new DataTable("CreditCardHeader");
			dtsMainM.Tables.Add(dtbCreditCardHeader);
					
			dtbCreditCardDetail = new DataTable("CreditCardDetail");
			dtsMainM.Tables.Add(dtbCreditCardDetail);
			
			
					
		}
		/// <summary>
		/// Load all data for the page
		/// </summary>
		private void LoadData()
		{
		
			LoadTableCreditCardHeader();
			if(DataSetMain.Tables["CreditCardHeader"].Rows.Count!=0)
			{
				CreateListOrderIDMultiTable();
				LoadTableCreditCardDetail();
				
			}
		
		}
		/// <summary>
		/// Load date from Remit Batch
		/// </summary>
		private void LoadTableCreditCardHeader()
		{
			this.Page.BusSearch.SelectSearchCreditCard(DataSetMain.Tables["CreditCardHeader"],List);
						
		}
		/// <summary>
		/// Load data from Order Detail Remit history
		/// </summary>
		private void LoadTableCreditCardDetail()
		{
			this.Page.BusSearch.SelectSearchCreditCardDetails(DataSetMain.Tables["CreditCardDetail"],ListToSearch);
			
		}
		
	
		#endregion
		/// <summary>
		/// 
		/// </summary>
		protected void Bind()
		{
					
			dtgMain.DataSource = DataSetMain;
			
			SetRelation(DataSetMain);
							
			dtgMain.DataMember = "CreditCardHeader";
			if(this.Page.NewSearch) 
			{
				dtgMain.CurrentPageIndex = 0;
				dtgMain.SelectedIndex = -1;
			}

			dtgMain.DataBind();
						
		}
		#region events
		private void higMain_PageIndexChanged(object source,System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dtgMain.CurrentPageIndex = e.NewPageIndex;
			dtgMain.RowExpanded.CollapseAll();
			this.Page.PageChanged = true;
			this.Page.NewSearch = false;
			dtgMain.SelectedIndex = -1;
			ListToSearch.Clear();
			dtgMain.DataBind();
		}
		protected void UpdateView(object sender, System.EventArgs e)
		{			
			Bind();
			
		}

		private void higMain_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			switch(e.Row.Table.TableName)
			{
				case "CreditCardDetail":
					e.TemplateFilename = "ResultProductCreditCard" + ".ascx";
					
					break;

				
				
				
			}
		}
		#endregion
		#region functions
		private int GetPosition(int CurrentPageIndex,int PageSize)
		{
			return (CurrentPageIndex * PageSize);
		}
		private int GetPostionEnd(int Start,int rowCount,int PageSize)
		{
			if(Start + PageSize > rowCount)
			{
				if(Start+PageSize > rowCount)
					return rowCount-1;

				return Start+ PageSize;
			}

			else return Start + PageSize -1;
		}

		private void CreateListOrderIDMultiTable()
		{
			int Start = GetPosition(dtgMain.CurrentPageIndex,dtgMain.PageSize);
			int End = GetPostionEnd(Start,DataSetMain.Tables["CreditCardHeader"].Rows.Count,dtgMain.PageSize);
			
			if(DataSetMain.Tables["CreditCardHeader"].Rows.Count != 0)
			{
				int index = 0;
				int OrderID =0;
				for(int i = Start; i <= End ; i++)
				{
					DataRow row = DataSetMain.Tables["CreditCardHeader"].Rows[i];
					OrderID = Convert.ToInt32(row["customerorderheaderinstance"]);
					CreateNewRow(ListToSearch,OrderID);
					
					index ++;
				}

			}

			
		}
		private void CreateNewRow(DataTable Table,int OrderID)
		{
			DataRow row = Table.NewRow();
			row["customerorderheaderinstance"] = OrderID;
			Table.Rows.Add(row);
		}


		public DataSet DataSetMain
		{
			get
			{
				if(ViewState["MyData"] == null)
				{
						CreateDataSet();
						ViewState["MyData"] = dtsMainM;
				}

				return (DataSet)ViewState["MyData"];
				
			}
			set
			{
					this.ViewState["MyData"] = value;
			}
		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == DataGrid.SelectCommandName)
			{
				CurrentOrderInfo coi = new CurrentOrderInfo();
				coi.OrderID = GetOrderID(e.Item);
				coi.TransID = 0;
				coi.CustomerOrderHeaderInstance = GetCustomerOrderHeaderInstance(e.Item);
				
				this.Page.FireEventSelect(new SelectResultClickedArgs(coi,false));	
			}
		}
		private int GetOrderID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblOrderID")).Text);
		}
		
		private int GetCustomerOrderHeaderInstance(DataGridItem e)
		{
			
			return Convert.ToInt32(((Label)e.FindControl("lblCustomerOrderHeaderInstance")).Text);
		}
		
		
		
		
	}
}

