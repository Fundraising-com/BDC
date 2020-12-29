namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Result
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int email_template_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int email_template_type_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string email_template_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string description { get; set; }

        [StringLength(100)]
        public string param_procedure_call { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string from_name { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string from_email_address { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string reply_to_name { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string reply_to_email_address { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string bounce_name { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string bounce_email_address { get; set; }

        [Key]
        [Column(Order = 10)]
        public DateTime create_date { get; set; }
    }
}
