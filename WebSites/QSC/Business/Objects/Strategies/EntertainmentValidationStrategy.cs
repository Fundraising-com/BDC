using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
    internal class EntertainmentValidationStrategy : CampaignProgramValidationStrategy
    {
        private const string PROGRAM_NAME = "Entertainment";

        internal EntertainmentValidationStrategy(Message messageManager) : base(messageManager) { }

        internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
        {
            bool isValid = true;

            return isValid;
        }

    }
}
