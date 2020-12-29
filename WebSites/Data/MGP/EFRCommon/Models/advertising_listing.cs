namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class advertising_listing
    {
        [Key]
        public int advertising_listing_id { get; set; }

        public int listing_id { get; set; }

        public int advertising_id { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        public DateTime create_date { get; set; }

        public virtual advertising advertising { get; set; }

        public virtual listing listing { get; set; }
    }
}
