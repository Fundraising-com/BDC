namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_info
    {
        public payment_info()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        public int payment_info_id { get; set; }

        public int? group_id { get; set; }

        public int? postal_address_id { get; set; }

        public int? phone_number_id { get; set; }

        [Required]
        [StringLength(100)]
        public string payment_name { get; set; }

        [StringLength(100)]
        public string on_behalf_of_name { get; set; }

        [StringLength(100)]
        public string ship_to_name { get; set; }

        [StringLength(50)]
        public string ssn { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }

        public int? event_id { get; set; }

        public virtual group group { get; set; }

        public virtual ICollection<payment> payments { get; set; }

        public virtual postal_address postal_address { get; set; }
    }
}
