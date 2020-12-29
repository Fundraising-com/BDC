namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mailing_Name
    {
        [Key]
        public int Mailing_Name_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string List_Name { get; set; }

        public int List_ID { get; set; }

        [StringLength(50)]
        public string Contact_Name { get; set; }

        [StringLength(35)]
        public string Title { get; set; }

        [StringLength(60)]
        public string School_Name { get; set; }

        [StringLength(70)]
        public string School_Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(4)]
        public string State_Code { get; set; }

        [StringLength(15)]
        public string Zip { get; set; }

        [StringLength(15)]
        public string Phone_Number { get; set; }

        [StringLength(15)]
        public string Fax_Number { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(2)]
        public string School_Type { get; set; }
    }
}
