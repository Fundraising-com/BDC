using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for GiftValidationStrategy.
	/// </summary>
	internal class CookieDoughValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising ||
                    catalogType == CatalogType.CookieDough ||
               catalogType == CatalogType.GourmetTastyTreats);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.CookieDough);
		}
	}
}
