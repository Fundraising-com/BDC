namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sale")]
    public partial class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        public int? Carrier_ID { get; set; }

        public int? Shipping_Option_ID { get; set; }

        public int Payment_Term_ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Client_Sequence_Code { get; set; }

        public int Client_ID { get; set; }

        public int Sales_Status_ID { get; set; }

        public int Payment_Method_ID { get; set; }

        public int? PO_Status_ID { get; set; }

        [StringLength(50)]
        public string PO_Number { get; set; }

        public int Consultant_ID { get; set; }

        public DateTime Sales_Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Shipping_Fees { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Shipping_Fees_Discount { get; set; }

        public DateTime? Payment_Due_Date { get; set; }

        public DateTime? Confirmed_Date { get; set; }

        public DateTime? Scheduled_Delivery_Date { get; set; }

        public DateTime? Scheduled_Ship_Date { get; set; }

        public DateTime? Actual_Ship_Date { get; set; }

        [StringLength(20)]
        public string Waybill_No { get; set; }

        [StringLength(2000)]
        public string Comment { get; set; }

        public bool? Coupon_Sheet_Assigned { get; set; }

        public int? Production_Status_ID { get; set; }

        public int? Billing_Company_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total_Amount { get; set; }

        public int? AR_Status_ID { get; set; }

        public DateTime? Invoice_date { get; set; }

        public DateTime? Cancellation_date { get; set; }

        public bool Is_Ordered { get; set; }

        public DateTime? PO_Received_On { get; set; }

        public bool Is_Delivered { get; set; }

        public bool Local_Sponsor_Found { get; set; }

        public int? Sponsor_Consultant_ID { get; set; }

        public int? AR_Consultant_ID { get; set; }

        public DateTime? Box_Return_Date { get; set; }

        public DateTime? Reship_Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UpFront_Payment_Required { get; set; }

        public DateTime? UpFront_Payment_Due_Date { get; set; }

        public int? UpFront_Payment_Method_ID { get; set; }

        public bool Sponsor_Required { get; set; }

        public DateTime? Actual_Delivery_Date { get; set; }

        [StringLength(2000)]
        public string Accounting_Comments { get; set; }

        public int? Lead_ID { get; set; }

        [StringLength(9)]
        public string SSN_Number { get; set; }

        [StringLength(50)]
        public string SSN_Address { get; set; }

        [StringLength(50)]
        public string SSN_City { get; set; }

        [StringLength(10)]
        public string SSN_State_Code { get; set; }

        [StringLength(10)]
        public string SSN_Country_Code { get; set; }

        [StringLength(10)]
        public string SSN_Zip_Code { get; set; }

        public bool? Is_Validated { get; set; }
    }
}
