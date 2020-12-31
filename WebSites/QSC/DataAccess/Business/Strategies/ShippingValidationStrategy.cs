using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class ShippingValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Fundraising);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Misc);
		}
	}
}
