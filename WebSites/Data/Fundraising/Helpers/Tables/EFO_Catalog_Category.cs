namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Catalog_Category
    {
        public EFO_Catalog_Category()
        {
            EFO_Campaign_Image = new HashSet<EFO_Campaign_Image>();
        }

        [Key]
        public int Catalog_Category_ID { get; set; }

        [Required]
        [StringLength(40)]
        public string Description { get; set; }

        public virtual ICollection<EFO_Campaign_Image> EFO_Campaign_Image { get; set; }
    }
}
