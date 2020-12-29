namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("blog.categories")]
    public partial class blog_categories
    {

        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        
        [Required]
        public string url { get; set; }


    }
}

