namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("blog.tags")]
    public partial class blog_tags
    {

        [Key]
        public int id { get; set; }
        
        [Required]
        public string name { get; set; }

        [Required]
        public string url { get; set; }


    }
}
