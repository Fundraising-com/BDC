using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
    /// Summary description for ToRememberThisValidationStrategy.
	/// </summary>
	internal class ToRememberThisValidationStrategy : CampaignProgramValidationStrategy
	{
        private const string PROGRAM_NAME = "To Remember This";

        internal ToRememberThisValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
                isValid = false;
            }

            /*if (campaignProgram.RunsProgram(CurrentPrograms.Festival))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Festival.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.KitchenCollection))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.KitchenCollection.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Bloom))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Bloom.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.GiftsWeLove.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.NaturallyGood))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.NaturallyGood.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LifeIsSweet))
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.LifeIsSweet.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.CookieDough.ToString() }));
                isValid = false;
            }*/

            /*if (campaignProgram.RunsProgram(CurrentPrograms.Magazine))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Magazine.ToString() + " because To Remember This is automatically included with Magazine Express" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.MagazineExpress.ToString() + " because To Remember This is automatically included with Magazine Express" }));
                isValid = false;
            }*/

            /*if (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) &&
                ((campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService)) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough)))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Magazine Express And Cookie Dough" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Candles))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.Candles.ToString() }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.TaylorsTotes))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.TaylorsTotes.ToString() }));
                isValid = false;
            }*/

			/*if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.ToRememberThis.ToString()));
				isValid = false;
			}*/
				
			return isValid;
		}
	}
}
