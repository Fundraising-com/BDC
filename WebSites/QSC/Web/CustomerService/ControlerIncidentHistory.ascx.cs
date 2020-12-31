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
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for ControlerIncidentHistory.
	/// </summary>
	public class ControlerIncidentHistory : CustomerServiceControlDataGrid
	{
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		private int iIncidentBefore = 0;
		private string css = "csIncidentHistoryItem1";
		private const string CSINCIDENTHISTORYITEM1 = "csIncidentHistoryItem1";
		private const string CSINCIDENTHISTORYITEM2 = "csIncidentHistoryItem2";
		protected Label lblMessage;
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.Message= "No incident history";
		}
		private void Page_PreRender(object sender, EventArgs e)
		{
			if(this.Page.ResultSelected)
			{
				if(IsSelectOnly && !IsPostBack)
				{
					
					this.dtgMain.ShowFooter = false;
					this.dtgMain.Columns[8].Visible = true;
				}

				DataBind();
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);
			this.dtgMain.ItemDataBound +=new DataGridItemEventHandler(dtgMain_ItemDataBound);
		}
		#endregion

		protected override void LoadData()
		{
			try
			{
				DataSource = new IncidentActionTable();
				this.Page.BusIncident.SelectByCOHInstance(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
			}
			catch(NullReferenceException ex)
			{
				bool hasKey = false;

				foreach(string key in Session.Keys) 
				{
					if(key == "CurrentInfoSession") 
					{
						hasKey = true;
					}
				}

				if(hasKey) 
				{
					ex.Source += " Has the session key.";
				} 
				else 
				{
					ex.Source += " Does not have the session key.";
				}

				throw ex;
			}
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			
			int iCurrentIncident = 0;
			foreach(DataGridItem item in this.dtgMain.Items)
			{
				if(item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.EditItem)
				{	
					iCurrentIncident = GetIncidentInstance(item);

					if(iIncidentBefore != iCurrentIncident && item.ItemIndex !=0)
					{
						if(css == CSINCIDENTHISTORYITEM1)
							css = CSINCIDENTHISTORYITEM2;
						else
							css = CSINCIDENTHISTORYITEM1;
					}
				
					item.CssClass	= css;
					iIncidentBefore =  iCurrentIncident;
				}
			}
			
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				DataRowView view = (DataRowView)e.Item.DataItem;
				((HyperLink)e.Item.FindControl("hypEditComment")).Attributes.Add("onclick","javascript:Open('Comments.aspx?IsNewWindow=true&Instance="+ view[IncidentActionTable.FLD_INSTANCE].ToString()+"')");
			}
		}
		private int GetIncidentInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblIncidentInstance")).Text);
		}

		
	}
}
