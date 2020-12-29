namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Billing_Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Billing_Company_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Billing_Company_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Billing_Company_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Street_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City_Name { get; set; }

        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Zip_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Country_Code { get; set; }

        [Required]
        [StringLength(20)]
        public string Telephone_Number { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string Web { get; set; }

        [Column(TypeName = "text")]
        public string Logo { get; set; }

        [StringLength(20)]
        public string Invoice_Title { get; set; }

        [Column(TypeName = "text")]
        public string Invoice_Footer { get; set; }

        [StringLength(20)]
        public string fax_number { get; set; }

        [StringLength(100)]
        public string logo_path { get; set; }

        public byte culture_id { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual State State { get; set; }
    }
}
