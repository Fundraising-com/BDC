namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Country_ID { get; set; }

        [Column("Price", TypeName = "numeric")]
        public decimal Price1 { get; set; }

        public int? Profit_Percentage { get; set; }

        public virtual Product Product { get; set; }
    }
}
