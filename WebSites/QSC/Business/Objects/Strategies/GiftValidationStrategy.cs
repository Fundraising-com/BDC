using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class GiftValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Gift Program (Taylor's Totes)";

		internal GiftValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;
			
			//if(dataSet.CampaignProgram.Count == 1) 
			//{
			//	CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_ALONE_1, CurrentPrograms.Gift.ToString()));
			//	isValid = false;
			//}

            /*
            if(campaignProgram.RunsProgram(CurrentPrograms.Gift) &&
                //!campaignProgram.RunsProgram(CurrentPrograms.Magazine) &&
                (!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) && !campaignProgram.RunsProgram(CurrentPrograms.CookieDough) && !campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService))) 
            {
                //CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Gift Program", CurrentPrograms.Magazine + " or Magazine Express"}));
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITHOUT_2, new string[] {PROGRAM_NAME, "Magazine Express Or Cookie Dough"}));
                isValid = false;
            }*/

            if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Magazine.ToString() }));
                isValid = false;
            }

            //Temporary while Taylor Totes is considered gift
            /*if (campaignProgram.RunsProgram(CurrentPrograms.Gift) &&
                //!campaignProgram.RunsProgram(CurrentPrograms.Magazine) &&
                ((campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService)) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough)))
            {
                //CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {PROGRAM_NAME, "Gift Program", CurrentPrograms.Magazine + " or Magazine Express"}));
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine Express And Cookie Dough" }));
                isValid = false;
            }*/

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.Gift.ToString()));
				isValid = false;
			}
				
			//if(campaignProgram.RunsProgram(CurrentPrograms.Magazine)) 
			//{
			//	CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Gift.ToString(), CurrentPrograms.Magazine.ToString()}));
			//	isValid = false;
			//}

			if(campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Gift.ToString(), "Gift Card Coupon"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.DiscoverCanadaProgram) &&
				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {CurrentPrograms.Gift.ToString(), "Discover Canada", "Magazine Express"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.GiftOnlyProgram)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Gift.ToString(), "Gift Only"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.SweetSensations)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Gift.ToString(), "Sweet Sensations"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.CooksCollection)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {CurrentPrograms.Gift.ToString(), "Cook's Collection"}));
				isValid = false;
			}

//			if(campaignProgram.RunsProgram(CurrentPrograms.TreasureQuest) &&
//				!campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
//			{
//				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_WITHOUT_3, new string[] {CurrentPrograms.Gift.ToString(), "Cumulative (Treasure Quest)", "Magazine Express"}));
//				isValid = false;
//			}

			return isValid;
		}
	}
}
