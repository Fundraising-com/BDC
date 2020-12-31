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
using QSPFulfillment.CustomerService;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.MarketingMgt.Control;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class ImportProducts : MarketingMgtPage
	{
		private const string IMPORT_LIST_CONFIRMATION_MESSAGE = "Are you sure you want to import this contract list?";
		private const string CONTROL_PATH = "Control\\";
		private const int PAGE_SIZE = 20;

		protected ProductContractSearchControl ctrlProductContractSearchControl;
		protected ControlerConfirmationPage ctrlControlerConfirmationPage;

        private void OnPreLoad(object sender, EventArgs e)
        {
            try
            {
                LoadProductContractSearchControl();
				this.ctrlProductContractSearchControl.ProductTypeChanged += new ProductTypeChangedEventHandler(ctrlProductContractSearchControl_ProductTypeChanged);
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
        
		protected void ImportProducts_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				try 
				{
					DataBind();
				} 
				catch(Exception ex) 
				{
					ManageError(ex);
				}
			}
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerConfirmationPage.Confirmed += new ConfirmEventHandler(ctrlControlerConfirmationPage_Confirmed);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
            this.PreLoad += new EventHandler(OnPreLoad);
            this.PreRender += new System.EventHandler(this.ImportProducts_PreRender);
		}
		#endregion

		protected void btnImport_Click(object sender, System.EventArgs e)
		{
			try 
			{
				ImportProductContracts();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected void btnImportList_Click(object sender, EventArgs e)
		{
			try 
			{
				this.ctrlControlerConfirmationPage.Message = IMPORT_LIST_CONFIRMATION_MESSAGE;
				this.ctrlControlerConfirmationPage.ShowConfirmationWindow();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> self.close(); </script>");
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlControlerConfirmationPage_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				ImportProductContractsList();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlProductContractSearchControl_ProductTypeChanged(object sender, ProductTypeChangedArgs e)
		{
			this.ProductType = e.ProductType;
			SwitchProductType();
		}

		private ProductType ProductType 
		{
			get 
			{
				ProductType productType = ProductTypeDefault;

				if(ViewState["ProductType"] != null) 
				{
					productType = (ProductType) ViewState["ProductType"];
				}

				return productType;
			}
			set 
			{
				ViewState["ProductType"] = value;
			}
		}

		private ProductType ProductTypeDefault
		{
			get 
			{
				ProductType productTypeDefault = ProductType.Magazine;

				try 
				{
					if(Request.QueryString["ProductType"] != null) 
					{
						productTypeDefault = (ProductType) Convert.ToInt32(Request.QueryString["ProductType"]);
					}
				} 
				catch { }

				return productTypeDefault;
			}
		}

		public override void DataBind()
		{
			ctrlProductContractSearchControl.DataBindInitialData();

			SetVisibleImportForSeason();
		}

		private void SetVisibleImportForSeason() 
		{
			this.divImportForSeason.Visible = (this.CatalogInfo.Season == "Y");
		}

		private void ImportProductContracts() 
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductType, this.MessageManager);
			DataGridItemCollection dgiSelected = this.ctrlProductContractSearchControl.SelectedItems;

			foreach(DataGridItem item in dgiSelected) 
			{
				productContractBusiness.Import(ctrlProductContractSearchControl.GetProductContractID(item), ctrlProductContractSearchControl.GetProductInstance(item), this.ctrlProductContractSearchControl.GetProductType(item), this.CatalogSectionInfo.CatalogSectionID, GetImportForSeason(), this.UserID, ctrlProductContractSearchControl.GetProductCode(item));
			}

			this.Page.RegisterStartupScript("ConfirmReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.SetDataBind(); window.opener.Refresh(); </script>");
		}

		private void ImportProductContractsList() 
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductType, this.MessageManager);

			productContractBusiness.ImportList(this.CatalogSectionInfo.CatalogSectionID, this.CatalogInfo.Type, this.CatalogSectionInfo.Type, 0, this.ctrlProductContractSearchControl.ProductCodeSearch, this.ctrlProductContractSearchControl.RemitCodeSearch, this.ctrlProductContractSearchControl.ProductNameSearch, this.ctrlProductContractSearchControl.YearSearch, this.ctrlProductContractSearchControl.SeasonSearch, this.ctrlProductContractSearchControl.ProductStatusSearch, this.ctrlProductContractSearchControl.ProductTypeSearch, this.ctrlProductContractSearchControl.NumberOfIssuesSearch, this.ctrlProductContractSearchControl.OracleCodeSearch, this.ctrlProductContractSearchControl.CatalogCodeSearch, this.ctrlProductContractSearchControl.PublisherSearch, this.ctrlProductContractSearchControl.FulfillmentHouseSearch, GetImportForSeason(), this.UserID);
			this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.SetDataBind(); window.opener.Refresh(); self.close(); </script>");
		}

		private string GetImportForSeason() 
		{
			string importForSeason = String.Empty;

			if(this.CatalogInfo.Season == "Y") 
			{
				importForSeason = this.rblImportForSeason.SelectedValue;
			}

			return importForSeason;
		}

		private void LoadProductContractSearchControl() 
		{
			string path = ProductContractSearchControlFactory.Instance.GetProductContractSearchControlPath(ProductType);

			ctrlProductContractSearchControl = (ProductContractSearchControl) LoadControl(CONTROL_PATH + path);
			ctrlProductContractSearchControl.ID = "ctrlProductContractSearchControl";
			ctrlProductContractSearchControl.ProductType = ProductType;
			ctrlProductContractSearchControl.ShowSearch = true;
			ctrlProductContractSearchControl.ShowCheckBoxes = true;
			ctrlProductContractSearchControl.ShowProductCodeEdit = true;
			ctrlProductContractSearchControl.ShowSelect = false;
			ctrlProductContractSearchControl.ShowDelete = false;
			ctrlProductContractSearchControl.ShowModify = false;
			ctrlProductContractSearchControl.ShowYear = true;
			ctrlProductContractSearchControl.ShowSeason = true;
			ctrlProductContractSearchControl.ShowProductStatus = false;
			ctrlProductContractSearchControl.ShowCatalogInformation = true;
			ctrlProductContractSearchControl.ProductTypeEnabled = true;
			ctrlProductContractSearchControl.PageSize = PAGE_SIZE;

			this.plhProductContractSearchControl.Controls.Add(ctrlProductContractSearchControl);
		}

		private void SwitchProductType() 
		{
			ProductContractSearchControl oldProductContractSearchControl = ctrlProductContractSearchControl;
			LoadProductContractSearchControl();

			this.ctrlProductContractSearchControl.DataBindInitialData();
			oldProductContractSearchControl.CopyTo(ctrlProductContractSearchControl);
			this.ctrlProductContractSearchControl.DataBind();

			this.plhProductContractSearchControl.Controls.Remove(oldProductContractSearchControl);
		}
	}
}
