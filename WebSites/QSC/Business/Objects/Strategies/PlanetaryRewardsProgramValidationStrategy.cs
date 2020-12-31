using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class PlanetaryRewardsProgramValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Planetary Rewards Program";

		internal PlanetaryRewardsProgramValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(dataSet.CampaignProgram.Count == 1) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_ALONE_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.DrawPrize)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Draw Prize"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.KanataExtremeRewardsProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Kanata Extreme Rewards Program"}));
				isValid = false;
			}
			
			if(campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Gift Card Coupon"}));
				isValid = false;
			}
			
			if(campaignProgram.RunsProgram(CurrentPrograms.DiscoverCanadaProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Discover Canada"}));
				isValid = false;
			}
			
			if(campaignProgram.RunsProgram(CurrentPrograms.SweetSensations) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Sweet Sensations", "Magazine Express"}));
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

			if(campaignProgram.RunsProgram(CurrentPrograms.PrizeZone)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Large Chart (Pick-A-Prize)"}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.PrizeZone))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart (Pick-A-Prize) with #'s" }));
                isValid = false;
            }

			return isValid;
		}
	}
}
