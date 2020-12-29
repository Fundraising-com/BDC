using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;
using GA.BDC.Data.Fundraising.EFundraisingProd.Mappers;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Repositories
{
   public class RegionRepository : IRegionRepository
   {
      private readonly DataProvider _dataProvider;
      public RegionRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Region GetById(int id)
      {
         throw new NotImplementedException();
      }

      public IList<Region> GetAll()
      {
         const string sql = "SELECT state_code FROM State (NOLOCK)";
         var codes = _dataProvider.Database.Connection.Query<string>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return codes.Select(GetByCode).ToList();
      }

      public int Save(Region model)
      {
         throw new NotImplementedException();
      }

      public void Update(Region model)
      {
         throw new NotImplementedException();
      }

      public void Delete(Region model)
      {
         throw new NotImplementedException();
      }

      public Region GetByCode(string code)
      {
         const string sql = "SELECT * FROM State (NOLOCK) WHERE state_code = @code";
         var row = _dataProvider.Database.Connection.QueryFirst<State>(sql, new { code }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var entity = RegionMapper.Hydrate(row);
         return entity;
      }

      public IList<Region> GetByCountryCode(string code)
      {
         const string sql = "SELECT state_code FROM State (NOLOCK) WHERE country_code = @code";
         var codes = _dataProvider.Database.Connection.Query<string>(sql, new {code}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return codes.Select(GetByCode).ToList();
      }
   }
}
