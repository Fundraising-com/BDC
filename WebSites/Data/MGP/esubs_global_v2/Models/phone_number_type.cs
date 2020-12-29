namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class phone_number_type
    {
        public phone_number_type()
        {
            member_phone_number = new HashSet<member_phone_number>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int phone_number_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string phone_number_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<member_phone_number> member_phone_number { get; set; }
    }
}
