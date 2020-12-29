namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_Class_ID { get; set; }

        public int Division_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(10)]
        public string Product_Code { get; set; }

        [StringLength(100)]
        public string Display_Name { get; set; }

        [StringLength(2000)]
        public string Product_Class_Web_Desc { get; set; }

        [StringLength(50)]
        public string Product_Class_Web_Profit { get; set; }

        [StringLength(50)]
        public string Product_Class_Image { get; set; }

        [StringLength(50)]
        public string Product_Class_Title_Image { get; set; }

        public bool Is_Displayable { get; set; }

        public int? Accounting_Class_ID { get; set; }
    }
}
