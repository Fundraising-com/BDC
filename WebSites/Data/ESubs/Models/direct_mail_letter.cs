namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class direct_mail_letter
    {
        [Key]
        [Column(Order = 0)]
        public int direct_mail_letter_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int direct_mail_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string letter_bar_code_1 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(256)]
        public string letter_bar_code_2 { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int letter_type { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime create_date { get; set; }
    }
}
