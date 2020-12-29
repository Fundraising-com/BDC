namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shipping_group
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int shipping_group_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int shipping_fee_id { get; set; }

        [Column("default")]
        public bool? _default { get; set; }

        public virtual shipping_fee shipping_fee { get; set; }
    }
}
