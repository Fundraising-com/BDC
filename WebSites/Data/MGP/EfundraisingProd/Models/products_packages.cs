namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class products_packages
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte package_id { get; set; }

        public byte? display_order { get; set; }

        public bool displayable { get; set; }

        public virtual package1 package { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
