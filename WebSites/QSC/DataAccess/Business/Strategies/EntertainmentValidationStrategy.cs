using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
    /// Summary description for EntertainmentValidationStrategy.
	/// </summary>
    internal class EntertainmentValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
            return (catalogType == CatalogType.Entertainment ||
                    catalogType == CatalogType.EntertainmentFaculty ||
                    catalogType == CatalogType.EntertainmentFMBulk);
        }

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
            return (catalogSectionType == CatalogSectionType.Entertainment);
		}
	}
}
