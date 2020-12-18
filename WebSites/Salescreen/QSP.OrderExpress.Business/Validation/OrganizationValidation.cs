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
    public class OrganizationValidation
    {
        public MethodResult ValidateOrganization(OrganizationData organization)
        {
            MethodResult result = new MethodResult();

            if (organization.Name.Trim().Length == 0)
            {
                MethodResultNotification notification = new MethodResultNotification();

                notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                notification.Message = "Organization name is mandatory";

                result.ResultNotifications.Add(notification);
            }

            PostalAddressValidation postalAddressValidation = new PostalAddressValidation();

            MethodResult billingAddressValidation = postalAddressValidation.ValidatePostalAddress(organization.BillingAddress);
            result.Merge(billingAddressValidation, "Business address");

            MethodResult shippingAddressValidation = postalAddressValidation.ValidatePostalAddress(organization.ShippingAddress);
            result.Merge(shippingAddressValidation, "Shipping address");

            return result;
        }
    }
}
