using System;
using System.Data;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.MarketingMgt.Control
{
	public delegate void ProductTypeChangedEventHandler(object sender, ProductTypeChangedArgs e);

	/// <summary>
	/// Summary description for ProductMaintenanceControl.
	/// </summary>
	public class ProductMaintenanceControl : MarketingMgtControl
	{
		private const string DEFAULT_LANGUAGE = "EN/FR";
		private const int DEFAULT_STATUS = 30600;
		private const int DEFAULT_CURRENCY = 801;

		public event System.EventHandler ProductSaved;
		public event System.EventHandler ProductCancelled;
		public event ProductTypeChangedEventHandler ProductTypeChanged;

		public virtual ProductType ProductType
		{
			get 
			{
				throw new NotImplementedException("ProductType");
			}
			set 
			{
				throw new NotImplementedException("ProductType");
			}
		}

		public int ProductInstance
		{
			get 
			{
				int productInstance = 0;

				if(ViewState["ProductInstance"] != null) 
				{
					productInstance = Convert.ToInt32(ViewState["ProductInstance"]);
				}

				return productInstance;
			}
			set 
			{
				ViewState["ProductInstance"] = value;
			}
		}

		#region Fields

		protected virtual ProductType EnteredProductType 
		{
			get 
			{
				throw new NotImplementedException("ProductType");
			}
			set 
			{
				throw new NotImplementedException("ProductType");
			}
		}

		protected virtual string EnteredProductCode
		{
			get 
			{
				throw new NotImplementedException("ProductCode");
			}
			set 
			{
				throw new NotImplementedException("ProductCode");
			}
		}

		protected virtual string EnteredSeason
		{
			get 
			{
				throw new NotImplementedException("Season");
			}
			set 
			{
				throw new NotImplementedException("Season");
			}
		}

		protected virtual int EnteredYear
		{
			get 
			{
				throw new NotImplementedException("Year");
			}
			set 
			{
				throw new NotImplementedException("Year");
			}
		}

		protected virtual string ProductName
		{
			get 
			{
				throw new NotImplementedException("ProductName");
			}
			set 
			{
				throw new NotImplementedException("ProductName");
			}
		}

		protected virtual string ProductSortName 
		{
			get 
			{
				throw new NotImplementedException("ProductSortName");
			}
			set 
			{
				throw new NotImplementedException("ProductSortName");
			}
		}

		protected virtual string Language
		{
			get 
			{
				throw new NotImplementedException("Language");
			}
			set 
			{
				throw new NotImplementedException("Language");
			}
		}

		protected virtual int CategoryID
		{
			get 
			{
				throw new NotImplementedException("CategoryID");
			}
			set 
			{
				throw new NotImplementedException("CategoryID");
			}
		}

		protected virtual int Status
		{
			get 
			{
				throw new NotImplementedException("Status");
			}
			set 
			{
				throw new NotImplementedException("Status");
			}
		}

		protected virtual int DaysLeadTime
		{
			get 
			{
				throw new NotImplementedException("DaysLeadTime");
			}
			set 
			{
				throw new NotImplementedException("DaysLeadTime");
			}
		}

		protected virtual int NumberOfIssues
		{
			get 
			{
				throw new NotImplementedException("NumberOfIssues");
			}
			set 
			{
				throw new NotImplementedException("NumberOfIssues");
			}
		}

		protected virtual int PublisherID
		{
			get 
			{
				throw new NotImplementedException("PublisherID");
			}
			set 
			{
				throw new NotImplementedException("PublisherID");
			}
		}

		protected virtual int FulfillmentHouseID
		{
			get 
			{
				throw new NotImplementedException("FulfillmentHouseID");
			}
			set 
			{
				throw new NotImplementedException("FulfillmentHouseID");
			}
		}

		protected virtual string Comment
		{
			get 
			{
				throw new NotImplementedException("Comment");
			}
			set 
			{
				throw new NotImplementedException("Comment");
			}
		}

		protected virtual string VendorNumber
		{
			get 
			{
				throw new NotImplementedException("VendorNumber");
			}
			set 
			{
				throw new NotImplementedException("VendorNumber");
			}
		}

		protected virtual string VendorSiteName
		{
			get 
			{
				throw new NotImplementedException("VendorSiteName");
			}
			set 
			{
				throw new NotImplementedException("VendorSiteName");
			}
		}

		protected virtual string PayGroupLookUpCode
		{
			get 
			{
				throw new NotImplementedException("PayGroupLookUpCode");
			}
			set 
			{
				throw new NotImplementedException("PayGroupLookUpCode");
			}
		}

		protected virtual int Currency
		{
			get 
			{
				throw new NotImplementedException("Currency");
			}
			set 
			{
				throw new NotImplementedException("Currency");
			}
		}

		protected virtual string GSTRegistrationNumber
		{
			get 
			{
				throw new NotImplementedException("GSTRegistrationNumber");
			}
			set 
			{
				throw new NotImplementedException("GSTRegistrationNumber");
			}
		}

		protected virtual string HSTRegistrationNumber
		{
			get 
			{
				throw new NotImplementedException("HSTRegistrationNumber");
			}
			set 
			{
				throw new NotImplementedException("HSTRegistrationNumber");
			}
		}

		protected virtual string PSTRegistrationNumber
		{
			get 
			{
				throw new NotImplementedException("PSTRegistrationNumber");
			}
			set 
			{
				throw new NotImplementedException("PSTRegistrationNumber");
			}
		}

		protected virtual string OracleCode 
		{
			get 
			{
				throw new NotImplementedException("OracleCode");
			}
			set 
			{
				throw new NotImplementedException("OracleCode");
			}
		}

		protected virtual string PrizeLevel 
		{
			get 
			{
				throw new NotImplementedException("PrizeLevel");
			}
			set 
			{
				throw new NotImplementedException("PrizeLevel");
			}
		}

		protected virtual int PrizeLevelQuantity 
		{
			get 
			{
				throw new NotImplementedException("PrizeLevelQuantity");
			}
			set
			{
				throw new NotImplementedException("PrizeLevelQuantity");
			}
		}

		protected virtual string RemitCode 
		{
			get 
			{
				throw new NotImplementedException("RemitCode");
			}
			set 
			{
				throw new NotImplementedException("RemitCode");
			}
		}

		protected virtual bool IsQSPExclusive 
		{
			get 
			{
				throw new NotImplementedException("IsQSPExclusive");
			}
			set 
			{
				throw new NotImplementedException("IsQSPExclusive");
			}
		}

		protected virtual string EnglishDescription 
		{
			get 
			{
				throw new NotImplementedException("EnglishDescription");
			}
			set 
			{
				throw new NotImplementedException("EnglishDescription");
			}
		}

		protected virtual string FrenchDescription 
		{
			get 
			{
				throw new NotImplementedException("FrenchDescription");
			}
			set 
			{
				throw new NotImplementedException("FrenchDescription");
			}
		}

        protected virtual string VendorProductCode
        {
            get
            {
                return string.Empty;
            }
            set { }
        }

		#endregion

		#region Events

		protected virtual void OnProductSaved(System.EventArgs e) 
		{
			if(ProductSaved != null) 
			{
				ProductSaved(this, e);
			}
		}

		protected virtual void OnProductCancelled(System.EventArgs e) 
		{
			if(ProductCancelled != null) 
			{
				ProductCancelled(this, e);
			}
		}

		protected virtual void OnProductTypeChanged(ProductTypeChangedArgs e) 
		{
			if(ProductTypeChanged != null) 
			{
				ProductTypeChanged(this, e);
			}
		}

		#endregion

		public override void DataBind()
		{
			if(ProductInstance != 0) 
			{
				LoadData();
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		protected virtual void LoadData() 
		{
			ProductBusiness productBusiness = ProductBusinessFactory.Instance.GetProductBusiness(ProductType, this.Page.MessageManager);
			DataSource = new DataTable("Product");

			productBusiness.SelectByProductInstance(DataSource, ProductInstance);
		}

		protected virtual void SetValue() 
		{
			DataRow row;
				
			if(DataSource.Rows.Count > 0) 
			{
				row	= DataSource.Rows[0];

				EnteredProductType = this.ProductType;
				EnteredProductCode = row["Product_Code"].ToString();
				EnteredYear = Convert.ToInt32(row["Product_Year"]);
				EnteredSeason = row["Product_Season"].ToString();
				ProductName = row["Product_Name"].ToString();
				ProductSortName = row["Product_Sort_Name"].ToString();
				Language = row["Lang"].ToString();
				CategoryID = Convert.ToInt32(row["Category_Code"]);
				Status = Convert.ToInt32(row["Status"]);
				DaysLeadTime = Convert.ToInt32(row["DaysLeadTime"]);
				NumberOfIssues = Convert.ToInt32(row["Nbr_Of_Issues_Per_Year"]);
				PublisherID = Convert.ToInt32(row["Pub_Nbr"]);
				FulfillmentHouseID = Convert.ToInt32(row["Fulfill_House_Nbr"]);
				Comment = row["Comment"].ToString();
				VendorNumber = row["VendorNumber"].ToString();
				VendorSiteName = row["VendorSiteName"].ToString();
				PayGroupLookUpCode = row["PayGroupLookUpCode"].ToString();
				Currency = Convert.ToInt32(row["Currency"]);
				GSTRegistrationNumber = row["GST_Registration_Nbr"].ToString();
				HSTRegistrationNumber = row["HST_Registration_Nbr"].ToString();
				PSTRegistrationNumber = row["PST_Registration_Nbr"].ToString();
				OracleCode = row["OracleCode"].ToString();
				PrizeLevel = row["Prize_Level"].ToString();
				PrizeLevelQuantity = Convert.ToInt32(row["Prize_Level_Qty_Required"]);
				RemitCode = row["RemitCode"].ToString();
				IsQSPExclusive = Convert.ToBoolean(row["IsQSPExclusive"]);
				EnglishDescription = row["EnglishDescription"].ToString();
				FrenchDescription = row["FrenchDescription"].ToString();
                VendorProductCode = row["VendorProductCode"].ToString();
			}
		}

		protected virtual void SetValueEmpty() 
		{
			EnteredProductType = this.ProductType;
			EnteredProductCode = String.Empty;

			if(this.Page.CatalogInfo != null) 
			{
				EnteredYear = this.Page.CatalogInfo.Year;
				EnteredSeason = this.Page.CatalogInfo.Season;
			} 
			else 
			{
				EnteredYear = 0;
				EnteredSeason = String.Empty;
			}

			ProductName = String.Empty;
			ProductSortName = String.Empty;
			Language = DEFAULT_LANGUAGE;
			CategoryID = 0;
			Status = DEFAULT_STATUS;
			DaysLeadTime = 0;
			NumberOfIssues = 0;
			PublisherID = 0;
			FulfillmentHouseID = 0;
			Comment = String.Empty;
			VendorNumber = String.Empty;
			VendorSiteName = String.Empty;
			PayGroupLookUpCode = String.Empty;
			Currency = DEFAULT_CURRENCY;
			GSTRegistrationNumber = String.Empty;
			HSTRegistrationNumber = String.Empty;
			PSTRegistrationNumber = String.Empty;
			OracleCode = String.Empty;
			PrizeLevel = String.Empty;
			PrizeLevelQuantity = 0;
			RemitCode = String.Empty;
			IsQSPExclusive = false;
			EnglishDescription = String.Empty;
			FrenchDescription = String.Empty;
		    VendorProductCode = String.Empty;
		}

		protected virtual void SaveProductInformation() 
		{
			if(this.ProductInstance == 0) 
			{
				InsertProductInformation();
			} 
			else 
			{
				UpdateProductInformation();
			}
		}

		protected virtual void InsertProductInformation() 
		{
			ProductBusiness productBusiness = ProductBusinessFactory.Instance.GetProductBusiness(ProductType, this.Page.MessageManager);
			ProductInstance = productBusiness.Insert(EnteredProductCode, EnteredSeason, EnteredYear, ProductName, ProductSortName, Language, CategoryID, Status, Convert.ToInt32(ProductType), DaysLeadTime, NumberOfIssues, PublisherID, FulfillmentHouseID, Comment, VendorNumber, VendorSiteName, PayGroupLookUpCode, Currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, OracleCode, PrizeLevel, PrizeLevelQuantity, RemitCode, IsQSPExclusive, EnglishDescription, FrenchDescription, this.Page.UserID, VendorProductCode);
		}

		protected virtual void UpdateProductInformation() 
		{
			ProductBusiness productBusiness = ProductBusinessFactory.Instance.GetProductBusiness(ProductType, this.Page.MessageManager);
			productBusiness.Update(ProductInstance, EnteredProductCode, EnteredSeason, EnteredYear, ProductName, ProductSortName, Language, CategoryID, Status, Convert.ToInt32(ProductType), DaysLeadTime, NumberOfIssues, PublisherID, FulfillmentHouseID, Comment, VendorNumber, VendorSiteName, PayGroupLookUpCode, Currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, OracleCode, PrizeLevel, PrizeLevelQuantity, RemitCode, IsQSPExclusive, EnglishDescription, FrenchDescription, VendorProductCode);
		}

		public void CopyTo(ProductMaintenanceControl control) 
		{
			control.ProductInstance = this.ProductInstance;
			control.EnteredProductCode = this.EnteredProductCode;
			control.EnteredYear = this.EnteredYear;
			control.EnteredSeason = this.EnteredSeason;
			control.ProductName = this.ProductName;
			control.ProductSortName = this.ProductSortName;
			control.Language = this.Language;
			control.CategoryID = this.CategoryID;
			control.Status = this.Status;
			control.DaysLeadTime = this.DaysLeadTime;
			control.NumberOfIssues = this.NumberOfIssues;
			control.PublisherID = this.PublisherID;
			control.FulfillmentHouseID = this.FulfillmentHouseID;
			control.Comment = this.Comment;
			control.VendorNumber = this.VendorNumber;
			control.VendorSiteName = this.VendorSiteName;
			control.PayGroupLookUpCode = this.PayGroupLookUpCode;
			control.Currency = this.Currency;
			control.GSTRegistrationNumber = this.GSTRegistrationNumber;
			control.HSTRegistrationNumber = this.HSTRegistrationNumber;
			control.PSTRegistrationNumber = this.PSTRegistrationNumber;
			control.OracleCode = this.OracleCode;
			control.PrizeLevel = this.PrizeLevel;
			control.PrizeLevelQuantity = this.PrizeLevelQuantity;
			control.RemitCode = this.RemitCode;
			control.IsQSPExclusive = this.IsQSPExclusive;
			control.EnglishDescription = this.EnglishDescription;
			control.FrenchDescription = this.FrenchDescription;
		    control.VendorProductCode = this.VendorProductCode;
		}
	}
}
