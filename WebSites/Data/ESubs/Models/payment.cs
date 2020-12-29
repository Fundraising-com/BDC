namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payment")]
    public partial class payment
    {
        public payment()
        {
            payment_comment = new HashSet<payment_comment>();
            payment_exception_type = new HashSet<payment_exception_type>();
            payment_item = new HashSet<payment_item>();
            payment_payment_status = new HashSet<payment_payment_status>();
        }

        [Key]
        public int payment_id { get; set; }

        public int payment_type_id { get; set; }

        public int payment_info_id { get; set; }

        public int payment_period_id { get; set; }

        public int cheque_number { get; set; }

        public DateTime cheque_date { get; set; }

        [Column(TypeName = "money")]
        public decimal paid_amount { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(50)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string address_1 { get; set; }

        [StringLength(100)]
        public string address_2 { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

        [StringLength(7)]
        public string subdivision_code { get; set; }

        public DateTime create_date { get; set; }

        public int? payment_batch_id { get; set; }

        public bool? is_validated { get; set; }

        public bool? is_processed { get; set; }

        public virtual country country { get; set; }

        public virtual ICollection<payment_comment> payment_comment { get; set; }

        public virtual ICollection<payment_exception_type> payment_exception_type { get; set; }

        public virtual ICollection<payment_item> payment_item { get; set; }

        public virtual payment_batch payment_batch { get; set; }

        public virtual payment_info payment_info { get; set; }

        public virtual payment_period payment_period { get; set; }

        public virtual ICollection<payment_payment_status> payment_payment_status { get; set; }

        public virtual payment_type payment_type { get; set; }

        public virtual subdivision subdivision { get; set; }
    }
}
