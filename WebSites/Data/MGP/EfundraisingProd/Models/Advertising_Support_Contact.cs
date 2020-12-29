namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertising_Support_Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertising_Support_Contact_ID { get; set; }

        public int Advertising_Support_ID { get; set; }

        [StringLength(35)]
        public string First_Name { get; set; }

        [StringLength(35)]
        public string Last_Name { get; set; }

        [StringLength(25)]
        public string Phone_Number { get; set; }

        [StringLength(25)]
        public string Fax_Number { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual Advertising_Support Advertising_Support { get; set; }
    }
}
