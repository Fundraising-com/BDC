namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        public Store()
        {
            Partner_Sales_Commission = new HashSet<Partner_Sales_Commission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Store_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Description { get; set; }

        public virtual ICollection<Partner_Sales_Commission> Partner_Sales_Commission { get; set; }
    }
}
