namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class phone_number
    {
        public phone_number()
        {
            member_phone_number = new HashSet<member_phone_number>();
        }

        [Key]
        public int phone_number_id { get; set; }

        [Column("phone_number")]
        [Required]
        [StringLength(50)]
        public string phone_number1 { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<member_phone_number> member_phone_number { get; set; }
    }
}
