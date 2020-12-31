using System;
using Business.Objects;

namespace Business.Objects.Strategies
{
	/// <summary>
	/// Summary description for MagSummaryReportStrategy.
	/// </summary>
	[SummaryFormsReport(SummaryReports.MagComboSummary)]
	public class MagComboSummaryReportStrategy : SummaryFormsReportStrategy
	{
		public override bool Validate(CampaignProgram campaignProgram)
		{
            return (((campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.Magazine)) && (campaignProgram.RunsProgram(CurrentPrograms.Gift) || campaignProgram.RunsProgram(CurrentPrograms.Candles))) ||
				((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) || campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService)) &&
				(campaignProgram.RunsProgram(CurrentPrograms.CooksCollection) ||
				//!campaignProgram.RunsProgram(CurrentPrograms.AllOccasion) ||
				campaignProgram.RunsProgram(CurrentPrograms.CookieDough))) &&
				!campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder &&
				!campaignProgram.RunsProgram(CurrentPrograms.TreasureQuest)&&
				!campaignProgram.RunsProgram(CurrentPrograms.AllOccasion));
		}
	}
}
