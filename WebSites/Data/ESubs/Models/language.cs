namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("language")]
    public partial class language
    {
        public language()
        {
            cultures = new HashSet<culture>();
        }

        [Key]
        [StringLength(2)]
        public string language_code { get; set; }

        [Required]
        [StringLength(50)]
        public string language_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<culture> cultures { get; set; }
    }
}
