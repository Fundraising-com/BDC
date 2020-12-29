namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Scratch_Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Scratch_Book_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Raising_Potential { get; set; }

        [Required]
        [StringLength(15)]
        public string Product_Code { get; set; }

        [StringLength(50)]
        public string Current_Description { get; set; }

        public int? Product_Class_ID { get; set; }

        public int? Supplier_ID { get; set; }

        public bool Is_Active { get; set; }

        public int Package_ID { get; set; }

        public int? Order_Taker_ID { get; set; }

        [StringLength(50)]
        public string Small_Image { get; set; }

        [StringLength(50)]
        public string Front_Image { get; set; }

        [StringLength(50)]
        public string Back_Image { get; set; }

        [StringLength(2000)]
        public string Scratch_Booh_Web_Desc { get; set; }

        public bool Is_Displayable { get; set; }

        public int? Total_Qty { get; set; }
    }
}
