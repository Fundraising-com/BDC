namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class questions_entry_form
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int question_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int web_form_id { get; set; }

        public bool? required { get; set; }

        public int? question_order { get; set; }

        [StringLength(100)]
        public string insert_table { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string insert_column { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string default_value { get; set; }
    }
}
