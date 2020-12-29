using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class NotificationRepository : INotificationRepository
   {
      private readonly DataProvider _dataProvider;
      public NotificationRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }

      public Notification GetById(int id)
      {
         throw new NotImplementedException();
      }

      public IList<Notification> GetAll()
      {
         throw new NotImplementedException();
      }

      public int Save(Notification notification)
      {

          notification.Id = _dataProvider.notifications.Max(p => p.id) + 1;
          notification.Created = DateTime.Now;
          var notificationToBePersisted = NotificationMapper.Dehydrate(notification);
          _dataProvider.notifications.Add(notificationToBePersisted);
          _dataProvider.SaveChanges();

          return notificationToBePersisted.id;
        
      }

      public void Update(Notification model)
      {
         throw new NotImplementedException();
      }

      public void Delete(Notification model)
      {
         throw new NotImplementedException();
      }

      

   }
}
