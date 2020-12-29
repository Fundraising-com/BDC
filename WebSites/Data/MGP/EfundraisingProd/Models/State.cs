namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        public State()
        {
            Alias_State = new HashSet<Alias_State>();
            Billing_Company = new HashSet<Billing_Company>();
            client_address = new HashSet<client_address>();
            EFO_Sale = new HashSet<EFO_Sale>();
            FSM_Address = new HashSet<FSM_Address>();
            Local_Sponsor = new HashSet<Local_Sponsor>();
            Organizations = new HashSet<Organization>();
            sale_to_add = new HashSet<sale_to_add>();
            State_Tax = new HashSet<State_Tax>();
        }

        [Key]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string State_Name { get; set; }

        public short Avg_Delivery_Days { get; set; }

        public int? Time_Zone_Difference { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(10)]
        public string SAP_State_Code { get; set; }

        public virtual ICollection<Alias_State> Alias_State { get; set; }

        public virtual ICollection<Billing_Company> Billing_Company { get; set; }

        public virtual ICollection<client_address> client_address { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual ICollection<EFO_Sale> EFO_Sale { get; set; }

        public virtual ICollection<FSM_Address> FSM_Address { get; set; }

        public virtual ICollection<Local_Sponsor> Local_Sponsor { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }

        public virtual ICollection<State_Tax> State_Tax { get; set; }
    }
}
