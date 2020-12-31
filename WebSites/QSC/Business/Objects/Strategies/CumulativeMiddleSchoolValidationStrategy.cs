using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineValidationStrategy.
	/// </summary>
	internal class CumulativeMiddleSchoolValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Cumulative (Middle School)";

        internal CumulativeMiddleSchoolValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			if(dataSet.CampaignProgram.Count == 1) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_ALONE_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.Campaign.dataSet.Campaign[0].IncentivesDistributionID == 0) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, campaignProgram.Campaign.dataSet.Campaign.IncentivesDistributionIDColumn.Caption));
				isValid = false;
			}

			/*if(campaignProgram.Campaign.dataSet.Campaign[0].IncentivesBillToID != 51004)
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_INCENTIVES_PROGRAM_BILL_TO_ID_2, new string[] {campaignProgram.Campaign.dataSet.Campaign.IncentivesBillToIDColumn.Caption, PROGRAM_NAME}));
				isValid = false;
			}*/

			if(campaignProgram.RunsProgram(CurrentPrograms.DrawPrize)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Draw Prize"}));
				isValid = false;
			}
			
			if(campaignProgram.RunsProgram(CurrentPrograms.LargeChart)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Large Chart (Pick-A-Prize)"}));
				isValid = false;
			}

            if (campaignProgram.RunsProgram(CurrentPrograms.PrizeTime))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Prize Time" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Cumulative))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.Hybrid))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Hybrid" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.GameOn))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative (Game On)" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.LargeChartWithNumSubs))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Large Chart (Pick-A-Prize) with #'s" }));
                isValid = false;
            }

            if (campaignProgram.RunsProgram(CurrentPrograms.GoForTheGold))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, "Cumulative" }));
                isValid = false;
            }

			return isValid;
		}
	}
}
