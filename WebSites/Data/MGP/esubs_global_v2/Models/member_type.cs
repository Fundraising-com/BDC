namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short member_type_id { get; set; }

        [Required]
        [StringLength(20)]
        public string member_type_name { get; set; }

        [StringLength(100)]
        public string email_description { get; set; }
    }
}
