namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bank")]
    public partial class Bank
    {
        public Bank()
        {
            Bank_Account = new HashSet<Bank_Account>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Bank_ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(100)]
        public string Street_Address { get; set; }

        [StringLength(10)]
        public string State_Code { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip_Code { get; set; }

        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        public virtual ICollection<Bank_Account> Bank_Account { get; set; }
    }
}
