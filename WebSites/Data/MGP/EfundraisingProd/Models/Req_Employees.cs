namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Req_Employees
    {
        [Key]
        public int Employee_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Employee_Name { get; set; }

        public bool Is_MIS { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public bool Is_Manager { get; set; }

        public bool? Is_Active { get; set; }
    }
}
