using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Data.Fundraising.EFundStore.Repositories;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
   public class PromotionCodeRepository : IPromotionCodeRepository
   {

      private readonly DataProvider _dataProvider;
      private readonly EFundStore.Tables.DataProvider _efundStoreDataProvider;

      public PromotionCodeRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
         _efundStoreDataProvider = new EFundStore.Tables.DataProvider();
         _efundStoreDataProvider.Database.BeginTransaction();
      }

      public PromotionCode GetById(int id)
      {
         var promotionCodeFound =
            _dataProvider.Database.Connection.Query<Promotion_Code>(
               "SELECT TOP 1 * FROM Promotion_Code (NOLOCK) WHERE Promotion_Code_ID = @id", new { id },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
         var productRepository = new ProductRepository(_efundStoreDataProvider);
         var productIds = _dataProvider.Database.Connection.Query<int>("SELECT scratch_book_id FROM Promotion_Code_Product (NOLOCK) WHERE Promotion_Code_ID = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var promotionCode = PromotionCodeMapper.Hydrate(promotionCodeFound);
         promotionCode.Products = productIds.Select(p => productRepository.GetByScratchbookId(p)).ToList();
         return promotionCode;
      }

      public IList<PromotionCode> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT Promotion_Code_ID FROM Promotion_Code (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

      public int Save(PromotionCode model)
      {
         const string sqlProducts = "INSERT INTO Promotion_Code_Product (promotion_code_id, scratch_book_id) VALUES(@promotionCodeId, @scratchBookId);";
         model.Created = DateTime.Now;
         var row = PromotionCodeMapper.Dehydrate(model);
         var id = (int)_dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         if (model.ScopeType == PromotionCodeScopeType.Products)
         {
            foreach (var product in model.Products.Where(p => p.ScratchBookId > 0))
            {
               _dataProvider.Database.Connection.Execute(sqlProducts, new { promotionCodeId = id, scratchBookId = product.ScratchBookId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            }
         }
         return id;

      }

      public void Update(PromotionCode model)
      {
         const string sqlProducts = "INSERT INTO Promotion_Code_Product (promotion_code_id, scratch_book_id) VALUES(@promotionCodeId, @scratchBookId);";
         const string sqlDeleteProducts = "DELETE Promotion_Code_Product WHERE promotion_code_id = @promotionCodeId;";
         var row = PromotionCodeMapper.Dehydrate(model);
         _dataProvider.Database.Connection.Update(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         if (model.ScopeType == PromotionCodeScopeType.Products)
         {
            _dataProvider.Database.Connection.Execute(sqlDeleteProducts, new { promotionCodeId = model.Id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            foreach (var product in model.Products.Where(p => p.ScratchBookId > 0))
            {
               _dataProvider.Database.Connection.Execute(sqlProducts, new { promotionCodeId = model.Id, scratchBookId = product.ScratchBookId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            }
         }
      }

      public void Delete(PromotionCode model)
      {
         const string sqlDeleteProducts = "DELETE Promotion_Code_Product WHERE promotion_code_id = @promotionCodeId;";
         _dataProvider.Database.Connection.Execute(sqlDeleteProducts, new { promotionCodeId = model.Id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var row = PromotionCodeMapper.Dehydrate(model);
         _dataProvider.Database.Connection.Delete(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         
      }

      public PromotionCode GetByCode(string code)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT Promotion_Code_ID FROM Promotion_Code (NOLOCK) WHERE Code = @code", new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Any() ? GetById(ids.First()) : null;
      }
   }
}
