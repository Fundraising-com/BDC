namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_from_esubs_20080414
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_type_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string partner_name { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool has_collection_site { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid guid { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime create_date { get; set; }

        [StringLength(255)]
        public string partner_path { get; set; }

        [StringLength(255)]
        public string esubs_url { get; set; }

        [Column("Partner Name")]
        [StringLength(255)]
        public string Partner_Name1 { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }
    }
}
