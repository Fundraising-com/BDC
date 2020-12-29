using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ItemVendorMapper
    {
        public static List<ItemVendor> Hydrate(IList<item_vend_map_tbl> vendors)
        {
            var result = new List<ItemVendor>();
            foreach (var vendor in vendors)
            {
                var item = new ItemVendor
                {
                    ItemCode = vendor.ITEM_CDE,
                    VendorCode = vendor.VEND_CDE,
                    VendorItemCode = vendor.VEND_ITEM_CDE,
                    ItemCostAmount = vendor.ITEM_COST_AMT,
                    ItemSeqNumber = vendor.ITEM_SEQ_NBR
                };
                result.Add(item);
            }
            return result;
        }
    }
}
