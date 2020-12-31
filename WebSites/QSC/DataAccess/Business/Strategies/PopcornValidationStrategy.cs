using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
   /// <summary>
   /// Summary description for PopcornValidationStrategy.
   /// </summary>
   internal class PopcornValidationStrategy : ProductTypeValidationStrategy
   {
      protected override bool ValidateCatalog(CatalogType catalogType)
      {
         return (catalogType == CatalogType.Popcorn ||
               catalogType == CatalogType.PapaJackPopcorn);
      }

      protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
      {
         return (catalogSectionType == CatalogSectionType.Popcorn);
      }
   }
}