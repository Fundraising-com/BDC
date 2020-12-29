namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("prize")]
    public partial class prize
    {
        public prize()
        {
            prize_item = new HashSet<prize_item>();
            prize1 = new HashSet<prize>();
            program_partner_prize = new HashSet<program_partner_prize>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_id { get; set; }

        public int prize_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string prize_name { get; set; }

        public DateTime create_date { get; set; }

        public int? parent_prize_id { get; set; }

        public virtual ICollection<prize_item> prize_item { get; set; }

        public virtual ICollection<prize> prize1 { get; set; }

        public virtual prize prize2 { get; set; }

        public virtual prize_type prize_type { get; set; }

        public virtual ICollection<program_partner_prize> program_partner_prize { get; set; }
    }
}
