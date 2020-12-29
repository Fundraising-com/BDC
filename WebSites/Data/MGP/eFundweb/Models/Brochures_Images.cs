namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brochures_Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Brochures_Images_ID { get; set; }

        public int Product_ID { get; set; }

        [StringLength(100)]
        public string Base_Filename { get; set; }

        [StringLength(5)]
        public string File_Ext { get; set; }

        public int? Number_Pages { get; set; }
    }
}
