namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class package_templates
    {
        public package_templates()
        {
            packages = new HashSet<package1>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte package_template_id { get; set; }

        [Required]
        [StringLength(50)]
        public string package_template_desc { get; set; }

        public virtual ICollection<package1> packages { get; set; }
    }
}
