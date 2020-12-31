using System;
using Common;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for CookieDoughValidationStrategy.
	/// </summary>
	internal class ChocolateValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "Chocolate";

		internal ChocolateValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.Chocolate.ToString()));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Magazine" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Mag Express" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Gift))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Gift" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChart))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Pick A Prize" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Pick A Prize With Nums" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.FreeSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Free Subs" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LoyaltyBonus))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Loyalty Bonus" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Cumulative))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Cumulative" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Hybrid))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Hybrid" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Cookie Dough" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.PrizeC))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Prize C" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Magazine Full Service" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CumulativeMiddleSchool))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.Chocolate.ToString(), "Cumulative (Middle School)" }));
                isValid = false;
            }

			return isValid;
		}
	}
}
