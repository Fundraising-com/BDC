using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
   /// <summary>
   /// Summary description for PretzelRodsValidationStrategy.
   /// </summary>
   internal class PretzelRodsValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
            return (catalogType == CatalogType.PretzelRods2
                     || catalogType == CatalogType.PretzelRods3);
        }

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
            return (catalogSectionType == CatalogSectionType.PretzelRods2
                     || catalogSectionType == CatalogSectionType.PretzelRods3);
		}
	}
}
