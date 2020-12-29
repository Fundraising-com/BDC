using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class PriceRangeMapper
	{
        public static List<Profit> Hydrate(IList<price_range> priceRanges)
        {
            var result = new List<Profit>();
            foreach (var priceRange in priceRanges)
            {
                var profit = new Profit
					 {
                    Min = priceRange.minimum_qty,
                    Max = priceRange.maximum_qty,
                    Price = (double)priceRange.unit_price
                };
                result.Add(profit);
            }
            return result;
        }
    }
}

