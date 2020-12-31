namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Business.Strategies;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.ProductData;

	[Serializable]
	public enum ProductType 
	{
		None			= 0,
		Magazine		= 46001,
		Gift			= 46002,
		WFC				= 46003,
		FieldSupplies	= 46004,
		Food			= 46005,
		Books			= 46006,
		GiftCard			= 46007,
		Incentives		= 46008,
		NewBusiness		= 46009,
		MMB				= 46010,
		National		= 46011,
		Video			= 46012,
		IncentiveMag	= 46013,
		IncentiveGift	= 46014,
		IncentiveFood	= 46015,
        ProcessingFee   = 46017,
        CookieDough     = 46018,
        Popcorn       = 46019,
        Jewelry         = 46020,
        Shipping        = 46021,
        Candles         = 46022,
        ToRememberThis  = 46023,
        Entertainment   = 46024,
        PretzelRods     = 46025
	}

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class ProductTypeBusiness : BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public ProductTypeBusiness(Message messageManager) : base(messageManager) { }

		public ProductTypeBusiness(bool asMessageManager) : base(asMessageManager) { }

		public void SelectAllProductTypes(DataTable table) 
		{
			try 
			{
				dataAccess.SelectAllProductTypes(table);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllProductTypes(DataTable table, CatalogType catalogType, CatalogSectionType catalogSectionType) 
		{
			SelectAllProductTypes(table);

			GetAvailableProductTypes(table, catalogType, catalogSectionType);
		}

		private void GetAvailableProductTypes(DataTable table, CatalogType catalogType, CatalogSectionType catalogSectionType) 
		{
			ProductType productType;

			foreach(DataRow row in table.Rows) 
			{
				productType = (ProductType) Convert.ToInt32(row["Instance"]);

				if(!ProductTypeValidationFactory.Instance.GetProductTypeValidationStrategy(productType).Validate(catalogType, catalogSectionType)) 
				{
					row.Delete();
				}
			}

			table.AcceptChanges();
		}

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}