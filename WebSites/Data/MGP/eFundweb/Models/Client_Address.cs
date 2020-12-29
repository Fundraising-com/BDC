namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Address_ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Client_Sequence_Code { get; set; }

        public int Client_ID { get; set; }

        [Required]
        [StringLength(2)]
        public string Address_Type { get; set; }

        [StringLength(100)]
        public string Street_Address { get; set; }

        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Country_Code { get; set; }

        [StringLength(100)]
        public string Attention_of { get; set; }
    }
}
