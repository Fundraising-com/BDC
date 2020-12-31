using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for IProductTypeValidationStrategy.
	/// </summary>
	internal abstract class ProductTypeValidationStrategy
	{
		protected abstract bool ValidateCatalog(CatalogType catalogType);

		protected abstract bool ValidateCatalogSection(CatalogSectionType catalogSectionType);

		internal bool Validate(CatalogType catalogType, CatalogSectionType catalogSectionType) 
		{
			return (ValidateCatalog(catalogType) && ValidateCatalogSection(catalogSectionType));
		}
	}
}
