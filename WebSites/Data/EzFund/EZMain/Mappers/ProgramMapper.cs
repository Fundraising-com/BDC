using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public class ProgramMapper
    {
        public static Product Hydrate(site_pgm_tbl program)
        {
            return new Product
            {
                Code = program.PGM_CDE,
                Name = program.PGM_NME,
                Description = program.DESC_TXT,
                Url = program.CLEAN_URL,
                Image = new Image
                {
                    Url = $"{program.IMAGE_PRFX_NME}_SM.{program.IMAGE_EXT_NME}",
                    AlternativeText = program.IMAGE_DESC_TXT
                },
                BannerImage = new Image
                {
                    Url = program.IMAGE_BANNER
                },
                Category = new Category
                {
                    Name = program.PGM_GRP_CDE
                },
                ExtraInformation = program.FEAT_TXT != null ? program.FEAT_TXT : "",//order info
                METADescription = program.META_DESC_TXT,
                METAKeywords = program.META_KYWD_TXT,
                METATitle = program.HTML_TITL_TXT
            };
        }
    }
}
