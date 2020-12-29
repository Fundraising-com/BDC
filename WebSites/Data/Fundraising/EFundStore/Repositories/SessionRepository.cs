using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using Dapper;
namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class SessionRepository : ISessionRepository
   {
      private readonly DataProvider _dataProvider;
      public SessionRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Session GetById(int id)
      {
         var session = _dataProvider.Database.Connection.Query<session>("SELECT TOP 1 id, anonymous_id, created_on FROM session (NOLOCK) WHERE id = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
         var sessionItems = _dataProvider.Database.Connection.Query<session_item>("SELECT id, sessionId, created_on, name, value  FROM session_item (NOLOCK) WHERE sessionId = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return SessionMapper.Hydrate(session, sessionItems);
      }

      public IList<Session> GetAll()
      {
         throw new NotImplementedException();
      }

      public Session GetByAnonymousId(string anonymousId)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT TOP 1 id FROM session (NOLOCK) WHERE anonymous_id = @anonymousId", new { anonymousId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Any() ? GetById(ids[0]) : null;
      }

      public int Save(Session session)
      {
         var sessionToBePersisted = SessionMapper.Dehydrate(session);
         sessionToBePersisted.created_on = DateTime.Now;

         _dataProvider.sessions.Add(sessionToBePersisted);
         _dataProvider.SaveChanges();
         foreach (var property in session.Properties)
         {
            var propertyToBeSaved = SessionMapper.DehydrateItem(property);
            propertyToBeSaved.sessionId = sessionToBePersisted.id;
            propertyToBeSaved.created_on = DateTime.Now;
            _dataProvider.session_item.Add(propertyToBeSaved);
            _dataProvider.SaveChanges();

         }
         return sessionToBePersisted.id;
      }

      public void Update(Session session)
      {
         foreach (var property in session.Properties)
         {
            var currentProperty =
                _dataProvider.session_item.FirstOrDefault(
                    p => p.sessionId == session.Id && p.name == property.Key);
            if (currentProperty == null)
            {
               var propertyToBeSaved = SessionMapper.DehydrateItem(property);
               propertyToBeSaved.sessionId = session.Id;
               propertyToBeSaved.created_on = DateTime.Now;
               _dataProvider.session_item.Add(propertyToBeSaved);
            }
            else
            {
               currentProperty.value = property.Value;
            }
         }
         _dataProvider.SaveChanges();
      }

      public void Delete(Session model)
      {
         throw new NotImplementedException();
      }
   }
}
