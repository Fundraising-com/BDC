namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fffeedback")]
    public partial class fffeedback
    {
        [Key]
        public int fbid { get; set; }

        [StringLength(100)]
        public string fbcustname { get; set; }

        [StringLength(255)]
        public string fbemail { get; set; }

        [StringLength(4000)]
        public string fbmessage { get; set; }

        public DateTime? receiveddate { get; set; }

        public int? replystatus { get; set; }

        public DateTime? replieddate { get; set; }
    }
}
