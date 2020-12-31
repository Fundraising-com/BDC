using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
    internal class LoyaltyBonusValidationStrategy : CampaignProgramValidationStrategy
	{
        internal LoyaltyBonusValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, "Loyalty Bonus"));
                isValid = false;
            }

            /*
			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
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
            */
			return isValid;
		}
	}
}
