namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Audit
    {
        [Key]
        public int AUDIT_ID { get; set; }

        [Required]
        [StringLength(2)]
        public string AUDIT_OPERATION { get; set; }

        [StringLength(50)]
        public string HOST { get; set; }

        [Required]
        [StringLength(50)]
        public string AUDIT_USERID { get; set; }

        public DateTime AUDIT_DATETIME { get; set; }

        public int? sales_id { get; set; }

        public int? consultant_id { get; set; }

        public byte? carrier_id { get; set; }

        public byte? shipping_option_id { get; set; }

        public byte? payment_term_id { get; set; }

        [StringLength(2)]
        public string client_sequence_code { get; set; }

        public int? client_id { get; set; }

        public int? sales_status_id { get; set; }

        public byte? payment_method_id { get; set; }

        public byte? po_status_id { get; set; }

        public int? production_status_id { get; set; }

        public int? sponsor_consultant_id { get; set; }

        public int? ar_consultant_id { get; set; }

        public int? ar_status_id { get; set; }

        public int? lead_id { get; set; }

        public int? billing_company_id { get; set; }

        public byte? upfront_payment_method_id { get; set; }

        public int? confirmer_id { get; set; }

        public int? collection_status_id { get; set; }

        public int? confirmation_method_id { get; set; }

        public int? credit_approval_method_id { get; set; }

        [StringLength(50)]
        public string po_number { get; set; }

        [StringLength(7)]
        public string expiry_date { get; set; }

        public DateTime? sales_date { get; set; }

        public decimal? shipping_fees { get; set; }

        public decimal? shipping_fees_discount { get; set; }

        public DateTime? payment_due_date { get; set; }

        public DateTime? confirmed_date { get; set; }

        public DateTime? scheduled_delivery_date { get; set; }

        public DateTime? scheduled_ship_date { get; set; }

        public DateTime? actual_ship_date { get; set; }

        [StringLength(20)]
        public string waybill_no { get; set; }

        [StringLength(200)]
        public string comment { get; set; }

        public bool? coupon_sheet_assigned { get; set; }

        public decimal? total_amount { get; set; }

        public DateTime? invoice_date { get; set; }

        public DateTime? cancellation_date { get; set; }

        public bool? is_ordered { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? po_received_on { get; set; }

        public bool? is_delivered { get; set; }

        public bool? local_sponsor_found { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? box_return_date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? reship_date { get; set; }

        public decimal? upfront_payment_required { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? upfront_payment_due_date { get; set; }

        public bool? sponsor_required { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? actual_delivery_date { get; set; }

        [Column(TypeName = "text")]
        public string accounting_comments { get; set; }

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

        public bool? is_validated { get; set; }

        public DateTime? promised_due_date { get; set; }

        public bool? general_flag { get; set; }

        public byte? fuelsurcharge { get; set; }

        public bool? is_packed_by_participant { get; set; }

        public int? carrier_tracking_id { get; set; }

        public int? qsp_order_id { get; set; }

        public int? ext_order_id { get; set; }

        public byte[] credit_card_no { get; set; }

        [StringLength(50)]
        public string wfc_invoice_number { get; set; }

        [StringLength(3)]
        public string cvv2 { get; set; }

        public int? po_consultant_commission { get; set; }

        public int? ext_sales_status_id { get; set; }

        [StringLength(20)]
        public string ext_shipping_account_id { get; set; }

        [StringLength(20)]
        public string ext_billing_account_id { get; set; }
    }
}
