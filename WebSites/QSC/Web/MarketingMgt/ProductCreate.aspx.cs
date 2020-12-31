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
	public partial class ProductCreate : MarketingMgtPage, IOnloadJSEvent
	{
		protected ProductMaintenanceControl ctrlProductMaintenanceControl;
			
		private void ProductCreate_Init(object sender, EventArgs e)
		{
			try 
			{
				LoadProductMaintenanceControl();

				ctrlProductMaintenanceControl.ProductSaved += new EventHandler(ctrlProductMaintenanceControl_ProductSaved);
				ctrlProductMaintenanceControl.ProductCancelled += new EventHandler(ctrlProductMaintenanceControl_ProductCancelled);
				ctrlProductMaintenanceControl.ProductTypeChanged += new ProductTypeChangedEventHandler(ctrlProductMaintenanceControl_ProductTypeChanged);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected void ProductCreate_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!IsPostBack) 
				{
					this.ctrlProductMaintenanceControl.DataBind();
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
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Init += new EventHandler(ProductCreate_Init);
			this.PreRender += new System.EventHandler(this.ProductCreate_PreRender);

		}
		#endregion

		private void ctrlProductMaintenanceControl_ProductSaved(object sender, EventArgs e)
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  window.opener.SetProductInformation(\"" + this.ctrlProductMaintenanceControl.ProductInstance.ToString() + "\", \"" + ((int) this.ctrlProductMaintenanceControl.ProductType).ToString() + "\");\n";
			script += "  window.opener.pleasewait();\n";
			script += "  window.opener.Refresh();\n";
			script += "  self.close();\n";
			script += "</script>";

			this.Page.RegisterStartupScript("ConfirmCloseReload", script);
		}

		private void ctrlProductMaintenanceControl_ProductCancelled(object sender, EventArgs e)
		{
			this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> self.close(); </script>");
		}

		private void ctrlProductMaintenanceControl_ProductTypeChanged(object sender, ProductTypeChangedArgs e)
		{
			this.ProductType = e.ProductType;
			SwitchProductType();
		}

		private ProductType ProductType 
		{
			get 
			{
				if(this.ViewState["ProductType"] == null) 
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
				ProductType productTypeDefault = ProductType.Magazine;

				if(Request.QueryString["ProductType"] != null) 
				{
					productTypeDefault = (ProductType) Convert.ToInt32(Request.QueryString["ProductType"]);
				}

				return productTypeDefault;
			}
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

		private void LoadProductMaintenanceControl() 
		{
			string path = Control.ProductMaintenanceControlFactory.Instance.GetProductMaintenanceControlPath(this.ProductType);

			ctrlProductMaintenanceControl = (ProductMaintenanceControl) LoadControl(path);
			ctrlProductMaintenanceControl.ID = "ctrlProductMaintenanceControl";

			this.plhProductMaintenanceControl.Controls.Add(ctrlProductMaintenanceControl);

			ctrlProductMaintenanceControl.ProductType = ProductType;

		}

		private void SwitchProductType() 
		{
			ProductMaintenanceControl oldProductMaintenanceControl = ctrlProductMaintenanceControl;
			LoadProductMaintenanceControl();
			
			this.ctrlProductMaintenanceControl.DataBind();
			oldProductMaintenanceControl.CopyTo(ctrlProductMaintenanceControl);

			this.plhProductMaintenanceControl.Controls.Remove(oldProductMaintenanceControl);
		}
	}
}
