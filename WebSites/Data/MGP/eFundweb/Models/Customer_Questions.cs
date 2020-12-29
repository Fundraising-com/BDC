namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Questions
    {
        [Key]
        public int Customer_Question_ID { get; set; }

        [StringLength(25)]
        public string First_Name { get; set; }

        [StringLength(25)]
        public string Last_Name { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(5)]
        public string Phone_Ext { get; set; }

        [StringLength(75)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Question { get; set; }

        public DateTime Date_Inserted { get; set; }
    }
}
