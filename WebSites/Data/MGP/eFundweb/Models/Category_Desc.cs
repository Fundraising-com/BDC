namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category_Desc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Category_ID { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(220)]
        public string Category_Name { get; set; }

        public virtual Category Category { get; set; }

        public virtual Language Language { get; set; }
    }
}
