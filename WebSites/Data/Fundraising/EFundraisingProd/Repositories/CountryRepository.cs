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
   public class CountryRepository : ICountryRepository
   {
      private readonly DataProvider _dataProvider;
      public CountryRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Country GetById(int id)
      {
         throw new NotImplementedException();
      }

      public IList<Country> GetAll()
      {
         const string sql = "SELECT country_code FROM countries (NOLOCK) ORDER BY country_name";
         var codes = _dataProvider.Database.Connection.Query<string>(sql, null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return codes.Select(GetByCode).ToList();
      }

      public int Save(Country model)
      {
         throw new NotImplementedException();
      }

      public void Update(Country model)
      {
         throw new NotImplementedException();
      }

      public void Delete(Country model)
      {
         throw new NotImplementedException();
      }

      public Country GetByCode(string code)
      {
         const string sql = "SELECT * FROM countries (NOLOCK) WHERE country_code = @code";
         var row = _dataProvider.Database.Connection.QueryFirst<country>(sql, new {code},
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var entity = CountryMapper.Hydrate(row);
         return entity;
      }
   }
}
