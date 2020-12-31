﻿using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class NaturallyGoodValidationStrategy : CampaignProgramValidationStrategy
    {
       private const string PROGRAM_NAME = "Naturally Good";

        internal NaturallyGoodValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
               CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.NaturallyGood.ToString()));
                isValid = false;
            }

            /*if (campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms.GiftsWeLove.ToString() }));
                isValid = false;
            }*/

            return isValid;
        }

    }
}
