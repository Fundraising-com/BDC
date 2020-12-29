namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Interested_In
    {
        public Product_Interested_In()
        {
            Product_Interested_In_Desc1 = new HashSet<Product_Interested_In_Desc>();
            Product_Interested_Partner = new HashSet<Product_Interested_Partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_Interested_In_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Product_Interested_In_Desc { get; set; }

        public virtual ICollection<Product_Interested_In_Desc> Product_Interested_In_Desc1 { get; set; }

        public virtual ICollection<Product_Interested_Partner> Product_Interested_Partner { get; set; }
    }
}
