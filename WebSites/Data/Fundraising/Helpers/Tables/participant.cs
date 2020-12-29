namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("participant")]
    public partial class participant
    {
        public participant()
        {
            sales_item = new HashSet<sales_item>();
        }

        [Key]
        public int participant_id { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<sales_item> sales_item { get; set; }
    }
}
