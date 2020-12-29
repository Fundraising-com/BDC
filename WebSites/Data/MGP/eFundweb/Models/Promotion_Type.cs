namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion_Type
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string Promotion_Type_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Default_Commission_Rate { get; set; }
    }
}
