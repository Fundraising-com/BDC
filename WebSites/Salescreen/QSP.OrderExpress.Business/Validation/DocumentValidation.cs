using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Business.Validation
{
    public class DocumentValidation
    {
        public MethodResult ValidateDocument(DocumentData document)
        {
            MethodResult result = new MethodResult();

            if (document.IsApproved)
            {
                #region Exemption number

                if (document.ExemptionNumber == null)
                {
                    MethodResultNotification notification = new MethodResultNotification();

                    notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                    notification.Message = "If the exemption form is approved, an exemption number is mandatory";

                    result.ResultNotifications.Add(notification);
                }
                else
                {
                    if (document.ExemptionNumber.Trim().Length == 0)
                    {
                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = "If the exemption form is approved, an exemption number is mandatory";

                        result.ResultNotifications.Add(notification);
                    }
                }

                #endregion

                #region Start date

                if (!document.ExemptionStartDate.HasValue)
                {
                    MethodResultNotification notification = new MethodResultNotification();

                    notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                    notification.Message = "If the exemption form is approved, an exemption start date is mandatory";

                    result.ResultNotifications.Add(notification);
                }

                #endregion

                #region End date

                if (!document.ExemptionEndDate.HasValue)
                {
                    MethodResultNotification notification = new MethodResultNotification();

                    notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                    notification.Message = "If the exemption form is approved, an exemption end date is mandatory";

                    result.ResultNotifications.Add(notification);
                }

                #endregion

                #region End date > start date

                if (document.ExemptionStartDate.HasValue && document.ExemptionEndDate.HasValue)
                {
                    if (document.ExemptionStartDate.Value > document.ExemptionEndDate.Value)
                    {
                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = "If the exemption form is approved, the exemption end date must be greater than the start date";

                        result.ResultNotifications.Add(notification);
                    }
                }

                #endregion
            }

            return result;
        }
    }
}
