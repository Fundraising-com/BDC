using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class TestimonialMapper
    {

        public static Testimonial Hydrate(site_testml_tbl row)
        {
            return new Testimonial
            {
                Id = row.TESTML_ID,
                Created = row.CREA_DTE,
                Author =  row.CTCT_NME,
                Message = row.TESTML_TXT,
                Account = row.CREA_PRSN_CDE
            };
        }

    }
}
