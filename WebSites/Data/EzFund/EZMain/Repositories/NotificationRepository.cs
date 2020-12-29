using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper.Contrib.Extensions;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
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
            notification.Created = DateTime.Now;
            var row = NotificationMapper.Dehydrate(notification);
            try
            {
                var notificationToBePersistedId = _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                return (int)notificationToBePersistedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
