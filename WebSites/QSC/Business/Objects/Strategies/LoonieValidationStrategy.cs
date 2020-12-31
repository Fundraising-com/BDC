using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for LoonieValidationStrategy.
	/// </summary>
	internal class LoonieValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Loonie Magazine Promotion";
		
		internal LoonieValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;
			//int nCountMag = 0;
			if(dataSet.CampaignProgram.Count == 1) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITHOUT_2 ,new string[] {PROGRAM_NAME, "Magazine Program"}));
				isValid = false;
			}

			if(!campaignProgram.RunsProgram(CurrentPrograms.Magazine)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CAMPAIGN_PROGRAM_MINIMUM_0, PROGRAM_NAME));
				isValid = false;

			}
			if(campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Magazine Express Program"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.KanataExtremeRewardsProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Kanata Extreme Rewards Program"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.SweetSensations)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Sweet Sensations"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.CooksCollection)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Cook's Collection"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.TreasureQuest)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Cumulative (Treasure Quest)"}));
				isValid = false;
			}
			return isValid;
		}
	}
}
