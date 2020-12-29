using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ItemWarehouseMapper
    {
        public static List<ItemWarehouse> Hydrate(IList<item_whse_map_tbl> warehouse)
        {
            var result = new List<ItemWarehouse>();
            foreach (var ware in warehouse)
            {
                var item = new ItemWarehouse {
                    ItemCode = ware.ITEM_CDE,
                    WarehouseCode = ware.WHSE_CDE,
                    WarehouseItemCode = ware.WHSE_ITEM_CDE,
                    WarehouseItemName = ware.WHSE_ITEM_NME,
                    WarehouseItemSeqNumber = ware.WHSE_ITEM_SEQ_NBR
                };
                result.Add(item);
            }
            return result;
        }
    }
}
