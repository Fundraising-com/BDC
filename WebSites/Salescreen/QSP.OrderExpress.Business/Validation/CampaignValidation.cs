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
    /// Handles the validation logic regarding campaigns
    /// </summary>
    public class CampaignValidation
    {

        #region Methods

        /// <summary>
        /// Validates the campaign information
        /// </summary>
        /// <param name="campaign">The campaign object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsValid property determines if the operation was a success or not.
        /// </param>
        public static void ValidateCampaign(BusinessObject.Campaign campaign, BusinessCommunication.Notifications notifications)
        {
            if (campaign.AccountId <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's account id is incorrect", null, BusinessCommunication.NotificationType.Error));
            }

            if (campaign.CampaignName == String.Empty)
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's name is missing", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (campaign.CampaignName.Length > 100)
                {
                    notifications.Add(new BusinessCommunication.Notification("Campaign's name is too long. Maximum length of 100 characters allowed.", null, BusinessCommunication.NotificationType.Error));
                }
            }

            if (campaign.StartDate == null || campaign.StartDate == new DateTime())
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's start date is missing", null, BusinessCommunication.NotificationType.Error));
            }

            if (campaign.EndDate == null || campaign.StartDate == new DateTime())
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's end date is missing", null, BusinessCommunication.NotificationType.Error));
            }

            if (campaign.ProgramTypeId <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's program type id is incorrect", null, BusinessCommunication.NotificationType.Error));
            }

            if (campaign.FiscalYear <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Campaign's fiscal year is incorrect", null, BusinessCommunication.NotificationType.Error));
            }

            if ((campaign.StartDate != null || campaign.StartDate == new DateTime()) && (campaign.EndDate != null || campaign.StartDate == new DateTime()))
            {
                if (campaign.StartDate > campaign.EndDate)
                {
                    notifications.Add(new BusinessCommunication.Notification("Campaign's start date must be before the end date", null, BusinessCommunication.NotificationType.Error));
                }
            }


            // TODO: look for data validation rules in OE code and database SPs
        }

        #endregion

    }
}
