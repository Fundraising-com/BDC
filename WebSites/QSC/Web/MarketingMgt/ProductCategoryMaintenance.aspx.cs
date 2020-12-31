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
using QSPFulfillment.MarketingMgt.Control;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class ProductCategoryMaintenance : MarketingMgtPage
	{
		protected ProductCategorySearchControl ctrlProductCategorySearchControl;
		protected ProductCategoryMaintenanceControl ctrlProductCategoryMaintenanceControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		protected void ProductCategoryMaintenance_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!this.EditMode) 
				{
					this.ctrlProductCategorySearchControl.DataBind();
					this.lblInstructions.Visible = true;
					this.ctrlProductCategorySearchControl.Visible = true;
					this.ctrlProductCategoryMaintenanceControl.Visible = false;
					this.btnCreateNew.Visible = true;
				} 
				else 
				{
					this.ctrlProductCategoryMaintenanceControl.DataBind();
					this.lblInstructions.Visible = false;
					this.ctrlProductCategorySearchControl.Visible = false;
					this.ctrlProductCategoryMaintenanceControl.Visible = true;
					this.btnCreateNew.Visible = false;
				}
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
			this.ctrlProductCategorySearchControl.SelectProductCategoryClick += new SelectProductCategoryEventHandler(ctrlProductCategorySearchControl_SelectProductCategoryClick);
			this.ctrlProductCategoryMaintenanceControl.SelectProductCategoryClick += new SelectProductCategoryEventHandler(ctrlProductCategoryMaintenanceControl_SelectProductCategoryClick);
			this.ctrlProductCategoryMaintenanceControl.ProductCategoryCancelled += new EventHandler(ctrlProductCategoryMaintenanceControl_ProductCategoryCancelled);
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
			this.PreRender += new System.EventHandler(this.ProductCategoryMaintenance_PreRender);

		}
		#endregion

		protected void btnCreateNew_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlProductCategoryMaintenanceControl.ProductCategoryInfo = null;

				this.EditMode = true;
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlProductCategorySearchControl_SelectProductCategoryClick(object sender, SelectProductCategoryClickedArgs e)
		{
			this.ctrlProductCategoryMaintenanceControl.ProductCategoryInfo = e.ProductCategoryInfo;

			this.EditMode = true;
		}

		private void ctrlProductCategoryMaintenanceControl_SelectProductCategoryClick(object sender, SelectProductCategoryClickedArgs e)
		{
			this.EditMode = false;
		}

		private void ctrlProductCategoryMaintenanceControl_ProductCategoryCancelled(object sender, EventArgs e)
		{
			this.EditMode = false;
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
	}
}
