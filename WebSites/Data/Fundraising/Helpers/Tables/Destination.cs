namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Destination_ID { get; set; }

        public int? Web_Site_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string URL { get; set; }
    }
}
