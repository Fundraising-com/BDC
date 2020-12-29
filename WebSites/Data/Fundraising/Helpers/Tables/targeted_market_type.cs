namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class targeted_market_type
    {
        public targeted_market_type()
        {
            Targeted_Market = new HashSet<Targeted_Market>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int targeted_market_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public bool decision_maker { get; set; }

        public byte group_type_id { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        public virtual group_type group_type { get; set; }

        public virtual ICollection<Targeted_Market> Targeted_Market { get; set; }
    }
}
