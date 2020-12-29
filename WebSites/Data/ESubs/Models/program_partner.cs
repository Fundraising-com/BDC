namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class program_partner
    {
        public program_partner()
        {
            program_partner_prize = new HashSet<program_partner_prize>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int program_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [Required]
        [StringLength(255)]
        public string program_url { get; set; }

        public DateTime create_date { get; set; }

        public virtual program program { get; set; }

        public virtual ICollection<program_partner_prize> program_partner_prize { get; set; }
    }
}
