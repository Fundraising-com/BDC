using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class SitePgmTbl
    {
       public string pgmcode { get; set; }
       public string pgmname { get; set; }
       public string pgmdesctxt { get; set; }
       public string EXTPGMDESCTXT { get; set; }
       public string PGMPRFTTXT { get; set; }
      public string PGMGRPCDE { get; set; }
      public int PGMSEQNBR { get; set; }
      public string MENUTXT { get; set; }
      public string IMAGEPRFXNME { get; set; }
      public string IMAGEEXTNME { get; set; }
      public string IMAGEDESCTXT { get; set; }
      public string SHRTFEATTXT { get; set; }
      public string DESCTXT { get; set; }
      public string FEATTXT { get; set; }
      public int OFRMPAGEQTY { get; set; }
      public int XTRNPAGEQTY { get; set; }
      public int PDFFILEQTY { get; set; }
      public bool PAGEORIENTPORTFLG { get; set; }
      public string FEATPGMDESCTXT { get; set; }
      public DateTime XTRNSTRTDTE { get; set; }
      public DateTime XTRNENDDTE { get; set; }
      public string METAKYWDTXT { get; set; }
      public string METADESCTXT { get; set; }
      public string HTMLTITLTXT { get; set; }
      public string SITEMAPDESCTXT { get; set; }
      public string SITEMAPURLTXT { get; set; }

        
    }
}
