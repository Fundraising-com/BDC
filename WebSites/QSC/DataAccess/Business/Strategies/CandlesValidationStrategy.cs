using System;

namespace QSPFulfillment.DataAccess.Business.Strategies
{
	/// <summary>
    /// Summary description for CandlesValidationStrategy.
	/// </summary>
    internal class CandlesValidationStrategy : ProductTypeValidationStrategy
	{
		protected override bool ValidateCatalog(CatalogType catalogType)
		{
			return (catalogType == CatalogType.Candles);
		}

		protected override bool ValidateCatalogSection(CatalogSectionType catalogSectionType)
		{
			return (catalogSectionType == CatalogSectionType.Candles);
		}
	}
}
