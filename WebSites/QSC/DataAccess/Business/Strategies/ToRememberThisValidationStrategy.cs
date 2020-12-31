using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
    /// Summary description for ToRememberThisValidationStrategy.
	/// </summary>
	internal class ToRememberThisValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
            return (catalogType == CatalogType.ToRememberThis ||
                    catalogType == CatalogType.ToRememberThisFaculty);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.ToRememberThis);
		}
	}
}
