namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("package")]
    public partial class package
    {
        public package()
        {
            package_desc = new HashSet<package_desc>();
            product_package = new HashSet<product_package>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int package_id { get; set; }

        public int? parent_package_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public byte? profit_percentage { get; set; }

        public bool enabled { get; set; }

        public DateTime? create_date { get; set; }

        public virtual ICollection<package_desc> package_desc { get; set; }

        public virtual ICollection<product_package> product_package { get; set; }
    }
}
