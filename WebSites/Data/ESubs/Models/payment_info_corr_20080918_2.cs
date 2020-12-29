namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_info_corr_20080918_2
    {
        public int? group_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postal_address_id { get; set; }

        public int? phone_number_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string payment_name { get; set; }

        [StringLength(100)]
        public string on_behalf_of_name { get; set; }

        [StringLength(100)]
        public string ship_to_name { get; set; }

        [StringLength(50)]
        public string ssn { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int active { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_id { get; set; }

        public bool? done { get; set; }
    }
}
