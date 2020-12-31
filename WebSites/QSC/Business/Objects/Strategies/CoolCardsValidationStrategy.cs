﻿using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for CoolCardsValidationStrategy.
   /// </summary>
   internal class CoolCardsValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Cool Cards";

        internal CoolCardsValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

         if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
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

         if (campaignProgram.Campaign.dataSet.Campaign[0].CoolCardsBoxes == -1)
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_COOLCARDSBOXES, CurrentPrograms.CoolCards.ToString()));
            isValid = false;
         }

         return isValid;
		}
   }
}
