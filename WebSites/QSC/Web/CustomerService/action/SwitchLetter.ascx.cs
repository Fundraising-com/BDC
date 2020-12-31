namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for cancelsub.
	/// </summary>
	public partial class SwitchLetter : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Switch Letter";
		private DataTable Table;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if(!IsPostBack)
				SetValueDDL();
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

		
		
		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			DataTable customerOrderDetailRemitHistoryTable = new CustomerOrderDetailRemitHistoryTable();
			this.Page.BusCustomerOrderDetailRemitHistory.SelectOneLastTransaction(customerOrderDetailRemitHistoryTable,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);

			Business.Objects.InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = new Business.Objects.InactiveMagazineLetterBatchItem(Convert.ToInt32(this.Page.UserID));
			
			if(customerOrderDetailRemitHistoryTable.Rows.Count!=0)
				inactiveMagazineLetterBatchItem.ProductCode = customerOrderDetailRemitHistoryTable.Rows[0][CustomerOrderDetailRemitHistoryTable.FLD_TITLECODE].ToString();	
			inactiveMagazineLetterBatchItem.Reason = GetReason();
			this.Page.BusSwitchLetterBatch.Generate(inactiveMagazineLetterBatchItem, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);	
		}
		
		private void SetValueDDL()
		{
			LoadData();
			if(Table.Rows.Count != 0)
			{
				this.ddlReason.DataSource = Table;
				DataRow dtrow = Table.NewRow();
				dtrow[CodeDetailTable.FLD_DESCRIPTION]= "Select";
				dtrow[CodeDetailTable.FLD_INSTANCE] = 0;
				Table.Rows.InsertAt(dtrow,0);
				this.ddlReason.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlReason.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlReason.DataBind();
			}
		
		}
		private void LoadData()
		{
			try
			{
				
				Table = new DataTable("CodeDetail");
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table,1000);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private int GetReason()
		{

			return Convert.ToInt32(this.ddlReason.SelectedItem.Value);
		}
		
			
			
				
			
			
		
	}
}
