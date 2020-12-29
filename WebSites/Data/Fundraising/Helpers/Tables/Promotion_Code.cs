namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;
   using System.Data.Entity.Spatial;

   [Table("promotion_code")]
   public partial class Promotion_Code
   {
      [Key]
      [Dapper.Contrib.Extensions.Key]
      public int Promotion_Code_ID { get; set; }

      [Required]
      [StringLength(25)]
      public string Promotion_Code_Desc { get; set; }

      [StringLength(200)]
      public string Description { get; set; }
      [Required]
      [StringLength(20)]
      public string Code { get; set; }
      [Required]
      public DateTime Created { get; set; }
      [Required]
      public bool Is_Enabled { get; set; }

      public DateTime? Date_Limit { get; set; }

      [StringLength(4)]
      public string Country_Code { get; set; }

      public int Limit_Type { get; set; }
      public int? Quantity_Limit { get; set; }
      public int Scope_Type { get; set; }
      public int Partner_Scope_Type { get; set; }
      public int? Partner_Id { get; set; }
      public int Discount_Type { get; set; }
      public int Minimum_Requirement_Type { get; set; }
      public int? Minimum_Quantity { get; set; }

      public double? Amount_Discount { get; set; }
      public double? Percentage_Discount { get; set; }
      public double? Minimum_Amount { get; set; }
   }
}
