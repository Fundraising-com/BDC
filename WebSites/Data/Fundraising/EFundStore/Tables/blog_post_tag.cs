namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("blog.posts_tags")]
    public partial class post_tag
    {


        [Key]
        public int ID { get; set; }
        
        [Required]
        public int post_id { get; set; }
        
        [Required]
        public int tag_id { get; set; }

        
    }
}
