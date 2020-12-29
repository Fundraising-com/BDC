namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shopping_cart
    {
        [Key]
        public int id { get; set; }
        [StringLength(128)]
        public string user_id { get; set; }
        [StringLength(128)]
        public string anonymous_id { get; set; }

        [StringLength(400)]
        public string comments { get; set; }
        [Required]
        public DateTime created { get; set; }
        [Required]
        public int status { get; set; }

        public int? client_id { get; set; }

       public int? promotion_code_id { get; set; }
    }
}
