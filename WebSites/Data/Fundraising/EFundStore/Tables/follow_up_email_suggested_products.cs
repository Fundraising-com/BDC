

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

     [Table("follow_up_email_suggested_products")]
    public partial class follow_up_email_suggested_products
    {
         [Key]
         public int id { get; set; }
         
         public int package_id { get; set; }

         public int product_image_id { get; set; }

         public string product_url { get; set; }

        
    }
}
