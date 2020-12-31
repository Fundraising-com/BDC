namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CommonWeb.UC;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class DefaultSingleContractMaintenanceControl : ProductContractMaintenanceControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectProductContractClickedArgs args;

			try 
			{
				SaveProductContractInformations();

				args = new SelectProductContractClickedArgs(ProductContractID);

				OnProductContractSaved(args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				OnProductContractCancelled(EventArgs.Empty);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public override ProductType ProductType
		{
			get
			{
				ProductType productType = ProductType.Magazine;

				if(ViewState["ProductType"] != null) 
				{
					productType = (ProductType) ViewState["ProductType"];
				}

				return productType;
			}
			set 
			{
				ViewState["ProductType"] = value;

                if (value == ProductType.ToRememberThis)
                {
                    LabelVendorProductCode.Attributes.Add("style", "display: ");
                    lblLabelVendorProductCodeValue.Attributes.Add("style", "display: ");
                    lblAddlHandlingFee.Attributes.Add("style", "display: ");
                    tbxAddlHandlingFee.Attributes.Add("style", "display: ");
                    LabelInternetApproval.Attributes.Add("style", "display: ");
                    rblInternetApproval.Attributes.Add("style", "display: ");
                }
                else if (value == ProductType.Entertainment || value == ProductType.CookieDough || value == ProductType.Gift || value == ProductType.Jewelry || value == ProductType.GiftCard)
                {
                    LabelVendorProductCode.Attributes.Add("style", "display: none");
                    lblLabelVendorProductCodeValue.Attributes.Add("style", "display: none");
                    lblAddlHandlingFee.Attributes.Add("style", "display: none");
                    tbxAddlHandlingFee.Attributes.Add("style", "display: none");
                    LabelInternetApproval.Attributes.Add("style", "display: ");
                    rblInternetApproval.Attributes.Add("style", "display: ");
                }
                else
                {
                    LabelVendorProductCode.Attributes.Add("style", "display: none");
                    lblLabelVendorProductCodeValue.Attributes.Add("style", "display: none");
                    lblAddlHandlingFee.Attributes.Add("style", "display: none");
                    tbxAddlHandlingFee.Attributes.Add("style", "display: none");
                    LabelInternetApproval.Attributes.Add("style", "display: none");
                    rblInternetApproval.Attributes.Add("style", "display: none");
                }
			}
		}

		#region Fields

		protected override string ProductCodeDisplay
		{
			get
			{
				return this.lblProductCode.Text;
			}
			set
			{
				this.lblProductCode.Text = value;
			}
		}

		protected override string ProductNameDisplay
		{
			get
			{
				return this.lblProductName.Text;
			}
			set
			{
				this.lblProductName.Text = value;
			}
		}

		protected override int YearDisplay
		{
			get
			{
				int year = 0;

				try 
				{
					year = Convert.ToInt32(this.lblYear.Text);
				} 
				catch { }

				return year;
			}
			set
			{
				this.lblYear.Text = value.ToString();
			}
		}

		protected override string SeasonDisplay
		{
			get
			{
				return this.lblSeason.Text;
			}
			set
			{
				this.lblSeason.Text = value;
			}
		}

		protected override int Status
		{
			get
			{
				return Convert.ToInt32(this.rblContractStatus.SelectedValue);
			}
			set
			{
				this.rblContractStatus.SelectedIndex = this.rblContractStatus.Items.IndexOf(this.rblContractStatus.Items.FindByValue(value.ToString()));
			}
		}

		protected override DateTime EffectiveDate
		{
			get
			{
				DateTime effectiveDate = new DateTime(1995, 1, 1);

				if(ViewState["EffectiveDate"] != null) 
				{
					effectiveDate = Convert.ToDateTime(ViewState["EffectiveDate"]);
				}

				return effectiveDate;
			}
			set
			{
				ViewState["EffectiveDate"] = value;
			}
		}

		protected override DateTime EndDate
		{
			get
			{
				DateTime endDate = new DateTime(1995, 1, 1);

				if(ViewState["EndDate"] != null) 
				{
					endDate = Convert.ToDateTime(ViewState["EndDate"]);
				}

				return endDate;
			}
			set
			{
				ViewState["EndDate"] = value;
			}
		}

		protected override DateTime DateSubmitted
		{
			get
			{
				DateTime dateSubmitted = new DateTime(1995, 1, 1);

				if(ViewState["DateSubmitted"] != null) 
				{
					dateSubmitted = Convert.ToDateTime(ViewState["DateSubmitted"]);
				}

				return dateSubmitted;
			}
			set
			{
				ViewState["DateSubmitted"] = value;
			}
		}

		protected override double RemitRate
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double ConversionRate
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double NewsStandPrice
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int ListingLevelID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string ListingCopyText
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int AdInCatalog
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int AdPageSizeID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int AdPaymentCurrency
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double AdCost
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string Comment
		{
			get
			{
				return this.tbxComment.Text;
			}
			set
			{
				this.tbxComment.Text = value;
			}
		}

		protected override int EffortKeyRequired
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string EffortKey
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int NumberOfIssues
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double BasePrice
		{
			get
			{
				return 0;
			}
			set { }
		}
		protected override double BasePriceSansPostage 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double BaseRemitRate
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double QSPPriceGST
		{
			get
			{
				return this.tbxCatalogPrice.Value;
			}
			set
			{
				this.tbxCatalogPrice.Value = value;
			}
		}

		protected override double QSPPriceHST
		{
			get
			{
				return this.tbxCatalogPrice.Value;
			}
			set { }
		}

		protected override int InternetApproval
		{
            get
            {
                return Convert.ToBoolean(this.rblInternetApproval.SelectedValue) ? 1 : 0;
            }
            set
            {
                if (ProductInstance != 0)
                {
                    if (ProductType == ProductType.ToRememberThis)
                    {
                        this.rblInternetApproval.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.rblInternetApproval.SelectedIndex = this.rblInternetApproval.Items.IndexOf(this.rblInternetApproval.Items.FindByValue(((bool) (value == 1)).ToString()));
                }
            }
		}

		protected override string ABCCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int QSPPremiumID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string OracleCode
		{
			get
			{
				return this.lblOracleCode.Text;
			}
			set
			{
				this.lblOracleCode.Text = value;
			}
		}

        protected override string VendorProductCode
        {
            get
            {
                return this.lblLabelVendorProductCodeValue.Text;
            }
            set
            {
                this.lblLabelVendorProductCodeValue.Text = value;
            }
        }

		protected override int FSApplicabilityId
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int FSDistributionLevelID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int FSExtraLimitRate
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override bool FSIsBrochure
		{
			get
			{
				return false;
			}
			set { }
		}

		protected override string FSCatalogProductCode
		{
			get
			{
				return ProductCodeDisplay;
			}
			set { }
		}

		protected override string FSContentCatalogCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int FSProgramID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int TaxRegionID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string PremiumIndicator
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PremiumCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PremiumCopy
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string FSProvinceCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override int ContractFormReceived 
		{
			get
			{
				return 0;
			}
			set { }
		}
		protected override string MagazineCoverFilename 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override string CatalogAdFilename 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override int CatalogPageNumber 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		
		protected override int PlacementLevel 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		
		protected override string ContractComments 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override string PrinterComments 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

        protected override double AddlHandlingFee
        {
            get
            {
                return this.tbxAddlHandlingFee.Value;
            }
            set
            {
                this.tbxAddlHandlingFee.Value = value;
            }
        }

		protected override double PostageRemitRate 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double PostageAmount 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override string Currency
		{
			get
			{
				return String.Empty;
			}
			set
			{
				
			}
		}
		#endregion
	}
}
