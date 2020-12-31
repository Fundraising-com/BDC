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
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.MarketingMgt.Control;
using QSP.WebControl;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class ProductMaintenance : MarketingMgtPage, IOnloadJSEvent
	{
		protected ProductSearchControl ctrlProductSearchControl;
		protected ProductMaintenanceControl ctrlProductMaintenanceControl;

		private void ProductMaintenance_PreLoad(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					LoadProductSearchControl();

					this.ctrlProductSearchControl.EditProductClick += new SelectProductEventHandler(ctrlProductSearchControl_EditProductClick);
					this.ctrlProductSearchControl.DeleteProductClick += new SelectProductEventHandler(ctrlProductSearchControl_DeleteProductClick);
					this.ctrlProductSearchControl.ProductTypeChanged += new ProductTypeChangedEventHandler(ctrlProductSearchControl_ProductTypeChanged);
				} 
				else 
				{
					LoadProductMaintenanceControl();

					this.ctrlProductMaintenanceControl.ProductSaved += new EventHandler(ctrlProductMaintenanceControl_ProductSaved);
					this.ctrlProductMaintenanceControl.ProductCancelled += new EventHandler(ctrlProductMaintenanceControl_ProductCancelled);
					this.ctrlProductMaintenanceControl.ProductTypeChanged += new ProductTypeChangedEventHandler(ctrlProductMaintenanceControl_ProductTypeChanged);
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
	
		protected void ProductMaintenance_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					this.ctrlProductSearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					this.lblInstructions.Visible = false;
					this.btnCreateNew.Visible = false;
				}

				this.onload_script += "; window_onunload();";
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
		
 		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			//this.hidDataBind.ServerChange +=new EventHandler(hidDataBind_ServerChange);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreLoad += new EventHandler(ProductMaintenance_PreLoad);
			this.PreRender += new System.EventHandler(this.ProductMaintenance_PreRender);

		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				LoadProductMaintenanceControl();

				this.ctrlProductMaintenanceControl.ProductInstance = 0;
				this.ctrlProductMaintenanceControl.DataBind();
				this.EditMode = true;
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlProductSearchControl_EditProductClick(object sender, SelectProductClickedArgs e)
		{
			ProductTypeSearch = ProductType;
			ProductType = (ProductType) e.ProductInfo.ProductType;
			LoadProductMaintenanceControl();

			this.ctrlProductMaintenanceControl.ProductInstance = e.ProductInfo.ProductInstance;
			this.ctrlProductMaintenanceControl.DataBind();
			this.EditMode = true;
		}

		private void ctrlProductSearchControl_DeleteProductClick(object sender, SelectProductClickedArgs e)
		{
			this.ctrlProductSearchControl.DataBind();
		}

		private void ctrlProductMaintenanceControl_ProductSaved(object sender, EventArgs e)
		{
			ProductType = ProductTypeSearch;
			LoadProductSearchControl();
			this.EditMode = false;
		}

		private void ctrlProductMaintenanceControl_ProductCancelled(object sender, EventArgs e)
		{
			ProductType = ProductTypeSearch;
			LoadProductSearchControl();
			this.EditMode = false;
		}

		private void ctrlProductSearchControl_ProductTypeChanged(object sender, ProductTypeChangedArgs e)
		{
			this.ProductType = e.ProductType;
			this.ProductTypeSearch = e.ProductType;
			SwitchProductSearchType();
		}

		private void ctrlProductMaintenanceControl_ProductTypeChanged(object sender, ProductTypeChangedArgs e)
		{
			this.ProductType = e.ProductType;
			SwitchProductMaintenanceType();
		}

		public string onload_script 
		{
			get 
			{
				if(BodyTag.Attributes["onload"] == null)
					BodyTag.Attributes["onload"] = "";

				return BodyTag.Attributes["onload"];
			}
			set 
			{
				BodyTag.Attributes["onload"] = value;
			}
		}

		private bool EditMode 
		{
			get 
			{
				if(this.ViewState["EditMode"] == null)
					this.ViewState["EditMode"] = false;

				return Convert.ToBoolean(this.ViewState["EditMode"]);
			}
			set 
			{
				this.ViewState["EditMode"] = value;
			}
		}

		private ProductType ProductType 
		{
			get 
			{
				if(ViewState["ProductType"] == null) 
				{
					ViewState["ProductType"] = ProductType.None;
				}

				return (ProductType) ViewState["ProductType"];
			}
			set 
			{
				ViewState["ProductType"] = value;
			}
		}

		private ProductType ProductTypeSearch 
		{
			get 
			{
				if(ViewState["ProductTypeSearch"] == null) 
				{
					ViewState["ProductTypeSearch"] = ProductType;
				}

				return (ProductType) ViewState["ProductTypeSearch"];
			}
			set 
			{
				ViewState["ProductTypeSearch"] = value;
			}
		}

		private void LoadProductSearchControl() 
		{
			string path = Control.ProductSearchControlFactory.Instance.GetProductSearchControlPath(ProductType);

			ctrlProductSearchControl = (ProductSearchControl) LoadControl(path);
			ctrlProductSearchControl.ID = "ctrlProductSearchControl";

			if(ctrlProductMaintenanceControl != null) 
			{
				this.plhProductControl.Controls.Remove(ctrlProductMaintenanceControl);
			}
			this.plhProductControl.Controls.Add(ctrlProductSearchControl);

			ctrlProductSearchControl.ProductType = ProductType;
			ctrlProductSearchControl.ShowSearch = true;
			ctrlProductSearchControl.ShowSelect = false;
			ctrlProductSearchControl.ShowEdit = true;
		}

		private void LoadProductMaintenanceControl() 
		{
			string path = Control.ProductMaintenanceControlFactory.Instance.GetProductMaintenanceControlPath(ProductType);

			ctrlProductMaintenanceControl = (ProductMaintenanceControl) LoadControl(path);
			ctrlProductMaintenanceControl.ID = "ctrlProductMaintenanceControl";

			if(ctrlProductSearchControl != null) 
			{
				this.plhProductControl.Controls.Remove(ctrlProductSearchControl);
			}
			this.plhProductControl.Controls.Add(ctrlProductMaintenanceControl);

			ctrlProductMaintenanceControl.ProductType = ProductType;
		}

		private void SwitchProductSearchType() 
		{
			ProductSearchControl oldProductSearchControl = ctrlProductSearchControl;
			LoadProductSearchControl();
			
			ctrlProductSearchControl.DataBindInitialData();
			oldProductSearchControl.CopyTo(ctrlProductSearchControl);
			this.plhProductControl.Controls.Remove(oldProductSearchControl);
		}

		private void SwitchProductMaintenanceType() 
		{
			ProductMaintenanceControl oldProductMaintenanceControl = ctrlProductMaintenanceControl;
			LoadProductMaintenanceControl();

			this.ctrlProductMaintenanceControl.DataBind();
			oldProductMaintenanceControl.CopyTo(ctrlProductMaintenanceControl);

			this.plhProductControl.Controls.Remove(oldProductMaintenanceControl);
		}
	}
}
