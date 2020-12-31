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
	public partial class GiftCardOutput : QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected const string RELATIONNAME = "RemitBatchID";
		protected DataSet dtsMainM;
		private const string SP_SELECTGIFTCARDOUTPUTALL  = "sp_SelectGiftCardOutputAll";
		private const string SP_SELECT_REMITBATCH_ALL = "sp_SelectRemitBatchByStatus";
		
		private const string TABLE_NAME_GIFTCARDOUTPUT = "GiftCardOutput";
		private const string TABLE_NAME_REMITBATCH = "RemitBatch";
		protected DBauer.Web.UI.WebControls.HierarGrid higMain;
		private RemitBatchBusiness buiRemitBatch = new RemitBatchBusiness(false);
		private GiftCardOutputBusiness buiGiftCard = new GiftCardOutputBusiness(false);
		private RemitBatchTable dtbRemitBatch = new RemitBatchTable();
		private GiftCardOutputTable dtbGiftCard = new GiftCardOutputTable();
		private DateTime dFrom;
		private DateTime dTo;
		private bool SearchClicked = false;
		protected SearchModule ctrlSearchModule;

		protected void Page_Load(object sender, System.EventArgs e)
		{
					
				
							
		}
		protected void Page_PreRender(object sender,EventArgs e)
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
			this.ctrlSearchModule.SearchClicked += new SearchEventHandler(ctrlSearchModule_SearchClicked);
			base.OnInit(e);
		}
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.higMainM.TemplateSelection += new DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventHandler(this.higMain_TemplateSelection);
			this.higMainM.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.higMainM_PageIndexChanged);

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
				DataRelation rel1 = new DataRelation(RELATIONNAME, 
					dts.Tables[GiftCardOutputTable.TBL_GIFTCARDOUTPUT].Columns["ID"], 
					dts.Tables[RemitBatchTable.TBL_REMITBATCH].Columns["GiftCardOutputID"],false);
			
				dts.Relations.Add(rel1);
			}
			catch
			{
			}
		
		}
		/// <summary>
		/// create the dataset with 2 tables
		/// </summary>
		private void CreateDataSet()
		{
			dtsMainM = new DataSet();
			dtsMainM.Tables.Add(dtbGiftCard);
			dtsMainM.Tables.Add(dtbRemitBatch);
			
			
		}
		/// <summary>
		/// Load All data for the page
		/// </summary>
		private void LoadData()
		{
			CreateDataSet();
			LoadTableRemitBatch();
			LoadTableGiftCardOutput();
			
			Cache["MyData"] = dtsMainM;
		}
		/// <summary>
		/// Load Data from table remit batch
		/// </summary>
		private void LoadTableRemitBatch()
		{

			try
			{
				buiRemitBatch.SelectByDateNestedGridGiftCardOutputSecondLevel(dtbRemitBatch,42001,DateTime.MinValue,DateTime.MinValue);
			}
			catch
			{
			
			}
		}
		/// <summary>
		/// Load Data from Gift Card output
		/// </summary>
		private void LoadTableGiftCardOutput()
		{

			try
			{
				buiGiftCard.SelectByDate(dtbGiftCard,dFrom,dTo);
			}
			catch
			{
			
			}
		
		}
		/// <summary>
		/// Get information about participant and supporter how bought 
		/// </summary>
		protected void Bind()
		{
			dtsMainM = (DataSet) Cache["MyData"];;
		
			higMainM.DataSource = dtsMainM;//(DataSet) ((DataGridItem) this.BindingContainer).DataItem;
			if(dtsMainM.Tables[0].Rows.Count != 0)
			{
						
				if(!dtsMainM.Relations.Contains(RELATIONNAME))
					SetRelation(dtsMainM);
				
			}
			
		
			higMainM.DataMember = GiftCardOutputTable.TBL_GIFTCARDOUTPUT;
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
				case "RemitBatch" :
					e.TemplateFilename = e.Row.Table.TableName + "Card.ascx";
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
