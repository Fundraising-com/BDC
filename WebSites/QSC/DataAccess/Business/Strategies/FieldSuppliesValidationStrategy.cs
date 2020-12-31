using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class FieldSuppliesValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
					catalogType == CatalogType.Incentives ||
					catalogType == CatalogType.Rewards ||
					catalogType == CatalogType.Promotional ||
                    catalogType == CatalogType.FieldSupplies ||
                    catalogType == CatalogType.FieldSuppliesFMBulk);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Inventory ||
					catalogSectionType == CatalogSectionType.InventoryNoTax ||
					catalogSectionType == CatalogSectionType.FieldSupplies ||
					catalogSectionType == CatalogSectionType.Prizes);
		}
	}
}
