using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        IList<Category> GetExcludedCategories(int partnerId);
        IList<Category> GetByPartner(int country, int partnerId);
        IList<Category> GetByParent(int parentId);
        IList<Category> GetCategoryByUrl(int country, string url);
        IList<Category> GetCategoryByUrl(string url);
        /// <summary>
        /// Returns a List of Categories by it's Parent Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IList<Category> GetByCleanUrl(string parentUrl);
        /// <summary>
        /// Return a specific Category by it's Clean url and its Parent Url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parentUrl"></param>
        /// <returns></returns>
        Category GetByCleanUrl(string url, string parentUrl);
        /// <summary>
        /// Returns a List of all product Categories
        /// </summary>
        /// <returns></returns>
        IList<Category> GetAllCategories();
        /// <summary>
        /// Return a specific Category by a product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Category GetByProductId(int productId);
        Category GetParentById(int parentId);

        IList<Category> GetAllCategoriesSiteMap();
    }
}
