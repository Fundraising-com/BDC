namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_profit_backup
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int order_profit_id { get; set; }

        public int? event_id { get; set; }

        public int? event_participation_id { get; set; }

        public int? order_id { get; set; }

        public DateTime? order_date { get; set; }

        public double? item_price { get; set; }

        public double? profit { get; set; }

        public double? total_profit { get; set; }

        public int? payment_id { get; set; }

        public int? order_item_id { get; set; }

        public int? order_status_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime create_date { get; set; }

        public DateTime? update_date { get; set; }
    }
}
