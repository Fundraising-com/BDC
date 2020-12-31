using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class JewelryValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
                    catalogType == CatalogType.Gift ||
                    catalogType == CatalogType.GiftFMBulk ||
                    catalogType == CatalogType.DreamBig ||
                    catalogType == CatalogType.OrganicEdibles ||
                    catalogType == CatalogType.TheCureJewelry);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Jewelry);
		}
	}
}
