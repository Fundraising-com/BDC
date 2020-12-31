using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for PapaJackPopcornValidationStrategy.
   /// </summary>
   internal class PapaJackPopcornValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Papa Jack Popcorn";

        internal PapaJackPopcornValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].OnlineOnlyPrograms)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CAMPAIGN_NO_ONLINE_ONLY, PROGRAM_NAME));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Magazine.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine Express" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cookie Dough" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Donations))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Donations.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Cumulative))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Cumulative.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Top20Magazines))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Top 20 Magazines" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.Tervis))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Tervis.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.TheCureJewelry))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "The Cure Jewelry" }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.FFTTPopcorn))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "From Farm To Table Popcorn" }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.GiftCard))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gift Card" }));
               isValid = false;
            }

         return isValid;
		}
    }
}
