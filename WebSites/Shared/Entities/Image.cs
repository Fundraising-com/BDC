namespace GA.BDC.Shared.Entities
{
   public class Image
   {
      public int Id { get; set; }
      public string Url { get; set; }
      public string AlternativeText { get; set; }
      public bool IsCover { get; set; }
      public int Order { get; set; }
   }
}
