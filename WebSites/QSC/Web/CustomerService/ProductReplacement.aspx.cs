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
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.TableDef;
using QSPFulfillment.DataAccess.Common;
using QSP.WebControl;

namespace QSPFulfillment.CustomerService
{
	[Serializable]
	public enum ProductReplacementStep 
	{
		SelectCampaign,
		SelectProducts,
		ConfirmOrder
	}

	/// <summary>
	/// Summary description for GiftReplacement.
	/// </summary>
	public partial class ProductReplacement : CustomerServicePage, IOnloadJSEvent
	{
		protected ControlerCampaignsForProductReplacement ctrlControlerCampaignsForProductReplacement;
		protected ControlerProductMultiSelect ctrlControlerProductMultiSelect;
		protected ControlerProductReplacement ctrlControlerProductReplacement;
		protected System.Web.UI.WebControls.Button btnSelectProductList;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				if(!IsPostBack) 
				{
					SetValueProductType();
					SetValueStepSelectCampaign();
				} 
				else if(CurrentStep == ProductReplacementStep.ConfirmOrder) 
				{
					this.ctrlControlerProductReplacement.CurrentBatch = this.CurrentBatch;
				}

				SetValueEmptyConfirmation();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		protected void ProductReplacement_PreRender(object sender, EventArgs e)
		{
			try 
			{
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
			this.ctrlControlerCampaignsForProductReplacement.SelectCampaignClick += new SelectCampaignEventHandler(ctrlControlerCampaignsForProductReplacement_SelectCampaignClick);
			this.ctrlControlerProductMultiSelect.StepBackClicked += new EventHandler(ctrlControlerProductMultiSelect_StepBackClicked);
			this.ctrlControlerProductMultiSelect.ProductItemsSelected += new SelectProductItemsEventHandler(ctrlControlerProductMultiSelect_ProductItemsSelected);
			this.ctrlControlerProductReplacement.NextStudentClicked += new EventHandler(ctrlControlerProductReplacement_NextStudentClicked);
			this.ctrlControlerProductReplacement.EditProductsClicked += new OrderHeaderEventHandler(ctrlControlerProductReplacement_EditProductsClicked);
			this.ctrlControlerProductReplacement.ConfirmedClicked += new EventHandler(ctrlControlerProductReplacement_ConfirmedClicked);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new System.EventHandler(this.ProductReplacement_PreRender);

		}
		#endregion

		private void ctrlControlerCampaignsForProductReplacement_SelectCampaignClick(object sender, Campaign campaign)
		{
			StartNewBatch();
			this.CurrentBatch.Campaign = campaign;

			SetValueStepSelectProducts();
		}

		private void ctrlControlerProductMultiSelect_StepBackClicked(object sender, EventArgs e)
		{
			SetValueStepSelectCampaign();
		}

		private void ctrlControlerProductMultiSelect_ProductItemsSelected(object sender, OrderHeader orderHeader)
		{
			if(!this.CurrentBatch.OrderHeaders.Contains(orderHeader)) 
			{
				this.CurrentBatch.OrderHeaders.Add(orderHeader);
			} 
			else 
			{
				this.CurrentBatch.OrderHeaders[this.CurrentBatch.OrderHeaders.IndexOf(orderHeader)] = orderHeader;
			}

			SetValueStepConfirmOrder();
		}

		private void ctrlControlerProductReplacement_NextStudentClicked(object sender, EventArgs e)
		{
			this.ctrlControlerProductMultiSelect.CurrentOrderHeader = new OrderHeader();

			SetValueStepSelectProducts();
		}

		private void ctrlControlerProductReplacement_EditProductsClicked(object sender, OrderHeader orderHeader)
		{
			this.ctrlControlerProductMultiSelect.CurrentOrderHeader = orderHeader;

			SetValueStepSelectProducts();
		}

		private void ctrlControlerProductReplacement_ConfirmedClicked(object sender, EventArgs e)
		{
			SaveCurrentBatch();
			SetValueConfirmation();

			StartNewBatch();
			SetValueStepSelectCampaign();
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

		private ProductReplacementStep CurrentStep 
		{
			get 
			{
				if(this.ViewState["CurrentStep"] == null)
					this.ViewState["CurrentStep"] = ProductReplacementStep.SelectCampaign;

				return (ProductReplacementStep) this.ViewState["CurrentStep"];
			}
			set 
			{
				this.ViewState["CurrentStep"] = value;
			}
		}

		private ProductReplacementType ProductType
		{
			get 
			{
				return ProductReplacementTypeFactory.Instance.GetProductReplacementType(Request.QueryString["ProductType"]);
			}
		}

		private Batch CurrentBatch 
		{
			get 
			{
				return (Batch) ViewState["Batch"];
			}
			set 
			{
				ViewState["Batch"] = value;
			}
		}

		private void StartNewBatch() 
		{
			this.CurrentBatch = new Batch();
			this.CurrentBatch.OrderQualifierID = OrderQualifierFactory.Instance.GetOrderQualifierForProductReplacement(this.ProductType);

			this.ctrlControlerProductMultiSelect.CurrentOrderHeader = new OrderHeader();
		}

		private void SetValueStepSelectCampaign()
		{
			this.CurrentStep = ProductReplacementStep.SelectCampaign;

			this.ctrlControlerCampaignsForProductReplacement.Visible = true;
			this.ctrlControlerProductMultiSelect.Visible = false;
			this.ctrlControlerProductReplacement.Visible = false;

			if(IsPostBack) 
			{
				this.ctrlControlerCampaignsForProductReplacement.DataBind();
			}

			this.lblInstructions.Text = "Please select a campaign for which you wish to replace product(s).";
		}

		private void SetValueStepSelectProducts() 
		{
			this.CurrentStep = ProductReplacementStep.SelectProducts;

			this.ctrlControlerCampaignsForProductReplacement.Visible = false;
			this.ctrlControlerProductMultiSelect.Visible = true;
			this.ctrlControlerProductReplacement.Visible = false;

			this.ctrlControlerProductMultiSelect.CampaignID = this.CurrentBatch.Campaign.CampaignID;
			this.ctrlControlerProductMultiSelect.DataBind();

			this.lblInstructions.Text = "Please select the product(s) you wish to replace.";
		}

		private void SetValueStepConfirmOrder() 
		{
			this.CurrentStep = ProductReplacementStep.ConfirmOrder;

			this.ctrlControlerCampaignsForProductReplacement.Visible = false;
			this.ctrlControlerProductMultiSelect.Visible = false;
			this.ctrlControlerProductReplacement.Visible = true;

			this.ctrlControlerProductReplacement.CurrentBatch = CurrentBatch;
			this.ctrlControlerProductReplacement.DataBind();

			this.lblInstructions.Text = "Please fill in the informations for the product replacement.";
		}

		private void SetValueConfirmation() 
		{
			if(this.CurrentBatch != null) 
			{
				this.lblConfirmation.Text = "The " + this.ProductType.ProductTypeName + " Replacement order " + this.CurrentBatch.OrderID.ToString() + " for campaign " + this.CurrentBatch.Campaign.CampaignID.ToString() + " has been sent.";
				this.lblConfirmation.Visible = true;
			}
			else 
			{
				SetValueEmptyConfirmation();
			}
		}

		private void SetValueEmptyConfirmation() 
		{
			this.lblConfirmation.Visible = false;
		}

		private void SetValueProductType() 
		{
			this.lblPageTitle.Text = this.ProductType.ProductTypeName + " Replacement";
			this.ctrlControlerCampaignsForProductReplacement.ProductType = this.ProductType;
			this.ctrlControlerProductMultiSelect.ProductType = this.ProductType.SearchID;
		}

		private int SaveCurrentBatch() 
		{
			return this.BusProduct.ProductReplacement(this.CurrentBatch, QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance);
		}
	}
}
