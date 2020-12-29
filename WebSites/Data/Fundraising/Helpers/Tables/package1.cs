namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("packages")]
    public partial class package1
    {
        public package1()
        {
            package_desc = new HashSet<package_desc>();
            packages1 = new HashSet<package1>();
            partner_packages = new HashSet<partner_packages>();
            products_packages = new HashSet<products_packages>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte package_id { get; set; }

        public byte? parent_package_id { get; set; }

        public byte? package_template_id { get; set; }

        public byte? accounting_class_id { get; set; }

        [Required]
        [StringLength(50)]
        public string package_name { get; set; }

        public byte? profit_percentage { get; set; }

        public byte? display_order { get; set; }

        public bool package_enabled { get; set; }

        public bool contains_products { get; set; }

        public byte nb_participants_per_case { get; set; }

        public virtual accounting_class accounting_class { get; set; }

        public virtual ICollection<package_desc> package_desc { get; set; }

        public virtual package_templates package_templates { get; set; }

        public virtual ICollection<package1> packages1 { get; set; }

        public virtual package1 package { get; set; }

        public virtual ICollection<partner_packages> partner_packages { get; set; }

        public virtual ICollection<products_packages> products_packages { get; set; }
    }
}
