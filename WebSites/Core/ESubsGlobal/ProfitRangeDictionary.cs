using System;
using System.Collections.Generic;
using System.Text;

using GA.BDC.Core.eFundraisingCommon;

namespace GA.BDC.Core.ESubsGlobal
{
    public class ProfitRangeDictionary
    {
        private static Dictionary<int, ProfitRange> profitRange;
        public static Dictionary<int, ProfitRange> GetProfitRange()
        {
            if (profitRange == null)
            {
                List<ProfitRange> profitRange_list = ProfitRange.GetProfitRange();

                if (profitRange_list != null)
                {
                    profitRange = new Dictionary<int, ProfitRange>();
                    foreach (ProfitRange pr in profitRange_list)
                    {
                        profitRange.Add(pr.ProfitID, pr);
                    }
                }
                else
                {
                    throw new ESubsGlobalException("ESubsGlobal: Failed to instantiate ProfitRange object");
                }
            }
            return profitRange;
        }

        public static bool IsProfitRangePercentage(double profit_percentage)
        {
            bool result = false;
            foreach (KeyValuePair<int, ProfitRange> kvp in GetProfitRange())
            {
                if (kvp.Value.ProfitRangePercentage == profit_percentage)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

    }
}
