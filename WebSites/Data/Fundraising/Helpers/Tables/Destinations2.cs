namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Destinations2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Destination_ID { get; set; }

        public int? Web_Site_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string URL { get; set; }
    }
}
