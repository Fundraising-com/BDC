using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class DepositOnlyExtraServiceValidationStrategy : CampaignProgramValidationStrategy
    {
        private const string PROGRAM_NAME = "Deposit Only / Extra Service";

        internal DepositOnlyExtraServiceValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.DepositOnlyExtraService.ToString()));
                isValid = false;
            }

            if (dataSet.CampaignProgram.Count == 1)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_ALONE_1, CurrentPrograms.DepositOnlyExtraService.ToString()));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.FFTTPopcorn))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.FFTTPopcorn.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.PapaJackPopcorn))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.PapaJackPopcorn.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods40))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.PretzelRods40.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods30))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.PretzelRods30.ToString() }));
               isValid = false;
            }

         return isValid;
        }

    }
}
