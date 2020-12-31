using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class _59MinuteFundraiserValidationStrategy : CampaignProgramValidationStrategy
    {
        private const string PROGRAM_NAME = "59 Minute Fundraiser";

        internal _59MinuteFundraiserValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms._59MinuteFundraiser.ToString()));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine Express" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Top20Magazines))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Top 20 Magazine" }));
                isValid = false;
            }

            /*if (dataSet.CampaignProgram.Count == 1)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_ALONE_1, CurrentPrograms._59MinuteFundraiser.ToString()));
                isValid = false;
            }*/

            /*if (campaignProgram.RunsProgram(CurrentPrograms.DrawPrize))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Draw Prize" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChart))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart (Pick-A-Prize)" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart (Pick-A-Prize) with #'s" }));
                isValid = false;
            }*/

            /*if (campaignProgram.RunsProgram(CurrentPrograms.CumulativeMiddleSchool))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative Middle School" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.TimeToBeAmazing))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative" }));
                isValid = false;
            }*/

            return isValid;
        }

    }
}
