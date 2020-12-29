namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salutation")]
    public partial class Salutation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Salutation_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Salutation_Desc { get; set; }

        [StringLength(100)]
        public string Salutation_Fr { get; set; }
    }
}
