using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for CandlesValidationStrategy.
	/// </summary>
	internal class CandlesValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Candles";

        internal CandlesValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            /*if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Magazine.ToString() }));
                isValid = false;
            }*/

            /*if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.MagazineExpress.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Candles) &&
                ((campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService)) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough)))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine Express And Cookie Dough" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.TaylorsTotes))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.TaylorsTotes.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.ToRememberThis.ToString() }));
                isValid = false;
            }*/

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.Candles.ToString()));
				isValid = false;
			}
				
			return isValid;
		}
	}
}
