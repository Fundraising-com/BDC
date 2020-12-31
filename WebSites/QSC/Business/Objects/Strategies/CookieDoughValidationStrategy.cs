using System;
using Common;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for CookieDoughValidationStrategy.
	/// </summary>
	internal class CookieDoughValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "Cookie Dough";

		internal CookieDoughValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.CookieDough.ToString()));
                isValid = false;
            }

            if (!(campaignProgram.Campaign.dataSet.Campaign[0].CookieDoughDeliveryDate >= campaignProgram.Campaign.dataSet.Campaign[0].StartDate))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CD_BAD_DATE, CurrentPrograms.CookieDough.ToString()));
                isValid = false;
            }

			return isValid;
		}
	}
}
