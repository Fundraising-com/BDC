namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("partner")]
    public partial class partner
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_group_type_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_subgroup_type_id { get; set; }

        [StringLength(255)]
        public string country_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string partner_name { get; set; }

        [StringLength(255)]
        public string partner_path { get; set; }

        [StringLength(255)]
        public string esubs_url { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string estore_url { get; set; }

        [StringLength(255)]
        public string free_kit_url { get; set; }

        [StringLength(255)]
        public string logo { get; set; }

        [StringLength(255)]
        public string phone_number { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string email_ext { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid guid { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_eligible { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool has_collection_site { get; set; }

        [StringLength(255)]
        public string partner_folder { get; set; }

        public int? partner_password { get; set; }
    }
}
