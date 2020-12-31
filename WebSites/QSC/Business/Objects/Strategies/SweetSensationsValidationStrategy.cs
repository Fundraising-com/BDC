using System;
using Common;
using Common.TableDef;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class SweetSensationsValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Sweet Sensations";

		internal SweetSensationsValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(campaignProgram.RunsProgram(CurrentPrograms.Magnet)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, CurrentPrograms.Magnet.ToString()}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Gift)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, CurrentPrograms.Gift.ToString()}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.DrawPrize) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Draw Prize", "Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.PlanetaryRewardsProgram) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Planetary Rewards Program", "Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.KanataExtremeRewardsProgram) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Kanata Extreme Rewards Program", "Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Gift Card Coupon"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.DiscoverCanadaProgram) &&
				!campaignProgram.RunsProgram(CurrentPrograms.Magazine) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Discover Canada", CurrentPrograms.Magazine.ToString() + " or Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Gift Only"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.TreasureQuest) &&
				!campaignProgram.RunsProgram(CurrentPrograms.Magazine) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Cumulative (Treasure Quest)", CurrentPrograms.Magazine.ToString() + " or Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.PrizeZone) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Large Chart (Pick-A-Prize)", "Magazine Express"}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs) &&
                !campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] { PROGRAM_NAME, "Large Chart (Pick-A-Prize) with #'s", "Magazine Express" }));
                isValid = false;
            }

			return isValid;
		}
	}
}
