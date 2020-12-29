namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Accounting_Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Accounting_Class_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? Rank { get; set; }
    }
}
