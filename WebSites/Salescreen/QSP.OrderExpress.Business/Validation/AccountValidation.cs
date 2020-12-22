using System;
using System.Collections.Generic;
using System.Text;

using BusinessCommunication = QSPForm.Business.Communication;
using BusinessObject = QSP.Business.Fulfillment;
using QSPForm.Common;
using QSPForm.Common.DataDef;

namespace QSPForm.Business.Validation
{

    /// <summary>
    /// Handles the validation logic regarding accounts
    /// </summary>
    public class AccountValidation
    {

        #region Methods

        /// <summary>
        /// Validates the account information
        /// </summary>
        /// <param name="account">The account object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsValid property determines if the operation was a success or not.
        /// </param>
        public static void ValidateAccount(BusinessObject.Account account, BusinessCommunication.Notifications notifications)
        {
            if (account.OrganizationId <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Account's organization id is incorrect", null, BusinessCommunication.NotificationType.Error));
            }

            if (account.AccountTypeId <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Account's account type id is incorrect", null, BusinessCommunication.NotificationType.Error));
            }

            if (account.FmId == String.Empty)
            {
                notifications.Add(new BusinessCommunication.Notification("Account's FM id is missing", null, BusinessCommunication.NotificationType.Error));
            }

            // TODO: look for data validation rules in OE code and database SPs
        }

        #endregion

    }
}
