using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class BlogPostRepository : IBlogPostRepository
   {
      private readonly DataProvider _dataProvider;

      public BlogPostRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public BlogPost GetById(int id)
      {
         const string sql = "SELECT * FROM blog.post (NOLOCK) WHERE id = @id";
         var row = _dataProvider.Database.Connection.QueryFirstOrDefault<blog_post>(sql, new {id}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var result = BlogPostsMapper.Hydrate(row);
         return result;
      }


      public IList<BlogPost> GetAll()
      {
         const string sql = "SELECT id FROM blog.post (NOLOCK);";
         var ids = _dataProvider.Database.Connection.Query<int>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }



      public BlogPost GetByUrl(string url, bool isPreview)
      {
         const string sql = "SELECT id FROM blog.post (NOLOCK) WHERE url = @url AND published < GETDATE() AND (1 = @isPreview OR is_draft = @isPreview);";
         var id = _dataProvider.Database.Connection.QueryFirstOrDefault<int>(sql, new {url, isPreview = isPreview ? 1 : 0 }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return GetById(id);
      }

      public BlogPost GetByCategory(int id)
      {
         const string sql = "SELECT id FROM blog.post (NOLOCK) WHERE category_id = @id AND published < GETDATE() AND is_draft = 0";
         var rowId = _dataProvider.Database.Connection.QueryFirst<int>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return GetById(rowId);
      }

      public IList<BlogPost> GetAllByTagId(int tagId)
      {
         const string sql = "SELECT P.id FROM blog.post P (NOLOCK) JOIN blog.posts_tags T (NOLOCK) ON P.id = T.post_id WHERE T.tag_id = @tagId AND P.published < GETDATE() AND P.is_draft = 0";
         var ids = _dataProvider.Database.Connection.Query<int>(sql, new { tagId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public IList<BlogPost> GetAllByCategoryId(int categoryId)
      {
         const string sql = "SELECT id FROM blog.post (NOLOCK) WHERE category_id = @categoryId AND published < GETDATE() AND is_draft = 0";
         var ids = _dataProvider.Database.Connection.Query<int>(sql, new { categoryId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }


      public int Save(BlogPost post)
      {
         post.Created = DateTime.Now;
         post.CategoryId = post.CategoryId == 0 && post.Category != null ? post.Category.Id : post.CategoryId;
         if (post.IsDraft)
         {
            post.Published = DateTime.Now.AddYears(10);
         }
         var row = BlogPostsMapper.DeHydrate(post);
         var id = (int) _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         
         foreach (var tag in post.Tags)
         {
            var postTag = new post_tag {post_id = id, tag_id = tag.Id};
            _dataProvider.Database.Connection.Insert(postTag, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            
         }
         return id;
      }

      public void Update(BlogPost post)
      {
         if (post.IsDraft)
         {
            post.Published = DateTime.Now.AddYears(10);
         }
         post.CategoryId = post.Category.Id;
         const string sqlTag = "SELECT * FROM blog.posts_tags (NOLOCK) WHERE post_id = @id";
         var postTags = _dataProvider.Database.Connection.Query<post_tag>(sqlTag, new { id = post.Id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         foreach (var postTag in postTags)
         {
            _dataProvider.Database.Connection.Delete(postTag, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         }
         foreach (var tag in post.Tags)
         {
            var postTag = new post_tag { post_id = post.Id, tag_id = tag.Id };
            _dataProvider.Database.Connection.Insert(postTag, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         }
         var row = BlogPostsMapper.DeHydrate(post);
         _dataProvider.Database.Connection.Update(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }

      public void Delete(BlogPost model)
      {
         const string sqlTag = "SELECT * FROM blog.posts_tags (NOLOCK) WHERE post_id = @id";
         var postTags = _dataProvider.Database.Connection.Query<post_tag>(sqlTag, new { id = model.Id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         foreach (var postTag in postTags)
         {
            _dataProvider.Database.Connection.Delete(postTag, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         }
         var row = BlogPostsMapper.DeHydrate(model);
         _dataProvider.Database.Connection.Delete(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }
   }
}