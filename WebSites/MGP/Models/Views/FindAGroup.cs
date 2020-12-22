using System.ComponentModel.DataAnnotations;

namespace GA.BDC.Web.MGP.Models.Views
{
   public class FindAGroup
   {
      [Required(AllowEmptyStrings = false), MaxLength(100)]
      public string Name { get; set; }
      [MaxLength(5)]
      public string State { get; set; }
      [Required, MaxLength(3)]
      public string SearchType { get; set; }

      public int? GroupId { get; set; }
   }

   public class FindAGroupResult
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public double Amount { get; set; }
      public string Image { get; set; }
      public string Url { get; set; }
      public string State { get; set; }
   }
}