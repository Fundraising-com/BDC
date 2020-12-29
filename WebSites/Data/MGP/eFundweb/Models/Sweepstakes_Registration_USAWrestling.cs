namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sweepstakes_Registration_USAWrestling
    {
        [Key]
        public int Sweepstakes_Registration_ID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(5)]
        public string Phone_Ext { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public bool Is_Transfered { get; set; }

        public DateTime Entry_Date { get; set; }
    }
}
