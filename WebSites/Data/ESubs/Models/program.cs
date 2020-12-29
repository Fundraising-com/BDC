namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("program")]
    public partial class program
    {
        public program()
        {
            program_partner = new HashSet<program_partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int program_id { get; set; }

        [Required]
        [StringLength(50)]
        public string program_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<program_partner> program_partner { get; set; }
    }
}
