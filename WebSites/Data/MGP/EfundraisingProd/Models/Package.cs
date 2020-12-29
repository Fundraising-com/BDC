namespace GA.BDC.Data.MGP.EfundraisingProd.Models
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
            Price_Range = new HashSet<Price_Range>();
            scratch_book = new HashSet<scratch_book>();
            SS_Drop_Box_Package = new HashSet<SS_Drop_Box_Package>();
        }

        [Key]
        public int Package_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        [StringLength(50)]
        public string Package_Image { get; set; }

        [StringLength(50)]
        public string Package_Profit { get; set; }

        [Column(TypeName = "text")]
        public string Package_Web_Desc { get; set; }

        [StringLength(50)]
        public string Package_Title_Image { get; set; }

        public bool Is_Displayable { get; set; }

        public int? product_class_id { get; set; }

        public decimal? profit_min { get; set; }

        public decimal? profit_max { get; set; }

        public decimal? profit_default { get; set; }

        public virtual product_class product_class { get; set; }

        public virtual ICollection<Price_Range> Price_Range { get; set; }

        public virtual ICollection<scratch_book> scratch_book { get; set; }

        public virtual ICollection<SS_Drop_Box_Package> SS_Drop_Box_Package { get; set; }
    }
}
