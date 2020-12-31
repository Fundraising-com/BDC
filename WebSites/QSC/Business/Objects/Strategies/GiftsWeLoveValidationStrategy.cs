using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class GiftsWeLoveValidationStrategy : CampaignProgramValidationStrategy
    {
        private const string PROGRAM_NAME = "Dream Big";

        internal GiftsWeLoveValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            if (campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder)
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_NO_STAFF_1, CurrentPrograms.GiftsWeLove.ToString()));
                isValid = false;
            }

            /*if (campaignProgram.RunsProgram(CurrentPrograms._59MinuteFundraiser))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITH_2, new string[] { PROGRAM_NAME, CurrentPrograms._59MinuteFundraiser.ToString() }));
                isValid = false;
            }*/

            if (!campaignProgram.RunsProgram(CurrentPrograms.Magazine) && !campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress))
            {
                CurrentMessageManager.Add(CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_PROGRAM_CANNOT_BE_RUN_WITHOUT_3, new string[] { PROGRAM_NAME, CurrentPrograms.Magazine.ToString(), CurrentPrograms.MagazineExpress.ToString() }));
                isValid = false;
            }

            if (campaignProgram.FieldSupplyPacket(CurrentPrograms.GiftsWeLove) && (campaignProgram.FieldSupplyPacket(CurrentPrograms.CookieDough)))
            {
                CurrentMessageManager.Add(Message.ERRMSG_FIELDSUPPLYPACKET);
                isValid = false;
            }

            return isValid;
        }

    }
}
