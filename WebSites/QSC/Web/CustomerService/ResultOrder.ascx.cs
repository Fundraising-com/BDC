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
	public partial class ResultOrder : ControlerResult
	{
		protected string RELATIONNAME = "OrderID";
		protected DataSet dtsMainM;
		private DataTable dtbOrder;
		private DataTable dtbSub; 
		private DataTable dtbProduct;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!ListToSearch.Columns.Contains(BatchTable.FLD_ORDERID))
				ListToSearch.Columns.Add(BatchTable.FLD_ORDERID,typeof(Int32));
			
		}

		private void ResultOrder_PreRender(object sender, EventArgs e)
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
			this.PreRender += new EventHandler(ResultOrder_PreRender);
			this.dtgMain.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.higMain_TemplateSelection);
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.higMain_PageIndexChanged);
			
			
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
				if(DataSetMain.Tables["Order"].Rows.Count !=0)
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

				if(ItemType == (int)ProductType.Magazine)
				{
					rel = new DataRelation("RelSubTier",
							   dts.Tables["Order"].Columns[BatchTable.FLD_ORDERID],
							   dts.Tables["Subscription"].Columns[BatchTable.FLD_ORDERID],false);

					
				}
				else
				{
					rel = new DataRelation("RelProduct",
						dts.Tables["Order"].Columns[BatchTable.FLD_ORDERID],
						dts.Tables["Product"].Columns[BatchTable.FLD_ORDERID],false);
				}
				
				dts.Relations.Add(rel);

				/*else
				{
					rel = new DataRelation("RelGift",
						dts.Tables["Order"].Columns[BatchTable.FLD_ORDERID],
						dts.Tables["Gift"].Columns[BatchTable.FLD_ORDERID],false);

				dts.Relations.Add(rel);
				/*/
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
			dtbOrder = new DataTable("Order");
			dtsMainM.Tables.Add(dtbOrder);
					
			dtbSub = new DataTable("Subscription");
			dtsMainM.Tables.Add(dtbSub);
			
			
			/*dtbGift = new DataTable("Gift");
			dtsMainM.Tables.Add(dtbGift);*/
			
			dtbProduct = new DataTable("Product");
			dtsMainM.Tables.Add(dtbProduct);
					
		}
		/// <summary>
		/// Load all data for the page
		/// </summary>
		private void LoadData()
		{
		
			LoadTableOrder();
			if(DataSetMain.Tables["Order"].Rows.Count!=0)
			{
				CreateListOrderIDMultiTable();

				if(ItemType == (int)ProductType.Magazine)
				{
					LoadTableSubscription();
				}
				/*else if(ItemType == ProductType.Gift)
				{
					LoadGift();
				}*/
				else
				{
					LoadProduct();
				}
			}
		
		}
		/// <summary>
		/// Load date from Remit Batch
		/// </summary>
		private void LoadTableOrder()
		{
			this.Page.BusSearch.SelectSearchOrder(DataSetMain.Tables["Order"],List);
						
		}
		/// <summary>
		/// Load data from Order Detail Remit history
		/// </summary>
		private void LoadTableSubscription()
		{
			this.Page.BusSearch.SelectSearchSubscription(DataSetMain.Tables["Subscription"],ListToSearch,this.Filter);
			
		}
		/*private void LoadGift()
		{
			sbSearch.SelectSearchProduct(DataSetMain.Tables["Gift"],dtbSubTier,ItemType);
		}*/
		private void LoadProduct()
		{
			this.Page.BusSearch.SelectSearchProduct(DataSetMain.Tables["Product"],ListToSearch,ItemType);
		}

		#endregion
		/// <summary>
		/// 
		/// </summary>
		protected void Bind()
		{
					
			dtgMain.DataSource = DataSetMain;
			
			SetRelation(DataSetMain);
							
			dtgMain.DataMember = "Order";
			if(this.Page.NewSearch)
			{
				dtgMain.CurrentPageIndex = 0;
				dtgMain.RowExpanded.CollapseAll();
			}

			dtgMain.DataBind();
						
		}
		#region events
		private void higMain_PageIndexChanged(object source,System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dtgMain.CurrentPageIndex = e.NewPageIndex;
			dtgMain.RowExpanded.CollapseAll();
			this.Page.NewSearch = false;
			this.Page.PageChanged = true;
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
				case "Subscription":
					e.TemplateFilename = "ResultSubscription" + ".ascx";
					
					break;

				/*case "Gift":
					e.TemplateFilename = "ResultGift" + ".ascx";
					break;*/
				
				case "Product":
					e.TemplateFilename = "ResultProduct" + ".ascx";
					
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
				return rowCount-1;

			else return Start + PageSize -1;
		}

		private void CreateListOrderIDMultiTable()
		{
			int Start = GetPosition(dtgMain.CurrentPageIndex,dtgMain.PageSize);
			int End = GetPostionEnd(Start,DataSetMain.Tables["Order"].Rows.Count,dtgMain.PageSize);
			
			if(DataSetMain.Tables["Order"].Rows.Count != 0)
			{
				//sList = new string[End-Start+1];
				int index = 0;
				int OrderID =0;
				for(int i = Start; i <= End ; i++)
				{
					DataRow row = DataSetMain.Tables["Order"].Rows[i];
					OrderID =Convert.ToInt32(row[BatchTable.FLD_ORDERID]);
					//CreateNewRow(dtbSubGiftTier,OrderID);
					CreateNewRow(ListToSearch,OrderID);
					
					index ++;
				}

			}

			
		}
		private void CreateNewRow(DataTable Table,int OrderID)
		{
			DataRow row = Table.NewRow();
			row[BatchTable.FLD_ORDERID] = OrderID;
			Table.Rows.Add(row);
		}


		public DataSet DataSetMain
		{
			get
			{
				if(dtsMainM == null)
				{
						CreateDataSet();
						
				}

				return dtsMainM;
				
			}
			
		}
		#endregion

	}
}

