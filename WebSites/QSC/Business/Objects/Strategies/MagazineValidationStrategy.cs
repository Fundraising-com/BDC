using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class MagazineValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "Magazine";

		internal MagazineValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magazine.ToString(), "Magazine Express"}));
				isValid = false;
			}
			//if(campaignProgram.RunsProgram(CurrentPrograms.PrizeDimension)) 
			//{
			//	CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magazine.ToString(), "Prize Dimension"}));
			//	isValid = false;
			//}

			//if(campaignProgram.RunsProgram(CurrentPrograms.Gift)) 
			//{
			//	CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magazine.ToString(), CurrentPrograms.Gift.ToString()}));
			//	isValid = false;
			//}

			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magazine.ToString(), "Gift Only"}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { CurrentPrograms.MagazineFullService.ToString(), "Magazine Full Service" }));
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
