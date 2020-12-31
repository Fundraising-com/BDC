using System;
using Common;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for CookieDoughValidationStrategy.
	/// </summary>
	internal class PrizeCValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "Prize C";

		internal PrizeCValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.PrizeC.ToString()));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Cumulative))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Hybrid))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Hybrid" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChart))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart With Num Subs" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.DrawPrize))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Draw Prize" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CumulativeMiddleSchool))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative (Middle School)" }));
                isValid = false;
            }

			return isValid;
		}
	}
}
