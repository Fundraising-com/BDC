using System;
using System.Collections.Generic;
using System.Linq;


namespace GA.BDC.Shared.Entities
{
    public class SiteRefPcklLkupTbl
    {
        /// <summary>
        /// ElemId
        /// </summary>
        public int ElemId { get; set; }

        /// <summary>
        /// Application Product category
        /// </summary>
        public string ApplNme { get; set; }

        /// <summary>
        /// Category Code type
        /// </summary>
        public string ListNme { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string ElemCde { get; set; }

        /// <summary>
        /// Category Text
        /// </summary>
        public string ElemTxt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ElemCdeNbr { get; set; }

        /// <summary>
        /// Category Order sort Number
        /// </summary>
        public int ElemSeqNbr { get; set; }

        /// <summary>
        /// Menu text
        /// </summary>
        public string MenuTxt { get; set; }

        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageNme { get; set; }

        /// <summary>
        /// Image Desc
        /// </summary>
        public string ImageDescTxt { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        public string ShrtFeatTxt { get; set; }

        /// <summary>
        /// Category Description text
        /// </summary>
        public string DescTxt { get; set; }

        /// <summary>
        /// Feat Txt
        /// </summary>
        public string FeatTxt { get; set; }

        /// <summary>
        /// Url category link Txt
        /// </summary>
        public string UrlTxt { get; set; }

        /// <summary>
        /// LoclCde
        /// </summary>
        public string LoclCde { get; set; }

        /// <summary>
        /// enable category start date
        /// </summary>
        public DateTime? XtrnStrtDte { get; set; }

        /// <summary>
        /// disable category end date
        /// </summary>
        public DateTime? XtrnEndDte { get; set; }

        /// <summary>
        /// category meta keyword text
        /// </summary>
        public string MetaKywdTxt { get; set; }

        /// <summary>
        /// category meta description text
        /// </summary>
        public string MetaDescTxt { get; set; }

        /// <summary>
        /// Category Title txt
        /// </summary>
        public string HtmlTitlTxt { get; set; }

        /// <summary>
        /// Site map url text 
        /// </summary>
        public string SiteMapDescTxt { get; set; }

        /// <summary>
        /// Site map url link
        /// </summary>
        public string SiteMapUrlTxt { get; set; }

    }
}
