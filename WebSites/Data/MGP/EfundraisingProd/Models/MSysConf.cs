namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MSysConf")]
    public partial class MSysConf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Config { get; set; }

        [StringLength(255)]
        public string CHValue { get; set; }

        public int? NValue { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }
    }
}
