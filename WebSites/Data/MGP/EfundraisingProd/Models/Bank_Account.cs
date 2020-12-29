namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bank_Account
    {
        public Bank_Account()
        {
            deposits = new HashSet<deposit>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Bank_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Bank_Account_No { get; set; }

        [Required]
        [StringLength(10)]
        public string Currency_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string GL_Account_No { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual ICollection<deposit> deposits { get; set; }
    }
}
