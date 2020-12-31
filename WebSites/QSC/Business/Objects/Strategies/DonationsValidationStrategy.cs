using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class DonationsValidationStrategy : CampaignProgramValidationStrategy
    {
       private const string PROGRAM_NAME = "Donations";

       internal DonationsValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.Donations.ToString()));
                isValid = false;
            }

            return isValid;
        }

    }
}
