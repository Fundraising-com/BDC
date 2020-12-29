namespace GA.BDC.Data.MGP.fastfundraising.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_samplerequest
    {
        [StringLength(100)]
        public string orgname { get; set; }

        [StringLength(50)]
        public string contactname { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        [StringLength(50)]
        public string zip { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(255)]
        public string emailaddr { get; set; }

        [StringLength(50)]
        public string grouptype { get; set; }

        [StringLength(50)]
        public string numberofsellers { get; set; }

        [StringLength(50)]
        public string desiredprofit { get; set; }

        [StringLength(50)]
        public string desiredprofit2 { get; set; }

        [StringLength(8000)]
        public string notes { get; set; }

        [StringLength(50)]
        public string processedby { get; set; }

        public DateTime? processeddatetime { get; set; }

        public DateTime? requestdatetime { get; set; }

        [StringLength(50)]
        public string requeststring { get; set; }

        public int? cookieid { get; set; }

        [StringLength(50)]
        public string besttimetocall { get; set; }

        public int? batchid { get; set; }

        [StringLength(100)]
        public string hearaboutus { get; set; }

        public int? callyesno { get; set; }

        [Key]
        public int srid { get; set; }
    }
}
