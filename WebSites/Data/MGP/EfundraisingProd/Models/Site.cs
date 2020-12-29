namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Site")]
    public partial class Site
    {
        [Key]
        public int Site_Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Site_Title { get; set; }

        [StringLength(150)]
        public string Site_Content { get; set; }
    }
}
