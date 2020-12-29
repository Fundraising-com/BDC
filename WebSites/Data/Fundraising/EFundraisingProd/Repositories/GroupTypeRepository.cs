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
   public class GroupTypeRepository : IGroupTypeRepository
   {
      private readonly DataProvider _dataProvider;
      public GroupTypeRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }
      public GroupType GetById(int id)
      {
         var row = _dataProvider.Database.Connection.QueryFirst<group_type>("SELECT * FROM group_type (NOLOCK) WHERE group_type_id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return GroupTypeMapper.Hydrate(row);
      }

      public IList<GroupType> GetAll()
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT group_type_id FROM group_type (NOLOCK)", null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         return ids.Select(GetById).ToList();
      }

      public int Save(GroupType model)
      {
         throw new NotImplementedException();
      }

      public void Update(GroupType model)
      {
         throw new NotImplementedException();
      }

      public void Delete(GroupType model)
      {
         throw new NotImplementedException();
      }
   }
}
