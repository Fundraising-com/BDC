using System;
using Common;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for CookieDoughValidationStrategy.
	/// </summary>
	internal class TastyTreatsValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "Gourmet Tasty Treats";

		internal TastyTreatsValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
                isValid = false;
            }

            if (!(campaignProgram.Campaign.dataSet.Campaign[0].CookieDoughDeliveryDate >= campaignProgram.Campaign.dataSet.Campaign[0].StartDate))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CD_BAD_DATE, PROGRAM_NAME));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gourmet Cookie Dough" }));
               isValid = false;
            }

         return isValid;
		}
	}
}
