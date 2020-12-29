namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Field_Sales_Manager
    {
        public Field_Sales_Manager()
        {
            FSM_Address = new HashSet<FSM_Address>();
            Organizations = new HashSet<Organization>();
        }

        [Key]
        public int FSM_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string QSP_ID { get; set; }

        public int? Area_Manager_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(15)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(15)]
        public string Home_Phone { get; set; }

        [StringLength(15)]
        public string Work_Phone { get; set; }

        [StringLength(15)]
        public string Fax_Number { get; set; }

        [StringLength(15)]
        public string Toll_Free_Phone { get; set; }

        [StringLength(15)]
        public string Mobile_Phone { get; set; }

        [StringLength(15)]
        public string Pager_Phone { get; set; }

        [StringLength(30)]
        public string Region { get; set; }

        public virtual Area_Manager Area_Manager { get; set; }

        public virtual ICollection<FSM_Address> FSM_Address { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
