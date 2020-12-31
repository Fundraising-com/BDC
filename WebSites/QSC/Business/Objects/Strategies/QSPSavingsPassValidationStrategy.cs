using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineFullServiceStrategy.
	/// </summary>
	internal class QSPSavingsPassValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "QSP Savings Pass";

        internal QSPSavingsPassValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
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

            /*if (campaignProgram.GroupProfit(CurrentPrograms.QSPSavingsPass) != 50.00 && (campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.CookieDough) || campaignProgram.RunsProgram(CurrentPrograms.TastyTreats) || campaignProgram.RunsProgram(CurrentPrograms.Tervis) || campaignProgram.RunsProgram(CurrentPrograms.TheCureJewelry) || campaignProgram.RunsProgram(CurrentPrograms.GiftCard)))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_SAVINGSPASS_GROUPPROFIT, PROGRAM_NAME));
               isValid = false;
            }*/

         return isValid;
		}
    }
}
