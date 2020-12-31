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
	
	public delegate void SelectCatalogSectionEventHandler(object sender, SelectCatalogSectionClickedArgs e);
	/// <summary>
	///		Summary description for CatalogSearchControl.
	/// </summary>
	public class CatalogSectionSearchControl : MarketingMgtControlDataGrid
	{
		private const string DELETE_SECTION_CONFIRMATION_MESSAGE = "Are you sure you want to delete this section and all its associated contracts?";

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnSearch;
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
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlSeason;
		protected System.Web.UI.WebControls.DropDownList ddlType;
		protected System.Web.UI.WebControls.DropDownList ddlLanguage;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox tbxCampaignID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox tbxProductCode;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;
		protected DataGridObject dtgMain;
		
		public event SelectCatalogSectionEventHandler SelectCatalogSectionClick;
		public event SelectCatalogSectionEventHandler SelectCatalogSectionIncludeProducts;
		public event SelectCatalogSectionEventHandler SelectCatalogSectionDelete;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerConfirmationPageDelete.Confirmed += new ConfirmEventHandler(ctrlControlerConfirmationPageDelete_Confirmed);
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				SelectCatalogSectionClickedArgs args;
				
				if(e.CommandName == "Edit")
				{
                    args = new SelectCatalogSectionClickedArgs(new CatalogSection(GetCatalogSectionID(e.Item), GetType(e.Item), GetTypeDescription(e.Item), GetName(e.Item), GetFSProgramID(e.Item)));
                    if (SelectCatalogSectionClick != null)
						SelectCatalogSectionClick(source,args);
				} 
				else if(e.CommandName == "IncludeProducts") 
				{
                    args = new SelectCatalogSectionClickedArgs(new CatalogSection(GetCatalogSectionID(e.Item), GetType(e.Item), GetTypeDescription(e.Item), GetName(e.Item), GetFSProgramID(e.Item)));
                    if (SelectCatalogSectionIncludeProducts != null)
						SelectCatalogSectionIncludeProducts(source,args);
				} 
				else if(e.CommandName == "Delete") 
				{
                    if (this.Page.BusCatalogSection.ValidateDelete(GetCatalogSectionID(e.Item)))
                    {
                        this.CatalogSectionIDToDelete = GetCatalogSectionID(e.Item);

                        DeleteSection(CatalogSectionIDToDelete);
                    }

					//ShowDeleteSectionConfirmation(GetCatalogSectionID(e.Item));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerConfirmationPageDelete_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				if(CatalogSectionIDToDelete != 0) 
				{
					DeleteSection(CatalogSectionIDToDelete);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private int CatalogSectionIDToDelete 
		{
			get 
			{
				int catalogSectionIDToDelete = 0;

				if(ViewState["CatalogSectionIDToDelete"] != null)
				{
					catalogSectionIDToDelete = Convert.ToInt32(this.ViewState["CatalogSectionIDToDelete"]);
				} 

				return catalogSectionIDToDelete;
			}
			set 
			{
				this.ViewState["CatalogSectionIDToDelete"] = value;
			}
		}

		public override void DataBind()
		{
			if(this.Page.CatalogInfo != null) 
			{
				LoadData();
				Bind();
			} 
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Catalog");

			this.Page.BusCatalogSection.SelectSearch(DataSource, this.Page.CatalogInfo.CatalogID);
		}

		private void ShowDeleteSectionConfirmation(int catalogSectionID) 
		{
			if(this.Page.BusCatalogSection.ValidateDelete(catalogSectionID))
			{
				this.CatalogSectionIDToDelete = catalogSectionID;

				this.ctrlControlerConfirmationPageDelete.Message = DELETE_SECTION_CONFIRMATION_MESSAGE;
				this.ctrlControlerConfirmationPageDelete.ShowConfirmationWindow();
			}
		}

		protected virtual void DeleteSection(int catalogSectionID)
		{
			SelectCatalogSectionClickedArgs args = null;

			this.Page.BusCatalogSection.Delete(catalogSectionID);

			args = new SelectCatalogSectionClickedArgs(new CatalogSection(catalogSectionID, CatalogSectionType.None, String.Empty, String.Empty, 0));

			if(SelectCatalogSectionDelete != null) 
			{
				SelectCatalogSectionDelete(this, args);
			}
		}

		private int GetCatalogSectionID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblSectionID")).Text);
		}

		private string GetName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblSectionName")).Text;
		}

		private CatalogSectionType GetType(DataGridItem e) 
		{
			try 
			{
				return (CatalogSectionType) Enum.ToObject(typeof(CatalogSectionType), Convert.ToInt32(((Label)e.FindControl("lblSectionTypeID")).Text));
			} 
			catch(System.ArgumentException)
			{
				return CatalogSectionType.None;
			}
		}

		private string GetTypeDescription(DataGridItem e)
		{
			return ((Label)e.FindControl("lblSectionType")).Text;
		}

		private int GetFSProgramID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblFSProgramID")).Text);
		}
	}
}