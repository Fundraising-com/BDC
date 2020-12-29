namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Category_Desc = new HashSet<Category_Desc>();
            Category_Package = new HashSet<Category_Package>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Category_ID { get; set; }

        [StringLength(50)]
        public string Category_Key { get; set; }

        public bool Is_Combo_Item { get; set; }

        [StringLength(100)]
        public string Scratchcard_Image { get; set; }

        public virtual ICollection<Category_Desc> Category_Desc { get; set; }

        public virtual ICollection<Category_Package> Category_Package { get; set; }
    }
}
