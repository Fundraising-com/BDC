namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CustomerService;
	using QSP.WebControl;
	
	public delegate void SelectPublisherEventHandler(object sender, SelectPublisherClickedArgs e);

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class PublisherSearchControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxName;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxCity;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div1;
		protected DataGridObject dtgMain;

		public event SelectPublisherEventHandler SelectPublisherClick;
		
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
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			SelectPublisherClickedArgs args;

			if(e.CommandName == "Select")
			{
				try 
				{				
					args = new SelectPublisherClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Publisher(GetPublisherNumber(e.Item), GetName(e.Item), GetStatus(e.Item), GetAddress1(e.Item), GetAddress2(e.Item), GetCity(e.Item), GetStateProvince(e.Item), GetZip(e.Item), GetCountryCode(e.Item)));
				
					if(SelectPublisherClick != null)
						SelectPublisherClick(source,args);
				} 
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Page.NewSearch = true;
				this.dtgMain.FilterExpression ="";

				if(GetNameSearch() != String.Empty) 
				{
					this.dtgMain.FilterExpression = "Pub_Name_WithoutAccents like '%" + StringExtended.ReplaceAccents(GetNameSearch()) + "%'";
				}

				if(GetStatusSearch() != String.Empty) 
				{
					if(this.dtgMain.FilterExpression != String.Empty) 
					{
						this.dtgMain.FilterExpression += " and Pub_Status = '" + GetStatusSearch() + "'";
					} 
					else 
					{
						this.dtgMain.FilterExpression = "Pub_Status = '" + GetStatusSearch() + "'";
					}
				}

				if(GetCitySearch() != String.Empty) 
				{
					if(this.dtgMain.FilterExpression != String.Empty) 
					{
						this.dtgMain.FilterExpression += " and Pub_City = '" + GetCitySearch() + "'";
					} 
					else 
					{
						this.dtgMain.FilterExpression = "Pub_City = '" + GetCitySearch() + "'";
					}
				}

				this.dtgMain.SelectedIndex = -1;

				// TO REMOVE:
				DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();
		}


		protected override void LoadData()
		{
			DataSource = new DataTable("Publisher");

			this.Page.BusPublisher.SelectAll(DataSource);
		}

		private void SetValueDDL() 
		{
			SetValueDDLStatus();
		}

		private void SetValueDDLStatus() 
		{
			if(this.ddlStatus.Items.Count == 0)
			{
				this.ddlStatus.Items.Add(new ListItem("", ""));
				this.ddlStatus.Items.Add(new ListItem("Active", "ACTIVE"));
				this.ddlStatus.Items.Add(new ListItem("Inactive", "INACTIVE"));
			}
		}

		private string GetNameSearch() 
		{
			return this.tbxName.Text.Replace("'", "''");
		}

		private string GetStatusSearch() 
		{
			return this.ddlStatus.SelectedValue;
		}

		private string GetCitySearch() 
		{
			return this.tbxCity.Text.Replace("'", "''");
		}

		private int GetPublisherNumber(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPublisherNumber")).Text);
		}
		private string GetName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblName")).Text;
		}
		private string GetStatus(DataGridItem e)
		{
			return ((Label)e.FindControl("lblStatus")).Text;
		}
		private string GetAddress1(DataGridItem e)
		{
			return ((Label)e.FindControl("lblAddress1")).Text;
		}
		private string GetAddress2(DataGridItem e)
		{
			return ((Label)e.FindControl("lblAddress2")).Text;
		}
		private string GetCity(DataGridItem e)
		{
			return ((Label)e.FindControl("lblCity")).Text;
		}
		private string GetStateProvince(DataGridItem e)
		{
			return ((Label)e.FindControl("lblStateProvince")).Text;
		}
		private string GetZip(DataGridItem e)
		{
			return ((Label)e.FindControl("lblZip")).Text;
		}
		private string GetCountryCode(DataGridItem e)
		{
			return ((Label)e.FindControl("lblCountryCode")).Text;
		}
	}
}