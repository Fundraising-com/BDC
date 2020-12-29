namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tellafriend")]
    public partial class tellafriend
    {
        public tellafriend()
        {
            tellafriend_recipient = new HashSet<tellafriend_recipient>();
        }

        [Key]
        public int tellafriend_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string subject { get; set; }

        [StringLength(8000)]
        public string body_html { get; set; }

        [StringLength(8000)]
        public string body_txt { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<tellafriend_recipient> tellafriend_recipient { get; set; }
    }
}
