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
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.MarketingMgt.Control;
using QSP.WebControl;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class ProductContractMaintenance : MarketingMgtPage, IOnloadJSEvent
	{
		protected QSPFulfillment.MarketingMgt.Control.ProductContractMaintenanceControl ctrlProductContractMaintenanceControl;

		private void ProductContractMaintenance_Init(object sender, EventArgs e)
		{
			try 
			{
				LoadProductContractMaintenanceControl();

				this.ctrlProductContractMaintenanceControl.ProductContractSaved += new SelectProductContractEventHandler(ctrlProductContractMaintenanceControl_ProductContractSaved);
				this.ctrlProductContractMaintenanceControl.ProductContractCancelled += new EventHandler(ctrlProductContractMaintenanceControl_ProductContractCancelled);
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}
	
		protected void ProductContractMaintenance_PreRender(object sender, EventArgs e)
		{
			ProductContractType productContractType;

			if(!IsPostBack) 
			{
				try 
				{
					if(this.MagPriceInstanceGST != ProductContractID.EmptyValue) 
					{
						productContractType = ProductContractTypeFactory.Instance.GetProductContractType(ProductType);

						this.ctrlProductContractMaintenanceControl.ProductContractID = ProductContractIDFactory.Instance.GetProductContractID(this.MagPriceInstanceGST, this.MagPriceInstanceHST, productContractType);
						this.ctrlProductContractMaintenanceControl.DataBind();
					} 
					else if(this.ProductInstance != 0) 
					{
						this.ctrlProductContractMaintenanceControl.ProductInstance = this.ProductInstance;
						this.ctrlProductContractMaintenanceControl.DataBind();
					}

					this.onload_script += "; window_onunload();";
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
			this.Init += new EventHandler(ProductContractMaintenance_Init);
			this.PreRender += new System.EventHandler(this.ProductContractMaintenance_PreRender);

		}
		#endregion

		private void ctrlProductContractMaintenanceControl_ProductContractSaved(object sender, SelectProductContractClickedArgs e)
		{
			this.RegisterStartupScript("ConfirmCloseReload", "<script language=\"javascript\"> window.opener.pleasewait(); window.opener.SetDataBind(); window.opener.Refresh(); self.close(); </script>");
		}

		private void ctrlProductContractMaintenanceControl_ProductContractCancelled(object sender, EventArgs e)
		{
			this.RegisterStartupScript("ConfirmClose", "<script language=\"javascript\"> self.close(); </script>");
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

		private int MagPriceInstanceGST
		{
			get 
			{
				int magPriceInstanceGST = 0;

				if(Request.QueryString["MagPriceInstanceGST"] != null) 
				{
					magPriceInstanceGST = Convert.ToInt32(Request.QueryString["MagPriceInstanceGST"]);
				}

				return magPriceInstanceGST;
			}
		}

		private int MagPriceInstanceHST
		{
			get 
			{
				int magPriceInstanceHST = 0;

				if(Request.QueryString["MagPriceInstanceHST"] != null) 
				{
					magPriceInstanceHST = Convert.ToInt32(Request.QueryString["MagPriceInstanceHST"]);
				}

				return magPriceInstanceHST;
			}
		}

		private int ProductInstance
		{
			get 
			{
				int productInstance = 0;

				if(Request.QueryString["ProductInstance"] != null) 
				{
					productInstance = Convert.ToInt32(Request.QueryString["ProductInstance"]);
				}

				return productInstance;
			}
		}

		private ProductType ProductType 
		{
			get 
			{
				ProductType productType = ProductType.Magazine;

				if(Request.QueryString["ProductType"] != null) 
				{
					productType = (ProductType) Convert.ToInt32(Request.QueryString["ProductType"]);
				}

				return productType;
			}
		}

		private void LoadProductContractMaintenanceControl() 
		{
			string path = Control.ProductContractMaintenanceControlFactory.Instance.GetProductContractMaintenanceControlPath(this.ProductType);

			ctrlProductContractMaintenanceControl = (ProductContractMaintenanceControl) LoadControl(path);
			ctrlProductContractMaintenanceControl.ID = "ctrlProductContractMaintenanceControl";

			this.plhProductContractMaintenanceControl.Controls.Clear();
			this.plhProductContractMaintenanceControl.Controls.Add(ctrlProductContractMaintenanceControl);

			ctrlProductContractMaintenanceControl.ProductType = ProductType;
		}
	}
}
