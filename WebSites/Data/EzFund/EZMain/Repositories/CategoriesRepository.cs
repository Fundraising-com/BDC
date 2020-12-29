using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataProvider _dataProvider;

        public CategoriesRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(Category model)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            const string sql = @"SELECT TOP 1 * FROM category (NOLOCK) WHERE category_id = @id";
            var row = _dataProvider.Database.Connection.QueryFirst<category>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return CategoryMapper.Hydrate(row);
        }

        public IList<Category> GetByParent(int parentId)
        {
            const string sql = @"SELECT category_id FROM category (NOLOCK) WHERE parent_category_id = @parentId AND enabled = 1  ORDER BY [order] ASC";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, new { parentId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Category> GetByPartner(int country, int partnerId)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetByParentCode(string code)
        {
            const string sql = "SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL (NOLOCK) WHERE APPL_NME = @code AND CLEAN_URL IS NOT NULL";
            var ids = _dataProvider.Database.Connection.Query<int>(sql,
                 new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(GetById).ToList();
        }

        public IList<Category> GetCategoryByUrl(int country, string url)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetExcludedCategories(int partnerId)
        {
            throw new NotImplementedException();
        }

        public int Save(Category model)
        {
            throw new NotImplementedException();
        }

        public void Update(Category model)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetByCleanUrl(string parentUrl)
        {
            const string sql = @"SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL (NOLOCK) WHERE  CLEAN_URL=@parentUrl AND PARENT_CLEAN_URL IS NULL AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE)
                                 SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL (NOLOCK) WHERE PARENT_CLEAN_URL = @parentUrl AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE)";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { parentUrl }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var parentCategoryId = multi.Read<int>().SingleOrDefault();
            var subCategoryIds = multi.Read<int>().AsList();
            var listResult = subCategoryIds.Select(GetById).ToList();
            listResult.First().Parent = GetById(parentCategoryId);
            return listResult;
        }

        public Category GetByCleanUrl(string url, string parentUrl)
        {
            const string sql = @"SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL (NOLOCK) WHERE PARENT_CLEAN_URL = @parentUrl AND CLEAN_URL = @url AND XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE)
                                 SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL(NOLOCK) WHERE CLEAN_URL = @parentUrl AND PARENT_CLEAN_URL IS NULL";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { parentUrl, url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var subCategoryId = multi.Read<int>().Single();
            var parentCategoryId = multi.Read<int>().SingleOrDefault();
            var category = GetById(subCategoryId);
            category.Parent = GetById(parentCategoryId);
            return category;
        }

        public IList<Category> GetAllSubCategories()
        {
            const string sql = "SELECT ELEM_ID FROM SITE_REF_PCKL_LKUP_TBL (NOLOCK) WHERE XTRN_END_DTE >= CAST(CURRENT_TIMESTAMP AS DATE) AND XTRN_STRT_DTE <= CAST(CURRENT_TIMESTAMP AS DATE) AND ELEM_CDE IS NOT NULL ORDER BY APPL_NME ASC";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, null , _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public IList<Category> GetAllCategories()
        {
            const string sql = "SELECT category_id FROM category (NOLOCK) WHERE enabled = 1 AND parent_category_id = 1 ORDER BY [order] ASC";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();

        }

        public IList<Category> GetAllCategoriesSiteMap()
        {
            const string sql = "SELECT category_id FROM category (NOLOCK) WHERE enabled = 1 ORDER BY name ASC";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();

        }

        public IList<Category> GetCategoryByUrl(string url)
        {
            const string sql = @"SELECT category_id FROM category (NOLOCK) WHERE url = @url AND enabled = 1";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, new { url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }

        public Category GetByProductId(int productId)
        {
            const string sql = @"SELECT TOP 1 category_id FROM product_category (NOLOCK) WHERE product_id = @productId";
            var categoryId = _dataProvider.Database.Connection.QueryFirst<int>(sql, new { productId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            const string sqlCategory = @"SELECT TOP 1 category_id FROM category (NOLOCK) WHERE category_id = @categoryId";
            var id = _dataProvider.Database.Connection.QueryFirst<int>(sqlCategory, new { categoryId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
        }

        public Category GetParentById(int parentId)
        {
            const string sql = @"SELECT category_id FROM category (NOLOCK) WHERE category_id = @parentId AND enabled = 1 ORDER BY [order] ASC";
            var id = _dataProvider.Database.Connection.QueryFirst<int>(sql, new { parentId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
        }
    }
}
