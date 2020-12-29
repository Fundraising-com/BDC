namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_attribute
    {
        public partner_attribute()
        {
            partner_attribute_value = new HashSet<partner_attribute_value>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_attribute_id { get; set; }

        [Required]
        [StringLength(50)]
        public string partner_attribute_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<partner_attribute_value> partner_attribute_value { get; set; }
    }
}
