namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rs_lastcommit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int origin { get; set; }

        [MaxLength(36)]
        public byte[] origin_qid { get; set; }

        [MaxLength(36)]
        public byte[] secondary_qid { get; set; }

        public DateTime? origin_time { get; set; }

        public DateTime? commit_time { get; set; }
    }
}
