namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            package_product = new HashSet<package_product>();
            Prices = new HashSet<Price>();
            Product_Desc = new HashSet<Product_Desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        [StringLength(2000)]
        public string Product_Short_Desc { get; set; }

        [StringLength(2000)]
        public string Image_Path { get; set; }

        public short? product_quantity { get; set; }

        public virtual ICollection<package_product> package_product { get; set; }

        public virtual ICollection<Price> Prices { get; set; }

        public virtual ICollection<Product_Desc> Product_Desc { get; set; }
    }
}
