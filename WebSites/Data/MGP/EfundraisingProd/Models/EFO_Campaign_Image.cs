namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Campaign_Image
    {
        [Key]
        public int Campaign_Image_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Image_Catalog_Path { get; set; }

        [Required]
        [StringLength(50)]
        public string Image_Catalog_Path_Sel { get; set; }

        public int Catalog_Category_ID { get; set; }

        public bool Is_Personalized { get; set; }

        public virtual EFO_Catalog_Category EFO_Catalog_Category { get; set; }
    }
}
