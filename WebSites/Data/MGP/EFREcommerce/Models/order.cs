namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        public order()
        {
            order_detail = new HashSet<order_detail>();
            receipts = new HashSet<receipt>();
        }

        [Key]
        public int order_id { get; set; }

        public int source_id { get; set; }

        public int status_id { get; set; }

        public int billing_postal_address_id { get; set; }

        public int? billing_phone_number_id { get; set; }

        public int? billing_email_id { get; set; }

        public int? ext_participation_id { get; set; }

        public int? recurrence_request_id { get; set; }

        public int? ext_lead_id { get; set; }

        public DateTime order_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? adjustment_amount { get; set; }

        [StringLength(4000)]
        public string comments { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(50)]
        public string ext_order_tracking { get; set; }

        [StringLength(50)]
        public string GUID { get; set; }

        public int? credit_card_id { get; set; }

        public int? order_comment_id { get; set; }

        public virtual credit_card credit_card { get; set; }

        public virtual email email { get; set; }

        public virtual phone_number phone_number { get; set; }

        public virtual postal_address postal_address { get; set; }

        public virtual ICollection<order_detail> order_detail { get; set; }

        public virtual order_comment order_comment { get; set; }

        public virtual source source { get; set; }

        public virtual status status { get; set; }

        public virtual ICollection<receipt> receipts { get; set; }
    }
}
