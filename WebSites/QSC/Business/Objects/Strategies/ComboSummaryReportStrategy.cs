using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Objects.Strategies
{
    /// <summary>
    /// Summary description for ComboSummaryReportStrategy.
    /// </summary>
    [SummaryFormsReport(SummaryReports.ComboSummary)]
    public class ComboSummaryReportStrategy : SummaryFormsReportStrategy
    {
        public override bool Validate(CampaignProgram campaignProgram)
        {
           return true;/*(((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) && campaignProgram.RunsProgram(CurrentPrograms.Candles))
                    || ((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
                    || (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) && campaignProgram.RunsProgram(CurrentPrograms.Candles))
                    || (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
                    || (campaignProgram.RunsProgram(CurrentPrograms.Candles) && campaignProgram.RunsProgram(CurrentPrograms.CookieDough))
                    || ((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) && campaignProgram.RunsProgram(CurrentPrograms.Entertainment))
                    || ((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) && campaignProgram.RunsProgram(CurrentPrograms.Festival))
                    || ((campaignProgram.RunsProgram(CurrentPrograms.Magazine) || campaignProgram.RunsProgram(CurrentPrograms.MagazineExpress)) && campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
                    || (campaignProgram.RunsProgram(CurrentPrograms.CookieDough) && campaignProgram.RunsProgram(CurrentPrograms.Entertainment))
                    || (campaignProgram.RunsProgram(CurrentPrograms.CookieDough) && campaignProgram.RunsProgram(CurrentPrograms.Festival))
                    || (campaignProgram.RunsProgram(CurrentPrograms.CookieDough) && campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
                    || (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) && campaignProgram.RunsProgram(CurrentPrograms.Entertainment))
                    || (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) && campaignProgram.RunsProgram(CurrentPrograms.Festival))
                    || (campaignProgram.RunsProgram(CurrentPrograms.ToRememberThis) && campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
                    || (campaignProgram.RunsProgram(CurrentPrograms.Entertainment) && campaignProgram.RunsProgram(CurrentPrograms.Festival))
                    || (campaignProgram.RunsProgram(CurrentPrograms.Entertainment) && campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove))
                    || (campaignProgram.RunsProgram(CurrentPrograms.Entertainment)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.GiftsWeLove)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.Festival)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.Bloom)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.KitchenCollection)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.Donations)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.NaturallyGood)) //Start shifting everything to using the ComboSummary
                    || (campaignProgram.RunsProgram(CurrentPrograms.LifeIsSweet)) //Start shifting everything to using the ComboSummary
                    );*/
        }
    }
}
