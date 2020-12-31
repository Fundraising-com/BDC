using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class IncentivesValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
					catalogType == CatalogType.Rewards ||
                    catalogType == CatalogType.Prizes ||
                    catalogType == CatalogType.PrizesFMBulk);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Inventory ||
					catalogSectionType == CatalogSectionType.InventoryNoTax ||
					catalogSectionType == CatalogSectionType.Prizes);
		}
	}
}
