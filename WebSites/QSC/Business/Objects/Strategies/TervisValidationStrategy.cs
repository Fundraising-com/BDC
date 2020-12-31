using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for TervisValidationStrategy.
   /// </summary>
   internal class TervisValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Tervis";

		internal TervisValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

         if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods40))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pretzel Rods 40%" }));
            isValid = false;
         }

         if (campaignProgram.RunsProgram(CurrentPrograms.PretzelRods30))
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Pretzel Rods 30%" }));
            isValid = false;
         }

         return isValid;
		}
	}
}
