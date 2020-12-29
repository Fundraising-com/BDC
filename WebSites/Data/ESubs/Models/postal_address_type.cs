namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class postal_address_type
    {
        public postal_address_type()
        {
            member_postal_address = new HashSet<member_postal_address>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postal_address_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string postal_address_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<member_postal_address> member_postal_address { get; set; }
    }
}
