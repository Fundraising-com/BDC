using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class IncentiveMagValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Incentives ||
					catalogType == CatalogType.Rewards ||
                    catalogType == CatalogType.Prizes ||
                    catalogType == CatalogType.PrizesFMBulk);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.InventoryNoTax ||
					catalogSectionType == CatalogSectionType.Incentives ||
					catalogSectionType == CatalogSectionType.Prizes);
		}
	}
}
