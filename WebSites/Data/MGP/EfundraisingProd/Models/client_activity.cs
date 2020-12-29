namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int client_activity_id { get; set; }

        public int client_id { get; set; }

        [Required]
        [StringLength(2)]
        public string client_sequence_code { get; set; }

        public byte client_activity_type_id { get; set; }

        public DateTime client_activity_date { get; set; }

        public DateTime? completed_date { get; set; }

        [Column(TypeName = "text")]
        public string comments { get; set; }

        public bool is_contacted { get; set; }

        public virtual client client { get; set; }

        public virtual client_activity_type client_activity_type { get; set; }
    }
}
