using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class MagazineExpressValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Magazine Express";

		internal MagazineExpressValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Magazine)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string [] {PROGRAM_NAME, CurrentPrograms.Magazine.ToString()}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string [] {PROGRAM_NAME, "Gift Only"}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.MagazineFullService.ToString() }));
                isValid = false;
            }

            if (campaignProgram.BlackboardPacket() && campaignProgram.Campaign.dataSet.Campaign[0].Lang == "FR")
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CAMPAIGN_BLACKBOARD_FRENCH, PROGRAM_NAME));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Entertainment) && campaignProgram.Campaign.dataSet.Campaign[0].Lang == "FR")
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CAMPAIGN_ENTERTAINMENT_FRENCH, PROGRAM_NAME));
                isValid = false;
            }

			return isValid;
		}
	}
}
