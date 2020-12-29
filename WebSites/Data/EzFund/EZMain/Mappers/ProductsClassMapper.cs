using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public class ProductsClassMapper
    {
        public static ProductClass Hydrate(pros_pdct_lkup_tbl row)
        {
            return new ProductClass
            {
                Code = row.PDCT_CDE,
                Name = row.XTRN_PDCT_NME
            };
        }
    }
}
