using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class BlogTagRepository : IBlogTagRepository
   {
      private readonly DataProvider _dataProvider;
      public BlogTagRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public IList<BlogTag> GetAll()
      {
         const string sql = "SELECT id FROM blog.tags (NOLOCK);";
         var ids = _dataProvider.Database.Connection.Query<int>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public void Delete(BlogTag model)
      {
         const string deleteRelationshipSql = "DELETE blog.posts_tags WHERE tag_id = @id";
         _dataProvider.Database.Connection.Execute(deleteRelationshipSql, new {id = model.Id},
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var row = BlogTagsMapper.DeHydrate(model);
         _dataProvider.Database.Connection.Delete(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }

      public BlogTag GetById(int id)
      {
         const string sql = "SELECT * FROM blog.tags (NOLOCK) WHERE id = @id";
         var row = _dataProvider.Database.Connection.QueryFirstOrDefault<blog_tags>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var result = BlogTagsMapper.Hydrate(row);
         return result;
      }

      public BlogTag GetByUrl(string url)
      {
         const string sql = "SELECT id FROM blog.tags (NOLOCK) WHERE url = @url";
         var id = _dataProvider.Database.Connection.QueryFirstOrDefault<int>(sql, new { url }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return GetById(id);
      }

      public IList<BlogTag> GetAllByPostId(int postId)
      {
         const string sql = "SELECT tag_id FROM blog.posts_tags (NOLOCK) WHERE post_id = @postId";
         var ids = _dataProvider.Database.Connection.Query<int>(sql, new { postId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public int Save(BlogTag tag)
      {
         var row = BlogTagsMapper.DeHydrate(tag);
         return (int) _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }

      public void Update(BlogTag tag)
      {
         var row = BlogTagsMapper.DeHydrate(tag);
         _dataProvider.Database.Connection.Update(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }
   }
}
