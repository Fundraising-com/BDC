using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class MagnetValidationStrategy : CampaignProgramValidationStrategy
	{
		internal MagnetValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.Magnet.ToString()));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Gift Card Coupon"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.DiscoverCanadaProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Discover Canada"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Gift Only"}));
				isValid = false;
			}
			
			if(campaignProgram.RunsProgram(CurrentPrograms.SweetSensations)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Sweet Sensations"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.CooksCollection)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Cook's Collection"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.TreasureQuest)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Magnet.ToString(), "Cumulative (Treasure Quest)"}));
				isValid = false;
			}

			return isValid;
		}
	}
}
