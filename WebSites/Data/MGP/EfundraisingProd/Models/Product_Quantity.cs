namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Quantity
    {
        [Key]
        public int Product_Quantity_ID { get; set; }

        public int Scratch_Book_ID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
