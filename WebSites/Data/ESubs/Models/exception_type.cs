namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class exception_type
    {
        public exception_type()
        {
            payment_exception_type = new HashSet<payment_exception_type>();
        }

        [Key]
        public int exception_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<payment_exception_type> payment_exception_type { get; set; }
    }
}
