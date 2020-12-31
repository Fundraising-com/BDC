namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	public enum ListActionType {Resume,Complete};
	/// <summary>
	///		Summary description for ControlerAction.
	/// </summary>
	public class ControlerListAction :  CustomerServiceControlDataGrid
	{
		protected DataGridObject dtgMain;
		private ListActionType mListActionType = ListActionType.Complete;
		protected Label lblMessage;
		
		private void Page_PreRender(object sender, EventArgs e)
		{
			this.Message = "";
			if(mListActionType == ListActionType.Resume)
			{
								
			}
			if(this.Page.IncidentID != -1)
			{
				this.DataBind();
			}
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,this.dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.Page_PreRender);
			this.dtgMain.ItemDataBound +=new DataGridItemEventHandler(dtgMain_ItemDataBound);
		}
		#endregion
		public ListActionType ActionType
		{
			get{return mListActionType;}
			set{mListActionType = value;}
		}

		protected override void LoadData()
		{
			DataSource = new IncidentActionTable();
			if(this.Page.IncidentID != -1) 
			{
				this.Page.BusIncidentAction.SelectByIncidentID(DataSource,this.Page.IncidentID);
			}
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				DataRowView view = (DataRowView)e.Item.DataItem;
				((HyperLink)e.Item.FindControl("hypEdit")).Attributes.Add("onclick","javascript:Open('Comments.aspx?IsNewWindow=true&Instance="+ view[IncidentActionTable.FLD_INSTANCE].ToString()+"')");
			}
			
		}

	}

}