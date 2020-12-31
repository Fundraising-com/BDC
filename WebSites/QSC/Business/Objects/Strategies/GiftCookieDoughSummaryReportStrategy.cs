using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Objects.Strategies
{
    /// <summary>
    /// Summary description for MagSummaryReportStrategy.
    /// </summary>
    [SummaryFormsReport(SummaryReports.GiftCookieDoughSummary)]
    public class GiftCookieDoughSummaryReportStrategy : SummaryFormsReportStrategy
    {
        public override bool Validate(CampaignProgram campaignProgram)
        {
            return ((campaignProgram.RunsProgram(CurrentPrograms.Gift) || campaignProgram.RunsProgram(CurrentPrograms.Candles)) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough) &&
                !campaignProgram.Campaign.dataSet.Campaign[0].IsStaffOrder &&
                !campaignProgram.RunsProgram(CurrentPrograms.Dollars20GiftCardCoupon) &&
                !campaignProgram.RunsProgram(CurrentPrograms.Magazine) &&
                !campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress) &&
                !campaignProgram.RunsProgram(CurrentPrograms.Magnet) &&
                !campaignProgram.RunsProgram(CurrentPrograms.SweetSensations) &&                
                !campaignProgram.RunsProgram(CurrentPrograms.CooksCollection) &&
                !campaignProgram.RunsProgram(CurrentPrograms.MagazineFullService));
        }
    }
}
