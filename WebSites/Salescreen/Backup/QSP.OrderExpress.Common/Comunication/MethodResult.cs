using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Comunication
{
    [Serializable]
    public class MethodResult
    {
        public MethodResult()
        {
            this.ResultItems = new Dictionary<string, object>();
            this.ResultNotifications = new List<MethodResultNotification>();
        }

        public List<MethodResultNotification> ResultNotifications { get; set; }
        public Dictionary<string, object> ResultItems { get; set; }
        public bool IsSuccessful 
        {
            get
            {
                bool result = true;

                foreach (MethodResultNotification notification in ResultNotifications)
                {
                    if (notification.NotificationType == MethodResultNotificationTypeEnum.Error ||
                        notification.NotificationType == MethodResultNotificationTypeEnum.Warning)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        public void Merge(MethodResult methodResult)
        {
            this.Merge(methodResult, "");
        }
        public void Merge(MethodResult methodResult, string keyPrefix)
        {
            foreach (KeyValuePair<string, object> item in methodResult.ResultItems)
            {
                this.ResultItems.Add(
                    string.Format("{1}-{0}", item.Key, keyPrefix), 
                    string.Format("{1} - {0}", item.Value, keyPrefix));
            }

            foreach (MethodResultNotification notification in methodResult.ResultNotifications)
            {
                MethodResultNotification newNotification = new MethodResultNotification();

                newNotification.DynamicValues = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> item in notification.DynamicValues)
                {
                    newNotification.DynamicValues.Add(
                        string.Format("{1}-{0}", item.Key, keyPrefix), 
                        string.Format("{1} - {0}", item.Value, keyPrefix));
                }

                newNotification.Message = string.Format("{1} - {0}", notification.Message, keyPrefix);
                newNotification.NotificationType = notification.NotificationType;

                this.ResultNotifications.Add(newNotification);
            }
        }
    }
}
