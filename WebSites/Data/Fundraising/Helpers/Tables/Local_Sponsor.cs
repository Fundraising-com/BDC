namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Local_Sponsor
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Brand_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Local_Sponsor_ID { get; set; }

        [StringLength(10)]
        public string Salutation { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Middle_Initial { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Street_Address { get; set; }

        [StringLength(50)]
        public string City_Name { get; set; }

        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(10)]
        public string Zip_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(20)]
        public string Day_Phone { get; set; }

        [StringLength(20)]
        public string Day_Time_Call { get; set; }

        [StringLength(20)]
        public string Evening_Phone { get; set; }

        [StringLength(20)]
        public string Evening_Time_Call { get; set; }

        [StringLength(50)]
        public string Alternate_Phone { get; set; }

        [StringLength(50)]
        public string Fax_Number { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime? Approval_Date { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public int Sponsor_Consultant_ID { get; set; }

        public DateTime? Last_Contact { get; set; }

        public int? Local_Sponsor_Steps_Id { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Local_Sponsor_Steps Local_Sponsor_Steps { get; set; }

        public virtual Sponsor_Consultant Sponsor_Consultant { get; set; }

        public virtual State State { get; set; }
    }
}
