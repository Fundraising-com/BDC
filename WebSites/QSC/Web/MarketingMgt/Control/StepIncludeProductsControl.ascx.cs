namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for StepCatalogInformationsControl.
	/// </summary>
	public partial class StepIncludeProductsControl : CatalogMaintenanceStepControl
	{
		private const int PAGE_SIZE = 10;

		protected QSPFulfillment.MarketingMgt.Control.ProductContractSearchControl ctrlProductContractSearchControl;

		public override void Initialize()
		{
			try 
			{
				LoadProductContractSearchControl();

				this.ctrlProductContractSearchControl.ProductContractDeleted += new SelectProductContractEventHandler(ctrlProductContractSearchControl_ProductContractDeleted);
				this.ctrlProductContractSearchControl.ProductTypeChanged += new ProductTypeChangedEventHandler(ctrlProductContractSearchControl_ProductTypeChanged);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void StepIncludeProductsControl_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(IsFirstLoad || IsDataRefresh) 
				{
					DataBind();
				}

				AddJavaScript();
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
			this.StepControl = Step.IncludeProducts;
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.StepIncludeProductsControl_PreRender);
		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			StepCompletedArgs args;
			
			try 
			{
				args = new StepCompletedArgs(this.StepControl);
				
				OnStepCompleted(this, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlProductContractSearchControl_ProductContractDeleted(object sender, SelectProductContractClickedArgs e)
		{
			ctrlProductContractSearchControl.DataBind();
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
				if(ViewState["ProductType"] == null) 
				{
					ViewState["ProductType"] = ProductTypeDefault;
				}

				return (ProductType) ViewState["ProductType"];
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
				return this.Page.BusCatalogSection.SelectDefaultProductTypeByCatalogSection(this.Page.CatalogSectionInfo.CatalogSectionID);
			}
		}

		private bool IsFirstLoad 
		{
			get 
			{
				bool isFirstLoad = true;

				if(ViewState["IsFirstLoad"] != null) 
				{
					isFirstLoad = Convert.ToBoolean(ViewState["IsFirstLoad"]);
				}

				return isFirstLoad;
			}
			set 
			{
				ViewState["IsFirstLoad"] = value;
			}
		}

		private bool IsDataRefresh 
		{
			get 
			{
				bool isDataRefresh = false;

				try 
				{
					isDataRefresh = Convert.ToBoolean(hidDataBind.Value);
				} 
				catch { }

				return isDataRefresh;
			}
			set 
			{
				hidDataBind.Value = value.ToString();
			}
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			AddJavaScriptSetProductInformation();
			AddJavaScriptSetDataBind();
			AddJavaScriptEditNewContract();
			AddJavaScriptCreateProduct();
			AddJavaScriptImportProducts();
		}

		private void AddJavaScriptSetProductInformation() 
		{
			string script = String.Empty;

			script  = "<script language=\"javascript\">\n";
			script += "  function SetProductInformation(productInstance, productType) {\n";
			script += "    document.getElementById(\"" + this.hidProductInstance.ClientID + "\").value = productInstance;\n";
			script += "    document.getElementById(\"" + this.hidProductType.ClientID + "\").value = productType;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetProductInformation", script);
		}

		private void AddJavaScriptSetDataBind() 
		{
			string script = String.Empty;

			script  = "<script language=\"javascript\">\n";
			script += "  function SetDataBind() {\n";
			script += "    document.getElementById(\"" + this.hidDataBind.ClientID + "\").value = \"true\";\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetDataBind", script);
		}

		private void AddJavaScriptEditNewContract() 
		{
			string script = String.Empty;

			if(this.hidProductInstance.Value != "0" && this.hidProductType.Value != "0") 
			{
				script  = "<script language=\"javascript\">\n";
				script += "  OpenCustom('ProductContractMaintenance.aspx?IsNewWindow=true&ProductInstance=" + this.hidProductInstance.Value + "&ProductType=" + this.hidProductType.Value + "', 650, 718);\n";
				script += "</script>\n";

				this.Page.RegisterClientScriptBlock("EditNewContract", script);

				this.hidProductInstance.Value = "0";
				this.hidProductType.Value = "0";
			}
		}

		private void AddJavaScriptCreateProduct() 
		{
			this.btnCreateProduct.Attributes["onClick"] = "OpenCustom('ProductCreate.aspx?IsNewWindow=true&ProductType=" + ((int) ProductType).ToString() + "', 650, 718);";
		}

		private void AddJavaScriptImportProducts() 
		{
			this.btnImport.Attributes["onClick"] = "OpenBig('ImportProducts.aspx?IsNewWindow=true&ProductType=" + ((int) ProductType).ToString() + "');";
		}

		#endregion

		public override void DataBind()
		{
			ctrlProductContractSearchControl.DataBindInitialData();
			ctrlProductContractSearchControl.DataBind();
			IsFirstLoad = false;
			IsDataRefresh = false;
		}

		private void LoadProductContractSearchControl() 
		{
			string path = ProductContractSearchControlFactory.Instance.GetProductContractSearchControlPath(ProductType);

			ctrlProductContractSearchControl = (ProductContractSearchControl) LoadControl(path);
			ctrlProductContractSearchControl.ID = "ctrlProductContractSearchControl";

			this.plhProductContractSearchControl.Controls.Add(ctrlProductContractSearchControl);

			ctrlProductContractSearchControl.ProductType = ProductType;
			ctrlProductContractSearchControl.ProgramSectionID = this.Page.CatalogSectionInfo.CatalogSectionID;
			ctrlProductContractSearchControl.ShowSearch = true;
			ctrlProductContractSearchControl.ShowCheckBoxes = false;
			ctrlProductContractSearchControl.ShowSelect = false;
			ctrlProductContractSearchControl.ShowDelete = true;
			ctrlProductContractSearchControl.ShowModify = true;
			ctrlProductContractSearchControl.ShowYear = true;
			ctrlProductContractSearchControl.ShowSeason = true;
			ctrlProductContractSearchControl.ShowProductStatus = true;
			ctrlProductContractSearchControl.ShowCatalogInformation = false;
			ctrlProductContractSearchControl.ProductTypeEnabled = true;
			ctrlProductContractSearchControl.PageSize = PAGE_SIZE;
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
