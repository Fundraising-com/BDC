using System;
using System.Collections.Generic;
using System.Text;

using GA.BDC.Core.eFundraisingCommon;

namespace GA.BDC.Core.ESubsGlobal
{
    public class PartnerProfitDictionary
    {
        private static Dictionary<int, int> partnerProfit;
        public static Dictionary<int, int> GetPartnetProfit()
        {
            if (partnerProfit == null)
            {
                // 1/6/2011: DEPRECATED AND IS NO LONGER USED => throw error to whoever uses it
                throw new ESubsGlobalException("ESubsGlobal: PartnerProfitDictionary.GetPartnetProfit() is no longer in use. Please use EFRCommon.dbo.partner_profit table");
                //Dictionary<int, Profit> profitId_profit_list = Profit.GetProfitDictionary();
                //List<PartnerProfit> partner_profits = PartnerProfit.GetCurrentPartnerProfits();

                //if (profitId_profit_list != null && partner_profits != null)
                //{
                //    partnerProfit = new Dictionary<int, int>();
                //    foreach (PartnerProfit pp in partner_profits)
                //    {
                //        if (pp.PartnerId != int.MinValue && pp.ProfitId != int.MinValue)
                //        {
                //            if (!partnerProfit.ContainsKey(pp.PartnerId))
                //            {
                //                partnerProfit.Add(pp.PartnerId, Convert.ToInt32(profitId_profit_list[pp.ProfitId].ProfitPercentage));
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    throw new ESubsGlobalException ("ESubsGlobal: Failed to instantiate partner profit");
                //}
            }
            return partnerProfit;
        }

    }  
}
