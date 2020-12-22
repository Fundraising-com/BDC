using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Comunication
{
    [Serializable]
    public class MethodResultNotification
    {
        public MethodResultNotification()
        {
            this.DynamicValues = new Dictionary<string, object>();
        }

        public string Message { get; set; }
        public Dictionary<string, object> DynamicValues { get; set; }
        public MethodResultNotificationTypeEnum NotificationType { get; set; }
    }
}
