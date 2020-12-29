using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class UsersRepository : IUsersRepository
   {
      private readonly DataProvider _dataProvider;
      public UsersRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public User GetById(int id)
      {
         var userFound = _dataProvider.users.First(p => p.Id == id.ToString());
         var userProfileFound = _dataProvider.user_profiles.First(p => p.Id == userFound.profile_id);
         var result = UserMapper.Hydrate(userFound, userProfileFound);
         return result;
      }

      public IList<User> GetAll()
      {
         throw new NotImplementedException();
      }

      public int Save(User model)
      {
         throw new NotImplementedException();
      }

      public void Update(User model)
      {
         throw new NotImplementedException();
      }

      public void Delete(User model)
      {
         throw new NotImplementedException();
      }
   }
}
