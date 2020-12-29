namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Package")]
    public partial class Package
    {
        public Package()
        {
            Category_Package = new HashSet<Category_Package>();
            Package_Desc = new HashSet<Package_Desc>();
            package_product = new HashSet<package_product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Package_ID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(120)]
        public string Image_Path { get; set; }

        public int Package_Profit { get; set; }

        public virtual ICollection<Category_Package> Category_Package { get; set; }

        public virtual ICollection<Package_Desc> Package_Desc { get; set; }

        public virtual ICollection<package_product> package_product { get; set; }
    }
}
