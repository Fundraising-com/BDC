namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country1
    {
        public Country1()
        {
            Alias_Country_Code = new HashSet<Alias_Country_Code>();
            Billing_Company = new HashSet<Billing_Company>();
            client_address = new HashSet<client_address>();
            EFO_Sale = new HashSet<EFO_Sale>();
            FSM_Address = new HashSet<FSM_Address>();
            Organizations = new HashSet<Organization>();
            sale_to_add = new HashSet<sale_to_add>();
            scratch_book_price_info = new HashSet<scratch_book_price_info>();
            States = new HashSet<State>();
        }

        [Key]
        [StringLength(10)]
        public string Country_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Country_Name { get; set; }

        [StringLength(10)]
        public string Currency_Code { get; set; }

        public virtual ICollection<Alias_Country_Code> Alias_Country_Code { get; set; }

        public virtual ICollection<Billing_Company> Billing_Company { get; set; }

        public virtual ICollection<client_address> client_address { get; set; }

        public virtual ICollection<EFO_Sale> EFO_Sale { get; set; }

        public virtual ICollection<FSM_Address> FSM_Address { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }

        public virtual ICollection<scratch_book_price_info> scratch_book_price_info { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
