using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class WFCValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
					catalogType == CatalogType.Incentives ||
					catalogType == CatalogType.Rewards);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Inventory ||
					catalogSectionType == CatalogSectionType.InventoryNoTax ||
					catalogSectionType == CatalogSectionType.Incentives ||
					catalogSectionType == CatalogSectionType.Prizes);
		}
	}
}
