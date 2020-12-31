using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for PlannerOnlyProgramValidationStrategy.
	/// </summary>
	internal class PlannerComboProgramValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Planner Combo";

		internal PlannerComboProgramValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;
			bool isMagCombo = false;

			if(campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.PlannerComboProgram) &&
				(campaignProgram.RunsProgram(CurrentPrograms.Magazine) )) 
			{
				isMagCombo = false;
				isValid = false;
			}
			if(campaignProgram.RunsProgram(CurrentPrograms.PlannerComboProgram) &&
							campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) 
			{
				isMagCombo = true;
				isValid = true;
			}
		
			if (isMagCombo){}
			else
			{
			CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITHOUT_2, new string[] {PROGRAM_NAME, " Magazine Express"}));
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Magnet)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, CurrentPrograms.Magnet.ToString()}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Gift)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, CurrentPrograms.Gift.ToString()}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Gift Card Coupon"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.SweetSensations)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Sweet Sensations"}));
				isValid = false;
			}

			if(campaignProgram.RunsProgram(CurrentPrograms.CooksCollection)) 
			{
				CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] {PROGRAM_NAME, "Cook's Collection"}));
				isValid = false;
			}

			return isValid;
		}
	}
}
