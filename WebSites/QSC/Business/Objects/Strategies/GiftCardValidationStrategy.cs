using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
   /// <summary>
   /// Summary description for GiftCardStrategy.
   /// </summary>
   internal class GiftCardValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Gift Card";

      internal GiftCardValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
         bool isValid = true;

         if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
         {
            CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, PROGRAM_NAME));
            isValid = false;
         }

         return isValid;
		}
    }
}
