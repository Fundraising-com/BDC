namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public Contact()
        {
            Advertisers = new HashSet<Advertiser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Contact_ID { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(20)]
        public string Phone_Number { get; set; }

        [StringLength(10)]
        public string Phone_Ext { get; set; }

        [StringLength(20)]
        public string Street_Address { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(10)]
        public string Zip_Code { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        public virtual ICollection<Advertiser> Advertisers { get; set; }
    }
}
