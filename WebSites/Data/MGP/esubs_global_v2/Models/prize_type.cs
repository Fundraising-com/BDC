namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class prize_type
    {
        public prize_type()
        {
            prizes = new HashSet<prize>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string prize_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<prize> prizes { get; set; }
    }
}
