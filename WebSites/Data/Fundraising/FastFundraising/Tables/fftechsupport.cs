namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fftechsupport")]
    public partial class fftechsupport
    {
        [Key]
        public int ticketid { get; set; }

        [StringLength(100)]
        public string tcustname { get; set; }

        [StringLength(255)]
        public string temail { get; set; }

        [StringLength(4000)]
        public string tmessage { get; set; }

        public DateTime? receiveddate { get; set; }

        public int? replystatus { get; set; }

        public DateTime? replieddate { get; set; }
    }
}
