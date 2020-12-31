using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
   /// <summary>
   /// Summary description for GiftCardValidationStrategy.
   /// </summary>
   internal class GiftCardValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
            return (catalogType == CatalogType.GiftCard);
        }

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
            return (catalogSectionType == CatalogSectionType.GiftCard);
		}
	}
}
