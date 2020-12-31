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
using System.Data.SqlClient;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.TableDef;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for Orders.
	/// </summary>
	public partial class TotalRemitBatch : QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected string RELATIONNAME = "RemitBatchID";
		protected DataSet dtsMainM;
		private const string TABLE_NAME_REMITBATCH_HEADER = "RemitBatchHeader";
		protected System.Web.UI.WebControls.Label Label1;
		protected SearchModule ctrlSearchModule;
		private DateTime dFrom;
		private DateTime dTo;
		private bool SearchClicked = false;
		private RemitBatchBusiness buiRemitBatch = new RemitBatchBusiness(false);
		private CustomerOrderDetailRemitHistoryBusiness buiCusOrdDetRemHis = new CustomerOrderDetailRemitHistoryBusiness(false);
		private RemitBatchTable dtbRemitBatch = new RemitBatchTable();
		private CustomerOrderDetailRemitHistoryTable dtbCODRH = new CustomerOrderDetailRemitHistoryTable();
		private DataTable dtbHeader = new DataTable(TABLE_NAME_REMITBATCH_HEADER);
		
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			dtbHeader.Columns.Add("Date",typeof(System.DateTime));
			
			
		}

		protected void Page_Render(object sender, System.EventArgs e)
		{			
	
			
			if (SearchClicked)
			{
				LoadData();
				Bind();
			}
							
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ctrlSearchModule.SearchClicked += new SearchEventHandler(ctrlSearchModule_SearchClicked);
			base.OnInit(e);
		}
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.higMainM.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.higMain_TemplateSelection);
			this.higMainM.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.higMainM_PageIndexChanged);
			this.PreRender += new System.EventHandler(this.Page_Render);

		}
		#endregion
		
		/// <summary>
		/// set relation of the dataset
		/// </summary>
		/// <param name="dts"></param>
		private  void SetRelation(DataSet dts)
		{
			try
			{
				DataRelation rel = new DataRelation("RelDateRemit",
					dts.Tables[TABLE_NAME_REMITBATCH_HEADER].Columns["Date"],
					dts.Tables[RemitBatchTable.TBL_REMITBATCH].Columns["Date"],false);

				dts.Relations.Add(rel);
		
				DataRelation rel1 = new DataRelation(RELATIONNAME, 
					dts.Tables[RemitBatchTable.TBL_REMITBATCH].Columns["ID"], 
					dts.Tables[CustomerOrderDetailRemitHistoryTable.TBL_CUSTOMERORDERDETAILREMITHISTORY].Columns["RemitBatchID"],false);
			
				dts.Relations.Add(rel1);
			}
			catch(Exception e)
			{
				string ss = e.Message;
			}
		
		}
		/// <summary>
		/// create the dataset with 2 tables
		/// </summary>
		private void CreateDataSet()
		{
			dtsMainM = new DataSet();
			dtsMainM.Tables.Add(dtbRemitBatch);//(TABLE_NAME_CUSTOMERORDERDETAILREMITHISTORY);
			dtsMainM.Tables.Add(dtbCODRH);
			dtsMainM.Tables.Add(dtbHeader);
			
		}
		/// <summary>
		/// Load all data for the page
		/// </summary>
		private void LoadData()
		{
			CreateDataSet();
			LoadTableRemitBatch();
			LoadTableCustomerOrderDetailRemitHistory();
			LoadRemitBatchDate();
			Cache["MyData"] = dtsMainM;
		}
		/// <summary>
		/// Load date from Remit Batch
		/// </summary>
		private void LoadTableRemitBatch()
		{
			try
			{
					
					buiRemitBatch.SelectByDate(dtbRemitBatch,42001,dFrom,dTo);
				
				
				
			}
			catch
			{
				
			}
			
		}
		/// <summary>
		/// Load data from Order Detail Remit history
		/// </summary>
		private void LoadTableCustomerOrderDetailRemitHistory()
		{
			try
			{
			
					buiCusOrdDetRemHis.SelectByDate(dtbCODRH,42001,dFrom,dTo);
			
			}
			catch
			{
			
			}
		}
		/// <summary>
		/// Load Data
		/// </summary>
		private void LoadRemitBatchDate()
		{
			try
			{
				buiRemitBatch.SelectByDateNestedGridHeader(dtbHeader,42001,dFrom,dTo);
			}
			catch
			{
			
			}
			
		}
		/// <summary>
		/// 
		/// </summary>
		protected void Bind()
		{
			dtsMainM = (DataSet) Cache["MyData"];;
		
			higMainM.DataSource = dtsMainM;
			if(dtsMainM.Tables[0].Rows.Count != 0)
			{
						
				if(!dtsMainM.Relations.Contains(RELATIONNAME))
					SetRelation(dtsMainM);
				
			}
					
			higMainM.DataMember = TABLE_NAME_REMITBATCH_HEADER;
			higMainM.DataBind();
						
		}
		private void higMain_PageIndexChanged(object source,System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			higMainM.CurrentPageIndex = e.NewPageIndex;
			higMainM.DataBind();
		}

		protected void UpdateView(object sender, System.EventArgs e)
		{			
			Bind();
		}

		private void higMain_TemplateSelection(object sender, DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs e)
		{
			switch(e.Row.Table.TableName)
			{
				case "RemitBatch":
					e.TemplateFilename = e.Row.Table.TableName + ".ascx";
					break;
				
			}
		}
		private void ctrlSearchModule_SearchClicked(object sender, SearchClickedArgs e)
		{
				dFrom =e.From;
				dTo  = e.To;
				SearchClicked = true;
		}

		private void higMainM_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.higMainM.CurrentPageIndex = e.NewPageIndex;
			Bind();
		}
	}
}
