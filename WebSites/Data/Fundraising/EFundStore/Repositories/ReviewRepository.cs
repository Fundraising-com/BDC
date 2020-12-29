using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.Fundraising.EFundStore.Tables;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class ReviewRepository : IReviewRepository
   {
      private readonly DataProvider _dataProvider;

      public ReviewRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public void Delete(Review model)
      {
         var row = ReviewMapper.DeHydrate(model);
         _dataProvider.Database.Connection.Delete(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }

      public IList<Review> GetAll()
      {
         const string sql = "SELECT Id FROM review (NOLOCK)";
         var ids = _dataProvider.Database.Connection.Query<int>(sql,
              null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

      public IList<Review> GetAllByProductId(int productId)
      {
         const string sql = "SELECT Id FROM review (NOLOCK) WHERE ProductId = @productId";
         var ids = _dataProvider.Database.Connection.Query<int>(sql,
              new { productId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

      public IList<Review> GetAllBySaleId(int saleId)
      {
         const string sql = "SELECT Id FROM review (NOLOCK) WHERE SaleId = @saleId";
         var ids = _dataProvider.Database.Connection.Query<int>(sql,
              new { saleId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

      public Review GetById(int id)
      {
         const string sql = "SELECT * FROM review (NOLOCK) WHERE Id = @id";
         const string sqlProduct = "SELECT name FROM product (NOLOCK) WHERE product_id = @productId";
         var row = _dataProvider.Database.Connection.QueryFirst<review>(sql, new {id}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var productName = _dataProvider.Database.Connection.QueryFirst<string>(sqlProduct, new { productId = row.ProductId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ReviewMapper.Hydrate(row, productName);
      }

      public int Save(Review model)
      {
         model.Created = DateTime.Now;
         var row = ReviewMapper.DeHydrate(model);
         _dataProvider.reviews.Add(row);
         _dataProvider.SaveChanges();
         return row.Id;
      }

      public void Update(Review model)
      {
         var row = ReviewMapper.DeHydrate(model);
         _dataProvider.Database.Connection.Update(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
      }
   }
}
