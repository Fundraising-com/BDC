namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card
    {
        [Key]
        public int credit_card_id { get; set; }

        public int online_user_id { get; set; }

        public byte credit_card_type_id { get; set; }

        [Column("credit_card")]
        [Required]
        [MaxLength(30)]
        public byte[] credit_card1 { get; set; }

        [Required]
        [StringLength(4)]
        public string last_digits { get; set; }

        public short year_expire { get; set; }

        public byte month_expire { get; set; }

        public DateTime date_created { get; set; }

        public bool removed { get; set; }
    }
}
