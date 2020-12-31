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
	
	public delegate void SelectCatalogEventHandler(object sender, SelectCatalogClickedArgs e);
	/// <summary>
	///		Summary description for CatalogSearchControl.
	/// </summary>
	public class CatalogSearchControl : MarketingMgtControlDataGrid
	{
		/************* vars for delete confirmation ********************/
		private const string DELETE_CATALOG_CONFIRMATION_MESSAGE = "Are you sure you want to delete this catalog and all its associated sections and contracts?";
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;
		/***************************************************************/

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList ddlNewRenew;
		protected System.Web.UI.WebControls.Label lblProductType;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox tbxCatalogCode;
		protected QSP.WebControl.DropDownListInteger ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlSeason;
		protected QSP.WebControl.DropDownListInteger ddlType;
		protected System.Web.UI.WebControls.DropDownList ddlLanguage;
		protected System.Web.UI.WebControls.Label Label6;
		protected QSP.WebControl.DropDownListInteger ddlStatus;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox tbxCatalogName;
		protected DataGridObject dtgMain;
		
		public event SelectCatalogEventHandler SelectCatalogClick;
		public event SelectCatalogEventHandler SelectCatalogDelete;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Page.NewSearch = true;

				DataBind();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "Select")
				{
				
					SelectCatalogClickedArgs args;
				
					args = new SelectCatalogClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Catalog(GetCatalogID(e.Item), GetCatalogCode(e.Item), GetName(e.Item), GetYear(e.Item), GetSeason(e.Item), GetType(e.Item), GetLanguage(e.Item), GetStatus(e.Item), GetIsReplacement(e.Item)));
				
					if(SelectCatalogClick != null)
						SelectCatalogClick(source,args);
				
				}

					//"Delete" command handling 
				else if(e.CommandName == "Delete") 
				{
                    if (this.Page.BusCatalog.ValidateDelete(GetCatalogID(e.Item)))
                    {
                        this.CatalogIDToDelete = GetCatalogID(e.Item);
                        
                        DeleteCatalog(CatalogIDToDelete);
                    }

					//ShowDeleteCatalogConfirmation(GetCatalogID(e.Item));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		/// <summary>
		/// when the deletion has been confirmed by the user - delete catalog, its sections and its contracts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ctrlControlerConfirmationPageDelete_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				if(CatalogIDToDelete != 0) 
				{
					DeleteCatalog(CatalogIDToDelete);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			btnSearch.Click +=new EventHandler(btnSearch_Click);
			this.ctrlControlerConfirmationPageDelete.Confirmed += new ConfirmEventHandler(ctrlControlerConfirmationPageDelete_Confirmed);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// CatalogID of the catalog that was marked for deletion
		/// </summary>
		private int CatalogIDToDelete 
		{
			get 
			{
				int catalogIDToDelete = 0;

				if(ViewState["CatalogIDToDelete"] != null)
				{
					catalogIDToDelete = Convert.ToInt32(this.ViewState["CatalogIDToDelete"]);
				} 

				return catalogIDToDelete;
			}
			set 
			{
				this.ViewState["CatalogIDToDelete"] = value;
			}
		}		

		public bool ShowSearch 
		{
			get 
			{
				return divSearch.Visible;
			}
			set 
			{
				divSearch.Visible = value;
			}
		}

		#region Fields

		private string Code
		{
			get 
			{
				return this.tbxCatalogCode.Text;
			}
			set 
			{
				this.tbxCatalogCode.Text = value;
			}
		}
		
		private string Name 
		{
			get 
			{
				return this.tbxCatalogName.Text;
			}
			set 
			{
				this.tbxCatalogName.Text = value;
			}
		}

		private int Year 
		{
			get 
			{
				return this.ddlYear.Value;
			}
			set 
			{
				this.ddlYear.Value = value;
			}
		}
		
		private string Season 
		{
			get 
			{
				return this.ddlSeason.SelectedValue;
			}
			set 
			{
				this.ddlSeason.SelectedIndex = this.ddlSeason.Items.IndexOf(this.ddlSeason.Items.FindByValue(value));
			}
		}
		
		private CatalogType Type 
		{
			get 
			{
				return (CatalogType) this.ddlType.Value;
			}
			set 
			{
				this.ddlType.Value = Convert.ToInt32(value);
			}
		}
		
		private string Language 
		{
			get 
			{
				return this.ddlLanguage.SelectedValue;
			}
			set 
			{
				this.ddlLanguage.SelectedIndex = this.ddlLanguage.Items.IndexOf(this.ddlLanguage.Items.FindByValue(value));
			}
		}
		
		private CatalogStatus Status 
		{
			get 
			{
				return (CatalogStatus) this.ddlStatus.Value;
			}
			set 
			{
				this.ddlStatus.Value = Convert.ToInt32(value);
			}
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind();
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Catalog");
			this.Page.BusCatalog.SelectSearch(DataSource, Code, Name, Year, Season, Type, Language, Status, 0, String.Empty);
		}

		//validate delete and display delete confirmation
		private void ShowDeleteCatalogConfirmation(int catalogID) 
		{
			if(this.Page.BusCatalog.ValidateDelete(catalogID))
			{
				this.CatalogIDToDelete = catalogID;

				this.ctrlControlerConfirmationPageDelete.Message = DELETE_CATALOG_CONFIRMATION_MESSAGE;
				this.ctrlControlerConfirmationPageDelete.ShowConfirmationWindow();
			}
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
			SetValueDDLType();
			SetValueDDLStatus();
		}

		private void SetValueDDLYear() 
		{
			if(this.ddlYear.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogFinancialYears(table);
						
				this.ddlYear.DataSource = table;
				this.ddlYear.DataTextField = "FiscalYear";
				this.ddlYear.DataValueField = "FiscalYear";
				this.ddlYear.DataBind();
			}
		}

		private void SetValueDDLSeason() 
		{
			if(this.ddlSeason.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogSeasons(table);
						
				this.ddlSeason.Items.Add(new ListItem("", ""));

				foreach(DataRow row in table.Rows)
				{
					this.ddlSeason.Items.Add(new ListItem(row["Season"].ToString(), row["Season"].ToString()));
				}	
			}
		}

		private void SetValueDDLType() 
		{
			if(this.ddlType.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogTypes(table);
						
				this.ddlType.DataSource = table;
				this.ddlType.DataTextField = "Description";
				this.ddlType.DataValueField = "Instance";
				this.ddlType.DataBind();
			}
		}

		private void SetValueDDLStatus() 
		{
			if(this.ddlStatus.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogStatus(table);
						
				this.ddlStatus.DataSource = table;
				this.ddlStatus.DataTextField = "Description";
				this.ddlStatus.DataValueField = "Instance";
				this.ddlStatus.DataBind();
			}
		}

		private int GetCatalogID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblCatalogID")).Text);
		}

		private string GetCatalogCode(DataGridItem e)
		{
			return ((Label)e.FindControl("lblCatalogCode")).Text;
		}

		private string GetName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblName")).Text;
		}

		private int GetYear(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblYear")).Text);
		}

		private string GetSeason(DataGridItem e)
		{
			return ((Label)e.FindControl("lblSeason")).Text;
		}

		private CatalogType GetType(DataGridItem e)
		{
			try 
			{
				return (CatalogType) Convert.ToInt32(((Label)e.FindControl("lblTypeID")).Text);
			} 
			catch(System.ArgumentException)
			{
				return CatalogType.None;
			}
		}

		private string GetLanguage(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblLanguage")).Text;
		}

		private CatalogStatus GetStatus(DataGridItem e)
		{
			try 
			{
				return (CatalogStatus) Enum.Parse(typeof(CatalogStatus), ((Label)e.FindControl("lblStatus")).Text, false);
			} 
			catch(System.ArgumentException) 
			{
				return CatalogStatus.None;
			}
		}

		private string GetIsReplacement(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblIsReplacement")).Text;
		}

		//deletes catalog, its sections and contracts
		protected virtual void DeleteCatalog(int catalogID)
		{
			SelectCatalogClickedArgs args = null;

			this.Page.BusCatalog.Delete(catalogID);

			args = new SelectCatalogClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Catalog(catalogID, String.Empty, String.Empty, 0, String.Empty, CatalogType.None, String.Empty, CatalogStatus.None, String.Empty));

			//raise "Catalog Deleted" event for the parent control
			if(SelectCatalogDelete != null) 
			{
				SelectCatalogDelete(this, args);
			}
		}
	}
}


	

