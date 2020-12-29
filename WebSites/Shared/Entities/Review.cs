using System;

namespace GA.BDC.Shared.Entities
{
   public class Review
   {
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Name
      /// </summary>
      public string Name { get; set; }
      /// <summary>
      /// Comments
      /// </summary>
      public string Comments { get; set; }
      /// <summary>
      /// Rating
      /// </summary>
      public int Rating { get; set; }
      /// <summary>
      /// Product Id
      /// </summary>
      public int ProductId { get; set; }
      /// <summary>
      /// Sale Id
      /// </summary>
      public int? SaleId { get; set; }
      /// <summary>
      /// Is Approved
      /// </summary>
      public bool IsApproved { get; set; }
      /// <summary>
      /// Created
      /// </summary>
      public DateTime Created { get; set; }
      /// <summary>
      /// NON PERSISTENT
      /// </summary>
      public string ProductName { get; set; }
      /// <summary>
      /// Email Address
      /// </summary>
      public string Email { get; set; }
   }
}
