namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sale_to_add
    {
        public sale_to_add()
        {
            Applicable_Tax_To_Add = new HashSet<Applicable_Tax_To_Add>();
            sales_item_to_add = new HashSet<sales_item_to_add>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sale_to_add_id { get; set; }

        public int? consultant_id { get; set; }

        public byte? payment_method_id { get; set; }

        public byte? po_status_id { get; set; }

        public int? sales_status_id { get; set; }

        public int lead_id { get; set; }

        public byte? payment_term_id { get; set; }

        public byte? carrier_id { get; set; }

        public byte? shipping_option_id { get; set; }

        public byte? upfront_payment_method_id { get; set; }

        [StringLength(50)]
        public string po_number { get; set; }

        [StringLength(16)]
        public string credit_card_no { get; set; }

        [StringLength(7)]
        public string expiry_date { get; set; }

        public DateTime sales_date { get; set; }

        public decimal shipping_fees { get; set; }

        public decimal? shipping_fees_discount { get; set; }

        public DateTime? payment_due_date { get; set; }

        public DateTime? scheduled_delivery_date { get; set; }

        [Column(TypeName = "text")]
        public string comment { get; set; }

        public decimal? total_amount { get; set; }

        public DateTime? confirmed_date { get; set; }

        public decimal? upfront_payment_required { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? upfront_payment_due_date { get; set; }

        public bool is_new { get; set; }

        public bool sponsor_required { get; set; }

        [StringLength(9)]
        public string ssn_number { get; set; }

        [StringLength(50)]
        public string ssn_address { get; set; }

        [StringLength(50)]
        public string ssn_city { get; set; }

        [StringLength(10)]
        public string ssn_state_code { get; set; }

        [StringLength(10)]
        public string ssn_country_code { get; set; }

        [StringLength(10)]
        public string ssn_zip_code { get; set; }

        public virtual ICollection<Applicable_Tax_To_Add> Applicable_Tax_To_Add { get; set; }

        public virtual carrier carrier { get; set; }

        public virtual carrier_shipping_option carrier_shipping_option { get; set; }

        public virtual consultant consultant { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual lead lead { get; set; }

        public virtual payment_method payment_method { get; set; }

        public virtual payment_method payment_method1 { get; set; }

        public virtual payment_term payment_term { get; set; }

        public virtual po_status po_status { get; set; }

        public virtual Sales_Status Sales_Status { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<sales_item_to_add> sales_item_to_add { get; set; }
    }
}
