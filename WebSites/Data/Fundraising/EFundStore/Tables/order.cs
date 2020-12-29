namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        [Key]
        public int order_id { get; set; }

        public int shopping_cart_id { get; set; }

        public int online_user_id { get; set; }

        public int credit_card_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        public int random_number { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(26)]
        public string order_number { get; set; }

        [Column(TypeName = "numeric")]
        public decimal order_total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal shipping_total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tax_total { get; set; }

        public bool order_submitted { get; set; }

        public DateTime date_created { get; set; }

        public DateTime scheduled_delivery_date { get; set; }
    }
}
