namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organization_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Organization_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Organization_Type_Desc { get; set; }

        public int Party_Type_ID { get; set; }

        [StringLength(50)]
        public string Organization_Type_Desc_Fr { get; set; }
    }
}
