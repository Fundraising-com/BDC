namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class Organization
    {
        [Key]
        public int Organization_ID { get; set; }

        public int FSM_ID { get; set; }

        public int? Flag_Pole_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Organization_Name { get; set; }

        public int? Organization_Status_ID { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        public int? Organization_Type_ID { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        public int? Number_of_Members { get; set; }

        public int? Number_of_Class_Rooms { get; set; }

        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Country_Code { get; set; }

        public int? Agent_ID { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual Field_Sales_Manager Field_Sales_Manager { get; set; }

        public virtual Flag_Pole Flag_Pole { get; set; }

        public virtual Organization_Status Organization_Status { get; set; }

        public virtual State State { get; set; }
    }
}
