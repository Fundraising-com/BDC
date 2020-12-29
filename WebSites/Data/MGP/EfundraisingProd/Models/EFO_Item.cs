namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Item
    {
        public EFO_Item()
        {
            EFO_Sale_Item = new HashSet<EFO_Sale_Item>();
        }

        [Key]
        public int Item_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Amount2Supplier { get; set; }

        public decimal Amount2Group { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public virtual ICollection<EFO_Sale_Item> EFO_Sale_Item { get; set; }
    }
}
