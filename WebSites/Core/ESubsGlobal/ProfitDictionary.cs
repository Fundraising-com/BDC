using System;
using System.Collections.Generic;
using System.Text;

using GA.BDC.Core.eFundraisingCommon;

namespace GA.BDC.Core.ESubsGlobal
{
    public class ProfitDictionary
    {
        private static Dictionary<int, Profit> profit;
        public static Dictionary<int, Profit> GetProfit()
        {
            if (profit == null)
            {
                Dictionary<int, Profit> profitId_profit_list = Profit.GetProfitDictionary();

                if (profitId_profit_list != null)
                {
                    profit = profitId_profit_list;
                }
                else
                {
                    throw new ESubsGlobalException("ESubsGlobal: Failed to instantiate profit object");
                }
            }
            return profit;
        }

        public static Profit GetProfitByProfitPercentage(double profit_percentage)
        {
            Profit prf = null;
            foreach (KeyValuePair<int, Profit> kvp in GetProfit())
            {
                if (kvp.Value.ProfitPercentage == profit_percentage)
                {
                    prf = kvp.Value;
                    break;
                }
            }
            return prf;
        }

    }
}
