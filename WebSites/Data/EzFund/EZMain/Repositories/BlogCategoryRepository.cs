using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Data.EzFund.EZMain.Mappers;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private readonly DataProvider _dataProvider;
        public BlogCategoryRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IList<BlogCategory> GetAll()
        {
            const string sql = "SELECT id FROM blog.categories (NOLOCK);";
            var ids = _dataProvider.Database.Connection.Query<int>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return ids.Select(GetById).ToList();
        }


        public BlogCategory GetById(int id)
        {
            const string sql = "SELECT * FROM blog.categories (NOLOCK) WHERE id = @id";
            var row = _dataProvider.Database.Connection.QueryFirst<blog_categories>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            var result = BlogCategoriesMapper.Hydrate(row);
            return result;
        }

        public BlogCategory GetByUrl(string url)
        {
            const string sql = "SELECT id FROM blog.categories (NOLOCK) WHERE url = @url;";
            var id = _dataProvider.Database.Connection.QueryFirst<int>(sql, new { url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return GetById(id);
        }

        public int Save(BlogCategory category)
        {
            var row = BlogCategoriesMapper.DeHydrate(category);
            return (int)_dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        }


        public void Update(BlogCategory category)
        {
            var row = BlogCategoriesMapper.DeHydrate(category);
            _dataProvider.Database.Connection.Update(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        }

        public void Delete(BlogCategory model)
        {
            var numberOfPostsWithCategory =
               _dataProvider.Database.Connection.ExecuteScalar<int>("SELECT COUNT(*) FROM blog.post WHERE category_id = @id",
                  new { id = model.Id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            if (numberOfPostsWithCategory > 0)
            {
                throw new Exception("Can't delete Category because there are Posts assigned to it");
            }
            var row = BlogCategoriesMapper.DeHydrate(model);
            _dataProvider.Database.Connection.Delete(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        }
    }
}
