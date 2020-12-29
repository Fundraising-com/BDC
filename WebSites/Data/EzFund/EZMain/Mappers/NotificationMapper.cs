using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class NotificationMapper
    {
        public static Notification Hydrate(notification notification)
        {
            var _notification = new Notification
            {
                Id = notification.id,
                ExternalId = notification.id,
                Email = notification.email,
                ExtraData = notification.extra_data,
                Type = (NotificationType)notification.type,
                Created = notification.created_on
            };
            return _notification;
        }

        public static notification Dehydrate(Notification notification)
        {
            var _notification = new notification
            {
                external_id = notification.ExternalId,
                email = notification.Email,
                extra_data = notification.ExtraData,
                type = (int)notification.Type,
                created_on = notification.Created
            };
            return _notification;
        }
    }
}
