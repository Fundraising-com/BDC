using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class GiftValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
					catalogType == CatalogType.Rewards ||
                    catalogType == CatalogType.Gift ||
                    catalogType == CatalogType.GiftFMBulk ||
                    catalogType == CatalogType.DreamBig ||
                    catalogType == CatalogType.Festival ||
                    catalogType == CatalogType.EnjoySomethingSweet ||
                    catalogType == CatalogType.Tervis ||
                    catalogType == CatalogType.CoolCards ||
                    catalogType == CatalogType.Donations ||
                    catalogType == CatalogType.Rally ||
                    catalogType == CatalogType.FieldSupplies);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Inventory ||
					catalogSectionType == CatalogSectionType.InventoryNoTax ||
					catalogSectionType == CatalogSectionType.Prizes ||
                    catalogSectionType == CatalogSectionType.FieldSupplies);
		}
	}
}
