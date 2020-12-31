using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for LeapLabelsStrategy.
	/// </summary>
	internal class LeapLabelsValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Leap Labels";

        internal LeapLabelsValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

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

         return isValid;
		}
    }
}
