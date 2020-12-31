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
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSP.WebControl;
	
	public delegate void SelectFulfillmentHouseEventHandler(object sender, SelectFulfillmentHouseClickedArgs e);

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class FulfillmentHouseSearchControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxCity;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div1;
		protected System.Web.UI.WebControls.TextBox tbxName;

		private const string PRIMARY_KEY = "Ful_Nbr";

		public event SelectFulfillmentHouseEventHandler SelectFulfillmentHouseClick;
		
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
			if(e.CommandName == "Select")
			{
				try 
				{
					SelectFulfillmentHouseClickedArgs args;
				
					args = new SelectFulfillmentHouseClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouse(GetFulfillmentHouseNumber(e.Item), GetName(e.Item), GetStatus(e.Item), GetAddress1(e.Item), GetAddress2(e.Item), GetCity(e.Item), GetStateProvince(e.Item), GetZip(e.Item), GetCountry(e.Item), GetInterfaceMedia(e.Item), GetInterfaceLayout(e.Item), GetTransmissionMethod(e.Item), GetHardCopy(e.Item), GetQSPAgencyCode(e.Item), GetIsEffortKeyRequired(e.Item), GetPayGroupLookUpCode(e.Item)));
				
					if(SelectFulfillmentHouseClick != null)
						SelectFulfillmentHouseClick(source,args);
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
					this.dtgMain.FilterExpression = "Ful_Name_WithoutAccents like '%" + StringExtended.ReplaceAccents(GetNameSearch()) + "%'";
				}

				if(GetStatusSearch() != String.Empty) 
				{
					if(this.dtgMain.FilterExpression != String.Empty) 
					{
						this.dtgMain.FilterExpression += " and Ful_Status = '" + GetStatusSearch() + "'";
					} 
					else 
					{
						this.dtgMain.FilterExpression = "Ful_Status = '" + GetStatusSearch() + "'";
					}
				}

				if(GetCitySearch() != String.Empty) 
				{
					if(this.dtgMain.FilterExpression != String.Empty) 
					{
						this.dtgMain.FilterExpression += " and Ful_City = '" + GetCitySearch() + "'";
					} 
					else 
					{
						this.dtgMain.FilterExpression = "Ful_City = '" + GetCitySearch() + "'";
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

		public FulfillmentHouse FulfillmentHouseInfo 
		{
			get 
			{
				return (FulfillmentHouse) ViewState["FulfillmentHouseInfo"];
			}
			set 
			{
				ViewState["FulfillmentHouseInfo"] = value;
			}
		}

		public override void DataBind()
		{
			int index;

			SetValueDDL();
			base.DataBind ();

			if(this.FulfillmentHouseInfo != null) 
			{				
				index = this.dtgMain.SearchDataRow(PRIMARY_KEY,this.FulfillmentHouseInfo.FulfillmentHouseNumber.ToString());
				this.dtgMain.ShowDataGridItem(index);
				this.FulfillmentHouseInfo = null;
			}
		}


		protected override void LoadData()
		{
			DataSource = new DataTable("FulfillmentHouse");

			this.Page.BusFulfillmentHouse.SelectAll(DataSource);
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

		private int GetFulfillmentHouseNumber(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblFulfillmentHouseNumber")).Text);
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

		private string GetCountry(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblCountry")).Text;
		}

		private InterfaceMedia GetInterfaceMedia(DataGridItem e)
		{
			return (InterfaceMedia) Convert.ToInt32(((Label)e.FindControl("lblInterfaceMediaID")).Text);
		}

		private InterfaceLayout GetInterfaceLayout(DataGridItem e)
		{
			return (InterfaceLayout) Convert.ToInt32(((Label)e.FindControl("lblInterfaceLayoutID")).Text);
		}

		private TransmissionMethod GetTransmissionMethod(DataGridItem e)
		{
			return (TransmissionMethod) Convert.ToInt32(((Label)e.FindControl("lblTransmissionMethodID")).Text);
		}

		private bool GetHardCopy(DataGridItem e)
		{
			return Convert.ToBoolean(((Label)e.FindControl("lblHardCopy")).Text);
		}

		private string GetQSPAgencyCode(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblQSPAgencyCode")).Text;
		}

		private string GetIsEffortKeyRequired(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblIsEffortKeyRequired")).Text;
		}

		private string GetPayGroupLookUpCode(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblPayGroupLookUpCode")).Text;
		}
	}
}


	

