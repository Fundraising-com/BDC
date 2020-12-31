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

	/// <summary>
	///		Summary description for ResultShippement.
	/// </summary>
	public class ResultShipment : ControlerResult
	{
		
		protected DataGridObject dtgMain;
		protected Label lblMessage;

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
			this.dtgMain.ItemCommand +=new DataGridCommandEventHandler(dtgMain_ItemCommand);
			this.dtgMain.PageIndexChanged += new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void LoadData()
		{
			this.Page.BusSearch.SelectSearchShippement(Table,List);
		}

		
		public override void DataBind()
		{
			try
			{
				DataTable Table = new DataTable();
				this.Page.BusSearch.SelectSearchShippement(Table,List);
				dtgMain.DataSource = Table;
				if(this.Page.NewSearch) 
				{
					dtgMain.CurrentPageIndex = 0;
					dtgMain.SelectedIndex = -1;
				}
				if(Table.Rows.Count!=0)
				{
					this.dtgMain.Visible = true;
					this.lblMessage.Visible= false;
					
					dtgMain.DataBind();
				}
				else
				{
					lblMessage.Text = "No result";
					this.dtgMain.Visible = false;
					this.lblMessage.Visible= true;
					
				}
							
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == DataGrid.SelectCommandName)
			{
				CurrentOrderInfo coi = new CurrentOrderInfo();
				coi.OrderID = GetOrderID(e.Item);
				coi.ShipmentID = GetShipmentID(e.Item);
				this.Page.FireEventSelect(new SelectResultClickedArgs(coi,false));	
			}
		}
		private int GetOrderID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblOrderID")).Text);
		}
		private int GetShipmentID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblShipmentID")).Text);
		}

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Page.PageChanged = true;
			this.Page.NewSearch = false;
			this.dtgMain.SelectedIndex = -1;
			this.dtgMain.CurrentPageIndex = e.NewPageIndex;
			this.dtgMain.DataBind();
		}
	}
}
