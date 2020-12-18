using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Business.Validation
{
    public class PostalAddressValidation
    {
        private string zipUSPattern = @"^\d{5}(\-\d{4})?$";
        private string zipCAPattern = @"^[A-Y][0-9][A-Z] {0,1}[0-9][A-Z][0-9]$";
        private string phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$";
        private string emailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public MethodResult ValidatePostalAddress(AddressData address)
        {
            MethodResult result = new MethodResult();

            #region Zip code required and format

            if (address.Zip == null)
            {
                #region Zip is required

                MethodResultNotification notification = new MethodResultNotification();

                notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                notification.Message = "Zip code is mandatory";

                result.ResultNotifications.Add(notification);

                #endregion
            }
            else
            {
                if (address.Zip.Trim().Length == 0)
                {
                    #region Zip is required

                    MethodResultNotification notification = new MethodResultNotification();

                    notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                    notification.Message = "Zip code is mandatory";

                    result.ResultNotifications.Add(notification);

                    #endregion
                }
                else
                {
                    #region Check zip code number format

                    OrderExpressDataContext db = new OrderExpressDataContext();

                    Subdivision subdivision =
                        (
                        from s in db.Subdivisions
                        where s.SubdivisionCode == address.Subdivision.Code
                        select s
                        ).SingleOrDefault();

                    if (subdivision != null)
                    {
                        if (subdivision.CountryCode == "US")
                        {
                            #region Check US zip code

                            Regex regex = new Regex(zipUSPattern, RegexOptions.IgnoreCase);

                            if (!regex.IsMatch(address.Zip.Trim()))
                            {
                                MethodResultNotification notification = new MethodResultNotification();

                                notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                                notification.Message = string.Format("Zip code ({0}) is not in the correct format (##### or #####-####)", address.Zip.Trim());

                                result.ResultNotifications.Add(notification);
                            }

                            #endregion
                        }
                        else if (subdivision.CountryCode == "CA")
                        {
                            #region Check CA zip code

                            Regex regex = new Regex(zipCAPattern, RegexOptions.IgnoreCase);

                            if (!regex.IsMatch(address.Zip.Trim()))
                            {
                                MethodResultNotification notification = new MethodResultNotification();

                                notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                                notification.Message = string.Format("Zip code ({0}) is not in the correct format A#A#A# or A#A #A#)", address.Zip.Trim());

                                result.ResultNotifications.Add(notification);
                            }

                            #endregion
                        }
                        else
                        {
                            #region Only US or CA zip codes are allowed

                            MethodResultNotification notification = new MethodResultNotification();

                            notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                            notification.Message = "Only US or CA zip codes are allowed";

                            result.ResultNotifications.Add(notification);

                            #endregion
                        }
                    }
                    else
                    {
                        #region Only US or CA zip codes are allowed

                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = "Only US or CA zip codes are allowed";

                        result.ResultNotifications.Add(notification);

                        #endregion
                    }

                    #endregion
                }
            }

            #endregion

            #region Phone number required and format

            if (address.Phone == null)
            {
                #region Phone number is required

                MethodResultNotification notification = new MethodResultNotification();

                notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                notification.Message = "Phone number is mandatory";

                result.ResultNotifications.Add(notification);

                #endregion
            }
            else
            {
                if (address.Phone.Trim().Length == 0)
                {
                    #region Phone number is required

                    MethodResultNotification notification = new MethodResultNotification();

                    notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                    notification.Message = "Phone number is mandatory";

                    result.ResultNotifications.Add(notification);

                    #endregion
                }
                else
                {
                    #region Check phone number format

                    Regex regex = new Regex(phonePattern);

                    if (!regex.IsMatch(address.Phone.Trim()))
                    {
                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = string.Format("Phone number ({0}) is not in the correct format (###-###-####)", address.Phone.Trim());

                        result.ResultNotifications.Add(notification);
                    }

                    #endregion
                }
            }

            #endregion

            #region Fax format

            if (address.Fax != null)
            {
                if (address.Fax.Trim().Length == 0)
                {
                }
                else
                {
                    #region Fax number format

                    Regex regex = new Regex(phonePattern);

                    if (!regex.IsMatch(address.Fax.Trim()))
                    {
                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = string.Format("Fax number ({0}) is not in the correct format (###-###-####)", address.Fax.Trim());

                        result.ResultNotifications.Add(notification);
                    }

                    #endregion
                }
            }

            #endregion

            #region Email format

            if (address.Email != null)
            {
                if (address.Email.Trim().Length == 0)
                {
                }
                else
                {
                    #region Email format

                    Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

                    if (!regex.IsMatch(address.Email.Trim()))
                    {
                        MethodResultNotification notification = new MethodResultNotification();

                        notification.NotificationType = MethodResultNotificationTypeEnum.Warning;
                        notification.Message = string.Format("Email ({0}) is not valid", address.Email.Trim());

                        result.ResultNotifications.Add(notification);
                    }

                    #endregion
                }
            }

            #endregion

            return result;
        }

    }
}
