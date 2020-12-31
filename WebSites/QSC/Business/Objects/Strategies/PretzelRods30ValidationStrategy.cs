using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for PretzelRods30ValidationStrategy.
   /// </summary>
   internal class PretzelRods30ValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Pretzel Rods 30";

        internal PretzelRods30ValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
               isValid = false;
            }

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

            if (campaignProgram.RunsProgram(CurrentPrograms.Cumulative))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Cumulative.ToString() }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.DrawPrize))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Draw Prize" }));
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

            if (campaignProgram.RunsProgram(CurrentPrograms.QSPSavingsPass))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "QSP Savings Pass" }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LeapLabels))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Leap Labels" }));
               isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.GiftCard))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Gift Card" }));
               isValid = false;
            }

         if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods40))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pretzel Rods 40%" }));
            isValid = false;
         }

         /*string province = AccountShippingProvince(campaignProgram);
         if (province != "AB" && province != "SK" && province != "BC" && province != "MB" && province != "ON")
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PRETZEL_RODS_30_PROVINCE, ""));
            isValid = false;
         }*/
         
         return isValid;
		}

      private string AccountShippingProvince(Business.Objects.CampaignProgram campaignProgram)
      {
         int accountID = campaignProgram.Campaign.dataSet.Campaign[0].ShipToAccountID;

         CAccount account = new CAccount();
         account.GetOneById(accountID);

         int AddressListID = account.dataSet.CAccount[0].AddressListID;

         Address address = new Address();
         address.GetAllByAddressListID(AddressListID);

         string province = address.GetOneByType(AddressType.ShipTo).stateProvince;

         return province;
      }
   }
}
