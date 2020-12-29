namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FSM_Address
    {
        [Key]
        public int FSM_Address_ID { get; set; }

        public int FSM_ID { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [Required]
        [StringLength(5)]
        public string FSM_Address_Type { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(35)]
        public string Street_Address { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual Field_Sales_Manager Field_Sales_Manager { get; set; }

        public virtual State State { get; set; }
    }
}
