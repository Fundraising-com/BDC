namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SC_SECTION
    {
        [Key]
        public int Section_Id { get; set; }

        [StringLength(100)]
        public string Section_Title { get; set; }

        [StringLength(200)]
        public string Section_Image { get; set; }

        [Column(TypeName = "text")]
        public string Section_Text { get; set; }

        [StringLength(200)]
        public string Section_Template { get; set; }

        [StringLength(100)]
        public string Section_Sub_Title { get; set; }
    }
}
