namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_type
    {
        public payment_type()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int payment_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string payment_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<payment> payments { get; set; }
    }
}
