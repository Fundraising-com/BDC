using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
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
                id = notification.Id,
                external_id = notification.ExternalId,
                email = notification.Email,
                extra_data = notification.ExtraData,
                type =  (int)notification.Type,
               
                created_on = notification.Created
            };
            return _notification;
        }
    }
}
