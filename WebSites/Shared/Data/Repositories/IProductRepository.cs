using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IList<Product> GetAllFeatured();
        IList<Product> GetByCategory(int categoryId);
        IList<Product> GetAllByCountry(int country);
        IList<Product> GetFiltered(double price, double profit, int[] packageIds);
        Product GetByScratchbookId(int id);
        Product GetByScratchbookIdSalesScreen(int id);
        KeyValuePair<int, string> GetProductClass(int scratchBookId);
        Product GetById(int id, bool showUnapprovedReviews);
        /// <summary>
        /// Used on EzFund to retrieve Programs as Products, they have Code instead of ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Product GetByCode(string code);
        /// <summary>
        /// Returns Product by its full clean URL
        /// </summary>
        /// <param name="rootCategoryUrl"></param>
        /// <param name="categoryUrl"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        Product GetByUrl(string url);
        Product GetByCleanUrl(string rootCategoryUrl, string categoryUrl, string url);
        IList<Product> GetByCleanUrl(string rootCategoryUrl, string categoryUrl);
        IList<Product> GetRelatedProducts(string code, int maxResults, bool isRandom, bool canBePurchased);
        IList<Product> GetRelatedProducts(int id, int maxResults, bool isRandom);
        Product GetSimpleById(int id);
        SubProduct GetSubProductByCode(string code);


    }
}
