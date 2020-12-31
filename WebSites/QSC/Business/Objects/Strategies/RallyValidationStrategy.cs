using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for RallyValidationStrategy.
   /// </summary>
   internal class RallyValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Rally";

        internal RallyValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

         if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
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

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.CookieDough))
         {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cookie Dough" }));
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

         if (campaignProgram.RunsProgram(CurrentPrograms.FFTTPopcorn))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "From Farm To Table Popcorn" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.PapaJackPopcorn))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Papa Jack Popcorn" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.QSPSavingsPass))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "QSP Savings Pass" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.LeapLabels))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Leap Labels" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.GiftCard))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gift Card" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods30))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pretzel Rods 30%" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods40))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pretzel Rods 40%" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.CoolCards))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cool Cards" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.CookieDough))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gourmet Cookie Dough" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.TastyTreats))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gourmet Tasty Treats" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.TheCureJewelry))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "The Cure Jewelry" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.Tervis))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Tervis" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgramIncludingOnlineAccountDeliveredOnly(CurrentPrograms.CoolCards))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cool Cards" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.DrawPrize))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.DrawPrize.ToString() }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.LargeChart))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pick A Prize" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pick A Prize With # Subs" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.Donations))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Donations.ToString() }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.DepositOnlyExtraService))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Deposit Only / Extra Service" }));
            isValid = false;
         }

         return isValid;
		}
   }
}
