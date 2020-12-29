namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fedex")]
    public partial class fedex
    {
        [Key]
        public int fedex_id { get; set; }

        [StringLength(25)]
        public string fedex_uid { get; set; }

        [StringLength(35)]
        public string company_name { get; set; }

        [StringLength(35)]
        public string contact_name { get; set; }

        [StringLength(35)]
        public string address_line_1 { get; set; }

        [StringLength(35)]
        public string address_line_2 { get; set; }

        [StringLength(35)]
        public string city { get; set; }

        [StringLength(2)]
        public string province_state { get; set; }

        [StringLength(2)]
        public string country { get; set; }

        [StringLength(10)]
        public string zip_postal_code { get; set; }

        [StringLength(10)]
        public string telephone { get; set; }

        [StringLength(5)]
        public string extention { get; set; }

        [StringLength(15)]
        public string tax_id_ssn { get; set; }

        public int? fedex_account { get; set; }

        [StringLength(120)]
        public string shipalert_email_address { get; set; }

        [StringLength(450)]
        public string shipalert_email_message { get; set; }

        public int? shipalert_email_option { get; set; }

        public int? total_package_weight { get; set; }

        public int? number_of_packages { get; set; }

        public int? dimension_height { get; set; }

        public int? dimension_width { get; set; }

        public int? dimension_length { get; set; }

        [StringLength(3)]
        public string sevice_level { get; set; }

        public int? bill_freight_charges_to { get; set; }

        [StringLength(148)]
        public string inter_part_description { get; set; }

        public decimal? inter_unit_value { get; set; }

        [StringLength(3)]
        public string inter_currency { get; set; }

        [StringLength(3)]
        public string inter_unit_of_measure { get; set; }

        public int? inter_quantity { get; set; }

        [StringLength(2)]
        public string inter_country_of_manufacture { get; set; }

        public long? inter_harmonized_code { get; set; }

        [StringLength(20)]
        public string inter_part_number { get; set; }

        [StringLength(15)]
        public string inter_marks_number { get; set; }

        [StringLength(15)]
        public string inter_sku_upc_item { get; set; }

        public int? inter_bill_duties_taxes_to { get; set; }

        public DateTime? inter_create_date { get; set; }

        [StringLength(127)]
        public string inter_tracking_number { get; set; }

        public DateTime? inter_label_date_shipped_date { get; set; }

        public DateTime? inter_update_sale_date { get; set; }

        public decimal? inter_shipping_quote { get; set; }

        public short? cancelled { get; set; }

        public decimal? cod_amount { get; set; }

        public short? cod_payment_method { get; set; }
    }
}
