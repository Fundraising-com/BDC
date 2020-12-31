using System;
using Common;
using Common.TableDef;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagazineFullServiceStrategy.
	/// </summary>
	internal class StainlessSteelTravelCupValidationStrategy : CampaignProgramValidationStrategy
	{
		private const string PROGRAM_NAME = "Stainless Steel Travel Cup";

        internal StainlessSteelTravelCupValidationStrategy(Message messageManager) : base(messageManager) { }

		internal override bool Validate(Business.Objects.CampaignProgram campaignProgram, CampaignProgramDataSet dataSet)
		{
			bool isValid = true;

			return isValid;
		}
    }
}
