using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
   public class ProductClassRepository : IProductClassRepository
   {
      private readonly DataProvider _dataProvider;
      public ProductClassRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }
      public ProductClass GetById(int id)
      {
         var entity = _dataProvider.Database.Connection.QueryFirst<product_class>("SELECT * FROM product_class (NOLOCK) WHERE product_class_id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ProductClassMapper.Hydrate(entity);
      }

      public IList<ProductClass> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT product_class_id FROM product_class (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public int Save(ProductClass model)
      {
         throw new NotImplementedException();
      }

      public void Update(ProductClass model)
      {
         throw new NotImplementedException();
      }

      public void Delete(ProductClass model)
      {
         throw new NotImplementedException();
      }
   }
}
