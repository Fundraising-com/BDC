namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string Client_Sequence_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Client_ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Organization_Class_Code { get; set; }

        public int? Group_Type_ID { get; set; }

        [Required]
        [StringLength(4)]
        public string Channel_Code { get; set; }

        public int Promotion_ID { get; set; }

        public int? Lead_ID { get; set; }

        public int Division_ID { get; set; }

        [StringLength(10)]
        public string Salutation { get; set; }

        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Last_name { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        [StringLength(20)]
        public string Day_Phone { get; set; }

        [StringLength(45)]
        public string Day_Time_Call { get; set; }

        [StringLength(20)]
        public string Evening_Phone { get; set; }

        [StringLength(20)]
        public string Evening_Time_Call { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(2000)]
        public string Extra_Comment { get; set; }

        public bool Interested_In_Agent { get; set; }

        public int? CSR_Consultant_ID { get; set; }

        public bool Interested_In_Online { get; set; }

        [StringLength(10)]
        public string Day_Phone_Ext { get; set; }

        [StringLength(10)]
        public string Evening_Phone_Ext { get; set; }

        [StringLength(20)]
        public string Other_Phone { get; set; }

        [StringLength(10)]
        public string Other_Phone_Ext { get; set; }

        public int? Title_ID { get; set; }
    }
}
